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
    /// Interaction logic for WinMesas.xaml
    /// </summary>
    public partial class WinMesas : Window
    {
        private int mesa = 0;
        private int cantMesas = 20;
        private int cantColumnas = 0;
        private int cantFilas = 0;

        public WinMesas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            determinarOrden();
            double anchoBoton = (mesas.Width - (10*cantColumnas)) / cantColumnas;
            double altoBoton = (mesas.Height - (10*cantFilas)) / cantFilas;
            for (int j = 0; j < cantMesas; j++)
            {
                mesa = mesa + 1;
                Button temp = new Button();
                temp.Height = altoBoton;
                temp.Width = anchoBoton;
                modificarBoton(temp);
                mesas.Children.Add(temp);
            }
        }

        private Button modificarBoton(Button temp)
        {
            temp.Margin = new Thickness(10, 0, 0, 10);
            temp.Name = "button" + (mesa);
            temp.Content = "Mesa " + (mesa);
            if (temp.Content.ToString() == "Mesa 11" || temp.Content.ToString() == "Mesa 17")
                temp.Background = Brushes.Red;
            else
                temp.Background = Brushes.Green;
            temp.FontSize = 20;
            temp.Foreground = Brushes.White;
            temp.Click += new RoutedEventHandler(mesa_click);
            return temp;
        }

        private void determinarOrden()
        {
            bool encontrado = false;
            for (int m = 6; m > 3 && encontrado == false; m--)
            {
                if (cantMesas % m == 0)
                {
                    encontrado = true;
                    cantFilas = cantMesas / m;
                    cantColumnas = cantMesas / cantFilas;
                }
                else
                {
                    cantColumnas = 5;
                    cantFilas = (cantMesas / cantColumnas) + 1;
                }
            }
        }

        private void mesa_click(Object sender, EventArgs e)
        {
            Button mesaSeleccionada = (Button)sender;
            if (mesaSeleccionada.Background == Brushes.Green)
            {
                MessageBoxResult result = MessageBox.Show("Ocupar mesa?", "MESA LIBRE", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    mesaSeleccionada.Background = Brushes.Red;
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Liberar mesa?", "MESA OCUPADA", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    mesaSeleccionada.Background = Brushes.Green;
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenuMozo = new WinMenuMozo();
            wWinMenuMozo.Show();
            this.Close();
        }

    }
}
