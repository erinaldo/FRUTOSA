using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SIGEFA.Conexion;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;

namespace SIGEFA.InterMySql
{
    class MysqlStatusCajaChica : IStatusCajaChica
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;


        #region Implementacion IFlujoCaja

        public clsStatusCajaChica CargaStatusFlujosCaja(DateTime FechaInicio, DateTime FechaFin)
        {
            clsStatusCajaChica sta = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ReporteStatusCajaChica", con.conector);
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sta = new clsStatusCajaChica();
                        sta.AperturaCaja = dr.GetDecimal(0);
                        sta.Ingresos = dr.GetDecimal(1);
                        sta.Egresos = dr.GetDecimal(2);
                        sta.TotalVentas = dr.GetDecimal(3);
                        sta.PorCobrar = dr.GetDecimal(4);
                        sta.TotalPagos = dr.GetDecimal(5);
                        sta.PorPagar = dr.GetDecimal(6);
                        sta.SumaAperturasCaja = dr.GetDecimal(7);
                        sta.SumaCierresCaja = dr.GetDecimal(8);

                    }

                }
                return sta;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsStatusCajaChica CargaStatusFlujosCaja_SP(DateTime Fecha)
        {
            clsStatusCajaChica sta = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ReporteStatusCajaChica_SP", con.conector);
                cmd.Parameters.AddWithValue("fec", Fecha);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sta = new clsStatusCajaChica();
                        sta.AperturaCaja = dr.GetDecimal(0);
                        sta.Ingresos = dr.GetDecimal(1);
                        sta.Egresos = dr.GetDecimal(2);
                        sta.TotalVentas = dr.GetDecimal(3);
                        sta.PorCobrar = dr.GetDecimal(4);
                        sta.TotalPagos = dr.GetDecimal(5);
                        sta.PorPagar = dr.GetDecimal(6);

                    }

                }
                return sta;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsStatusCajaChica VerificaStadoCaja()
        {
            clsStatusCajaChica sta = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("VerificaStatusCajaChica", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        sta = new clsStatusCajaChica();
                        sta.Cantidad = dr.GetInt32(0);
                    }

                }
                return sta;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }


        #endregion
    }
}
