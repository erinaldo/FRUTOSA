using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces
{
    internal interface IIngresoCamara
    {
        Boolean insert(clsIngresoCamara IngresoCamara);


        //raga
        Boolean insertContenedor(clsIngresoCamara IngresoCamara);
        Boolean insertDetalleContenedor(clsDetalleIngresoCamara detalleIngresoCamara);
        DataTable ListaIngresoContenedor(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion);

        DataTable ListaDetalleIngresoContenedor(Int32 Cod);

        Boolean UpdateDetalleIngContenedor(clsDetalleIngresoCamara dIngresoCamara);


        //

        Boolean update(clsIngresoCamara IngresoCamara);
        Boolean UpdateSalida(clsIngresoCamara IngresoCamara);
        Boolean RecepcionCamara(clsIngresoCamara IngresoCamara);
        Boolean updateSituacion(Int32 CodIngresoCamara, Int32 Situacion);
        Boolean delete(Int32 CodigoIngresoCamara);


        Boolean deleteIngresoContenedor(Int32 CodigoIngresoCamara);


        clsIngresoCamara CargaIngresoCamara(Int32 CodIngresoCamara);

        clsIngresoCamara CargaIngresoContenedor(Int32 CodIngresoContenedor);


        DataTable ListaIngresoCamaras(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion);
        DataTable ListaRecepcionCamaras(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion);
        clsIngresoCamara CargaIngresoCamaraNotaI(Int32 CodIngresoCamara);
       
    }
}
