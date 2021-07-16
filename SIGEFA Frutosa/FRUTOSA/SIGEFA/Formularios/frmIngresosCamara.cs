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

    public partial class frmIngresosCamara : DevComponents.DotNetBar.OfficeForm    
    {
        clsAdmIngresoCamara AdmIngreso = new clsAdmIngresoCamara();
        public clsIngresoCamara ingreso = new clsIngresoCamara();
        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta (4)Captura de datos
        public Int32 CodIngresoCamara = 0;

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;

        public Boolean bcamara = false;

        public frmIngresosCamara()
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


            if (Proceso == 1)
                data.DataSource = AdmIngreso.ListaIngresoContenedor(dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa,1);

            if (Proceso == 4)
             data.DataSource = AdmIngreso.ListaIngresoContenedor(dtpDesde.Value, dtpHasta.Value, frmLogin.iCodEmpresa,2);

            data.Filter = String.Empty;
            filtro = String.Empty;
            dataGridView1.ClearSelection();

        }

        private void btnIrPedido_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && dataGridView1.CurrentRow != null)
            {

                if (Proceso == 1)
                {

                  DataGridViewRow row = dataGridView1.CurrentRow;
                  frmIngresoCamaras form = new frmIngresoCamaras();
                  form.MdiParent = this.MdiParent;
                  form.CodIngresoCamara = ingreso.CodIngresoCamara;
                  form.Proceso = 3;
                  form.Show();
                }
                if (Proceso == 4)
                {

                    frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];

                    if (dataGridView1.CurrentRow.Cells[situacion1.Name].Value.ToString() == "PENDIENTE")
                    {
                    form.CodProveedor = Int32.Parse(dataGridView1.CurrentRow.Cells[codProveedor.Name].Value.ToString());
                    form.CodIngresoCamara = Int32.Parse(dataGridView1.CurrentRow.Cells[codIngresoContenedor.Name].Value.ToString());
                    form.txtIngreso.Text = dataGridView1.CurrentRow.Cells[numdocumento.Name].Value.ToString();
                    bcamara = true;
                    this.Close();
                   
                    
                    }
                    else
                    {
                        MessageBox.Show("La recepción ya ha sido atendida");
                    }

                }
                



            }
        }

        private void frmPedidosPendientes_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1);

            if (Proceso == 4)
            {
                btnAnular.Visible = false;
                button1.Visible = false;
                btnReporte.Visible = false;
                btnIrGuia.Text = "Seleccionar";
            }

            label7.Text = "numdocumento";
            label6.Text = "numdocumento";
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
                    else
                    {
                        MessageBox.Show("No es posible anular el Ingreso", "Ingreso de camaras", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Information);
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

            CRRecepciones rpt = new CRRecepciones();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();

            rpt.SetDataSource(ds.ListaReporteRecepcion(dtpDesde.Value,dtpHasta.Value,frmLogin.iCodEmpresa,1).Tables[0]);
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
                ds.WriteXml("C:\\XML\\ListaReporteRecepcion.xml", XmlWriteMode.WriteSchema);

                CRRecepciones rpt = new CRRecepciones();
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
            /*
            if (dgvIngresosCamara.Rows.Count >= 1 && e.Row.Selected)
            {
                ingreso.CodIngresoCamara = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                ingreso.NumeroOrden = Convert.ToInt32(e.Row.Cells[numorden.Name].Value.ToString());
            }
            */
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            CargaLista();
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
                DataGridViewRow row = dgvIngresosCamara.CurrentRow;
                frmIngresoCamaras form = new frmIngresoCamaras();
               // form.MdiParent = this.MdiParent;
                form.CodIngresoCamara = ingreso.CodIngresoCamara;
                form.Proceso = 2;
                form.ShowDialog();
                CargaLista();
            }
*/

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
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
            */
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
            if (Proceso == 1)
            {

                DataGridViewRow row = dataGridView1.CurrentRow;
                frmIngresoCamaras form = new frmIngresoCamaras();
                form.MdiParent = this.MdiParent;
                form.CodIngresoCamara = ingreso.CodIngresoCamara;
                form.Proceso = 3;
                form.Show();
            }
            if (Proceso == 4)
            {

                frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];

                form.CodProveedor = Int32.Parse(dataGridView1.CurrentRow.Cells[codProveedor.Name].Value.ToString());
                form.CodIngresoCamara = Int32.Parse(dataGridView1.CurrentRow.Cells[codIngresoContenedor.Name].Value.ToString());
                form.txtIngreso.Text = dataGridView1.CurrentRow.Cells[numdocumento.Name].Value.ToString();
                bcamara = true;
                this.Close();

            }



        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dataGridView1.Rows.Count >= 1 && e.Row.Selected)
            {
                ingreso.CodIngresoCamara = Convert.ToInt32(e.Row.Cells[codIngresoContenedor.Name].Value);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
