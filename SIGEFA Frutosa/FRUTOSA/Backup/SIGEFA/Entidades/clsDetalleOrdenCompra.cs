using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsDetalleOrdenCompra
    {
        #region Propiedades

        private Int32 moneda;
        private decimal preciounitario;
        private decimal subtotal;
        private decimal dscto1;
        private decimal dscto2;
        private decimal dscto3;
        private decimal montodscto;
        private decimal igv;
        private decimal importe;
        private decimal precioreal;
        private decimal valorreal;
        private decimal dflete;

        public Int32 CodDetalleOrdenCompra
        {
            get;
            set;
        }
        public Int32 CodProducto
        {
            get;
            set;
        }
        public Int32 CodOrdenCompra
        {
            get;
            set;
        }
       
        public Int32 Unidad
        {
            get;
            set;
        }
        public Decimal CantidadPendiente
        {
            get;
            set;
        }
        
        public Double Cantidad
        {
            get;
            set;
        }
       
        public DateTime FechaRegistro
        {
            get;
            set;
        }
       
        public Int32 CodUser
        {
            get;
            set;
        }

        public Int32 codDetalleRequerimiento
        {
            get;
            set;
        }

        public Int32 Estado
        { get; set; }
        public Int32 Anulado
        { get; set; }

        private Int32 codAlmacen;

        public Int32 CodAlmacen
        {
            get { return codAlmacen; }
            set { codAlmacen = value; }
        }

        public int Moneda
        {
            get { return moneda; }
            set { moneda = value; }
        }

        public decimal Preciounitario
        {
            get { return preciounitario; }
            set { preciounitario = value; }
        }

        public decimal Subtotal
        {
            get { return subtotal; }
            set { subtotal = value; }
        }

        public decimal Dscto1
        {
            get { return dscto1; }
            set { dscto1 = value; }
        }

        public decimal Dscto2
        {
            get { return dscto2; }
            set { dscto2 = value; }
        }

        public decimal Dscto3
        {
            get { return dscto3; }
            set { dscto3 = value; }
        }

        public decimal Montodscto
        {
            get { return montodscto; }
            set { montodscto = value; }
        }

        public decimal Igv
        {
            get { return igv; }
            set { igv = value; }
        }

        public decimal Importe
        {
            get { return importe; }
            set { importe = value; }
        }

        public decimal Precioreal
        {
            get { return precioreal; }
            set { precioreal = value; }
        }

        public decimal Valorreal
        {
            get { return valorreal; }
            set { valorreal = value; }
        }

        public decimal Flete
        {
            get { return dflete; }
            set { dflete = value; }
        }

        #endregion Propiedades


        #region IEquatable<clsDetalleOrdenCompra> Members

  

        public bool CodProEquals(Int32 codigo)
        {
            if (this.CodProducto == codigo)
                return true;
            return false;
        }

        #endregion
    }
}
