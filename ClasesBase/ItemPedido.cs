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
        private decimal precio;

        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        private decimal cantidad;

        public decimal Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        private decimal importe;

        public decimal Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        private Articulo articulo;
        public Articulo Articulo
        {
            get
            {
                return articulo;
            }
            set
            {
                articulo = value;
            }
        }

        public ItemPedido(int idItemPed, int id, int idArt, decimal pre, decimal cant, decimal imp, Articulo art) 
        {
            itemPedId = idItemPed;
            ped_Id = id;
            art_Id = idArt;
            precio = pre;
            cantidad = cant;
            importe = imp;
            articulo = art;
        }

        public ItemPedido() { }
    }
}
