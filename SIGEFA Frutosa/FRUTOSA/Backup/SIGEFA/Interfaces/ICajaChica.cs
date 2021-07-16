using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface ICajaChica
    {
        Boolean Insert(clsCajaChica NuevaCajaChica);
        Boolean Update(clsCajaChica CajaChica);
        Boolean Delete(Int32 Codigo);

        DataTable ListaCajaChica(Int32 codSucursal);
        DataTable ListaCajaChicaFechas(Int32 codSucursal, DateTime fecha1, DateTime fecha2);

        clsCajaChica CargaSaldoCajaChica(Int32 codSucursal);
        clsCajaChica VerificaSaldoCajaChica(Int32 codSucursal);

        Boolean RendicionCaja(Int32 Codigo);
        DataTable ListaRendiciones(Int32 codSucursal);
        DataTable ListaRendicionesXSucursal(Int32 codSucursal);
        DataTable ListaDetalleRendiciones(Int32 Codigo);
        Boolean AnulaRendicionCajaChica(Int32 Codigo);
       
        Boolean GestionaRendicionCajaChica(clsCajaChica CajaChica);
        Boolean GestionaRendicion(Int32 Codigo, String Observacion, Int32 Tipo);
        Boolean ApruebaRendicion(Int32 Codigo);
        Boolean DesapruebaRendicion(Int32 Codigo);

        //********************************************************
        Boolean InsertRendicion(clsCajaChica NuevaCajaChica);
        Boolean InsertDetalleRendicion(clsCajaChica NuevoDetalle);
        //********************************************************
        DataTable ListaRendicionesTesoreria();
        Boolean GeneraLiquidacion(clsCajaChica CajaChica);
        Boolean InsertLiquidacion(clsCajaChica CajaChica);
        DataTable ListaLiquidacionesVigentes(Int32 CodSucursal);
        
    }
}
