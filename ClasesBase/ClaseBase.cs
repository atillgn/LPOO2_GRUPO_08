using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace ClasesBase
{
    public abstract class ClaseBase : IDataErrorInfo
    {
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public abstract string this[string columnName]
        {
            get;
        }

        protected string validarValor<T>(string campo, T valor)
        {
            if (valor == null)
            {
                return "El valor del campo " + campo + " es obligatorio";
            }
            return null;
        }

        protected string validarTexto(string campo, string valor, int minLong = 1, int maxLong = 0)
        {
            if (String.IsNullOrEmpty(valor))
            {
                return "El valor del campo " + campo + " es obligatorio";
            }
            if (valor.Length < minLong)
            {
                return "El valor del campo " + campo + " debe tener al menos " + minLong.ToString() + " caracteres";
            }
            if (maxLong > 0 && valor.Length > maxLong)
            {
                return "El valor del campo " + campo + " debe tener a lo sumo " + maxLong.ToString() + " caracteres";
            }
            return null;
        }

        protected string validarLetras(string campo, string valor)
        {
            if (!Regex.IsMatch(valor, "^[a-zA-Z áéíóú]+$"))
            {
                return "El campo " + campo + " debe contener solo letras";
            }
            return null;
        }

        protected string validarDecimal(string campo, string valor)
        {
            if (!Regex.IsMatch(valor, "^[0-9,.]+$"))
            {
                return "El campo " + campo + " debe contener un número";
            }
            return null;
        }

        protected string validarNumero(string campo, int valor, int minVal = 0, int maxVal = Int32.MaxValue)
        {
            //if (valor == null) return "El valor del campo " + campo + " es obligatorio";
            if (valor < minVal)
            {
                return "El valor del campo " + campo + " debe ser al menos " + minVal.ToString();
            }
            if (valor > maxVal)
            {
                return "El valor del campo " + campo + " debe ser a lo sumo " + minVal.ToString();
            }
            return null;
        }

        protected string validarNumero(string campo, decimal valor, decimal minVal = 0, decimal maxVal = Decimal.MaxValue)
        {
            //if (valor == null) return "El valor del campo " + campo + " es obligatorio";
            if (valor < minVal)
            {
                return "El valor del campo " + campo + " debe ser al menos " + minVal.ToString();
            }
            if (valor > maxVal)
            {
                return "El valor del campo " + campo + " debe ser a lo sumo " + minVal.ToString();
            }
            return null;
        }

        public string isValid()
        {
            var allProperties = GetType().GetProperties();
            foreach (var property in allProperties)
            {
                string error = this[property.Name];
                if (error != null) return error;
            }
            return null;
        }
    }
}
