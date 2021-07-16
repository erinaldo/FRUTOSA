using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGEFA.Entidades;
namespace SIGEFA.Interfaces
{
    interface ICentros
    {
        Boolean Insert(clsCentroCosto centro);
        Boolean Update(clsCentroCosto centro);
        Boolean Delete(Int32 Codigo);
        clsCentroCosto CargaCentros(Int32 Codigo);
        DataTable ListaCentros(Int32 Codigo);


        DataTable ListaCentrosNivel(Int32 Codigo);
    }
}
