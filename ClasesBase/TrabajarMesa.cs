using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarMesa
    {
        //Conexion con la base de datos
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static int contarMesas()
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand("ContarMesas", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = new SqlParameter("@CantidadMesas", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return Convert.ToInt32(cmd.Parameters["@CantidadMesas"].Value);
        }

        public static DataTable traerMesas()
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "ListarMesas";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return (dt);
        }

        public static void editarMesa(Mesa oMesa)
        {
            SqlConnection cnn = connection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "EditarMesa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@mesaId", oMesa.Mesa_Id);
            cmd.Parameters.AddWithValue("@posicion", oMesa.Mesa_Posicion);
            cmd.Parameters.AddWithValue("@estado", oMesa.Mesa_Estado);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        public static Mesa buscarMesaById(int mesaId)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarMesaById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mesaId", mesaId);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var oMesa = transformarMesa(0, dt);

            return oMesa;
        }

        public static Mesa buscarMesaByPosicion(int mesaPosicion)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarMesaByPosicion";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mesaPosicion", mesaPosicion);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var oMesa = transformarMesa(0, dt);

            return oMesa;
        }

        private static Mesa transformarMesa(int posicion, DataTable dt)
        {
            var oMesaBuscada = new Mesa();
            oMesaBuscada.Mesa_Id = (int)dt.Rows[posicion]["Mesa_Id"];
            oMesaBuscada.Mesa_Posicion = (int)dt.Rows[posicion]["Mesa_Posicion"];
            oMesaBuscada.Mesa_Estado = (int)dt.Rows[posicion]["Mesa_Estado"];

            return oMesaBuscada;
        }

        public static ObservableCollection<Mesa> traerMesasObjetos()
        {
            DataTable dt = traerMesas();
            var mesas = new ObservableCollection<Mesa>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var oNuevaMesa = transformarMesa(i, dt);
                mesas.Add(oNuevaMesa);
            }

            return mesas;
        }

        public static DataTable buscarMesaByEstado(int estado)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarMesasByEstado";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado", estado);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            return dt;
        }
    }
}
