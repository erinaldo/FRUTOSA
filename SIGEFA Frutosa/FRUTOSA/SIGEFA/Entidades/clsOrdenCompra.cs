using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsOrdenCompra
    {
        private Int32 moneda;
        private decimal tipocambio;
        private decimal bruto;
        private decimal montodesc;
        private decimal igv;
        private decimal total;
        private Int32 formapago;
        private DateTime fechapago;
        private decimal dflete;

        public Int32 CodOrdenCompraNuevo
        { get; set; }
        public Int32 CodOrdenCompra
        {
            get;
            set;
        }
        public Int32 CodAlmacen
        {
            get;
            set;
        }
        public Int32 CodProveedor
        {
            get;
            set;
        }
        public String RUCProveedor
        {
            get;
            set;
        }
        public String RazonSocialProveedor
        {
            get;
            set;
        }
        public String Comentario
        {
            get;
            set;
        }
         public Int32 Tipo
        {
            get;
            set;
        }
         public Int32 CodTipoDocumento
         {
             get;
             set;
         }
         public String SiglaDocumento
         {
             get;
             set;
         }
         public String DescripcionDocumento
         { get; set; }

         public Int32 CodSerie
         {
             get;
             set;
         }
         public String Serie
         {
             get;
             set;
         }
         public Int32 NumDoc
         {
             get;
             set;
         }

         public DateTime FechaOrden
         {
             get;
             set;
         }
         public Int32 CodUser
         {
             get;
             set;
         }
         public DateTime FechaRegistro
         {
             get;
             set;
         }

        
        public Int32 Estado
        {
            get;
            set;
        }
        public Int32 Anulado
        {
            get;
            set;
        }
        public Int32 Atendido
        {
            get;
            set;
        }
        public Int32 CodSubOrden
        {
            get;
            set;
        }
        public List<clsDetalleOrdenCompra> Detalle
        {
            get;
            set;
        }

        public decimal Tipocambio
        {
            get { return tipocambio; }
            set { tipocambio = value; }
        }

        public int Moneda
        {
            get { return moneda; }
            set { moneda = value; }
        }

        public decimal Bruto
        {
            get { return bruto; }
            set { bruto = value; }
        }

        public decimal Montodesc
        {
            get { return montodesc; }
            set { montodesc = value; }
        }

        public decimal Igv
        {
            get { return igv; }
            set { igv = value; }
        }

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }

        public int Formapago
        {
            get { return formapago; }
            set { formapago = value; }
        }

        public DateTime Fechapago
        {
            get { return fechapago; }
            set { fechapago = value; }
        }

        public decimal Flete
        {
            get { return dflete; }
            set { dflete = value; }
        }
    }
}
