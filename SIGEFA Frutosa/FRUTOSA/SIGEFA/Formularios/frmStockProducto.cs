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

    public partial class frmStockProducto : DevComponents.DotNetBar.OfficeForm    
    {
        clsAdmIngresoCamara AdmIngreso = new clsAdmIngresoCamara();
        public clsIngresoCamara ingreso = new clsIngresoCamara();
        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta (4)Captura de datos
        public Int32 CodIngresoCamara = 0;

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;

        public Boolean bcamara = false;

        //RAGA
        public Int32 codProducto;
        clsAdmProducto admProd = new clsAdmProducto();

        public frmStockProducto()
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

            if(Proceso == 4)
            data.DataSource = AdmIngreso.ListaIngresoContenedor(dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa, 1);
            else
            data.DataSource = AdmIngreso.ListaIngresoContenedor(dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa, 2);

            data.Filter = String.Empty;
            filtro = String.Empty;
            dataGridView1.ClearSelection();

            /*
            if (Proceso == 4)
            {
                dgvIngresosCamara.DataSource = data;
                data.DataSource = AdmIngreso.MuestraIngresoCamaras(dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa,1);//camaras en espera de la empresa logeada
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvIngresosCamara.ClearSelection();
            }
            else 
            {
                dgvIngresosCamara.DataSource = data;
                data.DataSource = AdmIngreso.MuestraIngresoCamaras(dtpDesde.Value, dtpHasta.Value, 0,0);//todas las camaras
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvIngresosCamara.ClearSelection();
            }


            */
        }

        private void btnIrPedido_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && dataGridView1.CurrentRow != null)
            {

                if (Proceso == 4)
                {

                    frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];

                    if (dataGridView1.CurrentRow.Cells[situacion1.Name].Value.ToString() == "PENDIENTE")
                    {
                    form.CodProveedor = Int32.Parse(dataGridView1.CurrentRow.Cells[codProveedor.Name].Value.ToString());
                    form.CodIngresoCamara = Int32.Parse(dataGridView1.CurrentRow.Cells[codIngresoContenedor.Name].Value.ToString());
                    form.txtIngreso.Text = dataGridView1.CurrentRow.Cells[nroTicket1.Name].Value.ToString();
                    bcamara = true;
                    this.Close();
                   
                    
                    }
                    else
                    {
                        MessageBox.Show("La recepción ya ha sido atendida");
                    }

                }
                else
                {

                DataGridViewRow row = dgvIngresosCamara.CurrentRow;
                frmIngresoCamaras form = new frmIngresoCamaras();
                form.MdiParent = this.MdiParent;
                form.CodIngresoCamara = ingreso.CodIngresoCamara;
                form.Proceso = 3;
                form.Show();

                }





            
  

            }
        }

        private void frmPedidosPendientes_Load(object sender, EventArgs e)
        {

            /*
            if (Proceso == 4)
            {
                btnAnular.Visible = false;
                button1.Visible = false;
                btnReporte.Visible = false;
                btnIrGuia.Text = "Seleccionar";

            }


            CargaLista();
            label7.Text = "Especie";
            label6.Text = "especie";

            */
        }

        
        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && ingreso.CodIngresoCamara != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el ingreso seleccionado", "Ingreso de camaras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (AdmIngreso.deleteIngresoContenedor(ingreso.CodIngresoCamara))
                    {
                        MessageBox.Show("El ingreso se ha anulado correctamente", "Ingreso de camaras", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                if (txtFiltro.Text.Length >= 1)
                {
                    data.Filter = String.Format("[{0}] like '*{1}*'", label6.Text.Trim(), txtFiltro.Text.Trim());
                //    data.Filter = String.Format("[{0}] between '*{1}*' and '*{2}*'", label6.Text.Trim(), txtFiltro.Text.Trim(),"");

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





        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = ((DataTable)dgv.DataSource).Copy();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                if (!column.Visible)
                {
                    dt.Columns.Remove(column.Name);
                }
            }
            return dt;
        }





        private void btnReporte_Click(object sender, EventArgs e)
        {
            /*
           
            clsReporteFrutosa ds = new clsReporteFrutosa();

            CRFiltroProdTerminado rpt = new CRFiltroProdTerminado();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds.getReporteProdTerminado(frmLogin.iCodAlmacen,codProducto).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
            */





            try
            {
                dataGridView2.Rows.Clear();
                foreach (DataGridViewRow row in dgvProductoTerminado.Rows)
                {

                    dataGridView2.Rows.Add(row.Cells[lote1.Name].Value.ToString(), row.Cells[fechaproduccion1.Name].Value.ToString(),
                        row.Cells[cantidad2.Name].Value.ToString(), row.Cells[pesoneto1.Name].Value.ToString(), row.Cells[gradosbrix0.Name].Value.ToString(),
                        row.Cells[unidad1.Name].Value.ToString(), row.Cells[producto1.Name].Value.ToString(), row.Cells[almacen1.Name].Value.ToString());
                }
                DataSet ds = new DataSet();
                DataTable dt = new DataTable("Table");
                // Columnas
                foreach (DataGridViewColumn column in dataGridView2.Columns)
                {
                    DataColumn dc = new DataColumn(column.Name.ToString());
                    dt.Columns.Add(dc);
                }
                // Datos
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {
                    DataGridViewRow row = dataGridView2.Rows[i];
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < dataGridView2.Columns.Count; j++)
                    {
                        dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                    }
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
                ds.WriteXml("C:\\XML\\ReporteVentaproduccion.xml", XmlWriteMode.WriteSchema);
                
                CRFiltroProdTerminado rpt = new CRFiltroProdTerminado();
                frmListaIngresosCamara frm = new frmListaIngresosCamara();
                rpt.SetDataSource(ds);
                frm.crvIngresosCamara.ReportSource = rpt;
                frm.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }

           

        }

        private void dgvIngresosCamara_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
        //    label7.Text = dgvIngresosCamara.Columns[e.ColumnIndex].HeaderText;
         //   label6.Text = dgvIngresosCamara.Columns[e.ColumnIndex].DataPropertyName;
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
            if (dgvIngresosCamara.Rows.Count >= 1 && e.Row.Selected)
            {
                ingreso.CodIngresoCamara = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                ingreso.NumeroOrden = Convert.ToInt32(e.Row.Cells[numorden.Name].Value.ToString());
            }
        }

        private void dgvIngresosCamara_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
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
        }

        private void button1_Click(object sender, EventArgs e)
        {


            dgvProductoTerminado.DataSource = data;
            data.DataSource = admProd.ListaProductosTerminados(codProducto, frmLogin.iCodAlmacen,0);
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvProductoTerminado.ClearSelection();


        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvIngresosCamara.Rows.Count >= 1 && dgvIngresosCamara.CurrentRow != null)
            {
                DataGridViewRow row = dgvIngresosCamara.CurrentRow;
                frmIngresoCamaras form = new frmIngresoCamaras();
                form.MdiParent = this.MdiParent;
                form.CodIngresoCamara = ingreso.CodIngresoCamara;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void dgvIngresosCamara_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
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
        }

        private void dgvIngresosCamara_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvIngresosCamara.Rows.Count >= 1 && e.RowIndex != -1 && dgvIngresosCamara.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dgvIngresosCamara.Rows[e.RowIndex];
                ingreso.CodIngresoCamara = Convert.ToInt32(Row.Cells[codigo.Name].Value); 
            }
        }

        private void frmStockProducto_Shown(object sender, EventArgs e)
        {
            //CargaLista();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvIngresosCamara.Rows.Count >= 1 && dgvIngresosCamara.CurrentRow != null)
            {
                DataGridViewRow row = dgvIngresosCamara.CurrentRow;
                frmIngresoCamaras form = new frmIngresoCamaras();
               // form.MdiParent = this.MdiParent;
                form.CodIngresoCamara = ingreso.CodIngresoCamara;
                form.Proceso = 2;
                form.ShowDialog();
                CargaLista();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvIngresosCamara.CurrentRow != null && ingreso.CodIngresoCamara != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el ingreso seleccionado", "Ingreso de Camaras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (AdmIngreso.delete(ingreso.CodIngresoCamara))
                    {
                        MessageBox.Show("El ingreso se ha anulado correctamente", "Ingreso de Camaras", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void fechaYHoraDeSalidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmIngresoCamaraSalida frm = new frmIngresoCamaraSalida();
                frm.ingreso.CodIngresoCamara = ingreso.CodIngresoCamara;
                frm.label4.Text = ingreso.NumeroOrden.ToString();
                frm.ShowDialog();
                CargaLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label7.Text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            label6.Text = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && e.RowIndex != -1 && dataGridView1.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dataGridView1.Rows[e.RowIndex];
                ingreso.CodIngresoCamara = Convert.ToInt32(Row.Cells[codIngresoContenedor.Name].Value);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso == 4)
            {

                frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];

                if (dataGridView1.CurrentRow.Cells[situacion1.Name].Value.ToString() == "PENDIENTE")
                {
                    form.CodProveedor = Int32.Parse(dataGridView1.CurrentRow.Cells[codProveedor.Name].Value.ToString());
                    form.CodIngresoCamara = Int32.Parse(dataGridView1.CurrentRow.Cells[codIngresoContenedor.Name].Value.ToString());
                    form.txtIngreso.Text = dataGridView1.CurrentRow.Cells[nroTicket1.Name].Value.ToString();
                    bcamara = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("La recepción ya ha sido atendida");
                }
            }
            else
            {

                DataGridViewRow row = dgvIngresosCamara.CurrentRow;
                frmIngresoCamaras form = new frmIngresoCamaras();
                form.MdiParent = this.MdiParent;
                form.CodIngresoCamara = ingreso.CodIngresoCamara;
                form.Proceso = 3;
                form.Show();

            }

        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && e.Row.Selected)
            {
                ingreso.CodIngresoCamara = Convert.ToInt32(e.Row.Cells[codIngresoContenedor.Name].Value);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmProductosLista"] != null)
                {
                    Application.OpenForms["frmProductosLista"].Activate();
                }
                else
                {
                    frmProductosLista frm = new frmProductosLista();
                    frm.Procede = 15;
                    frm.ShowDialog();
                }
            }

            
        }

        private void dgvProductoTerminado_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label7.Text = dgvProductoTerminado.Columns[e.ColumnIndex].HeaderText;
            label6.Text = dgvProductoTerminado.Columns[e.ColumnIndex].DataPropertyName;

            if (label6.Text == "gradosbrix0")
            {
                label8.Visible = true;
                textBox1.Visible = true;
                label11.Visible = true;
                textBox2.Visible = true;
           //     label6.Visible = false;
                txtFiltro.Visible = false;
            }
            else
            {
                label8.Visible = false;
                textBox1.Visible = false;
                label11.Visible = false;
                textBox2.Visible = false;
            //    label6.Visible = true;
                txtFiltro.Visible = true;
            }


        }

        private void dgvProductoTerminado_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            /*
            if (dataGridView1.Rows.Count >= 1 && e.Row.Selected)
            {
                ingreso.CodIngresoCamara = Convert.ToInt32(e.Row.Cells[codIngresoContenedor.Name].Value);
            }
            */
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {/*
            try
            {
                if (textBox1.Text.Length >= 1 && textBox2.Text.Length >= 1)
                {
                  //  data.Filter = String.Format("[{0}] like '*{1}*'", label6.Text.Trim(), txtFiltro.Text.Trim());
                    data.Filter = String.Format("[{0}] between '*{1}*' and '*{2}*'", label6.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim());
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

            */
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Length >= 1 && textBox2.Text.Length >= 1)
                {
                    //  data.Filter = String.Format("[{0}] like '*{1}*'", label6.Text.Trim(), txtFiltro.Text.Trim());
                    data.Filter = String.Format("[gradosbrix0] > '{0}' AND [gradosbrix0] < '{1}'", textBox1.Text, textBox2.Text);
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
    }
}
