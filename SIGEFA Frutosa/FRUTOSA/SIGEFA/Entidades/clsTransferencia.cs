using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsTransferencia
    {
        private String icodTransferencianuevo;
        private Int32 icodTransferencia;
        private Int32 icodTipoDocumento;
        private String ssiglaDocumento;
        private String sdescripcionDocumento;
        private Int32 icodSerie;
        private String sserie;
        private String snumDoc;
        private Int32 icodAlmacenO;
        private Int32 icodAlmacenD;
        private Int32 icodAlmacen;
        private String icomentario;
        private DateTime dfechaingreso;
        private DateTime dfechaaceptacion;
        private Int32 iaceptado;
        private Int32 iestado;
        private Int32 ianulado;
        private Int32 icodUsuario;
        private List<clsDetalleTransferencia> lDetalle;

        public String CodTransferencianuevo
        {
            get { return icodTransferencianuevo; }
            set { icodTransferencianuevo = value; }
        }

        public int CodTransferencia
        {
            get { return icodTransferencia; }
            set { icodTransferencia = value; }
        }

        public int CodTipoDocumento
        {
            get { return icodTipoDocumento; }
            set { icodTipoDocumento = value; }
        }

        public string SiglaDocumento
        {
            get { return ssiglaDocumento; }
            set { ssiglaDocumento = value; }
        }

        public string DescripcionDocumento
        {
            get { return sdescripcionDocumento; }
            set { sdescripcionDocumento = value; }
        }

        public int CodSerie
        {
            get { return icodSerie; }
            set { icodSerie = value; }
        }

        public string Serie
        {
            get { return sserie; }
            set { sserie = value; }
        }

        public string NumDoc
        {
            get { return snumDoc; }
            set { snumDoc = value; }
        }

        public int CodAlmacenO
        {
            get { return icodAlmacenO; }
            set { icodAlmacenO = value; }
        }

        public int CodAlmacenD
        {
            get { return icodAlmacenD; }
            set { icodAlmacenD = value; }
        }

        public int CodAlmacen
        {
            get { return icodAlmacen; }
            set { icodAlmacen = value; }
        }

        public string Comentario
        {
            get { return icomentario; }
            set { icomentario = value; }
        }

        public DateTime Fechaingreso
        {
            get { return dfechaingreso; }
            set { dfechaingreso = value; }
        }

        public DateTime Fechaaceptacion
        {
            get { return dfechaaceptacion; }
            set { dfechaaceptacion = value; }
        }

        public int Aceptado
        {
            get { return iaceptado; }
            set { iaceptado = value; }
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

        public int CodUsuario
        {
            get { return icodUsuario; }
            set { icodUsuario = value; }
        }

        public List<clsDetalleTransferencia> Detalle
        {
            get { return lDetalle; }
            set { lDetalle = value; }
        }
    }
}
