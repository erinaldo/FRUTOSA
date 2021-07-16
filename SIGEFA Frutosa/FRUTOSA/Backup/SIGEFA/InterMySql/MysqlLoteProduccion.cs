using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Entidades;
using SIGEFA.Conexion;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql
{
    class MysqlLoteProduccion:ILoteProduccion
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        public Boolean insert(clsLoteProduccion lote)
        {
            try
            {
                con.conectarBD();
                String msj = "";
                cmd = new MySqlCommand("GuardarLoteProduccion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codAuto", lote.CodAutorizado);
                oParam = cmd.Parameters.AddWithValue("codEsp", lote.CodEspecie);
                oParam = cmd.Parameters.AddWithValue("numeroor", null);
                oParam = cmd.Parameters.AddWithValue("fech", lote.Fecha);
                oParam = cmd.Parameters.AddWithValue("fechaf", null);
                oParam = cmd.Parameters.AddWithValue("horafinal", null);
                oParam = cmd.Parameters.AddWithValue("horainicio", lote.HoraInicio);
                oParam = cmd.Parameters.AddWithValue("observ", lote.Observacion);
                oParam = cmd.Parameters.AddWithValue("codUser", lote.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                lote.CodLoteProduccion = Convert.ToInt32(cmd.Parameters["newid"].Value);
                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public Boolean ActualizarEstado(Int32 codLote, int estado)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizaEstadoLoteProduccion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codLote);
                cmd.Parameters.AddWithValue("esta", estado);
                int x = cmd.ExecuteNonQuery();
                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public Boolean ActualizarFechayHoraFinal(Int32 codLote, DateTime fechafinal, DateTime horafinal)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaDatosFinalLoteProduccion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codLote);
                cmd.Parameters.AddWithValue("fecFinal", fechafinal);
                cmd.Parameters.AddWithValue("horFinal", horafinal);
                int x = cmd.ExecuteNonQuery();
                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsLoteProduccion CargaLote(Int32 Codigo)
        {
            clsLoteProduccion lote = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraLoteProduccion", con.conector);
                cmd.Parameters.AddWithValue("codlote", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lote = new clsLoteProduccion();
                        lote.CodLoteProduccion = Convert.ToInt32(dr.GetDecimal(0));
                        lote.CodEspecie = Convert.ToInt32(dr.GetDecimal(1));
                        lote.CodAutorizado = Convert.ToInt32(dr.GetDecimal(2));
                        lote.Numeroorden = dr.GetString(3);
                        lote.Fecha = dr.GetDateTime(4);
                        if (DBNull.Value == dr[5]) { lote.Fechafin = DateTime.Now.Date; } else { lote.Fechafin = dr.GetDateTime(5); };
                        lote.HoraInicio = dr.GetDateTime(6);
                        if (DBNull.Value == dr[7]) { lote.HoraFinal = DateTime.Now.Date; } else { lote.HoraFinal = dr.GetDateTime(7); };
                        lote.Observacion = dr.GetString(8);
                        lote.Estado = dr.GetBoolean(10);
                        lote.IEstadoLote = dr.GetBoolean(9);
                        lote.CodUser = Convert.ToInt32(dr.GetDecimal(11));
                        lote.FechaRegistro = dr.GetDateTime(12);// capturo la fecha
                        lote.CodMateriaPrima = dr.GetInt32(13);
                        lote.CodProductoTerminado = dr.GetInt32(14);
                        lote.Matprima = dr.GetString(15);
                        lote.Prodterminado = dr.GetString(16);
                        lote.Especie = dr.GetString(17);

                    }

                }
                return lote;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }






        public decimal GetRendimientoCosto(DateTime fecha1, DateTime fecha2, Int32 CodProducto, Int32 CodConcepto)
        {
            decimal lote = 0;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ReporteRendimientos", con.conector);
                cmd.Parameters.AddWithValue("fecha1", fecha1);
                cmd.Parameters.AddWithValue("fecha2", fecha2);
                cmd.Parameters.AddWithValue("CodProducto", CodProducto);
                cmd.Parameters.AddWithValue("CodConcepto", CodConcepto);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        lote = dr.GetDecimal(0);
                    }

                }
                return lote;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }









        public DataTable CargaLoteProduccion()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaLoteProduccion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
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




        //raga
        public DataTable MuestraLoteMPSalProd(int codLote)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraLoteMPSalProd", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codnota", codLote);
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


        public DataTable CargaLoteProduccionPorEstado(int estado, DateTime fecha1, DateTime fecha2)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaLoteProduccionXEstado", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("esta", estado);
                cmd.Parameters.AddWithValue("fecha1", fecha1);
                cmd.Parameters.AddWithValue("fecha2", fecha2);
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
        public DataTable CargaDetalleIngresoPorLote(int codLoteProduccion,int tipo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaDetalleIngresoXLote", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codLoteProduccion);
                cmd.Parameters.AddWithValue("codTipo", tipo);
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
        public Boolean Delete(Int32 codLote)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("EliminaLote", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codlote", codLote);
                int x = cmd.ExecuteNonQuery();
                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable CargaDetalleProductosXLote(int codLoteProduccion, int tipo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaDetalleProductosXLote", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codLoteProduccion);
                cmd.Parameters.AddWithValue("codTipo", tipo);
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

        public DataTable CargaProductoTerminadoXlote(int codLoteProduccion, int tipo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaProductoTerminadoXlote", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codLoteProduccion);
                cmd.Parameters.AddWithValue("codTipo", tipo);
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


        public DataTable CargaProductoTerminadoXReproceso(int codLoteProduccion, int tipo, int tipobatch)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaProductoTerminadoXreproceso", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codLoteProduccion);
                cmd.Parameters.AddWithValue("codTipo", tipo);
                cmd.Parameters.AddWithValue("codTipoBatch", tipobatch);
                //codTipoBatch
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







    }
}
