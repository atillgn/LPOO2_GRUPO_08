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
    /// Lógica de interacción para WinItemPedido.xaml
    /// </summary>
    public partial class WinItemPedido : Window
    {
        private Articulo oArticuloSeleccionado = new Articulo();
        private List<ItemPedido> listaItems;
        private Pedido pedidoEnCurso;

        public WinItemPedido()
        {
            InitializeComponent();
        }

        public WinItemPedido(List<ItemPedido> lista, Pedido pedido)
        {
            InitializeComponent();
            if (lista == null)
            {
                lista = new List<ItemPedido>();
            }
            listaItems = lista;
            pedidoEnCurso = pedido;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window winPedidos = new WinPedidos(listaItems, pedidoEnCurso);
            winPedidos.Show();
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ItemPedido oItem = cargarItem();
            listaItems.Add(oItem);
            Window wWinPedidos = new WinPedidos(listaItems, pedidoEnCurso);
            wWinPedidos.Show();
            this.Close();
        }

        private ItemPedido cargarItem()
        {
            ItemPedido oItem = new ItemPedido();
            oItem.Art_Id = (int)cmbArticulo.SelectedValue;
            oItem.Precio = oArticuloSeleccionado.Art_Precio;
            oItem.Cantidad = Convert.ToInt32(txtCantidad.Text);
            oItem.Importe = oArticuloSeleccionado.Art_Precio * Convert.ToInt32(txtCantidad.Text);
            oItem.Articulo = TrabajarArticulos.buscarArticuloById((int)cmbArticulo.SelectedValue);
            return oItem;
        }

        private void cmbArticulo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            oArticuloSeleccionado = TrabajarArticulos.buscarArticuloById((int)cmbArticulo.SelectedValue);
            txtPrecio.Text = Convert.ToString(oArticuloSeleccionado.Art_Precio);
            txtCantidad.Text = "";
            txtTotal.Text = "";
        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCantidad.Text != "")
                txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text));
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

        private void cmbArticulo_Loaded(object sender, RoutedEventArgs e)
        {
            cmbArticulo.DisplayMemberPath = "Art_Descripcion";
            cmbArticulo.SelectedValuePath = "Art_Id";
            cmbArticulo.ItemsSource = TrabajarArticulos.traerArticulosPedido().DefaultView;
        }

    }
}
