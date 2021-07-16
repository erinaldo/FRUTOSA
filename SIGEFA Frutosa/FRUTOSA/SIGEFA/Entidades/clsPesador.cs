using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SIGEFA.Entidades
{
     public class clsPesador
    {
        private Int32 codPesador;
        private String dni;
        private String nombre;
        private String telefono;
        private String direccion;
        private Int32 tipo;
        private Int32 codUser;
        private Image fotopesador;


        public Image Fotopesador
        {
            get { return fotopesador; }
            set { fotopesador = value; }
        }

        public Int32 CodUser
        {
            get { return codUser; }
            set { codUser = value; }
        }

        public Int32 Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public String Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public String Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public String Dni
        {
            get { return dni; }
            set { dni = value; }
        }

        public Int32 CodPesador
        {
            get { return codPesador; }
            set { codPesador = value; }
        }
    }
}
