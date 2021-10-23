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
        public WinABMArticulos()
        {
            InitializeComponent();
        }

        CollectionView Vista;
        ObservableCollection<Articulo> listaArticulos;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)this.Resources["ListArt"];
            listaArticulos = odp.Data as ObservableCollection<Articulo>;
            Vista = (CollectionView)CollectionViewSource.GetDefaultView(stkp_Content.DataContext);
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

        private void btnAltaArticulo_Click(object sender, RoutedEventArgs e)
        {
            Window wWinArticulo = new WinABMArticulo();
            wWinArticulo.Show();
            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Desea eliminar el articulo? \n", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TrabajarArticulos.borrarArticulo(Convert.ToInt32(txtId.Text));
                    
                }
                catch (Exception a)
                {
                    MessageBox.Show("La tabla esta vacia");
                }
                MessageBox.Show("ARTICULO ELIMINADO CON EXITO!");
                ((ObjectDataProvider)FindResource("ListArt")).Refresh();
                ObjectDataProvider odp = (ObjectDataProvider)this.Resources["ListArt"];
                listaArticulos = odp.Data as ObservableCollection<Articulo>;
                Vista = (CollectionView)CollectionViewSource.GetDefaultView(stkp_Content.DataContext);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Articulo oArticulo = (Articulo) Vista.CurrentItem;
            Window wWinEArticulo = new WinEditArticulo(oArticulo);
            wWinEArticulo.Show();
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
    }
}
