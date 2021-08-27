using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class ItemPedido
    {
        private int itemPedId;

        public int ItemPedId
        {
            get { return itemPedId; }
            set { itemPedId = value; }
        }
        private int ped_Id;

        public int Ped_Id
        {
            get { return ped_Id; }
            set { ped_Id = value; }
        }
        private int art_Id;

        public int Art_Id
        {
            get { return art_Id; }
            set { art_Id = value; }
        }
        private float precio;

        public float Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        private float cantidad;

        public float Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private float importe;

        public float Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        public ItemPedido(int idItemPed, int id, int idArt, float pre, float cant, float imp) 
        {
            itemPedId = idItemPed;
            ped_Id = id;
            art_Id = idArt;
            precio = pre;
            cantidad = cant;
            importe = imp;
        }
    }
}
