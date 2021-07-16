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
    class MysqlFlujoCaja : IFlujoCaja
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IFlujoCaja

        public bool Insert(clsFlujoCaja flu)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaFlujoCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("concep", flu.Concepto);
                oParam = cmd.Parameters.AddWithValue("monto", flu.Monto);
                oParam = cmd.Parameters.AddWithValue("tipo", flu.Tipo);
                oParam = cmd.Parameters.AddWithValue("fecha", flu.Fecha);
                oParam = cmd.Parameters.AddWithValue("fechareg", flu.FechaRegistro);
                oParam = cmd.Parameters.AddWithValue("codusu", flu.CodUser);
                oParam = cmd.Parameters.AddWithValue("codpaser", flu.CodPagoServicio);
                oParam = cmd.Parameters.AddWithValue("alma", flu.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                flu.CodFlujoCajaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public bool Update(clsFlujoCaja flu)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaFlujoCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codflu", flu.CodFlujoCaja);
                cmd.Parameters.AddWithValue("concep", flu.Concepto);
                cmd.Parameters.AddWithValue("mont", flu.Monto);
                cmd.Parameters.AddWithValue("tip", flu.Tipo);
                cmd.Parameters.AddWithValue("fech", flu.Fecha);
                cmd.Parameters.AddWithValue("alma", flu.CodAlmacen);
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

        public bool Delete(int CodFlujoCaja, Int32 CodAlmacen)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarFlujoCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codflu", CodFlujoCaja);
                cmd.Parameters.AddWithValue("alma", CodAlmacen);
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

        public clsFlujoCaja CargaFlujosCaja(int CodFlujoCaja, int CodAlmacen)
        {
            clsFlujoCaja flu = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraFlujoCaja", con.conector);
                cmd.Parameters.AddWithValue("codflu", CodFlujoCaja);
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        flu = new clsFlujoCaja();
                        flu.CodFlujoCaja = Convert.ToInt32(dr.GetDecimal(0));
                        flu.Concepto = dr.GetString(1);
                        flu.Monto = dr.GetDecimal(2);
                        flu.Tipo = dr.GetInt32(3);
                        flu.Fecha = dr.GetDateTime(4);
                        flu.Estado = dr.GetBoolean(5);
                        flu.CodUser = Convert.ToInt32(dr.GetDecimal(6));
                        flu.FechaRegistro = dr.GetDateTime(7);// capturo la fecha 
                        flu.CodAlmacen = Convert.ToInt32(dr.GetDecimal(8));
                    }

                }
                return flu;
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable ListaFlujosCaja(Int32 CodAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaFlujoCaja", con.conector);
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
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

        public DataTable ListaPagoCobro()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarTipoPagoServ", con.conector);
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
