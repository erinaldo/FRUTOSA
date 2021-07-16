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
    class MysqlTransferencia : ITransferencia
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region IRequerimiento Members

        public bool insert(clsTransferencia transferencia)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codTipoDoc", transferencia.CodTipoDocumento);
                oParam = cmd.Parameters.AddWithValue("codser", transferencia.CodSerie);
                oParam = cmd.Parameters.AddWithValue("numerac", transferencia.NumDoc);
                oParam = cmd.Parameters.AddWithValue("codalmaO", transferencia.CodAlmacenO);
                oParam = cmd.Parameters.AddWithValue("codalmaD", transferencia.CodAlmacenD);
                oParam = cmd.Parameters.AddWithValue("coment", transferencia.Comentario);
                oParam = cmd.Parameters.AddWithValue("codUsu", transferencia.CodUsuario);
                oParam = cmd.Parameters.AddWithValue("codalma", transferencia.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                transferencia.CodTransferencianuevo= cmd.Parameters["newid"].Value.ToString();

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

        public bool insertdetalle(clsDetalleTransferencia Detalle)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaDetalleTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codProducto", Detalle.CodProducto);
                oParam = cmd.Parameters.AddWithValue("codTransf", Detalle.CodTransferencia);
                oParam = cmd.Parameters.AddWithValue("unidad", Detalle.Unidad);
                oParam = cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
                oParam = cmd.Parameters.AddWithValue("codUser", Detalle.Codusuario);
                oParam = cmd.Parameters.AddWithValue("valorprom", Detalle.Valorpromedio);
                oParam = cmd.Parameters.AddWithValue("valorproms", Detalle.Valorpromediosoles);
                oParam = cmd.Parameters.AddWithValue("precioprom", Detalle.Preciopromedio);
                oParam = cmd.Parameters.AddWithValue("codrequerim", Detalle.CodRequerimiento);
                oParam = cmd.Parameters.AddWithValue("codalma", Detalle.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                Detalle.CodDetalleTransferencia = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public bool update(clsTransferencia transferencia)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codTipoDoc", Convert.ToInt32(transferencia.CodTipoDocumento));
                cmd.Parameters.AddWithValue("codser", transferencia.CodSerie);
                cmd.Parameters.AddWithValue("numer", transferencia.NumDoc);
                cmd.Parameters.AddWithValue("codalmaO", transferencia.CodAlmacenO);
                cmd.Parameters.AddWithValue("codalmaD", transferencia.CodAlmacenD);
                cmd.Parameters.AddWithValue("coment", transferencia.Comentario);
                cmd.Parameters.AddWithValue("fechaI", transferencia.Fechaingreso);
                cmd.Parameters.AddWithValue("fechaA", transferencia.Fechaaceptacion);
                cmd.Parameters.AddWithValue("codUsu", transferencia.CodUsuario);
                cmd.Parameters.AddWithValue("codalma", transferencia.CodAlmacen);
                cmd.Parameters.AddWithValue("codtrans", transferencia.CodTransferencia);
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
        public bool updatedetalle(clsDetalleTransferencia Detalle)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaDetalletransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codDetalleTrans", Detalle.CodDetalleTransferencia);
                cmd.Parameters.AddWithValue("codProd", Detalle.CodProducto);
                cmd.Parameters.AddWithValue("codTrans", Detalle.CodTransferencia);
                cmd.Parameters.AddWithValue("unid", Detalle.Unidad);
                cmd.Parameters.AddWithValue("cant", Detalle.Cantidad);
                cmd.Parameters.AddWithValue("codUsu", Detalle.Codusuario);
                cmd.Parameters.AddWithValue("valorprom", Detalle.Valorpromedio);
                cmd.Parameters.AddWithValue("valorproms", Detalle.Valorpromediosoles);
                cmd.Parameters.AddWithValue("precioprom", Detalle.Preciopromedio);
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

        public bool deletedetalle(int CodigoDetalle)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarDetalleRequerimiento", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("coddeta", CodigoDetalle);
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

        public DataTable MuestraTransferenciasVigentes(Int32 CodAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaTransferenciasVigentes", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public bool anular(int codigotransferencia)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("AnularTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codigotransferencia);
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
        public bool atender(int codigotransferencia)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("AtenderTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codigotransferencia);
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

        public clsTransferencia CargaTransferencia(int codigotransferencia)
        {
            clsTransferencia transferencia = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("CargaTransferencia", con.conector);
                cmd.Parameters.AddWithValue("cod", codigotransferencia);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        transferencia = new clsTransferencia();
                        transferencia.CodTransferencia = dr.GetInt32(0);
                        transferencia.CodTransferencianuevo = dr.GetString(1);
                        transferencia.CodAlmacenD = dr.GetInt32(2);
                        transferencia.CodTipoDocumento = dr.GetInt32(3);
                        transferencia.SiglaDocumento = dr.GetString(4);
                        transferencia.DescripcionDocumento = dr.GetString(5);
                        transferencia.CodSerie = dr.GetInt32(6);
                        transferencia.Serie = dr.GetString(7);
                        transferencia.NumDoc = dr.GetString(8);
                        transferencia.Fechaingreso = dr.GetDateTime(9);
                        transferencia.Comentario = dr.GetString(10);

                    }
                }
                return transferencia;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public DataTable CargaDetalle(int codigotransferencia)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("BuscaDetalleTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codigotransferencia);
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
