using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SIGEFA.Entidades;
using SIGEFA.Interfaces;
using SIGEFA.InterMySql;
using System.Windows.Forms;
namespace SIGEFA.Administradores
{
    public class clsAdmProveedorGarita
    {
        IProveedorGarita iprove = new MysqlProveedorGarita();

        public Boolean Insert(clsProveedorGarita prove) {
            try
            {
                return iprove.Insert(prove);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return false;
            }
        }
        public Boolean Update(clsProveedorGarita prove) {
            try
            {
                return iprove.Update(prove);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return false;
            }
        }
        public Boolean Delete(Int32 codprove) {
            try
            {
                return iprove.Delete(codprove);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return false;
            }
        }


        public DataTable ListaProveedorGarita() {
            try
            {
                return iprove.ListaProveedorGarita();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
                return null;
            }
        }

        public clsProveedorGarita CargaProvGarita(Int32 Codigo) {
            try
            {
                return iprove.CargaProvGarita(Codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
                return null;
            }
        }
    }
}
