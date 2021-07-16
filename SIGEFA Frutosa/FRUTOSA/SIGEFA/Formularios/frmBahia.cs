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
    public partial class frmBahia : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmBahia AdmAut = new clsAdmBahia();
        public clsBahia aut = new clsBahia();
        public Int32 Proceso = 0; //(1) Nuevo (2)Editar (3)Consulta

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;

        public frmBahia()
        {
            InitializeComponent();
        }

        private void CargaLista()
        {
            dgvBahias.DataSource = data;
            data.DataSource = AdmAut.MuestraBahias();
            data.Filter = String.Empty;
            filtro = String.Empty;
        }

        private void CambiarEstados(Boolean Estado)
        {
            groupBox1.Visible = Estado;
            groupBox2.Visible = !Estado;
            btnGuardar.Enabled = !Estado;
            btnGuardar.Visible = !Estado;            
            btnNuevo.Enabled = Estado;
            btnEditar.Enabled = Estado;
            btnEliminar.Enabled = Estado;
            btnReporte.Enabled = Estado;
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            superValidator1.Validate();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CambiarEstados(false);
            groupBox2.Text = "Registro Nuevo";
            Proceso = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            CambiarEstados(false);
            groupBox2.Text = "Editar Registro";
            Proceso = 2;
            txtCodigo.Text = aut.CodBahia.ToString();
            txtDescripcion.Text = aut.Nombre;
        }

        private void frmBahia_Load(object sender, EventArgs e)
        {
            CargaLista();
            label2.Text = "Codigo";
            label3.Text = "codCaracteristica";
            if (Proceso == 3)
            {
                bloquearbotones();
            }
        }

        private void bloquearbotones()
        {
            btnNuevo.Visible = false;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            btnReporte.Visible = false;
            btnGuardar.Text = "Aceptar";
            btnGuardar.ImageIndex = 6;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBahias.CurrentRow.Index != -1 && aut.CodBahia != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Bahia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (AdmAut.delete(aut.CodBahia))
                    {
                        MessageBox.Show("Los datos han sido eliminado correctamente", "Bahia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                this.Close();
            }
            else
            {
                Proceso = 0;
                CambiarEstados(true);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                if (Proceso != 0 && txtDescripcion.Text != "")
                {
                    aut.Nombre = txtDescripcion.Text;
                    if (Proceso == 1)
                    {
                        aut.CodUser = frmLogin.iCodUser;
                        if (AdmAut.insert(aut))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Bahia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                        }
                    }
                    else if (Proceso == 2)
                    {
                        if (AdmAut.update(aut))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Bahia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                        }
                    }
                    Proceso = 0;
                }
            }
        }

        private void dgvBahias_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvBahias.Rows.Count >= 1 && e.Row.Selected)
            {
                aut.CodBahia = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                aut.Nombre = e.Row.Cells[nombre.Name].Value.ToString();
                aut.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
                aut.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;               
            }
            else if(dgvBahias.Rows.Count == 0)
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void dgvBahias_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dgvBahias.Columns[e.ColumnIndex].HeaderText;
            label3.Text = dgvBahias.Columns[e.ColumnIndex].DataPropertyName;
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

        private void customValidator1_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void dgvBahias_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso == 3)
            {               
                this.Close();
            }
        }

        private void dgvBahias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso == 3)
                btnGuardar.Enabled = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("Bahias");
            // Columnas
            foreach (DataGridViewColumn column in dgvBahias.Columns)
            {
                DataColumn dc = new DataColumn(column.Name.ToString());
                dt.Columns.Add(dc);
            }
            // Datos
            for (int i = 0; i < dgvBahias.Rows.Count; i++)
            {
                DataGridViewRow row = dgvBahias.Rows[i];
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dgvBahias.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                dt.Rows.Add(dr);
            }

            frmBahiaRP frm = new frmBahiaRP();
            frm.DTable = dt;
            frm.Show();
        }
    }
}
