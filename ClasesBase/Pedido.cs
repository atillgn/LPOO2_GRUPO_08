using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ClasesBase
{
    public class Pedido : IDataErrorInfo
    {
        private int ped_Id;

        public int Ped_Id
        {
            get { return ped_Id; }
            set { ped_Id = value; }
        }
        private int usu_Id;

        public int Usu_Id
        {
            get { return usu_Id; }
            set { usu_Id = value; }
        }
        private int mesa_Id;

        public int Mesa_Id
        {
            get { return mesa_Id; }
            set { mesa_Id = value; }
        }
        private int cli_Id;

        public int Cli_Id
        {
            get { return cli_Id; }
            set { cli_Id = value; }
        }
        private DateTime ped_FechaEmision;

        public DateTime Ped_FechaEmision
        {
            get { return ped_FechaEmision; }
            set { ped_FechaEmision = value; }
        }
        private DateTime ped_fechaEntrega;

        public DateTime Ped_fechaEntrega
        {
            get { return ped_fechaEntrega; }
            set { ped_fechaEntrega = value; }
        }
        private int ped_Comensales;

        public int Ped_Comensales
        {
            get { return ped_Comensales; }
            set { ped_Comensales = value; }
        }
        private bool ped_Facturado;

        public bool Ped_Facturado
        {
            get { return ped_Facturado; }
            set { ped_Facturado = value; }
        }

        private Mesa oMesa;
        public Mesa OMesa
        {
            get { return oMesa; }
            set { oMesa = value; }
        }

        private Cliente oCliente;
        public Cliente OCliente
        {
            get { return oCliente; }
            set { oCliente = value; }
        }

        private Usuario oMozo;
        public Usuario OMozo
        {
            get { return oMozo; }
            set { oMozo = value; }
        }

        private decimal totalPedido;
        public decimal TotalPedido
        {
            get { return totalPedido; }
            set { totalPedido = value; }
        }

        public Pedido(int id, int idUsu, int idMesa, int idCli, DateTime fechaEmision, DateTime fechaEntrega, int comensales, bool facturado, Usuario mozo, Cliente client, Mesa mesa, decimal totalP)
        {
            ped_Id = id;
            usu_Id = idUsu;
            mesa_Id = idMesa;
            cli_Id = idCli;
            ped_FechaEmision = fechaEmision;
            ped_fechaEntrega = fechaEntrega;
            ped_Comensales = comensales;
            ped_Facturado = facturado;
            oCliente = client;
            oMesa = mesa;
            oMozo = mozo;
            totalPedido = totalP;
        }

        public Pedido() { }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                string msg_error = null;

                switch (columnName)
                {
                    case "Ped_Comensales":
                        msg_error = validad_Comensales();
                        break;
                }
                return msg_error;
            }
        }

        private string validad_Comensales()
        {
            string sComensales = Convert.ToString(ped_Comensales);
            if (String.IsNullOrEmpty(sComensales))
                return "El valor del campo es obligatorio";
            return null;
        }
    }
}