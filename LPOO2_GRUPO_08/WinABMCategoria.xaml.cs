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
    /// Interaction logic for WinABMCategoria.xaml
    /// </summary>
    public partial class WinABMCategoria : Window
    {
        bool edit;
        ObservableCollection<Categoria> obvCat;
        Categoria catMod = new Categoria();
        public WinABMCategoria()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window wWinMenuAdmin = new WinMenuAdmin();
            wWinMenuAdmin.Show();
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

        private Categoria cargarDatos()
        {
            Categoria oCategoria = new Categoria();
            oCategoria.Cat_Descripcion = txtDescrip.Text;
            return oCategoria;
        }

        private void cargarCampos()
        {
            txtDescrip.Text = catMod.Cat_Descripcion;
        }

        private string encadenarDatos(Categoria oCategoria)
        {
            string sCadenaDatos = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Descripcion: " + oCategoria.Cat_Descripcion + "\n";
            return sCadenaDatos;
        }

        private void limpiarCampos()
        {
            txtDescrip.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)(this.Resources["listCat"]);
            obvCat = odp.Data as ObservableCollection<Categoria>;
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
            edit = false;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtDescrip.Text != "")
            {
                if (edit)
                {
                    Categoria oCategoria = cargarDatos();
                    MessageBoxResult result = MessageBox.Show(encadenarDatos(oCategoria) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            oCategoria.Cat_Id = catMod.Cat_Id;
                            TrabajarCategoria.editarCategoria(oCategoria);
                            MessageBox.Show("DATOS MODIFICADOS CON EXITO!");
                            limpiarCampos();
                            btnDisabled(btnModificar);
                            btnDisabled(btnEliminar);
                            btnGuardar.Content = "Guardar";
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show("La categoria ya se encuentra registrado", "ERROR CATEGORIA EXISTENTE");
                            txtDescrip.Focus();
                        }
                    }
                }
                else
                {
                    try
                    {
                        Categoria oCategoria = cargarDatos();
                        MessageBoxResult result = MessageBox.Show(encadenarDatos(oCategoria) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            TrabajarCategoria.agregarCategoria(oCategoria);
                            MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                            limpiarCampos();
                            obvCat.Add(oCategoria);
                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("La categoria ya se encuentra registrado", "ERROR CATEGORIA EXISTENTE");
                        txtDescrip.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("No pueden haber campos vacíos", "ERROR CAMPO VACÍO");
            }
        }

        private void txtDescrip_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtDescrip_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void txtDescrip_LostFocus(object sender, RoutedEventArgs e)
        {

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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvCat.SelectedIndex != -1)
            {
                Categoria cat = (Categoria)lvCat.SelectedItem;
                btnEnable(btnEliminar);
                btnEnable(btnModificar);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Categoria cat = (Categoria)lvCat.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Seguro que desea eliminar a \"" + cat.Cat_Descripcion + "\" ?", "ELIMINAR", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TrabajarCategoria.borrarCategoria(cat.Cat_Id);
                    MessageBox.Show("CATEGORIA ELIMINADA CON ÉXITO");
                    btnDisabled(btnModificar);
                    btnDisabled(btnEliminar);
                    lvCat.SelectedIndex = -1;
                    var FamDelete = obvCat.Single(i => i.Cat_Id == cat.Cat_Id);
                    obvCat.Remove(FamDelete);
                }
                catch (Exception a)
                {
                    MessageBox.Show("Esta familia esta vinculada a otros articulos", "ERROR ELIMINACION FAMILIA");
                }

            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            edit = true;
            catMod = (Categoria)lvCat.SelectedItem;
            cargarCampos();
            btnGuardar.Content = "Editar";
        }
    }
}
