using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface ITransferencia
    {
        Boolean insert(clsTransferencia transferencia);
        Boolean update(clsTransferencia transferencia);
        Boolean anular(Int32 codigotransferencia);
        Boolean atender(Int32 codigotransferencia);

        Boolean insertdetalle(clsDetalleTransferencia Detalle);

        Boolean updatedetalle(clsDetalleTransferencia Detalle);

        Boolean deletedetalle(Int32 CodigoDetalle);

        clsTransferencia CargaTransferencia(Int32 codigotransferencia);

        DataTable CargaDetalle(Int32 codigotransferencia);

        //DataTable Cargaconsolidado();

        //DataTable cargaRequerimientosTotales(Int32 alma);

        DataTable MuestraTransferenciasVigentes(Int32 CodAlmacen);
    }
}
