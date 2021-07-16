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
    class clsAdmOrdenCompra
    {
        IOrdenCompra MOrden = new MysqlOrdenCompra();

    public Boolean insert(clsOrdenCompra Orden)
    {
        try
        {
            return MOrden.insert(Orden);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Duplicate entry")) DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: Documento Duplicado", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    public Boolean insertdetalle(clsDetalleOrdenCompra detalle)
    {
        try
        {
            return MOrden.insertdetalle(detalle);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
    public Boolean insertdetalleConsolidado(clsDetalleConsolidado detalle)
    {
        try
        {
            return MOrden.insertdetalleConsolidado(detalle);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    public Boolean update(clsOrdenCompra Orden)
    {
        try
        {
            return MOrden.update(Orden);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    public Boolean updatedetalle(clsDetalleOrdenCompra detalle)
    {
        try
        {
            return MOrden.updatedetalle(detalle);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    //public Boolean delete(Int32 CodigoOrden)
    //{
    //    try
    //    {
    //        return MOrden.delete(CodigoOrden);
    //    }
    //    catch (Exception ex)
    //    {
    //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    //        return false;
    //    }
    //}
    public Boolean deleteConsolidado(Int32 codalma, Int32 codusu)
    {
        try
        {
            return MOrden.deleteConsolidado(codalma, codusu);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    public Boolean anular(Int32 CodigoOrden)
    {
        try
        {
            return MOrden.anular(CodigoOrden);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
    public Boolean AtenderRequerimiento(Int32 Codigo, Int32 Codorden)
    {
        try
        {
            return MOrden.AtenderRequerimiento(Codigo, Codorden);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
        //public Boolean updatesubOrdenes(Int32 suborden, Int32 codorden)
        //{
        //    try
        //    {
        //        return MOrden.updatesubOrdenes(suborden, codorden);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }
        //}
        
        //public Boolean NoAtenderorden(Int32 CodigoOrden)
        //{
        //    try
        //    {
        //        return MOrden.NoAtenderorden(CodigoOrden);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }
        //}

        //public Boolean activar(Int32 CodigoOrden)
        //{
        //    try
        //    {
        //        return MOrden.activar(CodigoOrden);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return false;
        //    }
        //}

    public Boolean atenderOrden_Nota()
    {
        try
        {
            return MOrden.atenderOrden_Nota();
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
    public Boolean deletedetalle(Int32 CodigoOrden, Int32 codpro)
    {
        try
        {
            return MOrden.deletedetalle(CodigoOrden, codpro);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    public Boolean suprimirdetalle(Int32 CodigoOrden)
    {
        try
        {
            return MOrden.suprimirdetalle(CodigoOrden);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    public clsOrdenCompra CargaOrdenCompra(Int32 CodOrden)
    {
        try
        {
            return MOrden.CargaOrdenCompra(CodOrden);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }
        //public clsOrdenCompra CargaRequerimiento(Int32 CodOrden)
        //{
        //    try
        //    {
        //        return MOrden.CargaRequerimiento(CodOrden);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}

        //public DataTable Cargaconsolidado()
        //{
        //    try
        //    {
        //        return MOrden.Cargaconsolidado();
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}
    public DataTable Cargadetalleconsolidado(Int32 alma, Int32 usu)
    {
        try
        {
            return MOrden.Cargadetalleconsolidado(alma,usu);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }
    
        //public DataTable buscaconsolidado(Int32 doc)
        //{
        //    try
        //    {
        //        return MOrden.buscaconsolidado(doc);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}

        public DataTable MuestraDetalleOrden_Nota(Int32 CodOrden)
        {
            try
            {
                return MOrden.MuestraDetalleOrden_Nota(CodOrden);
            }
            catch (Exception ex)
            {
                DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        //public DataTable CargaDetalle(Int32 CodOrden)
        //{
        //    try
        //    {
        //        return MOrden.CargaDetalle(CodOrden);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}
    public DataTable CargaDetalleOrdenCompra(Int32 CodOrden)
    {
        try
        {
            return MOrden.CargaDetalleOrdenCompra(CodOrden);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }
        //public DataTable ListaOrdenesCompra(Int32 Criterio, Int32 CodAlmacen, DateTime FechaInicial, DateTime FechaFinal)
        //{
        //    try
        //    {
        //        return MOrden.ListaOrdenesCompra(Criterio, CodAlmacen, FechaInicial, FechaFinal);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}

        //public DataTable MuestraOrdenes(Int32 CodAlmacen)
        //{
        //    try
        //    {
        //        return MOrden.ListaOrdenes(CodAlmacen);
        //    }
        //    catch (Exception ex)
        //    {
        //        DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return null;
        //    }
        //}
    public DataTable MuestraOrdenesVigentes(DateTime FechaInicial, DateTime FechaFinal, Int32 codalma)
    {
        try
        {
            return MOrden.ListaOrdenesVigentes( FechaInicial, FechaFinal, codalma);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }
    public Boolean Envio(Int32 codOrd)
    {
        try
        {
            return MOrden.envio(codOrd);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
    public Boolean rechazado(Int32 codOrd)
    {
        try
        {
            return MOrden.rechazado(codOrd);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }
    public Boolean terminada(Int32 codOrd)
    {
        try
        {
            return MOrden.terminada(codOrd);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    public Boolean aprobar(Int32 codOrd)
    {
        try
        {
            return MOrden.aprobar(codOrd);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }


    public DataTable MuestraOrdenesHistorial(DateTime FechaInicial, DateTime FechaFinal, Int32 codalma)
    {
        try
        {
            return MOrden.ListaOrdenesHistorial(FechaInicial, FechaFinal, codalma);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }
    public DataTable MuestraOrdenesVigentesParaNota(Int32 codalma, Int32 codpro)
    {
        try
        {
            return MOrden.ListaOrdenesVigentesParaNota(codalma, codpro);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }

    public clsOrdenCompra BuscaCabeceraOrden(Int32 CodOrden)
    {
        try
        {
            return MOrden.BuscaCabeceraOrden(CodOrden);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
    }

    public Boolean VistaSucursal(Int32 codigo, Int32 valor)
    {
        try
        {
            return MOrden.VistaSucursal(codigo, valor);
        }
        catch (Exception ex)
        {
            DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message,
                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
    }

    }
}
