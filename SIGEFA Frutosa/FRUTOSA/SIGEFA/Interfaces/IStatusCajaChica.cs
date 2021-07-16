using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    interface IStatusCajaChica
    {
        clsStatusCajaChica CargaStatusFlujosCaja(DateTime FechaInicio, DateTime FechaFin);
        clsStatusCajaChica CargaStatusFlujosCaja_SP(DateTime Fecha);

        clsStatusCajaChica VerificaStadoCaja();
    }
}
