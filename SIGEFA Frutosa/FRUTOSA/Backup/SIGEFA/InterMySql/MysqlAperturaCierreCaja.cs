using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Entidades;
using SIGEFA.Conexion;
using SIGEFA.Interfaces;


namespace SIGEFA.InterMySql
{
    class MysqlAperturaCierreCaja : IAperturaCierre
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;       

        #region Implementacion IColor

        public Boolean Insert(clsAperturaCierre aper)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaApertura", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("montoape", aper.MontoApertura);
                oParam = cmd.Parameters.AddWithValue("codusu", aper.CodUser);
                oParam = cmd.Parameters.AddWithValue("codalma", aper.CodAlmacen);
                //oParam = cmd.Parameters.AddWithValue("tipoc", aper.TipoCaja);
                //oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public Boolean UpdateApertura(clsAperturaCierre aper)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizaApertura", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("mont", aper.MontoApertura);
                cmd.Parameters.AddWithValue("codalma", aper.CodAlmacen);
                int x = cmd.ExecuteNonQuery();
                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public Boolean UpdateCierre(clsAperturaCierre aper)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaCierreCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("montocie", aper.MontoCierre);
                cmd.Parameters.AddWithValue("codusu", aper.CodUser);
                cmd.Parameters.AddWithValue("codalma", aper.CodAlmacen);
                int x = cmd.ExecuteNonQuery();
                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public Boolean AnularCierre(Int32 codAlmacen)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("AnularCierreCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                int x = cmd.ExecuteNonQuery();
                if (x != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsAperturaCierre CargaAperturaCaja(Int32 codAlmacen)
        {
            clsAperturaCierre aper = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraAperturaCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("alma",codAlmacen);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        aper = new clsAperturaCierre();
                        aper.MontoApertura = Convert.ToDecimal(dr.GetDecimal(0));
                        aper.MontoCierre = Convert.ToDecimal(dr.GetDecimal(1));
                    }
                }
                return aper;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsAperturaCierre CargaCierreCaja(Int32 codAlmacen)
        {
            clsAperturaCierre aper = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraCierreCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("alma", codAlmacen);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        aper = new clsAperturaCierre();
                        aper.MontoApertura = Convert.ToDecimal(dr.GetDecimal(0));
                        aper.MontoCierre = Convert.ToDecimal(dr.GetDecimal(1));
                    }
                }
                return aper;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsAperturaCierre ValidarAperturaCierre(int codAlmacen)
        {
            clsAperturaCierre aper = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ValidarAperturaCaja", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codalma", codAlmacen);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        aper = new clsAperturaCierre();
                        aper.CodApertura = Convert.ToInt32(dr.GetDecimal(0));
                        aper.MontoApertura = Convert.ToDecimal(dr.GetDecimal(1));
                        aper.FechaRegistro = dr.GetDateTime(2);
                        aper.Estado = Convert.ToBoolean(dr.GetBoolean(3));
                        aper.CodUser = Convert.ToInt32(dr.GetDecimal(4));
                        aper.CodAlmacen = Convert.ToInt32(dr.GetDecimal(5));
                    }
                    
                }
                return aper;
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