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
using System.Data;
using ClasesBase;
using System.Collections.ObjectModel;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Lógica de interacción para WinFacturacionCompleta.xaml
    /// </summary>
    public partial class WinFacturacionCompleta : Window
    {
        CollectionViewSource PedFilter;
        private ObservableCollection<ItemPedido> listaItem = new ObservableCollection<ItemPedido>();
        private ObservableCollection<Pedido> listadoPedidos;
        private ObservableCollection<Pedido> listadoPedidosFiltrado = new ObservableCollection<Pedido>();

        public WinFacturacionCompleta()
        {
            InitializeComponent();
            PedFilter = (CollectionViewSource)(this.Resources["PedidoColl"]);
            listadoPedidos = TrabajarPedido.listarPedidos();
            calcularTotal();
        }

        private void FiltroPedido(object sender, FilterEventArgs e)
        {
            if (dpBuscarFecha.SelectedDate != null)
            {
                Pedido oItem = (Pedido)e.Item;
                DateTime fechaPedido = oItem.Ped_FechaEmision.Date;
                DateTime fechaSeleccionada = (DateTime)dpBuscarFecha.SelectedDate;
                DateTime fechaSeleccionada2 = fechaSeleccionada.Date;
                if (fechaPedido.CompareTo(fechaSeleccionada2) == 0)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            else
            {
                e.Accepted = true;
            }
        }

        private void dpBuscarFecha_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PedFilter != null)
            {
                PedFilter.Filter += new FilterEventHandler(FiltroPedido);
            }
            cargarListadoFiltrado();
            calcularTotal();
        }

        private void calcularTotal()
        {
            decimal suma = 0;
            if (dpBuscarFecha.SelectedDate != null)
            {
                foreach (Pedido p in listadoPedidosFiltrado)
                {
                    listaItem = TrabajarItemPedido.buscarItemByPedidoId((int)p.Ped_Id);
                    suma = suma + generarTotal();
                }
            }
            else
            {
                foreach (Pedido p in listadoPedidos)
                {
                    listaItem = TrabajarItemPedido.buscarItemByPedidoId((int)p.Ped_Id);
                    suma = suma + generarTotal();
                }
            }
            lblTotal.Content = "$ " + Convert.ToString(suma);
        }

        private decimal generarTotal()
        {
            decimal importeTotal = 0;
            foreach (ItemPedido item in listaItem)
            {
                importeTotal = importeTotal + item.Importe;
            }
            return importeTotal;
        }

        private void cargarListadoFiltrado()
        {
            if (dpBuscarFecha.SelectedDate != null)
            {
                DateTime fechaSeleccionada = (DateTime)dpBuscarFecha.SelectedDate;
                DateTime fechaSeleccionada2 = fechaSeleccionada.Date;
                foreach (Pedido p in listadoPedidos)
                    if(p.Ped_FechaEmision.Date.CompareTo(fechaSeleccionada2) == 0)
                        listadoPedidosFiltrado.Add(p);
            }
        }

        private void btnGenerarComprobante_Click(object sender, RoutedEventArgs e)
        {
            Pedido oPedidoSeleccionado = (ClasesBase.Pedido)lblPedidos.SelectedItem;
            int pedidoId = Convert.ToInt32(oPedidoSeleccionado.Ped_Id);
            Window winComprobante = new WinComprobantePedido(pedidoId, 2);
            winComprobante.Show();
            this.Close();
        }

        private void lblPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblPedidos.SelectedIndex != -1)
                btnGenerarComprobante.IsEnabled = true;
        }

        private void btnVerTodos_Click(object sender, RoutedEventArgs e)
        {
            dpBuscarFecha.SelectedDate = null;
            if (PedFilter != null)
            {
                PedFilter.Filter += new FilterEventHandler(FiltroPedido);
            }
            calcularTotal();
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

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinFacturacion = new WinFacturacion();
            wWinFacturacion.Show();
            this.Close();
        }

    }
}

