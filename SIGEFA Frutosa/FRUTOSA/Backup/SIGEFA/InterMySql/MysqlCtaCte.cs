using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql
{
    class MysqlCtaCte : ICtaCte
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IBancos

        public bool Insert(clsCtaCte cta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaCtaCte", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codban", cta.CodBanco);
                oParam = cmd.Parameters.AddWithValue("ctacte", cta.CtaCte);
                oParam = cmd.Parameters.AddWithValue("tipo", cta.TipoCuenta);
                oParam = cmd.Parameters.AddWithValue("mone", cta.Moneda);
                oParam = cmd.Parameters.AddWithValue("codusu", cta.Coduser);
                oParam = cmd.Parameters.AddWithValue("codalma", cta.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                cta.CodCtaCteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public bool Update(clsCtaCte cta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaCtaCte", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codctacte", cta.CodCtaCte);
                cmd.Parameters.AddWithValue("ctacte", cta.CtaCte);
                cmd.Parameters.AddWithValue("tipo", cta.TipoCuenta);
                cmd.Parameters.AddWithValue("mone", cta.Moneda);
                cmd.Parameters.AddWithValue("codalma", cta.CodAlmacen);
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

        public bool Delete(int codCtaCte, int codAlmacen)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ElimimaCtaCte", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codctacte", codCtaCte);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

        public DataTable ListaCtasBanco(int CodBanco, Int32 CodAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaCuentasCorrientes", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codban", CodBanco);
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
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

        public DataTable ListaCtaCte(Int32 codAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaCtaCte", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

        public clsCtaCte CargaTipoCuenta(int CodCuenta, Int32 CodAlmacen)
        {
            clsCtaCte tc = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraTipoCuenta", con.conector);
                cmd.Parameters.AddWithValue("codcuenta", CodCuenta);
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        tc = new clsCtaCte();
                        tc.TipoCuenta = dr.GetString(0);
                        tc.Moneda = dr.GetInt32(1);
                        tc.saldo = dr.GetDecimal(2);
                        tc.descripcion = dr.GetString(3);
                    }
                }
                return tc;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable CargarMovxCuenta(string Cuenta, Int32 codAlmacen)
        {
           try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarMovxCuenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cuenta", Cuenta);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(tabla);
                return tabla;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsCtaCte BuscaMovimiento(int codMov, int codAlmacen)
        {
            clsCtaCte tc = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("BuscarMovimientoCta", con.conector);
                cmd.Parameters.AddWithValue("codmovi", codMov);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        tc = new clsCtaCte();
                        tc.CodMovi = dr.GetInt32(0);
                        tc.CodBanco = dr.GetInt32(1);
                        tc.NombreBanco = dr.GetString(2);
                        tc.CodCtaCte = dr.GetInt32(3);
                        tc.CtaCte = dr.GetString(4);
                        tc.TipoCuenta = dr.GetString(5);
                        tc.NumTransaccion = dr.GetString(6);
                        tc.FechaMovimiento = dr.GetDateTime(7);
                        tc.DescTipo = dr.GetString(8);
                        tc.egreso = dr.GetDecimal(9);
                        tc.ingreso = dr.GetDecimal(10);
                        tc.saldo = dr.GetDecimal(11);
                        tc.descripcion = dr.GetString(12);
                        tc.tipocambio = dr.GetDecimal(13);
                        tc.TipoCVenta = dr.GetDecimal(14);
                        tc.Moneda = dr.GetInt32(15);
                    }
                }
                return tc;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public bool InsertMovi(clsCtaCte cta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardarCuentaMovimiento", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codcuen", cta.CodCtaCte);
                oParam = cmd.Parameters.AddWithValue("codalma", cta.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("codtrans", cta.NumTransaccion);
                oParam = cmd.Parameters.AddWithValue("descrip", cta.descripcion);
                oParam = cmd.Parameters.AddWithValue("mone", cta.Moneda);
                oParam = cmd.Parameters.AddWithValue("tipo", cta.Tipo);
                oParam = cmd.Parameters.AddWithValue("tcve", cta.tipocambio);
                oParam = cmd.Parameters.AddWithValue("tccom", cta.TipoCVenta);
                oParam = cmd.Parameters.AddWithValue("ingresoegreso", cta.Dmonto);
                oParam = cmd.Parameters.AddWithValue("codu", cta.Coduser);
                oParam = cmd.Parameters.AddWithValue("fechamov", cta.FechaMovimiento);
                oParam = cmd.Parameters.AddWithValue("codtipo", cta.CodTipoPagoServicio);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                cta.CodCtaCteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public bool UpdateMovi(clsCtaCte cta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaMovimiento", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codmovi", cta.CodMovi);
                oParam = cmd.Parameters.AddWithValue("codcuen", cta.CodCtaCte);
                oParam = cmd.Parameters.AddWithValue("descrip", cta.descripcion);
                oParam = cmd.Parameters.AddWithValue("mone", cta.Moneda);
                oParam = cmd.Parameters.AddWithValue("ingresoegreso", cta.Dmonto);
                oParam = cmd.Parameters.AddWithValue("codu", cta.Coduser);
                oParam = cmd.Parameters.AddWithValue("codalma", cta.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("fechamov", cta.FechaMovimiento);
                oParam = cmd.Parameters.AddWithValue("tipo", cta.Tipo);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                cta.CodCtaCteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public bool DeleteMov(int CodMov, Int32 codAlmacen)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminaMovimiento", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codmov", CodMov);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

        public DataTable ListaMovimientos(Int32 codAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarMovimientos", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

        public DataTable ListatipoCtas_x_Banco(int CodBanco, Int32 codAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("con_lista_tipocta_x_banco", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codbanco_ex", CodBanco);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
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

        public DataTable ListanumCta_x_tipocta(int CodBanco, string tipocuenta, Int32 codAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("con_lista_numcta_x_tipocta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codbanco_ex", CodBanco);
                cmd.Parameters.AddWithValue("tipocuenta_ex", tipocuenta);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
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
