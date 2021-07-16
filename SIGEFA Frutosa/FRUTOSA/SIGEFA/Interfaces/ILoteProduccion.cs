using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    internal interface ILoteProduccion    
    {
        DataTable CargaLoteProduccion();


        //RAGA
        DataTable MuestraLoteMPSalProd(int codLote);

        DataTable CargaLoteProduccionPorEstado(int estado, DateTime fecha1, DateTime fecha2);
        DataTable CargaDetalleIngresoPorLote(int codLoteProduccion, int tipo);
        Boolean insert(clsLoteProduccion lote);
        Boolean ActualizarEstado(Int32 codLote, Int32 estado);
        clsLoteProduccion CargaLote(Int32 Codigo);

        //RAGA
        decimal GetRendimientoCosto(DateTime fecha1, DateTime fecha2, Int32 CodProducto, Int32 CodConcepto);


        Boolean ActualizarFechayHoraFinal(Int32 codLote, DateTime fechafinal, DateTime horafinal);
        Boolean Delete(Int32 codLote);
        DataTable CargaDetalleProductosXLote(int codLoteProduccion, int tipo);
        DataTable CargaProductoTerminadoXlote(int codLoteProduccion, int tipo);

        DataTable CargaProductoTerminadoXReproceso(int codLoteProduccion, int tipo, int tipobatch);


    }
}
