using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    class clsCtaCte
    {
        #region propiedades

        private Int32 iCodBanco;
        private String sNombreBanco;

        private Int32 iCodCtaCte;
        private Int32 iCodCtaCteNuevo;
        private String sCtaCte;
        private Int32 iCodMovi;
        private String sMovimiento;

        private String sTipoCuenta;
        private Int32 iMoneda;
        private Boolean bEstado;
        private Int32 iCoduser;
        private DateTime dFechaRegistro;

        private decimal itipocambio;
        private String iDescrip;
        private decimal iIngreso;
        private decimal iEgreso;
        private decimal iSaldo;
        private decimal dmonto;
        private Int32 tipo;
        private String descTipo;
        private DateTime dFechaMovimiento;

        private Int32 iCodAlmacen;

        private decimal dTipoCVenta;

        public decimal TipoCVenta
        {
            get { return dTipoCVenta; }
            set { dTipoCVenta = value; }
        }
        private Int32 iCodTipoPagoServicio;

        private String iNumTransaccion;


        public String NumTransaccion
        {
            get { return iNumTransaccion; }
            set { iNumTransaccion = value; }
        }
        public Int32 CodTipoPagoServicio
        {
            get { return iCodTipoPagoServicio; }
            set { iCodTipoPagoServicio = value; }
        }

        public Int32 Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        public decimal Dmonto
        {
            get { return dmonto; }
            set { dmonto = value; }
        }

        public String descripcion
        {
            get { return iDescrip; }
            set { iDescrip = value; }
        }

        public decimal ingreso
        {
            get { return iIngreso; }
            set { iIngreso = value; }
        }

        public decimal egreso
        {
            get { return iEgreso; }
            set { iEgreso = value; }
        }
        public decimal saldo
        {
            get { return iSaldo; }
            set { iSaldo = value; }
        }


        public decimal tipocambio
        {
            get { return itipocambio; }
            set { itipocambio = value; }
        }


        public Int32 CodBanco
        {
            get { return iCodBanco; }
            set { iCodBanco = value; }
        }
        public Int32 CodMovi
        {
            get { return iCodMovi; }
            set { iCodMovi = value; }
        }
        public Int32 CodCtaCte
        {
            get { return iCodCtaCte; }
            set { iCodCtaCte = value; }
        }
        public Int32 CodCtaCteNuevo
        {
            get { return iCodCtaCteNuevo; }
            set { iCodCtaCteNuevo = value; }
        }
        public String CtaCte
        {
            get { return sCtaCte; }
            set { sCtaCte = value; }
        }
        public String TipoCuenta
        {
            get { return sTipoCuenta; }
            set { sTipoCuenta = value; }
        }
        public Int32 Moneda
        {
            get { return iMoneda; }
            set { iMoneda = value; }
        }
        public Boolean Estado
        {
            get { return bEstado; }
            set { bEstado = value; }
        }
        public Int32 Coduser
        {
            get { return iCoduser; }
            set { iCoduser = value; }
        }
        public DateTime FechaRegistro
        {
            get { return dFechaRegistro; }
            set { dFechaRegistro = value; }
        }

        public int CodAlmacen
        {
            get { return iCodAlmacen; }
            set { iCodAlmacen = value; }
        }

        public string NombreBanco
        {
            get { return sNombreBanco; }
            set { sNombreBanco = value; }
        }

        public string Movimiento
        {
            get { return sMovimiento; }
            set { sMovimiento = value; }
        }

        public DateTime FechaMovimiento
        {
            get { return dFechaMovimiento; }
            set { dFechaMovimiento = value; }
        }

        public string DescTipo
        {
            get { return descTipo; }
            set { descTipo = value; }
        }

        #endregion propiedades
    }
}
