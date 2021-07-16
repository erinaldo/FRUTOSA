﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    class clsCaracteristica
    {
        #region propiedades

        private Int32 iCodCaracteristica;
        private Int32 iCodCaracteristicaNuevo;
        private String sDescripcion;
        private Boolean iEstado;
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;


        public Int32 CodCaracteristicaNuevo
        {
            get { return iCodCaracteristicaNuevo; }
            set { iCodCaracteristicaNuevo = value; }
        }
        public Int32 CodCaracteristica
        {
            get { return iCodCaracteristica; }
            set { iCodCaracteristica = value; }
        }
        public String Descripcion
        {
            get { return sDescripcion; }
            set { sDescripcion = value; }
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

        #endregion propiedades
    }
}
