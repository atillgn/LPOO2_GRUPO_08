using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class UnidadMedida: IDataErrorInfo
    {
        private int um_Id;

        public int Um_Id
        {
            get { return um_Id; }
            set { um_Id = value; }
        }
        private string um_Descripcion;

        public string Um_Descripcion
        {
            get { return um_Descripcion; }
            set { um_Descripcion = value; }
        }
        private string um_Abrev;

        public string Um_Abrev
        {
            get { return um_Abrev; }
            set { um_Abrev = value; }
        }

        public UnidadMedida(int id, string descripcion, string abrev) 
        {
            um_Id = id;
            um_Descripcion = descripcion;
            um_Abrev = abrev;
        }

        public UnidadMedida() { }

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
                    case "Um_Descripcion":
                        msg_error = validad_Campo(um_Descripcion);
                        break;
                    case "Um_Abrev":
                        msg_error = validad_Campo(um_Abrev);
                        break;
                }
                return msg_error;
            }
        }

        private string validad_Campo(string texto)
        {
            if (String.IsNullOrEmpty(texto)) 
                return "El valor del campo es obligatorio";
            return null;
        }
    }
}
