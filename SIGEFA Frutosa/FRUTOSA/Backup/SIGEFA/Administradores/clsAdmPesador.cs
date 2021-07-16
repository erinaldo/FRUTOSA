using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using SIGEFA.Entidades;
using SIGEFA.Administradores;
using SIGEFA.InterMySql;
using SIGEFA.Interfaces;
namespace SIGEFA.Administradores
{
    public class clsAdmPesador
    {
        IPesador pesador = new MysqlPesador();

        public Boolean Insert(clsPesador pesad) {
            try
            {
                return pesador.Insert(pesad);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: "+ex.Message.ToString());
                return false;
            }
        }
        public Boolean Update(clsPesador pesad)
        {
            try
            {
                return pesador.Update(pesad);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return false;
            }
        }
        public Boolean Delete(Int32 codpesador) {
            try
            {
                return pesador.Delete(codpesador);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return false;
            }
        }
        public DataTable ListaPesadores() {
            try
            {
                return pesador.ListaPesadores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return null;
            }
        }
        public clsPesador CargaPesador(Int32 Codigo) {
            try
            {
                return pesador.CargaPesador(Codigo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar: " + ex.Message.ToString());
                return null;
            }
        }
    }
}
