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
    /// Interaction logic for WinTablaArticulos.xaml
    /// </summary>
    public partial class WinTablaArticulos : Window
    {
        CollectionViewSource ArtFilter;

        public WinTablaArticulos()
        {
            InitializeComponent();
            ArtFilter = (CollectionViewSource)(this.Resources["ArtColl"]);
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWindowABMArticulos = new WinABMArticulos();
            wWindowABMArticulos.Show();
            this.Close();
        }

        private void FiltroArticulo(object sender, FilterEventArgs e)
        {
            Articulo oItem = (Articulo) e.Item;
            if (oItem.Art_Descripcion.StartsWith(txtFilter.Text, StringComparison.CurrentCultureIgnoreCase))
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
            if (ArtFilter != null)
            {
                ArtFilter.Filter += new FilterEventHandler(FiltroArticulo);
            }
        } 

    }
}
