using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGEFA.Entidades;
namespace SIGEFA.Interfaces
{
    interface IPesador
    {
        Boolean Insert(clsPesador pesador);
        Boolean Update(clsPesador pesador);
        Boolean Delete(Int32 codpesador);
        DataTable ListaPesadores();
        clsPesador CargaPesador(Int32 Codigo);
    }
}
