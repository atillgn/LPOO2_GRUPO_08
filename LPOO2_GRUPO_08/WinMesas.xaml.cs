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
using System.Text.RegularExpressions;

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
        Style stl;

        public WinMesas()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            controlarMesas();
            determinarOrden();
            double anchoBoton = (mesas.Width - (10 * cantColumnas)) / cantColumnas;
            double altoBoton = (mesas.Height - (10 * cantFilas)) / cantFilas;
            for (int j = 0; j < cantMesas; j++)
            {
                Button temp = new Button();
                temp.Height = altoBoton;
                temp.Width = anchoBoton;
                temp.Style = this.FindResource("BtnMesas") as Style;
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
            temp.MouseDoubleClick += new MouseButtonEventHandler(mesa_MouseDoubleClick);
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

        private Button mesaSeleccionada = new Button();

        private void mesa_click(Object sender, EventArgs e)
        {
            mesaSeleccionada.Foreground = Brushes.White;
            lbEstados.SelectedIndex = -1;
            mesaSeleccionada = (Button)sender;
            mesaSeleccionada.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#1cbfff");
        }

        private void mesa_MouseDoubleClick(Object sender, MouseButtonEventArgs e)
        {
            lbEstados.SelectedIndex = -1;
            Button mesaElegida = (Button)sender;
            if (mesaElegida.Background == Brushes.Green)
             {
                 btnDisabled(btnCantidadMesas);
                 MessageBoxResult result = MessageBox.Show("Ocupar mesa?", "MESA LIBRE", MessageBoxButton.YesNo);
                 if (result == MessageBoxResult.Yes)
                 {
                     Mesa mesaEditable = TrabajarMesa.buscarMesaByPosicion(Convert.ToInt32(Regex.Replace(Convert.ToString(mesaElegida.Content), @"[^\d]", "")));
                     Window winPedido = new WinPedidos(mesaEditable, 3);
                     winPedido.Show();
                     this.Close();
                 }
             }else
            {
                MessageBoxResult result = MessageBox.Show("Generar Factura?", "FACTURAR", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Mesa oMesaOcupada = TrabajarMesa.buscarMesaByPosicion(Convert.ToInt32(Regex.Replace(Convert.ToString(mesaElegida.Content), @"[^\d]", "")));
                    oMesaOcupada.Mesa_Estado = 1;
                    TrabajarMesa.editarMesa(oMesaOcupada);

                    Pedido pedido = TrabajarPedido.buscarPedidoByMesaAndEstado(oMesaOcupada.Mesa_Id);
                    pedido.Ped_Facturado = true;
                    TrabajarPedido.editar_Pedido(pedido);

                    Window winComprobante = new WinComprobantePedido(pedido.Ped_Id, 3);
                    winComprobante.Show();
                    this.Close();
                }
            }
        }

        private void controlarMesas()
        {
            foreach (Mesa m in listaMesas)
            {
                if (m.Mesa_Estado != 1 && btnCantidadMesas.IsEnabled == true)
                    btnDisabled(btnCantidadMesas);
            }
        }

        private void btnDisabled(Button b)
        {
            b.IsEnabled = false;
            Style stl = Application.Current.FindResource("BtnDisLogin") as Style;
            b.Style = stl;

        }

        private void btnEnable(Button b)
        {
            b.IsEnabled = true;
            Style stl = Application.Current.FindResource("BtnLogin") as Style;
            b.Style = stl;
        }

        private void lbEstados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbEstados.SelectedIndex != -1)
            {
                try
                {
                    if (mesaSeleccionada.Background != Brushes.Green)
                    {
                        if (lbEstados.SelectedIndex != 0)
                        {
                            string result = Regex.Replace(Convert.ToString(mesaSeleccionada.Content), @"[^\d]", "");
                            Mesa mesaEditable = TrabajarMesa.buscarMesaByPosicion(Convert.ToInt32(result));
                            mesaEditable.Mesa_Estado = lbEstados.SelectedIndex + 1;
                            TrabajarMesa.editarMesa(mesaEditable);
                            asignarColorFondo(mesaSeleccionada, mesaEditable);
                        }
                        else
                        {
                            MessageBox.Show("No se puede cambiar el estado de una mesa a libre, primero debe facturar", "FACTURAR");
                            lbEstados.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se puede cambiar el estado de una mesa libre, asigne un pedido", "ASIGNE PEDIDO");
                        lbEstados.SelectedIndex = -1;
                    }
                }
                catch (Exception a)
                {
                    MessageBox.Show("Primer eliga una mesa", "NO SELECCIONA MESA");
                    lbEstados.SelectedIndex = -1;
                }
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

        private void btnCantidadMesas_Click(object sender, RoutedEventArgs e)
        {
            Window winCantMesas = new WinCantMesas();
            winCantMesas.Show();
            this.Close();
        }

    }
}
