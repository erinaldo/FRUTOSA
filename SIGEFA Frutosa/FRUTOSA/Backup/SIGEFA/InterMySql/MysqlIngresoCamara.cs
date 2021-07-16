using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using SIGEFA.Entidades;
using SIGEFA.Conexion;
using SIGEFA.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SIGEFA.InterMySql
{
    public class MysqlIngresoCamara : IIngresoCamara
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion de IIngresoCamara

        public Boolean insert(clsIngresoCamara IngresoCamara)
        {
            try
            {
                con.conectarBD();
                
                cmd = new MySqlCommand("GuardaIngresoCamara", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codempre", IngresoCamara.CodEmpresa);
                if (IngresoCamara.CodBahia == 0) { oParam = cmd.Parameters.AddWithValue("codbahia", null); } else { oParam = cmd.Parameters.AddWithValue("codbahia", IngresoCamara.CodBahia); };
                oParam = cmd.Parameters.AddWithValue("numorden", IngresoCamara.NumeroOrden);                
                oParam = cmd.Parameters.AddWithValue("fechaingre", IngresoCamara.FechaIngreso);                
                oParam = cmd.Parameters.AddWithValue("comentario", IngresoCamara.Comentario);
                oParam = cmd.Parameters.AddWithValue("codProveedorGarita", IngresoCamara.CodProveedorGarita);
                oParam = cmd.Parameters.AddWithValue("guia", IngresoCamara.GuiaTransporte);
                oParam = cmd.Parameters.AddWithValue("codVehiculoTransporte", IngresoCamara.CodVehiculoTransporte);
                oParam = cmd.Parameters.AddWithValue("codprocedencia", IngresoCamara.CodProcedencia);
                oParam = cmd.Parameters.AddWithValue("codespecie", IngresoCamara.CodEspecie);
                oParam = cmd.Parameters.AddWithValue("cajas", IngresoCamara.Cajas);
                oParam = cmd.Parameters.AddWithValue("pesoaprox", IngresoCamara.PesoAprox);
                byte[] areglobyte = ImagenAbyte(IngresoCamara.FotoCamion);
                oParam = cmd.Parameters.AddWithValue("fotocamion", areglobyte);
                oParam = cmd.Parameters.AddWithValue("codConductor", IngresoCamara.CodConductor);
                byte[] areglobyte2 = ImagenAbyte(IngresoCamara.FotoConductor);
                oParam = cmd.Parameters.AddWithValue("fotoconductor", areglobyte2);
                oParam = cmd.Parameters.AddWithValue("codusu", IngresoCamara.CodUser);
                oParam = cmd.Parameters.AddWithValue("horaIngreso", IngresoCamara.HoraIngreso);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                IngresoCamara.CodIngresoCamara = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean update(clsIngresoCamara IngresoCamara)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizaIngresoCamara", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codingreso", IngresoCamara.CodIngresoCamara);
                cmd.Parameters.AddWithValue("codempre", IngresoCamara.CodEmpresa);
                cmd.Parameters.AddWithValue("codbahia", IngresoCamara.CodBahia);
                cmd.Parameters.AddWithValue("numorden", IngresoCamara.NumeroOrden);
                cmd.Parameters.AddWithValue("fechaingre", IngresoCamara.FechaIngreso);
                cmd.Parameters.AddWithValue("comentario", IngresoCamara.Comentario);
                cmd.Parameters.AddWithValue("codProveedorGarita", IngresoCamara.CodProveedorGarita);
                cmd.Parameters.AddWithValue("guia", IngresoCamara.GuiaTransporte);
                cmd.Parameters.AddWithValue("codVehiculoTransporte", IngresoCamara.CodVehiculoTransporte);
                cmd.Parameters.AddWithValue("codprocedencia", IngresoCamara.CodProcedencia);
                cmd.Parameters.AddWithValue("codespecie", IngresoCamara.CodEspecie);
                cmd.Parameters.AddWithValue("cajas", IngresoCamara.Cajas);
                cmd.Parameters.AddWithValue("pesoaprox", IngresoCamara.PesoAprox);
                byte[] areglobyte = ImagenAbyte(IngresoCamara.FotoCamion);
                cmd.Parameters.AddWithValue("fotocamion", areglobyte);
                cmd.Parameters.AddWithValue("codConductor", IngresoCamara.CodConductor);
                byte[] areglobyte2 = ImagenAbyte(IngresoCamara.FotoConductor);
                cmd.Parameters.AddWithValue("fotoconductor", areglobyte2);
                cmd.Parameters.AddWithValue("horaIngreso", IngresoCamara.HoraIngreso);
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

        public Boolean UpdateSalida(clsIngresoCamara IngresoCamara)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizaIngresoCamaraHoraSalida", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codingreso",IngresoCamara.CodIngresoCamara);
                cmd.Parameters.AddWithValue("horaSalida", IngresoCamara.HoraSalida);
                cmd.Parameters.AddWithValue("fechasalida", IngresoCamara.FechaSalida);
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

        public bool RecepcionCamara(clsIngresoCamara IngresoCamara)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("RecepcionCamara", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codingreso", IngresoCamara.CodIngresoCamara);
                cmd.Parameters.AddWithValue("olor_ex", IngresoCamara.Olor);
                cmd.Parameters.AddWithValue("color_ex", IngresoCamara.Color);
                cmd.Parameters.AddWithValue("textura_ex", IngresoCamara.Textura);
                cmd.Parameters.AddWithValue("apariencia_ex", IngresoCamara.Apariencia);
                cmd.Parameters.AddWithValue("peso_ex", IngresoCamara.PesoReal);
                cmd.Parameters.AddWithValue("rechazado_ex", IngresoCamara.PesoRechazado);
                cmd.Parameters.AddWithValue("codPesador_ex",IngresoCamara.CodPesador);
                cmd.Parameters.AddWithValue("ticket", IngresoCamara.NumTicket);
                cmd.Parameters.AddWithValue("iniciodescarga_ex", IngresoCamara.Iniciodescarga);
                cmd.Parameters.AddWithValue("findescarga_ex",IngresoCamara.Findescarga);
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

        public bool updateSituacion(Int32 CodIngresoCamara, Int32 Situacion)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizaSituacionIC", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codingreso", CodIngresoCamara);
                cmd.Parameters.AddWithValue("situ", Situacion);
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

        public Boolean delete(Int32 CodigoIngresoCamara)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarIngresoCamara", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codingreso", CodigoIngresoCamara);
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

        public Boolean deleteIngresoContenedor(Int32 CodigoIngresoCamara)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarIngresoContenedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codingreso", CodigoIngresoCamara);
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
       

        public clsIngresoCamara CargaIngresoCamara(Int32 CodIngresoCamara)
        {
            clsIngresoCamara IngresoCamara = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraIngresoCamara", con.conector);
                cmd.Parameters.AddWithValue("codingreso", CodIngresoCamara);                
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IngresoCamara = new clsIngresoCamara();
                        IngresoCamara.CodIngresoCamara = dr.GetInt32(0);
                        IngresoCamara.CodEmpresa = dr.GetInt32(1);
                        if (dr[2] == DBNull.Value) { IngresoCamara.CodBahia = 0; } else { IngresoCamara.CodBahia = dr.GetInt32(2); };
                        IngresoCamara.NumeroOrden = dr.GetInt32(3);
                        IngresoCamara.FechaIngreso = dr.GetDateTime(4);
                        IngresoCamara.Comentario = dr.GetString(5);
                        IngresoCamara.CodProveedorGarita = dr.GetInt32(6);
                        IngresoCamara.GuiaTransporte = dr.GetString(7);
                        IngresoCamara.CodVehiculoTransporte = dr.GetInt32(8);
                        IngresoCamara.CodProcedencia = dr.GetInt32(9);
                        IngresoCamara.CodEspecie = dr.GetInt32(10);
                        IngresoCamara.Cajas = dr.GetInt32(11);
                        IngresoCamara.PesoAprox = dr.GetDecimal(12);
                       
                        if (dr["fotocamion"] != DBNull.Value && ((byte[])dr["fotocamion"]).Length > 1)
                        {
                            IngresoCamara.FotoCamion = ByteArrayToImage((byte[])dr["fotocamion"]);
                        }
                        IngresoCamara.CodConductor = dr.GetInt32(14);
                        if (dr["fotoconductor"] != DBNull.Value && ((byte[])dr["fotoconductor"]).Length > 1)
                        {
                            IngresoCamara.FotoConductor = ByteArrayToImage((byte[])dr["fotoconductor"]);
                        }
                        IngresoCamara.Estado = dr.GetBoolean(16);
                        IngresoCamara.Situacion = Convert.ToInt32(dr.GetString(17));
                        IngresoCamara.CodUser = Convert.ToInt32(dr.GetDecimal(18));
                        IngresoCamara.FechaRegistro = dr.GetDateTime(19);
                        IngresoCamara.HoraIngreso = dr.GetString(27);
                    }
                }
                return IngresoCamara;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }

        
        //RAGA
        public clsIngresoCamara CargaIngresoContenedor(Int32 CodIngresoContenedor)
        {
            clsIngresoCamara IngresoCamara = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraIngresoContenedor", con.conector);
                cmd.Parameters.AddWithValue("cod", CodIngresoContenedor);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IngresoCamara = new clsIngresoCamara();
                        IngresoCamara.CodIngresoCamara = dr.GetInt32(0);
                        IngresoCamara.SNumDoc = dr.GetString(1);
                        IngresoCamara.ICodProveedor = dr.GetInt32(2);
                       // IngresoCamara.Iraz = dr.GetString(1);
                        IngresoCamara.CodPesador = dr.GetInt32(4);
                        IngresoCamara.CodProcedencia = dr.GetInt32(6);
                        IngresoCamara.FechaIngreso = dr.GetDateTime(8);
                        IngresoCamara.ITipoDocumento   = dr.GetInt32(9);
                        IngresoCamara.SNumDoc = dr.GetString(10);
                        IngresoCamara.CodEspecie = dr.GetInt32(11);
                        


                        if (dr.GetDateTime(12) != null)
                        {
                            IngresoCamara.DtHora = dr.GetDateTime(12);
                        }


                        IngresoCamara.SSigla = dr.GetString(13);
                        

                        /*
                        IngresoCamara.CodEmpresa = dr.GetInt32(1);
                        if (dr[2] == DBNull.Value) { IngresoCamara.CodBahia = 0; } else { IngresoCamara.CodBahia = dr.GetInt32(2); };
                        IngresoCamara.NumeroOrden = dr.GetInt32(3);
                        IngresoCamara.FechaIngreso = dr.GetDateTime(4);
                        IngresoCamara.Comentario = dr.GetString(5);
                        IngresoCamara.CodProveedorGarita = dr.GetInt32(6);
                        IngresoCamara.GuiaTransporte = dr.GetString(7);
                        IngresoCamara.CodVehiculoTransporte = dr.GetInt32(8);
                        IngresoCamara.CodProcedencia = dr.GetInt32(9);
                        IngresoCamara.CodEspecie = dr.GetInt32(10);
                        IngresoCamara.Cajas = dr.GetInt32(11);
                        IngresoCamara.PesoAprox = dr.GetDecimal(12);

                        if (dr["fotocamion"] != DBNull.Value && ((byte[])dr["fotocamion"]).Length > 1)
                        {
                            IngresoCamara.FotoCamion = ByteArrayToImage((byte[])dr["fotocamion"]);
                        }
                        IngresoCamara.CodConductor = dr.GetInt32(14);
                        if (dr["fotoconductor"] != DBNull.Value && ((byte[])dr["fotoconductor"]).Length > 1)
                        {
                            IngresoCamara.FotoConductor = ByteArrayToImage((byte[])dr["fotoconductor"]);
                        }
                        IngresoCamara.Estado = dr.GetBoolean(16);
                        IngresoCamara.Situacion = Convert.ToInt32(dr.GetString(17));
                        IngresoCamara.CodUser = Convert.ToInt32(dr.GetDecimal(18));
                        IngresoCamara.FechaRegistro = dr.GetDateTime(19);
                        IngresoCamara.HoraIngreso = dr.GetString(27);

                        */
                    }
                }
                return IngresoCamara;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }




        public DataTable ListaIngresoCamaras(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaIngresoCamaras", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.Parameters.AddWithValue("codempre", CodEmpresa);
                cmd.Parameters.AddWithValue("situ", Situacion);
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

        public DataTable ListaRecepcionCamaras(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaIngresosMatprima", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.Parameters.AddWithValue("codempre", CodEmpresa);
                cmd.Parameters.AddWithValue("situ", Situacion);
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

       













        public static byte[] ImagenAbyte(Image Imagen)
        {
            MemoryStream memory = new MemoryStream();
            if (Imagen != null)
            {
                Imagen.Save(memory, ImageFormat.Jpeg);
            }
            else
            {
            }
            return memory.ToArray();
        }
        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            return Image.FromStream(ms);
        }

        public clsIngresoCamara CargaIngresoCamaraNotaI(Int32 CodIngresoCamara)
        {
            clsIngresoCamara IngresoCamara = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraIngresoCamaraNI", con.conector);
                cmd.Parameters.AddWithValue("codingreso", CodIngresoCamara);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IngresoCamara = new clsIngresoCamara();
                        IngresoCamara.CodIngresoCamara = dr.GetInt32(0);
                        IngresoCamara.Olor = dr.GetInt32(1);
                        IngresoCamara.Color = dr.GetInt32(2);
                        IngresoCamara.Textura = dr.GetInt32(3);
                        IngresoCamara.Apariencia = dr.GetInt32(4);
                        IngresoCamara.PesoRechazado = dr.GetDecimal(5);
                        IngresoCamara.NumTicket = dr.GetString(6);
                        IngresoCamara.HoraIngreso = dr.GetString(7);
                        IngresoCamara.HoraSalida = dr.GetString(8);
                        IngresoCamara.FechaSalida = dr.GetDateTime(9);
                        IngresoCamara.TiempoGarita = dr.GetString(10);// Tiempo total en planta.
                        IngresoCamara.CodPesador = dr.GetInt32(11);
                        IngresoCamara.Iniciodescarga = dr.GetString(12);
                        IngresoCamara.Findescarga = dr.GetString(13);
                        IngresoCamara.Tiempodescarga = dr.GetString(14);
                        IngresoCamara.Tiempoespera = dr.GetString(15);
                    }
                }
                return IngresoCamara;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }



        //RAGA

        public Boolean insertContenedor(clsIngresoCamara IngresoCamara)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaIngresoContenedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("numdoc", IngresoCamara.SNumDoc);
                oParam = cmd.Parameters.AddWithValue("codTipoDocumento", IngresoCamara.ITipoDocumento);
                oParam = cmd.Parameters.AddWithValue("fechaingreso", IngresoCamara.FechaIngreso);
                oParam = cmd.Parameters.AddWithValue("codProveedor", IngresoCamara.ICodProveedor);
                oParam = cmd.Parameters.AddWithValue("codPesador", IngresoCamara.CodPesador);
                oParam = cmd.Parameters.AddWithValue("codprocedencia", IngresoCamara.CodProcedencia);
                oParam = cmd.Parameters.AddWithValue("comentario", IngresoCamara.Comentario);
                oParam = cmd.Parameters.AddWithValue("codusu", IngresoCamara.CodUser);
                //codespecie 
                oParam = cmd.Parameters.AddWithValue("codespecie", IngresoCamara.CodEspecie);
                oParam = cmd.Parameters.AddWithValue("horaing", IngresoCamara.DtHora);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                IngresoCamara.CodIngresoCamara = Convert.ToInt32(cmd.Parameters["newid"].Value);

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


        public Boolean insertDetalleContenedor(clsDetalleIngresoCamara detalleIngresoCamara)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaDetalleIngresoContenedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                //  oParam = cmd.Parameters.AddWithValue("numdocumento", IngresoCamara.SNumDoc);
                oParam = cmd.Parameters.AddWithValue("codProducto", detalleIngresoCamara.ICodProductor);
                oParam = cmd.Parameters.AddWithValue("codIngresoContenedor", detalleIngresoCamara.ICodIngresoCamara);
                oParam = cmd.Parameters.AddWithValue("unidadingresada", detalleIngresoCamara.ICodUnidIngresada);
                oParam = cmd.Parameters.AddWithValue("cantidad", detalleIngresoCamara.ICantidad);
                oParam = cmd.Parameters.AddWithValue("cantidadDev", detalleIngresoCamara.ICantidadDev);
                oParam = cmd.Parameters.AddWithValue("cantidadTotal", detalleIngresoCamara.ICantidadTotal);
                oParam = cmd.Parameters.AddWithValue("fechaingreso", detalleIngresoCamara.DtFechaIngreso);
                oParam = cmd.Parameters.AddWithValue("codUser", detalleIngresoCamara.ICodUser);

                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
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



        //RAGA

        public DataTable ListaIngresoContenedor(DateTime FechaInicio, DateTime FechaFin, Int32 CodEmpresa, Int32 Situacion)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaIngresoContenedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("fechaini", FechaInicio);
                cmd.Parameters.AddWithValue("fechafin", FechaFin);
                cmd.Parameters.AddWithValue("codempre", CodEmpresa);
                cmd.Parameters.AddWithValue("situ", Situacion);
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

        public DataTable ListaDetalleIngresoContenedor(Int32 Cod)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaDetalleIngresoContenedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", Cod);
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


        //RAGA

        public Boolean UpdateDetalleIngContenedor(clsDetalleIngresoCamara dIngresoCamara)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("ActualizaDetalleIngContenedor", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("cod", dIngresoCamara.ICodDetalleIngresoCamara);
                cmd.Parameters.AddWithValue("coding", dIngresoCamara.ICodIngresoCamara);
                cmd.Parameters.AddWithValue("codprod", dIngresoCamara.ICodProductor);
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




        #endregion
    }
}
