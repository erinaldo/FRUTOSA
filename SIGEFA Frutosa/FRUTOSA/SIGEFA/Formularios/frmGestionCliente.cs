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
using System.Net;
using System.IO;
using System.Xml;

namespace SIGEFA.Formularios
{
    public partial class frmGestionCliente : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 Proceso = 0; //(1) Nuevo Cliente (2)Editar Cliente (3) Nota Salida
        clsAdmCliente admCli = new clsAdmCliente();
        public clsCliente cli = new clsCliente();
        private Boolean Validacion = true;
        clsConsultasExternas ext = new clsConsultasExternas();
        clsLocalidad local = new clsLocalidad();
        clsAdmFormaPago AdmPago = new clsAdmFormaPago();
        clsAdmListaPrecio admLista = new clsAdmListaPrecio();
        clsAdmZona AdmZona = new clsAdmZona();
        clsZona zona = new clsZona();
        clsAdmVendedor AdmVen = new clsAdmVendedor();
        clsValidar ok = new clsValidar();
        String CodPer = null;
        clsMoneda moneda = new clsMoneda();
        clsAdmMoneda AdmMon = new clsAdmMoneda();
         
        public frmGestionCliente()
        {
            InitializeComponent();
            
        }     
        

        private void btnAceptar_Click(object sender, EventArgs e)
        {
                
            if (superValidator1.Validate())
            {
                cli.CodigoPersonalizado = txtCodigoPersonalizado.Text;
                if (txtDni.Text != "") { cli.Dni = txtDni.Text; } else { cli.Dni = null; }
                if (txtRUC.Text != "") { cli.Ruc = txtRUC.Text; } else { cli.Ruc = null; }
                cli.RazonSocial = txtRazonSocial.Text;
                cli.Nombre = txtRazonSocial.Text;
                cli.DireccionLegal = txtDireccionLegal.Text;
                cli.DireccionEntrega = txtDireccionEntrega.Text;
                cli.Telefono = txtTelefono.Text;
                cli.Email = txtEmail.Text;
                cli.Web = txtWeb.Text;
                //cli.Pais = cbPais.SelectedText;
                if (cbDepartamento.SelectedIndex != -1) { cli.Departamento = cbDepartamento.SelectedValue.ToString(); } 
                if (cbProvincia.SelectedIndex != -1) { cli.Provincia = cbProvincia.SelectedValue.ToString(); } 
                if (cbDistrito.SelectedIndex != -1) { cli.Distrito = cbDistrito.SelectedValue.ToString(); } 
                if (cbZona.SelectedIndex != -1) { cli.Zona = Convert.ToInt32(cbZona.SelectedValue); }
                cli.Estado = cbActivo.Checked;
                if (txtDscto.Text != "") { cli.Descuento = Convert.ToDecimal(txtDscto.Text); } 
                if (cbListaPrecios.SelectedIndex != -1) { cli.CodListaPrecio = Convert.ToInt32(cbListaPrecios.SelectedValue); }
                if (cmbVendedores.SelectedIndex != -1) { cli.CodVendedor = Convert.ToInt32(cmbVendedores.SelectedValue); } 
                if (cbFormaPago.SelectedIndex != -1) { cli.FormaPago = Convert.ToInt32(cbFormaPago.SelectedValue); }                 
                if (cbMoneda.SelectedIndex != -1) { cli.Moneda = Convert.ToInt32(cbMoneda.SelectedValue); }
                if (txtLineaCredito.Text != "") { cli.LineaCredito = Convert.ToDecimal(txtLineaCredito.Text); } 
                cli.Banco = txtBanco.Text;
                cli.CtaCte = txtCtaCte.Text;
                cli.Contacto = txtContacto.Text;
                cli.TelefonoContacto = txtTelefonoContacto.Text;
                if (cbCalificacion.SelectedIndex != -1) { cli.Calificacion = Convert.ToInt32(cbCalificacion.SelectedValue); }
                cli.Comentario = txtComentario.Text;
                cli.CodUser = frmLogin.iCodUser;
                cli.ClienteFacturasVencidas = chbCliFacturasVencidas.Checked;

                if (Proceso == 1 || Proceso == 3 || Proceso == 4)
                {
                    if (admCli.insert(cli))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (Proceso == 1) { muestralista(); }
                        this.Close();
                    }
                }
                else if (Proceso == 2)
                {
                    if (admCli.update(cli))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        muestralista();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos requeridos(*)", "Gestion Cliente", MessageBoxButtons.OK, MessageBoxIcon.Error);                        
            }
        }

        private void ValidarDatos(Control.ControlCollection Coleccion)
        {
            Validacion = true;
            foreach (Control c in Coleccion)
            {
                if (Convert.ToInt32(c.Tag) == 1)
                {
                    if (c.Enabled == true && c.Text == "")
                    {
                        c.BackColor = Color.LightPink;
                        c.Focus();
                        Validacion = false;
                    }
                }
                if (c.HasChildren)
                {
                    ValidarDatos(c.Controls);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmGestionCliente_Load(object sender, EventArgs e)
        {
            CargaMoneda();
            CargaFormaPagos();
            CargaListaPrecios();
            CargaZonas();
            CargaVendedores();
            CargaLocalidades(cbDepartamento, "000000", 1);
            if (Proceso == 1 || Proceso == 4)
            {
                CodPer = admCli.CodigoPersonalizado(); //ultimo codigo personalizado para generar NUEVO
                generaCodPer(CodPer);
                cbActivo.Visible = false;
                if (Proceso == 4)
                {
                    tabControl1.Controls.Remove(tabPage2);
                    //tabPage2.Hide();
                }
            }
            else if (Proceso == 2)
            {
                CargaCliente(cli.CodCliente);
            }
            else if (Proceso == 3)
            {
                CargaCliente(cli.CodCliente);
                ext.sololectura(tabControl1.Controls);
                btnAceptar.Visible = false;
                btnCancelar.Text = "Aceptar";
                btnCancelar.ImageIndex = 1;
            }
        }

        private void CargaMoneda()
        {
            cbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
            cbMoneda.DisplayMember = "descripcion";
            cbMoneda.ValueMember = "codMoneda";
            cbMoneda.SelectedIndex = 0;
        }

        private void generaCodPer(string codper)
        {
            bool isValid = char.IsLetter(codper.FirstOrDefault());
            if (isValid)
            {
                codper = codper.Remove(0, 1);
            }

            int newcod = Convert.ToInt32(codper) + 1;
            string nuevocod = newcod.ToString().PadLeft(5, '0');
            txtCodigoPersonalizado.Text = "C" + nuevocod;

        }

        private void CargaVendedores()
        {
            cmbVendedores.DataSource = AdmVen.MuestraVendedoresDestaque();
            cmbVendedores.DisplayMember = "apellido";
            cmbVendedores.ValueMember = "codVendedor";
            cmbVendedores.SelectedIndex = -1;
        }

        private void CargaFormaPagos()
        {
            cbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
            cbFormaPago.DisplayMember = "descripcion";
            cbFormaPago.ValueMember = "codFormaPago";
            cbFormaPago.SelectedIndex = 0;
        }

        private void CargaZonas()
        {
            cbZona.DataSource = AdmZona.MuestraZonas();
            cbZona.DisplayMember = "descripcion";
            cbZona.ValueMember = "codZona";
            cbZona.SelectedIndex = -1;
        }

        private void CargaListaPrecios()
        {
            //cbListaPrecios.DataSource = admLista.MuestraListaPrecioxFormaPago(frmLogin.iCodSucursal,Convert.ToInt32(cbFormaPago.SelectedValue));
            //cbListaPrecios.DisplayMember = "nombre";
            //cbListaPrecios.ValueMember = "codListaPrecio";
            //cbListaPrecios.SelectedIndex = 0;
        }

        private void CargaLocalidades(ComboBox Combo,String Padre, Int32 Nivel )
        {
            Combo.DataSource = local.CargaLocalidades(Padre, Nivel);
            Combo.DisplayMember = "nombre";
            Combo.ValueMember = "codLocalidad";
            Combo.SelectedIndex = -1;
        }

        private void CargaCliente(Int32 codCliente)
        {
            cli = admCli.MuestraCliente(codCliente);
            txtCodigo.Text = cli.CodCliente.ToString();
            txtCodigoPersonalizado.Text = cli.CodigoPersonalizado;
            txtDni.Text = cli.Dni;            
            txtRUC.Text = cli.Ruc;
            txtRazonSocial.Text = cli.RazonSocial;
            txtDireccionLegal.Text = cli.DireccionLegal;
            txtDireccionEntrega.Text = cli.DireccionEntrega;
            txtTelefono.Text = cli.Telefono;
            txtEmail.Text = cli.Email;
            txtWeb.Text = cli.Web;
            cbPais.SelectedValue = cli.Pais;
            cbDepartamento.SelectedValue = cli.Departamento;
            if (cli.Departamento != "")
            {
                cbDepartamento.SelectedValue = cli.Departamento;
                CargaLocalidades(cbProvincia, cli.Departamento.ToString(), 2);
                cbProvincia.Enabled = true;

                if (cli.Provincia != "")
                {
                    cbProvincia.SelectedValue = cli.Provincia;
                    CargaLocalidades(cbDistrito, cli.Provincia.ToString(), 3);
                    cbDistrito.Enabled = true;
                    cbDistrito.SelectedValue = cli.Distrito;
                }
            }
            
            cbZona.SelectedValue = cli.Zona;
            cbActivo.Checked = cli.Estado;
            txtDscto.Text = cli.Descuento.ToString();
            cbFormaPago.SelectedValue = cli.FormaPago;
            cbMoneda.SelectedValue = cli.Moneda;
            CargaListaPrecios();
           //  cbListaPrecios.SelectedValue = cli.CodListaPrecio;
            cmbVendedores.SelectedValue = cli.CodVendedor;
            txtLineaCredito.Text = cli.LineaCredito.ToString();
            txtBanco.Text = cli.Banco;
            txtCtaCte.Text = cli.CtaCte;
            txtContacto.Text = cli.Contacto;
            txtTelefonoContacto.Text = cli.TelefonoContacto;
            cbCalificacion.SelectedIndex = cli.Calificacion;
            txtComentario.Text = cli.Comentario;
            txtFechaRegistro.Text = cli.FechaRegistro.Date.ToShortDateString();
            if (cli.Habilitado) { label29.Visible = false; } else { label29.Visible = true; }
            chbCliFacturasVencidas.Checked = cli.ClienteFacturasVencidas;
        }
        


        

        private void muestralista()
        {          
            if (Application.OpenForms["frmClientesCompletos"] != null)
            {
                frmClientesCompletos form = (frmClientesCompletos) Application.OpenForms["frmClientesCompletos"];
                form.Activate();
                form.CargaLista();                    
            }
            else
            {
                frmClientesCompletos form = new frmClientesCompletos();
                form.MdiParent = Application.OpenForms["mdi_Menu"];                
                form.Show();
            }           
        }
                

        private void btnSunat_Click(object sender, EventArgs e)
        {
            if (ext.rucsunat(txtRUC.Text))
            {
                txtRazonSocial.Text = ext.RazonSocial;
                txtDireccionLegal.Text = ext.DireccionLegal;
            }
            else
            {
                ext.limpiar(this.Controls);
            }
        }

        private void txtRUC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {   
                if (ext.rucsunat(txtRUC.Text))
                {
                    txtRazonSocial.Text = ext.RazonSocial;
                    txtDireccionLegal.Text = ext.DireccionLegal;
                }
                else
                {
                    ext.limpiar(this.Controls);
                }
            }
        }

        private void cbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargaLocalidades(cbProvincia, cbDepartamento.SelectedValue.ToString(), 2);            
            cbProvincia.Enabled = true;
        }

        private void cbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargaLocalidades(cbDistrito, cbProvincia.SelectedValue.ToString(), 3);
            cbDistrito.Enabled = true;
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

        private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
        }

        private void txtLineaCredito_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender,e);
        }

        private void cbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargaListaPrecios();
        }
    }
}
