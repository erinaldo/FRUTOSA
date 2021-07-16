using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsFlujoCaja
    {
        #region propiedades

        private Int32 iCodFlujoCaja;
        private Int32 iCodFlujoCajaNuevo;
        private String sConcepto;
        private Decimal dMonto;
        private Int32 iTipo;
        private DateTime dFecha;
        private Boolean iEstado;
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;
        private Int32 iCodPagoServicio;
        private Int32 iCodAlmacen;


        private Int32 iCentroCostos;

        public Int32 ICentroCostos
        {
            get { return iCentroCostos; }
            set { iCentroCostos = value; }
        }

        public Int32 CodPagoServicio
        {
            get { return iCodPagoServicio; }
            set { iCodPagoServicio = value; }
        }

        public Int32 CodFlujoCaja
        {
            get { return iCodFlujoCaja; }
            set { iCodFlujoCaja = value; }
        }
        public Int32 CodFlujoCajaNuevo
        {
            get { return iCodFlujoCajaNuevo; }
            set { iCodFlujoCajaNuevo = value; }
        }
        public String Concepto
        {
            get { return sConcepto; }
            set { sConcepto = value; }
        }
        public Decimal Monto
        {
            get { return dMonto; }
            set { dMonto = value; }
        }
        public int Tipo
        {
            get { return iTipo; }
            set { iTipo = value; }
        }
        public DateTime Fecha
        {
            get { return dFecha; }
            set { dFecha = value; }
        }
        public Boolean Estado
        {
            get { return iEstado; }
            set { iEstado = value; }
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

        public int CodAlmacen
        {
            get { return iCodAlmacen; }
            set { iCodAlmacen = value; }
        }

        #endregion propiedades
    }
}
