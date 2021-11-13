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
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Lógica de interacción para WinABMArticulo.xaml
    /// </summary>
    public partial class WinABMArticulo : Window
    {
        private bool edit;
        private Articulo oArticuloModificado;
        private int iPosicion;

        public WinABMArticulo()
        {
            InitializeComponent();
        }

        public WinABMArticulo(Articulo oArticulo, int posicion)
        {
            InitializeComponent();
            oArticuloModificado = oArticulo;
            iPosicion = posicion;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCategoria.SelectedIndex = 0;
            cmbFamilia.SelectedIndex = 0;
            cmbUM.SelectedIndex = 0;
            if (oArticuloModificado == null)
            {
                lblTitulo.Content = "ALTA ARTÍCULO";
                txbNombreVentana.Text = "Alta Artículo";
                oArticuloModificado = new Articulo();
                edit = false;
            }
            else
            {
                lblTitulo.Content = "EDITAR ARTÍCULO";
                txbNombreVentana.Text = "Editar Artículo";
                cargarArticulo();
                edit = true;
            }
            dpContenedor.DataContext = oArticuloModificado;
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinABMArticulos = new WinABMArticulos(iPosicion);
            wWinABMArticulos.Show();
            this.Close();
        }

        private Articulo cargarDatos()
        {
            Articulo oArticulo = new Articulo();
            oArticulo.Art_Codigo = txtCodigo.Text;
            oArticulo.Art_Descripcion = (string)txtDescrip.Text;
            oArticulo.Art_Precio_String = txtPrecio.Text;
            //Trace.WriteLine(oArticulo.Art_Precio_String);
            //oArticulo.Art_Precio = Convert.ToDecimal(oArticulo.Art_Precio_String);
            //oArticulo.Art_Precio = Convert.ToDecimal(txtPrecio.Text);
            //MessageBox.Show(txtPrecio.Text + " " + txtPrecio.Text.Replace('.', ',') + " " + oArticulo.Art_Precio);
            oArticulo.Cat_Id = (int)cmbCategoria.SelectedValue;
            oArticulo.Fam_Id = (int)cmbFamilia.SelectedValue;  
            oArticulo.Um_Id = (int)cmbUM.SelectedValue;
            oArticulo.Art_ManejaStock = (bool)chkStock.IsChecked;
            oArticulo.Art_Img = (string)txtImg.Text;

            return oArticulo;
        }

        private void cargarArticulo()
        {
            txtCodigo.Text = oArticuloModificado.Art_Codigo;
            txtDescrip.Text = oArticuloModificado.Art_Descripcion;
            oArticuloModificado.Art_Precio_String = Convert.ToString(oArticuloModificado.Art_Precio);
            txtPrecio.Text = oArticuloModificado.Art_Precio_String;
            cmbCategoria.SelectedValue = oArticuloModificado.Cat_Id;
            cmbFamilia.SelectedValue = oArticuloModificado.Fam_Id;
            cmbUM.SelectedValue = oArticuloModificado.Um_Id;
            cmbCategoria.Text = oArticuloModificado.ACategoria.Cat_Descripcion;
            cmbFamilia.Text = oArticuloModificado.AFamilia.Fam_Descripcion;
            cmbUM.Text = oArticuloModificado.AUnidadMedida.Um_Descripcion;
            chkStock.IsChecked = oArticuloModificado.Art_ManejaStock;
            txtImg.Text = oArticuloModificado.Art_Img;
            try
            {
                img.Source = new BitmapImage(new Uri(txtImg.Text));
            }
            catch { }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            Articulo oArticulo = cargarDatos();
            string error = oArticulo.isValid();
            if (error != null)
            {
                MessageBox.Show(error, "Error al ingresar los datos");
                return;
            }
            oArticulo.colocarPrecio();
            if (edit)
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        oArticulo.Art_Id = oArticuloModificado.Art_Id;
                        TrabajarArticulos.editarArticulo(oArticulo);
                        MessageBox.Show("DATOS MODIFICADOS CON EXITO!");
                        Window wWinABMArticulos = new WinABMArticulos(iPosicion);
                        wWinABMArticulos.Show();
                        this.Close();
                        limpiarCampos();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("El artículo ya se encuentra registrado", "ERROR ARTÍCULO EXISTENTE");
                    txtCodigo.Focus();
                }
            }
            else
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        TrabajarArticulos.agregarArticulo(oArticulo);
                        MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                        Window wWinABMArticulos = new WinABMArticulos(-1);
                        wWinABMArticulos.Show();
                        this.Close();
                        limpiarCampos();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("El artículo ya se encuentra registrado", "ERROR ARTÍCULO EXISTENTE");
                    txtCodigo.Focus();
                }
            }

            /*if (txtCodigo.Text != "" && txtDescrip.Text != "" && txtPrecio.Text != "" && cmbCategoria.Text != "" && cmbFamilia.Text != "" && cmbUM.Text != "" && txtImg.Text != "")
            {
                //Articulo oArticulo = new Articulo();
                oArticulo = cargarDatos();
                if (oArticuloModificado != null)
                {
                    try
                    {
                        
                        MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            oArticulo.Art_Id = oArticuloModificado.Art_Id;
                            TrabajarArticulos.editarArticulo(oArticulo);
                            MessageBox.Show("DATOS MODIFICADOS CON EXITO!");
                            Window wWinABMArticulos = new WinABMArticulos(iPosicion);
                            wWinABMArticulos.Show();
                            this.Close();
                            limpiarCampos();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Los articulos no pueden tener datos repetidos");
                    }
                }
                else
                {
                    try
                    {
                        
                        MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            TrabajarArticulos.agregarArticulo(oArticulo);
                            MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                            Window wWinABMArticulos = new WinABMArticulos(-1);
                            wWinABMArticulos.Show();
                            this.Close();
                            limpiarCampos();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Los articulos no pueden tener datos repetidos");
                    }
                }


            }
            else
            {
                MessageBox.Show("El articulo no puede tener campos vacio");
            }*/
            /*if (oArticuloModificado == null)
            {
                MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        TrabajarArticulos.agregarArticulo(oArticulo);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No se puede");
                    }
                    MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                    Window wWinABMArticulos = new WinABMArticulos(-1);
                    wWinABMArticulos.Show();
                    this.Close();
                    limpiarCampos();
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(encadenarDatosArticulo(oArticulo) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        oArticulo.Art_Id = oArticuloModificado.Art_Id;
                        TrabajarArticulos.editarArticulo(oArticulo);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No se puede");
                    }
                    MessageBox.Show("DATOS MODIFICADOS CON EXITO!");
                    Window wWinABMArticulos = new WinABMArticulos(iPosicion);
                    wWinABMArticulos.Show();
                    this.Close();
                    limpiarCampos();
                }
            }*/
        }

        private string encadenarDatosArticulo(Articulo oArticulo)
        {
            string sCadenaDatosArticulo = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Código:  " + oArticulo.Art_Codigo + "\n" +
                                              "Descripcion:  " + oArticulo.Art_Descripcion + "\n" +
                                              "Familia:  " + oArticulo.Fam_Id + ": " + cmbFamilia.Text + "\n" +
                                              "UM:  " + oArticulo.Um_Id + ": " + cmbUM.Text + "\n" +
                                              "Precio:  " + oArticulo.Art_Precio + "\n" +
                                              "Imagen: " + oArticulo.Art_Img + "\n" +
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
            txtImg.Text = "";
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

        private void btnCargar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Seleccione una imagen";
            dialog.Filter = "All supported graphics|*.jpg; *.jpeg; *.png" + "JPEG (*.jpg; *.jpeg)|*.jpg;*jpeg" + "Portable Network Graphic (*.png)|*.png";
            if (dialog.ShowDialog() == true)
            {
                txtImg.Text = dialog.FileName;
                img.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }
    }
}
