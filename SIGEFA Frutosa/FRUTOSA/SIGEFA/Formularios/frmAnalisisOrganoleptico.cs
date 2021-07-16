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
    public partial class frmAnalisisOrganoleptico : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 Proceso = 0; //(1) Nuevo (2)Editar (3)Consulta
        clsConsultasExternas ext = new clsConsultasExternas();
        clsAnalisisOrganoleptico ana = new clsAnalisisOrganoleptico();
        clsAdmAnalisisOrganoleptico admana = new clsAdmAnalisisOrganoleptico();
        public static BindingSource data = new BindingSource();
        String filtro;
        public frmAnalisisOrganoleptico()
        {
            InitializeComponent();
        }

        private void CargarAnalisis() {
            ana = admana.CargarAnalisis(ana.Codanalisis);
            if (ana != null)
            {
                txtCodigo.Text = ana.Codanalisis.ToString();
                txtnombre.Text = ana.Nombre.ToString();
            }
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
            txtnombre.Text = "";
            superValidator1.Validate();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CambiarEstados(false);
            groupBox2.Text = "Registro Nuevo";
            Proceso = 1;
            ext.limpiar(groupBox2.Controls);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ext.limpiar(groupBox2.Controls);
            CambiarEstados(false);
            groupBox2.Text = "Editar Registro";
            Proceso = 2;
            CargarAnalisis();
        }

        private void CargaLista()
        {
            dgvAnalisis.DataSource = data;
            data.DataSource = admana.ListaAnalisis();
            data.Filter = String.Empty;
            filtro = String.Empty;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAnalisis.CurrentRow.Index != -1 && ana.Codanalisis != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Analisis", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admana.Delete(ana.Codanalisis))
                    {
                        MessageBox.Show("Los datos han sido eliminado correctamente", "Analisis", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void frmAnalisisOrganoleptico_Load(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Proceso != 0)
            {
                ana.Nombre = txtnombre.Text;
                
                if (Proceso == 1)
                {
                    if (admana.Insert(ana))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Pesador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CambiarEstados(true);
                        CargaLista();
                        Proceso = 0;
                    }
                }
                else if (Proceso == 2)
                {
                    if (admana.Update(ana))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Conductor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CambiarEstados(true);
                        CargaLista();
                        Proceso = 0;
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

        private void dgvAnalisis_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvAnalisis.Rows.Count > 0 && e.Row.Selected)
            {
                ana.Codanalisis = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
            }
        }

        private void dgvAnalisis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }
    }
}
