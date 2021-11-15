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
            UsuarioFilter = (CollectionViewSource)(this.Resources["UsuarioColl"]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["ListUsuario"];
            listaUsuarios = odp.Data as ObservableCollection<Usuario>;
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
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

        private void btnDisabled(Button b)
        {
            b.IsEnabled = false;
            Style stl = Application.Current.FindResource("BtnDisLogin") as Style;
            b.Style = stl;
            
        }

        private void btnEnable(Button b)
        {
            b.IsEnabled = true;
            Style stl = Application.Current.FindResource("BtnLogin") as Style;
            b.Style = stl;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Usuario user = (Usuario)lblUsuarios.SelectedItem;
            if (user.Usu_Id != 34)
            {
                MessageBoxResult result = MessageBox.Show("Seguro que desea eliminar a \"" + user.Usu_ApellidoNombre + "\" ?", "ELIMINAR", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (user.Rol_Id == 2)
                    {
                        ObservableCollection<Pedido> listaPedidos = TrabajarPedido.buscarPedidosByMozo(user.Usu_Id);
                        foreach (Pedido p in listaPedidos)
                        {
                            p.Usu_Id = 34;
                            TrabajarPedido.editar_Pedido(p);
                        }
                    }
                    TrabajarUsuario.borrarUsuario(user.Usu_Id);
                    MessageBox.Show("USUARIO ELIMINADO CON ÉXITO");
                    btnDisabled(btnModificar);
                    btnDisabled(btnEliminar);
                    lblUsuarios.SelectedIndex = -1;
                    img.Source = null;
                    var UsuarioDelete = listaUsuarios.Single(i => i.Usu_Id == user.Usu_Id);
                    listaUsuarios.Remove(UsuarioDelete);
                }
            }
            else
            {
                MessageBox.Show("No se puede eliminar el usuario administrador principal", "ERROR");
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
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
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
                btnEnable(btnEliminar);
                btnEnable(btnModificar);
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
