using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace ClasesBase
{
    public class TrabajarItemPedido
    {
        //Conexion con la base de datos
        private static SqlConnection connection()
        {
            return new SqlConnection(ClasesBase.Properties.Settings.Default.MyConnection);
        }

        public static void insertar_ItemPedido(ItemPedido oItemPedido)
        {
            SqlConnection cnn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "InsertarItemPedido";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cnn;

            cmd.Parameters.AddWithValue("@pedId", oItemPedido.Ped_Id);
            cmd.Parameters.AddWithValue("@artId", oItemPedido.Art_Id);
            cmd.Parameters.AddWithValue("@precio", oItemPedido.Precio);
            cmd.Parameters.AddWithValue("@cantidad", oItemPedido.Cantidad);
            cmd.Parameters.AddWithValue("@importe", oItemPedido.Importe);

            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }

        private static ItemPedido transformarItem(int posicion, DataTable dt)
        {
            var oItem = new ItemPedido();
            oItem.ItemPedId = (int)dt.Rows[posicion]["Item_Ped_Id"];
            oItem.Ped_Id = (int)dt.Rows[posicion]["Ped_Id"];
            oItem.Art_Id = (int)dt.Rows[posicion]["Art_Id"];
            oItem.Precio = (decimal)dt.Rows[posicion]["Precio"];
            oItem.Cantidad = (decimal)dt.Rows[posicion]["Cantidad"];
            oItem.Importe = (decimal)dt.Rows[posicion]["Importe"];
            oItem.Articulo = TrabajarArticulos.traerArticulos().Single(j => j.Art_Id == oItem.Art_Id);

            return oItem;
        }

        public static ObservableCollection<ItemPedido> buscarItemByPedidoId(int pedidoId)
        {
            SqlConnection conn = connection();

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "BuscarItemByPedidoId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", pedidoId);
            cmd.Connection = conn;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            var listaItem = new ObservableCollection<ItemPedido>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                var oNuevoItem = transformarItem(i, dt);
                listaItem.Add(oNuevoItem);
            }

            return listaItem;
        }
    }
}
