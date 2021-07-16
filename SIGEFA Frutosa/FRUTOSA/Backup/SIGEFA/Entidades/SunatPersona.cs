using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using Tesseract.Internal;
using Tesseract.Interop;
using Tesseract;
using System.Drawing.Drawing2D;

namespace SIGEFA.Entidades
{
    class SunatPersona
    {   
        #region Enumerador
        public enum Resul
        {
            Ok = 0,
            NoResul = 1,
            ErrorCapcha = 2,
            Error = 3,
        }
        #endregion
        #region Propiedades
        private Resul state;
        private bool _ok;
        private string _error;

        private string _Ruc;

        public string Ruc
        {
            get { return _Ruc; }
            set { _Ruc = value; }
        }
        
        private string _RazonSocial;
        private string _TipoContribuyente;
        private string _TipoDocumento;
        private string _NumeroDocumento;
        private string _NombreComercial;
        private string _FechaInscpricion;
        private string _FechaInicioActividades;
        private string _EstadoContribuyente;
        private string _CondicionContribuyente;
        private string _DomicilioFiscal;
        private string _SistemaEmisionComprobante;
        private string _ActividadComercioExterior;
        private string _SistemaContabilidad;
        private string _ProfesionuOficio;
        private string _ActividadesEconomicas;
        private string _ComprobantesPago;
        private string _SistemaEmisionElectronica;
        private string _EmisorElectronicoDesde;
        private string _ComprobantesElectronicos;
        private string _AfiliadoPLE;
        private string _Padrones;
        private string _AgenteRetencion;
        private string _AgentePercepcion;
        private bool existeconexion;


        public bool Existeconexion
        {
            get { return existeconexion; }
            set { existeconexion = value; }
        }
        private CookieContainer myCookie;

        public Image GetCapcha { get { return ReadCapcha(); } }
        public string RazonSocial { get { return _RazonSocial; } }
        public string TipoContribuyente { get { return _TipoContribuyente; } }
        public string TipoDocumento { get { return _TipoDocumento; } }
        public string NumeroDocumento { get { return _NumeroDocumento; } }
        public string NombreComercial { get { return _NombreComercial; } }
        public string FechaIscripcion { get { return _FechaInscpricion; } }
        public string FechaInicioActividades { get { return _FechaInicioActividades; } }
        public string EstadoContribuyente { get { return _EstadoContribuyente; } }
        public string CondicionContribuyente { get { return _CondicionContribuyente; } }
        public string DomicilioFiscal { get { return _DomicilioFiscal; } }
        public string SistemaEmisionComprobante { get { return _SistemaEmisionComprobante; } }
        public string ActividadComercioExterior { get { return _ActividadComercioExterior; } }
        public string SistemaContabilidad { get { return _SistemaContabilidad; } }
        public string ProfesionuOficio { get { return _ProfesionuOficio; } }
        public string ActividadesEconomicas { get { return _ActividadesEconomicas; } }
        public string ComprobantesPago { get { return _ComprobantesPago; } }
        public string SistemaEmisionElectronica { get { return _SistemaEmisionElectronica; } }
        public string EmisorElectronicoDesde { get { return _EmisorElectronicoDesde; } }
        public string ComprobantesElectronicos { get { return _ComprobantesElectronicos; } }
        public string AfiliadoPLE { get { return _AfiliadoPLE; } }
        public string Padrones { get { return _Padrones; } }
        public string AgenteRetencion { get { return _AgenteRetencion; } }
        public string AgentePercepcion { get { return _AgentePercepcion; } }

        public Resul GetResul { get { return state; } }

        #endregion

        string xpernatural = "";
        string xruc = "";
        TesseractEngine engine;
        #region Variables Metodo GetInfo
        string xRazonSocial = "";
        string xTipoContribuyente = "";
        string xTipoDocumento = "";
        string xNumeroDocumento = "";
        string xNombreComercial = "";
        string xFechaInscpricion = "";
        string xFechaInicioActividades = "";
        string xEstadoContribuyente = "";
        string xCondicionContribuyente = "";
        string xDomicilioFiscal = "";
        string xSistemaEmisionComprobante = "";
        string xActividadComercioExterior = "";
        string xSistemaContabilidad = "";
        string xProfesionuOficio = "";
        string xActividadesEconomicas = "";
        string xComprobantesPago = "";
        string xSistemaEmisionElectronica = "";
        string xEmisorElectronicoDesde = "";
        string xComprobantesElectronicos = "";
        string xAfiliadoPLE = "";
        string xPadrones = "";
        string xAgenteretencion = "";
        string xAgentepercepcion = "";
        #endregion

        public SunatPersona()
        {
            try
            {
                myCookie = null;
                myCookie = new CookieContainer();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
                Existeconexion = true;
                ReadCapcha();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No Existe Conexion a Pagina Web de SUNAT.", "Consulta Externa en Linea", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Existeconexion = false;
            }
        }
        private Boolean ValidarCertificado(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        //Aqui obtenemos el captcha
        private Image ReadCapcha()
        {
            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidarCertificado);
                //Esta es la direccion que les pase en el grupo de facebook para obtener el captcha
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image&magic=2");
                myWebRequest.CookieContainer = myCookie;
                myWebRequest.Proxy = null;

                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myImgStream = myWebResponse.GetResponseStream();


                //Modificación 1 ... Esta fue la primera modificación ... cree un mapa de bits que utilizaré como
                //parámetro para en fin ... mejor se los muestro xd
                Bitmap bm = new Bitmap(Image.FromStream(myImgStream));
                //quitamos el color a nuestro mapa de bits 
                qutarColor(bm);
                //Procesamos la imagen (separación de carácteres, alineación etc)
                //Y se devuelve la imagen lista para ser procesada por el OCR
                return (Image)PreProcessImage(bm);


                /*System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(ValidarCertificado);
                //Esta es la direccion que les pase en el grupo de facebook para obtener el captcha
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create("http://www.sunat.gob.pe/cl-ti-itmrconsruc/captcha?accion=image&magic=2");
                myWebRequest.CookieContainer = myCookie;
                myWebRequest.Proxy = null;

                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myImgStream = myWebResponse.GetResponseStream();

                //return Image.FromStream(myImgStream);

                //Modificación 1 ... Esta fue la primera modificación ... cree un mapa de bits que utilizaré como
                //parámetro para en fin ... mejor se los muestro xd
                Bitmap bm = new Bitmap(Image.FromStream(myImgStream));
                //quitamos el color a nuestro mapa de bits 
                qutarColor(bm);
                //Procesamos la imagen (separación de carácteres, alineación etc)
                //Y se devuelve la imagen lista para ser procesada por el OCR
                return (Image)PreProcessImage(bm);*/

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetInfo(string numRuc, string ImgCapcha)
        {
            try
            {   //A este link le pasamos los datos , RUC y valor del captcha
                string myUrl = String.Format("http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorRuc&nroRuc={0}&codigo={1}", numRuc, ImgCapcha);
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
                myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                myWebRequest.CookieContainer = myCookie;
                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                myWebRequest.Proxy = null;
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                myWebRequest.Timeout = 10;
                Stream myStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                //Leemos los datos
                string xDat = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());

                if (xDat.Length <= 635)
                {
                    return;
                }

                xDat = xDat.Replace("     ", " ");
                xDat = xDat.Replace("    ", " ");
                xDat = xDat.Replace("   ", " ");
                xDat = xDat.Replace("  ", " ");
                xDat = xDat.Replace("( ", "(");
                xDat = xDat.Replace(" )", ")");
                xDat = xDat.Replace("\r", "");
                xDat = xDat.Replace("\n", "");
                xDat = xDat.Replace("\t", "");
                xDat = xDat.Replace("\r\n ", "");
                xDat = xDat.Replace("      ", "");
                xDat = xDat.Replace("   ", "");
                xDat = xDat.Replace("> <", "><");
                xDat = xDat.Replace(">  <", "><");
                xDat = xDat.Replace("</option><option value=\"00\" >", " /");
                xDat = xDat.Replace("<!-- JRR - 20/09/2010 - Se a�ade cambio de Igor -->", "");
                xDat = xDat.Replace("<!-- -->", "");
                string[] tabla;

                //Lo convertimos a tabla o mejor dicho a un arreglo de string como se ve declarado arriba
                tabla = Regex.Split(xDat, "<td class");

                List<string> _resul = new List<string>();
                for (int i = 0; i < tabla.Length; i++)
                {
                    if (!string.IsNullOrEmpty(tabla[i].Trim()))
                        _resul.Add(tabla[i].Trim());
                }
                if (_resul.Count == 4) //no es valido o algo salio mal
                {
                    _ok = false;
                    _error = " El número de RUC " + numRuc + " consultado no es válido. Debe verificar el número y volver a ingresar. ";
                }
                else
                {
                    if (_resul[0].Contains("Consulta RUC"))
                    {
                        _ok = true;
                    }
                    else
                    {
                        if (_resul[0].Contains("El codigo ingresado es incorrecto"))
                        {
                            _ok = false;
                            _error = "La aplicación ha retornado el siguiente mensaje : El Código Ingresado es Incorrecto";
                        }
                    }
                }
                /*switch (_resul.Count)
                {
                    case 1:
                        state = Resul.ErrorCapcha;
                        break;
                    case 4:
                        state = Resul.NoResul;
                        break;
                    case 84:
                        state = Resul.Ok;
                        break;
                    case 76:
                        state = Resul.Ok;
                        break;
                    case 78:
                        state = Resul.Ok;
                        break;
                    case 80:
                        state = Resul.Ok;
                        break;
                    default:
                        state = Resul.Error;
                        break;
                }*/

                if (_resul.Count > 0)
                {
                    state = Resul.Ok;
                }

               // if (numRuc.StartsWith("1") && state == Resul.Ok)
                if (numRuc.StartsWith("1"))
                {
                    //hacemos el parseo 
                    tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>", "");
                    tabla[1] = tabla[1].Replace("</td></tr><tr>", "");
                    tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
                    tabla[3] = tabla[3].Replace("</td></tr><tr>", "");
                    tabla[5] = tabla[5].Replace("=\"bg\" colspan=3>", "");
                    tabla[5] = tabla[5].Replace("</td></tr><tr>", "");
                    tabla[7] = tabla[7].Replace("=\"bg\" colspan=1>", "");
                    tabla[7] = tabla[7].Replace("</td></tr><tr>", "");
                    tabla[9] = tabla[9].Replace("=\"bg\" colspan=1>", "");
                    tabla[9] = tabla[9].Replace("</td><td width=\"27%\" colspan=1 class=\"bgn\">Fecha de Inicio de Actividades:</td>", "");
                    tabla[10] = tabla[10].Replace("=\"bg\" colspan=1> ", "");
                    tabla[10] = tabla[10].Replace("</td></tr><tr>", "");
                    tabla[12] = tabla[12].Replace("=\"bg\" colspan=1>", "");
                    tabla[12] = tabla[12].Replace("</td>", "");
                    tabla[15] = tabla[15].Replace("=\"bg\" colspan=3>", "");
                    tabla[15] = tabla[15].Replace("</td></tr><tr>", "");
                    tabla[17] = tabla[17].Replace("=\"bg\" colspan=3>", "");
                    tabla[17] = tabla[17].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
                    tabla[23] = tabla[23].Replace("=\"bg\" colspan=1>", "");
                    tabla[23] = tabla[23].Replace("</td>", "");
                    tabla[25] = tabla[25].Replace("=\"bg\" colspan=1>", "");
                    tabla[25] = tabla[25].Replace("</td></tr><tr>", "");
                    tabla[27] = tabla[27].Replace("=\"bg\" colspan=1>", "");
                    tabla[27] = tabla[27].Replace("</td>", "");
                    tabla[29] = tabla[29].Replace("=\"bg\" colspan=1>", "");
                    tabla[29] = tabla[29].Replace("</td></tr><tr>", "");
                    tabla[31] = tabla[31].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
                    tabla[31] = tabla[31].Replace("</option></select></td></tr><tr>", "");
                    tabla[33] = tabla[33].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
                    tabla[33] = tabla[33].Replace("</option></select></td></tr><!-- PCR Inicio Cambios --><tr>", "");
                    if (tabla[35].Contains("option value"))
                    {
                        tabla[35] = tabla[35].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
                        tabla[35] = tabla[35].Replace("</option></select></td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    }
                    else
                    {
                        tabla[35] = tabla[35].Replace("=\"bg\" colspan=3>", "");
                        tabla[35] = tabla[35].Replace("</td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    }
                    tabla[37] = tabla[37].Replace("=\"bg\" colspan=3>", "");
                    tabla[37] = tabla[37].Replace("</td></tr><tr>", "");
                    tabla[39] = tabla[39].Replace("=\"bg\" colspan=3>", "");
                    tabla[39] = tabla[39].Replace("</td></tr><!-- MLR Fin Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    tabla[41] = tabla[41].Replace("=\"bg\" colspan=3>", "");
                    tabla[41] = tabla[41].Replace("</td></tr><!-- PCR Fin Cambios --><tr>", "");
                    tabla[43] = tabla[43].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
                    tabla[43] = tabla[43].Replace("</option></select><div id=\"print\" style=\"display:none;\"><table cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" class=\"form-table\"><tr>", "");

                    xRazonSocial = (string)tabla[1].Substring(13);
                    xTipoContribuyente = (string)tabla[3];
                    xTipoDocumento = (string)tabla[5].Substring(0, 3);
                    xNumeroDocumento = (string)tabla[5].Substring(4, 9);
                    xNombreComercial = (string)tabla[7];
                    xFechaInscpricion = (string)tabla[9].Substring(0, 10);
                    xFechaInicioActividades = (string)tabla[10];
                    xEstadoContribuyente = (string)tabla[12].Trim();
                    xCondicionContribuyente = (string)tabla[15].Trim();
                    xDomicilioFiscal = (string)tabla[17];
                    xSistemaEmisionComprobante = (string)tabla[23];
                    xActividadComercioExterior = (string)tabla[25];
                    xSistemaContabilidad = (string)tabla[27];
                    xProfesionuOficio = (string)tabla[29];
                    xActividadesEconomicas = (string)tabla[31];
                    xComprobantesPago = (string)tabla[33];
                    xSistemaEmisionElectronica = (string)tabla[35];
                    xEmisorElectronicoDesde = (string)tabla[37];
                    xComprobantesElectronicos = (string)tabla[39];
                    xAfiliadoPLE = (string)tabla[41];
                    xPadrones = (string)tabla[43];
                    if (xPadrones.Equals("NINGUNO"))
                    {
                        xAgenteretencion = "NO";
                    }
                    else
                    {
                        xAgenteretencion = "SI";
                    }
                }
               // else if (numRuc.StartsWith("2") && state == Resul.Ok && (_resul.Count == 84 || _resul.Count == 76))
                else if (numRuc.StartsWith("2"))
                {//bueno aqui es empresa ...
                    tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>", "");
                    tabla[1] = tabla[1].Replace("</td></tr><tr>", "");
                    tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
                    tabla[3] = tabla[3].Replace("</td></tr><tr>", "");
                    tabla[5] = tabla[5].Replace("=\"bg\" colspan=1>", "");
                    tabla[5] = tabla[5].Replace("</td></tr><tr>", "");
                    tabla[7] = tabla[7].Replace("=\"bg\" colspan=1>", "");
                    tabla[7] = tabla[7].Replace("</td><td width=\"27%\" colspan=1 class=\"bgn\">Fecha de Inicio de Actividades:</td>", "");
                    tabla[8] = tabla[8].Replace("=\"bg\" colspan=1> ", "");
                    tabla[8] = tabla[8].Replace("</td></tr><tr>", "");
                    tabla[10] = tabla[10].Replace("=\"bg\" colspan=1>", "");
                    tabla[10] = tabla[10].Replace("</td>", "");
                    tabla[13] = tabla[13].Replace("=\"bg\" colspan=3>", "");
                    tabla[13] = tabla[13].Replace("</td></tr><tr>", "");
                    tabla[15] = tabla[15].Replace("=\"bg\" colspan=3>", "");
                    tabla[15] = tabla[15].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
                    tabla[21] = tabla[21].Replace("=\"bg\" colspan=1>", "");
                    tabla[21] = tabla[21].Replace("</td>", "");
                    tabla[23] = tabla[23].Replace("=\"bg\" colspan=1>", "");
                    tabla[23] = tabla[23].Replace("</td></tr><tr>", "");
                    tabla[25] = tabla[25].Replace("=\"bg\" colspan=1>", "");
                    tabla[25] = tabla[25].Replace("</td></tr><tr>", "");
                    tabla[27] = tabla[27].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
                    tabla[27] = tabla[27].Replace("</option></select></td></tr><tr>", "");
                    tabla[29] = tabla[29].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
                    tabla[29] = tabla[29].Replace("</option></select></td></tr><!-- PCR Inicio Cambios --><tr>", "");
                    if (tabla[31].Length > 92)
                    {
                        tabla[31] = tabla[31].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
                        tabla[31] = tabla[31].Replace("</option></select></td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    }
                    else
                    {
                        tabla[31] = tabla[31].Replace("=\"bg\" colspan=3>", "");
                        tabla[31] = tabla[31].Replace("</td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    }
                    tabla[33] = tabla[33].Replace("=\"bg\" colspan=3>", "");
                    tabla[33] = tabla[33].Replace("</td></tr><tr>", "");
                    tabla[35] = tabla[35].Replace("=\"bg\" colspan=3>", "");
                    tabla[35] = tabla[35].Replace("</td></tr><!-- MLR Fin Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    tabla[37] = tabla[37].Replace("=\"bg\" colspan=3>", "");
                    tabla[37] = tabla[37].Replace("</td></tr><!-- PCR Fin Cambios --><tr>", "");
                    tabla[39] = tabla[39].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
                    tabla[39] = tabla[39].Replace("</option></select><div id=\"print\" style=\"display:none;\"><table cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" class=\"form-table\"><tr>", "");

                    xRazonSocial = (string)tabla[1].Substring(13).Trim();
                    xTipoContribuyente = (string)tabla[3];
                    xTipoDocumento = "RUC";
                    xNumeroDocumento = (string)tabla[1].Substring(0, 11);
                    xNombreComercial = (string)tabla[5];
                    xFechaInscpricion = (string)tabla[7].Substring(0, 10);
                    xFechaInicioActividades = (string)tabla[8];
                    xEstadoContribuyente = (string)tabla[10].Trim();
                    xCondicionContribuyente = (string)tabla[13].Trim();
                    xDomicilioFiscal = (string)tabla[15];
                    xSistemaEmisionComprobante = (string)tabla[21];
                    xActividadComercioExterior = (string)tabla[23];
                    xSistemaContabilidad = (string)tabla[25];
                    xProfesionuOficio = "NO APLICA POR SER EMPRESA";
                    xActividadesEconomicas = (string)tabla[27];
                    xComprobantesPago = (string)tabla[29];
                    xSistemaEmisionElectronica = (string)tabla[31];
                    xEmisorElectronicoDesde = (string)tabla[33];
                    xComprobantesElectronicos = (string)tabla[35];
                    xAfiliadoPLE = (string)tabla[37];
                    xPadrones = (string)tabla[39];
                    if (xPadrones.Equals("NINGUNO"))
                    {
                        xAgenteretencion = "NO";
                        xAgentepercepcion = "NO";
                    }
                    else if (xPadrones.Contains("Retención de IGV"))
                    {
                        xAgenteretencion = "SI";
                    }
                    if (xPadrones.Contains("Percepción de IGV"))
                    {
                        xAgentepercepcion = "SI";
                    }
                }
                else if (numRuc.StartsWith("2"))
                {//bueno aqui es empresa ...
                    tabla[1] = tabla[1].Replace("=\"bg\" colspan=3>", "");
                    tabla[1] = tabla[1].Replace("</td></tr><tr>", "");
                    tabla[3] = tabla[3].Replace("=\"bg\" colspan=3>", "");
                    tabla[3] = tabla[3].Replace("</td></tr><tr>", "");
                    tabla[5] = tabla[5].Replace("=\"bg\" colspan=1>", "");
                    tabla[5] = tabla[5].Replace("</td></tr><tr>", "");
                    tabla[7] = tabla[7].Replace("=\"bg\" colspan=1>", "");
                    tabla[7] = tabla[7].Replace("</td><td width=\"27%\" colspan=1 class=\"bgn\">Fecha de Inicio de Actividades:</td>", "");
                    tabla[8] = tabla[8].Replace("=\"bg\" colspan=1> ", "");
                    tabla[8] = tabla[8].Replace("</td></tr><tr>", "");
                    tabla[10] = tabla[10].Replace("=\"bg\" colspan=1>", "");
                    tabla[10] = tabla[10].Replace("</td>", "");
                    if (tabla[14].Contains("http://www.sunat.gob.pe/orientacion/Nohallados/index.html"))
                    {
                        tabla[14] = tabla[14].Replace("=\"bg\" colspan=3><a target=\"_blank\" href=\"http://www.sunat.gob.pe/orientacion/Nohallados/index.html\" title=\"Deberá declarar el nuevo domicilio fiscal o confirmar el señalado en el RUC. Para ello, deberá acercarse a los Centros de Servicios al Contribuyente con los documentos que sustenten el nuevo domicilio.\" >", "");
                        tabla[14] = tabla[14].Replace("</a></td></tr><tr>", "");
                    }
                    else
                    {
                        tabla[14] = tabla[14].Replace("=\"bg\" colspan=3>", "");
                        tabla[14] = tabla[14].Replace("</td></tr><tr>", "");
                    }
                    tabla[16] = tabla[16].Replace("=\"bg\" colspan=3>", "");
                    tabla[16] = tabla[16].Replace("</td></tr><!-- SE COMENTO POR INDICACION DEL PASE PAS20134EA20000207 --><!-- <tr> --><!-- ", "");
                    tabla[22] = tabla[22].Replace("=\"bg\" colspan=1>", "");
                    tabla[22] = tabla[22].Replace("</td>", "");
                    tabla[24] = tabla[24].Replace("=\"bg\" colspan=1>", "");
                    tabla[24] = tabla[24].Replace("</td></tr><tr>", "");
                    tabla[26] = tabla[26].Replace("=\"bg\" colspan=1>", "");
                    tabla[26] = tabla[26].Replace("</td></tr><tr>", "");
                    tabla[28] = tabla[28].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
                    tabla[28] = tabla[28].Replace("</option></select></td></tr><tr>", "");
                    tabla[30] = tabla[30].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
                    tabla[30] = tabla[30].Replace("</option></select></td></tr><!-- PCR Inicio Cambios --><tr>", "");
                    if (tabla[31].Length > 92)
                    {
                        tabla[32] = tabla[32].Replace("=\"bg\" colspan=3><select name=\"select\"><option value=\"00\" >", "");
                        tabla[32] = tabla[32].Replace("</option></select></td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    }
                    else
                    {
                        tabla[32] = tabla[32].Replace("=\"bg\" colspan=3>", "");
                        tabla[32] = tabla[32].Replace("</td></tr><!-- MLR Inicio Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    }
                    tabla[34] = tabla[34].Replace("=\"bg\" colspan=3>", "");
                    tabla[34] = tabla[34].Replace("</td></tr><tr>", "");
                    tabla[36] = tabla[36].Replace("=\"bg\" colspan=3>", "");
                    tabla[36] = tabla[36].Replace("</td></tr><!-- MLR Fin Cambios P_DSNT_CPLE_0009_5_FE-MASIFICACION--><tr>", "");
                    tabla[38] = tabla[38].Replace("=\"bg\" colspan=3>", "");
                    tabla[38] = tabla[38].Replace("</td></tr><!-- PCR Fin Cambios --><tr>", "");
                    tabla[40] = tabla[40].Replace("=\"bg\" colspan=3><select name=\"select\" ><option value=\"00\" >", "");
                    tabla[40] = tabla[40].Replace("</option></select><div id=\"print\" style=\"display:none;\"><table cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" class=\"form-table\"><tr>", "");

                    xRazonSocial = (string)tabla[1].Substring(13).Trim();
                    xTipoContribuyente = (string)tabla[3];
                    xTipoDocumento = "RUC";
                    xNumeroDocumento = (string)tabla[1].Substring(0, 11);
                    xNombreComercial = (string)tabla[5];
                    xFechaInscpricion = (string)tabla[7].Substring(0, 10);
                    xFechaInicioActividades = (string)tabla[8];
                    xEstadoContribuyente = (string)tabla[10].Trim();
                    xCondicionContribuyente = (string)tabla[14].Trim();
                    xDomicilioFiscal = (string)tabla[16];
                    xSistemaEmisionComprobante = (string)tabla[22];
                    xActividadComercioExterior = (string)tabla[24];
                    xSistemaContabilidad = (string)tabla[26];
                    xProfesionuOficio = "NO APLICA POR SER EMPRESA";
                    xActividadesEconomicas = (string)tabla[28];
                    xComprobantesPago = (string)tabla[30];
                    xSistemaEmisionElectronica = (string)tabla[32];
                    xEmisorElectronicoDesde = (string)tabla[34];
                    xComprobantesElectronicos = (string)tabla[36];
                    xAfiliadoPLE = (string)tabla[38];
                    xPadrones = (string)tabla[40];
                    if (xPadrones.Equals("NINGUNO"))
                    {
                        xAgenteretencion = "NO";
                        xAgentepercepcion = "NO";
                    }
                    else if (xPadrones.Contains("Retención de IGV"))
                    {
                        xAgenteretencion = "SI";
                    }
                    else if (xPadrones.Contains("Percepción de IGV"))
                    {
                        xAgentepercepcion = "SI";
                    }
                }

                if (state == Resul.Ok)
                {
                    _Ruc = numRuc;
                    _RazonSocial = xRazonSocial;
                    _TipoContribuyente = xTipoContribuyente;
                    _TipoDocumento = xTipoDocumento;
                    _NumeroDocumento = xNumeroDocumento;
                    _NombreComercial = xNombreComercial;
                    _FechaInscpricion = xFechaInscpricion;
                    _FechaInicioActividades = xFechaInicioActividades;
                    _EstadoContribuyente = xEstadoContribuyente;
                    _CondicionContribuyente = xCondicionContribuyente;
                    _DomicilioFiscal = xDomicilioFiscal;
                    _SistemaEmisionComprobante = xSistemaEmisionComprobante;
                    _ActividadComercioExterior = xActividadComercioExterior;
                    _SistemaContabilidad = xSistemaContabilidad;
                    _ProfesionuOficio = xProfesionuOficio;
                    _ActividadesEconomicas = xActividadesEconomicas;
                    _ComprobantesPago = xComprobantesPago;
                    _SistemaEmisionElectronica = xSistemaEmisionElectronica;
                    _EmisorElectronicoDesde = xEmisorElectronicoDesde;
                    _ComprobantesElectronicos = xComprobantesElectronicos;
                    _AfiliadoPLE = xAfiliadoPLE;
                    _Padrones = xPadrones;
                    _AgenteRetencion = xAgenteretencion;
                    _AgentePercepcion = xAgentepercepcion;
                }
            }
            catch (Exception ex)
            {
                _ok = false;
                _error = _error + ex.Message;
                MessageBox.Show("No se puedo realizar la operacion, Intente nuevamente!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void GetInfoDNI(string numDoc, string ImgCapcha)
        {
            try
            {   //A este link le pasamos los datos , RUC y valor del captcha


                string myUrl = String.Format("http://www.sunat.gob.pe/cl-ti-itmrconsruc/jcrS00Alias?accion=consPorTipdoc&nrodoc={0}&codigo={1}&tipdoc={2}", numDoc, ImgCapcha, "1");


                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(myUrl);
                myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                myWebRequest.CookieContainer = myCookie;
                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                myWebRequest.Proxy = null;
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                myWebRequest.Timeout = 10;
                Stream myStream = myHttpWebResponse.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myStream);
                //Leemos los datos
                string xDat = HttpUtility.HtmlDecode(myStreamReader.ReadToEnd());

                if (xDat.Length <= 635)
                {
                    return;
                }

                xDat = xDat.Replace("     ", " ");
                xDat = xDat.Replace("    ", " ");
                xDat = xDat.Replace("   ", " ");
                xDat = xDat.Replace("  ", " ");
                xDat = xDat.Replace("( ", "(");
                xDat = xDat.Replace(" )", ")");
                xDat = xDat.Replace("\r", "");
                xDat = xDat.Replace("\n", "");
                xDat = xDat.Replace("\t", "");
                xDat = xDat.Replace("\r\n ", "");
                xDat = xDat.Replace("      ", "");
                xDat = xDat.Replace("   ", "");
                xDat = xDat.Replace("> <", "><");
                xDat = xDat.Replace(">  <", "><");
                xDat = xDat.Replace("</option><option value=\"00\" >", " /");
                xDat = xDat.Replace("<!-- JRR - 20/09/2010 - Se a�ade cambio de Igor -->", "");
                xDat = xDat.Replace("<!-- -->", "");
                string[] tabla;

                //Lo convertimos a tabla o mejor dicho a un arreglo de string como se ve declarado arriba
                tabla = Regex.Split(xDat, "<td class");

                List<string> _resul = new List<string>();
                for (int i = 0; i < tabla.Length; i++)
                {
                    if (!string.IsNullOrEmpty(tabla[i].Trim()))
                        _resul.Add(tabla[i].Trim());
                }
                if (_resul.Count == 4) //no es valido o algo salio mal
                {
                    _ok = false;
                    _error = " El número de DNI " + numDoc + " consultado no es válido. Debe verificar el número y volver a ingresar. ";
                }
                else
                {
                    if (_resul[0].Contains("Consulta DNI"))
                    {
                        _ok = true;
                    }
                    else
                    {
                        if (_resul[0].Contains("El codigo ingresado es incorrecto"))
                        {
                            _ok = false;
                            _error = "La aplicación ha retornado el siguiente mensaje : El Código Ingresado es Incorrecto";
                        }
                    }
                }
               switch (_resul.Count)
                {
                    case 1:
                        state = Resul.ErrorCapcha;
                        break;
                    case 4:
                        state = Resul.NoResul;
                        break;
                    default:
                        state = Resul.Ok;
                        break;
                }

                if (state == Resul.Ok)
                {

                    tabla[3] = tabla[3].Replace("=beta><table cellSpacing=1 cellPadding=2 width=\"100%\"><tr><th class=beta noWrap>Ruc</th><th class=beta noWrap>Nombre / Razón Social </th><th class=beta noWrap>Ubicación</th><th class=beta noWrap>Estado</th></tr><tr><td align=\"center\" class=\"bg\"><a href=\"javascript:sendNroRuc(", "");
                    tabla[3] = tabla[3].Substring(0, 11);
                    xruc = tabla[3];

                    tabla[4] = tabla[4].Replace("=\"bg\" align=\"left\">", "");
                    tabla[4] = tabla[4].Replace("</td>", "");
                    
                    xpernatural = tabla[4];
                    tabla[5] = tabla[5].Replace("=\"bg\" align=\"left\">", "");
                    tabla[5] = tabla[5].Replace("</td>", "");
                    xDomicilioFiscal = tabla[5];


                    if (tabla[6].Contains("ACTIVO") == true)
                    {
                        xEstadoContribuyente = "ACTIVO";
                    }
                    else
                    {
                        xEstadoContribuyente = "BAJA DE OFICIO";
                    }
                    //xCondicionContribuyente = (string)tabla[14].Trim();
                    //xCondicionContribuyente = (string)tabla[13].Trim();
                    //xCondicionContribuyente = (string)tabla[15].Trim();
                    _Ruc = xruc;
                    _RazonSocial = xpernatural;
                    _EstadoContribuyente = xEstadoContribuyente;
                    _CondicionContribuyente = xCondicionContribuyente;
                    _DomicilioFiscal = xDomicilioFiscal;
                }

                if (state == Resul.ErrorCapcha)
                {
                    _Ruc = "";
                }
                if (state == Resul.NoResul)
                {
                    _Ruc = null;
                }

            }
            catch (Exception ex)
            {
                _ok = false;
                _error = _error + ex.Message;
                MessageBox.Show("No se puedo realizar la operacion, Intente nuevamente!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public string Error
        {
            get
            {
                if (_ok)
                    return string.Empty;
                else
                    return _error;
            }
        }



       



        public static void qutarColor(Bitmap bm)
        {
            for (int x = 0; x < bm.Width; x++)
                for (int y = 0; y < bm.Height; y++)
                {
                    Color pix = bm.GetPixel(x, y);
                    //Aqui puedes jugar con los valores del brillo yo he probado poco pero tu puedes cambiarlo
                    if (pix.GetBrightness() > 0.870f)
                    {
                        bm.SetPixel(x, y, Color.White);
                    }
                    else
                    {
                        bm.SetPixel(x, y, Color.Black);
                    }
                }
        }

        private static Bitmap PreProcessImage(Bitmap memStream)
        {
            Bitmap bm = memStream;
            // Flatten Image to Black and White
            qutarColor(bm);

            // We have a know 6 charcter captcha
            List<Rectangle> charcters = new List<Rectangle>();
            List<int> blackin_x = new List<int>();

            int x_max = bm.Width - 1;
            int y_max = bm.Height - 1;

            // Here we are going to scan through the columns to determine if there in any black in them (charcter)
            for (int temp_x = 0; temp_x <= x_max; temp_x++)
            {
                for (int temp_y = 0; temp_y <= y_max; temp_y++)
                {
                    if (bm.GetPixel(temp_x, temp_y).Name != "ffffffff")
                    {
                        blackin_x.Add(temp_x);
                        break;
                    }
                }
            }

            // Building inital rectangles with X Boundaries
            // This is where we are using our previous results to build the horiztonal boundaries of our charcters
            int temp_start = blackin_x[0];
            for (int temp_x = 0; temp_x < blackin_x.Count - 1; temp_x++)
            {
                if (temp_x == blackin_x.Count - 2) // handles the last iteration
                {
                    Rectangle r = new Rectangle();
                    r.X = temp_start;
                    r.Width = blackin_x[temp_x] - r.X + 2;

                    charcters.Add(r);
                }
                if (blackin_x[temp_x] - blackin_x[temp_x + 1] == -1)
                {
                    continue;
                }
                else
                {
                    Rectangle r = new Rectangle();
                    r.X = temp_start;
                    r.Width = blackin_x[temp_x] - r.X + 1;
                    temp_start = blackin_x[temp_x + 1];
                    charcters.Add(r);
                }

            }

            // Finish out by getting y boundaries
            for (int i = 0; i < charcters.Count; i++)
            {
                Rectangle r = charcters[i];

                for (int temp_y = 0; temp_y < y_max; temp_y++)
                {
                    if (r.Y == 0)
                    {
                        if (!IsRowWhite(bm, temp_y, r.X, r.X + r.Width - 1))
                            r.Y = temp_y;
                    }
                    else if (r.Height == 0)
                    {
                        if (IsRowWhite(bm, temp_y, r.X, r.X + r.Width - 1))
                            r.Height = temp_y - r.Y + 1;
                    }
                    else
                        break;

                }

                charcters[i] = r; // have to do this as rectangle is struct

            }

            int totalWidth = 1 + charcters.Sum(o => o.Width) + (charcters.Count * 2); // we need padding
            int totalHeight = charcters.Max(o => o.Height) + 2; // padding here too 
            int current_x = 1; // start off the left edge 1px

            Bitmap bmp = new Bitmap(totalWidth, totalHeight);
            Graphics g = Graphics.FromImage(bmp);

            // the following four lines are added to help image quality
            g.Clear(Color.White);
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // take our four charcters and move them into a new bitmap 
            foreach (Rectangle r in charcters)
            {
                g.DrawImage(bm, current_x, 1, r, GraphicsUnit.Pixel);
                current_x += r.Width + 2;
            }

            //  bmp.Save(@"C:\postprocess.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            return bmp;
        }

        private static bool IsRowWhite(Bitmap bm, int temp_y, int x, int width)
        {
            for (int i = x; i < width; i++)
            {
                if (bm.GetPixel(i, temp_y).Name != "ffffffff")
                    return false;
            }
            return true;
        }

        public string UseTesseract()
        {

            string text = String.Empty;
            //Recordemos que el metodo ( si ya obviaré las tildes ) ... 
            // el metodo ReadCapcha devuelve la imagen ya procesada ...
            using (Bitmap bm = new Bitmap(ReadCapcha()))
            {
                //Instanciamos el TesseractEngine declarado arriba !
                engine = new TesseractEngine(@".\tessdata", "eng", EngineMode.Default);
                engine.DefaultPageSegMode = PageSegMode.SingleBlock;
                Tesseract.Page p = engine.Process(bm);
                text = p.GetText().Trim().ToUpper().Replace(" ", "");
                //  Console.WriteLine("Text recognized: " + text);
            }
            //Retornamos luego del trabajo del OCR el texto obtenido 
            return text;
        }
    }
}
