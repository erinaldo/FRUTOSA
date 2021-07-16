using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace SIGEFA.Entidades
{
    public class clsIngresoCamara
    {
        #region propiedades

        //RAGA
        private Int32 iCodIngresoCamara;
        private String sNumDoc;
        private DateTime dtFechaIngreso;
        private DateTime horaInicio;
        private DateTime dtHora;

        public DateTime DtHora
        {
            get { return dtHora; }
            set { dtHora = value; }
        }


        private String sSigla;

        public String SSigla
        {
            get { return sSigla; }
            set { sSigla = value; }
        }


        private Int32 iCodProveedor;
        private Int32 codPesador;
        private Int32 iCodProcedencia;
        private String sComentario;

        private Int32 iCodUser;

        private Int32 iTipoDocumento;

        public Int32 ITipoDocumento
        {
            get { return iTipoDocumento; }
            set { iTipoDocumento = value; }
        }
        private DateTime dtFechaRegistro;
        private Boolean iEstado;
        //

        public Int32 ICodProveedor
        {
            get { return iCodProveedor; }
            set { iCodProveedor = value; }
        }
       
         public String SNumDoc
        {
            get { return sNumDoc; }
            set { sNumDoc = value; }
        }
      
         private Int32 iCodIngresoCamaraNuevo;
        private Int32 iCodEmpresa;
      
        private Int32 iNumeroOrden;
        private Int32 iCodBahia;
      
        
        private String sGuiaTransporte;
        private String sPlaca;        
        private String sMarca;
       
        private Int32 iCodEspecie;
        private Int32 iCajas;
        private Decimal dPesoAprox;
        private Image iFotoCamion;

        private String sNombreConductor;
        private String sDni;
        private String sLicencia;
        private Image iFotoConductor;

        private Int32 iSituacion;
      
        private Int32 sOlor;
        private Int32 sColor;
        private Int32 sTextura;
        private Int32 sApariencia;
        private String sPesador;
        private Decimal dPesoReal;
        private Decimal dPesoRechazado;
        private String sNumTicket;
        private Int32 codProveedorGarita;
        private Int32 codVehiculoTransporte;
        private Int32 codConductor;
        
        private String horaIngreso;
        private String horaSalida;
        private DateTime fechaSalida;
        private String tiempoGarita;
        private String iniciodescarga;
        private String findescarga;
        private String tiempodescarga;
        private String tiempoespera;


        public String Tiempoespera
        {
            get { return tiempoespera; }
            set { tiempoespera = value; }
        }

        public String Tiempodescarga
        {
            get { return tiempodescarga; }
            set { tiempodescarga = value; }
        }

        public String Findescarga
        {
            get { return findescarga; }
            set { findescarga = value; }
        }

        public String Iniciodescarga
        {
            get { return iniciodescarga; }
            set { iniciodescarga = value; }
        }

        public String TiempoGarita
        {
            get { return tiempoGarita; }
            set { tiempoGarita = value; }
        }

        public DateTime FechaSalida
        {
            get { return fechaSalida; }
            set { fechaSalida = value; }
        }

        public String HoraSalida
        {
            get { return horaSalida; }
            set { horaSalida = value; }
        }

        public String HoraIngreso
        {
            get { return horaIngreso; }
            set { horaIngreso = value; }
        }


     
        public Int32 CodPesador
        {
            get { return codPesador; }
            set { codPesador = value; }
        }

        public Int32 CodConductor
        {
            get { return codConductor; }
            set { codConductor = value; }
        }

        public Int32 CodVehiculoTransporte
        {
            get { return codVehiculoTransporte; }
            set { codVehiculoTransporte = value; }
        }

        public Int32 CodProveedorGarita
        {
            get { return codProveedorGarita; }
            set { codProveedorGarita = value; }
        }

        public Int32 CodIngresoCamaraNuevo
        {
            get { return iCodIngresoCamaraNuevo; }
            set { iCodIngresoCamaraNuevo = value; }
        }
        public Int32 CodIngresoCamara
        {
            get { return iCodIngresoCamara; }
            set { iCodIngresoCamara = value; }
        }
        public Int32 CodEmpresa
        {
            get { return iCodEmpresa; }
            set { iCodEmpresa = value; }
        }
        public DateTime FechaIngreso
        {
            get { return dtFechaIngreso; }
            set { dtFechaIngreso = value; }
        }
        public Int32 CodBahia
        {
            get { return iCodBahia; }
            set { iCodBahia = value; }
        }
        public Int32 NumeroOrden
        {
            get { return iNumeroOrden; }
            set { iNumeroOrden = value; }
        }
        public String Comentario
        {
            get { return sComentario; }
            set { sComentario = value; }
        }
               
        
        /*
        public String Proveedor
        {
            get { return sProveedor; }
            set { sProveedor = value; }
        }
        */

        public String GuiaTransporte
        {
            get { return sGuiaTransporte; }
            set { sGuiaTransporte = value; }
        }
        public String Placa
        {
            get { return sPlaca; }
            set { sPlaca = value; }
        }        
        public String Marca
        {
            get { return sMarca; }
            set { sMarca = value; }
        }
        public Int32 CodProcedencia
        {
            get { return iCodProcedencia; }
            set { iCodProcedencia = value; }
        }
        public Int32 CodEspecie
        {
            get { return iCodEspecie; }
            set { iCodEspecie = value; }
        }
        public Int32 Cajas
        {
            get { return iCajas; }
            set { iCajas = value; }
        }
        public Decimal PesoAprox
        {
            get { return dPesoAprox; }
            set { dPesoAprox = value; }
        }
        public Image FotoCamion
        {
            get { return iFotoCamion; }
            set { iFotoCamion = value; }
        }
        
        

        public String NombreConductor
        {
            get { return sNombreConductor; }
            set { sNombreConductor = value; }
        }
        public String Dni
        {
            get { return sDni; }
            set { sDni = value; }
        }
        public String Licencia
        {
            get { return sLicencia; }
            set { sLicencia = value; }
        }
        public Image FotoConductor
        {
            get { return iFotoConductor; }
            set { iFotoConductor = value; }
        }


        public Int32 Situacion
        {
            get { return iSituacion; }
            set { iSituacion = value; }
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

        public Int32 Olor
        {
            get { return sOlor; }
            set { sOlor = value; }
        }
        public Int32 Color
        {
            get { return sColor; }
            set { sColor = value; }
        }
        public Int32 Textura
        {
            get { return sTextura; }
            set { sTextura = value; }
        }
        public Int32 Apariencia
        {
            get { return sApariencia; }
            set { sApariencia = value; }
        }
        public String Pesador
        {
            get { return sPesador; }
            set { sPesador = value; }
        }
        public Decimal PesoReal
        {
            get { return dPesoReal; }
            set { dPesoReal = value; }
        }
        public Decimal PesoRechazado
        {
            get { return dPesoRechazado; }
            set { dPesoRechazado = value; }
        }
        public String NumTicket
        {
            get { return sNumTicket; }
            set { sNumTicket = value; }
        }

        #endregion propiedades
    }
}
