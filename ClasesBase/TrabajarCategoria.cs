using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarCategoria
    {
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static DataTable TraerCategorias()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Categoria";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static ObservableCollection<Categoria> traerCategoriasObv()
        {
            DataTable dt = TraerCategorias();

            var listaCategorias = new ObservableCollection<Categoria>();
            foreach (DataRow r in dt.Rows)
            {
                listaCategorias.Add(transformarCategoria(r));
            }

            return (listaCategorias);
        }

        private static Categoria transformarCategoria(DataRow dt)
        {
            var oCategoria = new Categoria();
            oCategoria.Cat_Id = (int)dt["Cat_Id"];
            oCategoria.Cat_Descripcion = (string)dt["Cat_Descripcion"];
            return oCategoria;
        }

        public static void agregarCategoria(Categoria oCat)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AltaCategoria";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@descripcion", oCat.Cat_Descripcion);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void borrarCategoria(int id)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BorrarCategoria";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void editarCategoria(Categoria oCat)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ActualizarCategoria";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", oCat.Cat_Id);
            cmd.Parameters.AddWithValue("@des", oCat.Cat_Descripcion);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
