using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ClasesBase;
using System.Collections.ObjectModel;

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

        public static DataTable TraerHistorial(int id)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarHistorial";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", id);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static ObservableCollection<HistorialLogin> traerHistorialObv(int id)
        {
            DataTable dt = TraerHistorial(id);

            var listaHistorial = new ObservableCollection<HistorialLogin>();
            foreach (DataRow r in dt.Rows)
            {
                listaHistorial.Add(transformarHistorial(r));
            }

            return (listaHistorial);
        }

        private static HistorialLogin transformarHistorial(DataRow dt)
        {
            var oHistorial = new HistorialLogin();
            oHistorial.Log_Id = (int)dt["Log_Id"];
            oHistorial.Log_Descripcion = (string)dt["Log_Descripcion"];
            oHistorial.Log_FechaHora = (DateTime)dt["Log_FechaHora"];
            oHistorial.Usu_Id = (int)dt["Usu_Id"];
            return oHistorial;
        }
    }
}
