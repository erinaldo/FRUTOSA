using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IBahia
    {
        Boolean Insert(clsBahia NuevoBahia);
        Boolean Update(clsBahia Bahia);
        Boolean Delete(Int32 Codigo);

        clsBahia CargaBahia(Int32 Codigo);
        DataTable ListaBahias();
        DataTable CargaBahias();
    }
}
