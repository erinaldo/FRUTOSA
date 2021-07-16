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
    class MysqlPesador : IPesador
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

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

        public Boolean Insert(clsPesador pesador)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardarPesador", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("dni_ex", pesador.Dni);
                oParam = cmd.Parameters.AddWithValue("nombre_ex", pesador.Nombre);
                oParam = cmd.Parameters.AddWithValue("telefono_ex", pesador.Telefono);
                oParam = cmd.Parameters.AddWithValue("direccion_ex", pesador.Direccion);
                oParam = cmd.Parameters.AddWithValue("tipo_ex",pesador.Tipo);
                byte[] areglobyte = ImagenAbyte(pesador.Fotopesador);
                oParam = cmd.Parameters.AddWithValue("fotopesador_ex", areglobyte);
                oParam = cmd.Parameters.AddWithValue("codUser_ex", pesador.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();
                pesador.CodPesador = Convert.ToInt32(cmd.Parameters["newid"].Value);

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

        public Boolean Update(clsPesador pesador)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizarPesador", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codPesador_ex",pesador.CodPesador);
                cmd.Parameters.AddWithValue("dni_ex", pesador.Dni);
                cmd.Parameters.AddWithValue("nombre_ex", pesador.Nombre);
                cmd.Parameters.AddWithValue("telefono_ex", pesador.Telefono);
                cmd.Parameters.AddWithValue("direccion_ex", pesador.Direccion);
                cmd.Parameters.AddWithValue("tipo_ex", pesador.Tipo);
                byte[] areglobyte = ImagenAbyte(pesador.Fotopesador);
                cmd.Parameters.AddWithValue("fotopesador_ex", areglobyte);
                cmd.Parameters.AddWithValue("codUser_ex", pesador.CodUser);
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

        public Boolean Delete(Int32 codpesador)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("EliminarPesador", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codPes", codpesador);
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

        public DataTable ListaPesadores()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarPesador", con.conector);
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

        public clsPesador CargaPesador(Int32 Codigo)
        {
            clsPesador pesa = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraPesador", con.conector);
                cmd.Parameters.AddWithValue("codPes", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        pesa = new clsPesador();
                        pesa.CodPesador = Convert.ToInt32(dr.GetDecimal(0));
                        pesa.Dni = dr.GetString(1);
                        pesa.Nombre = dr.GetString(2);
                        pesa.Telefono = dr.GetString(3);
                        pesa.Direccion = dr.GetString(4);
                        pesa.Tipo = dr.GetInt32(5);
                        if (dr["fotopesador"] != DBNull.Value && ((byte[])dr["fotopesador"]).Length > 1)
                        {
                            pesa.Fotopesador = ByteArrayToImage((byte[])dr["fotopesador"]);
                        }
                    }

                }
                return pesa;
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
    }
}
