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
    /// Interaction logic for WinEditArticulo.xaml
    /// </summary>
    public partial class WinEditArticulo : Window
    {
        public Articulo oArticulo = new Articulo();
        public WinEditArticulo()
        {
            InitializeComponent();
        }

        public WinEditArticulo(Articulo art)
        {
            InitializeComponent();
            oArticulo = art;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDescrip.Text = oArticulo.Art_Descripcion;
            txtPrecio.Text = Convert.ToString(oArticulo.Art_Precio);
            cmbCategoria.SelectedValue = oArticulo.Cat_Id;
            cmbFamilia.SelectedValue = oArticulo.Fam_Id;
            cmbUM.SelectedValue = oArticulo.Um_Id;
            cmbCategoria.Text = oArticulo.ACategoria.Cat_Descripcion;
            cmbFamilia.Text = oArticulo.AFamilia.Fam_Descripcion;
            cmbUM.Text = oArticulo.AUnidadMedida.Um_Descripcion;
            chkStock.IsChecked = oArticulo.Art_ManejaStock;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinABMArticulos = new WinABMArticulos();
            wWinABMArticulos.Show();
            this.Close();
        }

        private Articulo cargarDatos()
        {
            oArticulo.Art_Descripcion = txtDescrip.Text;
            oArticulo.Art_Precio = Convert.ToDecimal(txtPrecio.Text);
            oArticulo.Cat_Id = (int)cmbCategoria.SelectedValue;
            oArticulo.Fam_Id = (int)cmbFamilia.SelectedValue;
            oArticulo.Um_Id = (int)cmbUM.SelectedValue;
            oArticulo.Art_ManejaStock = (bool)chkStock.IsChecked;

            return oArticulo;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            Articulo oArticulo = cargarDatos();
            MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TrabajarArticulos.editarArticulo(oArticulo);
                }
                catch (Exception a)
                {
                    MessageBox.Show("No se puede");
                }
                MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                Window wWinABMArticulos = new WinABMArticulos();
                wWinABMArticulos.Show();
                this.Close();
                limpiarCampos();
            }
        }

        private string encadenarDatosArticulo(Articulo oArticulo)
        {
            string sCadenaDatosArticulo = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Descripcion:  " + oArticulo.Art_Descripcion + "\n" +
                                              "Familia:  " + oArticulo.Fam_Id + ": " + cmbFamilia.Text + "\n" +
                                              "UM:  " + oArticulo.Um_Id + ": " + cmbUM.Text + "\n" +
                                              "Precio:  " + oArticulo.Art_Precio + "\n" +
                                              "Maneja Stock:  " + oArticulo.Art_ManejaStock + "\n";
            return sCadenaDatosArticulo;
        }

        private void limpiarCampos()
        {
            txtDescrip.Clear();
            txtPrecio.Clear();
            cmbCategoria.SelectedValue = 1;
            cmbUM.SelectedValue = 1;
            cmbFamilia.SelectedValue = 1;
            chkStock.IsChecked = false;
        }

        private void verArticulos_Click(object sender, RoutedEventArgs e)
        {
            Window wABMArticulos = new WinABMArticulos();
            wABMArticulos.Show();
            this.Close();
        }
    }
}
