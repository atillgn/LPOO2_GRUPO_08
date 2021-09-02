using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Articulo
    {
        private int art_Id;

        public int Art_Id
        {
            get { return art_Id; }
            set { art_Id = value; }
        }
        private string art_Descripcion;

        public string Art_Descripcion
        {
            get { return art_Descripcion; }
            set { art_Descripcion = value; }
        }
        private int fam_Id;

        public int Fam_Id
        {
            get { return fam_Id; }
            set { fam_Id = value; }
        }
        private int um_Id;

        public int Um_Id
        {
            get { return um_Id; }
            set { um_Id = value; }
        }
        private decimal art_Precio;

        public decimal Art_Precio
        {
            get { return art_Precio; }
            set { art_Precio = value; }
        }
        private Boolean art_ManejaStock;

        public Boolean Art_ManejaStock
        {
            get { return art_ManejaStock; }
            set { art_ManejaStock = value; }
        }

        public Articulo(int id, string descripcion, int idFam, int idUM, decimal precio, bool manejaStock) 
        {
            art_Id = id;
            art_Descripcion = descripcion;
            fam_Id = idFam;
            um_Id = idUM;
            art_Precio = precio;
            art_ManejaStock = manejaStock;
        }

        public Articulo()
        {
        }
    }
}
