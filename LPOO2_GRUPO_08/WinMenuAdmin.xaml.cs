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
        public WinMenuAdmin()
        {
            InitializeComponent();
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


    }
}
