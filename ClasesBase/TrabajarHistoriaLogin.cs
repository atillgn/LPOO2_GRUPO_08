using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ClasesBase
{
    public class TrabajarHistoriaLogin
    {
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static DataTable traerHistorialMozo()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarHistorialMozo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static DataTable traerHistorialAdmin()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarHistorialAdmin";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }
    }
}
