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
    /// Lógica de interacción para WinTablaArticulosPedido.xaml
    /// </summary>
    public partial class WinTablaArticulosPedido : Window
    {
        private CollectionViewSource ArtFilter;
        private List<ItemPedido> listaItems;
        private Pedido pedidoEnCurso;
        private int iTipo;
        private int filtro;

        public WinTablaArticulosPedido(List<ItemPedido> lista, Pedido pedido, int tipo)
        {
            InitializeComponent();
            ArtFilter = (CollectionViewSource)(this.Resources["ArtColl"]);
            listaItems = lista;
            pedidoEnCurso = pedido;
            iTipo = tipo;
        }

        private void cmbCategoría_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvArt.SelectedIndex = -1;
            filtro = 1;
            txtFilter.Text = "";
            if (ArtFilter != null)
            {
                ArtFilter.Filter += new FilterEventHandler(FiltroArticulo);
            }
        }

        private void lvArt_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Articulo oArtSeleccionado = (ClasesBase.Articulo)lvArt.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Seleccionar a \"" + oArtSeleccionado.Art_Descripcion + "\" ?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Articulo art = (Articulo)lvArt.SelectedItem;
                Window winItem = new WinItemPedido(listaItems, pedidoEnCurso, iTipo, art);
                winItem.Show();
                this.Close();
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wItem = new WinItemPedido(listaItems, pedidoEnCurso, iTipo);
            wItem.Show();
            this.Close();
        }

        private void FiltroArticulo(object sender, FilterEventArgs e)
        {
            Articulo oItem = (Articulo)e.Item;
            if (filtro == 0)
            {
                if (oItem.Art_Descripcion.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
            else
            {
                if (oItem.ACategoria.Cat_Id == (int)cmbCategoría.SelectedValue)
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            lvArt.SelectedIndex = -1;
            cmbCategoría.Text = "";
            filtro = 0;
            if (ArtFilter != null)
            {
                ArtFilter.Filter += new FilterEventHandler(FiltroArticulo);
            }
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

        private void btnVistaPrevia_Click(object sender, RoutedEventArgs e)
        {
            Window wVistaPrevia = new WinVistaPreviaImpresion(ArtFilter);
            wVistaPrevia.Show();
            this.Close();
        }

        private void txtFilter_GotFocus(object sender, RoutedEventArgs e)
        {
            filtro = 0;
            if (ArtFilter != null)
            {
                ArtFilter.Filter += new FilterEventHandler(FiltroArticulo);
            }
        }

    }
}
