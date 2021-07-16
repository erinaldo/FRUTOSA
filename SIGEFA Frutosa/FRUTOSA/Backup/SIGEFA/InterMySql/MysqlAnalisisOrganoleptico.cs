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
    class MysqlAnalisisOrganoleptico : IAnalisisOrganoleptico
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        public Boolean Insert(clsAnalisisOrganoleptico analisis)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardarAnalisisOrganoleptico", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("nombre_ex", analisis.Nombre);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();
                analisis.Codanalisis= Convert.ToInt32(cmd.Parameters["newid"].Value);
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
        public Boolean Update(clsAnalisisOrganoleptico analisis)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizarAnalisisOrganoleptico", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codanalisis_ex",analisis.Codanalisis);
                cmd.Parameters.AddWithValue("nombre_ex", analisis.Nombre);
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
        public Boolean Delete(Int32 codanalisis)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarAnalisisOrganoleptico", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codanalisis_ex", codanalisis);
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
        public clsAnalisisOrganoleptico CargarAnalisis(Int32 codanalisis)
        {
            try
            {
                clsAnalisisOrganoleptico ana = null;
                con.conectarBD();
                cmd = new MySqlCommand("EliminarAnalisisOrganoleptico", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codanalisis_ex", codanalisis);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ana = new clsAnalisisOrganoleptico();
                        ana.Codanalisis = dr.GetInt32(0);
                        ana.Nombre = dr.GetString(1);
                        ana.Estado = dr.GetInt32(2);
                        ana.Fecharegistro = dr.GetDateTime(3);
                    }
                }
                return ana;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
        public DataTable ListaAnalisis()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarAnalisisOrganoleptico", con.conector);
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
        
    }
}
