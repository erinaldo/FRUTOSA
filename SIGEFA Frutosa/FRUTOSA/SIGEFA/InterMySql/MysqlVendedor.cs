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
    class MysqlVendedor:IVendedor
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IVendedor

        public Boolean Insert(clsVendedor ven)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaVendedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("dni", ven.Dni);
                oParam = cmd.Parameters.AddWithValue("nombre", ven.Nombre);
                oParam = cmd.Parameters.AddWithValue("apellido", ven.Apellido);
                oParam = cmd.Parameters.AddWithValue("fechanac", ven.FechaNac);
                oParam = cmd.Parameters.AddWithValue("direccion", ven.Direccion);
                oParam = cmd.Parameters.AddWithValue("telefono", ven.Telefono);
                oParam = cmd.Parameters.AddWithValue("celular", ven.Celular);
                oParam = cmd.Parameters.AddWithValue("email", ven.Email);                                
                oParam = cmd.Parameters.AddWithValue("codusu", ven.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                ven.CodVendedorNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean Update(clsVendedor ven)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaVendedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codven", ven.CodVendedor);
                cmd.Parameters.AddWithValue("dni", ven.Dni);
                cmd.Parameters.AddWithValue("nombre", ven.Nombre);
                cmd.Parameters.AddWithValue("apellido", ven.Apellido);
                cmd.Parameters.AddWithValue("fechanac", ven.FechaNac);
                cmd.Parameters.AddWithValue("direccion", ven.Direccion);
                cmd.Parameters.AddWithValue("telefono", ven.Telefono);
                cmd.Parameters.AddWithValue("celular", ven.Celular);
                cmd.Parameters.AddWithValue("email", ven.Email);                
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

        public Boolean Delete(Int32 CodVendedor)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarVendedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codven", CodVendedor);               
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

        
        
        public clsVendedor CargaVendedor(Int32 Codigo)
        {
            clsVendedor ven = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraVendedor", con.conector);
                cmd.Parameters.AddWithValue("codven", Codigo);               
                cmd.CommandType = CommandType.StoredProcedure;               
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ven = new clsVendedor();
                        ven.CodVendedor = Convert.ToInt32(dr.GetDecimal(0));
                        ven.Dni = dr.GetString(1);
                        ven.Nombre = dr.GetString(2);
                        ven.Apellido = dr.GetString(3);
                        ven.FechaNac = dr.GetDateTime(4);// capturo la fecha
                        ven.Direccion = dr.GetString(5);
                        ven.Telefono = dr.GetString(6);
                        ven.Celular = dr.GetString(7);
                        ven.Email = dr.GetString(8);                        
                        ven.Estado = dr.GetBoolean(9);
                        ven.CodUser = Convert.ToInt32(dr.GetString(10));
                        ven.FechaRegistro = dr.GetDateTime(11);// capturo la fecha 
                    }

                }
                return ven;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public DataTable ListaVendedores()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaVendedores", con.conector);
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
        public DataTable ListaVendedoresDestaque()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaVendedoresDestaque", con.conector);
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
        public DataTable BuscaVendedores(Int32 Criterio, String Filtro) 
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("FiltraVendedores", con.conector);
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
        public DataTable ListaComisiones(DateTime FechaInicial, DateTime FechaFinal)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaComisiones", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("fechaini", FechaInicial);
                cmd.Parameters.AddWithValue("fechafin", FechaFinal);
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

        public DataTable MuestraComisonesFiltros(Int32 Codigo, DateTime FechaInicial, DateTime FechaFinal)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraComisonesFiltros", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codven", Codigo);
                cmd.Parameters.AddWithValue("fechaini", FechaInicial);
                cmd.Parameters.AddWithValue("fechafin", FechaFinal);
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
        public DataTable CargaVendedores()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaVendedores", con.conector);
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
        public DataTable ListarComisionesPorDocumentoFecha()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarComisionesPorDocumentoFecha", con.conector);
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
        public DataTable MuestraComisionPorDocumentoFecha(Int32 Mes, Int32 Año)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraComisionesPorDocumentoFecha", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mes", Mes);
                cmd.Parameters.AddWithValue("@año", Año);
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

        public DataTable MuestraComisionPorDocumentoPorVendedor(Int32 Mes, Int32 Año, Int32 CodVendedor)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraComisionesPorDocumentoPorVendedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("mes", Mes);
                cmd.Parameters.AddWithValue("año", Año);
                cmd.Parameters.AddWithValue("vend", CodVendedor);
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

        public DataTable MuestraComisionPorDocumentoPorVendedorZona(Int32 Mes, Int32 Año, Int32 CodVendedor, Int32 CodZona)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("MuestraComisionesPorDocumentoPorVendedorZona", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("mes", Mes);
                cmd.Parameters.AddWithValue("año", Año);
                cmd.Parameters.AddWithValue("vend", CodVendedor);
                cmd.Parameters.AddWithValue("zon", CodZona);
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

        public DataTable CargaVendedoresReportes()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaVendedoresReporte", con.conector);
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
        
        

        #endregion 

    }
}
