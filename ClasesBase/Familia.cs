using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Familia : ClaseBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int fam_Id;
        public int Fam_Id
        {
            get { return fam_Id; }
            set { fam_Id = value;
            OnPropertyChanged("Fam_Id");
            }
        }

        private string fam_Descripcion;
        public string Fam_Descripcion
        {
            get { return fam_Descripcion; }
            set { fam_Descripcion = value;
            OnPropertyChanged("Fam_Descripcion");
            }
        }

        public Familia(int id, string descripcion) 
        {
            fam_Id = id;
            fam_Descripcion = descripcion;
        }

        public Familia() { }

        protected void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Fam_Descripcion":
                        error = validarTexto("Descripción", fam_Descripcion);
                        break;
                }
                return error;
            }
        }
    }
}
