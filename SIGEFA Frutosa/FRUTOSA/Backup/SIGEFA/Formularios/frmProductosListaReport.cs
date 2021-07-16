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
    public partial class frmProductosListaReport : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmProducto AdmPro = new clsAdmProducto();
        clsAdmTipoArticulo AdmTip = new clsAdmTipoArticulo();
        public clsProducto pro = new clsProducto();
        public Int32 Proceso = 0; //(1) Ingreso (2)Salida (3)Relacion
        public Int32 Inicio = 0;
        public Int32 Procede = 0; //(1)Nota de Salida (2)Venta (3)Materia Prima
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        public List<Int32> seleccion = new List<Int32>();
        public List<Int32> submateria = new List<Int32>();// relacion de los productos que ya han sido asignados

        public Int32 codAlmacen;

        public frmProductosListaReport()
        {
            InitializeComponent();
        }

        private void frmProductosListaReport_Load(object sender, EventArgs e)
        {
           
            label2.Text = "Descripcion";
            label3.Text = "descripcion";
        }

        private void dgvProductos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            //if (dgvProductos.Rows.Count >= 1 && e.Row.Selected)
            //{
            //    pro.CodProducto = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
            //    pro.Referencia = e.Row.Cells[referencia.Name].Value.ToString();
            //    pro.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
            //}
        }

        private void CargaLista(Int32 inicio)
        {  
            dgvProductos.DataSource = data;
            if (Procede == 3)
            {
                data.DataSource = AdmPro.ListaProductosMP(Convert.ToInt32(cbTipoArticulo.SelectedValue), inicio);
            }
            else
            {
                data.DataSource = AdmPro.ListaProductosReporte(codAlmacen, Convert.ToInt32(cbTipoArticulo.SelectedValue), inicio);
            }
            DepurarLista();
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvProductos.ClearSelection();
        }

        private void DepurarLista()
        {
            foreach (Int32 deta in submateria)
            {
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta)
                    {
                        dgvProductos.Rows.Remove(row);
                    }
                }
            }
        }

        private void CargaTipoArticulos()
        {
            cbTipoArticulo.DataSource = AdmTip.MuestraTipoArticulos();
            cbTipoArticulo.DisplayMember = "descripcion";
            cbTipoArticulo.ValueMember = "codTipoArticulo";
            if (Procede == 3)
            {
                cbTipoArticulo.SelectedIndex = 3;
                cbTipoArticulo.Enabled = false;
            }
            else
            {
                cbTipoArticulo.SelectedIndex = 0;
            }
        }

        private void frmProductosListaReport_Shown(object sender, EventArgs e)
        {
            CargaTipoArticulos();
            CargaLista(Inicio);
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

        private void dgvProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dgvProductos.Columns[e.ColumnIndex].HeaderText;
            label3.Text = dgvProductos.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
            
            //if (Proceso == 3)
            //{
            //    foreach (int cod in seleccion)
            //    {
            //        if (Application.OpenForms["ReporteInventario"] != null)
            //        {
            //            Application.OpenForms["ReporteInventario"].Close();
            //        }
            //        ReporteInventario form = new ReporteInventario();
            //        form.txtInicio.Text = row.Cells[referencia.Name].Value.ToString();
            //        form.codArticulo1 = Convert.ToInt32(row.Cells[codigo.Name].Value);
            //        form.ShowDialog();
            //    }
            //}
            //this.Close();
            this.DialogResult = DialogResult.Yes;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //if (dgvProductos.SelectedRows.Count > 1)
            //{
            //    DataGridViewRow row = dgvProductos.SelectedRows[0];
            //    //this.Close();
            //    if (Proceso == 3)
            //    {
            //        foreach (int cod in seleccion)
            //        {
            //            if (Application.OpenForms["ReporteInventario"] != null)
            //            {
            //                Application.OpenForms["ReporteInventario"].Close();
            //            }
            //            ReporteInventario form = new ReporteInventario();
            //            form.txtInicio.Text = row.Cells[referencia.Name].Value.ToString();
            //            form.codArticulo1 = Convert.ToInt32(row.Cells[codigo.Name].Value);
            //            form.ShowDialog();
            //        }
            //    }
            //    this.Close();
            //}
            //else
            //{

            //}
            //this.Close();
            this.DialogResult = DialogResult.Yes;
        }       

        private void cbTipoArticulo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargaLista(Inicio);
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvProductos.Rows.Count >= 1 && e.RowIndex != -1 && dgvProductos.CurrentRow.Index == e.RowIndex)
                {
                    DataGridViewRow Row = dgvProductos.Rows[e.RowIndex];
                    pro.CodProducto = Convert.ToInt32(Row.Cells[codigo.Name].Value);
                    pro.Referencia = Row.Cells[referencia.Name].Value.ToString();
                    pro.Descripcion = Row.Cells[descripcion.Name].Value.ToString();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
