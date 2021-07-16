using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IOrdenCompra
    {
    Boolean insert(clsOrdenCompra Orden);
    Boolean update(clsOrdenCompra Orden);
        //Boolean delete(Int32 CodigoOrden);
    Boolean anular(Int32 CodigoOrden);
    Boolean envio(Int32 codOrd);
    Boolean rechazado(Int32 codOrd);
    Boolean terminada(Int32 codOrd);

    //raga
    Boolean aprobar(Int32 codOrd);



        //Boolean activar(Int32 CodigoOrden);
    Boolean AtenderRequerimiento(Int32 Codigo, Int32 Codorden);
    Boolean atenderOrden_Nota();
        //Boolean NoAtenderorden(Int32 CodigoOrden);

    Boolean insertdetalle(clsDetalleOrdenCompra Detalle);
    Boolean insertdetalleConsolidado(clsDetalleConsolidado Detalle);
    Boolean updatedetalle(clsDetalleOrdenCompra Detalle);
        //Boolean updatesubOrdenes(Int32 suborden, Int32 codorden);
    Boolean deletedetalle(Int32 CodigoDetalle, Int32 codpro);
    Boolean suprimirdetalle(Int32 codigo);
    Boolean deleteConsolidado(Int32 codalma, Int32 codusu);

    clsOrdenCompra CargaOrdenCompra(Int32 CodOrden);
        //clsOrdenCompra CargaRequerimiento(Int32 CodOrden);
    DataTable MuestraDetalleOrden_Nota(Int32 CodOrden);
        //DataTable CargaDetalle(Int32 CodOrden);
    DataTable CargaDetalleOrdenCompra(Int32 CodOrden);

        //DataTable Cargaconsolidado();
    DataTable Cargadetalleconsolidado(Int32 alma, Int32 usu);
        //DataTable buscaconsolidado(Int32 doc);
        //DataTable ListaOrdenesCompra(Int32 Criterio, Int32 CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);
        //DataTable ListaOrdenes(Int32 CodAlmacen);
    DataTable ListaOrdenesVigentes(DateTime FechaInicial, DateTime FechaFinal, Int32 codalma);
    DataTable ListaOrdenesHistorial(DateTime FechaInicial, DateTime FechaFinal, Int32 codalma);
    DataTable ListaOrdenesVigentesParaNota(Int32 codalma, Int32 codpro);
    clsOrdenCompra BuscaCabeceraOrden(Int32 codOrden);

    Boolean VistaSucursal(Int32 codigo, Int32 valor);
    }
}
