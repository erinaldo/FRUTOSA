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
    public partial class frmConfigurarMateria : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 codPadre;
        public Int32 codHijo;
        clsAdmConfigurarMP admCon = new clsAdmConfigurarMP();
        clsConfigurarMP con = new clsConfigurarMP();
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        clsValidar ok = new clsValidar();
        public List<Int32> lista = new List<Int32>();

        public frmConfigurarMateria()
        {
            InitializeComponent();
        }

        private void frmConfigurarMateria_Load(object sender, EventArgs e)
        {
            
        }

        private void RecorreDetalle()
        {
            lista.Clear();            
            if (dgvSubMateria.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvSubMateria.Rows)
                {
                    Int32 cod = Convert.ToInt32(row.Cells[codproducto.Name].Value);
                    lista.Add(cod);
                }
            }
        }

       

        private void txtPadre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmProductosListaReport frm = new frmProductosListaReport();
                frm.Proceso = 3;
                frm.Procede = 3;
                frm.Inicio = 0;                
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.Yes)
                {
                    codPadre = frm.pro.CodProducto;
                    cambiaestados(true);
                    txtPadre.Text = frm.pro.Referencia;
                    txtDescripcionPadre.Text = frm.pro.Descripcion;
                    txtHijo.Focus();
                    CargaListaConfiguracion();
                }
            }
        }

        private void cambiaestados(Boolean estado)
        {
            txtHijo.Visible = estado;
            txtDescripcionHijo.Visible = estado;
            txtPorcentaje.Visible = estado;
            label2.Visible = estado;
            label3.Visible = estado;
            label4.Visible = estado;

        }
        private void limpiar()
        {
            txtHijo.Text = "";
            txtDescripcionHijo.Text = "";
            txtPorcentaje.Text = "";
            txtHijo.Focus(); 
        }

        private void txtHijo_KeyDown(object sender, KeyEventArgs e)
        {
            RecorreDetalle();
            if (e.KeyCode == Keys.F1)
            {
                frmProductosListaReport frm = new frmProductosListaReport();
                frm.Proceso = 3;
                frm.Procede = 3;
                frm.submateria = lista;
                frm.Inicio = codPadre;
                DialogResult res = frm.ShowDialog();
                if (res == DialogResult.Yes)
                {
                    codHijo = frm.pro.CodProducto;
                    txtHijo.Text = frm.pro.Referencia;
                    txtDescripcionHijo.Text = frm.pro.Descripcion;
                    txtPorcentaje.Focus();
                }
            }
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                con.CodPadre = codPadre;
                con.CodHijo = codHijo;
                con.Porcentaje = Convert.ToDecimal(txtPorcentaje.Text);                
                con.CodUser = frmLogin.iCodUser;
                
                if (admCon.insert(con))
                {
                    MessageBox.Show("Los datos se guardaron correctamente", "Materia Prima", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    CargaListaConfiguracion();
                }
                
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos requeridos(*)", "Materia Prima", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaListaConfiguracion()
        {
            dgvSubMateria.DataSource = data;
            data.DataSource = admCon.MuestraConfiguracionMP(codPadre);
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvSubMateria.ClearSelection();
        }

        private void customValidator1_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            
        }

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
           
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            
        }

        private void customValidator3_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            //if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            //else
            //    e.IsValid = true;
        }

        private void btnremove_Click(object sender, EventArgs e)
        {
            if (dgvSubMateria.CurrentRow.Index != -1 && con.CodConfigurarMP != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Materia Prima", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admCon.delete(con.CodConfigurarMP))
                    {
                        MessageBox.Show("Los datos han sido eliminado correctamente", "Materia Prima", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaListaConfiguracion();
                    }
                }
            }
        }

        private void dgvSubMateria_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            try
            {
                if (dgvSubMateria.Rows.Count >= 1 && e.RowIndex != -1 && dgvSubMateria.CurrentRow.Index == e.RowIndex)
                {
                    DataGridViewRow Row = dgvSubMateria.Rows[e.RowIndex];
                    con.CodConfigurarMP = Convert.ToInt32(Row.Cells[codConfi.Name].Value);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
