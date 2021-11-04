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
    /// Lógica de interacción para WinComprobantePedido.xaml
    /// </summary>
    public partial class WinComprobantePedido : Window
    {
        private Pedido pedido;
        private ObservableCollection<ItemPedido> listaItem;

        public WinComprobantePedido()
        {
            InitializeComponent();
        }

        public WinComprobantePedido(int pedidoId)
        {
            InitializeComponent();
            pedido = TrabajarPedido.buscarPedidoById(pedidoId);
            listaItem = TrabajarItemPedido.buscarItemByPedidoId(pedidoId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txbId.Text = Convert.ToString(pedido.Ped_Id);
            txbFecha.Text = Convert.ToString(pedido.Ped_FechaEmision);
            Mesa oMesaOcupada = TrabajarMesa.buscarMesaById(pedido.Mesa_Id);
            txbMesa.Text = Convert.ToString(oMesaOcupada.Mesa_Posicion);
            Usuario oMozo = TrabajarUsuario.buscarUsuarioById(pedido.Usu_Id);
            txbMozo.Text = oMozo.Usu_ApellidoNombre;
            cargarLista();
            txbTotal.Text = generarTotal();
        }

        private void cargarLista()
        {
            if (listaItem != null)
            {
                foreach (ItemPedido item in listaItem)
                {
                    lvItems.Items.Add(item);
                }
            }
        }

        private string generarTotal()
        {
            decimal importeTotal = 0;
            foreach (ItemPedido item in listaItem)
            {
                importeTotal = importeTotal + item.Importe;
            }
            return Convert.ToString(importeTotal);
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMozo = new WinMenuMozo();
            wWinMozo.Show();
            this.Close();
        }

    }
}
