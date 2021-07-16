using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface ICtaCte
    {
        Boolean Insert(clsCtaCte cta);
        Boolean Update(clsCtaCte cta);
        Boolean Delete(Int32 codCtaCte, Int32 codAlmacen);

        DataTable ListaCtasBanco(Int32 CodBanco, Int32 codAlmacen);
        DataTable ListaCtaCte(Int32 codAlmacen);
        clsCtaCte CargaTipoCuenta(Int32 CodCuenta, Int32 codAlmacen);


        DataTable CargarMovxCuenta(String Cuenta, Int32 codAlmacen);
        clsCtaCte BuscaMovimiento(Int32 codMov, Int32 codAlmacen);

        Boolean InsertMovi(clsCtaCte cta);

        Boolean UpdateMovi(clsCtaCte cta);
        Boolean DeleteMov(Int32 CodMov, Int32 codAlmacen);
        DataTable ListaMovimientos(Int32 codAlmacen);

        DataTable ListatipoCtas_x_Banco(Int32 CodBanco, Int32 CodAlmacen);
        DataTable ListanumCta_x_tipocta(Int32 CodBanco, String tipocuenta, Int32 codAlmacen);
    }
}
