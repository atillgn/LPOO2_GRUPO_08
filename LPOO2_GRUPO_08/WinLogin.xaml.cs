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
            Usuario oUser4 = new Usuario(2, "Marcia Velarde", "admin", "123", 1);
            Usuario oUser2 = new Usuario(3, "Oviedo Ignacio", "gekaidas", "123", 2);
            Usuario oUser3 = new Usuario(4, "Cruz Pablo", "joacru", "123", 3);
            Usuario[] usuarios = { oUser1, oUser2, oUser3, oUser4};
            bool login = false;
            int aux = 0;
            foreach (Usuario oUsu in usuarios)
            {
                if (oUsu.Usu_NombreUsuario == clogin.txtUser.Text && oUsu.Usu_Contrasenia == clogin.txtPass.Password)
                {
                    login = true;
                    aux = oUsu.Rol_Id;
                }
            }
            if (login)
            {
                abrirMenu(aux);
            }
            else
            {
                MessageBox.Show("Datos incorrectos");
                clogin.txtUser.Focus();
            }
        }

        private void abrirMenu(int aux)
        {
            MessageBox.Show("Bienvenido: " + clogin.txtUser.Text);
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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        /*
        private void color_Changed()
        {
            if (clogin.txtUser.IsFocused == true)
            {
                clogin.rUser.Fill = new SolidColorBrush(Color.FromRgb(28, 191, 255));
            }
        }*/
    }
}
