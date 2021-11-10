using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class VerificarLogin
    {
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static DataTable buscarUsuario(string u, string p)
        {
            SqlConnection con = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Login";
            cmd.Parameters.AddWithValue("user", u);
            cmd.Parameters.AddWithValue("pas", p);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            return dt;

        }

        public static void agregarHistorial(HistorialLogin his)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AltaHistorial";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@des", his.Log_Descripcion);
            cmd.Parameters.AddWithValue("@fecha", his.Log_FechaHora);
            cmd.Parameters.AddWithValue("@usuId", his.Usu_Id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
