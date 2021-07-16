using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IAperturaCierre
    {
        Boolean Insert(clsAperturaCierre AperturaNuevo);
        Boolean UpdateApertura(clsAperturaCierre Apertura);
        Boolean UpdateCierre(clsAperturaCierre Cierre);

        Boolean AnularCierre(Int32 codAlmacen);

        clsAperturaCierre CargaAperturaCaja(Int32 codAlmacen);
        clsAperturaCierre CargaCierreCaja(Int32 codAlmacen);
        clsAperturaCierre ValidarAperturaCierre(Int32 codAlmacen);
    }
}
