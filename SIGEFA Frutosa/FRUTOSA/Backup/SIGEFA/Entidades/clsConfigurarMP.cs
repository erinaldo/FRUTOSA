using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    class clsConfigurarMP
    {
        #region propiedades

        private Int32 iCodConfigurarMP;
        private Int32 iCodConfigurarMPNuevo;
        private Int32 iCodPadre;
        private Int32 iCodHijo;
        private Decimal dPorcentaje;
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;


        public Int32 CodConfigurarMP
        {
            get { return iCodConfigurarMP; }
            set { iCodConfigurarMP = value; }
        }
        public Int32 CodConfigurarMPNuevo
        {
            get { return iCodConfigurarMPNuevo; }
            set { iCodConfigurarMPNuevo = value; }
        }
        public Int32 CodPadre
        {
            get { return iCodPadre; }
            set { iCodPadre = value; }
        }
        public Int32 CodHijo
        {
            get { return iCodHijo; }
            set { iCodHijo = value; }
        }
        public Decimal Porcentaje
        {
            get { return dPorcentaje; }
            set { dPorcentaje = value; }
        }
        public Int32 CodUser
        {
            get { return iCodUser; }
            set { iCodUser = value; }
        }
        public DateTime FechaRegistro
        {
            get { return dtFechaRegistro; }
            set { dtFechaRegistro = value; }
        }

        #endregion propiedades
    }
}
