using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
namespace ClasesBase
{
    public class TrabajarCliente
    {
        //Conexion con la base de datos
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public Cliente traerCliente()
        {
            Cliente oCliente = new Cliente();
            oCliente.Cli_Apellido = "";
            oCliente.Cli_Nombre = "";
            oCliente.Cli_Telefono = "";
            return oCliente;
        }

        public static DataTable traerClientes()
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarClientes";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return (dt);
        }

        public static ObservableCollection<Cliente> traerClientes2()
        {
            DataTable dt = traerClientes();

            ObservableCollection<Cliente> listaCliente = new ObservableCollection<Cliente>();
            foreach (DataRow r in dt.Rows)
            {
                listaCliente.Add(transformarCliente(r));
            }

            return (listaCliente);
        }

        private static Cliente transformarCliente(DataRow r)
        {
            var oNuevoCliente = new Cliente();
            oNuevoCliente.Cli_Id = (int)r["Cli_Id"];
            oNuevoCliente.Cli_Apellido = (string)r["Cli_Apellido"];
            oNuevoCliente.Cli_Nombre = (string)r["Cli_Nombre"];
            oNuevoCliente.Cli_Telefono = (string)r["Cli_Telefono"];
            oNuevoCliente.Cli_Email = (string)r["Cli_Email"];
            oNuevoCliente.Cli_Domicilio = (string)r["Cli_Domicilio"];
            oNuevoCliente.Cli_Dni = (string)r["Cli_Dni"];

            return oNuevoCliente;
        }

        public static Cliente buscarClienteById(int clienteId)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarClienteById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", clienteId);
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var oCliente = transformarCliente(dt.Rows[0]);

            return oCliente;
        }

        public static void borrarCliente(int id)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BorrarCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void agregarCliente(Cliente oCli)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AltaCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@apellido", oCli.Cli_Apellido);
            cmd.Parameters.AddWithValue("@nombre", oCli.Cli_Nombre);
            cmd.Parameters.AddWithValue("@email", oCli.Cli_Email);
            cmd.Parameters.AddWithValue("@telefono", oCli.Cli_Telefono);
            cmd.Parameters.AddWithValue("@domicilio", oCli.Cli_Domicilio);
            cmd.Parameters.AddWithValue("@dni", oCli.Cli_Dni);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void editarCliente(Cliente oCli)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ActualizarCliente";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", oCli.Cli_Id);
            cmd.Parameters.AddWithValue("@apellido", oCli.Cli_Apellido);
            cmd.Parameters.AddWithValue("@nombre", oCli.Cli_Nombre);
            cmd.Parameters.AddWithValue("@email", oCli.Cli_Email);
            cmd.Parameters.AddWithValue("@telefono", oCli.Cli_Telefono);
            cmd.Parameters.AddWithValue("@domicilio", oCli.Cli_Domicilio);
            cmd.Parameters.AddWithValue("@dni", oCli.Cli_Dni);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

    }
}