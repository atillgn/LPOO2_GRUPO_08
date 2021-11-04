using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Articulo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int art_Id;
        public int Art_Id
        {
            get { return art_Id; }
            set { 
                art_Id = value;
                OnPropertyChanged("Art_Id");
            }
        }

        private string art_Codigo;
        public string Art_Codigo
        {
            get { return art_Codigo; }
            set
            {
                art_Codigo = value;
                OnPropertyChanged("Art_Codigo");
            }
        }

        private string art_Descripcion;
        public string Art_Descripcion
        {
            get { return art_Descripcion; }
            set { 
                art_Descripcion = value;
                OnPropertyChanged("Art_Descripcion");
            }
        }
        
        private int fam_Id;
        public int Fam_Id
        {
            get { return fam_Id; }
            set { 
                fam_Id = value;
                OnPropertyChanged("Fam_Id");
            }
        }
        
        private int um_Id;
        public int Um_Id
        {
            get { return um_Id; }
            set { 
                um_Id = value;
                OnPropertyChanged("Um_Id");
            }
        }

        private int cat_Id;
        public int Cat_Id
        {
            get { return cat_Id; }
            set
            {
                cat_Id = value;
                OnPropertyChanged("Cat_Id");
            }
        }
        
        private decimal art_Precio;
        public decimal Art_Precio
        {
            get { return art_Precio; }
            set { 
                art_Precio = value;
                OnPropertyChanged("Art_Precio");
            }
        }
        
        private Boolean art_ManejaStock;
        public Boolean Art_ManejaStock
        {
            get { return art_ManejaStock; }
            set { 
                art_ManejaStock = value;
                OnPropertyChanged("Art_ManejaStock");
            }
        }

        private string art_Img;
        public string Art_Img
        {
            get { return art_Img; }
            set
            {
                art_Img = value;
                OnPropertyChanged("Art_Img");
            }
        }

        private Familia aFamilia;
        public Familia AFamilia
        {
            get { return aFamilia; }
            set {
                aFamilia = value;
                OnPropertyChanged("AFamilia");
            }

        }

        private Categoria aCategoria;
        public Categoria ACategoria
        {
            get { return aCategoria; }
            set
            {
                aCategoria = value;
                OnPropertyChanged("ACategoria");
            }   

        }

        private UnidadMedida aUnidadMedida;
        public UnidadMedida AUnidadMedida
        {
            get { return aUnidadMedida; }
            set
            {
                aUnidadMedida = value;
                OnPropertyChanged("AUnidadMedida");
            }
        }

        public Articulo(int id, string codigo, string descripcion, int idFam, int idUM, int idCat, decimal precio, bool manejaStock, string imagen) 
        {
            art_Id = id;
            art_Codigo = codigo;
            art_Descripcion = descripcion;
            fam_Id = idFam;
            um_Id = idUM;
            cat_Id = idCat;
            art_Precio = precio;
            art_ManejaStock = manejaStock;
            art_Img = imagen;
        }

        public Articulo()
        {
        }


        //Constructor con familia, categoria y unidad de medida

        public Articulo(int id, string codigo, string descripcion, decimal precio, bool manejaStock, string imagen, Familia familia, Categoria categoria, UnidadMedida unidadMedida)
        {
            art_Id = id;
            art_Codigo = codigo;
            art_Descripcion = descripcion;
            art_Precio = precio;
            art_ManejaStock = manejaStock;
            art_Img = imagen;
            aFamilia = familia;
            aCategoria = categoria;
            aUnidadMedida = unidadMedida;
        }

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public override string ToString()
        {
            return Art_Descripcion.ToString() + ", " + Art_Precio.ToString() + ", " + Art_Descripcion.ToString();
        }

        public Familia Familia
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Categoria Categoria
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public UnidadMedida UnidadMedida
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
