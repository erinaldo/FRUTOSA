using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios
{
    public partial class frmAprobacionPagos : DevComponents.DotNetBar.OfficeForm
    {

        clsAdmPago Admpag = new clsAdmPago();
        private Int32 Cod = 0, aprob=0;
        public frmAprobacionPagos()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cargaLista()
        {
            dgvDetalle.DataSource = Admpag.MuestraPagosPorAprobar(cmbEstado.SelectedIndex, dtpFecha1.Value, dtpFecha2.Value);
        }

       
      
        private void biAprobar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow != null && dgvDetalle.Rows.Count >= 1)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Aprobar el Pago", "Aprobación de Pagos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {

                    Admpag.AprobarPago(Cod,2);//aprobar
                    MessageBox.Show("El Pago se Aprobado Satisfactoriamente", "Aprobación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargaLista();
                }
            }
        }

        private void biDesaprobar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow != null && dgvDetalle.Rows.Count >= 1)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Aprobar el Pago", "Aprobación de Pagos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    Admpag.AprobarPago(Cod, 3);//desaprobar
                    MessageBox.Show("El Pago se Aprobado Satisfactoriamente", "Aprobación de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargaLista();
                }
            }
        }

        private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
                {
                    aprob = Convert.ToInt32(e.Row.Cells[Aprobado1.Name].Value);
                    Cod = Convert.ToInt32(e.Row.Cells[codPago.Name].Value);
                }
            }
            catch (Exception ex) { 
            
                
            }
            

        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            cargaLista();
        }

        private void frmAprobacionPagos_Load(object sender, EventArgs e)
        {
            cmbEstado.SelectedIndex = 1;
        }

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (aprob == 1)
            {//por aprobar
                biAprobar.Enabled = true;
                biDesaprobar.Enabled = true;
            }
            else
            {
                biAprobar.Enabled = false;
                biDesaprobar.Enabled = false;
            }
        }

      
    }
}
