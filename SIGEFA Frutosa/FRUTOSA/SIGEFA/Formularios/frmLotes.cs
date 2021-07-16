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
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios
{

    public partial class frmLotes : DevComponents.DotNetBar.OfficeForm    
    {
        clsLoteProduccion lotes = new clsLoteProduccion();
        clsAdmLoteProduccion admLotes = new clsAdmLoteProduccion();
        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta (4)Captura de datos
        public Int32 CodLote = 0;

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;        



        public frmLotes()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargaLista()
        {
            //int estao = 0;
            //if (Convert.ToInt32(cmbEstado.SelectedIndex) == 0)
            //{
            //    estao = 2;
            //}
                dgvLotes.DataSource = data;
                data.DataSource = admLotes.CargaLoteProduccionPorEstado(Convert.ToInt32(cmbEstado.SelectedIndex), dtpDesde.Value, dtpHasta.Value);//camaras en espera de la empresa logeada
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvLotes.ClearSelection();

            
        }

        private void btnIrPedido_Click(object sender, EventArgs e)
        {            
            if (dgvLotes.Rows.Count >= 1 && dgvLotes.CurrentRow != null)
            {
                DataGridViewRow row = dgvLotes.CurrentRow;
                frmLoteProduccion form = new frmLoteProduccion();
                form.MdiParent = this.MdiParent;
                form.CodLoteProduccion = lotes.CodLoteProduccion;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void frmPedidosPendientes_Load(object sender, EventArgs e)
        {

            dtpDesde.Value = new DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1);


            //CargaLista();
            cmbEstado.SelectedIndex = 2;
            label7.Text = "Nro Parte";
            label6.Text = "codigo";

            if (Proceso == 4)
            {
                btnAnular.Visible = false;
                btnReporte.Visible = false;
                btnIrGuia.Visible = false;
                cmbEstado.SelectedIndex = 1;
                cmbEstado.Enabled = false;
            }

        }

        
        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dgvLotes.CurrentRow != null && lotes.CodLoteProduccion != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el lote seleccionado", "Lotes de Produccion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if ( admLotes.Delete(lotes.CodLoteProduccion) == true )
                    {
                        MessageBox.Show("El lote se ha anulado correctamente", "Lotes de Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                    else
                    {
                        MessageBox.Show("No fue posible eliminar el lote", "Lotes de Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text.Length > 0)
                {


                    if (label6.Text == "codLoteProduccion")
                    data.Filter = String.Format("[{0}] = {1}", label6.Text.Trim(), txtFiltro.Text.Trim());
                    else
                    data.Filter = String.Format("[{0}] like '*{1}*'", label6.Text.Trim(), txtFiltro.Text.Trim());
                }
                else
                {
                    data.Filter = String.Empty;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {



            /*
            
            clsReporteFrutosa ds = new clsReporteFrutosa();
            CRListaRecepcionNotaIngreso rpt = new CRListaRecepcionNotaIngreso();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds.lis (Int32.Parse(CodNota), frmLogin.iCodAlmacen, 2).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
            

            CRListaLotes rpt = new CRListaLotes();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds.lis  (Int32.Parse(CodNota), frmLogin.iCodAlmacen, 2).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();

            */
            

            DataSet ds = new DataSet();
            DataTable dt = new DataTable("ListaLotesProduccion");
            // Columnas
            foreach (DataGridViewColumn column in dgvLotes.Columns)
            {
                DataColumn dc = new DataColumn(column.Name.ToString());
                dt.Columns.Add(dc);
            }
            // Datos
            for (int i = 0; i < dgvLotes.Rows.Count; i++)
            {
                DataGridViewRow row = dgvLotes.Rows[i];
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dgvLotes.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);
            ds.WriteXml("C:\\XML\\ListaLotes.xml", XmlWriteMode.WriteSchema);


            CRListaLotes rpt = new CRListaLotes();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();

            
        }

        private void dgvIngresosCamara_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label7.Text = dgvLotes.Columns[e.ColumnIndex].HeaderText;
            label6.Text = dgvLotes.Columns[e.ColumnIndex].DataPropertyName;


         


        }

        private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void dtpHasta_ValueChanged_1(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void dgvIngresosCamara_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && e.Row.Selected)
            {
                lotes.CodLoteProduccion = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
            }
        }

        private void dgvIngresosCamara_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso != 4)
            {
                if (dgvLotes.Rows.Count >= 1 && e.RowIndex != -1)
                {
                    frmLoteProduccion form = new frmLoteProduccion();
                    form.MdiParent = this.MdiParent;
                    form.CodLoteProduccion = lotes.CodLoteProduccion;
                    form.Proceso = 3;
                    form.Show();
                }
            }
            else 
            {
                CodLote = lotes.CodLoteProduccion;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && dgvLotes.CurrentRow != null)
            {
                DataGridViewRow row = dgvLotes.CurrentRow;
                frmLoteProduccion form = new frmLoteProduccion();
                form.MdiParent = this.MdiParent;
                form.CodLoteProduccion = lotes.CodLoteProduccion;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void dgvIngresosCamara_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*
            dgvLotes.ContextMenuStrip = new ContextMenuStrip();
            if (e.RowIndex != -1)
            {
                dgvLotes.Rows[e.RowIndex].Selected = true;
                if (e.Button == MouseButtons.Right && e.RowIndex != -1)
                {
                    if (dgvLotes.SelectedCells.Count > 0)
                    {
                        dgvLotes.ContextMenuStrip = contextMenuStrip1;                        
                    }
                }
            }

            */
        }

        private void dgvIngresosCamara_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && e.RowIndex != -1 && dgvLotes.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dgvLotes.Rows[e.RowIndex];
                lotes.CodLoteProduccion = Convert.ToInt32(Row.Cells[codigo.Name].Value); 
            }
        }

        private void frmIngresosCamara_Shown(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && dgvLotes.CurrentRow != null)
            {
                DataGridViewRow row = dgvLotes.CurrentRow;
                frmLoteProduccion form = new frmLoteProduccion();
                form.MdiParent = this.MdiParent;
                form.CodLoteProduccion = lotes.CodLoteProduccion;
                form.Proceso = 2;
                form.Show();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLotes.CurrentRow != null && lotes.CodLoteProduccion != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el lote seleccionado", "Lotes de Produccion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admLotes.Delete(lotes.CodLoteProduccion))
                    {
                        MessageBox.Show("El Lote se ha anulado correctamente", "Lotes de Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargaLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
