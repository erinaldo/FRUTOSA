using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IPago
    {
        Boolean Insert(clsPago NuevoPago);
        DataTable MuestraListaPagosNota(Int32 CodNotaIngreso, Boolean InOut, Int32 Tipo);
        Boolean AnularPago(Int32 CodigoPago);
        clsPago MuestraPagoVenta(Int32 codAlmacen,Int32 venta);
        DataTable MuestraPagosPorAprobar(Int32 Estado, DateTime Fecha1, DateTime Fecha2);
        Boolean AprobarPago(Int32 codigo, Int32 valor);
        DataTable MuestraListaPagosNota2(Int32 CodNotaIngreso, Boolean InOut, Int32 Tipo);
    }
}
