using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarPedido
    {
        //Conexion con la base de datos
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static void insertar_Pedido(Pedido oPedido)
        {
            SqlConnection cnn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertarPedido";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@usuId", oPedido.Usu_Id);
            cmd.Parameters.AddWithValue("@mesaId", oPedido.Mesa_Id);
            cmd.Parameters.AddWithValue("@cliId", oPedido.Cli_Id);
            cmd.Parameters.AddWithValue("@fechaEmision", oPedido.Ped_FechaEmision);
            cmd.Parameters.AddWithValue("@fechaEntrega", oPedido.Ped_fechaEntrega);
            cmd.Parameters.AddWithValue("@comensales", oPedido.Ped_Comensales);
            cmd.Parameters.AddWithValue("@facturado", oPedido.Ped_Facturado);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static void editar_Pedido(Pedido oPedido)
        {
            SqlConnection cnn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EditarPedido";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@pedidoId", oPedido.Ped_Id);
            cmd.Parameters.AddWithValue("@usuId", oPedido.Usu_Id);
            cmd.Parameters.AddWithValue("@mesaId", oPedido.Mesa_Id);
            cmd.Parameters.AddWithValue("@cliId", oPedido.Cli_Id);
            cmd.Parameters.AddWithValue("@fechaEmision", oPedido.Ped_FechaEmision);
            cmd.Parameters.AddWithValue("@fechaEntrega", oPedido.Ped_fechaEntrega);
            cmd.Parameters.AddWithValue("@comensales", oPedido.Ped_Comensales);
            cmd.Parameters.AddWithValue("@facturado", oPedido.Ped_Facturado);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static Pedido buscarPedidoByFechaEmision(DateTime fecha)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarPedidoByFechaEmision";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            Pedido oPedido = transformarPedido(dt.Rows[0], false);

            return oPedido;
        }

        public static DataTable buscarPedidosSinFacturarHoy()
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarPedidosSinFacturarHoy";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
            cmd.Parameters.AddWithValue("@facturado", false);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return dt;
        }

        public static ObservableCollection<Pedido> listarPedidos()
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarPedidos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var listaPedido = new ObservableCollection<Pedido>();
            foreach (DataRow r in dt.Rows)
            {
                listaPedido.Add(transformarPedido(r, true));
            }

            return listaPedido;
        }

        private static Pedido transformarPedido(DataRow r, bool tipo)
        {
            var oPedido = new Pedido();
            oPedido.Mesa_Id = (int)r["Mesa_Id"];
            oPedido.Ped_Id = (int)r["Ped_Id"];
            oPedido.Usu_Id = (int)r["Usu_Id"];
            oPedido.Cli_Id = (int)r["Cli_Id"];
            oPedido.Ped_FechaEmision = (DateTime)r["Ped_FechaEmision"];
            oPedido.Ped_fechaEntrega = (DateTime)r["Ped_FechaEntrega"];
            oPedido.Ped_Comensales = (int)r["Ped_Comensales"];
            oPedido.Ped_Facturado = (bool)r["Ped_Facturado"];
            if (tipo == true)
            {
                oPedido.OCliente = TrabajarCliente.buscarClienteById(oPedido.Cli_Id);
                oPedido.OMesa = TrabajarMesa.buscarMesaById(oPedido.Mesa_Id);
                oPedido.OMozo = TrabajarUsuario.buscarUsuarioById(oPedido.Usu_Id);
                oPedido.TotalPedido = generarTotal(oPedido.Ped_Id);
            }
            return oPedido;
        }

        private static decimal generarTotal(int pedidoID)
        {
            ObservableCollection<ItemPedido> listaItem = TrabajarItemPedido.buscarItemByPedidoId(pedidoID);
            decimal importeTotal = 0;
            foreach (ItemPedido item in listaItem)
            {
                importeTotal = importeTotal + item.Importe;
            }
            return importeTotal;
        }

        public static Pedido buscarPedidoById(int pedidoId)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarPedidoById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", pedidoId);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var oPedido = transformarPedido(dt.Rows[0], false);

            return oPedido;
        }

        public static Pedido buscarPedidoByMesaAndEstado(int idMesa)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarPedidoByMesaAndEstado";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idMesa", idMesa);
            cmd.Parameters.AddWithValue("@fact", false);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            Pedido oPedido = transformarPedido(dt.Rows[0], false);

            return oPedido;
        }
        
    }
}
