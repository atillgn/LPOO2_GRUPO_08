using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

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
    }
}
