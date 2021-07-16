﻿using System;
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
    class MysqlEmpresa:IEmpresa
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IEmpresa

        public Boolean Insert(clsEmpresa emp)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaEmpresa", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("razonsocial", emp.RazonSocial);
                oParam = cmd.Parameters.AddWithValue("ruc", emp.Ruc);
                oParam = cmd.Parameters.AddWithValue("direccion", emp.Direccion);
                oParam = cmd.Parameters.AddWithValue("telefono", emp.Telefono);
                oParam = cmd.Parameters.AddWithValue("fax", emp.Fax);
                oParam = cmd.Parameters.AddWithValue("representante", emp.Representante);
                oParam = cmd.Parameters.AddWithValue("estado", emp.Estado);
                oParam = cmd.Parameters.AddWithValue("codusu", emp.CodUser); 
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                emp.CodEmpresaNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean Update(clsEmpresa emp)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaEmpresa", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codemp", emp.CodEmpresa );
                cmd.Parameters.AddWithValue("razonsocial", emp.RazonSocial);
                cmd.Parameters.AddWithValue("ruc", emp.Ruc );
                cmd.Parameters.AddWithValue("direccion", emp.Direccion);
                cmd.Parameters.AddWithValue("telefono", emp.Telefono);
                cmd.Parameters.AddWithValue("fax", emp.Fax);
                cmd.Parameters.AddWithValue("representante", emp.Representante);
                cmd.Parameters.AddWithValue("estado", emp.Estado);                
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

        public Boolean Delete(Int32 CodEmpresa)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarEmpresa", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codemp", CodEmpresa);
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


        public clsEmpresa CargaEmpresa(Int32 Codigo)
        {
            clsEmpresa emp = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraEmpresa", con.conector);
                cmd.Parameters.AddWithValue("codempre", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        emp = new clsEmpresa();
                        emp.CodEmpresa = Convert.ToInt32(dr.GetDecimal(0));
                        emp.RazonSocial = dr.GetString(1);
                        emp.Ruc = dr.GetString(2);
                        emp.Direccion = dr.GetString(3);
                        emp.Telefono = dr.GetString(4);
                        emp.Fax = dr.GetString(5);
                        emp.Representante = dr.GetString(6);
                        emp.Estado = dr.GetBoolean(7);                       
                        emp.FechaRegistro = dr.GetDateTime(8);// capturo la fecha 
                    }

                }
                return emp;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public Boolean VerificaRUC(String RUC)
        {            
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("VerificaRUC", con.conector);
                cmd.Parameters.AddWithValue("rucingresado", RUC);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
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

        public DataTable ListaEmpresas()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaEmpresas", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(tabla);
                return tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable BuscaEmpresas(Int32 Criterio, String Filtro)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("FiltraEmpresa", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@criterio", Criterio);
                cmd.Parameters.AddWithValue("@filtro", Filtro);
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(tabla);
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable CargaEmpresas()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaEmpresas", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                adap = new MySqlDataAdapter(cmd);
                adap.Fill(tabla);
                return tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public Boolean UpdateConfiguracion(clsParametros Config)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaConfiguracion", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("config", Config.CodConfiguracion); 
                cmd.Parameters.AddWithValue("igv", Config.IGV);
                cmd.Parameters.AddWithValue("dias", Config.DiasVigencia);  
                cmd.Parameters.AddWithValue("codusu", Config.CodUser);
                cmd.Parameters.AddWithValue("venfac", Config.FacturasVencidas);
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

        public clsParametros CargaConfiguracion()
        {
            clsParametros config = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("CargaConfiguracion", con.conector);                
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        config = new clsParametros();
                        config.CodConfiguracion = Convert.ToInt32(dr.GetDecimal(0));
                        config.IGV = Convert.ToDouble(dr.GetDecimal(1));
                        config.DiasVigencia = Convert.ToInt32(dr.GetDecimal(2));
                        config.Estado = dr.GetBoolean(3);
                        config.CodUser = Convert.ToInt32(dr.GetDecimal(4));
                        config.FechaRegistro = dr.GetDateTime(5);// capturo la fecha 
                        config.FacturasVencidas = dr.GetBoolean(6);
                    }
                }
                return config;

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
