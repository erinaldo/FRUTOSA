﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;

namespace SIGEFA.Administradores
{
    class clsAdmStatusCajaChica
    {
        IStatusCajaChica Msta = new MysqlStatusCajaChica();


        public clsStatusCajaChica CargaStatusFlujosCaja(DateTime FechaInicial, DateTime FechaFinal)
        {
            try
            {
                return Msta.CargaStatusFlujosCaja(FechaInicial, FechaFinal);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsStatusCajaChica CargaStatusFlujosCaja_SP(DateTime Fecha)
        {
            try
            {
                return Msta.CargaStatusFlujosCaja_SP(Fecha);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }


        public clsStatusCajaChica VerificaStadoCaja()
        {
            try
            {
                return Msta.VerificaStadoCaja();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
    }
}
