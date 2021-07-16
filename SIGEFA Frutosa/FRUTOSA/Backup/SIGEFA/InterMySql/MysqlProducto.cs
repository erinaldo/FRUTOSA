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
    class MysqlProducto:IProducto
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IProducto

        public Boolean Insert(clsProducto prod)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codusu", prod.CodUsuario);
                if (prod.CodGrupo != 0) { oParam = cmd.Parameters.AddWithValue("codgru", prod.CodGrupo); } else { oParam = cmd.Parameters.AddWithValue("codgru", null); }
                if (prod.CodLinea != 0) { oParam = cmd.Parameters.AddWithValue("codlin", prod.CodLinea); } else { oParam = cmd.Parameters.AddWithValue("codlin", null); }
                oParam = cmd.Parameters.AddWithValue("codfam", prod.CodFamilia);
                if (prod.CodMarca != 0) { oParam = cmd.Parameters.AddWithValue("codmar", prod.CodMarca); } else { oParam = cmd.Parameters.AddWithValue("codmar", null); }
                oParam = cmd.Parameters.AddWithValue("coduni", prod.CodUnidadMedida);
                oParam = cmd.Parameters.AddWithValue("codtip", prod.CodTipoArticulo);
                oParam = cmd.Parameters.AddWithValue("control", prod.CodControlStock);
                oParam = cmd.Parameters.AddWithValue("referencia", prod.Referencia);
                oParam = cmd.Parameters.AddWithValue("descripcion", prod.Descripcion);
                oParam = cmd.Parameters.AddWithValue("igv", prod.Igv);
                oParam = cmd.Parameters.AddWithValue("precioconigv", prod.ConIgv);
                oParam = cmd.Parameters.AddWithValue("detraccion", prod.Detraccion);
                oParam = cmd.Parameters.AddWithValue("estado", prod.Estado);
                oParam = cmd.Parameters.AddWithValue("comision", prod.Comision);
                oParam = cmd.Parameters.AddWithValue("precioca", prod.PrecioCatalogo);
                oParam = cmd.Parameters.AddWithValue("maxPorcDesc", prod.MaxPorcDesc);

                if (prod.CodTipoArticulo == 5)
                {
                   oParam = cmd.Parameters.AddWithValue("codigoProductoTerminado", prod.ICodProductoReferencia);
                }
                else
                {
                    oParam = cmd.Parameters.AddWithValue("codigoProductoTerminado", null);
                }
                oParam = cmd.Parameters.AddWithValue("codigoMateriaPrima", null);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                prod.CodProducto = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean InsertProductoAlmacen(clsProducto prod)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaProductoAlmacen", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codusu", prod.CodUsuario);                
                oParam = cmd.Parameters.AddWithValue("valorpromedio", prod.ValorProm);
                oParam = cmd.Parameters.AddWithValue("preciopromedio", prod.PrecioProm);
                oParam = cmd.Parameters.AddWithValue("recargo", prod.Recargo);
                oParam = cmd.Parameters.AddWithValue("precioventa", prod.PrecioVenta);
                oParam = cmd.Parameters.AddWithValue("oferta", prod.Oferta);
                oParam = cmd.Parameters.AddWithValue("descuento", prod.PDescuento);
                oParam = cmd.Parameters.AddWithValue("montodescuento", prod.MontoDscto);
                oParam = cmd.Parameters.AddWithValue("preciooferta", prod.PrecioOferta);
                oParam = cmd.Parameters.AddWithValue("preciovariable", prod.PrecioVariable);
                oParam = cmd.Parameters.AddWithValue("maximodscto", prod.MaximoDscto);
                oParam = cmd.Parameters.AddWithValue("stockminimo", prod.StockMinimo);
                oParam = cmd.Parameters.AddWithValue("stockmaximo", prod.StockMaximo);
                oParam = cmd.Parameters.AddWithValue("stockreposicion", prod.StockReposicion);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                prod.CodProductoAlmacen = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean InsertCaracteristica(clsCaracteristicaProducto carpro)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaCaracteristicaProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codpro", carpro.CodProducto);
                oParam = cmd.Parameters.AddWithValue("codcar", carpro.CodCaracteristica);
                oParam = cmd.Parameters.AddWithValue("valor", carpro.Valor);
                oParam = cmd.Parameters.AddWithValue("codusu", carpro.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                carpro.CodCaracteristicaProductoNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean InsertNota(clsNotaProducto notpro)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaNotaProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codpro", notpro.CodProducto);                
                oParam = cmd.Parameters.AddWithValue("nota", notpro.Nota);
                oParam = cmd.Parameters.AddWithValue("codusu", notpro.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                notpro.CodNotaProducto = Convert.ToInt32(cmd.Parameters["newid"].Value);

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



        public Boolean ActualizaLoteProducto(Int32 codTipo, Int32 codlote, Int32 cod)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaLoteProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codTipo", codTipo);
                oParam = cmd.Parameters.AddWithValue("codlote", codlote);
                oParam = cmd.Parameters.AddWithValue("cod", cod);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
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













        public Boolean Update(clsProducto prod)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("codpro", prod.CodProducto);
                if (prod.CodGrupo != 0) { cmd.Parameters.AddWithValue("codgru", prod.CodGrupo); } else { cmd.Parameters.AddWithValue("codgru", null); }
                if (prod.CodLinea != 0) { cmd.Parameters.AddWithValue("codlin", prod.CodLinea); } else { cmd.Parameters.AddWithValue("codlin", null); }
                cmd.Parameters.AddWithValue("codfam", prod.CodFamilia);
                if (prod.CodMarca != 0) { cmd.Parameters.AddWithValue("codmar", prod.CodMarca); } else { cmd.Parameters.AddWithValue("codmar", null); }
                cmd.Parameters.AddWithValue("coduni", prod.CodUnidadMedida);
                cmd.Parameters.AddWithValue("codtip", prod.CodTipoArticulo);
                cmd.Parameters.AddWithValue("control", prod.CodControlStock);
                cmd.Parameters.AddWithValue("referencia", prod.Referencia);
                cmd.Parameters.AddWithValue("descripcion", prod.Descripcion);                
                cmd.Parameters.AddWithValue("igv", prod.Igv);
                cmd.Parameters.AddWithValue("precioconigv", prod.ConIgv);
                cmd.Parameters.AddWithValue("detraccion", prod.Detraccion);
                cmd.Parameters.AddWithValue("estado", prod.Estado);
                cmd.Parameters.AddWithValue("comision", prod.Comision);
                cmd.Parameters.AddWithValue("precioca", prod.PrecioCatalogo);
                cmd.Parameters.AddWithValue("maxPorcDesc", prod.MaxPorcDesc);
                if (prod.CodTipoArticulo == 5)
                {
                    cmd.Parameters.AddWithValue("codigoProductoTerminado", prod.ICodProductoReferencia);
                }
                else
                {
                    cmd.Parameters.AddWithValue("codigoProductoTerminado", null);
                }


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

        public Boolean UpdateProductoAlmacen(clsProducto prod)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaProductoAlmacen", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpro", prod.CodProducto);
                cmd.Parameters.AddWithValue("codalma", prod.CodAlmacen);  
                cmd.Parameters.AddWithValue("valorprom", prod.ValorProm);
                cmd.Parameters.AddWithValue("precioprom", prod.PrecioProm);
                cmd.Parameters.AddWithValue("recargo", prod.Recargo);
                cmd.Parameters.AddWithValue("valorventa", prod.ValorVenta);
                cmd.Parameters.AddWithValue("precioventa", prod.PrecioVenta);
                cmd.Parameters.AddWithValue("oferta", prod.Oferta);
                cmd.Parameters.AddWithValue("descuento", prod.PDescuento);
                cmd.Parameters.AddWithValue("montodescuento", prod.MontoDscto);
                cmd.Parameters.AddWithValue("preciooferta", prod.PrecioOferta);
                cmd.Parameters.AddWithValue("preciovariable", prod.PrecioVariable);
                cmd.Parameters.AddWithValue("maximodscto", prod.MaximoDscto);
                cmd.Parameters.AddWithValue("stockminimo", prod.StockMinimo);
                cmd.Parameters.AddWithValue("stockmaximo", prod.StockMaximo);
                cmd.Parameters.AddWithValue("stockreposicion", prod.StockReposicion);
                cmd.Parameters.AddWithValue("igv", prod.Igv);
                cmd.Parameters.AddWithValue("precioconigv", prod.ConIgv);
                cmd.Parameters.AddWithValue("detraccion", prod.Detraccion);
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



        public Boolean Delete(Int32 CodProducto)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codprod", CodProducto);
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

        public Boolean DeleteProductoAlmacen(Int32 CodProductoAlmacen)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarProductoAlmacen", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codprod", CodProductoAlmacen);
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

        public Boolean DeleteCaracteristica(Int32 CodCarPro)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarCaracteristicaProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codcarpro", CodCarPro);
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

        public Boolean DeleteNota(Int32 CodNotaProducto)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarNotaProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codnota", CodNotaProducto);
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

        public clsProducto CargaProducto(Int32 CodPro, Int32 CodAlm)
         {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProducto", con.conector);
                cmd.Parameters.AddWithValue("codpro", CodPro);
                cmd.Parameters.AddWithValue("codalm", CodAlm);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pro = new clsProducto();
                        pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                        pro.Referencia = dr.GetString(1);
                        pro.Descripcion = dr.GetString(2);
                        pro.Igv = dr.GetBoolean(3);
                        pro.Detraccion = dr.GetBoolean(4);
                        pro.CodTipoArticulo = Convert.ToInt32(dr.GetString(5));
                        pro.CodFamilia = Convert.ToInt32(dr.GetString(6));
                        pro.CodLinea = Convert.ToInt32(dr.GetString(7));
                        pro.CodGrupo = Convert.ToInt32(dr.GetString(8));
                        pro.CodMarca = Convert.ToInt32(dr.GetString(9));
                        pro.CodControlStock = Convert.ToInt32(dr.GetString(10));
                        pro.CodUnidadMedida = Convert.ToInt32(dr.GetString(11));
                        
                        pro.CodUsuario = Convert.ToInt32(dr.GetString(13));
                        pro.UltimaModificacion = dr.GetDateTime(14);// capturo la fecha 
                        pro.FechaRegistro = dr.GetDateTime(15);// capturo la fecha 
                        pro.CodProductoAlmacen = Convert.ToInt32(dr.GetString(16));
                        pro.CodAlmacen = Convert.ToInt32(dr.GetString(17));
                        pro.PrecioProm = Convert.ToDouble(dr.GetString(18));
                        pro.ValorProm = Convert.ToDouble(dr.GetString(19));
                        pro.Recargo = Convert.ToDouble(dr.GetString(20));
                        pro.ValorVenta = Convert.ToDouble(dr.GetString(21));
                        pro.PrecioVenta = Convert.ToDouble(dr.GetString(22));
                        
                        pro.PDescuento = Convert.ToDouble(dr.GetString(24));
                        pro.MontoDscto = Convert.ToDouble(dr.GetString(25));
                        pro.PrecioOferta = Convert.ToDouble(dr.GetString(26));
                        pro.MaximoDscto = Convert.ToDouble(dr.GetString(27));
                        
                        pro.StockActual = Convert.ToDouble(dr.GetString(30));
                        pro.StockDisponible = Convert.ToDouble(dr.GetString(31));
                        pro.StockMaximo = Convert.ToDouble(dr.GetString(32));
                        pro.StockMinimo = Convert.ToDouble(dr.GetString(33));
                        pro.StockReposicion = Convert.ToDouble(dr.GetString(34));
                        pro.Comision = Convert.ToDecimal(dr.GetString(35));
                        pro.PrecioCatalogo = Convert.ToDecimal(dr.GetString(36));
                        pro.Estado = dr.GetBoolean(12);
                        //pro.Oferta = Convert.ToBoolean(dr.GetString(23));

                        pro.Oferta = dr.GetBoolean(23);
                        pro.ConIgv = dr.GetBoolean(28);
                        //pro.PrecioVariable = Convert.ToBoolean(dr.GetString(29));
                        pro.PrecioVariable = dr.GetBoolean(29);
                        pro.StockFuturo = dr.GetDecimal(38);
                        pro.StockPorRecibir = dr.GetDecimal(37);
                        pro.MaxPorcDesc = dr.GetDecimal(39);
                        pro.ICodProductoReferencia = dr.GetInt32(40);
                        pro.IInicioSerie = dr.GetInt32(41);
                        pro.Serieactual = dr.GetInt32(42);
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public clsProducto CargaProductoDetalle(Int32 CodPro, Int32 CodAlm, Int32 Caso, Int32 CodLista)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProductoDetalle", con.conector);
                cmd.Parameters.AddWithValue("codpro", CodPro);
                cmd.Parameters.AddWithValue("codalm", CodAlm);
                cmd.Parameters.AddWithValue("caso", Caso);
                cmd.Parameters.AddWithValue("lista", CodLista);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (Caso == 1)
                        {
                            pro = new clsProducto();
                            pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                            pro.Referencia = dr.GetString(1);
                            pro.Descripcion = dr.GetString(2);
                            pro.StockDisponible = Convert.ToDouble(dr.GetDecimal(3));
                            pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
                            pro.UnidadDescrip = dr.GetString(5);
                            pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
                            pro.ConIgv = dr.GetBoolean(7);
                            pro.Igv = dr.GetBoolean(8);
                            pro.MaxPorcDesc = dr.GetDecimal(9);
                            pro.Serieactual = dr.GetInt32(10);//RAGA
                        }
                        else
                        {
                            pro = new clsProducto();
                            pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                            pro.Referencia = dr.GetString(1);
                            pro.Descripcion = dr.GetString(2);
                            pro.StockDisponible = Convert.ToDouble(dr.GetDecimal(3));
                            pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
                            pro.UnidadDescrip = dr.GetString(5);
                            pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
                            pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(7));
                            pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(8));
                            pro.PrecioVariable = dr.GetBoolean(9);
                            pro.Oferta = dr.GetBoolean(10);
                            pro.PDescuento = Convert.ToDouble(dr.GetDecimal(11));
                            pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(12));
                            pro.ConIgv = dr.GetBoolean(13);
                            pro.Igv = dr.GetBoolean(14);
                            pro.MaxPorcDesc = dr.GetDecimal(18);
                            pro.Serieactual = dr.GetInt32(19);//RAGA
                        }
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }




        public clsProducto CargaStockProducto(Int32 CodPro, Int32 CodAlm,Int32 tipo)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraStockProducto", con.conector);
                cmd.Parameters.AddWithValue("tipo", tipo);
                cmd.Parameters.AddWithValue("codpro", CodPro);
                cmd.Parameters.AddWithValue("codalma", CodAlm);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pro = new clsProducto();
                        pro.StockDisponible = Convert.ToInt32(dr.GetDecimal(0));
                       
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }






        public clsProducto CargaProductoTermDetalle(Int32 CodPro, Int32 CodAlm, Int32 Caso)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProductoTermDetalle", con.conector);
                cmd.Parameters.AddWithValue("codpro", CodPro);
                cmd.Parameters.AddWithValue("codalm", CodAlm);
                cmd.Parameters.AddWithValue("caso", Caso);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (Caso == 1)
                        {
                            pro = new clsProducto();
                            pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                            pro.Referencia = dr.GetString(1);
                            pro.Descripcion = dr.GetString(2);
                            pro.StockDisponible = Convert.ToDouble(dr.GetDecimal(3));
                            pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
                            pro.UnidadDescrip = dr.GetString(5);
                            pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
                            pro.ConIgv = dr.GetBoolean(7);
                            pro.Igv = dr.GetBoolean(8);
                            pro.MaxPorcDesc = dr.GetDecimal(9);
                            pro.Serieactual = dr.GetInt32(10);//RAGA
                        }
                        else
                        {
                            pro = new clsProducto();
                            pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                            pro.Referencia = dr.GetString(1);
                            pro.Descripcion = dr.GetString(2);
                            pro.StockDisponible = Convert.ToDouble(dr.GetDecimal(3));
                            pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
                            pro.UnidadDescrip = dr.GetString(5);
                            pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
                            pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(7));
                            pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(8));
                            pro.PrecioVariable = dr.GetBoolean(9);
                            pro.Oferta = dr.GetBoolean(10);
                            pro.PDescuento = Convert.ToDouble(dr.GetDecimal(11));
                            pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(12));
                            pro.ConIgv = dr.GetBoolean(13);
                            pro.Igv = dr.GetBoolean(14);
                            pro.MaxPorcDesc = dr.GetDecimal(18);
                            pro.Serieactual = dr.GetInt32(19);//RAGA
                        }
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }




        //RAGA
        public clsProducto CargaProductoDetalleIngMatPrima(Int32 CodAlm, Int32 coddetalleing)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProductoDetalleIngreso", con.conector);
                cmd.Parameters.AddWithValue("codalma", CodAlm);
                cmd.Parameters.AddWithValue("coddetalleing", coddetalleing);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pro = new clsProducto();
                        pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                        pro.Referencia = dr.GetString(1);
                        pro.Descripcion = dr.GetString(2);
                        pro.StockDisponible = Convert.ToDouble(dr.GetDecimal(3));
                        pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
                        pro.UnidadDescrip = dr.GetString(5);
                        pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
                        pro.ConIgv = dr.GetBoolean(7);
                        pro.Igv = dr.GetBoolean(8);
                        pro.MaxPorcDesc = dr.GetDecimal(9);
                        pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(10));
                        pro.Serieactual = dr.GetInt32(11);
                        pro.ICodProductoReferencia = dr.GetInt32(12);


                    //    pro.Serieactual = dr.GetInt32(10);//RAGA
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }







        public clsProducto CargaDatosProductoOrden(Int32 CodPro, Int32 CodAlm, Int32 codusu, Decimal cant)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("CargaDatosProductoOrden", con.conector);
                cmd.Parameters.AddWithValue("alma", CodAlm);
                cmd.Parameters.AddWithValue("usu", codusu);
                cmd.Parameters.AddWithValue("codpro",CodPro);
                cmd.Parameters.AddWithValue("cant", cant);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {                        
                            pro = new clsProducto();
                            pro.Porllegar = Convert.ToInt32(dr.GetDecimal(0));
                            pro.PorAtender = Convert.ToInt32(dr.GetDecimal(1));
                            pro.PorCompletar = Convert.ToInt32(dr.GetDecimal(2));
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public clsProducto CargaProductoDetalleR(String Referencia, Int32 CodAlm, Int32 Caso, Int32 Lista)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProductoDetalleR", con.conector);
                cmd.Parameters.AddWithValue("refe", Referencia);
                cmd.Parameters.AddWithValue("codalm", CodAlm);
                cmd.Parameters.AddWithValue("caso", Caso);
                cmd.Parameters.AddWithValue("lista", Lista);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (Caso == 1)
                        {
                            pro = new clsProducto();
                            pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                            pro.Referencia = dr.GetString(1);
                            pro.Descripcion = dr.GetString(2);
                            pro.StockDisponible = Convert.ToDouble(dr.GetDecimal(3));
                            pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
                            pro.UnidadDescrip = dr.GetString(5);
                            pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
                            pro.ConIgv = dr.GetBoolean(7);
                            pro.Igv = dr.GetBoolean(8);
                            pro.MaxPorcDesc = dr.GetDecimal(9);
                        }
                        else
                        {
                            pro = new clsProducto();
                            pro.CodProducto = Convert.ToInt32(dr.GetDecimal(0));
                            pro.Referencia = dr.GetString(1);
                            pro.Descripcion = dr.GetString(2);
                            pro.StockDisponible = Convert.ToDouble(dr.GetDecimal(3));
                            pro.CodUnidadMedida = Convert.ToInt32(dr.GetDecimal(4));
                            pro.UnidadDescrip = dr.GetString(5);
                            pro.CodControlStock = Convert.ToInt32(dr.GetDecimal(6));
                            pro.PrecioVenta = Convert.ToDouble(dr.GetDecimal(7));
                            pro.PrecioVentaSoles = Convert.ToDouble(dr.GetDecimal(8));
                            pro.PrecioVariable = dr.GetBoolean(9);
                            pro.Oferta = dr.GetBoolean(10);
                            pro.PDescuento = Convert.ToDouble(dr.GetDecimal(11));
                            pro.PrecioOferta = Convert.ToDouble(dr.GetDecimal(12));
                            pro.ConIgv = dr.GetBoolean(13);
                            pro.Igv = dr.GetBoolean(14);
                            pro.MaxPorcDesc = dr.GetDecimal(18);
                        }
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public DataTable ListaProductos(Int32 nivel, Int32 codigo, Int32 codalmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaProductos", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nivel", nivel);
                cmd.Parameters.AddWithValue("codigo", codigo);
                cmd.Parameters.AddWithValue("codalm", codalmacen);
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


        public DataTable ListaProductosPT(Int32 nivel, Int32 codigo, Int32 codalmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaProductosPT", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nivel", nivel);
                cmd.Parameters.AddWithValue("codigo", codigo);
                cmd.Parameters.AddWithValue("codalm", codalmacen);
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





        public DataTable ListaAlmacenMatprima(Int32 nivel, Int32 codigo, Int32 codalmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaAlmacenMatprima", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nivel", nivel);
                cmd.Parameters.AddWithValue("codigo", codigo);
                cmd.Parameters.AddWithValue("codalm", codalmacen);
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


        public DataTable ListaProductosTerminados(Int32 codprod, Int32 codalm, Int32 gradosbrix)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ReporteProdTerminados", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codprod", codprod);
                cmd.Parameters.AddWithValue("codalm", codalm);
                cmd.Parameters.AddWithValue("gradosbrix", gradosbrix);
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


        public DataTable CatalogoProductos()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CatalogoProductos", con.conector);
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


        // public DataTable ListaProductosxArticulo(int tipoart )





        public DataTable BuscaProductosTerminadosReproceso(Int32 codpro, Int32 codalma, Int32 codlote)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("BuscaProductosTerminadosReproceso", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpro", codpro);
                cmd.Parameters.AddWithValue("codalma", codalma);
                cmd.Parameters.AddWithValue("codlote", codlote);
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







        public DataTable ListaProductosxArticulo(int tipoart )
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("listaProductosxArticulo", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tipoart", tipoart);
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



        public DataTable StockProductoAlmacenes(Int32 codempre, Int32 codpro)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("StockProductoxAlmacen", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpro", codpro);
                cmd.Parameters.AddWithValue("codempre", codempre);                
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

        public DataTable ListaProductosReporte(Int32 codalmacen, Int32 Tipo, Int32 Inicio)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaProductosReporte", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;               
                cmd.Parameters.AddWithValue("codalma", codalmacen);
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("inicio", Inicio);
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

        public DataTable ListaProductosMP(Int32 Tipo, Int32 CodPadre)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaProductosMP", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("codpadre", CodPadre);
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
        public DataTable RelacionProductosIngreso(Int32 Tipo, Int32 codalma)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("RelacionProductosIngreso", con.conector);
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("codalma", codalma);
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







        //RAGA
        public DataTable RelacionProductosTerminadosIngreso(Int32 Tipo, Int32 codalma)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("RelacionProductosTermIngreso", con.conector);
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("codalma", codalma);
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

        //RAGA
        public DataTable RelacionMateriaPrima(Int32 Tipo, Int32 codalma)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("RelacionProductosMatPrima", con.conector);
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("codalma", codalma);
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












        public DataTable RelacionIngresoPorProveedor(Int32 Tipo, Int32 codalma, Int32 codproveedor)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("RelacionProductosIngresoPorProveedor", con.conector);
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("codalma", codalma);
                cmd.Parameters.AddWithValue("codprov", codproveedor);
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

        public DataTable RelacionProductosSalida(Int32 Tipo,Int32 codalmacen, Int32 codlista)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("RelacionProductosSalida", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("codalma", codalmacen);
                cmd.Parameters.AddWithValue("codlista", codlista);
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

        public DataTable ListaCaracteristicas(Int32 codigo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaCaracteristicaProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;                
                cmd.Parameters.AddWithValue("codpro", codigo);
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

        public DataTable ListaNotas(Int32 codigo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaNotasProducto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpro", codigo);
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

        public DataTable BuscaProductos(Int32 Criterio, String Filtro)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("FiltraProductos", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@criterio", Criterio);
                cmd.Parameters.AddWithValue("@filtro", Filtro);
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
























        public clsProducto CargaMateriaPrimaXLote(int codLoteProduccion, int tipo)
        {

            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("CargaMateriaPrimaXLote", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", codLoteProduccion);
                cmd.Parameters.AddWithValue("codTipo", tipo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pro = new clsProducto();
                        pro.CodProducto = dr.GetInt32(0);
                      
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }


        }
















        public DataTable ArbolProductos()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaArbolProductos", con.conector);
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

        public DataTable MuestraProductosProveedor(Int32 codProducto, Int32 codAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProductosProveedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpro", codProducto);
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

      
        public clsProducto MuestraProductosTransferencia(Int32 codProducto, Int32 codAlmacen)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProductoTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpro", codProducto);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                            pro = new clsProducto();
                            pro.ValorProm = Convert.ToDouble(dr.GetDecimal(0));
                            pro.ValorPromsoles = Convert.ToDecimal(dr.GetDecimal(1));
                            pro.PrecioProm = Convert.ToDouble(dr.GetDecimal(2));
                            pro.StockActual = Convert.ToDouble(dr.GetDecimal(3));
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsProducto MuestraProductosTransferencia_nuevo(Int32 codProducto, Int32 codAlmacen)
        {
            clsProducto pro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProductoTransferencia", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpro", codProducto);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pro = new clsProducto();
                        pro.ValorProm = Convert.ToDouble(dr.GetDecimal(0));
                        pro.ValorPromsoles = Convert.ToDecimal(dr.GetDecimal(1));
                        pro.PrecioProm = Convert.ToDouble(dr.GetDecimal(2));
                        pro.StockActual = Convert.ToDouble(dr.GetDecimal(3));
                        pro.Cantidad = Convert.ToInt32(dr.GetInt32(4));
                    }
                }
                return pro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable RelacionProductosCotizacion(int Tipo, int codAlmacen, int codlista)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("RelacionProductosCotizacion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("tipo", Tipo);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                cmd.Parameters.AddWithValue("codlista", codlista);
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

        public Decimal CargaPrecioProducto(Int32 CodPro, Int32 CodAlm, Int32 codmon)
        {
            Decimal Precio = 0;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("BuscaPrecioProducto", con.conector);
                cmd.Parameters.AddWithValue("codpro", CodPro);
                cmd.Parameters.AddWithValue("codalma", CodAlm);
                cmd.Parameters.AddWithValue("mon", codmon);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Precio= Convert.ToDecimal(dr.GetDecimal(0));
                    }
                }
                return Precio;


            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable MuestraStockAlmacenes()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("consultadinamicastock", con.conector);
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

        #region Implementacion IUnidadEquivalente

        public Boolean InsertUnidad(clsUnidadEquivalente uni)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaUnidadEquivalente", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codpro", uni.CodProducto);
                oParam = cmd.Parameters.AddWithValue("coduni", uni.CodUnidad);
                oParam = cmd.Parameters.AddWithValue("factor", uni.Factor);
                oParam = cmd.Parameters.AddWithValue("codusu", uni.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                uni.CodUnidadEquivalente = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean UpdateUnidad(clsUnidadEquivalente uni)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaUnidadEquivalente", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("factor", uni.Factor);
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

        public Boolean DeleteUnidad(Int32 CodUnidad)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarUnidadEquivalente", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("coduni", CodUnidad);
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

        public clsUnidadEquivalente CargaUnidadEquivalente(Int32 Coduni, Int32 Codpro)
        {
            clsUnidadEquivalente uni = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraUnidadEquivalente", con.conector);
                cmd.Parameters.AddWithValue("coduni", Coduni);
                cmd.Parameters.AddWithValue("codpro", Codpro);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        uni = new clsUnidadEquivalente();
                        uni.CodUnidadEquivalente = Convert.ToInt32(dr.GetDecimal(0));
                        uni.CodProducto = Convert.ToInt32(dr.GetDecimal(1));
                        uni.CodUnidad = Convert.ToInt32(dr.GetDecimal(2));
                        uni.Factor = Convert.ToDouble(dr.GetDecimal(3));
                        uni.CodUser = Convert.ToInt32(dr.GetDecimal(4));
                        uni.FechaRegistro = dr.GetDateTime(5);// capturo la fecha 
                    }

                }
                return uni;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public DataTable ListaUnidadesEquivalentes(Int32 CodigoProducto)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaUnidadesEquivalentes", con.conector);
                cmd.Parameters.AddWithValue("codpro", CodigoProducto);
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

        public DataTable CargaUnidadesEquivalentes(Int32 CodigoProducto)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaUnidadesEquivalentes", con.conector);
                cmd.Parameters.AddWithValue("codpro", CodigoProducto);
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

        public DataTable BuscarProducto(Int32 codProducto)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("BuscarProducto", con.conector);
                cmd.Parameters.AddWithValue("codprod", codProducto);
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
