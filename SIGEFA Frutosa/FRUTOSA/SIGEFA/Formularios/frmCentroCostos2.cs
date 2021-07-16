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
using SIGEFA.Reportes;
namespace SIGEFA.Formularios
{
    public partial class frmCentroCostos2 : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 Proceso = 0; //(1) Nuevo (2)Editar (3)Consulta
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        public clsAreas area = new clsAreas();
        clsAdmAreas admarea = new  clsAdmAreas();

        public clsCentroCosto centro = new clsCentroCosto();
        public clsCentroCosto centroparent = new clsCentroCosto();

        clsAdmCentro admcentro = new clsAdmCentro();

        public Int32 Param = 1;
        public Int32 codPadre;

     //   clsReporteFrutosa ds = new clsReporteFrutosa();
        public Int32 Procede;

        public frmCentroCostos2()
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
           // area = admarea.CargaAreas(area.Codarea,Param);
            centro = admcentro.CargaCentros(centro.Codcentro);



            CambiarEstados(false);
            groupBox2.Text = "Editar Registro";
            Proceso = 2;
            txtCodigo.Text = centro.Codcentro.ToString();
            txtDescripcion.Text = centro.Nombre;
        }

        private void CargaLista() {
            dgvAreas.DataSource = admcentro.ListaCentros(centroparent.Codcentro);

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
                    if (admcentro.Delete(0))
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


                    centro.Nombre = txtDescripcion.Text;
                    centro.Nivel = 2;
                    centro.Padre = centroparent.Codcentro;
                   
                    if (Proceso == 1)
                    {
                       
                        if (admcentro.Insert(centro))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Area", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                        }
                    }
                    else if (Proceso == 2)
                    {
                        if (admcentro.Update(centro))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Area", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                        }
                    }
                    Proceso = 1;
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
                    centro.Codcentro = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                    centro.Nombre = e.Row.Cells[nombre.Name].Value.ToString();
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

        private void CargaListaNiveles()
        {
            dgvAreas.DataSource = admcentro.ListaCentrosNivel(2);

        }


        private void frmCentroCostos2_Load(object sender, EventArgs e)
        {
            //btnLineas.Text = "Nivel " + Param;
            if (Proceso == 1 || Proceso == 2)
                CargaLista();

         //   label2.Text = "Codigo";
          //  label3.Text = "codCaracteristica";
            if (Proceso == 3 || Proceso == 4)
            {
                CargaLista();
                bloquearbotones();
            }

            if (Proceso == 5 || Proceso == 6)
            {
                CargaListaNiveles();
                bloquearbotones();
                btnLineas.Visible = false;
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

            if (Proceso == 3)//frmNotaIngreso
            {
                frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                form.centro = this.centro;
            }
            else if (Proceso == 4)//frmNotaSalida
            {



              
                frmCentroCostos form = (frmCentroCostos)Application.OpenForms["frmCentroCostos"];
                form.centro = this.centro;
                form.Proceso = this.Proceso;
               /*
               
                
                frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
                form.centro = this.centro;*/
            }


            if (Proceso == 5)//frmRptCentroCostos
            {
                frmRptCentroCostos form = (frmRptCentroCostos)Application.OpenForms["frmRptCentroCostos"];
                form.txtUnArt.Text = centro.Codcentro.ToString();
            }
            else if (Proceso == 6)//frmRptCentroCostos
            {
                frmRptCentroCostos form = (frmRptCentroCostos)Application.OpenForms["frmRptCentroCostos"];
                form.textBox1.Text = centro.Codcentro.ToString();
            }

            if (Proceso >= 3)
            {
                this.Close();
            }
        }

        private void btnLineas_Click(object sender, EventArgs e)
        {


            if (dgvAreas.RowCount == 0)
                return;

            frmCentroCostos3 form = new frmCentroCostos3();
            form.Proceso = this.Proceso;
            form.centroparent = this.centro;
            //      form.MdiParent = this;
            form.ShowDialog();
            
        }
    }
}
