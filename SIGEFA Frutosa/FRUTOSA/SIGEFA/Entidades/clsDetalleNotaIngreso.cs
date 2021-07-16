﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsDetalleNotaIngreso
    {
        #region propiedades

        private Int32 iCodDetalleIngreso;
        private Int32 iCodProducto;
        private Int32 iCodNotaIngreso;
        private Int32 iCodAlmacen;
        private Int32 iUnidadIngresada;
        private Int32 iMoneda;
        private String sSerieLote;
        private Double dCantidad;
        private Int32 iCodUnidad;
        private Double dPrecioUnitario;
        private Double dSubtotal;
        private Double dDescuento1;
        private Double dDescuento2;
        private Double dDescuento3;
        private Double dMontoDescuento;
        private Double dIgv;
        private Double dFlete;
        private Double dImporte;
        private Double dPrecioReal;
        private Double dValoReal;
        private Boolean bEstado;
        private DateTime dFechaIngreso;
        private DateTime dFechaRegistro;
        private Int32 iCodUser;
        private Int32 coddetalleOrden;
        private Double iValorrealSoles;
        private Int32 iCodDetalleRequerimiento;
        private Boolean bBonificacion;


        //RAGA
        private Double dCantidadDev;
        private Double dNroBrix;
        private Double dPesoneto;

        public Double DNroBrix
        {
            get { return dNroBrix; }
            set { dNroBrix = value; }
        }
        

        public Double DPesoneto
        {
            get { return dPesoneto; }
            set { dPesoneto = value; }
        }

        private Int32 iBatch;

        public Int32 IBatch
        {
            get { return iBatch; }
            set { iBatch = value; }
        }

        public Double DCantidadDev
        {
            get { return dCantidadDev; }
            set { dCantidadDev = value; }
        }
        private Double dCantidadTot;

        public Double DCantidadTot
        {
            get { return dCantidadTot; }
            set { dCantidadTot = value; }
        }



        public Int32 CoddetalleOrden
        {
            get { return coddetalleOrden; }
            set { coddetalleOrden = value; }
        }

        public Int32 CodDetalleIngreso
        {
            get { return iCodDetalleIngreso; }
            set { iCodDetalleIngreso = value; }
        }  
        public Int32 CodProducto
        {
            get { return iCodProducto; }
            set { iCodProducto = value; }
        }        
        public Int32 CodNotaIngreso
        {
            get { return iCodNotaIngreso; }
            set { iCodNotaIngreso = value; }
        }
        public Int32 CodAlmacen
        {
            get { return iCodAlmacen; }
            set { iCodAlmacen = value; }
        }
        public Int32 Moneda
        {
            get { return iMoneda; }
            set { iMoneda = value; }
        }
        public Int32 UnidadIngresada
        {
            get { return iUnidadIngresada; }
            set { iUnidadIngresada = value; }
        }
        public String SerieLote
        {
            get { return sSerieLote; }
            set { sSerieLote = value; }
        }
        public Double Cantidad
        {
            get { return dCantidad; }
            set { dCantidad = value; }
        }
        public Int32 CodUnidad
        {
            get { return iCodUnidad; }
            set { iCodUnidad = value; }
        }
        public Double PrecioUnitario
        {
            get { return dPrecioUnitario; }
            set { dPrecioUnitario = value; }
        }
        public Double Subtotal
        {
            get { return dSubtotal; }
            set { dSubtotal = value; }
        }
        public Double Descuento1
        {
            get { return dDescuento1; }
            set { dDescuento1 = value; }
        }
        public Double Descuento2
        {
            get { return dDescuento2; }
            set { dDescuento2 = value; }
        }
        public Double Descuento3
        {
            get { return dDescuento3; }
            set { dDescuento3 = value; }
        }
        public Double MontoDescuento
        {
            get { return dMontoDescuento; }
            set { dMontoDescuento = value; }
        }
        public Double Igv
        {
            get { return dIgv; }
            set { dIgv = value; }
        }
        public Double Flete
        {
            get { return dFlete; }
            set { dFlete = value; }
        }
        public Double Importe
        {
            get { return dImporte; }
            set { dImporte = value; }
        }
        public Double PrecioReal
        {
            get { return dPrecioReal; }
            set { dPrecioReal = value; }
        }
        public Double ValoReal
        {
            get { return dValoReal; }
            set { dValoReal = value; }
        }
        public Boolean Estado
        {
            get { return bEstado; }
            set { bEstado = value; }
        }
        public DateTime FechaIngreso
        {
            get { return dFechaIngreso; }
            set { dFechaIngreso = value; }
        }
        public DateTime FechaRegistro
        {
            get { return dFechaRegistro; }
            set { dFechaRegistro = value; }
        }
        public Int32 CodUser
        {
            get { return iCodUser; }
            set { iCodUser = value; }
        }
        public Int32 CodProveedor
        { get; set; }

        public double ValorrealSoles
        {
            get { return iValorrealSoles; }
            set { iValorrealSoles = value; }
        }

        public int CodDetalleRequerimiento
        {
            get { return iCodDetalleRequerimiento; }
            set { iCodDetalleRequerimiento = value; }
        }

        public Boolean Bonificacion
        {
            get { return bBonificacion; }
            set { bBonificacion = value; }
        }

        #endregion propiedades

        #region IEquatable<clsDetalleNotaIngreso> Members

        public bool Equals(clsDetalleNotaIngreso other)
        {
            if (this.CodDetalleIngreso == other.CodDetalleIngreso && this.CodAlmacen == other.CodAlmacen)
                return true;
            return false;
        }

        public bool CodProEquals(Int32 codigo)
        {
            if (this.CodProducto == codigo)
                return true;
            return false;
        }

        #endregion
    }
}
