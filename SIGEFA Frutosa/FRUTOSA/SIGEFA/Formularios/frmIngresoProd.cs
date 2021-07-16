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
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios
{
    public partial class frmIngresoProd : DevComponents.DotNetBar.OfficeForm    
    {
        clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();
        clsNotaIngreso notaI = new clsNotaIngreso();
        clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();
        clsNotaSalida notaS = new clsNotaSalida();
        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta

        clsAdmProducto AdmPro = new clsAdmProducto();

        Int32 codTransaccion;
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;        

        public frmIngresoProd()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            CargaLista(cmbTipoDocumento.SelectedIndex);
            activarfiltro();
        }

        private void activarfiltro()
        {
            label1.Visible = true;
            label2.Visible = true;
            txtFiltro.Visible = true;
            label2.Text = "Numero";
            label3.Text = "codNotaIngreso";
        }

        private void CargaLista(Int32 caso)
        {
            dgvDocumentos.DataSource = data;
            data.DataSource = AdmPro.ListaTotalIngProdTerm(codTransaccion, frmLogin.iCodAlmacen, dtpDesde.Value.Date, dtpHasta.Value.Date);
          

            
            
            
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvDocumentos.ClearSelection();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text.Length >= 2)
                {
                    data.Filter = String.Format("[{0}] like '*{1}*'", label3.Text.Trim(), txtFiltro.Text.Trim());
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

        private void btnIrNota_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvDocumentos.SelectedRows[0];
            if (dgvDocumentos.Rows.Count >= 1)
            {

                frmNotaIngreso form = new frmNotaIngreso();
                form.MdiParent = this.MdiParent;
                form.CodNota = notaI.CodNotaIngreso;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIngresoProd_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1);

            cmbTipoDocumento.SelectedIndex = 0;

        //    cmbTipoDocumento.SelectedIndex = 2;
/*

            if (Proceso == 4)
            {
                btnEliminar.Visible = true;
            }
            else if (Proceso == 5)
            {
                btnAnular.Visible = true;
            }
            */
        }

        private void cmbTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoDocumento.SelectedIndex == 0)
            {
                codTransaccion = 24;
            }
            else if (cmbTipoDocumento.SelectedIndex == 1)
            {
                codTransaccion = 27;
            }
        }

        private void dgvDocumentos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvDocumentos.Rows.Count >= 1 && e.Row.Selected)
            {


                notaI.CodNotaIngreso = e.Row.Cells[numero.Name].Value.ToString();


                /*
                if (e.Row.Cells[tipo.Name].Value.ToString() == "NI")
                {
                    notaI.CodNotaIngreso = e.Row.Cells[numero.Name].Value.ToString();
                }
                else
                {
                    notaS.CodNotaSalida = e.Row.Cells[numero.Name].Value.ToString();
                }

                */
            }
        }

        private void dgvDocumentos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDocumentos.Rows.Count >= 1 && e.RowIndex != -1)
            {


                frmNotaIngreso form = new frmNotaIngreso();
                form.MdiParent = this.MdiParent;
                form.CodNota = notaI.CodNotaIngreso;
                form.Proceso = 3;
                form.Show();


            }
        }

        private void dgvDocumentos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dgvDocumentos.Columns[e.ColumnIndex].HeaderText;
            label3.Text = dgvDocumentos.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void dgvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {/*
            if (dgvDocumentos.Rows.Count >= 1 && e.RowIndex != -1)
            {
                btnIrNota.Enabled = true;
                btnEliminar.Enabled = true;
                if (dgvDocumentos.Rows[e.RowIndex].Cells[anulado.Name].Value.ToString() == "ACTIVO")
                {
                    btnAnular.Text = "Anular";
                    btnAnular.Enabled = true;
                    btnAnular.ImageIndex = 10;
                }
                else 
                {
                    btnAnular.Text = "Activar";
                    btnAnular.Enabled = true; 
                    btnAnular.ImageIndex = 11;
                }
            }
 
            */
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {



            DataGridViewRow row = dgvDocumentos.SelectedRows[0];
            DialogResult dlgResult;
            if (dgvDocumentos.Rows.Count >= 1 && dgvDocumentos.CurrentRow.Index != -1)
            {

                    dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (AdmNotaI.delete(Convert.ToInt32(notaI.CodNotaIngreso)))
                        {
                            MessageBox.Show("Los datos han sido eliminado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargaLista(cmbTipoDocumento.SelectedIndex);
                        }
                    }
                
            }
                else
                {
                  
                    
                    dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Almacenes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (AdmNotaS.delete(Convert.ToInt32(notaS.CodNotaSalida)))
                        {
                            MessageBox.Show("Los datos han sido eliminado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargaLista(cmbTipoDocumento.SelectedIndex);
                        }
                    }
                }
                       
        }

        private void btrnAnular_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvDocumentos.CurrentRow;
            DialogResult dlgResult ;
            if(btnAnular.Text == "Anular")
            {
                if (dgvDocumentos.Rows.Count >= 1 && dgvDocumentos.CurrentRow.Index != -1)
                {
                    
                         dlgResult = MessageBox.Show("Esta seguro que desea anular el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (AdmNotaI.anular1(Convert.ToInt32(notaI.CodNotaIngreso)))
                            {
                                MessageBox.Show("El documento ha sido anulado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargaLista(cmbTipoDocumento.SelectedIndex);
                            }
                        }
                }
                    else
                    {
                         dlgResult = MessageBox.Show("Esta seguro que desea anular el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (AdmNotaS.anular(Convert.ToInt32(notaS.CodNotaSalida)))
                            {
                                MessageBox.Show("El documento ha sido anulado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargaLista(cmbTipoDocumento.SelectedIndex);
                            }
                        }
                    }
                }
            
            else if (btnAnular.Text == "Activar")
            {
                /*
                if (dgvDocumentos.Rows.Count >= 1 && dgvDocumentos.CurrentRow.Index != -1)
                {
                    if (row.Cells[tipo.Name].Value.ToString() == "NI")
                    {
                        DialogResult dlgResult = MessageBox.Show("Esta seguro que desea activar el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (AdmNotaI.activar(Convert.ToInt32(notaI.CodNotaIngreso)))
                            {
                                MessageBox.Show("El documento ha sido activado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargaLista(cmbTipoDocumento.SelectedIndex);
                            }
                        }
                    }
                    else
                    {
                        DialogResult dlgResult = MessageBox.Show("Esta seguro que desea activar el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (AdmNotaS.activar(Convert.ToInt32(notaS.CodNotaSalida)))
                            {
                                MessageBox.Show("El documento ha sido activado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargaLista(cmbTipoDocumento.SelectedIndex);
                            }
                        }
                    }
                }
                 */
            }

            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable("ListaIngresoProd");
            // Columnas
            foreach (DataGridViewColumn column in dgvDocumentos.Columns)
            {
                DataColumn dc = new DataColumn(column.Name.ToString());
                dt.Columns.Add(dc);
            }
            // Datos
            for (int i = 0; i < dgvDocumentos.Rows.Count; i++)
            {
                DataGridViewRow row = dgvDocumentos.Rows[i];
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dgvDocumentos.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);
            ds.WriteXml("C:\\XML\\ListaIngresoProd.xml", XmlWriteMode.WriteSchema);


            CRptIngresosProd rpt = new CRptIngresosProd();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();

        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

        }

      

       

       
    }
}
