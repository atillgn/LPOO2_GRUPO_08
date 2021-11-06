using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarUsuario
    {
        //Conexion con la base de datos
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public Usuario traerUsuario()
        {
            Usuario oUsuario = new Usuario();
            oUsuario.Usu_ApellidoNombre = "";
            oUsuario.Usu_Contrasenia = "";
            oUsuario.Usu_NombreUsuario = "";
            return oUsuario;
        }

        public static DataTable traerUsuarios()
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarUsuarios";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return (dt);
        }

        public static DataTable traerMozos()
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarMozos";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return (dt);
        }

        public static Usuario buscarUsuarioById(int usuarioId)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarUsuarioById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", usuarioId);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var oUsuario = transformarUsuario(0, dt);

            return oUsuario;
        }

        private static Usuario transformarUsuario(int posicion, DataTable dt)
        {
            var oUsuarioBuscado = new Usuario();
            oUsuarioBuscado.Usu_Id = (int)dt.Rows[posicion]["Usu_Id"];
            oUsuarioBuscado.Usu_ApellidoNombre = (string)dt.Rows[posicion]["Usu_ApellidoNombre"];
            oUsuarioBuscado.Usu_Contrasenia = (string)dt.Rows[posicion]["Usu_Contrasenia"];
            oUsuarioBuscado.Usu_NombreUsuario = (string)dt.Rows[posicion]["Usu_NombreUsuario"];
            oUsuarioBuscado.Rol_Id = (int)dt.Rows[posicion]["Rol_Id"];
            oUsuarioBuscado.Usu_Img = (string)dt.Rows[posicion]["Usu_Img"];

            return oUsuarioBuscado;
        }

        public static void agregarUsuario(Usuario oUsu)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "AltaUsuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@apellidoNombre", oUsu.Usu_ApellidoNombre);
            cmd.Parameters.AddWithValue("@usuario", oUsu.Usu_NombreUsuario);
            cmd.Parameters.AddWithValue("@pass", oUsu.Usu_Contrasenia);
            cmd.Parameters.AddWithValue("@rol", oUsu.Rol_Id);
            cmd.Parameters.AddWithValue("@img", oUsu.Usu_Img);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public static Usuario buscarUsuarioByNombreUsuario(string sUsu)
        {
            SqlConnection cn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarUsuarioByNombreUsuario";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@usuario", sUsu);
            cmd.Connection = cn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            Usuario oUsuario = null;

            if(dt.Rows.Count != 0)
                oUsuario = transformarUsuario(0, dt);

            return oUsuario;
        }
    }
}
