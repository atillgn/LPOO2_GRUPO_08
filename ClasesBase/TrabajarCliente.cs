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
    }
}
