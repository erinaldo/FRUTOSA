using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IFlujoCaja
    {
        Boolean Insert(clsFlujoCaja flu);
        Boolean Update(clsFlujoCaja flu);
        Boolean Delete(Int32 CodFlujoCaja, Int32 CodAlmacen);

        clsFlujoCaja CargaFlujosCaja(Int32 CodFlujoCaja, Int32 CodAlmacen);
        DataTable ListaFlujosCaja(Int32 CodAlmacen);
        DataTable ListaPagoCobro();
        
    }
}
