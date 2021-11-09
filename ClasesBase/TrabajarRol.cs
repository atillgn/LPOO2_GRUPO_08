using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarRol
    {
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static DataTable traerRoles()
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Rol";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cn;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;
        }

        public static ObservableCollection<Rol> traerRoles2()
        {
            DataTable dt = traerRoles();

            var listaRoles = new ObservableCollection<Rol>();
            foreach (DataRow r in dt.Rows)
            {
                listaRoles.Add(transformarRol(r));
            }

            return listaRoles;
        }

        private static Rol transformarRol(DataRow dt)
        {
            var oRolBuscado = new Rol();
            oRolBuscado.Rol_Id = (int)dt["Rol_Id"];
            oRolBuscado.Rol_Descripcion = (string)dt["Rol_Descripcion"];

            return oRolBuscado;
        }
    }
}
