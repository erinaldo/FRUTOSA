using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    internal interface IFacturaVenta
    {
        Boolean insert(clsFacturaVenta factura_venta);
        Boolean update(clsFacturaVenta factura_venta);
        Boolean delete(Int32 codigoventa);
        Boolean anular(Int32 codigoventa);
        Boolean activar(Int32 codigoventa);
        Boolean rollbackfactura(Int32 codigoventa);
        Boolean insertdetalle(clsDetalleFacturaVenta detalle_venta);
        Boolean updatedetalle(clsDetalleFacturaVenta detalle_venta);
        Boolean deletedetalle(Int32 codigodetalle_venta);
        
        Boolean insertdetalleventasalida(Int32 codVen, Int32 codSalida);
        Boolean deletedetalleventasalida();
        Boolean actualizaEstadoImpreso(Int32 codVen);

        clsFacturaVenta fechaCorrelativoAnterior(Int32 codse);
        clsFacturaVenta BuscaFacturaVenta(Int32 codVenta, Int32 codAlmacen);
        clsFacturaVenta CargaFacturaVenta(Int32 codventa);

        DataTable ListaFacturaVenta(Int32 codalmacen);
        DataTable CargaDetalleVenta(Int32 codventa, Int32 codAlmacen);
        DataTable MuestraCobros(Int32 Estado, Int32 codAlmacen, DateTime Fecha1, DateTime Fecha2, Int32 codTipo);
        DataTable DocumentosPorCliente(Int32 CodCliente);
        DataTable Ventas(Int32 CodAlmacen, DateTime FechaInicio, DateTime FechaFin);
        DataTable MuestraGuiaVenta(Int32 CodAlmacen, Int32 CodCliente);
        DataTable MuestraDetalleGuiaVenta(Int32 CodAlmacen,Int32 codNota);
        DataTable MuestraDetalleGuiaVenta2(Int32 CodAlmacen);
        DataTable MuestraDetalleGuia(Int32 CodAlmacen, Int32 codNota);
        DataTable MuestraDetalleVentaGuia(Int32 codventa, Int32 codalmacen);

        Boolean VistaSucursal(Int32 codventa, Int32 valor);
        DataTable CargaDetalleVentaCredito(int codventa, int codAlmacen);
        Boolean ActualizaPendienteCreditoVenta(Double monto, Int32 codventa, Int32 codalm);
        DataTable ListaNotasDebito(Int32 CodAlmacen, DateTime fecha1, DateTime fecha2);
    }
}
