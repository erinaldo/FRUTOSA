using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsCentroCosto
    {
        private Int32 codcentro;

        public Int32 Codcentro
        {
            get { return codcentro; }
            set { codcentro = value; }
        }
        private String nombre;
        private Int32 codUser;
        private Int32 estado;
        private DateTime fecharegistro;
        //RAGA
        private Int32 nivel;
        private Int32 padre;

        public Int32 Padre
        {
            get { return padre; }
            set { padre = value; }
        }

        public Int32 Nivel
        {
            get { return nivel; }
            set { nivel = value; }
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
        



    }
}
