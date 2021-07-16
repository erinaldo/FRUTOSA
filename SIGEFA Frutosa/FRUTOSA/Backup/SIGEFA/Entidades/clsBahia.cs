using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsBahia
    {
        #region propiedades

        private Int32 iCodBahia;
        private Int32 iCodBahiaNuevo;
        private String sNombre;
        private Boolean iEstado;
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;


        public Int32 CodBahiaNuevo
        {
            get { return iCodBahiaNuevo; }
            set { iCodBahiaNuevo = value; }
        }
        public Int32 CodBahia
        {
            get { return iCodBahia; }
            set { iCodBahia = value; }
        }
        public String Nombre
        {
            get { return sNombre; }
            set { sNombre = value; }
        }
        public Int32 CodUser
        {
            get { return iCodUser; }
            set { iCodUser = value; }
        }
        public Boolean Estado
        {
            get { return iEstado; }
            set { iEstado = value; }
        }
        public DateTime FechaRegistro
        {
            get { return dtFechaRegistro; }
            set { dtFechaRegistro = value; }
        }

        #endregion propiedades

    }
}
