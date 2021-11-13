using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Usuario : ClaseBase, INotifyPropertyChanged
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

        public override string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Usu_ApellidoNombre":
                        error = validarTexto("Apellido y nombre", usu_ApellidoNombre);
                        if (error == null)
                        {
                            error = validarLetras("Apellido y nombre", usu_ApellidoNombre);
                        }
                        break;
                    case "Usu_NombreUsuario":
                        error = validarTexto("Nombre de Usuario", usu_NombreUsuario);
                        break;
                    case "Usu_Contrasenia":
                        error = validarTexto("Contraseña", usu_Contrasenia, minLong: 6);
                        break;
                    case "Usu_Img":
                        error = validarTexto("Imagen", usu_Img);
                        break;
                }
                return error;
            }
        }
    }
}