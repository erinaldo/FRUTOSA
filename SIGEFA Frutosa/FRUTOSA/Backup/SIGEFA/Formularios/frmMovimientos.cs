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
using SIGEFA.Reportes;

namespace SIGEFA.Formularios
{
    public partial class frmMovimientos : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmCtaCte admcta = new clsAdmCtaCte();
        clsCtaCte cta = new clsCtaCte();
        DataTable dt = new DataTable();
        public static BindingSource data = new BindingSource();

        

        public frmMovimientos()
        {
            InitializeComponent();
        }

        private void CargaMovimientos()
        {
            dgvDetalle.DataSource = data;
            data.DataSource = admcta.ListaMovimientos(frmLogin.iCodAlmacen);
            data.Filter = String.Empty;
            dgvDetalle.ClearSelection();
        }

        private void DarFormato()
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    if (Convert.ToDecimal(row.Cells[debe.Name].Value) > 0)
                    {
                        row.Cells[debe.Name].Style.ForeColor = Color.Red;
                    }
                    else if (Convert.ToDecimal(row.Cells[haber.Name].Value) > 0)
                    {
                        row.Cells[haber.Name].Style.ForeColor = Color.Blue;
                    }
                    else
                    {
                        //row.DefaultCellStyle.BackColor = Color.White;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                }
            }
        }

        private void frmMovimientos_Load(object sender, EventArgs e)
        {
            CargaMovimientos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmMovimientosControl frm = new frmMovimientosControl();
            frm.Proceso = 1;
            frm.ShowDialog();
            CargaMovimientos();
            DarFormato();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            cta.CodMovi = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
            frmMovimientosControl frm = new frmMovimientosControl();
            frm.Proceso = 3;
            frm.ShowDialog();
         }

        private void frmMovimientos_Shown(object sender, EventArgs e)
        {
            DarFormato();

            //label7.Text = "Banco";
            //label6.Text = "NomBanco";
        }

        private void dgvDetalle_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DarFormato();
        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
           
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            frmMovimientosControl frm = new frmMovimientosControl();
            frm.CodMovimiento = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
            frm.tipo = Convert.ToString(dgvDetalle.SelectedRows[0].Cells[transaccion.Name].Value);
            frm.Proceso = 2;
            frm.ShowDialog();
            CargaMovimientos();
            DarFormato();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            cta.CodMovi = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
            if (dgvDetalle.CurrentRow.Index != -1 && cta.CodMovi != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "CONTROL DE FLUJO DE CAJA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admcta.DeleteMov(cta.CodMovi, frmLogin.iCodAlmacen))
                    {
                        CargaMovimientos();
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            CargaMovimientos();
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDetalle_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmMovimientosControl frm = new frmMovimientosControl();
            frm.CodMovimiento = Convert.ToInt32(dgvDetalle.SelectedRows[0].Cells[codMovimientos.Name].Value);
            frm.tipo = Convert.ToString(dgvDetalle.SelectedRows[0].Cells[transaccion.Name].Value);
            frm.Proceso = 3;
            frm.ShowDialog();
        }

        private void biImprimir_Click(object sender, EventArgs e)
        {
            frmParamMovimientosBancarios frm = new frmParamMovimientosBancarios();
            frm.ShowDialog();
        }
    }
}
