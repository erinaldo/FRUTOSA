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
    public partial class frmarea : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 Proceso = 0; //(1) Nuevo (2)Editar (3)Consulta
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        public clsAreas area = new clsAreas();
        clsAdmAreas admarea = new  clsAdmAreas();

        public Int32 Param;

        public frmarea()
        {
            InitializeComponent();
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
            area = admarea.CargaAreas(area.Codarea,Param);
            CambiarEstados(false);
            groupBox2.Text = "Editar Registro";
            Proceso = 2;
            txtCodigo.Text = area.Codarea.ToString();
            txtDescripcion.Text = area.Nombre;
        }

        private void CargaLista() {
            dgvAreas.DataSource = admarea.ListaAreas(Param);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvAreas.CurrentRow.Index != -1 && area.Codarea != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Autorizado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admarea.Delete(area.Codarea, Param))
                    {
                        MessageBox.Show("Los datos han sido eliminado correctamente", "Autorizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                if (Proceso != 0 && txtDescripcion.Text != "")
                {
                    area.Nombre = txtDescripcion.Text;
                    area.Param = Param;
                   
                    if (Proceso == 1)
                    {
                        area.CodUser = frmLogin.iCodUser;
                        if (admarea.Insert(area))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Area", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                        }
                    }
                    else if (Proceso == 2)
                    {
                        if (admarea.Update(area))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Area", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                        }
                    }
                    Proceso = 0;
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

        private void dgvAreas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (dgvAreas.Rows.Count > 0 && e.Row.Selected)
                {
                    area.Codarea = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                    area.Nombre = e.Row.Cells[nombre.Name].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
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




        private void frmarea_Load(object sender, EventArgs e)
        {

            
                if (Param == 1)
                {
                    this.Text = "Areas";
                    this.groupBox1.Text = "Areas";
                }

                if (Param == 2)
                {
                    this.Text = "Centro de Costos";
                    this.groupBox1.Text = "Centro de Costos";
                }

                CargaLista();

         //   label2.Text = "Codigo";
          //  label3.Text = "codCaracteristica";
            if (Proceso == 3)
            {
                bloquearbotones();
            }

        }

        private void dgvAreas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {

        }

        private void dgvAreas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso == 3)
            {
                this.Close();
            }
        }
    }
}
