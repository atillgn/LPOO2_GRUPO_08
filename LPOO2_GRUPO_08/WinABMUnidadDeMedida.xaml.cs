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
    /// Interaction logic for WinABMUnidadDeMedida.xaml
    /// </summary>
    public partial class WinABMUnidadDeMedida : Window
    {
        bool edit;
        ObservableCollection<UnidadMedida> obvUni;
        UnidadMedida uniMod = new UnidadMedida();

        public WinABMUnidadDeMedida()
        {
            InitializeComponent();
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
            if (lvUnidad.SelectedIndex != -1)
            {
                UnidadMedida fam = (UnidadMedida)lvUnidad.SelectedItem;
                btnEnable(btnEliminar);
                btnEnable(btnModificar);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtDescrip.Text != "")
            {
                if (edit)
                {
                    UnidadMedida oUnidad = cargarDatos();
                    MessageBoxResult result = MessageBox.Show(encadenarDatos(oUnidad) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            oUnidad.Um_Id = uniMod.Um_Id;
                            TrabajarUnidadMedida.editarUnidadMedida(oUnidad);
                            MessageBox.Show("DATOS MODIFICADOS CON EXITO!");
                            limpiarCampos();
                            btnDisabled(btnModificar);
                            btnDisabled(btnEliminar);
                            btnGuardar.Content = "Guardar";
                            oUnidad = TrabajarUnidadMedida.traerUnidadMedidaObv().Single(i => i.Um_Id == oUnidad.Um_Id);
                            var indice = obvUni.IndexOf(uniMod);
                            obvUni.RemoveAt(indice);
                            obvUni.Insert(indice, oUnidad);
                            edit = false;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("La unidad ya se encuentra registrada", "ERROR UNIDAD EXISTENTE");
                            txtDescrip.Focus();
                        }
                    }
                }
                else
                {
                    try
                    {
                        UnidadMedida oUnidad = cargarDatos();
                        MessageBoxResult result = MessageBox.Show(encadenarDatos(oUnidad) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            TrabajarUnidadMedida.agregarUnidadMedida(oUnidad);
                            MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                            oUnidad = TrabajarUnidadMedida.traerUnidadMedidaObv().Single(i => i.Um_Descripcion == oUnidad.Um_Descripcion);
                            limpiarCampos();
                            obvUni.Add(oUnidad);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("La unidad ya se encuentra registrada", "ERROR UNIDAD EXISTENTE");
                        txtDescrip.Focus();
                        txtAbrev.Focus();
                    }
                }
            }
            else
            {
                MessageBox.Show("No pueden haber campos vacíos", "ERROR CAMPO VACÍO");
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            edit = true;
            uniMod = (UnidadMedida)lvUnidad.SelectedItem;
            cargarCampos();
            btnGuardar.Content = "Editar";
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            UnidadMedida uni = (UnidadMedida)lvUnidad.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Seguro que desea eliminar a \"" + uni.Um_Descripcion + "\" ?", "ELIMINAR", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TrabajarUnidadMedida.borrarUnidadMedida(uni.Um_Id);
                    MessageBox.Show("FAMILIA ELIMINADO CON ÉXITO");
                    btnDisabled(btnModificar);
                    btnDisabled(btnEliminar);
                    lvUnidad.SelectedIndex = -1;
                    var UniDelete = obvUni.Single(i => i.Um_Id == uni.Um_Id);
                    obvUni.Remove(UniDelete);
                }
                catch (Exception)
                {
                    MessageBox.Show("Esta unidad esta vinculada a otros articulos", "ERROR ELIMINACION UNIDAD");
                }

            }
        }

        private UnidadMedida cargarDatos()
        {
            UnidadMedida oUnidad = new UnidadMedida();
            oUnidad.Um_Descripcion = txtDescrip.Text;
            oUnidad.Um_Abrev = txtAbrev.Text;
            return oUnidad;
        }

        private void cargarCampos()
        {
            txtDescrip.Text = uniMod.Um_Descripcion;
            txtAbrev.Text = uniMod.Um_Abrev;
        }

        private string encadenarDatos(UnidadMedida oUnidad)
        {
            string sCadenaDatos = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Descripcion: " + oUnidad.Um_Abrev + " " + oUnidad.Um_Descripcion + "\n";
            return sCadenaDatos;
        }

        private void limpiarCampos()
        {
            txtDescrip.Clear();
            txtAbrev.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)(this.Resources["listUnidad"]);
            obvUni = odp.Data as ObservableCollection<UnidadMedida>;
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
            edit = false;
            dpContenedor.DataContext = uniMod;
        }

        private void txtAbrev_LostFocus(object sender, RoutedEventArgs e)
        {
            lblDescrip.Foreground = Brushes.White;
        }

        private void txtDescrip_LostFocus(object sender, RoutedEventArgs e)
        {
            lblDescrip.Foreground = Brushes.White;
        }

        private void txtAbrev_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
            lvUnidad.SelectedIndex = -1;
        }

        private void txtDescrip_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
            lvUnidad.SelectedIndex = -1;
        }

        private void txtDescrip_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
        }

        private void txtAbrev_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
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

    }
}
