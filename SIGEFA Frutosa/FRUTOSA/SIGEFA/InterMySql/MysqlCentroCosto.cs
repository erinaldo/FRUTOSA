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
    public class MysqlCentroCosto : ICentros
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IAutorizado
        public Boolean Insert(clsCentroCosto centro)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("con_GuardaCentroCosto", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("descripcion_ex", centro.Nombre);
                oParam = cmd.Parameters.AddWithValue("nivel_ex",centro.Nivel);
                oParam = cmd.Parameters.AddWithValue("padre_ex",centro.Padre);
          //      oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();
         //       centro.Codcentro = Convert.ToInt32(cmd.Parameters["newid"].Value);
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
        public Boolean Update(clsCentroCosto centro)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("con_actualiza_centrocosto", con.conector);
             
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("descripcion_ex", centro.Nombre);
                cmd.Parameters.AddWithValue("nivel_ex", centro.Nivel);
                cmd.Parameters.AddWithValue("padre_ex", centro.Padre);
                cmd.Parameters.AddWithValue("codigocentrocosto_ex", centro.Codcentro);
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
        public Boolean Delete(Int32 Codigo)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("con_EliminarCentroCosto", con.conector);
                cmd.Parameters.AddWithValue("codigocentrocosto_ex", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
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
        public clsCentroCosto CargaCentros(Int32 Codigo)
        {
            clsCentroCosto centro = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("con_MuestraCentroCosto", con.conector);
                cmd.Parameters.AddWithValue("codigocentrocosto_ex", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        centro = new clsCentroCosto();
                        centro.Codcentro = dr.GetInt32(0);
                        centro.Nombre = dr.GetString(1);
                        centro.Nivel = dr.GetInt32(2);
                        centro.Padre = dr.GetInt32(3);
                    }

                }
                return centro;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public DataTable ListaCentros(Int32 Codigo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("con_ListaCentroCosto", con.conector);
                cmd.Parameters.AddWithValue("codpadre", Codigo);
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

        public DataTable ListaCentrosNivel(Int32 Codigo)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("con_ListaCentroCostoNivel", con.conector);
                cmd.Parameters.AddWithValue("codnivel", Codigo);
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
