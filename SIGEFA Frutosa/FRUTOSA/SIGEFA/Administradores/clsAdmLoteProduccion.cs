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
    class clsAdmLoteProduccion
    {
        ILoteProduccion MProduccion = new MysqlLoteProduccion();

        public Boolean insert(clsLoteProduccion lote)
        {
            try
            {
                return MProduccion.insert(lote);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        public Boolean ActualizarEstado(Int32 codLote, Int32 estado)
        {
            try
            {
                return MProduccion.ActualizarEstado(codLote, estado);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public Boolean ActualizarFechayHoraFinal(Int32 codLote, DateTime fechafinal, DateTime horafinal)
        {
            try
            {
                return MProduccion.ActualizarFechayHoraFinal(codLote, fechafinal, horafinal);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public DataTable CargaLoteProduccion()
        {
            try
            {
                return MProduccion.CargaLoteProduccion();
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public clsLoteProduccion CargaLote(Int32 CodigoLote)
        {
            try
            {
                return MProduccion.CargaLote(CodigoLote);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }





        //RAGA

        public decimal GetRendimientoCosto(DateTime fecha1, DateTime fecha2, Int32 CodProducto, Int32 CodConcepto)
        {

            try
            {
                return MProduccion.GetRendimientoCosto(fecha1, fecha2, CodProducto, CodConcepto);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }


        }





        public DataTable CargaLoteProduccionPorEstado(int estado,DateTime fecha1, DateTime fecha2)
        {
            try
            {
                return MProduccion.CargaLoteProduccionPorEstado(estado, fecha1, fecha2);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        //RAGA

        public DataTable MuestraLoteMPSalProd(int codLote)
        {

            try
            {
                return MProduccion.MuestraLoteMPSalProd(codLote);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

        }

        public DataTable CargaDetalleIngresoPorLote(int cod, int codTipo)
        {
            try
            {
                return MProduccion.CargaDetalleIngresoPorLote(cod, codTipo);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public Boolean Delete(Int32 codLote)
        {
            try
            {
                return MProduccion.Delete(codLote);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                else DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        public DataTable CargaDetalleProductosXLote(int codLoteProduccion, int tipo)
        {
            try
            {
                return MProduccion.CargaDetalleProductosXLote(codLoteProduccion, tipo);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        public DataTable CargaProductoTerminadoXlote(int codLoteProduccion, int tipo)
        {
            try
            {
                return MProduccion.CargaProductoTerminadoXlote(codLoteProduccion, tipo);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }




        public DataTable CargaProductoTerminadoXReproceso(int codLoteProduccion, int tipo, int tipobatch)
        {
            try
            {
                return MProduccion.CargaProductoTerminadoXReproceso(codLoteProduccion, tipo, tipobatch);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }



        }






    }
}
