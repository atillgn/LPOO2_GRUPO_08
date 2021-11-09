using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Usuario : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int usu_Id;

        public int Usu_Id
        {
            get { return usu_Id; }
            set { 
                usu_Id = value;
                OnPropertyChanged("Usu_Id");
            }
        }
        private string usu_ApellidoNombre;

        public string Usu_ApellidoNombre
        {
            get { return usu_ApellidoNombre; }
            set {
                usu_ApellidoNombre = value;
                OnPropertyChanged("Usu_ApellidoNombre");
            }
        }
        private string usu_NombreUsuario;

        public string Usu_NombreUsuario
        {
            get { return usu_NombreUsuario; }
            set { 
                usu_NombreUsuario = value;
                OnPropertyChanged("Usu_NombreUsuario");
            }
        }
        private string usu_Contrasenia;

        public string Usu_Contrasenia
        {
            get { return usu_Contrasenia; }
            set { 
                usu_Contrasenia = value;
                OnPropertyChanged("Usu_Contrasenia");
            }
        }
        private int rol_Id;

        public int Rol_Id
        {
            get { return rol_Id; }
            set { 
                rol_Id = value;
                OnPropertyChanged("Rol_Id");
            }
        }

        private string usu_Img;
        public string Usu_Img
        {
            get { return usu_Img; }
            set
            {
                usu_Img = value;
                OnPropertyChanged("Usu_Img");
            }
        }

        private Rol oRol;
        public Rol ORol
        {
            get { return oRol; }
            set
            {
                oRol = value;
                OnPropertyChanged("ORol");
            }
        }

        public Usuario(int id, string apelldoNombre, string nombreUsuario, string contrasenia, int rolId, string img, Rol rol) 
        {
            usu_Id = id;
            usu_ApellidoNombre = apelldoNombre;
            usu_NombreUsuario = nombreUsuario;
            usu_Contrasenia = contrasenia;
            rol_Id = rolId;
            usu_Img = img;
            oRol = rol;
        }

        public Usuario()
        {
        }

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
                    case "Usu_ApellidoNombre":
                        msg_error = validad_Campo(usu_ApellidoNombre, 1);
                        break;
                    case "Usu_NombreUsuario":
                        msg_error = validad_Campo(usu_NombreUsuario, 0);
                        break;
                    case "Usu_Contrasenia":
                        msg_error = validad_Campo(usu_Contrasenia, 2);
                        break;
                    case "Usu_Img":
                        msg_error = validad_Campo(usu_Img, 0);
                        break;
                }
                return msg_error;
            }
        }

        private string validad_Campo(string texto, int control)
        {
            if (String.IsNullOrEmpty(texto))
                return "El valor del campo es obligatorio";
            else
            {
                if (texto.All(char.IsLetter) == false && control == 1)
                    return "Debe ingresar solo letras";
                if (texto.Length < 6 && control == 2)
                    return "La contraseña debe contener mínimo 6 caracteres";
            }
            return null;
        }
    }
}