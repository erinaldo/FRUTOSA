using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGEFA.Entidades;
namespace SIGEFA.Interfaces
{
    interface IAreas
    {
        Boolean Insert(clsAreas area);
        Boolean Update(clsAreas area);
        Boolean Delete(Int32 codarea, Int32 param);
        clsAreas CargaAreas(Int32 Codigo, Int32 param);
        DataTable ListaAreas(Int32 param);
    }
}
