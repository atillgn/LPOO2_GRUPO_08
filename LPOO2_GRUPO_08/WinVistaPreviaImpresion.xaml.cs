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
    /// Lógica de interacción para WinVistaPreviaImpresion.xaml
    /// </summary>
    public partial class WinVistaPreviaImpresion : Window
    {
        private CollectionViewSource vistaColection;

        public WinVistaPreviaImpresion()
        {
            InitializeComponent();
        }

        public WinVistaPreviaImpresion(CollectionViewSource coleccion)
        {
            InitializeComponent();
            vistaColection = coleccion;
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

        private void btnImprimir_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pdlg = new PrintDialog();
            if (pdlg.ShowDialog() == true)
            {
                pdlg.PrintDocument(((IDocumentPaginatorSource)DocMain).DocumentPaginator, "Imprimir");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var l = vistaColection.View.Cast<Articulo>().ToList();
            var c = new ObservableCollection<Articulo>(l);
            lvArticulos.ItemsSource = c;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWindowABMArticulos = new WinABMArticulos();
            wWindowABMArticulos.Show();
            this.Close();
        }
    }
}
