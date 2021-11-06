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
using Microsoft.Win32;
using System.IO;

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
            oUsuario.Rol_Id = (int)cmbRol.SelectedValue;
            oUsuario.Usu_Img = (string)txtImg.Text;
            return oUsuario;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbRol.Text != "" && txtApellido.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtImg.Text != "")
            {
                Usuario usuarioBuscado = TrabajarUsuario.buscarUsuarioByNombreUsuario(txtUsername.Text);
                if (usuarioBuscado == null)
                {
                    Usuario oUsuario = cargarDatos();
                    MessageBoxResult result = MessageBox.Show(encadenarDatosUsuario(oUsuario) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        TrabajarUsuario.agregarUsuario(oUsuario);
                        MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                        limpiarCampos();
                    }
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya se encuentra registrado", "ERROR USUARIO EXISTENTE");
                    txtUsername.Focus();
                }
            }
            else
            {
                MessageBox.Show("No pueden haber campos vacíos", "ERROR CAMPO VACÍO");
            }
        }

        private string encadenarDatosUsuario(Usuario oUsuario)
        {
            string sCadenaDatosUsuario = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Apellido y nombre:  " + oUsuario.Usu_ApellidoNombre + "\n" +
                                              "Rol:  " + oUsuario.Rol_Id + ": " + cmbRol.Text + "\n" +
                                              "Usuario:  " + oUsuario.Usu_NombreUsuario + "\n" +
                                              "Contraseña:  " + oUsuario.Usu_Contrasenia + "\n" +
                                              "Imagen: " + oUsuario.Usu_Img + "\n";
            return sCadenaDatosUsuario;
        }

        private void limpiarCampos()
        {
            txtApellido.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtImg.Text = "";
            cmbRol.Text = "";
            img.Source = null;
        }

        private void bntMinimizedScreen_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximizeScreen_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window wWinLogin = new MainWindow();
            wWinLogin.Show();
            this.Close();
        }

        private void titleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Seleccione una imagen";
            dialog.Filter = "All supported graphics|*.jpg; *.jpeg; *.png" + "JPEG (*.jpg; *.jpeg)|*.jpg;*jpeg" + "Portable Network Graphic (*.png)|*.png";
            if (dialog.ShowDialog() == true)
            {
                txtImg.Text = dialog.FileName;
                img.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }
 
    }
}
