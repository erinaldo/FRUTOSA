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
    public class MysqlAreas : IAreas
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IAutorizado
        public Boolean Insert(clsAreas area)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardarAreas", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("nombre_ex", area.Nombre);
                oParam = cmd.Parameters.AddWithValue("codusu_ex", area.CodUser);
                oParam = cmd.Parameters.AddWithValue("param", area.Param);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();
                area.Codarea = Convert.ToInt32(cmd.Parameters["newid"].Value);
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
        public Boolean Update(clsAreas area)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizarAreas", con.conector);
             
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codarea_ex",area.Codarea);
                cmd.Parameters.AddWithValue("nombre_ex", area.Nombre);
                cmd.Parameters.AddWithValue("codusu_ex", area.CodUser);
                cmd.Parameters.AddWithValue("param", area.Param);
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
        public Boolean Delete(Int32 codarea,Int32 param)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("EliminarAreas", con.conector);
                cmd.Parameters.AddWithValue("param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codarea_ex", codarea);
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
        public clsAreas CargaAreas(Int32 Codigo,Int32 param)
        {
            clsAreas areas = null;
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("MuestraAreas", con.conector);
                cmd.Parameters.AddWithValue("param", param);
                cmd.Parameters.AddWithValue("codarea_ex", Codigo);
                cmd.CommandType = CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        areas = new clsAreas();
                        areas.Codarea = dr.GetInt32(0);
                        areas.Nombre = dr.GetString(1);
                        areas.CodUser = dr.GetInt32(2);
                        areas.Estado = dr.GetInt32(3);
                        areas.Fecharegistro = dr.GetDateTime(4);// capturo la fecha 
                    }

                }
                return areas;

            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public DataTable ListaAreas(Int32 param)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarAreas", con.conector);
                cmd.Parameters.AddWithValue("param", param);
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
