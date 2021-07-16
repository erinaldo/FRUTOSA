using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGEFA.Entidades
{
    public class clsLoteProduccion
    {
        private Int32 codLoteProduccion;
        private Int32 codEspecie;
        private String numeroorden;
        private DateTime fecha;
        private DateTime fechafin;
        private DateTime horaInicio;
        private DateTime horaFinal;
        private String observacion;
        private Int32 codAutorizado;
        private Boolean iEstado;        
        private Int32 iCodUser;
        private DateTime dtFechaRegistro;
        private Boolean iEstadoLote;

        private Int32 codMateriaPrima;

        public Int32 CodMateriaPrima
        {
            get { return codMateriaPrima; }
            set { codMateriaPrima = value; }
        }
        private Int32 codProductoTerminado;

        public Int32 CodProductoTerminado
        {
            get { return codProductoTerminado; }
            set { codProductoTerminado = value; }
        }





        private String matprima;

        public String Matprima
        {
            get { return matprima; }
            set { matprima = value; }
        }
        private String prodterminado;

        public String Prodterminado
        {
            get { return prodterminado; }
            set { prodterminado = value; }
        }

        private String especie;

        public String Especie
        {
            get { return especie; }
            set { especie = value; }
        }




        //raga
        private Decimal cantmatprima;

        public Decimal Cantmatprima
        {
            get { return cantmatprima; }
            set { cantmatprima = value; }
        }
        private Decimal cantprodterm;

        public Decimal Cantprodterm
        {
            get { return cantprodterm; }
            set { cantprodterm = value; }
        }

        public Int32 CodAutorizado
        {
            get { return codAutorizado; }
            set { codAutorizado = value; }
        }
        
        public Int32 CodEspecie
        {
            get { return codEspecie; }
            set { codEspecie = value; }
        }

        public Int32 CodLoteProduccion
        {
            get { return codLoteProduccion; }
            set { codLoteProduccion = value; }
        }
        

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        

        public DateTime HoraInicio
        {
            get { return horaInicio; }
            set { horaInicio = value; }
        }
        

        public DateTime HoraFinal
        {
            get { return horaFinal; }
            set { horaFinal = value; }
        }
        

        public String Observacion
        {
            get { return observacion; }
            set { observacion = value; }
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

        public string Numeroorden
        {
            get { return numeroorden; }
            set { numeroorden = value; }
        }

        public DateTime Fechafin
        {
            get { return fechafin; }
            set { fechafin = value; }
        }

        public bool IEstadoLote
        {
            get { return iEstadoLote; }
            set { iEstadoLote = value; }
        }
    }
}
