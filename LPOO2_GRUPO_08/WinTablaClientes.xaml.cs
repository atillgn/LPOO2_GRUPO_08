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
    /// Lógica de interacción para WinTablaClientes.xaml
    /// </summary>
    public partial class WinTablaClientes : Window
    {
        private CollectionViewSource ClienteFilter;
        private List<ItemPedido> listaItems;
        private Pedido pedidoEnCurso;

        public WinTablaClientes(List<ItemPedido> lista, Pedido pedido)
        {
            InitializeComponent();
            ClienteFilter = Resources["ClienteColl"] as CollectionViewSource;
            listaItems = lista;
            pedidoEnCurso = pedido;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWindowPedidos = new WinPedidos(listaItems, pedidoEnCurso);
            wWindowPedidos.Show();
            this.Close();
        }

        private void FiltroCliente(object sender, FilterEventArgs e)
        {
            Cliente oCliente = (Cliente)e.Item;
            if (oCliente.Cli_Apellido.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase) || oCliente.Cli_Nombre.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            lblClientes.SelectedIndex = -1;
            btnSeleccionar.IsEnabled = false;
            if (ClienteFilter != null)
            {
                ClienteFilter.Filter += new FilterEventHandler(FiltroCliente);
            }
        }

        private void btnSeleccionar_Click(object sender, RoutedEventArgs e)
        {
            Cliente oClienteSeleccionado = (ClasesBase.Cliente)lblClientes.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Seleccionar a \"" + oClienteSeleccionado.Cli_Apellido + ", " + oClienteSeleccionado.Cli_Nombre + "\" ?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                pedidoEnCurso.Cli_Id = oClienteSeleccionado.Cli_Id;
                Window winPedido = new WinPedidos(listaItems, pedidoEnCurso);
                winPedido.Show();
                this.Close();
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblClientes.SelectedIndex != -1)
                btnSeleccionar.IsEnabled = true;
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
