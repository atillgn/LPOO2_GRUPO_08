﻿using System;
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
using System.Collections.ObjectModel;
using ClasesBase;

namespace LPOO2_GRUPO_08
{
    /// <summary>
    /// Lógica de interacción para WinCantMesas.xaml
    /// </summary>
    public partial class WinCantMesas : Window
    {
        public WinCantMesas()
        {
            InitializeComponent();
        }
        private ObservableCollection<Mesa> listaMesas;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listaMesas = TrabajarMesa.traerMesasObjetos();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCantidad.Text != "")
            {
               
                int cantNecesaria = Convert.ToInt32(txtCantidad.Text);
                if (cantNecesaria > 0 && cantNecesaria < 51)
                {
                    int cantActual = listaMesas.Count;
                    if (cantActual > cantNecesaria)
                    {
                        for (int i = cantNecesaria + 1; i <= cantActual; i++)
                        {
                            Mesa oMesaEliminada = TrabajarMesa.buscarMesaByPosicion(i);
                            ObservableCollection<Pedido> listaPedidos = TrabajarPedido.buscarPedidosByMesa(oMesaEliminada.Mesa_Id);
                            foreach (Pedido p in listaPedidos)
                            {
                                p.Mesa_Id = TrabajarMesa.buscarMesaByPosicion(cantNecesaria).Mesa_Id;
                                TrabajarPedido.editar_Pedido(p);
                            }
                            TrabajarMesa.borrarMesa(i);
                        }
                    }
                    else
                    {
                        if (cantActual < cantNecesaria)
                        {
                            for (int i = cantActual + 1; i <= cantNecesaria; i++)
                            {
                                Mesa oMesaNueva = new Mesa(i, 1);
                                TrabajarMesa.agregarMesa(oMesaNueva);
                            }
                        }
                    }
                    btnVolver_Click(sender, e);

                }
                else
                {
                    MessageBox.Show("La cantidad de mesas debe ser mayor a 0 y menor a 51", "ERROR");
                }

            }
            else
            {
                MessageBox.Show("Debe ingresar un número", "ERROR");
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            Window winMesas = new WinMesas();
            winMesas.Show();
            this.Close();
        }

        private void txtCantidad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!txtCantidad.Text.All(char.IsDigit))
            {
                MessageBox.Show("Debe ingresar un número", "ERROR");
                txtCantidad.Text = txtCantidad.Text.Remove(txtCantidad.Text.Count() - 1);
            }
        }
    }
}
