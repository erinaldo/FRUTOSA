using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsStatusCajaChica
    {
        #region propiedades

        private Decimal dAperturaCaja;
        private Decimal dSumaAperturasCaja;
        private Decimal dSumaCierresCaja;
        private Decimal dIngresos;
        private Decimal dEgresos;
        private Decimal dTotalVentas;
        private Decimal dPorCobrar;
        private Decimal dTotalPagos;
        private Decimal dPorPagar;
        private Int32 iCodAlmacen;
        private Int32 iCantidad;


        public Decimal AperturaCaja
        {
            get { return dAperturaCaja; }
            set { dAperturaCaja = value; }
        }
        public Decimal SumaAperturasCaja
        {
            get { return dSumaAperturasCaja; }
            set { dSumaAperturasCaja = value; }
        }
        public Decimal SumaCierresCaja
        {
            get { return dSumaCierresCaja; }
            set { dSumaCierresCaja = value; }
        }
        public Decimal Ingresos
        {
            get { return dIngresos; }
            set { dIngresos = value; }
        }
        public Decimal Egresos
        {
            get { return dEgresos; }
            set { dEgresos = value; }
        }
        public Decimal TotalVentas
        {
            get { return dTotalVentas; }
            set { dTotalVentas = value; }
        }
        public Decimal PorCobrar
        {
            get { return dPorCobrar; }
            set { dPorCobrar = value; }
        }
        public Decimal TotalPagos
        {
            get { return dTotalPagos; }
            set { dTotalPagos = value; }
        }
        public Decimal PorPagar
        {
            get { return dPorPagar; }
            set { dPorPagar = value; }
        }

        public Int32 Cantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }

        public int CodAlmacen
        {
            get { return iCodAlmacen; }
            set { iCodAlmacen = value; }
        }

        #endregion propiedades
    }
}
