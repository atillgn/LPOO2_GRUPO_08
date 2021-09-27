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
    /// Lógica de interacción para WinABMArticulo.xaml
    /// </summary>
    public partial class WinABMArticulo : Window
    {
        public WinABMArticulo()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenuAdmin = new WinMenuAdmin(1);
            wWinMenuAdmin.Show();
            this.Close();
        }

        private Articulo cargarDatos()
        {
            Articulo oArticulo = new Articulo();
            oArticulo.Art_Descripcion = txtDescrip.Text;
            oArticulo.Art_ManejaStock = (bool)chkStock.IsChecked;
            oArticulo.Art_Precio = Convert.ToDecimal(txtPrecio.Text) ;
            oArticulo.Fam_Id = determinarFamilia(cmbFamilia.Text);
            oArticulo.Um_Id = determinarUnidad(cmbUM.Text);
            return oArticulo;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Articulo oArticulo = cargarDatos();
            MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                limpiarCampos();
            }
        }

        private int determinarFamilia(string sFamilia)
        {
            int iId = 0;
            switch (sFamilia){
                case "Bebidas":
                    iId = 1;
                    break;
                case "Producto terminado":
                    iId = 2;
                    break;
                case "Materia prima":
                    iId = 3;
                    break;
            }
            return iId;
        }

        private int determinarUnidad(string sUnidad)
        {
            int iId = 0;
            switch (sUnidad)
            {
                case "Litros":
                    iId = 1;
                    break;
                case "Kilos":
                    iId = 2;
                    break;
                case "Unidades":
                    iId = 3;
                    break;
            }
            return iId;
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
            cmbUM.Text = "";
            cmbFamilia.Text = "";
            chkStock.IsChecked = false;
        }

    }
}
