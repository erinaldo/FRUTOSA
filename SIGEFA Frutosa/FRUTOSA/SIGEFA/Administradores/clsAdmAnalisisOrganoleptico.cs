using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;
namespace SIGEFA.Administradores
{
    public class clsAdmAnalisisOrganoleptico
    {
        IAnalisisOrganoleptico ianalisis = new MysqlAnalisisOrganoleptico();

        public Boolean Insert(clsAnalisisOrganoleptico analisis) {
            try
            {
                return ianalisis.Insert(analisis);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
                return false;
            }
        }
        public Boolean Update(clsAnalisisOrganoleptico analisis) {
            try
            {
                return ianalisis.Update(analisis);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return false;
            }
        }
        public Boolean Delete(Int32 codanalisis) {
            try
            {
                return ianalisis.Delete(codanalisis);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return false;
            }
        }
        public clsAnalisisOrganoleptico CargarAnalisis(Int32 codanalisis) {
            try
            {
                return ianalisis.CargarAnalisis(codanalisis);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return null;
            }
        }
        public DataTable ListaAnalisis() {
            try
            {
                return ianalisis.ListaAnalisis();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return null;
            } 
        }
    }
}
