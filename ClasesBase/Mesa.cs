using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Mesa
    {
        private int mesa_Id;

        public int Mesa_Id
        {
            get { return mesa_Id; }
            set { mesa_Id = value; }
        }
        private int mesa_Posicion;

        public int Mesa_Posicion
        {
            get { return mesa_Posicion; }
            set { mesa_Posicion = value; }
        }
        private int mesa_Estado;

        public int Mesa_Estado
        {
            get { return mesa_Estado; }
            set { mesa_Estado = value; }
        }

        public Mesa(int id, int posicion, int estado) 
        {
            mesa_Id = id;
            mesa_Posicion = posicion;
            mesa_Estado = estado;
        }
    }
}
