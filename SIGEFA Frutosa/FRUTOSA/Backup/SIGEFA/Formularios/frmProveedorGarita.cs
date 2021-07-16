using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Entidades;
using SIGEFA.Administradores;
namespace SIGEFA.Formularios
{
    public partial class frmProveedorGarita : DevComponents.DotNetBar.OfficeForm
    {
        clsValidar val = new clsValidar();
        public Int32 Proceso = 0; //(1) Nuevo (2)Editar (3)Consulta
        clsConsultasExternas ext = new clsConsultasExternas();
        clsProveedorGarita prove = new clsProveedorGarita();
        clsAdmProveedorGarita admprove = new clsAdmProveedorGarita();
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        public frmProveedorGarita()
        {
            InitializeComponent();
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }
        private void CambiarEstados(Boolean Estado)
        {
            groupBox1.Visible = Estado;
            groupBox2.Visible = !Estado;
            btnGuardar.Enabled = !Estado;
            btnNuevo.Enabled = Estado;
            btnEditar.Enabled = Estado;
            btnEliminar.Enabled = Estado;
            btnReporte.Enabled = Estado;
            txtCodigo.Text = "";
            txtRazonSocial.Text = "";
            superValidator1.Validate();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CambiarEstados(false);
            groupBox2.Text = "Registro Nuevo";
            Proceso = 1;
            ext.limpiar(groupBox2.Controls);
        }

        private void CargaLista() {
            data.DataSource = admprove.ListaProveedorGarita();
            dgvProveedorGarita.DataSource = data;
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvProveedorGarita.ClearSelection();
        }

        private void CargaProvGarita() {

            prove = admprove.CargaProvGarita(prove.CodProveedorGarita);
            if (prove !=  null)
            {
                txtCodigo.Text = prove.CodProveedorGarita.ToString();
                txtRuc.Text = prove.Ruc;
                txtRazonSocial.Text = prove.Razonsocial;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ext.limpiar(groupBox2.Controls);
            CambiarEstados(false);
            groupBox2.Text = "Editar Registro";
            Proceso = 2;
            CargaProvGarita();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedorGarita.CurrentRow.Index != -1 && prove.CodProveedorGarita != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Conductor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admprove.Delete(prove.CodProveedorGarita))
                    {
                        MessageBox.Show("Los datos han sido eliminado correctamente", "Conductor", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (Proceso != 0)
            {
                prove.Ruc = txtRuc.Text;
                prove.Razonsocial = txtRazonSocial.Text;

                if (Proceso == 1)
                {
                    if (admprove.Insert(prove))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Pesador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CambiarEstados(true);
                        CargaLista();
                        Proceso = 0;
                    }
                }
                else if (Proceso == 2)
                {
                    if (admprove.Update(prove))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Pesador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CambiarEstados(true);
                        CargaLista();
                        Proceso = 0;
                    }
                }
                //Proceso = 0;
            }
        }

        private void dgvProveedorGarita_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvProveedorGarita.Rows.Count > 0 && e.Row.Selected)
            {
                prove.CodProveedorGarita = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
            }
        }

        private void frmProveedorGarita_Load(object sender, EventArgs e)
        {
            CargaLista();
        }
    }
}
