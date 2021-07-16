using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsAreas
    {
        private Int32 codarea;
        private String nombre;
        private Int32 codUser;
        private Int32 estado;
        private DateTime fecharegistro;

        //RAGA
        private Int32 param;

        public Int32 Param
        {
            get { return param; }
            set { param = value; }
        }

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

        public Int32 CodUser
        {
            get { return codUser; }
            set { codUser = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public Int32 Codarea
        {
            get { return codarea; }
            set { codarea = value; }
        }
    }
}
