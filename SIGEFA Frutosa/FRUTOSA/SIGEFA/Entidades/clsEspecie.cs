using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsEspecie
    {
        #region propiedades

        private Int32 iCodEspecie;
        private Int32 iCodEspecieNuevo;
        private String sNombre;
        private Boolean iEstado;
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;


        public Int32 CodEspecieNuevo
        {
            get { return iCodEspecieNuevo; }
            set { iCodEspecieNuevo = value; }
        }
        public Int32 CodEspecie
        {
            get { return iCodEspecie; }
            set { iCodEspecie = value; }
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
