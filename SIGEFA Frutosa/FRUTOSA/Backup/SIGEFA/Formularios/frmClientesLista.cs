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
    public partial class frmClientesLista : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmCliente AdmCli = new clsAdmCliente();
        public clsCliente cli = new clsCliente();
        public Int32 Proceso = 0; //(1) Ingreso (2)Salida (3)Relacion
        public Int32 Procede = 0; //(1)Venta  (2)NotaSalida
        public Int32 Tipo = 0; //(0)DNI (1)RUC
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty; 
 
        public frmClientesLista()
        {
            InitializeComponent();
        }

        private void CargaLista()
        {
            dgvCliente.DataSource = data;
            data.DataSource = AdmCli.RelacionClientes();
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvCliente.ClearSelection();
        }

        private void frmClientesLista_Load(object sender, EventArgs e)
        {
            CargaLista();
            label2.Text = "RUC";
            label3.Text = "ruc";
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

        private void dgvCliente_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dgvCliente.Columns[e.ColumnIndex].HeaderText;
            label3.Text = dgvCliente.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
            //form.CodCliente = cli.CodCliente;
            //if (Tipo == 0)
            //{
            //    form.txtCodCliente.Text = cli.Dni;
            //    form.txtNombreCliente.Text = cli.Nombre;
            //    form.txtDireccion.Text = cli.DireccionLegal;
            //}
            //else
            //{
            //    form.txtCodCliente.Text = cli.Ruc;
            //    form.txtNombreCliente.Text = cli.RazonSocial;
            //    form.txtDireccion.Text = cli.DireccionLegal;
            //}
            this.Close(); 
        }

        private void dgvCliente_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvCliente.Rows.Count >= 1 && e.Row.Selected)
            {
                cli.CodCliente = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                cli.CodigoPersonalizado = e.Row.Cells[codperso.Name].Value.ToString();
                cli.Dni = e.Row.Cells[dni.Name].Value.ToString();
                cli.Ruc = e.Row.Cells[ruc.Name].Value.ToString();
                cli.RazonSocial = e.Row.Cells[razonsocial.Name].Value.ToString();
                //cli.Nombre = e.Row.Cells[nombre.Name].Value.ToString();
                cli.DireccionLegal = e.Row.Cells[direccionlegal.Name].Value.ToString();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmGestionCliente frm = new frmGestionCliente();
            frm.Proceso = 4;            
            frm.ShowDialog();
            CargaLista();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvCliente.SelectedRows.Count > 0)
            {
                //frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
                //form.CodCliente = cli.CodCliente;
                //if (Tipo == 0)
                //{
                //    form.txtCodCliente.Text = cli.Dni;
                //    form.txtNombreCliente.Text = cli.Nombre;
                //    form.txtDireccion.Text = cli.DireccionLegal;
                //}
                //else
                //{
                //    form.txtCodCliente.Text = cli.Ruc;
                //    form.txtNombreCliente.Text = cli.RazonSocial;
                //    form.txtDireccion.Text = cli.DireccionLegal;
                //}
                this.Close(); 
            }
           
        }

        private void txtFiltro_TextChanged_1(object sender, EventArgs e)
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

        public int GetCodigoCliente()
        {
            return cli.CodCliente;
        }

        private void frmClientesLista_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cli.CodCliente != 0)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void dgvCliente_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCliente.Rows.Count >= 1 && e.RowIndex != -1 && dgvCliente.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dgvCliente.Rows[e.RowIndex];
                cli.CodCliente = Convert.ToInt32(Row.Cells[codigo.Name].Value);
                cli.CodigoPersonalizado = Row.Cells[codperso.Name].Value.ToString();
                cli.Dni = Row.Cells[dni.Name].Value.ToString();
                cli.Ruc = Row.Cells[ruc.Name].Value.ToString();
                cli.RazonSocial = Row.Cells[razonsocial.Name].Value.ToString();
                //cli.Nombre = e.Row.Cells[nombre.Name].Value.ToString();
                cli.DireccionLegal = Row.Cells[direccionlegal.Name].Value.ToString();
            }
        }
    }
}
