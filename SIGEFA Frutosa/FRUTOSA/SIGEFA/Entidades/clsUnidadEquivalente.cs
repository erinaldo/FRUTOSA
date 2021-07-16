using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsUnidadEquivalente
    {
        #region propiedades

        private Int32 iCodUnidadEquivalente;
        private Int32 iCodProducto;
        private Int32 iCodUnidad;
        private Double dFactor;
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;


        public Int32 CodUnidadEquivalente
        {
            get { return iCodUnidadEquivalente; }
            set { iCodUnidadEquivalente = value; }
        }
        public Int32 CodProducto
        {
            get { return iCodProducto; }
            set { iCodProducto = value; }
        }
        public Int32 CodUnidad
        {
            get { return iCodUnidad; }
            set { iCodUnidad = value; }
        }
        public Double Factor
        {
            get { return dFactor; }
            set { dFactor = value; }
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
