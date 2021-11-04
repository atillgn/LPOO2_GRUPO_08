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
using System.Collections.ObjectModel;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Interaction logic for WinMesas.xaml
    /// </summary>
    public partial class WinMesas : Window
    {
        private int cantMesas = TrabajarMesa.contarMesas();
        private int cantColumnas = 0;
        private int cantFilas = 0;
        private ObservableCollection<Mesa> listaMesas = TrabajarMesa.traerMesasObjetos();

        public WinMesas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            determinarOrden();
            double anchoBoton = (mesas.Width - (10 * cantColumnas)) / cantColumnas;
            double altoBoton = (mesas.Height - (10 * cantFilas)) / cantFilas;
            for (int j = 0; j < cantMesas; j++)
            {
                Button temp = new Button();
                temp.Height = altoBoton;
                temp.Width = anchoBoton;
                modificarBoton(temp, listaMesas[j]);
                mesas.Children.Add(temp);
            }
        }

        private Button modificarBoton(Button temp, Mesa mesaActual)
        {
            temp.Margin = new Thickness(10, 0, 0, 10);
            temp.Name = "button" + (mesaActual.Mesa_Posicion);
            temp.Content = "Mesa " + (mesaActual.Mesa_Posicion);
            asignarColorFondo(temp, mesaActual);
            temp.FontSize = 20;
            temp.Foreground = Brushes.White;
            temp.Click += new RoutedEventHandler(mesa_click);
            return temp;
        }

        private Button asignarColorFondo(Button temp, Mesa mesaActual)
        {
            switch (mesaActual.Mesa_Estado)
            {
                case 1:
                    temp.Background = Brushes.Green;
                    break;
                case 2:
                    temp.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#d2691e");
                    break;
                case 3:
                    temp.Background = Brushes.Red;
                    break;
                case 4:
                    temp.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#9932cc");
                    break;
                case 5:
                    temp.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#90ee90");
                    break;
                case 6:
                    temp.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#ff6347");
                    break;
                case 7:
                    temp.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#4169e1");
                    break;
                case 8:
                    temp.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#91ab5c");
                    break;
            }
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

    }
}
