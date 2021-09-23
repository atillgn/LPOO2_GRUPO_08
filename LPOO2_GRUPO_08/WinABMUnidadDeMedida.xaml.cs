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
    /// Interaction logic for WinABMUnidadDeMedida.xaml
    /// </summary>
    public partial class WinABMUnidadDeMedida : Window
    {
        public WinABMUnidadDeMedida()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenuAdmin = new WinMenuAdmin();
            wWinMenuAdmin.Show();
            this.Close();
        }
    }
}
