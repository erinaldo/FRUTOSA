using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsCajaChica
    {
        #region propiedades


        private Int32 iCodCajaChica;
        private Int32 iCodCajaChicaNuevo;
        private Int32 iCodRendicion;
        private Int32 iCodDetalleRendicion;
        private String sConcepto;
        private Decimal dMonto;
        private Int32 itipo;
        private DateTime dtFecha;
        private String sNumGuia;
        private String sNumRecLiquidacion;
        private String sNumDocumento;
        private Decimal dToneladas;
        private Int32 iCargaDescarga;
        private Boolean iEstado;
        private DateTime dtFechaRegistro; 
        private Int32 iCodTipoPagoCaja;
        private Int32 iApertura;
        private Int32 iApruebaDesaprueba;
        private Decimal dSaldoCaja;
        private Int32 iCodUser;
        private Int32 iCodSucursal;
        private Int32 iCodVehiculo;

        private Int32 iCaso;
        private DateTime dtFechaRendicion;
        //**********************************
        private Decimal dMontoApertura;
        private Decimal dMontoIngresado;
        private Decimal dMontoEntregado;
        private Decimal dMontoDisponible;
        //**********************************
        private String sNumCheque;
        private Decimal dMontoLiquidado;
        private String sResponsable;
        private DateTime dFechaLiquidacion;
        //**********************************
        private String sGlosario;
        


        //raga
        private Int32 iCentroCosto;

        public Int32 ICentroCosto
        {
            get { return iCentroCosto; }
            set { iCentroCosto = value; }
        }


        
        public Int32 CodCajaChica
        {
            get { return iCodCajaChica; }
            set { iCodCajaChica = value; }
        }
        public Int32 CodCajaChicaNuevo
        {
            get { return iCodCajaChicaNuevo; }
            set { iCodCajaChicaNuevo = value; }
        }
        public Int32 CodRendicion
        {
            get { return iCodRendicion; }
            set { iCodRendicion = value; }
        }
        public Int32 CodDetalleRendicion
        {
            get { return iCodDetalleRendicion; }
            set { iCodDetalleRendicion = value; }
        }       
        public Int32 CodUser
        {
            get { return iCodUser; }
            set { iCodUser = value; }
        }
        public Int32 CodSucursal
        {
            get { return iCodSucursal; }
            set { iCodSucursal = value; }
        }

        //***************************************

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
        public Int32 tipo
        {
            get { return itipo; }
            set { itipo = value; }
        }
        public DateTime Fecha
        {
            get { return dtFecha; }
            set { dtFecha = value; }
        }
        public String NumGuia
        {
            get { return sNumGuia; }
            set { sNumGuia = value; }
        }
        public String NumRecLiquidacion
        {
            get { return sNumRecLiquidacion; }
            set { sNumRecLiquidacion = value; }
        }
        public String NumDocumento
        {
            get { return sNumDocumento; }
            set { sNumDocumento = value; }
        }
        public Decimal Toneladas
        {
            get { return dToneladas; }
            set { dToneladas = value; }
        }
        public Int32 CargaDescarga
        {
            get { return iCargaDescarga; }
            set { iCargaDescarga = value; }
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
        public Int32 CodTipoPagoCaja
        {
            get { return iCodTipoPagoCaja; }
            set { iCodTipoPagoCaja = value; }
        }
        public Int32 Apertura
        {
            get { return iApertura; }
            set { iApertura  = value; }
        }
        public Int32 ApruebaDesaprueba
        {
            get { return iApruebaDesaprueba; }
            set { iApruebaDesaprueba = value; }
        }
        public Decimal SaldoCaja
        {
            get { return dSaldoCaja; }
            set { dSaldoCaja = value; }
        }
        public Int32 Caso
        {
            get { return iCaso; }
            set { iCaso = value; }
        }
        public DateTime FechaRendicion
        {
            get { return dtFechaRendicion; }
            set { dtFechaRendicion = value; }
        }
        //*************************************
        public Decimal MontoApertura
        {
            get { return dMontoApertura; }
            set { dMontoApertura = value; }
        }
        public Decimal MontoIngresado
        {
            get { return dMontoIngresado; }
            set { dMontoIngresado = value; }
        }
        public Decimal MontoEntregado
        {
            get { return dMontoEntregado; }
            set { dMontoEntregado = value; }
        }
        public Decimal MontoDisponible
        {
            get { return dMontoDisponible; }
            set { dMontoDisponible = value; }
        }
        public String Glosario
        {
            get { return sGlosario; }
            set { sGlosario = value; }
        }
        public String NumCheque
        {
            get { return sNumCheque; }
            set { sNumCheque = value; }
        }
        public Decimal MontoLiquidado
        {
            get { return dMontoLiquidado; }
            set { dMontoLiquidado = value; }
        }
        public String Responsable
        {
            get { return sResponsable; }
            set { sResponsable = value; }
        }
        public DateTime FechaLiquidacion
        {
            get { return dFechaLiquidacion; }
            set { dFechaLiquidacion = value; }
        }

        public Int32 CodVehiculo
        {
            get { return iCodVehiculo; }
            set { iCodVehiculo = value; }
        }


        #endregion propiedades
    }
}
