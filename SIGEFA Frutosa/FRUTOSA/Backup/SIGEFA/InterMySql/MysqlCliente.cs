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
    class MysqlCliente : ICliente
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;
        String codper = null;

        #region Implementacion ICliente

        public Boolean Insert(clsCliente cli)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaCliente", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                if (cli.CodListaPrecio != 0) { oParam = cmd.Parameters.AddWithValue("codlista", cli.CodListaPrecio); } else { oParam = cmd.Parameters.AddWithValue("codlista", null); }
                if (cli.CodigoPersonalizado != "") { oParam = cmd.Parameters.AddWithValue("codper", cli.CodigoPersonalizado); } else { oParam = cmd.Parameters.AddWithValue("codper", null); }
                if (cli.CodVendedor != 0) { oParam = cmd.Parameters.AddWithValue("codven", cli.CodVendedor); } else { oParam = cmd.Parameters.AddWithValue("codven", null); }
                if (cli.Dni != "") { oParam = cmd.Parameters.AddWithValue("dni", cli.Dni); } else { oParam = cmd.Parameters.AddWithValue("dni", null); }
                oParam = cmd.Parameters.AddWithValue("nombre", cli.Nombre);
                if (cli.Ruc != "") { oParam = cmd.Parameters.AddWithValue("ruc", cli.Ruc); } else { oParam = cmd.Parameters.AddWithValue("ruc", null); }
                oParam = cmd.Parameters.AddWithValue("razonsocial", cli.RazonSocial);                
                oParam = cmd.Parameters.AddWithValue("direccionlegal", cli.DireccionLegal);
                oParam = cmd.Parameters.AddWithValue("direccionentrega", cli.DireccionEntrega);
                oParam = cmd.Parameters.AddWithValue("telefono", cli.Telefono);
                oParam = cmd.Parameters.AddWithValue("email", cli.Email);
                oParam = cmd.Parameters.AddWithValue("web", cli.Web);
                oParam = cmd.Parameters.AddWithValue("pais", cli.Pais);
                oParam = cmd.Parameters.AddWithValue("departamento", cli.Departamento);
                oParam = cmd.Parameters.AddWithValue("provincia", cli.Provincia);
                oParam = cmd.Parameters.AddWithValue("distrito", cli.Distrito);
                oParam = cmd.Parameters.AddWithValue("zona", cli.Zona);
                oParam = cmd.Parameters.AddWithValue("descuento", cli.Descuento);
                oParam = cmd.Parameters.AddWithValue("formapago", cli.FormaPago);
                oParam = cmd.Parameters.AddWithValue("moneda", cli.Moneda);                
                oParam = cmd.Parameters.AddWithValue("lineacredito", cli.LineaCredito);
                oParam = cmd.Parameters.AddWithValue("lineacreditodisponible", cli.LineaCredito);
                oParam = cmd.Parameters.AddWithValue("comentario", cli.Comentario);
                oParam = cmd.Parameters.AddWithValue("banco", cli.Banco);
                oParam = cmd.Parameters.AddWithValue("ctacte", cli.CtaCte);                
                oParam = cmd.Parameters.AddWithValue("contacto", cli.Contacto);
                oParam = cmd.Parameters.AddWithValue("telefonocontacto", cli.TelefonoContacto);
                oParam = cmd.Parameters.AddWithValue("calificacion", cli.Calificacion);                
                //oParam = cmd.Parameters.AddWithValue("estado", cli.Estado);
                oParam = cmd.Parameters.AddWithValue("codusu", cli.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                cli.CodClienteNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean Update(clsCliente cli)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizaCliente", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codcli", cli.CodCliente);
                cmd.Parameters.AddWithValue("codper", cli.CodigoPersonalizado);
                cmd.Parameters.AddWithValue("codlista", cli.CodListaPrecio);
                cmd.Parameters.AddWithValue("codven", cli.CodVendedor);
                if (cli.Dni == "") { cli.Dni = null; } 
                cmd.Parameters.AddWithValue("dni", cli.Dni);
                cmd.Parameters.AddWithValue("nombre", cli.Nombre);
                if (cli.Ruc == "") { cli.Ruc = null; }
                cmd.Parameters.AddWithValue("ruc", cli.Ruc);
                cmd.Parameters.AddWithValue("razonsocial", cli.RazonSocial);                
                cmd.Parameters.AddWithValue("direccionlegal", cli.DireccionLegal);
                cmd.Parameters.AddWithValue("direccionentrega", cli.DireccionEntrega);
                cmd.Parameters.AddWithValue("telefono", cli.Telefono);
                cmd.Parameters.AddWithValue("email", cli.Email);
                cmd.Parameters.AddWithValue("web", cli.Web);
                cmd.Parameters.AddWithValue("pais", cli.Pais);
                cmd.Parameters.AddWithValue("departamento", cli.Departamento);
                cmd.Parameters.AddWithValue("provincia", cli.Provincia);
                cmd.Parameters.AddWithValue("distrito", cli.Distrito);
                cmd.Parameters.AddWithValue("zona", cli.Zona);
                cmd.Parameters.AddWithValue("descuento", cli.Descuento);
                cmd.Parameters.AddWithValue("formapago", cli.FormaPago);
                cmd.Parameters.AddWithValue("moneda", cli.Moneda);                
                cmd.Parameters.AddWithValue("lineacredito", cli.LineaCredito);
                cmd.Parameters.AddWithValue("comentario", cli.Comentario);
                cmd.Parameters.AddWithValue("banco", cli.Banco);
                cmd.Parameters.AddWithValue("ctacte", cli.CtaCte);                
                cmd.Parameters.AddWithValue("contacto", cli.Contacto);
                cmd.Parameters.AddWithValue("telefonocontacto", cli.TelefonoContacto);
                cmd.Parameters.AddWithValue("calificacion", cli.Calificacion);
                cmd.Parameters.AddWithValue("habi", cli.Habilitado);
                cmd.Parameters.AddWithValue("facven", cli.ClienteFacturasVencidas);
                //cmd.Parameters.AddWithValue("estado", cli.Estado);
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

        public Boolean Delete(Int32 CodCliente)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarCliente", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codcli", CodCliente);
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

        public Boolean CambioHabilitado(Int32 CodCliente, Boolean Estado)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("CambiaEstadoHabilitadoCliente", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codcli", CodCliente);
                cmd.Parameters.AddWithValue("esta", Estado);
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


        public clsCliente CargaCliente(Int32 Codigo)
        {
            clsCliente cli = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraCliente", con.conector);
                cmd.Parameters.AddWithValue("codcli", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cli = new clsCliente();
                        cli.CodCliente = Convert.ToInt32(dr.GetDecimal(0));
                        //cli.Tipo = Convert.ToInt32(dr.GetDecimal(1));
                        cli.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(1));
                        cli.CodigoPersonalizado = dr.GetString(2);                        
                        cli.Nombre = dr.GetString(3);
                        cli.Dni = dr.GetString(4);
                        cli.Ruc = dr.GetString(5);
                        cli.RazonSocial = dr.GetString(6);                        
                        cli.DireccionLegal = dr.GetString(7);
                        cli.DireccionEntrega = dr.GetString(8);
                        cli.Telefono = dr.GetString(9);
                        cli.Email = dr.GetString(10);
                        cli.Web = dr.GetString(11);
                        cli.Pais = dr.GetString(12);
                        cli.Departamento = dr.GetString(13);
                        cli.Provincia = dr.GetString(14);
                        cli.Distrito = dr.GetString(15);
                        cli.Zona = Convert.ToInt32(dr.GetDecimal(16));
                        cli.Descuento = dr.GetDecimal(17);
                        cli.FormaPago = Convert.ToInt32(dr.GetDecimal(18));
                        cli.Moneda = Convert.ToInt32(dr.GetDecimal(19));
                        cli.LineaCredito = dr.GetDecimal(20);
                        cli.LineaCreditoDisponible = dr.GetDecimal(21);
                        cli.Comentario = dr.GetString(22);
                        cli.Banco = dr.GetString(23);
                        cli.CtaCte = dr.GetString(24);
                        cli.Contacto = dr.GetString(25);
                        cli.TelefonoContacto = dr.GetString(26);
                        cli.Estado = dr.GetBoolean(27);
                        cli.CodUser = Convert.ToInt32(dr.GetDecimal(28));
                        cli.FechaRegistro = dr.GetDateTime(29);// capturo la fecha 
                        cli.CodVendedor = Convert.ToInt32(dr.GetDecimal(30));
                        cli.Habilitado = dr.GetBoolean(31);
                        cli.RucDni = dr.GetString(32);
                        cli.LineaCreditoUsado = dr.GetDecimal(33);
                        cli.ClienteFacturasVencidas = dr.GetBoolean(34);
                    }

                }
                return cli;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsCliente BuscaCliente(String Filtro, Int32 Tipo)
        {
            clsCliente cli = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("BuscarClienteNota", con.conector);
                
                if (Tipo != 0)
                {
                    cmd.Parameters.AddWithValue("filtro", Filtro);
                    cmd.Parameters.AddWithValue("tipo", Tipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cli = new clsCliente();
                            cli.CodCliente = Convert.ToInt32(dr.GetDecimal(0));
                            cli.CodigoPersonalizado = dr.GetString(1);
                            cli.RazonSocial = dr.GetString(2);
                            cli.Ruc = dr.GetString(3);
                            cli.Dni = dr.GetString(4);
                            cli.DireccionLegal = dr.GetString(5);
                            cli.DireccionEntrega = dr.GetString(6);
                            cli.Tipo = Convert.ToInt32(dr.GetDecimal(7));
                            cli.FormaPago = Convert.ToInt32(dr.GetDecimal(8));
                            cli.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(9));
                            cli.Descuento = dr.GetDecimal(10);
                            cli.CodVendedor = Convert.ToInt32(dr.GetDecimal(11));
                            cli.LineaCredito = dr.GetDecimal(12);
                            cli.LineaCreditoDisponible = dr.GetDecimal(13);
                            cli.RucDni = dr.GetString(14);
                            cli.LineaCreditoUsado = dr.GetDecimal(15);
                        }
                    }
                    //return cli;
                }
                else
                {
                    Tipo = 1;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("filtro", Filtro);
                    cmd.Parameters.AddWithValue("tipo", Tipo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            cli = new clsCliente();
                            cli.CodCliente = Convert.ToInt32(dr.GetDecimal(0));
                            cli.CodigoPersonalizado = dr.GetString(1);
                            cli.RazonSocial = dr.GetString(2);
                            cli.Ruc = dr.GetString(3);
                            cli.Dni = dr.GetString(4);
                            cli.DireccionLegal = dr.GetString(5);
                            cli.DireccionEntrega = dr.GetString(6);
                            cli.Tipo = Convert.ToInt32(dr.GetDecimal(7));
                            cli.FormaPago = Convert.ToInt32(dr.GetDecimal(8));
                            cli.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(9));
                            cli.Descuento = dr.GetDecimal(10);
                            cli.CodVendedor = Convert.ToInt32(dr.GetDecimal(11));
                            cli.LineaCredito = dr.GetDecimal(12);
                            cli.LineaCreditoDisponible = dr.GetDecimal(13);
                            cli.RucDni = dr.GetString(14);
                            cli.LineaCreditoUsado = dr.GetDecimal(15);

                        }
                    }
                    else
                    {
                        Tipo = 2;
                        cmd.Parameters.Clear();
                        dr.Close();
                        cmd.Parameters.AddWithValue("filtro", Filtro);
                        cmd.Parameters.AddWithValue("tipo", Tipo);
                        cmd.CommandType = CommandType.StoredProcedure;
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                cli = new clsCliente();
                                cli.CodCliente = Convert.ToInt32(dr.GetDecimal(0));
                                cli.CodigoPersonalizado = dr.GetString(1);
                                cli.RazonSocial = dr.GetString(2);
                                cli.Ruc = dr.GetString(3);
                                cli.Dni = dr.GetString(4);
                                cli.DireccionLegal = dr.GetString(5);
                                cli.DireccionEntrega = dr.GetString(6);
                                cli.Tipo = Convert.ToInt32(dr.GetDecimal(7));
                                cli.FormaPago = Convert.ToInt32(dr.GetDecimal(8));
                                cli.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(9));
                                cli.Descuento = dr.GetDecimal(10);
                                cli.CodVendedor = Convert.ToInt32(dr.GetDecimal(11));
                                cli.LineaCredito = dr.GetDecimal(12);
                                cli.LineaCreditoDisponible = dr.GetDecimal(13);
                                cli.RucDni = dr.GetString(14);
                                cli.LineaCreditoUsado = dr.GetDecimal(15);
                            }
                        }
                        else
                        {
                            Tipo = 3;
                            cmd.Parameters.Clear();
                            dr.Close();
                            cmd.Parameters.AddWithValue("filtro", Filtro);
                            cmd.Parameters.AddWithValue("tipo", Tipo);
                            cmd.CommandType = CommandType.StoredProcedure;
                            dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    cli = new clsCliente();
                                    cli.CodCliente = Convert.ToInt32(dr.GetDecimal(0));
                                    cli.CodigoPersonalizado = dr.GetString(1);
                                    cli.RazonSocial = dr.GetString(2);
                                    cli.Ruc = dr.GetString(3);
                                    cli.Dni = dr.GetString(4);
                                    cli.DireccionLegal = dr.GetString(5);
                                    cli.DireccionEntrega = dr.GetString(6);
                                    cli.Tipo = Convert.ToInt32(dr.GetDecimal(7));
                                    cli.FormaPago = Convert.ToInt32(dr.GetDecimal(8));
                                    cli.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(9));
                                    cli.Descuento = dr.GetDecimal(10);
                                    cli.CodVendedor = Convert.ToInt32(dr.GetDecimal(11));
                                    cli.LineaCredito = dr.GetDecimal(12);
                                    cli.LineaCreditoDisponible = dr.GetDecimal(13);
                                    cli.RucDni = dr.GetString(14);
                                }
                            }
                        }
                    }
                }
                return cli;
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public DataTable ListaClientes()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaClientes", con.conector);
                //cmd.Parameters.AddWithValue("tip", Tipo);
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

        public DataTable BuscaClientes(Int32 Criterio, String Filtro)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("FiltraCliente", con.conector);
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

        public DataTable CargaClientes()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("CargaClientes", con.conector);
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

        public DataTable RelacionClientes()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("RelacionClientes", con.conector);
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
        public String CodigoPersonalizado()
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ultimoCodPersonalizado", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["codigopersonalizado"] != DBNull.Value)
                    {
                        codper = reader.GetString(0);
                    }
                    else
                    {
                        codper = "C00000";
                    }

                    //...
                }
                return codper;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }


        public clsCliente CargaDeuda(clsCliente Cliente)
        {           
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("CantidadDeudaCliente", con.conector);
                cmd.Parameters.AddWithValue("codcli", Cliente.CodCliente);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {    
                        Cliente.Cantidad = Convert.ToInt32(dr.GetDecimal(0));
                        Cliente.Deuda = dr.GetDecimal(1);                                             
                    }

                }
                return Cliente;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsCliente CargaFacturasVencidas(clsCliente cliente)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ConsultarFacturasVencidas", con.conector);
                cmd.Parameters.AddWithValue("codcli", cliente.CodCliente);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cliente.Cantidad = Convert.ToInt32(dr.GetDecimal(0));
                        cliente.Deuda = dr.GetDecimal(1);
                    }

                }
                return cliente;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        public clsCliente MuestraClienteNota(int CodigoCli)
        {
            clsCliente cli = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraClienteNota", con.conector);
                cmd.Parameters.AddWithValue("codcli", CodigoCli);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        cli = new clsCliente();
                        cli.CodCliente = Convert.ToInt32(dr.GetDecimal(0));
                        //cli.Tipo = Convert.ToInt32(dr.GetDecimal(1));
                        cli.CodListaPrecio = Convert.ToInt32(dr.GetDecimal(1));
                        cli.CodigoPersonalizado = dr.GetString(2);
                        cli.Dni = dr.GetString(3);
                        cli.Nombre = dr.GetString(4);
                        cli.Ruc = dr.GetString(5);
                        cli.RazonSocial = dr.GetString(6);
                        cli.DireccionLegal = dr.GetString(7);
                        cli.DireccionEntrega = dr.GetString(8);
                        cli.Telefono = dr.GetString(9);
                        cli.Email = dr.GetString(10);
                        cli.Web = dr.GetString(11);
                        cli.Pais = dr.GetString(12);
                        cli.Departamento = dr.GetString(13);
                        cli.Provincia = dr.GetString(14);
                        cli.Distrito = dr.GetString(15);
                        cli.Zona = Convert.ToInt32(dr.GetDecimal(16));
                        cli.Descuento = dr.GetDecimal(17);
                        cli.FormaPago = Convert.ToInt32(dr.GetDecimal(18));
                        cli.Moneda = Convert.ToInt32(dr.GetDecimal(19));
                        cli.LineaCredito = dr.GetDecimal(20);
                        cli.Comentario = dr.GetString(21);
                        cli.Banco = dr.GetString(22);
                        cli.CtaCte = dr.GetString(23);
                        cli.Contacto = dr.GetString(24);
                        cli.TelefonoContacto = dr.GetString(25);
                        cli.Estado = dr.GetBoolean(26);
                        cli.CodUser = Convert.ToInt32(dr.GetDecimal(27));
                        cli.FechaRegistro = dr.GetDateTime(28);// capturo la fecha 
                        cli.CodVendedor = Convert.ToInt32(dr.GetDecimal(29));
                        cli.Habilitado = dr.GetBoolean(30);
                    }

                }
                return cli;

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
