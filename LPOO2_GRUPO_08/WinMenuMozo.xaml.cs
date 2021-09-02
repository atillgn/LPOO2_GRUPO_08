﻿using System;
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
    /// Interaction logic for WinMenuMozo.xaml
    /// </summary>
    public partial class WinMenuMozo : Window
    {
        public WinMenuMozo()
        {
            InitializeComponent();
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
