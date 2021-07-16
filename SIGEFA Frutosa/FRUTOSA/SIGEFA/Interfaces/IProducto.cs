using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IProducto
    {
        Boolean Insert(clsProducto ProductoNuevo);
        Boolean Update(clsProducto Producto);
        Boolean Delete(Int32 Codigo);


        DataTable ListaTotalIngProdTerm(Int32 codTran, Int32 codalmacen, DateTime finicio, DateTime ffin);

        DataTable ListaTotalSalProd(Int32 codtipoart, Int32 codalmacen, DateTime finicio, DateTime ffin);



        Boolean InsertProductoAlmacen(clsProducto ProductoAlmacenNuevo);
        Boolean UpdateProductoAlmacen(clsProducto ProductoAlmacen);
        Boolean DeleteProductoAlmacen(Int32 CodProductoAlmacen);

        Boolean InsertCaracteristica(clsCaracteristicaProducto CaracNuevo);
        Boolean DeleteCaracteristica(Int32 Codigo);
        DataTable ListaCaracteristicas(Int32 CodProducto);

        Boolean InsertNota(clsNotaProducto NotaProducto);
        Boolean DeleteNota(Int32 Codigo);
        DataTable ListaNotas(Int32 CodProducto);


        Boolean ActualizaLoteProducto(Int32 codTipo, Int32 codlote, Int32 cod);


        Boolean InsertUnidad(clsUnidadEquivalente NuevaUnidad);
        Boolean UpdateUnidad(clsUnidadEquivalente Unidad);
        Boolean DeleteUnidad(Int32 Codigo);
        clsUnidadEquivalente CargaUnidadEquivalente(Int32 Coduni, Int32 Codpro);
        DataTable ListaUnidadesEquivalentes(Int32 CodigoProducto);
        DataTable CargaUnidadesEquivalentes(Int32 CodigoProducto);


         //RAGA
        clsProducto CargaProductoDetalleIngMatPrima(Int32 CodAlm, Int32 coddetalleing);


        clsProducto CargaProducto(Int32 CodProducto, Int32 CodAlmacen);
        clsProducto CargaProductoDetalle(Int32 CodPro, Int32 CodAlm, Int32 Caso, Int32 Lista);


        clsProducto CargaStockProducto(Int32 CodPro, Int32 CodAlm, Int32 tipo);


        clsProducto CargaProductoTermDetalle(Int32 CodPro, Int32 CodAlm, Int32 Caso);
        clsProducto CargaDatosProductoOrden(Int32 CodPro, Int32 CodAlm, Int32 codusu, Decimal cant);
        clsProducto CargaProductoDetalleR(String Referencia, Int32 CodAlm, Int32 Caso, Int32 Lista);
        DataTable ListaProductos(Int32 nivel, Int32 codigo, Int32 CodAlmacen);

        DataTable ListaProductosPT(Int32 nivel, Int32 codigo, Int32 codalmacen);

        //ListaAlmacenMatprima
        DataTable ListaAlmacenMatprima(Int32 nivel, Int32 codigo, Int32 CodAlmacen);



        DataTable CatalogoProductos();

        DataTable BuscaProductosTerminadosReproceso(Int32 codpro, Int32 codalma,Int32 codlote);


        DataTable ListaProductosxArticulo(Int32 tipoart);
        DataTable ListaProductosReporte(Int32 CodAlmacen, Int32 Tipo, Int32 Inicio);
        DataTable ListaProductosMP(Int32 Tipo, Int32 CodPadre);
        DataTable RelacionProductosIngreso(Int32 Tipo, Int32 codalma);

        //RAGA
        DataTable RelacionProductosTerminadosIngreso(Int32 Tipo, Int32 codalma);

        DataTable RelacionMateriaPrima(Int32 Tipo, Int32 codalma);

        DataTable ListaProductosTerminados(Int32 codprod, Int32 codalm, Int32 gradosbrix);

        DataTable RelacionIngresoPorProveedor(Int32 Tipo, Int32 codalma, Int32 codproveedor);
        DataTable RelacionProductosSalida(Int32 Tipo, Int32 codalmacen, Int32 codlista);
        DataTable BuscaProductos(Int32 Criterio, String Filtro);


        clsProducto CargaMateriaPrimaXLote(Int32 codLoteProduccion, Int32 tipo);


        DataTable ArbolProductos();
        DataTable StockProductoAlmacenes(Int32 codEmpre, Int32 codPro);
        DataTable MuestraProductosProveedor(Int32 codProducto, Int32 codAlmacen);
        clsProducto MuestraProductosTransferencia(Int32 codProducto, Int32 codAlmacen);
        clsProducto MuestraProductosTransferencia_nuevo(Int32 codProducto, Int32 codAlmacen);
        DataTable RelacionProductosCotizacion(Int32 Tipo, Int32 codAlmacen, Int32 codlista);
        Decimal CargaPrecioProducto(Int32 CodProducto, Int32 CodAlmacen,Int32 codmon);
        DataTable MuestraStockAlmacenes();

        DataTable BuscarProducto(Int32 codProducto);
    }
}
