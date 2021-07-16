using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGEFA.Entidades;
using SIGEFA.Administradores;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SIGEFA.Administradores
{
    class clsAdmProducto
    {
        IProducto Mpro = new MysqlProducto();

        public Boolean insert(clsProducto pro)
        {
            try
            {
                return Mpro.Insert(pro);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
                else
                    DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean insertproductoalmacen(clsProducto pro)
        {
            try
            {
                return Mpro.InsertProductoAlmacen(pro);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean insertcaracteristica(clsCaracteristicaProducto carpro)
        {
            try
            {
                return Mpro.InsertCaracteristica(carpro);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean insertnota(clsNotaProducto notapro)
        {
            try
            {
                return Mpro.InsertNota(notapro);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean insertunidadequivalente(clsUnidadEquivalente unidadequi)
        {
            try
            {
                return Mpro.InsertUnidad(unidadequi);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean update(clsProducto pro)
        {
            try
            {
                return Mpro.Update(pro);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean updateproductoalmacen(clsProducto pro)
        {
            try
            {
                return Mpro.UpdateProductoAlmacen(pro);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean updateunidadequivalente(clsUnidadEquivalente unidadequi)
        {
            try
            {
                return Mpro.UpdateUnidad(unidadequi);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean delete(Int32 Codpro)
        {
            try
            {
                return Mpro.Delete(Codpro);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean deleteproductoalmacen(Int32 Codpro)
        {
            try
            {
                return Mpro.DeleteProductoAlmacen(Codpro);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean deletecaracteristica(Int32 Codcarpro)
        {
            try
            {
                return Mpro.DeleteCaracteristica(Codcarpro);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean deletenota(Int32 Codnota)
        {
            try
            {
                return Mpro.DeleteNota(Codnota);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean deleteunidadequivalente(Int32 Coduniequi)
        {
            try
            {
                return Mpro.DeleteUnidad(Coduniequi);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public DataTable MuestraProductos(Int32 Nivel, Int32 Codigo, Int32 CodAlmacen)
        {
            try
            {
                return Mpro.ListaProductos(Nivel, Codigo, CodAlmacen);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }



        public DataTable MuestraProductosPT(Int32 Nivel, Int32 Codigo, Int32 CodAlmacen)
        {
            try
            {
                return Mpro.ListaProductosPT(Nivel, Codigo, CodAlmacen);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable MuestraProductosMatPrima(Int32 Nivel, Int32 Codigo, Int32 CodAlmacen)
        {
            try
            {
                return Mpro.ListaAlmacenMatprima(Nivel, Codigo, CodAlmacen);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }




        public DataTable CatalogoProductos()
        {
            try
            {
                return Mpro.CatalogoProductos();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        //DataTable ListaProductosxArticulo(Int32 tipoart);



        public DataTable BuscaProductosTerminadosReproceso(Int32 codpro, Int32 codalma,Int32 codlote)
        {
            try
            {
                return Mpro.BuscaProductosTerminadosReproceso(codpro, codalma, codlote);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }


        }



        public DataTable ListaProductosxArticulo(Int32 tipoart)
        {
            try
            {
                return Mpro.ListaProductosxArticulo(tipoart);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        public DataTable ListaProductosReporte(Int32 CodAlmacen, Int32 Tipo, Int32 Inicio)
        {
            try
            {
                return Mpro.ListaProductosReporte(CodAlmacen,Tipo, Inicio);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable ListaProductosMP(Int32 Tipo, Int32 CodPadre)
        {
            try
            {
                return Mpro.ListaProductosMP(Tipo, CodPadre);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable StockProductoAlmacenes(Int32 CodEmpresa, Int32 CodProducto)
        {
            try
            {
                return Mpro.StockProductoAlmacenes(CodEmpresa, CodProducto);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable RelacionIngreso(Int32 Tipo, Int32 codalma)
        {
            try
            {
                return Mpro.RelacionProductosIngreso(Tipo, codalma);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        //RAGA
         public DataTable RelacionProdTermIngreso(Int32 Tipo, Int32 codalma)
        {
            try
            {
                return Mpro.RelacionProductosTerminadosIngreso(Tipo, codalma);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }



         public DataTable RelacionMateriaPrima(Int32 Tipo, Int32 codalma)
         {
             try
             {
                 return Mpro.RelacionMateriaPrima(Tipo, codalma);
             }
             catch (Exception ex)
             {
                 DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return null;
             }
         }












        //RAGA
         public DataTable ListaProductosTerminados(Int32 codprod, Int32 codalm,Int32 gradosbrix)
         {
             try
             {
                 return Mpro.ListaProductosTerminados(codprod, codalm,gradosbrix);
             }
             catch (Exception ex)
             {
                 DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return null;
             }

         }

        public DataTable RelacionIngresoPorProveedor(Int32 Tipo, Int32 codalma, Int32 codproveedor)
        {
            try
            {
                return Mpro.RelacionIngresoPorProveedor(Tipo, codalma, codproveedor);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable RelacionSalida(Int32 Tipo, Int32 CodAlmacen, Int32 CodLista)
        {
            try
            {
                return Mpro.RelacionProductosSalida(Tipo,CodAlmacen,CodLista);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsProducto CargaMateriaPrimaXLote(Int32 codLoteProduccion, Int32 tipo)
        {
            try
            {
                return Mpro.CargaMateriaPrimaXLote(codLoteProduccion, tipo);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }


        }






        public DataTable MuestraCaracteristicas(Int32 CodigoProducto)
        {
            try
            {
                return Mpro.ListaCaracteristicas(CodigoProducto);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable MuestraProductosProveedor(Int32 CodigoProducto, Int32 CodigoAlmacen)
        {
            try
            {
                return Mpro.MuestraProductosProveedor(CodigoProducto, CodigoAlmacen);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable MuestraNotas(Int32 CodigoProducto)
        {
            try
            {
                return Mpro.ListaNotas(CodigoProducto);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public Boolean ActualizaLoteProducto(Int32 codTipo, Int32 codlote, Int32 cod)
        {

            try
            {
                return Mpro.ActualizaLoteProducto(codTipo, codlote, cod);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }





        }




        public DataTable MuestraUnidadesEquivalentes(Int32 CodigoProducto)
        {
            try
            {
                return Mpro.ListaUnidadesEquivalentes(CodigoProducto);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable CargaUnidadesEquivalentes(Int32 CodigoProducto)
        {
            try
            {
                return Mpro.CargaUnidadesEquivalentes(CodigoProducto);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsUnidadEquivalente CargaUnidadEquivalente(Int32 CodigoUnidad, Int32 CodigoProducto)
        {
            try
            {
                return Mpro.CargaUnidadEquivalente(CodigoUnidad, CodigoProducto);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsProducto CargaProducto(Int32 CodProducto, Int32 CodAlmacen)
        {
            try
            {
                return Mpro.CargaProducto(CodProducto,CodAlmacen);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsProducto CargaProductoDetalle(Int32 CodProducto, Int32 CodAlmacen, Int32 Caso, Int32 CodLista)
        {
            try
            {
                return Mpro.CargaProductoDetalle(CodProducto, CodAlmacen, Caso, CodLista);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsProducto CargaProductoTermDetalle(Int32 CodProducto, Int32 CodAlmacen, Int32 Caso)
        {
            try
            {
                return Mpro.CargaProductoTermDetalle(CodProducto, CodAlmacen, Caso);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }



        //RAGA
        public  clsProducto CargaProductoDetalleIngMatPrima(Int32 CodAlm, Int32 coddetalleing)
        {
            try
            {
                return Mpro.CargaProductoDetalleIngMatPrima (CodAlm, coddetalleing);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }



        public clsProducto CargaStockProducto(Int32 CodPro, Int32 CodAlm, Int32 tipo)
        {
            try
            {
                return Mpro.CargaStockProducto(CodPro, CodAlm, tipo);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }



        }


        public clsProducto CargaDatosProductoOrden(Int32 CodProducto, Int32 CodAlmacen,Int32 codusu, Decimal cant)
        {
            try
            {
                return Mpro.CargaDatosProductoOrden(CodProducto, CodAlmacen, codusu, cant);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsProducto CargaProductoDetalleR(String Referencia, Int32 CodAlmacen, Int32 Caso, Int32 CodLista)
        {
            try
            {
                return Mpro.CargaProductoDetalleR(Referencia, CodAlmacen, Caso, CodLista);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }        

        public DataTable ArbolProductos()
        {
            try
            {
                return Mpro.ArbolProductos();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        public clsProducto MuestraProductosTransferencia(Int32 codProducto, Int32 codAlmacen)
        {
            try
            {
                return Mpro.MuestraProductosTransferencia(codProducto,codAlmacen);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        public clsProducto MuestraProductosTransferencia_nuevo(Int32 codProducto, Int32 codAlmacen)
        {
            try
            {
                return Mpro.MuestraProductosTransferencia_nuevo(codProducto, codAlmacen);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable RelacionCotizacion(Int32 Tipo, Int32 CodAlmacen, Int32 CodLista)
        {
            try
            {
                return Mpro.RelacionProductosCotizacion(Tipo, CodAlmacen, CodLista);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public Decimal CargaPrecioProducto(Int32 CodProducto, Int32 CodAlmacen, Int32 codmon)
        {
            try
            {
                return Mpro.CargaPrecioProducto(CodProducto, CodAlmacen, codmon);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }

        public DataTable MuestraStockAlmacenes()
        {
            try
            {
                return Mpro.MuestraStockAlmacenes();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable BuscarProducto(Int32 codProducto)
        {
            try
            {
                return Mpro.BuscarProducto(codProducto);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

    }
}
