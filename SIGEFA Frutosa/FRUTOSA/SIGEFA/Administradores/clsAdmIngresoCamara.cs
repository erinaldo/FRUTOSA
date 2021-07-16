using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGEFA.Entidades;
using SIGEFA.Administradores;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SIGEFA.Administradores
{
    class clsAdmIngresoCamara
    {
        IIngresoCamara MIngresoCamara = new MysqlIngresoCamara();




      public  Boolean UpdateDetalleIngContenedor(clsDetalleIngresoCamara dIngresoCamara)
        {
            try
            {
                return MIngresoCamara.UpdateDetalleIngContenedor(dIngresoCamara);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }   


        public Boolean insert(clsIngresoCamara IngresoCamara)
        {
            try
            {
                return MIngresoCamara.insert(IngresoCamara);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }       

        //RAGA

        public Boolean insertContenedor(clsIngresoCamara IngresoCamara)
        {
            try
            {
                return MIngresoCamara.insertContenedor(IngresoCamara);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public  DataTable ListaIngresoContenedor(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {
                return MIngresoCamara.ListaIngresoContenedor(FechaInicio, FechaFin, CodEmpresa,Situacion);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable ListaDetalleIngresoContenedor(Int32 Cod)
        {

            try
            {
                return MIngresoCamara.ListaDetalleIngresoContenedor(Cod);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

        }





        public Boolean deleteIngresoContenedor(Int32 CodIngresoCamara)
        {
            try
            {
                return MIngresoCamara.deleteIngresoContenedor(CodIngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }



        public Boolean actualizaContenedor(clsIngresoCamara IngresoCamara)
        {
            try
            {
                return MIngresoCamara.actualizaContenedor(IngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }

        public Boolean updateDetalleContenedor(clsDetalleIngresoCamara detalleIngresoCamara)
        {

            try
            {
                return MIngresoCamara.updateDetalleContenedor(detalleIngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

        }






        public Boolean insertDetalleContenedor(clsDetalleIngresoCamara detalleIngresoCamara)
        {
            try
            {
                return MIngresoCamara.insertDetalleContenedor(detalleIngresoCamara);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }






        public Boolean update(clsIngresoCamara IngresoCamara)
        {
            try
            {
                return MIngresoCamara.update(IngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean RecepcionCamara(clsIngresoCamara IngresoCamara)
        {
            try
            {
                return MIngresoCamara.RecepcionCamara(IngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }


        public Boolean updateSituacion(Int32 CodIngresoCamara, Int32 Situacion)
        {
            try
            {
                return MIngresoCamara.updateSituacion(CodIngresoCamara, Situacion);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }



        public Boolean delete(Int32 CodIngresoCamara)
        {
            try
            {
                return MIngresoCamara.delete(CodIngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
       

        public clsIngresoCamara CargaIngresoCamara(Int32 CodIngresoCamara)
        {
            try
            {
                return MIngresoCamara.CargaIngresoCamara(CodIngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsIngresoCamara CargaIngresoContenedor(Int32 CodIngresoContenedor)
        {
            try
            {
                return MIngresoCamara.CargaIngresoContenedor(CodIngresoContenedor);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable MuestraIngresoCamaras(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {
                return MIngresoCamara.ListaIngresoCamaras(FechaInicio,FechaFin,CodEmpresa,Situacion);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable MuestraRecepcionCamaras(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {
                return MIngresoCamara.ListaRecepcionCamaras(FechaInicio, FechaFin, CodEmpresa, Situacion);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public Boolean UpdateSalida(clsIngresoCamara IngresoCamara) {
            try
            {
                return MIngresoCamara.UpdateSalida(IngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public clsIngresoCamara CargaIngresoCamaraNotaI(Int32 CodIngresoCamara) {
            try
            {
                return MIngresoCamara.CargaIngresoCamaraNotaI(CodIngresoCamara);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        
    }
}
