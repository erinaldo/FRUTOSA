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
    public class clsAdmCentro
    {
        ICentros icentro = new MysqlCentroCosto();

        public Boolean Insert(clsCentroCosto centro) {
            try
            {
                return icentro.Insert(centro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: "+ex.Message.ToString());
                return false;
            }
        }
        public Boolean Update(clsCentroCosto centro)
        {
            try
            {
                return icentro.Update(centro);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return false;
            }
        }
        public Boolean Delete(Int32 Codigo)
        {
            try
            {
                return icentro.Delete(Codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return false;
            }
        }
        public clsCentroCosto CargaCentros(Int32 Codigo)
        {
            try
            {
                return icentro.CargaCentros(Codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return null;
            }
        }
        public DataTable ListaCentros(Int32 Codigo)
        {
            try
            {
                return icentro.ListaCentros(Codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return null;
            }
        }


        public DataTable ListaCentrosNivel(Int32 Codigo)
        {
            try
            {
                return icentro.ListaCentrosNivel(Codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return null;
            }
        }
    }
}
