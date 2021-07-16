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
using SIGEFA.Formularios;

namespace SIGEFA.Formularios
{
    public partial class frmFlujoCajaRegistro : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 CodFlujoCaja = 0;
        public Int32 Proceso = 0;

        clsAdmFlujoCaja AdmFlu = new clsAdmFlujoCaja();
        clsFlujoCaja flu = new clsFlujoCaja();
        clsValidar ok = new clsValidar();
        clsAperturaCierre aper = new clsAperturaCierre();
        clsAdmAperturaCierre AdmAper = new clsAdmAperturaCierre();

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;




        clsAreas centro = new clsAreas();
        public Int32 codCentro;


        public frmFlujoCajaRegistro()
        {
            InitializeComponent();
        }

        private void HabilitaControles(Boolean Estado)
        {
            txtconcepto.Enabled = Estado;
            txtmonto.Enabled = Estado;
            dtpfecha.Enabled = Estado;
            cboTipo.Enabled = Estado;
            btnGuardar.Enabled = Estado;
            cmbtipopagoser.Enabled = Estado;
        }


        private void carga()
        {
            cmbtipopagoser.DataSource = AdmFlu.ListaPagoCobro();
            cmbtipopagoser.DisplayMember = "descripcion";
            cmbtipopagoser.ValueMember = "codtipopagoserv";
            cmbtipopagoser.SelectedIndex = -1;
        }

        private void frmFlujoCajaRegistro_Load(object sender, EventArgs e)
        {
            carga();
            if (Proceso == 1 || Proceso == 2)
            {
                HabilitaControles(true);
            }
            else if (Proceso == 3)
            {
                HabilitaControles(false);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                flu.Concepto = txtconcepto.Text;
                flu.Monto = Convert.ToDecimal(txtmonto.Text);
                flu.Fecha = dtpfecha.Value;
                flu.FechaRegistro = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                flu.CodUser = frmLogin.iCodUser;
                flu.CodAlmacen = frmLogin.iCodAlmacen;
                flu.ICentroCostos = Convert.ToInt32(textBox2.Text);

                if (cboTipo.Text == "INGRESO")
                {
                    flu.Tipo = 1;
                    flu.CodPagoServicio = 0;
                }
                else if (cboTipo.Text == "EGRESO")
                {
                    flu.Tipo = 2;
                    cmbtipopagoser.Enabled = true;
                    flu.CodPagoServicio = Convert.ToInt32(cmbtipopagoser.SelectedValue);
                }

                if (flu.Concepto != "" && flu.Monto != 0 && flu.CodUser != 0 && flu.Tipo != 0)
                {
                    if (Proceso == 1)
                    {
                        if (AdmFlu.Insert(flu))
                        {
                            MessageBox.Show("Los datos se Guardaron Correctamente", "CONTROL DE FLUJO DE CAJA",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un Error al Guardar los Datos", "CONTROL DE FLUJO DE CAJA",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (Proceso == 2)
                    {
                        flu.CodFlujoCaja = CodFlujoCaja;
                        if (AdmFlu.Update(flu))
                        {
                            MessageBox.Show("Los datos se Modificaron Correctamente", "CONTROL DE FLUJO DE CAJA",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un Error al Actualizar los Datos", "CONTROL DE FLUJO DE CAJA",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    Proceso = 0;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Faltan Datos, Verifique Informacion!", "CONTROL DE FLUJO DE CAJA",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtconcepto.Focus();
                }
            }
        }

        private void cboTipo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboTipo.SelectedIndex == 1 || cboTipo.SelectedIndex == 0)
            {
                cmbtipopagoser.Enabled = false;
                cmbtipopagoser.SelectedValue = 0;
            }
            else if (cboTipo.SelectedIndex == 2)
            {
                carga();
                cmbtipopagoser.Enabled = true;

            }
            btnGuardar.Enabled = true;
        }

        private void txtmonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
        }

        private void txtmonto_KeyUp(object sender, KeyEventArgs e)
        {
            //aper = AdmAper.ValidarAperturaCierre(frmLogin.iCodAlmacen);
            //if (Convert.ToDecimal(txtmonto.Text) >= aper.MontoApertura)
            //{
            //    MessageBox.Show("El Monto Excede del Monto Aperturado", "Fluja Caja", MessageBoxButtons.OK,
            //        MessageBoxIcon.Warning);
            //}
        }

        private void txtmonto_Leave(object sender, EventArgs e)
        {
            aper = AdmAper.ValidarAperturaCierre(frmLogin.iCodAlmacen);
            if (Convert.ToDecimal(txtmonto.Text) >= aper.MontoApertura)
            {
                MessageBox.Show("El Monto Ingresado Excede del Monto Aperturado ... [MONTO APERTURADO = "+aper.MontoApertura+"]", "Fluja Caja", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtmonto.Focus();
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

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator3_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.Enabled)
                if (Proceso != 0)
                    if (c.SelectedIndex != -1)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                else
                    e.IsValid = true;
            else
                e.IsValid = true;
        }

        private void txtconcepto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.letras(e);
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmarea"] != null)
                {
                    Application.OpenForms["frmarea"].Activate();
                }
                else
                {
                    frmarea form = new frmarea();
                    form.Proceso = 3;
                    form.Param = 2;
                    form.ShowDialog();
                    centro = form.area;
                    codCentro = centro.Codarea;
                    textBox1.Text = centro.Codarea.ToString();
                    textBox2.Text = centro.Nombre;
                }
            }
        }
    }
}
