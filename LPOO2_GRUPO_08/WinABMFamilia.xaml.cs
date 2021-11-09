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
    /// Interaction logic for WinABMFamilia.xaml
    /// </summary>
    public partial class WinABMFamilia : Window
    {
        bool edit;
        ObservableCollection<Familia> obvFam;
        Familia famMod= new Familia();
        public WinABMFamilia()
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
            if (lvFam.SelectedIndex != -1)
            {
                Familia fam = (Familia)lvFam.SelectedItem;
                /*
                btnEliminar.IsEnabled = true;
                btnModificar.IsEnabled = true;*/
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
                    Familia oFamilia = cargarDatos();
                    MessageBoxResult result = MessageBox.Show(encadenarDatos(oFamilia) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {   
                        try
                        {
                            oFamilia.Fam_Id = famMod.Fam_Id;
                            TrabajarFamilia.editarFamilia(oFamilia);
                            MessageBox.Show("DATOS MODIFICADOS CON EXITO!");
                            limpiarCampos();
                            btnDisabled(btnModificar);
                            btnDisabled(btnEliminar);
                            btnGuardar.Content = "Guardar";
                        }
                        catch (Exception a)
                        {
                            MessageBox.Show("La familia ya se encuentra registrado", "ERROR FAMILIA EXISTENTE");
                            txtDescrip.Focus();
                        }
                    }
                }
                else
                {
                    try
                    {
                        Familia oFamilia = cargarDatos();
                        MessageBoxResult result = MessageBox.Show(encadenarDatos(oFamilia) + "\n Guardar datos? \n", "", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            TrabajarFamilia.agregarFamilia(oFamilia);
                            MessageBox.Show("DATOS GUARDADOS CON EXITO!");
                            limpiarCampos();
                            obvFam.Add(oFamilia);
                        }
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("La familia ya se encuentra registrado", "ERROR FAMILIA EXISTENTE");
                        txtDescrip.Focus();
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
            famMod = (Familia)lvFam.SelectedItem;
            cargarCampos();
            btnGuardar.Content = "Editar";
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Familia fam = (Familia)lvFam.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Seguro que desea eliminar a \"" + fam.Fam_Descripcion + "\" ?", "ELIMINAR", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TrabajarFamilia.borrarFamilia(fam.Fam_Id);
                    MessageBox.Show("FAMILIA ELIMINADO CON ÉXITO");
                    btnDisabled(btnModificar);
                    btnDisabled(btnEliminar);
                    lvFam.SelectedIndex = -1;
                    var FamDelete = obvFam.Single(i => i.Fam_Id == fam.Fam_Id);
                    obvFam.Remove(FamDelete);
                }
                catch (Exception a)
                {
                    MessageBox.Show("Esta familia esta vinculada a otros articulos", "ERROR ELIMINACION FAMILIA");
                }
                
            }
        }

        private Familia cargarDatos()
        {
            Familia oFamilia = new Familia();
            oFamilia.Fam_Descripcion = txtDescrip.Text;
            return oFamilia;
        }
       
        private void cargarCampos()
        {
            txtDescrip.Text = famMod.Fam_Descripcion;
        }

        private string encadenarDatos(Familia oFamilia)
        {
            string sCadenaDatos = "DATOS A GUARDAR: \n" +
                                              "\n" +
                                              "Descripcion: " + oFamilia.Fam_Descripcion + "\n";
            return sCadenaDatos;
        }

        private void limpiarCampos()
        {
            txtDescrip.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObjectDataProvider odp = (ObjectDataProvider)(this.Resources["listFam"]);
            obvFam = odp.Data as ObservableCollection<Familia>;
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
            edit = false;
        }

        private void txtDescrip_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
        }

        private void txtDescrip_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            btnDisabled(btnModificar);
            btnDisabled(btnEliminar);
            lvFam.SelectedIndex = -1;
            lblDescrip.Foreground = new SolidColorBrush(Color.FromRgb(28, 191, 255));
        }

        private void txtDescrip_LostFocus(object sender, RoutedEventArgs e)
        {
            lblDescrip.Foreground = Brushes.White;
        }
    }
}
