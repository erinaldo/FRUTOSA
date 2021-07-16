using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsDetalleTransferencia
    {
        private Int32 icodDetalleTransferencia;
        private Int32 icodProducto;
        private Int32 icodTransferencia;
        private Int32 iunidad;
        private Decimal dcantidad;
        private DateTime dfechaRegistro;
        private Int32 icodusuario;
        private Int32 iestado;
        private Int32 ianulado;
        private decimal dvalorpromedio;
        private decimal dvalorpromediosoles;
        private decimal dpreciopromedio;
        private Int32 idetalleRequerimiento;
        private Int32 icodAlmacen;
        private Int32 baceptado;
        public int CodDetalleTransferencia
        {
            get { return icodDetalleTransferencia; }
            set { icodDetalleTransferencia = value; }
        }

        public int CodProducto
        {
            get { return icodProducto; }
            set { icodProducto = value; }
        }

        public int CodTransferencia
        {
            get { return icodTransferencia; }
            set { icodTransferencia = value; }
        }

        public int Unidad
        {
            get { return iunidad; }
            set { iunidad = value; }
        }

        public decimal Cantidad
        {
            get { return dcantidad; }
            set { dcantidad = value; }
        }

        public DateTime FechaRegistro
        {
            get { return dfechaRegistro; }
            set { dfechaRegistro = value; }
        }

        public int Codusuario
        {
            get { return icodusuario; }
            set { icodusuario = value; }
        }

        public int Estado
        {
            get { return iestado; }
            set { iestado = value; }
        }

        public int Anulado
        {
            get { return ianulado; }
            set { ianulado = value; }
        }

        public decimal Valorpromedio
        {
            get { return dvalorpromedio; }
            set { dvalorpromedio = value; }
        }

        public decimal Valorpromediosoles
        {
            get { return dvalorpromediosoles; }
            set { dvalorpromediosoles = value; }
        }

        public decimal Preciopromedio
        {
            get { return dpreciopromedio; }
            set { dpreciopromedio = value; }
        }

        public int CodRequerimiento
        {
            get { return idetalleRequerimiento; }
            set { idetalleRequerimiento = value; }
        }

        public int CodAlmacen
        {
            get { return icodAlmacen; }
            set { icodAlmacen = value; }
        }

        public Int32 Aceptado
        {
            get { return baceptado; }
            set { baceptado = value; }
        }
    }
}
