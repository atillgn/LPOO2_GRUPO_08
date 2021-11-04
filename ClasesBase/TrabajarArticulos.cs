using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarArticulos
    {
        //Conexion con la base de datos
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static ObservableCollection<Articulo> traerArticulos()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MostrarArticulos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            ObservableCollection<Articulo> listaArticulo = new ObservableCollection<Articulo>();
            foreach(DataRow r in dt.Rows)
            {
                listaArticulo.Add(transformarArticulo(r));
            }

            return listaArticulo;
        }

        public static DataTable traerArticulos2()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "MostrarArticulos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        private static Articulo transformarArticulo(DataRow r)
        {
            var oNuevoArticulo = new Articulo();
            oNuevoArticulo.Art_Id = (int)r["Id"];
            oNuevoArticulo.Art_Codigo = (string)r["Codigo"];
            oNuevoArticulo.Art_Descripcion = (string)r["Descripcion"];
            oNuevoArticulo.Art_Precio = (decimal)r["Precio"];
            oNuevoArticulo.Art_ManejaStock = (bool)r["Stock"];
            oNuevoArticulo.Art_Img = (string)r["Imagen"];
            Categoria cat = new Categoria((int)r["Categoria_Id"], (string)r["Categoria"]);
            oNuevoArticulo.ACategoria = cat;
            Familia fam = new Familia((int)r["Familia_Id"], (string)r["Familia"]);
            oNuevoArticulo.AFamilia = fam;
            UnidadMedida uniMed = new UnidadMedida((int)r["Unidad_Id"], (string)r["Unidad_Medida"], (string)r["Abreviacion"]);
            oNuevoArticulo.AUnidadMedida = uniMed;

            return oNuevoArticulo;
        }

        public static void agregarArticulo(Articulo art)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AltaArticulo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@descripcion", art.Art_Descripcion);
            cmd.Parameters.AddWithValue("@precio", art.Art_Precio);
            cmd.Parameters.AddWithValue("@stock", art.Art_ManejaStock);
            cmd.Parameters.AddWithValue("@familia", art.Fam_Id);
            cmd.Parameters.AddWithValue("@unidad", art.Um_Id);
            cmd.Parameters.AddWithValue("@categoria", art.Cat_Id);
            cmd.Parameters.AddWithValue("@codigo", art.Art_Codigo);
            cmd.Parameters.AddWithValue("@imagen", art.Art_Img);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void borrarArticulo(int cod)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BorrarArticulo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", cod);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void editarArticulo(Articulo art)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ActualizarArticulo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", art.Art_Id);
            cmd.Parameters.AddWithValue("@codigo", art.Art_Codigo);
            cmd.Parameters.AddWithValue("@descripcion", art.Art_Descripcion);
            cmd.Parameters.AddWithValue("@precio", art.Art_Precio);
            cmd.Parameters.AddWithValue("@stock", art.Art_ManejaStock);
            cmd.Parameters.AddWithValue("@familia", art.Fam_Id);
            cmd.Parameters.AddWithValue("@unidad", art.Um_Id);
            cmd.Parameters.AddWithValue("@categoria", art.Cat_Id);
            cmd.Parameters.AddWithValue("@imagen", art.Art_Img);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static DataTable traerArticulosPedido()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarArticulosPedido";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return (dt);
        }

        public static Articulo buscarArticuloById(int articuloId)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarArticuloById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", articuloId);
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var oArticulo = transformarArticulo(dt.Rows[0]);

            return oArticulo;
        }
    }
}
