using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class UnidadMedida: ClaseBase
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

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Um_Descripcion":
                        error = validarTexto("Descripción", um_Descripcion);
                        break;
                    case "Um_Abrev":
                        error = validarTexto("Abreviación", um_Abrev, maxLong: 10);
                        break;
                }
                return error;
            }
        }
    }
}
