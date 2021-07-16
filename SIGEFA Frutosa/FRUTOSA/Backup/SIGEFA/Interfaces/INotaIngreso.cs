using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface INotaIngreso
    {   
        Boolean insert(clsNotaIngreso Nota);

        Boolean update(clsNotaIngreso Nota);
        Boolean ActualizaCantidadPendiente(Double cantidad, Int32 produc, Int32 CodOrden);
        Boolean ActualizaCantidadPendiente2(Double cantidad, Int32 produc, Int32 alma, Int32 coduser);
        Boolean ActualizaCodNotaIngreso(Double cantidad, Int32 produc, Int32 CodDetalle , Int32 tipo);
        Boolean delete(Int32 CodigoNota);
        Boolean anular(Int32 CodSerie, String NumDoc);
        Boolean anular1(Int32 codigo);
        Boolean activar(Int32 CodigoNota);
        Boolean atender( Int32 codigo, Int32 CodSerie,  String NumDoc ,Int32 User);


        Boolean insertdetalle(clsDetalleNotaIngreso Detalle);





        Boolean insertdetalleConsolidado(Int32 orden,Int32 nota, Int32 codAlma, Int32 codUsu);
        Boolean updatedetalle(clsDetalleNotaIngreso Detalle);        
        Boolean deletedetalle(Int32 CodigoDetalle);
        Boolean deleteConsolidado(Int32 codalma, Int32 codusu);
        clsNotaIngreso CargaNotaIngreso(Int32 CodNota);
        //clsDetalleNotaIngreso ListaOrdenAlmacen(Int32 codAlmacen, Int32 codNota);
        //List<clsDetalleNotaIngreso> GuardaDetalleOrdenAlmacen(Int32 codAlmacen, Int32 codNota);
        DataTable CargaDetalle(Int32 CodNota);
        DataTable ListaNotasIngreso(Int32 Criterio, Int32 CodAlmacen,DateTime FechaInicial, DateTime FechaFinal);
       
        
        //RAGA
        DataTable ListaCompras(Int32 Criterio, Int32 CodAlmacen, DateTime FechaInicial, DateTime FechaFinal);
        
        
        
        
        DataTable ListaNotasCredito(Int32 CodAlmacen, DateTime fecha1, DateTime fecha2);
        DataTable MuestraPagos(Int32 Estado, Int32 codEmpresa, DateTime Fecha1, DateTime Fecha2);
        DataTable MuestraOrdenAlmacen(Int32 codAlmacen, Int32 codUsu);
        DataTable  MuestraNotaIngresoOrden(Int32 codAlmacen);


        //RAGA
        DataTable MuestraNotaIngresosSinOrden(Int32 codAlmacen);


        DataTable MuestraTransferenciasVigentes(Int32 CodAlmacen);
        DataTable CargaDetalleTransferencia(Int32 codigotransferencia);
        Boolean UpdateComentario(clsNotaIngreso Nota);
        DataTable MuestraGuia(Int32 codAlmacen, Int32 codUsu);

        DataTable CargaNotaCreditoVentaSinAplicar(Int32 Codcli);
        Boolean ActualizaNCreditoVentaSinAplicar(clsNotaIngreso nota);
        Boolean VerificarNCVentaAplicada(clsNotaIngreso nota);
    }
}
