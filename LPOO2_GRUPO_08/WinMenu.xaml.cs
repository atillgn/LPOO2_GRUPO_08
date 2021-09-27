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

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Interaction logic for WinMenuAdmin.xaml
    /// </summary>
    public partial class WinMenuAdmin : Window
    {
        public WinMenuAdmin(int a)
        {
            InitializeComponent();
            carga(a);
        }

        private void menuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            Window wWinUsuario = new WinABMUsuario();
            wWinUsuario.Show();
            this.Close();
        }

        private void menuArticulos_Click(object sender, RoutedEventArgs e)
        {
            Window wWinArticulo = new WinABMArticulo();
            wWinArticulo.Show();
            this.Close();
        }

        private void menuFamilia_Click(object sender, RoutedEventArgs e)
        {
            Window wWinFamilia = new WinABMFamilia();
            wWinFamilia.Show();
            this.Close();
        }

        private void menuCategoria_Click(object sender, RoutedEventArgs e)
        {
            Window wWinCategoria = new WinABMCategoria();
            wWinCategoria.Show();
            this.Close();
        }

        private void menuUnidades_Click(object sender, RoutedEventArgs e)
        {
            Window wWinUnidades = new WinABMUnidadDeMedida();
            wWinUnidades.Show();
            this.Close();
        }

        private void carga(int a)
        {
            if (a == 1)
            {
                menuCliente.Visibility = Visibility.Collapsed;
                menuMesas.Visibility = Visibility.Collapsed;
                menuPedido.Visibility = Visibility.Collapsed;
            }
            else 
            {
                menuUsuarios.Visibility = Visibility.Collapsed;
                menuUnidades.Visibility = Visibility.Collapsed;
                menuFamilia.Visibility = Visibility.Collapsed;
                menuCategoria.Visibility = Visibility.Collapsed;
                menuArticulos.Visibility = Visibility.Collapsed;
            }
        }

        private void menuCliente_Click(object sender, RoutedEventArgs e)
        {
            Window wWinCliente = new WinABMCliente();
            wWinCliente.Show();
            this.Close();
        }

        private void menuMesas_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMesas = new WinMesas();
            wWinMesas.Show();
            this.Close();
        }
    }
}
