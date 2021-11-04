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

            var oPedido = transformarPedido(0, dt);

            return oPedido;
        }

        private static Pedido transformarPedido(int posicion, DataTable dt)
        {
            var oPedido = new Pedido();
            oPedido.Mesa_Id = (int)dt.Rows[posicion]["Mesa_Id"];
            oPedido.Ped_Id = (int)dt.Rows[posicion]["Ped_Id"];
            oPedido.Usu_Id = (int)dt.Rows[posicion]["Usu_Id"];
            oPedido.Cli_Id = (int)dt.Rows[posicion]["Cli_Id"];
            oPedido.Ped_FechaEmision = (DateTime)dt.Rows[posicion]["Ped_FechaEmision"];
            oPedido.Ped_fechaEntrega = (DateTime)dt.Rows[posicion]["Ped_FechaEntrega"];
            oPedido.Ped_Comensales = (int)dt.Rows[posicion]["Ped_Comensales"];
            oPedido.Ped_Facturado = (bool)dt.Rows[posicion]["Ped_Facturado"];

            return oPedido;
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

            var oPedido = transformarPedido(0, dt);

            return oPedido;
        }
    }
}
