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
    /// Lógica de interacción para WinPedidos.xaml
    /// </summary>
    public partial class WinPedidos : Window
    {
        private List<ItemPedido> listaItem;
        private Pedido pedidoEnCurso;
        private Mesa mesaOcupable;
        private int iTipo = 0;

        public WinPedidos()
        {
            InitializeComponent();
        }

        public WinPedidos(Mesa mesa, int tipo)
        {
            InitializeComponent();
            mesaOcupable = mesa;
            iTipo = tipo;
        }

        public WinPedidos(List<ItemPedido> lista, Pedido pedido)
        {
            InitializeComponent();
            listaItem = lista;
            pedidoEnCurso = pedido;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            if (iTipo == 0)
            {
                Window wWinMenuMozo = new WinMenuMozo();
                wWinMenuMozo.Show();
                this.Close();
            }
            else
            {
                Window wWinMesas = new WinMesas();
                wWinMesas.Show();
                this.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (pedidoEnCurso == null)
            {
                txtFechaEmision.Text = Convert.ToString(DateTime.Now);
                txtFechaEntrega.Text = Convert.ToString(DateTime.Now);
                if (mesaOcupable != null)
                {
                    cmbMesa.SelectedValue = mesaOcupable.Mesa_Id;
                }
            }
            else
                cargarCampos();
            cargarLista();
        }

        private void cargarLista()
        {
            if (listaItem != null)
                foreach (ItemPedido item in listaItem)
                    lvItems.Items.Add(item);
        }

        private void btnAgregarItem_Click(object sender, RoutedEventArgs e)
        {
            pedidoEnCurso = cargarDatosPedido();
            Window wWinItemPedido = new WinItemPedido(listaItem, pedidoEnCurso);
            wWinItemPedido.Show();
            this.Close();
        }

        private void btnEliminarItem_Click(object sender, RoutedEventArgs e)
        {
            int eliminar = lvItems.SelectedIndex;
            var articuloDelete = listaItem[eliminar];
            listaItem.RemoveAt(eliminar);
            lvItems.Items.Remove(articuloDelete);
            if (listaItem.Count == 0)
                btnEliminarItem.IsEnabled = false;
        }

        private void lvItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEliminarItem.IsEnabled = true;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            pedidoEnCurso = cargarDatosPedido();
            MessageBoxResult result = MessageBox.Show("\n Guardar datos? \n", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                TrabajarPedido.insertar_Pedido(pedidoEnCurso);
                MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                Mesa oMesaOcupada = TrabajarMesa.buscarMesaById((int)cmbMesa.SelectedValue);
                oMesaOcupada.Mesa_Estado = 5;
                TrabajarMesa.editarMesa(oMesaOcupada);
                guardarItemPedido();
                Window wWinComprobantePedido = new WinComprobantePedido(TrabajarPedido.buscarPedidoByFechaEmision(Convert.ToDateTime(txtFechaEmision.Text)).Ped_Id);
                wWinComprobantePedido.Show();
                this.Close();
            }
        }

        private void guardarItemPedido()
        {
            if (listaItem != null)
            {
                foreach (ItemPedido item in listaItem)
                {
                    item.Ped_Id = TrabajarPedido.buscarPedidoByFechaEmision(Convert.ToDateTime(txtFechaEmision.Text)).Ped_Id;
                    TrabajarItemPedido.insertar_ItemPedido(item);
                }
            }
        }

        private Pedido cargarDatosPedido()
        {
            Pedido oPedido = new Pedido();
            if (cmbCliente.SelectedValue != null) { oPedido.Cli_Id = (int)cmbCliente.SelectedValue; }
            if (cmbMesa.SelectedValue != null) { oPedido.Mesa_Id = (int)cmbMesa.SelectedValue; }
            if (cmbMozo.SelectedValue != null) { oPedido.Usu_Id = (int)cmbMozo.SelectedValue; }
            oPedido.Ped_FechaEmision = Convert.ToDateTime(txtFechaEmision.Text);
            oPedido.Ped_fechaEntrega = Convert.ToDateTime(txtFechaEntrega.Text);
            if (txtComensales.Text != "") { oPedido.Ped_Comensales = Convert.ToInt32(txtComensales.Text); }
            oPedido.Ped_Facturado = false;
            return oPedido;
        }

        private void cargarCampos()
        {
            txtFechaEmision.Text = Convert.ToString(pedidoEnCurso.Ped_FechaEmision);
            cmbMozo.SelectedValue = pedidoEnCurso.Usu_Id;
            cmbMesa.SelectedValue = pedidoEnCurso.Mesa_Id;
            cmbCliente.SelectedValue = pedidoEnCurso.Cli_Id;
            txtFechaEntrega.Text = Convert.ToString(pedidoEnCurso.Ped_fechaEntrega);
            if (pedidoEnCurso.Ped_Comensales != 0) { txtComensales.Text = Convert.ToString(pedidoEnCurso.Ped_Comensales); }
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

        private void cmbMesa_Loaded(object sender, RoutedEventArgs e)
        {
            cmbMesa.DisplayMemberPath = "Mesa_Posicion";
            cmbMesa.SelectedValuePath = "Mesa_Id";
            cmbMesa.ItemsSource = TrabajarMesa.buscarMesaByEstado(1).DefaultView;
        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            pedidoEnCurso = cargarDatosPedido();
            Window winTablaCliente = new WinTablaClientes(listaItem, pedidoEnCurso);
            winTablaCliente.Show();
            this.Close();
        }



    }
}
