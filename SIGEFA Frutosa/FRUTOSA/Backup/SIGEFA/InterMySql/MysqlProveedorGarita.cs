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
    public class MysqlProveedorGarita : IProveedorGarita
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;


        public Boolean Insert(clsProveedorGarita prove)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardarProveedorGarita", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("ruc_ex", prove.Ruc);
                oParam = cmd.Parameters.AddWithValue("razonsocial_ex", prove.Razonsocial);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();
                prove.CodProveedorGarita = Convert.ToInt32(cmd.Parameters["newid"].Value);
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

        public Boolean Update(clsProveedorGarita prove)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("ActualizarProveedorGarita", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codProv",prove.CodProveedorGarita);
                cmd.Parameters.AddWithValue("ruc_ex", prove.Ruc);
                cmd.Parameters.AddWithValue("razonsocial_ex", prove.Razonsocial);
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

        public Boolean Delete(Int32 codprove)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("EliminarProveedorGarita", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codProv", codprove);
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
        public DataTable ListaProveedorGarita()
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListarProveedorGarita", con.conector);
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

        public clsProveedorGarita CargaProvGarita(Int32 Codigo)
        {
            try
            {
                clsProveedorGarita prov = null;
                con.conectarBD();
                cmd = new MySqlCommand("MuestraProveedorGarita", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codprove", Codigo);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        prov = new clsProveedorGarita();
                        prov.CodProveedorGarita = dr.GetInt32(0);
                        prov.Ruc = dr.GetString(1);
                        prov.Razonsocial = dr.GetString(2);
                    }

                }
                return prov;
            }
            catch (MySqlException ex)
            {
                throw ex;

            }
            finally { con.conector.Dispose(); cmd.Dispose(); con.desconectarBD(); }
        }
    }
}
