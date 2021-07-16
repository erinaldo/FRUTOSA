using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsAperturaCierre
    {
        #region propiedades

        private Int32 iCodApertura;
        private Int32 iCodAperturaNuevo;
        private Decimal dMontoApertura;
        private Decimal dMontoCierre;
        private Int32 iEstadoCierreCaja;
        private Int32 iCodUser;
        private Boolean iEstado;
        private DateTime dtFechaRegistro;
        private DateTime dtFechaCierre;
        private Int32 icodAlmacen;
        private Int32 iTipoCaja;
        private String sDesCaja;

        public Int32 CodApertura
        {
            get { return iCodApertura; }
            set { iCodApertura = value; }
        }
        public Int32 CodAperturaNuevo
        {
            get { return iCodAperturaNuevo; }
            set { iCodAperturaNuevo = value; }
        }
        public Decimal MontoApertura
        {
            get { return dMontoApertura; }
            set { dMontoApertura = value; }
        }
        public Decimal MontoCierre
        {
            get { return dMontoCierre; }
            set { dMontoCierre = value; }
        }
        public Int32 EstadoCierreCaja
        {
            get { return iEstadoCierreCaja; }
            set { iEstadoCierreCaja = value; }
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

        public int CodAlmacen
        {
            get { return icodAlmacen; }
            set { icodAlmacen = value; }
        }
        
        public int TipoCaja
        {
            get { return iTipoCaja; }
            set { iTipoCaja = value; }
        }

        public string DesCaja
        {
            get { return sDesCaja; }
            set { sDesCaja = value; }
        }

        public DateTime FechaCierre
        {
            get { return dtFechaCierre; }
            set { dtFechaCierre = value; }
        }

        #endregion propiedades
    }
}
