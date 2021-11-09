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
using Microsoft.Win32;
using ClasesBase;
using System.Collections.ObjectModel;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Lógica de interacción para WinTablaUsuarios.xaml
    /// </summary>
    public partial class WinTablaUsuarios : Window
    {
        private CollectionViewSource UsuarioFilter;
        ObservableCollection<Usuario> listaUsuarios;

        public WinTablaUsuarios()
        {
            InitializeComponent();
            UsuarioFilter = Resources["UsuarioColl"] as CollectionViewSource;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["ListUsuario"];
            listaUsuarios = odp.Data as ObservableCollection<Usuario>;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Window wWinAlta = new WinABMUsuario(new Usuario(), 1);
            wWinAlta.Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Usuario user = (Usuario)lblUsuarios.SelectedItem;
            WinABMUsuario winModificarUsuario = new WinABMUsuario(user, 0);
            winModificarUsuario.Show();
            this.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario user = (Usuario)lblUsuarios.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Seguro que desea eliminar a \"" + user.Usu_ApellidoNombre + "\" ?", "ELIMINAR", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                TrabajarUsuario.borrarUsuario(user.Usu_Id);
                MessageBox.Show("USUARIO ELIMINADO CON ÉXITO");
                btnModificar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                lblUsuarios.SelectedIndex = -1;
                img.Source = null;
                var UsuarioDelete = listaUsuarios.Single(i => i.Usu_Id == user.Usu_Id);
                listaUsuarios.Remove(UsuarioDelete);
            }
        }

        private void FiltroUsuario(object sender, FilterEventArgs e)
        {
            Usuario oUsuario = (Usuario)e.Item;
            if (oUsuario.Usu_ApellidoNombre.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase))
                e.Accepted = true;
            else
                e.Accepted = false;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            img.Source = null;
            lblUsuarios.SelectedIndex = -1;
            btnModificar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            if (UsuarioFilter != null)
            {
                UsuarioFilter.Filter += new FilterEventHandler(FiltroUsuario);
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblUsuarios.SelectedIndex != -1)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                Usuario user = (Usuario)lblUsuarios.SelectedItem;
                dialog.FileName = user.Usu_Img;
                img.Source = new BitmapImage(new Uri(dialog.FileName));
                btnEliminar.IsEnabled = true;
                btnModificar.IsEnabled = true;
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenu = new WinMenuAdmin();
            wWinMenu.Show();
            this.Close();
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

        private void Label_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            lblBuscar.Foreground = new SolidColorBrush(Color.FromRgb(28, 191, 255));
        }

        private void txtFilter_LostFocus(object sender, RoutedEventArgs e)
        {
            lblBuscar.Foreground = Brushes.White;
        }

    }
}
