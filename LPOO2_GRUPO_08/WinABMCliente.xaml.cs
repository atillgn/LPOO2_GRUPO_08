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
        public WinABMCliente()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenuMozo = new WinMenuMozo();
            wWinMenuMozo.Show();
            this.Close();
        }

        private Cliente cargarDatos()
        { 
            Cliente oCliente = new Cliente();
            oCliente.Cli_Nombre = txtNombre.Text;
            oCliente.Cli_Apellido = txtApellido.Text;
            oCliente.Cli_Domicilio = txtDomicilio.Text;
            oCliente.Cli_Email = txtEmail.Text;
            oCliente.Cli_Telefono = txtEmail.Text;
            return oCliente;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Cliente oCliente = cargarDatos();
            MessageBoxResult result = MessageBox.Show(encadenarDatosCliente(oCliente) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                limpiarCampos();
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
            txtEmail.Text = "";
        }
    }
}
