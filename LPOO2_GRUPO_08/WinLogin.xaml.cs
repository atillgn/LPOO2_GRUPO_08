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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClasesBase;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_ingresar_Click(object sender, RoutedEventArgs e)
        {
            //usuarios y roles en modo "hardcoded"
            Rol oRol1 = new Rol(1, "Administrador");
            Rol oRol2 = new Rol(2, "Mozo");
            Rol oRol3 = new Rol(3, "Vendedor");
            Usuario oUser1 = new Usuario(1, "Grafion Atilio", "atillgn", "123", 1);
            Usuario oUser4 = new Usuario(1, "Marcia Velarde", "admin", "123", 1);
            Usuario oUser2 = new Usuario(2, "Oviedo Ignacio", "gekaidas", "123", 2);
            Usuario oUser3 = new Usuario(3, "Cruz Pablo", "joacru", "123", 3);
            bool login = false;
            int aux = 0;
            if (oUser1.Usu_NombreUsuario == txtUser.Text && oUser1.Usu_Contrasenia == txtPass.Password) 
            {
                login = true;
                aux = oUser1.Rol_Id;
            }
            else if (oUser2.Usu_NombreUsuario == txtUser.Text && oUser2.Usu_Contrasenia == txtPass.Password) 
            {
                login = true;
                aux = oUser2.Rol_Id;
            }
            else if (oUser3.Usu_NombreUsuario == txtUser.Text && oUser3.Usu_Contrasenia == txtPass.Password)
            {
                login = true;
                aux = oUser3.Rol_Id;
            }
            else if (oUser4.Usu_NombreUsuario == txtUser.Text && oUser4.Usu_Contrasenia == txtPass.Password)
            {
                login = true;
                aux = oUser4.Rol_Id;
            }
            if (login)
            {
                MessageBox.Show("Bienvenido: " + txtUser.Text);
                txtUser.Clear();
                txtPass.Clear();
                Window wMenu;
                if (aux == 2 || aux == 3)
                {
                    wMenu = new WinMenuMozo();
                    wMenu.Show();
                }
                else 
                {
                    wMenu = new WinMenuAdmin();
                    wMenu.Show();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Datos incorrectos");
            }
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
