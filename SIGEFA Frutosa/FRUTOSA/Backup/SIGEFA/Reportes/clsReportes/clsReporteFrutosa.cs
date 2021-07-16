using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;
using SIGEFA.Conexion;

namespace SIGEFA.Reportes.clsReportes
{
    class clsReporteFrutosa
    {


        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataSet set = null;
        DataTable tabla;


        //RAGA
        public DataSet getReporteProdTerminado(Int32 codalm,Int32 codprod)
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ReporteProdTerminados", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("codalm", codalm);
                  cmd.Parameters.AddWithValue("codprod", codprod);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\ReporteProdTerminadosRPT.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataSet getReportePesadores()
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ListaPesadores", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\ReportePesadoresRPT.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }





        public DataSet getTicketIngresoContenedor(Int32 coding, Int32 codalm,Int32 proceso)
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ListaIngresoCamaras", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("coding", coding);
                cmd.Parameters.AddWithValue("codalm", codalm);
                cmd.Parameters.AddWithValue("proceso", proceso);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\ticketTrasportistaRPT.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataSet getRptIngMatPrima(Int32 coding, Int32 codalm, Int32 proceso)
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ListaIngresoCamaras", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("coding", coding);
                cmd.Parameters.AddWithValue("codalm", codalm);
                cmd.Parameters.AddWithValue("proceso", proceso);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\ingresoMateriaPrima.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }












        public DataSet getSalidaProduccion(Int32 codnotasal, Int32 codlote)
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraNotaSalidaProduccion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("codnotasalida", codnotasal);
                cmd.Parameters.AddWithValue("codlote", codlote);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\salidaProduccionRPT.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }



















        public DataSet rptCentroCostos1(Int32 reporte, Int32 criterio, Int32 codnivel, Int32 codmoneda, Boolean codtipocosto, Int32 desdecosto, Int32 hastacosto, Int32 desdeart, Int32 hastaart, DateTime fecha1, DateTime fecha2, Boolean valorizado, Int32 codalm)
        {
            //criterio int(1),codnivel int(10),codmoneda int(10),codtipocosto bit,desdecosto int(10),
            //hastacosto int(10),desdeart int(10),hastaart int(10),fecha1 date, fecha2 date,valorizado bit


            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("RptCentroCostos1", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("reporte", reporte);
                cmd.Parameters.AddWithValue("criterio", criterio);
                cmd.Parameters.AddWithValue("codnivel", codnivel);
                cmd.Parameters.AddWithValue("codmoneda", codmoneda);
                cmd.Parameters.AddWithValue("codtipocosto", codtipocosto);
                cmd.Parameters.AddWithValue("desdecosto", desdecosto);
                cmd.Parameters.AddWithValue("hastacosto", hastacosto);
                cmd.Parameters.AddWithValue("desdeart", desdeart);
                cmd.Parameters.AddWithValue("hastaart", hastaart);
                cmd.Parameters.AddWithValue("fecha1", fecha1);
                cmd.Parameters.AddWithValue("fecha2", fecha2);
                cmd.Parameters.AddWithValue("valorizado", valorizado);
                cmd.Parameters.AddWithValue("codalm",codalm);

                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\RptCentroCostos1.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
















        //RAGA  Reporte Compras
        public DataSet getReporteCompras(Int32 Criterio, Int32 CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ListaCompras", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("criterio", Criterio);
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
                cmd.Parameters.AddWithValue("fechaini", FechaInicial);
                cmd.Parameters.AddWithValue("fechafin", FechaFinal);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\reporteComprasRPT.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }










        public DataSet ListaIngresoContenedor(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {


                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ListaIngresoContenedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.Parameters.AddWithValue("codempre", CodEmpresa);
                cmd.Parameters.AddWithValue("situ", Situacion);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\reporteRecepcion.xml", XmlWriteMode.WriteSchema);
                return set;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }


        public DataSet ListaIngresoMatprima(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {


                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ListaIngresosMatprima", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.Parameters.AddWithValue("codempre", CodEmpresa);
                cmd.Parameters.AddWithValue("situ", Situacion);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\reporteRecepcionMP.xml", XmlWriteMode.WriteSchema);
                return set;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }



        public DataSet getNotaEntrega(Int32 coding)
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraNotaEntrega", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("codnota", coding);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\notaEntrega.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }



        //RAGA
        public DataSet ListaReporteRecepcion(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {


                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ListaReporteRecepcion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.Parameters.AddWithValue("codempre", CodEmpresa);
                cmd.Parameters.AddWithValue("situ", Situacion);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                
                set.WriteXml("C:\\XML\\ListaReporteRecepcion.xml", XmlWriteMode.WriteSchema);
                return set;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }



        public DataTable ListaReporteRecepcion2(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();

                cmd = new MySqlCommand("ListaReporteRecepcion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.Parameters.AddWithValue("codempre", CodEmpresa);
                cmd.Parameters.AddWithValue("situ", Situacion);

                adap = new MySqlDataAdapter(cmd);
                adap.Fill(tabla);
                return tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }














        //RAGA
        public DataSet getReporteParteDiario(Int32 codempresa, Int32 codprod, Int32 codlote, Int32 codmatprima,Decimal rendimiento)
        {
            try
            {
                set = new DataSet();
                con.conectarBD();
                cmd = new MySqlCommand("ReporteParteDiario", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 15;
                cmd.Parameters.AddWithValue("codempresa", codempresa);
                cmd.Parameters.AddWithValue("codprod", codprod);
                cmd.Parameters.AddWithValue("codlote", codlote);
                cmd.Parameters.AddWithValue("codmatprima", codmatprima);
                cmd.Parameters.AddWithValue("rend", rendimiento);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(set);
                set.WriteXml("C:\\XML\\RptParteDiarioRPT.xml", XmlWriteMode.WriteSchema);
                return set;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }


    }
}
