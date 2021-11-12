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
    /// Interaction logic for WinABMCliente.xaml
    /// </summary>
    public partial class WinABMCliente : Window
    {
        private Cliente cliMod = new Cliente();
        private int iTipo;

        public WinABMCliente()
        {
            InitializeComponent();
        }

        public WinABMCliente(Cliente cliente, int accion)
        {
            InitializeComponent();
            iTipo = accion;
            cliMod = cliente;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (iTipo == 1)
            {
                lblTitulo.Content = "ALTA CLIENTE";
                txtNombreVentana.Text = "Alta Cliente";
            }
            else
            {
                lblTitulo.Content = "MODIFICAR CLIENTE";
                txtNombreVentana.Text = "Modificar Cliente";
                cargarCampos();
            }
            gContenedor.DataContext = cliMod;
        }

        private Cliente cargarDatos()
        {
            Cliente oCliente = new Cliente();
            oCliente.Cli_Apellido = txtApellido.Text;
            oCliente.Cli_Nombre = txtNombre.Text;
            oCliente.Cli_Email = txtEmail.Text;
            oCliente.Cli_Domicilio = txtDomicilio.Text;
            oCliente.Cli_Telefono = txtTelefono.Text;
            oCliente.Cli_Dni = txtDni.Text;
            return oCliente;
        }

        private void cargarCampos()
        {
            txtApellido.Text = cliMod.Cli_Apellido;
            txtNombre.Text = cliMod.Cli_Nombre;
            txtEmail.Text = cliMod.Cli_Email;
            txtTelefono.Text = cliMod.Cli_Telefono;
            txtDomicilio.Text = cliMod.Cli_Domicilio;
            txtDni.Text = cliMod.Cli_Dni;
        }

        private void txtTelefono_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtTelefono.Text.All(char.IsDigit))
            {
                MessageBox.Show("Debe ingresar un número", "ERROR");
                txtTelefono.Text = txtTelefono.Text.Remove(txtTelefono.Text.Count() - 1);
            }
        }

        private void txtDni_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtDni.Text.All(char.IsDigit))
            {
                MessageBox.Show("Debe ingresar un número", "ERROR");
                txtDni.Text = txtDni.Text.Remove(txtDni.Text.Count() - 1);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinTablaClientes = new WinTablaClientesABM();
            wWinTablaClientes.Show();
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtApellido.Text != "" && txtNombre.Text != "" && txtTelefono.Text != "" && txtDni.Text != "")
            {
                if (txtDni.Text.Length > 5 && txtTelefono.Text.Length > 5)
                {
                    if (iTipo == 1)
                    {
                        try
                        {
                            Cliente oCliente = cargarDatos();
                            MessageBoxResult result = MessageBox.Show(encadenarDatosCliente(oCliente) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                TrabajarCliente.agregarCliente(oCliente);
                                MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                                limpiarCampos();
                                Window winTablaCliente = new WinTablaClientesABM();
                                winTablaCliente.Show();
                                this.Close();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("El dni ya se encuentra registrado", "ERROR CLIENTE EXISTENTE");
                            txtDni.Focus();
                        }
                    }
                    else
                    {
                        try
                        {
                            Cliente oCliente = cargarDatos();
                            MessageBoxResult result = MessageBox.Show(encadenarDatosCliente(oCliente) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                oCliente.Cli_Id = cliMod.Cli_Id;
                                TrabajarCliente.editarCliente(oCliente);
                                MessageBox.Show("DATOS MODIFICADOS CON EXITO!");
                                limpiarCampos();
                                Window winTablaCliente = new WinTablaClientesABM();
                                winTablaCliente.Show();
                                this.Close();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("El dni ya se encuentra registrado", "ERROR CLIENTE EXISTENTE");
                            txtDni.Focus();
                        }
                    }
                }
                else
                {
                    if (txtDni.Text.Length < 6 && txtTelefono.Text.Length < 6)
                    {
                        MessageBox.Show("El dni y el teléfono deben contener al menos 6 números", "ERROR");
                        txtDni.Focus();
                    }
                    else
                    {
                        if (txtDni.Text.Length < 6)
                        {
                            MessageBox.Show("El dni debe contener al menos 6 números", "ERROR DNI");
                            txtDni.Focus();
                        }
                        else
                        {
                            MessageBox.Show("El teléfono debe contener al menos 6 números", "ERROR TELÉFONO");
                            txtTelefono.Focus();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("El Apellido, Nombre, Dni y Teléfono son obligatorios", "ERROR CAMPO VACÍO");
            }
        }

        private string encadenarDatosCliente(Cliente oCliente)
        {
            string sCadenaDatosCliente = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Nombre:  " + oCliente.Cli_Nombre + "\n" +
                                              "Apellido:  " + oCliente.Cli_Apellido + "\n" +
                                              "Domicilio:  " + oCliente.Cli_Domicilio + "\n" +
                                              "Email:  " + oCliente.Cli_Email + "\n" +
                                              "Telefono:  " + oCliente.Cli_Telefono + "\n";
            return sCadenaDatosCliente;
        }

        private void limpiarCampos()
        {
            txtApellido.Clear();
            txtNombre.Clear();
            txtDomicilio.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
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
