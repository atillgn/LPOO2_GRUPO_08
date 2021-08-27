using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class HistorialLogin
    {
        private int log_Id;

        public int Log_Id
        {
            get { return log_Id; }
            set { log_Id = value; }
        }
        private int usu_Id;

        public int Usu_Id
        {
            get { return usu_Id; }
            set { usu_Id = value; }
        }
        private DateTime log_FechaHora;

        public DateTime Log_FechaHora
        {
            get { return log_FechaHora; }
            set { log_FechaHora = value; }
        }
        private string log_Descripcion;

        public string Log_Descripcion
        {
            get { return log_Descripcion; }
            set { log_Descripcion = value; }
        }

        public HistorialLogin(int id, int usuId, DateTime fechaHora, string descripcion) 
        {
            log_Id = id;
            usu_Id = usuId;
            log_FechaHora = fechaHora;
            log_Descripcion = descripcion;
        }
    }
}
