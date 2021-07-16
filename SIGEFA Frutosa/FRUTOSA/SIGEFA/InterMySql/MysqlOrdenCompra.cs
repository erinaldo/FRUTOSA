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
    class MysqlOrdenCompra : IOrdenCompra
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region IOrdenCompra Members

    public bool insert(clsOrdenCompra Orden)
    {
        try
        {
            con.conectarBD();

            cmd = new MySqlCommand("GuardaOrdenCompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlParameter oParam;
            oParam = cmd.Parameters.AddWithValue("codalma", Orden.CodAlmacen);
            if (Orden.CodProveedor != 0) { oParam = cmd.Parameters.AddWithValue("codprov", Orden.CodProveedor); } else { oParam = cmd.Parameters.AddWithValue("codprov", null); }
            oParam = cmd.Parameters.AddWithValue("coment", Orden.Comentario);
            oParam = cmd.Parameters.AddWithValue("codTipoDoc", Orden.CodTipoDocumento);
            oParam = cmd.Parameters.AddWithValue("codse", Orden.CodSerie);
            oParam = cmd.Parameters.AddWithValue("num", Orden.NumDoc);
            oParam = cmd.Parameters.AddWithValue("fechaord", Orden.FechaOrden);
            oParam = cmd.Parameters.AddWithValue("codUsu", Orden.CodUser);
            oParam = cmd.Parameters.AddWithValue("mon", Orden.Moneda);
            oParam = cmd.Parameters.AddWithValue("tipoc", Orden.Tipocambio);
            oParam = cmd.Parameters.AddWithValue("brut", Orden.Bruto);
            oParam = cmd.Parameters.AddWithValue("montod", Orden.Montodesc);
            oParam = cmd.Parameters.AddWithValue("igv", Orden.Igv);
            oParam = cmd.Parameters.AddWithValue("total", Orden.Total);
            oParam = cmd.Parameters.AddWithValue("formap", Orden.Formapago);
            oParam = cmd.Parameters.AddWithValue("fechap", Orden.Fechapago);
            oParam = cmd.Parameters.AddWithValue("flete", Orden.Flete);

            oParam = cmd.Parameters.AddWithValue("newid", 0);
            oParam.Direction = ParameterDirection.Output;
            int x = cmd.ExecuteNonQuery();

            Orden.CodOrdenCompraNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

    public bool update(clsOrdenCompra Orden)
    {
        try
        {
            con.conectarBD();

            cmd = new MySqlCommand("ActualizaOrdenCompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("comentario", Orden.Comentario);
            cmd.Parameters.AddWithValue("fechaorden", Orden.FechaOrden);
            cmd.Parameters.AddWithValue("codUsuario", Orden.CodUser);
            cmd.Parameters.AddWithValue("codFormaP", Orden.Formapago);
            cmd.Parameters.AddWithValue("codMon", Orden.Moneda);
            cmd.Parameters.AddWithValue("flet", Orden.Flete);
            cmd.Parameters.AddWithValue("fechap", Orden.Fechapago);
            cmd.Parameters.AddWithValue("codOrd", Convert.ToInt32(Orden.CodOrdenCompra));
            cmd.Parameters.AddWithValue("tipoc", Orden.Tipocambio);
            cmd.Parameters.AddWithValue("brut", Orden.Bruto);
            cmd.Parameters.AddWithValue("montod", Orden.Montodesc);
            cmd.Parameters.AddWithValue("igv", Orden.Igv);
            cmd.Parameters.AddWithValue("total", Orden.Total);
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

        //public bool delete(int CodigoOrden)
        //{
        //    try
        //    {
        //        con.conectarBD();
        //        cmd = new MySqlCommand("EliminarOrdenCompra", con.conector);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("codOrd", CodigoOrden);
        //        int x = cmd.ExecuteNonQuery();
        //        if (x != 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        throw ex;

        //    }
        //    finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        //}
    public bool deleteConsolidado(Int32 codalma, Int32 codusu)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("EliminarConsolidado", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codalma", codalma);
            cmd.Parameters.AddWithValue("codusu", codusu);
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
    public bool anular(int CodigoOrden)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("AnularOrdenCompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", CodigoOrden);
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
    public bool atenderOrden_Nota()
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("AtendidaOrdenCompra_Nota", con.conector);
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
    public bool AtenderRequerimiento(int Codigo,Int32 Codorden)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("AtendidoRequerimiento", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", Codigo);
            cmd.Parameters.AddWithValue("orde", Codorden);
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
        //public bool NoAtenderorden(int CodigoOrden)
        //{
        //    try
        //    {
        //        con.conectarBD();
        //        cmd = new MySqlCommand("NoAtendidoOrdenCompra", con.conector);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("codOrd", CodigoOrden);
        //        int x = cmd.ExecuteNonQuery();
        //        if (x != 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        throw ex;

        //    }
        //    finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        //}

        //public bool activar(int CodigoOrden)
        //{
        //    try
        //    {
        //        con.conectarBD();
        //        cmd = new MySqlCommand("ActivarOrdenCompra", con.conector);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("codOrd", CodigoOrden);
        //        int x = cmd.ExecuteNonQuery();
        //        if (x != 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        throw ex;

        //    }
        //    finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        //}

    public bool insertdetalle(clsDetalleOrdenCompra Detalle)
    {
        try
        {
            con.conectarBD();

            cmd = new MySqlCommand("GuardaDetalleOrden", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlParameter oParam;
            oParam = cmd.Parameters.AddWithValue("codProducto", Detalle.CodProducto);
            oParam = cmd.Parameters.AddWithValue("codOrdenCompra", Detalle.CodOrdenCompra);
            oParam = cmd.Parameters.AddWithValue("unidad", Detalle.Unidad);
            oParam = cmd.Parameters.AddWithValue("cantidad", Detalle.Cantidad);
            oParam = cmd.Parameters.AddWithValue("codUser", Detalle.CodUser);
            oParam = cmd.Parameters.AddWithValue("codAlm", Detalle.CodAlmacen);
            oParam = cmd.Parameters.AddWithValue("mon", Detalle.Moneda);
            oParam = cmd.Parameters.AddWithValue("preciou", Detalle.Preciounitario);
            oParam = cmd.Parameters.AddWithValue("subt", Detalle.Subtotal);
            oParam = cmd.Parameters.AddWithValue("dscto1", Detalle.Dscto1);
            oParam = cmd.Parameters.AddWithValue("dscto2", Detalle.Dscto2);
            oParam = cmd.Parameters.AddWithValue("dscto3", Detalle.Dscto3);
            oParam = cmd.Parameters.AddWithValue("montdes", Detalle.Montodscto);
            oParam = cmd.Parameters.AddWithValue("igv", Detalle.Igv);
            oParam = cmd.Parameters.AddWithValue("impor", Detalle.Importe);
            oParam = cmd.Parameters.AddWithValue("precio", Detalle.Precioreal);
            oParam = cmd.Parameters.AddWithValue("valor", Detalle.Valorreal);
            oParam = cmd.Parameters.AddWithValue("cantidadreq", Detalle.CantidadPendiente);
            oParam = cmd.Parameters.AddWithValue("flete", Detalle.Flete);
            oParam = cmd.Parameters.AddWithValue("newid", 0);
            oParam.Direction = ParameterDirection.Output;
            int x = cmd.ExecuteNonQuery();

            Detalle.CodDetalleOrdenCompra = Convert.ToInt32(cmd.Parameters["newid"].Value);

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
    public bool insertdetalleConsolidado(clsDetalleConsolidado Detalle)
    {
        try
        {
            con.conectarBD();

            cmd = new MySqlCommand("GuardaDetalleConsolidado", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            MySqlParameter oParam;
            oParam = cmd.Parameters.AddWithValue("coddeta", Detalle.CodDetalle);
            oParam = cmd.Parameters.AddWithValue("codprod", Detalle.CodProducto);
            oParam = cmd.Parameters.AddWithValue("cant", Detalle.Cantidad);
            oParam = cmd.Parameters.AddWithValue("alma", Detalle.CodAlmacen);
            oParam = cmd.Parameters.AddWithValue("usu", Detalle.CodUsuario);
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

    public bool updatedetalle(clsDetalleOrdenCompra Detalle)
    {
        try
        {
            con.conectarBD();

            cmd = new MySqlCommand("ActualizaDetalleOrden", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codProducto", Detalle.CodProducto);
            cmd.Parameters.AddWithValue("cant", Detalle.Cantidad);
            cmd.Parameters.AddWithValue("codusu", Detalle.CodUser);
            cmd.Parameters.AddWithValue("mon", Detalle.Moneda);
            cmd.Parameters.AddWithValue("pu", Detalle.Preciounitario);
            cmd.Parameters.AddWithValue("subtot", Detalle.Subtotal);
            cmd.Parameters.AddWithValue("d1", Detalle.Dscto1);
            cmd.Parameters.AddWithValue("d2", Detalle.Dscto2);
            cmd.Parameters.AddWithValue("d3", Detalle.Dscto3);
            cmd.Parameters.AddWithValue("montd", Detalle.Montodscto);
            cmd.Parameters.AddWithValue("igv", Detalle.Igv);
            cmd.Parameters.AddWithValue("impor", Detalle.Importe);
            cmd.Parameters.AddWithValue("precior", Detalle.Precioreal);
            cmd.Parameters.AddWithValue("valorr", Detalle.Valorreal);
            cmd.Parameters.AddWithValue("flet", Detalle.Flete);
            cmd.Parameters.AddWithValue("codDet", Detalle.CodDetalleOrdenCompra);
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

        //public bool updatesubOrdenes(Int32 suborden,Int32 codorden)
        //{
        //    try
        //    {
        //        con.conectarBD();

        //        cmd = new MySqlCommand("ActualizaSubOrdenCompra", con.conector);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("subordencompra", suborden);
        //        cmd.Parameters.AddWithValue("codOrd", codorden);
        //        int x = cmd.ExecuteNonQuery();
        //        if (x != 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (MySqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        //}

    public bool deletedetalle(int CodigoDetalle, Int32 codpro)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("EliminarDetalleOrden", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("coddeta", CodigoDetalle);
            cmd.Parameters.AddWithValue("codpro", codpro);
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

    public bool suprimirdetalle(int codigo)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("suprimirdetalle", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codigo", codigo);
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

        //public clsOrdenCompra CargaRequerimiento(int CodOrden)
        //{
        //    clsOrdenCompra orden = null;
        //    try
        //    {
        //        con.conectarBD();
        //        cmd = new MySqlCommand("BuscaRequerimiento", con.conector);
        //        cmd.Parameters.AddWithValue("codOrd", CodOrden);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        dr = cmd.ExecuteReader();
        //        if (dr.HasRows)
        //        {
        //            while (dr.Read())
        //            {
        //                orden = new clsOrdenCompra();
        //                orden.CodOrdenCompra = dr.GetInt32(0);
        //                orden.CodAlmacen = dr.GetInt32(2);
        //                orden.CodProveedor = Convert.ToInt32(dr.GetString(4));
        //                orden.RUCProveedor = dr.GetString(5);
        //                orden.RazonSocialProveedor = dr.GetString(6);
        //                orden.Tipo = dr.GetInt32(7);
        //                orden.CodTipoDocumento = dr.GetInt32(8);
        //                orden.SiglaDocumento = dr.GetString(9);
        //                orden.DescripcionDocumento = dr.GetString(10);
        //                orden.CodSerie = dr.GetInt32(11);
        //                orden.Serie = dr.GetString(12);
        //                orden.NumDoc = Convert.ToInt32(dr.GetString(13));

        //                orden.FechaOrden = dr.GetDateTime(14);
        //                orden.Comentario = dr.GetString(15);

        //            }
        //        }
        //        return orden;

        //    }
        //    catch (MySqlException ex)
        //    {
        //        throw ex;

        //    }
        //    finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        //}
    public clsOrdenCompra CargaOrdenCompra(int CodOrden)
    {
        clsOrdenCompra orden = null;
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("BuscaOrdencompra", con.conector);
            cmd.Parameters.AddWithValue("codOrd", CodOrden);
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    orden = new clsOrdenCompra();
                    orden.CodOrdenCompra = dr.GetInt32(0);
                    orden.CodAlmacen = dr.GetInt32(2);
                    orden.CodProveedor = Convert.ToInt32(dr.GetString(4));
                    orden.RUCProveedor = dr.GetString(5);
                    orden.RazonSocialProveedor = dr.GetString(6);
                    orden.CodTipoDocumento = dr.GetInt32(7);
                    orden.SiglaDocumento = dr.GetString(8);
                    orden.DescripcionDocumento = dr.GetString(9);
                    orden.CodSerie = dr.GetInt32(10);
                    orden.Serie = dr.GetString(11);
                    orden.NumDoc = Convert.ToInt32(dr.GetString(12));
                    orden.FechaOrden = dr.GetDateTime(13);
                    orden.Comentario = dr.GetString(14);
                    orden.Moneda =Convert.ToInt32(dr.GetString(20));
                    orden.Tipocambio =Convert.ToDecimal(dr.GetString(21));
                    orden.Bruto =Convert.ToDecimal(dr.GetString(22));
                    orden.Montodesc =Convert.ToDecimal(dr.GetString(23));
                    orden.Igv =Convert.ToDecimal(dr.GetString(24));
                    orden.Total = Convert.ToDecimal(dr.GetString(25));
                    orden.Formapago =Convert.ToInt32(dr.GetString(26));
                    orden.Fechapago = Convert.ToDateTime(dr.GetString(27));
                    orden.Flete = Convert.ToDecimal(dr.GetString(28));
                }
            }
            return orden;

        }
        catch (MySqlException ex)
        {
            throw ex;

        }
        finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
    }
        //public DataTable Cargaconsolidado()
        //{
        //    try
        //    {
        //        tabla = new DataTable();
        //        con.conectarBD();
        //        cmd = new MySqlCommand("CargaConsolidadoOrden", con.conector);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        adap = new MySqlDataAdapter(cmd);
        //        adap.Fill(tabla);
        //        return tabla;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        //}

    public DataTable Cargadetalleconsolidado(Int32 alma, Int32 usu)
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("CargaConsolidado", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("alma", alma);
            cmd.Parameters.AddWithValue("usu", usu);
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
    
    public DataTable MuestraDetalleOrden_Nota(int codOrden)
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("MuestraDetalleOrden_Nota", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", codOrden);
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
    public DataTable CargaDetalle(int coddetalle)
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("BuscaDetalleRequerimiento", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", coddetalle);
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
    public DataTable CargaDetalleOrdenCompra(Int32 codOrden)
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("BuscaDetalleOrdenCompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", codOrden);
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
    public DataTable CargaConsolidado()
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("CargaConsolidado", con.conector);
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

        //public DataTable ListaOrdenesCompra(int Criterio, int CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
        //{
        //    try
        //    {
        //        tabla = new DataTable();
        //        con.conectarBD();
        //        cmd = new MySqlCommand("ListaOrdenes", con.conector);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("criterio", Criterio);
        //        cmd.Parameters.AddWithValue("codalma", CodAlmacen);
        //        cmd.Parameters.AddWithValue("fechaini", FechaInicial);
        //        cmd.Parameters.AddWithValue("fechafin", FechaFinal);
        //        adap = new MySqlDataAdapter(cmd);
        //        adap.Fill(tabla);
        //        return tabla;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        //}

        public DataTable ListaOrdenes(Int32 CodAlmacen)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("BuscaRequerimientoPorSucursal", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codOrd", CodAlmacen);
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
    public DataTable ListaOrdenesVigentes(DateTime FechaInicial, DateTime FechaFinal, Int32 codalma)
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("BuscaOrdenCompraVigentes", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("fechaini", FechaInicial);
            cmd.Parameters.AddWithValue("fechafin", FechaFinal);
            cmd.Parameters.AddWithValue("codalma", codalma);
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
    public bool envio(int codOrd)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("enviarOrdenCompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cod", codOrd);
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
    public bool rechazado(int codOrd)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("rechazarordencompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", codOrd);
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
    public bool terminada(int codOrd)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("terminarordencompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", codOrd);
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



        //RAGA
    public bool aprobar(int codOrd)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("aprobarordencompra", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codOrd", codOrd);
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


    public DataTable ListaOrdenesHistorial(DateTime FechaInicial, DateTime FechaFinal,Int32 codalma)
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("BuscaOrdenCompraHistorial", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("fechaini", FechaInicial);
            cmd.Parameters.AddWithValue("fechafin", FechaFinal);
            cmd.Parameters.AddWithValue("codalma", codalma);
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

    public DataTable ListaOrdenesVigentesParaNota(Int32 codalma,Int32 codpro)
    {
        try
        {
            tabla = new DataTable();
            con.conectarBD();
            cmd = new MySqlCommand("BuscaOrdenCompraVigentesParaNota", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("codalma", codalma);
            cmd.Parameters.AddWithValue("codpro", codpro);
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
    public clsOrdenCompra BuscaCabeceraOrden(int CodOrden)
    {
        clsOrdenCompra orden = null;
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("BuscaCabeceraOrden", con.conector);
            cmd.Parameters.AddWithValue("codOrd", CodOrden);
            cmd.CommandType = CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    orden = new clsOrdenCompra();
                    orden.Moneda = Convert.ToInt32(dr.GetString(0));
                    orden.Tipocambio = Convert.ToDecimal(dr.GetString(1));
                    orden.Formapago = Convert.ToInt32(dr.GetString(2));
                    orden.Fechapago = Convert.ToDateTime(dr.GetString(3));
                }
            }
            return orden;

        }
        catch (MySqlException ex)
        {
            throw ex;

        }
        finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
    }

    public Boolean VistaSucursal(int codigo, Int32 valor)
    {
        try
        {
            con.conectarBD();
            cmd = new MySqlCommand("VistaOrden", con.conector);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cod", codigo);
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


        #endregion
    }
}
