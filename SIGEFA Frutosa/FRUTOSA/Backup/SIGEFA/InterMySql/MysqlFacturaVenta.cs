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
    public class MysqlFacturaVenta : IFacturaVenta
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion de IFacturaVenta

        public bool insert(clsFacturaVenta factura_venta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codSu", factura_venta.CodSucursal);
                oParam = cmd.Parameters.AddWithValue("codalma", factura_venta.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("codtran", factura_venta.CodTipoTransaccion);
                oParam = cmd.Parameters.AddWithValue("codtipo", factura_venta.CodTipoDocumento);
                oParam = cmd.Parameters.AddWithValue("codserie", factura_venta.CodSerie);
                oParam = cmd.Parameters.AddWithValue("serie", factura_venta.Serie);
                oParam = cmd.Parameters.AddWithValue("numdoc", factura_venta.NumDoc);
                oParam = cmd.Parameters.AddWithValue("tipocliente", factura_venta.TipoCliente);
                if (factura_venta.CodCliente != 0)
                {
                    oParam = cmd.Parameters.AddWithValue("codcli", factura_venta.CodCliente);
                }
                else
                {
                    oParam = cmd.Parameters.AddWithValue("codcli", null);
                }
                oParam = cmd.Parameters.AddWithValue("moneda", factura_venta.Moneda);
                oParam = cmd.Parameters.AddWithValue("codlista", factura_venta.CodListaPrecio);
                oParam = cmd.Parameters.AddWithValue("tipocambio", factura_venta.TipoCambio);
                oParam = cmd.Parameters.AddWithValue("fechasalida", factura_venta.FechaSalida);
                oParam = cmd.Parameters.AddWithValue("comentario", factura_venta.Comentario);
                oParam = cmd.Parameters.AddWithValue("bruto", factura_venta.MontoBruto);
                oParam = cmd.Parameters.AddWithValue("montodscto", factura_venta.MontoDscto);
                oParam = cmd.Parameters.AddWithValue("igv", factura_venta.Igv);
                oParam = cmd.Parameters.AddWithValue("total", factura_venta.Total);
                oParam = cmd.Parameters.AddWithValue("pendiente", factura_venta.Total);
                oParam = cmd.Parameters.AddWithValue("estado", factura_venta.Estado);
                oParam = cmd.Parameters.AddWithValue("formapago", factura_venta.FormaPago);
                oParam = cmd.Parameters.AddWithValue("fechapago", factura_venta.FechaPago);
                oParam = cmd.Parameters.AddWithValue("codven", factura_venta.CodVendedor);
                oParam = cmd.Parameters.AddWithValue("codCoti", factura_venta.CodCotizacion);
                oParam = cmd.Parameters.AddWithValue("codusu", factura_venta.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                if (factura_venta.DocumentoReferencia != null) { cmd.Parameters.AddWithValue("docreferencia", factura_venta.DocumentoReferencia); }
                else { cmd.Parameters.AddWithValue("docreferencia", null); }
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                factura_venta.CodFacturaVenta = Convert.ToString(cmd.Parameters["newid"].Value);

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

        public bool update(clsFacturaVenta factura_venta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", Convert.ToInt32(factura_venta.CodFacturaVenta));
                cmd.Parameters.AddWithValue("codalma", factura_venta.CodAlmacen);
                cmd.Parameters.AddWithValue("codtran", factura_venta.CodTipoTransaccion);
                cmd.Parameters.AddWithValue("codtipo", factura_venta.CodTipoDocumento);
                cmd.Parameters.AddWithValue("codserie", factura_venta.CodSerie);
                cmd.Parameters.AddWithValue("serie", factura_venta.Serie);
                cmd.Parameters.AddWithValue("numdoc", factura_venta.NumDoc);
                cmd.Parameters.AddWithValue("tipocliente", factura_venta.TipoCliente);
                if (factura_venta.CodCliente != 0)
                {
                    cmd.Parameters.AddWithValue("codcli", factura_venta.CodCliente);
                }
                else
                {
                    cmd.Parameters.AddWithValue("codcli", null);
                }
                cmd.Parameters.AddWithValue("moneda", factura_venta.Moneda);
                cmd.Parameters.AddWithValue("codlista", factura_venta.CodListaPrecio);
                cmd.Parameters.AddWithValue("tipocambio", factura_venta.TipoCambio);
                cmd.Parameters.AddWithValue("fechasalida", factura_venta.FechaSalida);
                cmd.Parameters.AddWithValue("comentario", factura_venta.Comentario);
                cmd.Parameters.AddWithValue("bruto", factura_venta.MontoBruto);
                cmd.Parameters.AddWithValue("montodscto", factura_venta.MontoDscto);
                cmd.Parameters.AddWithValue("igv", factura_venta.Igv);
                cmd.Parameters.AddWithValue("total", factura_venta.Total);
                cmd.Parameters.AddWithValue("estado", factura_venta.Estado);
                cmd.Parameters.AddWithValue("formapago", factura_venta.FormaPago);
                cmd.Parameters.AddWithValue("fechapago", factura_venta.FechaPago);
                cmd.Parameters.AddWithValue("codcredito", factura_venta.CodNotaCredito);
                cmd.Parameters.AddWithValue("documento", factura_venta.DocumentoReferencia);
                cmd.Parameters.AddWithValue("codven", factura_venta.CodVendedor);
                
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
        public bool rollbackfactura(int codigoventa)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("rollbackfactura", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codigoventa);
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

        public bool delete(int codigoventa)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codigoventa);
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

        public bool anular(int codigoventa)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("AnularFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codigoventa);
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

        public bool activar(int codigoventa)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActivarFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codigoventa);
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

        public bool insertdetalle(clsDetalleFacturaVenta detalle_venta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaDetalleFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codpro", detalle_venta.CodProducto);
                oParam = cmd.Parameters.AddWithValue("codventa", detalle_venta.CodVenta);
                oParam = cmd.Parameters.AddWithValue("codalma", detalle_venta.CodAlmacen);
                oParam = cmd.Parameters.AddWithValue("unidad", detalle_venta.UnidadIngresada);
                oParam = cmd.Parameters.AddWithValue("serielote", detalle_venta.SerieLote);
                oParam = cmd.Parameters.AddWithValue("cantidad", detalle_venta.Cantidad);
                oParam = cmd.Parameters.AddWithValue("cantidadp", detalle_venta.CantidadPendiente);
                oParam = cmd.Parameters.AddWithValue("moneda", detalle_venta.Moneda);
                oParam = cmd.Parameters.AddWithValue("precio", detalle_venta.PrecioUnitario);
                oParam = cmd.Parameters.AddWithValue("subtotal", detalle_venta.Subtotal);
                oParam = cmd.Parameters.AddWithValue("dscto1", detalle_venta.Descuento1);
                oParam = cmd.Parameters.AddWithValue("dscto2", detalle_venta.Descuento2);
                oParam = cmd.Parameters.AddWithValue("dscto3", detalle_venta.Descuento3);
                oParam = cmd.Parameters.AddWithValue("montodscto", detalle_venta.MontoDescuento);
                oParam = cmd.Parameters.AddWithValue("igv", detalle_venta.Igv);
                oParam = cmd.Parameters.AddWithValue("importe", detalle_venta.Importe);
                oParam = cmd.Parameters.AddWithValue("precioreal", detalle_venta.PrecioReal);
                oParam = cmd.Parameters.AddWithValue("valoreal", detalle_venta.ValoReal);
                oParam = cmd.Parameters.AddWithValue("codDetaCoti", detalle_venta.CodDetalleCotizacion);
                oParam = cmd.Parameters.AddWithValue("codusu", detalle_venta.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                detalle_venta.CodDetalleVenta = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public bool updatedetalle(clsDetalleFacturaVenta detalle_venta)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaDetalleFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("coddeta", detalle_venta.CodDetalleVenta);
                cmd.Parameters.AddWithValue("unidad", detalle_venta.UnidadIngresada);
                cmd.Parameters.AddWithValue("serielote", detalle_venta.SerieLote);
                cmd.Parameters.AddWithValue("cantidad", detalle_venta.Cantidad);
                cmd.Parameters.AddWithValue("precio", detalle_venta.PrecioUnitario);
                cmd.Parameters.AddWithValue("subtotal", detalle_venta.Subtotal);
                cmd.Parameters.AddWithValue("dscto1", detalle_venta.Descuento1);
                cmd.Parameters.AddWithValue("dscto2", detalle_venta.Descuento2);
                cmd.Parameters.AddWithValue("dscto3", detalle_venta.Descuento3);
                cmd.Parameters.AddWithValue("montodscto", detalle_venta.MontoDescuento);
                cmd.Parameters.AddWithValue("igv", detalle_venta.Igv);
                cmd.Parameters.AddWithValue("importe", detalle_venta.Importe);
                cmd.Parameters.AddWithValue("precioreal", detalle_venta.PrecioReal);
                cmd.Parameters.AddWithValue("valoreal", detalle_venta.ValoReal);
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

        public bool deletedetalle(int codigodetalle_venta)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarDetalleFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("coddeta", codigodetalle_venta);
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
        }

        public bool deletedetalleventasalida()
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarDetalleVentaSalida", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
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

        public bool actualizaEstadoImpreso(int codVen)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizarVentaImpresa", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("venta", codVen);
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

        public clsFacturaVenta fechaCorrelativoAnterior(Int32 codserie)
        {
            clsFacturaVenta factura = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("BuscaFechaAnteriorSerie", con.conector);
                cmd.Parameters.AddWithValue("codse", codserie);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        factura = new clsFacturaVenta();
                        factura.FechaSalida = dr.GetDateTime(0);
                    }
                }
                return factura;
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsFacturaVenta BuscaFacturaVenta(Int32 codVenta, Int32 codAlmacen)
        {
            clsFacturaVenta factura = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("BuscaFacturaVentaxDocumento", con.conector);
                cmd.Parameters.AddWithValue("codventa", codVenta);
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        factura = new clsFacturaVenta();
                        factura.CodFacturaVenta = dr.GetString(0);
                        factura.CodAlmacen = dr.GetInt32(1);
                        factura.CodTipoTransaccion = dr.GetInt32(2);
                        factura.SiglaTransaccion = dr.GetString(3);
                        factura.DescripcionTransaccion = dr.GetString(4);
                        factura.CodTipoDocumento = dr.GetInt32(5);
                        factura.SiglaDocumento = dr.GetString(6);
                        factura.CodSerie = dr.GetInt32(7);
                        factura.Serie = dr.GetString(8);
                        factura.NumDoc = dr.GetString(9);
                        factura.TipoCliente = dr.GetInt32(10); ;
                        factura.CodCliente = dr.GetInt32(11); ;
                        factura.DNI = dr.GetString(12);
                        factura.RUCCliente = dr.GetString(13);
                        factura.CodigoPersonalizado = dr.GetString(14);
                        factura.RazonSocialCliente = dr.GetString(15);
                        factura.Nombre = dr.GetString(16);
                        factura.Direccion = dr.GetString(17);
                        factura.Moneda = dr.GetInt32(18);
                        factura.TipoCambio = dr.GetDouble(19);
                        factura.FechaSalida = dr.GetDateTime(20);
                        factura.Comentario = dr.GetString(21);
                        factura.MontoBruto = dr.GetDouble(22);
                        factura.MontoDscto = dr.GetDouble(23);
                        factura.Igv = dr.GetDouble(24);
                        factura.Total = dr.GetDouble(25);
                        factura.Estado = dr.GetInt32(26);
                        factura.FormaPago = dr.GetInt32(27);
                        factura.FechaPago = dr.GetDateTime(28);
                        factura.CodUser = dr.GetInt32(29);
                        factura.FechaRegistro = dr.GetDateTime(30);
                    }
                }
                return factura;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsFacturaVenta CargaFacturaVenta(int codventa)
        {
            clsFacturaVenta factura = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraFacturaVenta", con.conector);
                cmd.Parameters.AddWithValue("codventa", codventa);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        factura = new clsFacturaVenta();
                        factura.CodFacturaVenta = dr.GetString(0);
                        factura.CodAlmacen = dr.GetInt32(1);
                        factura.CodTipoTransaccion = dr.GetInt32(2);
                        factura.SiglaTransaccion = dr.GetString(3);
                        factura.DescripcionTransaccion = dr.GetString(4);
                        factura.CodTipoDocumento = dr.GetInt32(5);
                        factura.SiglaDocumento = dr.GetString(6);
                        factura.CodSerie = dr.GetInt32(7);
                        factura.Serie = dr.GetString(8);
                        factura.NumDoc = dr.GetString(9);
                        factura.TipoCliente = dr.GetInt32(10); ;
                        factura.CodCliente = dr.GetInt32(11); ;
                        factura.DNI = dr.GetString(12);
                        factura.RUCCliente = dr.GetString(13);
                        factura.CodigoPersonalizado = dr.GetString(14);
                        factura.RazonSocialCliente = dr.GetString(15);
                        factura.Nombre = dr.GetString(16);
                        factura.Direccion = dr.GetString(17);
                        factura.Moneda = dr.GetInt32(18);
                        factura.TipoCambio = dr.GetDouble(19);
                        factura.FechaSalida = dr.GetDateTime(20);
                        factura.Comentario = dr.GetString(21);
                        factura.MontoBruto = dr.GetDouble(22);
                        factura.MontoDscto = dr.GetDouble(23);
                        factura.Igv = dr.GetDouble(24);
                        factura.Total = dr.GetDouble(25);
                        factura.Estado = dr.GetInt32(26);
                        factura.FormaPago = dr.GetInt32(27);
                        factura.FechaPago = dr.GetDateTime(28);
                        factura.CodUser = dr.GetInt32(29);
                        factura.FechaRegistro = dr.GetDateTime(30);
                        factura.Pendiente = dr.GetDouble(31);
                        factura.CodNotaCredito = dr.GetInt32(32);
                        factura.DocumentoReferencia = dr.GetString(33);
                        factura.CodVendedor = dr.GetInt32(34);
                        factura.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(35));
                        factura.LineaCreditoCliente = dr.GetDouble(36);
                        factura.CodCotizacion = Convert.ToInt32(dr.GetDecimal(37));
                    }
                }
                return factura;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable ListaFacturaVenta(int codalmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", codalmacen);
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

        public DataTable CargaDetalleVenta(int codventa, int codAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraDetalleFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codventa);
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

        

        public DataTable MuestraCobros(int Estado, int codAlmacen, DateTime Fecha1, DateTime Fecha2, int codTipo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraCobrosFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("esta", Estado);
                cmd.Parameters.AddWithValue("alma", codAlmacen);
                cmd.Parameters.AddWithValue("fecha1", Fecha1);
                cmd.Parameters.AddWithValue("fecha2", Fecha2);
                cmd.Parameters.AddWithValue("codtipo", codTipo);
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

        public DataTable DocumentosPorCliente(int CodCliente)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaDocumentosPorCliente_FV", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codcli", CodCliente);
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

        public DataTable Ventas(int CodAlmacen, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraVentas_FV", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
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

        public DataTable MuestraGuiaVenta(int CodAlmacen, int CodCliente)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraGuiasFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
                cmd.Parameters.AddWithValue("codcliente", CodCliente);
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

        public DataTable MuestraDetalleGuiaVenta(int CodAlmacen, Int32 codNota)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraDetalleGuiaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
                cmd.Parameters.AddWithValue("codSalida", codNota);
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

        public DataTable MuestraDetalleGuiaVenta2(int CodAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraDetalleGuiaVenta2", con.conector);
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

        public DataTable MuestraDetalleGuia(int CodAlmacen, int codNota)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraDetalleGuiaVenta2", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
                cmd.Parameters.AddWithValue("codnota", CodAlmacen);
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

        public Boolean insertdetalleventasalida(Int32 codVen, Int32 codSalida)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaDetalleVentaSalida", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codVen", codVen);
                oParam = cmd.Parameters.AddWithValue("codSalida", codSalida);
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

        public DataTable MuestraDetalleVentaGuia(int codventa, int codalmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaDetalleFacturaVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codventa);
                cmd.Parameters.AddWithValue("codalma", codalmacen);
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

        public Boolean VistaSucursal(int codigoventa, Int32 valor)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("VistaSucursal", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codigoventa);
                cmd.Parameters.AddWithValue("valor", valor);
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

        public DataTable CargaDetalleVentaCredito(int codventa, int codAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaDetalleVentaCredito", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codventa", codventa);
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


        public Boolean ActualizaPendienteCreditoVenta(Double monto, Int32 codventa, Int32 codalm)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizaPendienteCreditoVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("monto", monto);
                cmd.Parameters.AddWithValue("codventa", codventa);
                cmd.Parameters.AddWithValue("codalm", codalm);
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

        public DataTable ListaNotasDebito(Int32 CodAlmacen, DateTime fecha1, DateTime fecha2)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaNotaDebitoVenta", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", CodAlmacen);
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


        #endregion
    }
}
