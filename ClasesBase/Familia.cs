using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClasesBase
{
    public class Familia
    {
        private int fam_Id;
        public int Fam_Id
        {
            get { return fam_Id; }
            set { fam_Id = value; }
        }

        private string fam_Descripcion;
        public string Fam_Descripcion
        {
            get { return fam_Descripcion; }
            set { fam_Descripcion = value; }
        }

        public Familia(int id, string descripcion) 
        {
            fam_Id = id;
            fam_Descripcion = descripcion;
        }
    }
}
