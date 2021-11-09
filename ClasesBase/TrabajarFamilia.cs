using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarFamilia
    {
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static DataTable traerFamilias()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarFamilias";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static ObservableCollection<Familia> traerFamiliasObv(){
            DataTable dt = traerFamilias();

            var listaFamilias = new ObservableCollection<Familia>();
            foreach (DataRow r in dt.Rows)
            {
                listaFamilias.Add(transformarFamilia(r));
            }

            return (listaFamilias);
        }

        private static Familia transformarFamilia(DataRow dt){
            var oFamilia = new Familia();
            oFamilia.Fam_Id = (int)dt["Fam_Id"];
            oFamilia.Fam_Descripcion = (string)dt["Fam_Descripcion"];
            return oFamilia;
        }

        public static void agregarFamilia(Familia oFam)
        {
            SqlConnection cn = connection();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AltaFamilia";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@descripcion", oFam.Fam_Descripcion);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void borrarFamilia(int id)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BorrarFamilia";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", id);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static void editarFamilia(Familia oFam)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ActualizarFamilia";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@id", oFam.Fam_Id);
            cmd.Parameters.AddWithValue("@des", oFam.Fam_Descripcion);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}
