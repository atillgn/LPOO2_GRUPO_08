using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Familia : IDataErrorInfo, INotifyPropertyChanged
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

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string msg_error = null;

                switch (columnName)
                {
                    case "Fam_Descripcion":
                        msg_error = validad_Campo(fam_Descripcion);
                        break;
                }
                return msg_error;
            }
        }

        private string validad_Campo(string texto)
        {
            if (String.IsNullOrEmpty(texto)) return "El valor del campo es obligatorio";
            
            return null;
        }
    }
}
