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
using System.Collections.ObjectModel;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Lógica de interacción para WinTablaClientesABM.xaml
    /// </summary>
    public partial class WinTablaClientesABM : Window
    {
        public WinTablaClientesABM()
        {
            InitializeComponent();
        }

        private ObservableCollection<Cliente> listaCliente;
        private CollectionViewSource ClienteFilter;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["ListCliente"];
            ClienteFilter = Resources["ClienteColl"] as CollectionViewSource;
            listaCliente = odp.Data as ObservableCollection<Cliente>;
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Window wWinAlta = new WinABMCliente(new Cliente(), 1);
            wWinAlta.Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = (Cliente)lvClientes.SelectedItem;
            Window winModificarUsuario = new WinABMCliente(cliente, 0);
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
            Cliente cliente = (Cliente)lvClientes.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Seguro que desea eliminar a \"" + cliente.Cli_Apellido + ", " + cliente.Cli_Nombre + "\" ?", "ELIMINAR", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                TrabajarCliente.borrarCliente(cliente.Cli_Id);
                MessageBox.Show("USUARIO ELIMINADO CON ÉXITO");
                btnDisabled(btnModificar);
                btnDisabled(btnEliminar);
                lvClientes.SelectedIndex = -1;
                var ClienteDelete = listaCliente.Single(i => i.Cli_Id == cliente.Cli_Id);
                listaCliente.Remove(ClienteDelete);
            }
        }

        private void FiltroCliente(object sender, FilterEventArgs e)
        {
            Cliente oCliente = (Cliente)e.Item;
            if (oCliente.Cli_Dni.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase))
                e.Accepted = true;
            else
                e.Accepted = false;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            lvClientes.SelectedIndex = -1;
            if (!txtFilter.Text.All(char.IsDigit))
            {
                MessageBox.Show("Debe ingresar un número", "ERROR");
                txtFilter.Text = txtFilter.Text.Remove(txtFilter.Text.Count() - 1);
            }
            else
            {
                btnDisabled(btnModificar);
                btnDisabled(btnEliminar);
                if (ClienteFilter != null)
                {
                    ClienteFilter.Filter += new FilterEventHandler(FiltroCliente);
                }
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvClientes.SelectedIndex != -1)
            {
                btnEnable(btnEliminar);
                btnEnable(btnModificar);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenu = new WinMenuMozo();
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

    