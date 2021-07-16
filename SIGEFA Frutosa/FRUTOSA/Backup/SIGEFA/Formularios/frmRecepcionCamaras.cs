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

    public partial class frmRecepcionCamaras : DevComponents.DotNetBar.OfficeForm    
    {
        clsAdmIngresoCamara AdmIngreso = new clsAdmIngresoCamara();
        clsIngresoCamara ingreso = new clsIngresoCamara();
        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta (4)Captura de datos




        public Int32 CodIngresoCamara = 0;
        public Int32 CodNotaIng = 0;


        String numTicket;
        String nguia;

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        Int32 CodNotaIngreso;
        public frmRecepcionCamaras()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargaLista()
        {

            dataGridView1.DataSource = data;              
            data.DataSource = AdmIngreso.MuestraRecepcionCamaras(dtpDesde.Value,dtpHasta.Value,frmLogin.iCodEmpresa,0);            
            data.Filter = String.Empty;
            filtro = String.Empty;
       //     dgvIngresosCamara.ClearSelection();
        }

        private void btnIrPedido_Click(object sender, EventArgs e)
        {
            IrNotaIngreso();
        }
        private void IrNotaIngreso()
        {
            
            if (dataGridView1.Rows.Count >= 1 && dataGridView1.CurrentRow != null)
            {
              
                frmNotaIngreso form = new frmNotaIngreso();
                form.MdiParent = this.MdiParent;
                form.CodNota = CodNotaIngreso.ToString();
                form.CodIngresoCamara = CodIngresoCamara;
                form.Proceso = 3;
                form.txtIngreso.Text = nguia;
                form.Show();
            }
            
        }

        private void frmPedidosPendientes_Load(object sender, EventArgs e)
        {

            dtpDesde.Value = new DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1);



        //    CargaLista();
            label7.Text = "Especie";
            label6.Text = "especie";
        }

        
        private void btnAnular_Click(object sender, EventArgs e)
        {/*
            if (dgvIngresosCamara.CurrentRow != null && ingreso.CodIngresoCamara != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el ingreso seleccionado", "Recepcion de camaras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (AdmIngreso.updateSituacion(ingreso.CodIngresoCamara,3))
                    {
                        MessageBox.Show("El ingreso se ha rechazado correctamente", "Recepcion de camaras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
            */
            
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
                if (txtFiltro.Text.Length >= 2)
                {
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
            //ingreso.CodIngresoCamara 
            clsReporteFrutosa ds = new clsReporteFrutosa();

            CRIngMatprima rpt = new CRIngMatprima();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();

            rpt.SetDataSource(ds.ListaReporteRecepcion(dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa,2).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();

            */

            

            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable("Table");
                // Columnas
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    DataColumn dc = new DataColumn(column.Name.ToString());
                    dt.Columns.Add(dc);
                }
                // Datos
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView1.Rows[i];
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                    }
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
                ds.WriteXml("C:\\XML\\ReporteIngresoMatPrima.xml", XmlWriteMode.WriteSchema);

                CRIngMatprima rpt = new CRIngMatprima();
                frmListaIngresosCamara frm = new frmListaIngresosCamara();
                rpt.SetDataSource(ds);
                frm.crvIngresosCamara.ReportSource = rpt;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


           
           


          




        }

        private void dgvIngresosCamara_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          //  label7.Text = dgvIngresosCamara.Columns[e.ColumnIndex].HeaderText;
          //  label6.Text = dgvIngresosCamara.Columns[e.ColumnIndex].DataPropertyName;
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
        {/*
            if (dgvIngresosCamara.Rows.Count >= 1 && e.Row.Selected)
            {
                ingreso.CodIngresoCamara = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                CodNotaIngreso = Convert.ToInt32(e.Row.Cells[codNotaIngreso.Name].Value);
            }
            */
        }

        private void dgvIngresosCamara_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {/*
            if (Proceso != 4)
            {
                if (dgvIngresosCamara.Rows.Count >= 1 && e.RowIndex != -1)
                {
                    frmIngresoCamaras form = new frmIngresoCamaras();
                    form.MdiParent = this.MdiParent;
                    form.CodIngresoCamara = ingreso.CodIngresoCamara;
                    form.Proceso = 3;
                    form.Show();
                }
            }
            else 
            {
                CodIngresoCamara = ingreso.CodIngresoCamara;
                this.Close();
            }
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IrNotaIngreso();
        }

        private void dgvIngresosCamara_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {/*
            dgvIngresosCamara.ContextMenuStrip = new ContextMenuStrip();
            if (e.RowIndex != -1)
            {
                dgvIngresosCamara.Rows[e.RowIndex].Selected = true;
                if (e.Button == MouseButtons.Right && e.RowIndex != -1)
                {
                    if (dgvIngresosCamara.SelectedCells.Count > 0)
                    {
                        dgvIngresosCamara.ContextMenuStrip = contextMenuStrip1;                        
                    }
                }
            }
            */
        }

        private void dgvIngresosCamara_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (dgvIngresosCamara.Rows.Count >= 1 && e.RowIndex != -1 && dgvIngresosCamara.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dgvIngresosCamara.Rows[e.RowIndex];
                ingreso.CodIngresoCamara = Convert.ToInt32(Row.Cells[codigo.Name].Value); 
            }
            */
        }

        private void frmIngresosCamara_Shown(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (dgvIngresosCamara.Rows.Count >= 1 && dgvIngresosCamara.CurrentRow != null)
            {
                if (dgvIngresosCamara.Rows.Count >= 1 && dgvIngresosCamara.CurrentRow != null)
                {
                    DataGridViewRow row = dgvIngresosCamara.CurrentRow;
                    frmNotaIngreso form = new frmNotaIngreso();
                    form.MdiParent = this.MdiParent;
                    form.CodNota = CodNotaIngreso.ToString();
                    form.CodIngresoCamara = ingreso.CodIngresoCamara;
                    form.Proceso = 2;
                    form.Show();
                }
            }
            */
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            if (dgvIngresosCamara.CurrentRow != null && ingreso.CodIngresoCamara != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el ingreso seleccionado", "Recepcion de Camaras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (AdmIngreso.updateSituacion(ingreso.CodIngresoCamara, 3))
                    {
                        MessageBox.Show("El ingreso se ha rechazado correctamente", "Recepcion de Camaras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
            */
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && e.RowIndex != -1 && dataGridView1.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dataGridView1.Rows[e.RowIndex];
                CodIngresoCamara= Convert.ToInt32(Row.Cells[codIngresoContenedor.Name].Value);
              //  CodNotaIng = Convert.ToInt32(Row.Cells[codNotaIngreso1.Name].Value);
         //       nguia = Row.Cells[nroguia.Name].Value.ToString();


            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            IrNotaIngreso();
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label7.Text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            label6.Text = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {

            if (dataGridView1.Rows.Count >= 1 && e.Row.Selected)
            {

                ingreso.CodIngresoCamara = Convert.ToInt32(e.Row.Cells[codIngresoContenedor.Name].Value);
            //    CodNotaIngreso = Convert.ToInt32(e.Row.Cells[codNotaIngreso1.Name].Value);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Proceso != 4)
            {
                if (dataGridView1.Rows.Count > 0 && dataGridView1.CurrentRow != null && CodIngresoCamara != 0)
                {
                    frmIngresoCamaras form = new frmIngresoCamaras();
                    form.MdiParent = this.MdiParent;

                    if (CodIngresoCamara != 0)
                    {
                        form.CodIngresoCamara = CodIngresoCamara;
                        form.Proceso = 3;
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("No esta asociado a una Recepcion");
                    }

                }
            }
            else
            {
                CodIngresoCamara = ingreso.CodIngresoCamara;
                this.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && e.RowIndex != -1 && dataGridView1.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dataGridView1.Rows[e.RowIndex];
                CodIngresoCamara = Convert.ToInt32(Row.Cells[codIngresoContenedor.Name].Value);
                CodNotaIngreso = Convert.ToInt32(Row.Cells[CodigoNotaIngreso.Name].Value);
                nguia = Row.Cells[numdocumento.Name].Value.ToString();

            }
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && dataGridView1.CurrentRow != null)
            {

                frmNotaIngreso form = new frmNotaIngreso();
                form.MdiParent = this.MdiParent;
                form.CodNota = CodNotaIngreso.ToString();
                form.CodIngresoCamara = CodIngresoCamara;
                form.Proceso = 3;
                form.txtIngreso.Text = nguia;
                form.Show();
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            label7.Text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            label6.Text = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
        }
    }
}
