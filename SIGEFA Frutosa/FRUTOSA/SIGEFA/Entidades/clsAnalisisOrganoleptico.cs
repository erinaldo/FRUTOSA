using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsAnalisisOrganoleptico
    {
        private Int32 codanalisis;
        private String nombre;
        private Int32 estado;
        private DateTime fecharegistro;



        public DateTime Fecharegistro
        {
            get { return fecharegistro; }
            set { fecharegistro = value; }
        }

        public Int32 Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }


       

        public Int32 Codanalisis
        {
            get { return codanalisis; }
            set { codanalisis = value; }
        }
    }
}
