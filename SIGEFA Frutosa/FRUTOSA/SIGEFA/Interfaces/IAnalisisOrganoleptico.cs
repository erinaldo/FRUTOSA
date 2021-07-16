using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGEFA.Entidades;
namespace SIGEFA.Interfaces
{
    interface IAnalisisOrganoleptico
    {
        Boolean Insert(clsAnalisisOrganoleptico analisis);
        Boolean Update(clsAnalisisOrganoleptico analisis);
        Boolean Delete(Int32 codanalisis);
        clsAnalisisOrganoleptico CargarAnalisis(Int32 codanalisis);
        DataTable ListaAnalisis();
    }
}
