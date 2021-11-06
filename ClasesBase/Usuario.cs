using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Usuario : IDataErrorInfo
    {
        private int usu_Id;

        public int Usu_Id
        {
            get { return usu_Id; }
            set { usu_Id = value; }
        }
        private string usu_ApellidoNombre;

        public string Usu_ApellidoNombre
        {
            get { return usu_ApellidoNombre; }
            set { usu_ApellidoNombre = value; }
        }
        private string usu_NombreUsuario;

        public string Usu_NombreUsuario
        {
            get { return usu_NombreUsuario; }
            set { usu_NombreUsuario = value; }
        }
        private string usu_Contrasenia;

        public string Usu_Contrasenia
        {
            get { return usu_Contrasenia; }
            set { usu_Contrasenia = value; }
        }
        private int rol_Id;

        public int Rol_Id
        {
            get { return rol_Id; }
            set { rol_Id = value; }
        }

        private string usu_Img;
        public string Usu_Img
        {
            get { return usu_Img; }
            set
            {
                usu_Img = value;
            }
        }

        public Usuario(int id, string apelldoNombre, string nombreUsuario, string contrasenia, int rolId, string img) 
        {
            usu_Id = id;
            usu_ApellidoNombre = apelldoNombre;
            usu_NombreUsuario = nombreUsuario;
            usu_Contrasenia = contrasenia;
            rol_Id = rolId;
            usu_Img = img;
        }

        public Usuario()
        {
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
                    case "Usu_ApellidoNombre":
                        msg_error = validar_ApellidoNombre();
                        break;
                    case "Usu_NombreUsuario":
                        msg_error = validar_NombreUsuario();
                        break;
                    case "Usu_Contrasenia":
                        msg_error = validar_Contrasenia();
                        break;
                }
                return msg_error;
            }
        }

        private string validar_ApellidoNombre()
        {
            if (String.IsNullOrEmpty(usu_ApellidoNombre))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validar_NombreUsuario()
        {
            if (String.IsNullOrEmpty(usu_NombreUsuario))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

        private string validar_Contrasenia()
        {
            if (String.IsNullOrEmpty(usu_Contrasenia))
            {
                return "El valor del campo es obligatorio";
            }
            return null;
        }

    }
}