using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGEFA.Entidades;
namespace SIGEFA.Interfaces
{
    interface IProveedorGarita
    {
        Boolean Insert(clsProveedorGarita prove);
        Boolean Update(clsProveedorGarita prove);
        Boolean Delete(Int32 codprove);
        DataTable ListaProveedorGarita();
        clsProveedorGarita CargaProvGarita(Int32 Codigo);
    }
}
