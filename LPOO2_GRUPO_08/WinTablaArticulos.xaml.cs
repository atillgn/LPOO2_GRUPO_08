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

    }
}
