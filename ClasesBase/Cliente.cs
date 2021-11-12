using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase 
{
    public class Cliente : IDataErrorInfo
    {
        private int cli_Id;

        public int Cli_Id
        {
            get { return cli_Id; }
            set { cli_Id = value; }
        }
        private string cli_Apellido;

        public string Cli_Apellido
        {
            get { return cli_Apellido; }
            set { cli_Apellido = value; }
        }
        private string cli_Nombre;

        public string Cli_Nombre
        {
            get { return cli_Nombre; }
            set { cli_Nombre = value; }
        }
        private string cli_Domicilio;

        public string Cli_Domicilio
        {
            get { return cli_Domicilio; }
            set { cli_Domicilio = value; }
        }
        private string cli_Telefono;

        public string Cli_Telefono
        {
            get { return cli_Telefono; }
            set { cli_Telefono = value; }
        }
        private string cli_Email;

        public string Cli_Email
        {
            get { return cli_Email; }
            set { cli_Email = value; }
        }

        private string cli_Dni;
        public string Cli_Dni
        {
            get { return cli_Dni; }
            set { cli_Dni = value; }
        }

        public Cliente(string dni, int id, string apellido, string nombre, string domicilio, string telefono, string email)
        {
            cli_Id = id;
            cli_Apellido = apellido;
            cli_Nombre = nombre;
            cli_Domicilio = domicilio;
            cli_Telefono = telefono;
            cli_Email = email;
            cli_Dni = dni;
        }

        public Cliente() { }

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
                    case "Cli_Apellido":
                        msg_error = validad_Campo(cli_Apellido, 1);
                        break;
                    case "Cli_Nombre":
                        msg_error = validad_Campo(cli_Nombre, 1);
                        break;
                    case "Cli_Telefono":
                        msg_error = validad_Campo(cli_Telefono, 2);
                        break;
                    case "Cli_Dni":
                        msg_error = validad_Campo(Cli_Dni, 2);
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
                    return "El campo debe contener mínimo 6 caracteres";
            }
            return null;
        }

    }
}
