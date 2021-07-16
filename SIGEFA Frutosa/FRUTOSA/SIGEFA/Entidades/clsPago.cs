using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsPago
    {
        #region propiedades

        private Int32 iCodPago;        
        private String iCodNota;
        private Int32 iCodLetra;
        private Int32 iCodTipoPago;
        private Int32 iCodMoneda;
        private Int32 iCodCobrador;
        private Boolean bTipo;
        private Boolean bIngresoEgreso;
        private Decimal dTipoCambio;
        private Decimal dMontoPagado;
        private Decimal dMontoCobrado;
        private Decimal dVuelto;
        private String sNOperacion;
        private String sNCheque;
        private DateTime dtFechaPago;
        private String sObservacion;
        private Boolean iEstado;
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;
        private Int32 iCodAlmacen;
        private Int32 iCodSerie;
        private String sSerie;
        private String sNumDoc;

        //*** Datos Para el Pago ***
        private Int32 iCodMetPago;
        private Int32 iCodBanco;
        private Int32 iCodTarjeta;
        //**************************
        //**** Datos de la Cuenta***
        private Int32 icodCtaCte;
        private String sCtaCte;
        // --- Dato Por Aprobar Pago
        private Int32 iAprobado;
        // --- Dato Referencia de ND
        private String sReferencia;

        public String Referencia
        {
            get { return sReferencia; }
            set { sReferencia = value; }
        }

        public Int32 Aprobado
        {
            get { return iAprobado; }
            set { iAprobado = value; }
        }


        public Int32 CodPago
        {
            get { return iCodPago; }
            set { iCodPago = value; }
        }
        public String CodNota
        {
            get { return iCodNota; }
            set { iCodNota = value; }
        }
        public Int32 CodLetra
        {
            get { return iCodLetra; }
            set { iCodLetra = value; }
        }
        public Int32 CodTipoPago
        {
            get { return iCodTipoPago; }
            set { iCodTipoPago = value; }
        }
        public Int32 CodMoneda
        {
            get { return iCodMoneda; }
            set { iCodMoneda = value; }
        }
        public Int32 CodCobrador
        {
            get { return iCodCobrador; }
            set { iCodCobrador = value; }
        }
        public Boolean Tipo
        {
            get { return bTipo; }
            set { bTipo = value; }
        }
        public Boolean IngresoEgreso
        {
            get { return bIngresoEgreso; }
            set { bIngresoEgreso = value; }
        }
        public Decimal TipoCambio
        {
            get { return dTipoCambio; }
            set { dTipoCambio = value; }
        }
        public Decimal MontoPagado
        {
            get { return dMontoPagado; }
            set { dMontoPagado = value; }
        }
        public Decimal MontoCobrado
        {
            get { return dMontoCobrado; }
            set { dMontoCobrado = value; }
        }
        public Decimal Vuelto
        {
            get { return dVuelto; }
            set { dVuelto = value; }
        }
        public String NOperacion
        {
            get { return sNOperacion; }
            set { sNOperacion = value; }
        }
        public String NCheque
        {
            get { return sNCheque; }
            set { sNCheque = value; }
        }
        public DateTime FechaPago
        {
            get { return dtFechaPago; }
            set { dtFechaPago = value; }
        }
        public String Observacion
        {
            get { return sObservacion; }
            set { sObservacion = value; }
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

        public int CodMetPago
        {
            get { return iCodMetPago; }
            set { iCodMetPago = value; }
        }

        public int CodBanco
        {
            get { return iCodBanco; }
            set { iCodBanco = value; }
        }

        public int CodTarjeta
        {
            get { return iCodTarjeta; }
            set { iCodTarjeta = value; }
        }

        public int codCtaCte
        {
            get { return icodCtaCte; }
            set { icodCtaCte = value; }
        }

        public string CtaCte
        {
            get { return sCtaCte; }
            set { sCtaCte = value; }
        }

        public int CodAlmacen
        {
            get { return iCodAlmacen; }
            set { iCodAlmacen = value; }
        }

        public int CodSerie
        {
            get { return iCodSerie; }
            set { iCodSerie = value; }
        }

        public string NumDoc
        {
            get { return sNumDoc; }
            set { sNumDoc = value; }
        }

        public string Serie
        {
            get { return sSerie; }
            set { sSerie = value; }
        }

        #endregion propiedades
    }
}
