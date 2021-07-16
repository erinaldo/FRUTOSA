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
    class MysqlCajaChica : ICajaChica
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion ICajaChica

        public Boolean Insert(clsCajaChica caja)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaCajaChica", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                cmd.Parameters.AddWithValue("des", caja.Concepto);
                cmd.Parameters.AddWithValue("mon", caja.Monto);
                cmd.Parameters.AddWithValue("tip", caja.tipo);
                cmd.Parameters.AddWithValue("fec", caja.Fecha);
                cmd.Parameters.AddWithValue("NumDoc", caja.NumDocumento);
                cmd.Parameters.AddWithValue("numG", caja.NumGuia);
                cmd.Parameters.AddWithValue("numRecLiqui", caja.NumRecLiquidacion);
                cmd.Parameters.AddWithValue("ton", caja.Toneladas);
                cmd.Parameters.AddWithValue("carga", caja.CargaDescarga);
                cmd.Parameters.AddWithValue("codusu", caja.CodUser);
                cmd.Parameters.AddWithValue("freg", caja.FechaRegistro);
                cmd.Parameters.AddWithValue("codTPcaja", caja.CodTipoPagoCaja);
                cmd.Parameters.AddWithValue("codSucur", caja.CodSucursal);
                cmd.Parameters.AddWithValue("codveh", caja.CodVehiculo);
                cmd.Parameters.AddWithValue("caso", caja.Caso);
                cmd.Parameters.AddWithValue("codcentro", caja.ICentroCosto);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                caja.CodCajaChicaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean Update(clsCajaChica caja)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaCajaChica", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", caja.CodCajaChica);
                cmd.Parameters.AddWithValue("concep", caja.Concepto);
                cmd.Parameters.AddWithValue("mont", caja.Monto);
                cmd.Parameters.AddWithValue("tip", caja.tipo);
                cmd.Parameters.AddWithValue("fech", caja.Fecha);
                cmd.Parameters.AddWithValue("NumDoc", caja.NumDocumento);
                cmd.Parameters.AddWithValue("numGui", caja.NumGuia);
                cmd.Parameters.AddWithValue("numRecLiqui", caja.NumRecLiquidacion);
                cmd.Parameters.AddWithValue("tonel", caja.Toneladas);
                cmd.Parameters.AddWithValue("cargadesca", caja.CargaDescarga);
                cmd.Parameters.AddWithValue("fecharegis", caja.FechaRegistro);
                cmd.Parameters.AddWithValue("codTipoPa", caja.CodTipoPagoCaja);
                cmd.Parameters.AddWithValue("codSucur", caja.CodSucursal);
                cmd.Parameters.AddWithValue("codcentro", caja.ICentroCosto);
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

        public Boolean Delete(Int32 Codigo)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarCajaChica", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", Codigo);
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

        public DataTable ListaCajaChica(Int32 codSucursal)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaCajaChica", con.conector);
                cmd.Parameters.AddWithValue("codSucur", codSucursal);
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

        public DataTable ListaCajaChicaFechas(Int32 codAlma, DateTime fecha1, DateTime fecha2)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaCajaChicaFechas", con.conector);
                cmd.Parameters.AddWithValue("codAlma", codAlma);
                cmd.Parameters.AddWithValue("fecha1", fecha1);
                cmd.Parameters.AddWithValue("fecha2", fecha2);
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

        public Boolean RendicionCaja(Int32 Codigo)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("RendicionCajaChica", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", Codigo);
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

        public DataTable ListaRendiciones(Int32 codSucursal)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaRendiciones", con.conector);
                cmd.Parameters.AddWithValue("codSucur", codSucursal);
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

        public DataTable ListaRendicionesXSucursal(Int32 codSucursal)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaRendicionesSucursal", con.conector);
                cmd.Parameters.AddWithValue("codSucur", codSucursal);
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

        public DataTable ListaDetalleRendiciones(Int32 Codigo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaDetalleRendiciones", con.conector);
                cmd.Parameters.AddWithValue("codRend", Codigo);
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

        public Boolean AnulaRendicionCajaChica(Int32 Codigo)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("AnulaRendicionCajaChica", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", Codigo);
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
       
        public Boolean GestionaRendicionCajaChica(clsCajaChica caja)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GestionaRendicionCajaChica", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", caja.CodCajaChica);
                cmd.Parameters.AddWithValue("tipo", caja.ApruebaDesaprueba);
                cmd.Parameters.AddWithValue("codAlma", caja.CodSucursal);
                cmd.Parameters.AddWithValue("comentario", caja.Concepto);
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

        public Boolean GestionaRendicion(Int32 Codigo, String Observacion, Int32 Tipo)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GestionaRendicion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", Codigo);
                cmd.Parameters.AddWithValue("observa", Observacion);
                cmd.Parameters.AddWithValue("tipo", Tipo);
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

        public Boolean ApruebaRendicion(Int32 Codigo)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ApruebaRendicion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", Codigo);
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

        public Boolean DesapruebaRendicion(Int32 Codigo/*, Int32 Tipo, String Comentario*/)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("DesapruebaRendicion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codigo", Codigo);
                //cmd.Parameters.AddWithValue("tipo", Tipo);
                //cmd.Parameters.AddWithValue("comentario", Comentario);
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

        public clsCajaChica CargaSaldoCajaChica(Int32 codAlmacen)
        {
            clsCajaChica caja = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("VerificaAperturaCajaChica", con.conector);
                cmd.Parameters.AddWithValue("codAlma", codAlmacen);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        caja = new clsCajaChica();
                        caja.SaldoCaja = Convert.ToDecimal(dr.GetDecimal(0));
                    }
                }
                return caja;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsCajaChica VerificaSaldoCajaChica(Int32 codSucursal)
        {
            clsCajaChica caja = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("VerificaSaldoCajaChica", con.conector);
                cmd.Parameters.AddWithValue("codSucur", codSucursal);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        caja = new clsCajaChica();
                        caja.MontoApertura = Convert.ToDecimal(dr.GetDecimal(0));
                        caja.MontoIngresado = Convert.ToDecimal(dr.GetDecimal(1));
                        caja.MontoEntregado = Convert.ToDecimal(dr.GetDecimal(2));
                        caja.MontoDisponible = Convert.ToDecimal(dr.GetDecimal(3));
                    }
                }
                return caja;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public Boolean InsertRendicion(clsCajaChica caja)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaRendicion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                cmd.Parameters.AddWithValue("monto", caja.Monto);
                cmd.Parameters.AddWithValue("fecha", caja.FechaRendicion);
                cmd.Parameters.AddWithValue("codusu", caja.CodUser);
                cmd.Parameters.AddWithValue("codSucur", caja.CodSucursal);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                caja.CodRendicion = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean InsertDetalleRendicion(clsCajaChica caja)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaDetalleRendicion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                cmd.Parameters.AddWithValue("codrend", caja.CodRendicion);
                cmd.Parameters.AddWithValue("codcaja", caja.CodCajaChica);
                cmd.Parameters.AddWithValue("monto", caja.Monto);
                cmd.Parameters.AddWithValue("codusu", caja.CodUser);
                cmd.Parameters.AddWithValue("codAlma", caja.CodSucursal);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                caja.CodCajaChicaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public DataTable ListaRendicionesTesoreria()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaRendicionesTesoreria", con.conector);
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

        public Boolean GeneraLiquidacion(clsCajaChica caja)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GeneraLiquidacion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codRend", caja.CodRendicion);
                cmd.Parameters.AddWithValue("numChe", caja.NumCheque);
                cmd.Parameters.AddWithValue("montoLiqui", caja.MontoLiquidado);
                cmd.Parameters.AddWithValue("responsa", caja.Responsable);
                cmd.Parameters.AddWithValue("fechaliqui", caja.FechaLiquidacion);
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

        public Boolean InsertLiquidacion(clsCajaChica CajaChica)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaChequeCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                cmd.Parameters.AddWithValue("numChe", CajaChica.NumCheque);
                cmd.Parameters.AddWithValue("montoliq", CajaChica.MontoLiquidado);
                cmd.Parameters.AddWithValue("responsa", CajaChica.Responsable);
                cmd.Parameters.AddWithValue("fechaliq", CajaChica.FechaLiquidacion);
                cmd.Parameters.AddWithValue("codusu", CajaChica.CodUser);
                cmd.Parameters.AddWithValue("codsucur", CajaChica.CodSucursal);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                CajaChica.CodRendicion = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public DataTable ListaLiquidacionesVigentes(Int32 codSucursal)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("LiquidacionesVigentes", con.conector);
                cmd.Parameters.AddWithValue("codSucur", codSucursal);
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

        #endregion
    }
}
