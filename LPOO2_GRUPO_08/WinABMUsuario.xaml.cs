using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClasesBase;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Lógica de interacción para WinABMUsuario.xaml
    /// </summary>
    public partial class WinABMUsuario : Window
    {
        public WinABMUsuario()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenuAdmin = new WinMenuAdmin();
            wWinMenuAdmin.Show();
            this.Close();
        }

        private Usuario cargarDatos()
        {
            Usuario oUsuario = new Usuario();
            oUsuario.Usu_ApellidoNombre = txtApellido.Text;
            oUsuario.Usu_NombreUsuario = txtUsername.Text;
            oUsuario.Usu_Contrasenia = txtPassword.Text;
            oUsuario.Rol_Id = determinarRol(cmbRol.Text);
            return oUsuario;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Usuario oUsuario = cargarDatos();
            MessageBoxResult result = MessageBox.Show(encadenarDatosUsuario(oUsuario) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                limpiarCampos();
            }
        }

        private int determinarRol(string sRol)
        {
            int iId = 0;
            switch (sRol)
            {
                case "Administrador":
                    iId = 1;
                    break;
                case "Mozo":
                    iId = 2;
                    break;
                case "Vendedor":
                    iId = 3;
                    break;
            }
            return iId;
        }

        private string encadenarDatosUsuario(Usuario oUsuario)
        {
            string sCadenaDatosUsuario = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Apellido y nombre:  " + oUsuario.Usu_ApellidoNombre + "\n" +
                                              "Rol:  " + oUsuario.Rol_Id + ": " + cmbRol.Text + "\n" +
                                              "Usuario:  " + oUsuario.Usu_NombreUsuario + "\n" +
                                              "Contraseña:  " + oUsuario.Usu_Contrasenia + "\n";
            return sCadenaDatosUsuario;
        }

        private void limpiarCampos()
        {
            txtApellido.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRol.Text = "";
        }

 
    }
}
