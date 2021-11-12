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
        private Articulo oArticuloElegido;
        private List<ItemPedido> listaItems;
        private Pedido pedidoEnCurso;
        private int iTipo;

        public WinItemPedido()
        {
            InitializeComponent();
        }

        public WinItemPedido(List<ItemPedido> lista, Pedido pedido, int tipo)
        {
            InitializeComponent();
            if (lista == null)
            {
                lista = new List<ItemPedido>();
            }
            listaItems = lista;
            pedidoEnCurso = pedido;
            iTipo = tipo;
        }

        public WinItemPedido(List<ItemPedido> lista, Pedido pedido, int tipo, Articulo art)
        {
            InitializeComponent();
            if (lista == null)
            {
                lista = new List<ItemPedido>();
            }
            listaItems = lista;
            pedidoEnCurso = pedido;
            iTipo = tipo;
            oArticuloElegido = art;
            oArticuloSeleccionado = oArticuloElegido;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (oArticuloElegido != null)
            {
                cmbArticulo.SelectedValue = oArticuloElegido.Art_Id;
                txtPrecio.Text = Convert.ToString(oArticuloElegido.Art_Precio);
                txtCantidad.Text = "";
                txtTotal.Text = "";
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window winPedidos = new WinPedidos(listaItems, pedidoEnCurso, iTipo);
            winPedidos.Show();
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "" && txtPrecio.Text != "" && txtTotal.Text != "" && cmbArticulo.Text != "")
            {
                ItemPedido oItem = cargarItem();
                listaItems.Add(oItem);
                Window wWinPedidos = new WinPedidos(listaItems, pedidoEnCurso, iTipo);
                wWinPedidos.Show();
                this.Close();
            }
            else
                MessageBox.Show("No pueden haber campos vacíos", "ERROR CAMPO VACÍO");
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
            if (oArticuloElegido == null)
            {
                oArticuloSeleccionado = TrabajarArticulos.buscarArticuloById((int)cmbArticulo.SelectedValue);
                txtPrecio.Text = Convert.ToString(oArticuloSeleccionado.Art_Precio);
                txtCantidad.Text = "";
                txtTotal.Text = "";
            }
        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtCantidad.Text.All(char.IsDigit))
            {
                MessageBox.Show("Debe ingresar un número", "ERROR");
                txtCantidad.Text = txtCantidad.Text.Remove(txtCantidad.Text.Count() - 1);
            }
            else
            {
                if (txtCantidad.Text != "")
                    txtTotal.Text = Convert.ToString(Convert.ToDecimal(txtPrecio.Text) * Convert.ToInt32(txtCantidad.Text));
            }
        }

        private void btnBuscarArticulo_Click(object sender, RoutedEventArgs e)
        {
            Window winTablaArticulos = new WinTablaArticulosPedido(listaItems, pedidoEnCurso, iTipo);
            winTablaArticulos.Show();
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

    }
}
