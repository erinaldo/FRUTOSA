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
    public class clsAdmAreas
    {
        IAreas iarea = new MysqlAreas();

        public Boolean Insert(clsAreas area) {
            try
            {
                return iarea.Insert(area);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: "+ex.Message.ToString());
                return false;
            }
        }
        public Boolean Update(clsAreas area) {
            try
            {
                return iarea.Update(area);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return false;
            }
        }
        public Boolean Delete(Int32 codarea,Int32 param) {
            try
            {
                return iarea.Delete(codarea, param);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return false;
            }
        }
        public clsAreas CargaAreas(Int32 Codigo,Int32 param) {
            try
            {
                return iarea.CargaAreas(Codigo, param);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return null;
            }
        }
        public DataTable ListaAreas(Int32 param)
        {
            try
            {
                return iarea.ListaAreas(param);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return null;
            }
        }
    }
}
