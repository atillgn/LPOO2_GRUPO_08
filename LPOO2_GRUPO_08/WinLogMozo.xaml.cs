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
    /// Interaction logic for WinLogMozo.xaml
    /// </summary>
    public partial class WinLogMozo : Window
    {
        public WinLogMozo()
        {
            InitializeComponent();
        }

        private void bntMinimizedScreen_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window wWinLogin = new MainWindow();
            wWinLogin.Show();
            this.Close();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWindowMenuMozo = new WinMenuMozo();
            wWindowMenuMozo.Show();
            this.Close();
        }
    }
}
