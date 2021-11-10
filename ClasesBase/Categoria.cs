using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Categoria : IDataErrorInfo
    {
        private int cat_Id;
        public int Cat_Id
        {
            get { return cat_Id; }
            set { cat_Id = value; }
        }

        private string cat_Descripcion;
        public string Cat_Descripcion
        {
            get { return cat_Descripcion; }
            set { cat_Descripcion = value; }
        }

        public Categoria(int id, string descripcion) 
        {
            cat_Id = id;
            cat_Descripcion = descripcion;
        }

        public Categoria() { }

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
                    case "Cat_Descripcion":
                        msg_error = validad_Campo(cat_Descripcion);
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
