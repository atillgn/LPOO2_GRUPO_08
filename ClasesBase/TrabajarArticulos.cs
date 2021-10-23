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
                int Fam_Id = (int)r["Familia_Id"];
                string Fam_Descripcion = (string)r["Familia"];
                Familia oFamila = new Familia(Fam_Id, Fam_Descripcion);

                int Cat_Id = (int)r["Categoria_Id"];
                string Cat_Descripcion = (string)r["Categoria"];
                Categoria oCategoria = new Categoria(Cat_Id, Cat_Descripcion);

                int Unidad_Id = (int)r["Familia_Id"];
                string Um_Abreviacion = (string)r["Abreviacion"];
                string Um_Descripcion = (string)r["Unidad_Medida"];
                UnidadMedida oUnidadMedida = new UnidadMedida(Unidad_Id,Um_Descripcion, Um_Abreviacion);
                  
                int Art_Id = (int)r["Id"];
                string Art_Descripcion = (string)r["Descripcion"];
                int AFam_Id = (int)r["Familia_Id"];
                int Um_Id = (int)r["Unidad_Id"];
                decimal Art_Precio = (decimal)r["Precio"];
                bool Art_ManejaStock = (bool)r["Stock"];

                listaArticulo.Add(new Articulo(Art_Id, Art_Descripcion, Art_Precio, Art_ManejaStock, oFamila, oCategoria, oUnidadMedida));
            }
            /*
            ObservableCollection<Articulo> listaArticulo = new ObservableCollection<Articulo>();
            Familia oFamilia = new Familia(1, "messi");
            Categoria oCategoria = new Categoria(2, "adios");
            UnidadMedida oUnidadMedida = new UnidadMedida(3, "maradona", "ms");
            listaArticulo.Add(new Articulo(3, "hola", 5, false, oFamilia, oCategoria, oUnidadMedida));*/
            return listaArticulo;
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

            cmd.Parameters.Add("@codigo", cod);

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

            cmd.Parameters.AddWithValue("@codigo", art.Art_Id);
            cmd.Parameters.AddWithValue("@descripcion", art.Art_Descripcion);
            cmd.Parameters.AddWithValue("@precio", art.Art_Precio);
            cmd.Parameters.AddWithValue("@stock", art.Art_ManejaStock);
            cmd.Parameters.AddWithValue("@familia", art.Fam_Id);
            cmd.Parameters.AddWithValue("@unidad", art.Um_Id);
            cmd.Parameters.AddWithValue("@categoria", art.Cat_Id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
