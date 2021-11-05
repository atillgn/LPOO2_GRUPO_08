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

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Lógica de interacción para WinFacturacion.xaml
    /// </summary>
    public partial class WinFacturacion : Window
    {
        public WinFacturacion()
        {
            InitializeComponent();
        }

        private void btnGenerarComprobante_Click(object sender, RoutedEventArgs e)
        {
            DataRowView pedidoSeleccionado = lblPedidos.SelectedItem as DataRowView;
            int pedidoId = Convert.ToInt32(pedidoSeleccionado.Row[0]);
            MessageBoxResult result = MessageBox.Show("Generar comprobante para \"" + pedidoSeleccionado.Row[13] + "\" ?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Mesa oMesaOcupada = TrabajarMesa.buscarMesaById(Convert.ToInt32(pedidoSeleccionado.Row[7]));
                oMesaOcupada.Mesa_Estado = 1;
                TrabajarMesa.editarMesa(oMesaOcupada);

                Pedido pedido = TrabajarPedido.buscarPedidoById(pedidoId);
                pedido.Ped_Facturado = true;
                TrabajarPedido.editar_Pedido(pedido);

                Window winComprobante = new WinComprobantePedido(pedidoId, 1);
                winComprobante.Show();
                this.Close();
            }
        }

        private void btnVerTodos_Click(object sender, RoutedEventArgs e)
        {
            Window winFacturacionCompleta = new WinFacturacionCompleta();
            winFacturacionCompleta.Show();
            this.Close();
        }

        private void lblPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblPedidos.SelectedIndex != -1)
                btnGenerarComprobante.IsEnabled = true;
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
            Window wWindowMenuMozo = new WinMenuMozo();
            wWindowMenuMozo.Show();
            this.Close();
        }

    }
}

