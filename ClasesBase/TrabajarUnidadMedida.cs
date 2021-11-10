using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarUnidadMedida
    {
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static DataTable TraerUnidadMedidas()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM UnidadMedida";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static ObservableCollection<UnidadMedida> traerUnidadMedidaObv()
        {
            DataTable dt = TraerUnidadMedidas();

            var listaUnidadMedida = new ObservableCollection<UnidadMedida>();
            foreach (DataRow r in dt.Rows)
            {
                listaUnidadMedida.Add(transformarUnidadMedida(r));
            }

            return (listaUnidadMedida);
        }

        private static UnidadMedida transformarUnidadMedida(DataRow dt)
        {
            var oUnidad = new UnidadMedida();
            oUnidad.Um_Id = (int)dt["Um_Id"];
            oUnidad.Um_Abrev = (string)dt["Um_Abrev"];
            oUnidad.Um_Descripcion = (string)dt["Um_Descripcion"];
            return oUnidad;
        }

        public static void agregarUnidadMedida(UnidadMedida oUnidad)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AltaUnidadMedida";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@abrev", oUnidad.Um_Abrev);
            cmd.Parameters.AddWithValue("@des", oUnidad.Um_Descripcion);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void borrarUnidadMedida(int id)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BorrarUnidadMedida";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void editarUnidadMedida(UnidadMedida oUnidad)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ActualizarUnidadMedida";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", oUnidad.Um_Id);
            cmd.Parameters.AddWithValue("@abrev", oUnidad.Um_Abrev);
            cmd.Parameters.AddWithValue("@des", oUnidad.Um_Descripcion);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
