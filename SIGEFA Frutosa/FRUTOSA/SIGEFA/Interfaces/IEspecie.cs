using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IEspecie
    {
        Boolean Insert(clsEspecie NuevoEspecie);
        Boolean Update(clsEspecie Especie);
        Boolean Delete(Int32 Codigo);

        clsEspecie CargaEspecie(Int32 Codigo);
        DataTable ListaEspecies();
        DataTable CargaEspecies();
    }
}
