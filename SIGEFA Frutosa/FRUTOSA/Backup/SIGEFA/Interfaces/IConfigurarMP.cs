using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DevComponents.DotNetBar.Controls;
using System.Text;
using SIGEFA.Entidades;


namespace SIGEFA.Interfaces
{
    interface IConfigurarMP
    {
        Boolean Insert(clsConfigurarMP NuevaConfigurarMP);       
        Boolean Delete(Int32 Codigo);
        
        DataTable ListaConfiguracionMP(Int32 CodPadre);
    }
}
