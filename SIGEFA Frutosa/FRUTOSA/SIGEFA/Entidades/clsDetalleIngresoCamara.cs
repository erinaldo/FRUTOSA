using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsDetalleIngresoCamara
    {

        private Int32 iCodDetalleIngresoCamara;

        public Int32 ICodDetalleIngresoCamara
        {
            get { return iCodDetalleIngresoCamara; }
            set { iCodDetalleIngresoCamara = value; }
        }
        private Int32 iCodProductor;

        public Int32 ICodProductor
        {
            get { return iCodProductor; }
            set { iCodProductor = value; }
        }
        private Int32 iCodIngresoCamara;

        public Int32 ICodIngresoCamara
        {
            get { return iCodIngresoCamara; }
            set { iCodIngresoCamara = value; }
        }
        private Int32 iCodUnidIngresada;

        public Int32 ICodUnidIngresada
        {
            get { return iCodUnidIngresada; }
            set { iCodUnidIngresada = value; }
        }
        private String sSerieLote;

        public String SSerieLote
        {
            get { return sSerieLote; }
            set { sSerieLote = value; }
        }
        private Double iCantidad;

        public Double ICantidad
        {
            get { return iCantidad; }
            set { iCantidad = value; }
        }
        private DateTime dtFechaIngreso;

        public DateTime DtFechaIngreso
        {
            get { return dtFechaIngreso; }
            set { dtFechaIngreso = value; }
        }
        private Int32 iCodUser;

        public Int32 ICodUser
        {
            get { return iCodUser; }
            set { iCodUser = value; }
        }
        private DateTime dtFechaRegistro;

        public DateTime DtFechaRegistro
        {
            get { return dtFechaRegistro; }
            set { dtFechaRegistro = value; }
        }
        private Double iCantidadDev;

        public Double ICantidadDev
        {
            get { return iCantidadDev; }
            set { iCantidadDev = value; }
        }
        private Double iCantidadTotal;

        public Double ICantidadTotal
        {
            get { return iCantidadTotal; }
            set { iCantidadTotal = value; }
        }
             





    }
}
