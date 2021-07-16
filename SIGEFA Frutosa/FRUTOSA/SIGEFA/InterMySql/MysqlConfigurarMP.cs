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
    class MysqlConfigurarMP:IConfigurarMP
    {
        clsConexionMysql con = new clsConexionMysql();
        MySqlCommand cmd = null;
        MySqlDataReader dr = null;
        MySqlDataAdapter adap = null;
        DataTable tabla = null;

        #region Implementacion IConfigurarMP

        public Boolean Insert(clsConfigurarMP car)
        {
            try
            {
                con.conectarBD();

                cmd = new MySqlCommand("GuardaConfiguracionMP", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter oParam;
                oParam = cmd.Parameters.AddWithValue("codpa", car.CodPadre);
                oParam = cmd.Parameters.AddWithValue("codhi", car.CodHijo);
                oParam = cmd.Parameters.AddWithValue("porcent", car.Porcentaje);
                oParam = cmd.Parameters.AddWithValue("codusu", car.CodUser);
                oParam = cmd.Parameters.AddWithValue("newid", 0);
                oParam.Direction = ParameterDirection.Output;
                int x = cmd.ExecuteNonQuery();

                car.CodConfigurarMPNuevo = Convert.ToInt32(cmd.Parameters["newid"].Value);

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
       

        public Boolean Delete(Int32 CodConfigurarMP)
        {
            try
            {
                con.conectarBD();
                cmd = new MySqlCommand("EliminarConfiguracionMP", con.conector);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codcon", CodConfigurarMP);
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

       
        public DataTable ListaConfiguracionMP(Int32 CodPadre)
        {
            try
            {
                tabla = new DataTable();
                con.conectarBD();
                cmd = new MySqlCommand("ListaConfiguracionMP", con.conector);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("codpa", CodPadre);
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
