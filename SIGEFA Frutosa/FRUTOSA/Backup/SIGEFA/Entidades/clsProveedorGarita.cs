using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsProveedorGarita
    {
        private Int32 codProveedorGarita;
        private String ruc;
        private String razonsocial;


        public String Razonsocial
        {
            get { return razonsocial; }
            set { razonsocial = value; }
        }
        public String Ruc
        {
            get { return ruc; }
            set { ruc = value; }
        }

        public Int32 CodProveedorGarita
        {
            get { return codProveedorGarita; }
            set { codProveedorGarita = value; }
        }

    }
}
