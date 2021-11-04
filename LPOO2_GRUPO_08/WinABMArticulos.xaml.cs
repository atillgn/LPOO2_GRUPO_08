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
using System.Collections.ObjectModel;
using ClasesBase;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Interaction logic for WinABMArticulos.xaml
    /// </summary>
    public partial class WinABMArticulos : Window
    {
        private int posicion;

        public WinABMArticulos()
        {
            InitializeComponent();
        }

        public WinABMArticulos(int lugar)
        {
            InitializeComponent();
            this.posicion = lugar;
        }

        CollectionView Vista;
        ObservableCollection<Articulo> listaArticulos;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["ListArt"];
            listaArticulos = odp.Data as ObservableCollection<Articulo>;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(stkp_Content.DataContext);
            if (posicion == -1)
                Vista.MoveCurrentToLast();
            else
                Vista.MoveCurrentToPosition(posicion);
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToFirst();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToLast();
        }


        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToPrevious();
            if (Vista.IsCurrentBeforeFirst)
            {
                Vista.MoveCurrentToLast();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Vista.MoveCurrentToNext();
            if (Vista.IsCurrentAfterLast)
            {
                Vista.MoveCurrentToFirst();
            }
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            Window wWinArticulo = new WinABMArticulo(null, Vista.CurrentPosition);
            wWinArticulo.Show();
            this.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea eliminar el artículo \""+ txtDescrip.Text + "\" ? \n", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TrabajarArticulos.borrarArticulo(Convert.ToInt32(txtIdArticulo.Text));
                    
                }
                catch (Exception a)
                {
                    MessageBox.Show("La tabla esta vacia");
                }
                MessageBox.Show("ARTÍCULO ELIMINADO CON EXITO!");
                var articuloDelete = listaArticulos.Single(i => i.Art_Id == Convert.ToInt32(txtIdArticulo.Text));
                listaArticulos.Remove(articuloDelete);
                Vista.MoveCurrentToPosition(Vista.CurrentPosition - 1);
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            Articulo oArticulo = (Articulo) Vista.CurrentItem;
            Window wWinAMBArticulo = new WinABMArticulo(oArticulo, Vista.CurrentPosition);
            wWinAMBArticulo.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenuAdmin = new WinMenuAdmin();
            wWinMenuAdmin.Show();
            this.Close();
        }

        private void btnVerArticulos_Click(object sender, RoutedEventArgs e)
        {
            Window wListArticulos = new WinTablaArticulos();
            wListArticulos.Show();
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
