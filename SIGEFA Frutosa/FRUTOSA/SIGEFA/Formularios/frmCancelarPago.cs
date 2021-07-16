using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios
{
    public partial class frmCancelarPago : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmFactura Admfac=new clsAdmFactura();
        clsFactura fac=new clsFactura();
        clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();
        clsNotaSalida notaS = new clsNotaSalida();
        clsAdmLetra AdmLetra = new clsAdmLetra();
        clsLetra letra = new clsLetra();
        clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();
        clsTipoCambio tc = new clsTipoCambio();
        clsAdmMetodoPago admMPago = new clsAdmMetodoPago();
        clsValidar val = new clsValidar();
        clsPago Pag = new clsPago();
        clsAdmPago Admpag = new clsAdmPago();
        clsAdmVendedor AdmVen = new clsAdmVendedor();
        clsMoneda Mon = new clsMoneda();
        clsAdmMoneda AdmMoned = new clsAdmMoneda();
        clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();
        public clsFacturaVenta venta = new clsFacturaVenta();
        clsSerie ser = new clsSerie();
        clsAdmSerie Admser = new clsAdmSerie();
        clsConsultasExternas ext = new clsConsultasExternas();
        //*** Para la Carga de tarjetas y Bancos ***
        clsAdmBanco AdmBan = new clsAdmBanco();
        clsAdmTarjetaPago AdmTar = new clsAdmTarjetaPago();

        //******* Carga de las Cuentas Ctes ********
        clsAdmCtaCte AdmCtaCte = new clsAdmCtaCte();
        clsCtaCte CtaCte = new clsCtaCte();

        clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

        public String CodNota;
        public Int32 CodLetra;
        public Int32 tipo;
        Boolean tipopago;
        public Int32 Procede = 0; // (1)Venta

        public Int32 mon = 0;
        public Double Monto = 0;

        public Int32 CodSerie;

        public Int32 CodCliente;
        clsNotaIngreso notaI = new clsNotaIngreso();
        clsAdmNotaIngreso AdmNotaI = new clsAdmNotaIngreso();

        public frmCancelarPago()
        {
            InitializeComponent();
        }

        private void cargaMoneda()
        {
            cmbMoneda.DataSource = AdmMoned.ListaMonedas();
            cmbMoneda.DisplayMember = "descripcion";
            cmbMoneda.ValueMember = "codMoneda";
            cmbMoneda.SelectedIndex = -1;
        }

        //*** Para la Carga de tarjetas y Bancos ***

        private void CargarTarjetas()
        {
            try
            {
                cboTarjeta.DataSource = AdmTar.MuestraTarjetas(frmLogin.iCodAlmacen);
                cboTarjeta.DisplayMember = "tipo";
                cboTarjeta.ValueMember = "codtarjeta";
                cboTarjeta.SelectedIndex = -1;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        
        private void CargarBancos()
        {
            try
            {
                cboBanco.DataSource = AdmBan.MuestraBancos();
                cboBanco.DisplayMember = "descripcion";
                cboBanco.ValueMember = "codbanco";
                cboBanco.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void muestra_botones(Boolean activo)
        {
            label16.Visible = activo;
            txtSerie.Visible = activo;
        }

        private void posiciona_textbox()
        {
            txtSerie.Location = new Point(94, 15);
            txtNumero.Location = new Point(151, 15);
        }
        
        private void frmCancelarPago_Load(object sender, EventArgs e)
        {
            cargaMoneda();
            CargarBancos();
            CargarTarjetas();
            cboTarjeta.SelectedIndex = -1;
            cboBanco.SelectedIndex = -1;
            

            if (tipo == 1)
            {
                CargaFactura();
            }
            else if (tipo == 2)
            {
                CargaLetra();
            }
            else if (tipo == 3)
            {
                CargaVendedores();
                cbovendedor.Visible = true;
                CargaNotaSalida();
                muestra_botones(true);
                posiciona_textbox();
                
                //if (venta != null) 
                //{
                //    if (venta.FormaPago == 6)
                //    {
                //        cbovendedor.SelectedValue = 5;
                //    }
                //    else
                //    {
                //        cbovendedor.SelectedValue = venta.CodVendedor;
                //    }
                //}
            }
            else if (tipo == 4)
            {
                CargaVendedores();
                cbovendedor.Visible = true;
                CargaLetra();
                if (letra != null)
                {                    
                    cbovendedor.SelectedValue = 5;                   
                }
            }
            CargaMetodosPagos();
            cmbMetodoPago_SelectionChangeCommitted(cmbMetodoPago, null);
            //cmbMoneda.SelectedIndex = 0;
            Mon = AdmMoned.CargaMoneda(mon);
            if (tipo == 1 || tipo == 2)
            {
                if (Mon != null)
                {
                    txtMoneda.Text = Mon.SDescripcion;
                    tc = AdmTc.CargaTipoCambio(DateTime.Now.Date, 2);
                    if (tc != null)
                    {
                        txtTipoCambio.Text = tc.Venta.ToString();
                        txtTipoCambio.ReadOnly = true;
                    }
                    else
                    {
                        txtTipoCambio.Text = "";
                        txtTipoCambio.ReadOnly = false;
                    }
                    cmbMoneda.SelectedValue = Mon.IcodMoneda;
                }
            }
            else if(tipo == 3 || tipo == 4)
            {
                if (Mon != null)
                {
                    txtMoneda.Text = Mon.SDescripcion;
                    tc = AdmTc.CargaTipoCambio(DateTime.Now.Date, 2);
                    if (tc != null)
                    {
                        txtTipoCambio.Text = tc.Compra.ToString();
                        txtTipoCambio.ReadOnly = true;
                    }
                    else
                    {
                        txtTipoCambio.Text = "";
                        txtTipoCambio.ReadOnly = false;
                    }
                    cmbMoneda.SelectedValue = Mon.IcodMoneda;
                }
            }

        }

        private void CargaMetodosPagos()
        {
            cmbMetodoPago.DataSource = admMPago.CargaMetodoPagos();
            cmbMetodoPago.DisplayMember = "descripcion";
            cmbMetodoPago.ValueMember = "codMetodoPago";            
        }

        private void CargaVendedores()
        {
            cbovendedor.DataSource = AdmVen.MuestraVendedoresDestaque();
            cbovendedor.DisplayMember = "apellido";
            cbovendedor.ValueMember = "codVendedor";
            cbovendedor.SelectedIndex =0;
        }

        private void CargaFactura()
        {
            try
            {
                
                fac = Admfac.CargaFactura(Convert.ToInt32(CodNota));
                if (fac != null)
                {
                    txtDocumento.Text = fac.DocumentoFactura;
                    mon = fac.Moneda;
                    Mon=AdmMoned.CargaMoneda(mon);
                    txtMoneda.Text = Mon.SDescripcion;
                    txtMontoPendiente.Text = String.Format("{0:#,##0.00}", fac.Pendiente.ToString());   
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Cancelar Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void CargaLetra()
        {
            try
            {                
                letra = AdmLetra.CargaLetra(CodLetra);
                if (letra != null)
                {
                    txtDocumento.Text = letra.NumDocumento;
                    if (letra.CodMoneda == 1) { txtMoneda.Text = "NUEVOS SOLES"; } else { txtMoneda.Text = "DOLARES"; }
                    cmbMoneda.SelectedValue = letra.CodMoneda;
                    txtMontoPendiente.Text = String.Format("{0:#,##0.00}", letra.MontoPendiente);
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Cancelar Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void CargaNotaSalida()
        {
            try
            {
                venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodNota));
                Mon = AdmMoned.CargaMoneda(venta.Moneda);
                if (venta != null)
                {
                    mon = venta.Moneda;
                    txtDocumento.Text = venta.SiglaDocumento + " " + venta.Serie + " " + venta.NumDoc;
                    if (venta.Moneda == 1) { txtMoneda.Text = "NUEVOS SOLES"; } else { txtMoneda.Text = "DOLARES"; }
                    cmbMoneda.SelectedValue = venta.Moneda;
                    txtMontoPendiente.Text = String.Format("{0:#,##0.00}", venta.Pendiente);
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Cancelar Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {         
            if (superValidator1.Validate())
            {
                if (tipo == 1 || tipo == 2)//Pagos - salidas de dinero
                {
                    Pag.CodNota = fac.CodFactura.ToString();
                    Pag.CodLetra = letra.CodLetra;
                    Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue); //metodo de pago
                    Pag.CodMoneda =Convert.ToInt32(cmbMoneda.SelectedValue);
                    
                    Pag.Tipo = tipopago;// total o parcial
                    Pag.IngresoEgreso = false;//egreso
                    if (txtTipoCambio.Text == "") { Pag.TipoCambio = 0; } else { Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text); };
                    Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
                    Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
                    Pag.Vuelto = 0;
                    Pag.NOperacion = txtOperacion.Text;
                    Pag.NCheque = txtCheque.Text;
                    Pag.FechaPago = dtpFecha.Value;
                    Pag.Observacion = txtObservacion.Text;
                    Pag.CodUser = frmLogin.iCodUser;
                    Pag.CodAlmacen = frmLogin.iCodAlmacen;
                    Pag.CodSerie = 0;
                    Pag.Serie = "";
                    Pag.NumDoc = "";
                    if (Admpag.insert(Pag))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.Yes;
                        //this.Close();

                    }
                }
                else if (tipo == 3 || tipo == 4)//Cobros - ingresos de dinero
                {
                    Pag.CodNota = venta.CodFacturaVenta.ToString();
                    Pag.CodLetra = letra.CodLetra;
                    Pag.CodTipoPago = Convert.ToInt32(cmbMetodoPago.SelectedValue); //metodo de pago
                    Pag.CodMoneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                    Pag.CodCobrador = Convert.ToInt32(cbovendedor.SelectedValue); //Cobrador
                    Pag.Tipo = tipopago;// total o parcial
                    Pag.IngresoEgreso = true;//ingreso
                    if (txtTipoCambio.Text == "") { Pag.TipoCambio = 0; } else { Pag.TipoCambio = Convert.ToDecimal(txtTipoCambio.Text); };
                    Pag.MontoPagado = Convert.ToDecimal(txtMontoPago.Text);
                    Pag.MontoCobrado = Convert.ToDecimal(txtMontoPago.Text);
                    Pag.Vuelto = 0;
                    Pag.codCtaCte = Convert.ToInt32(cboNumCta.SelectedValue);
                    Pag.CtaCte = Convert.ToString(cboNumCta.Text);
                    Pag.NOperacion = txtOperacion.Text;
                    Pag.NCheque = txtCheque.Text;
                    Pag.FechaPago = dtpFecha.Value;
                    Pag.Observacion = txtObservacion.Text;
                    Pag.CodUser = frmLogin.iCodUser;
                    Pag.CodAlmacen = frmLogin.iCodAlmacen;
                    Pag.CodSerie = CodSerie;
                    Pag.Serie = txtSerie.Text;
                    Pag.NumDoc = txtNumero.Text;
                    Pag.Referencia = txtNc.Text;

                    //***** Datos para las operaciones de Venta ******
                    Pag.CodBanco = Convert.ToInt32(cboBanco.SelectedValue);
                    Pag.NOperacion = Convert.ToString(txtOperacion.Text.Trim());
                    Pag.CodTarjeta = Convert.ToInt32(cboTarjeta.SelectedValue);
                    Pag.NCheque = Convert.ToString(txtCheque.Text.Trim());
                    //************************************************


                    //********* Verificando campos faltantes **********
                    if (cmbMetodoPago.Text == "DEPOSITO" || cmbMetodoPago.Text == "TRANSFERENCIA")
                    {
                        if (txtOperacion.Text.Trim() == "" || cboBanco.Text == "")
                        {
                            MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else { Pagar(); btnImprimir.Visible = false;  }
                    }
                    else if (cmbMetodoPago.Text == "TARJETA")
                    {
                        if (txtOperacion.Text.Trim() == "" || cboTarjeta.Text == "")
                        {
                            MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else { Pagar(); }
                    }
                    else if (cmbMetodoPago.Text == "CHEQUE")
                    {
                        if (txtCheque.Text.Trim() == "" || cboBanco.Text == "")
                        {
                            MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else { Pagar(); btnImprimir.Visible = false;  }
                    }
                    else if (cmbMetodoPago.Text == "NOTA CREDITO")
                    {
                        if (txtNc.Text.Trim() == "")
                        {
                            MessageBox.Show("Ingresar Datos Necesarios", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Pagar();
                            notaI.CodReferencia = Convert.ToInt32(CodNota); // Nota Actual
                            AdmNotaI.ActualizaNCreditoVentaSinAplicar(notaI);
                        }
                    }
                    else
                    {
                        Pagar();
                    }
 
                }

            }
        }

        private void cargaPago(clsPago p)
        {
            p = Admpag.MuestraPagoVenta(frmLogin.iCodAlmacen, Pag.CodPago);
            if (p != null)
            {
                cmbMetodoPago.SelectedValue = p.CodTipoPago;
                cboBanco.SelectedValue = p.CodBanco;
                cboTarjeta.SelectedValue = p.CodTarjeta;
                cboNumCta.SelectedValue = p.codCtaCte;
                txtTipoCambio.Text = p.TipoCambio.ToString();
                txtCheque.Text = p.NCheque;
                txtObservacion.Text = p.Observacion;
                txtOperacion.Text = p.NOperacion;
                txtMontoPago.Text = p.MontoCobrado.ToString();
                dtpFecha.Value = p.FechaPago;
                cbovendedor.SelectedValue = p.CodCobrador;
                txtSerie.Text = p.Serie;
                txtNumero.Text = p.NumDoc;
            }
        }

        private void Deshabilita_botones(Boolean Estado)
        {
            cboBanco.Enabled = Estado;
            cboNumCta.Enabled = Estado;
            cboTarjeta.Enabled = Estado;
            cbovendedor.Enabled = Estado;
            txtCheque.Enabled = Estado;
            txtOperacion.Enabled = Estado;
            txtObservacion.Enabled = Estado;
            txtMontoPago.Enabled = Estado;
            dtpFecha.Enabled = Estado;
            btnAceptar.Enabled = Estado;
            btnCancelar.Enabled = Estado;
            txtSerie.Enabled = Estado;
            txtNumero.Enabled = Estado;
            txtNumero.Visible = !Estado;
        }

        private void Pagar()
        {
            try
            {
                if (Convert.ToInt32(cmbMetodoPago.SelectedValue) != 6 && Convert.ToInt32(cmbMetodoPago.SelectedValue) != 7 && Convert.ToInt32(cmbMetodoPago.SelectedValue) != 9)
                {
                    Pag.Aprobado = 2;
                }
                else
                {
                    Pag.Aprobado = 1;
                }
                   
                    if (Admpag.insert(Pag))
                    {
                        MessageBox.Show("Pago Realizado Correctamente", "Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnImprimir.Visible = true;
                        cargaPago(Pag);
                        Deshabilita_botones(false);
                        //if (Convert.ToDouble(txtMontoPendiente.Text).Equals(Convert.ToDouble(txtMontoPago.Text)))
                        //{
                        //    this.Close();
                        //}
                        //else
                        //{
                        //    txtMontoPendiente.Text = Convert.ToString(Convert.ToDouble(txtMontoPendiente.Text) - Convert.ToDouble(txtMontoPago.Text));
                        //    txtMontoPago.Text = "";
                        //    txtMontoPago.Focus();
                        //}
                    }
                    txtMontoPendiente.Text = Convert.ToString(Convert.ToDouble(txtMontoPendiente.Text) - Convert.ToDouble(txtMontoPago.Text));
                    //    txtMontoPago.Text = "";
                    //    txtMontoPago.Focus();
                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
            if (tc != null)
            {
                txtTipoCambio.Text = tc.Venta.ToString();
                txtTipoCambio.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTipoCambio.Text = "";
                txtTipoCambio.ReadOnly = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (Admpag.AnularPago(Pag.CodPago))
            //    {
            //        MessageBox.Show("Pago Cancelada", "Gestion de Pagos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        this.Close();
            //    }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            this.Close();
        }

        private void txtMontoPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.SOLONumeros(sender, e);
            
        }

        private void customValidator1_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {            
            if (e.ControlToValidate.Text != "")
                e.IsValid = true;
            else
                e.IsValid = false;            
        }

        private void frmCancelarPago_Shown(object sender, EventArgs e)
        {
            if (txtSerie.Visible) txtSerie.Focus();
            else txtMontoPago.Focus();
        }

        private void txtMontoPago_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMontoPago.Text != "")
                {
                    if (txtMontoPago.Text != "0")
                    {
                        if (Convert.ToDouble(txtMontoPago.Text) >= Convert.ToDouble(txtMontoPendiente.Text))
                        {
                            txtParcial.Text = "COBRO TOTAL";
                            tipopago = true;
                        }
                        else
                        {
                            txtParcial.Text = "COBRO PARCIAL";
                            tipopago = false;
                        }
                    }
                }
                else
                {
                    txtParcial.Text = "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void txtMontoPago_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Double box_double = 0;
                Double.TryParse(txtMontoPago.Text, out box_double);
                if (box_double > Convert.ToDouble(txtMontoPendiente.Text) && txtMontoPago.Text != "")
                {
                    txtMontoPago.Text = txtMontoPendiente.Text;
                    txtMontoPago.Select(txtMontoPago.Text.Length, 0);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void cmbMetodoPago_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 5)
                {
                    cboTarjeta.Enabled = false;
                    cboBanco.Enabled = false;
                    cboBanco.SelectedIndex = -1;
                    cboTarjeta.SelectedIndex = -1;
                    txtCheque.Text = "";
                    txtNc.Text = "";
                    txtMontoPago.Text = "";
                    txtOperacion.Text = "";
                    txtOperacion.Enabled = false;
                    txtCheque.Enabled = false;
                    txtMontoPago.Enabled = true;
                    cboNumCta.Enabled = false;
                    cboNumCta.SelectedIndex = -1;
                }
                else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 6 || Convert.ToInt32(cmbMetodoPago.SelectedValue) == 9)
                {
                    cboTarjeta.Enabled = false;
                    cboBanco.Enabled = true;
                    cboBanco.SelectedIndex = -1;
                    cboTarjeta.SelectedIndex = -1;
                    cboBanco.Focus();
                    txtCheque.Text = "";
                    txtOperacion.Text = "";
                    txtNc.Text = "";
                    txtMontoPago.Text = "";
                    txtOperacion.Enabled = true;
                    txtCheque.Enabled = false;
                    txtMontoPago.Enabled = true;
                    cboNumCta.Enabled = false;
                    cboNumCta.SelectedIndex = -1;
                }
                else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 7)
                {
                    cboTarjeta.Enabled = false;
                    cboBanco.Enabled = true;
                    cboBanco.Focus();
                    cboBanco.SelectedIndex = -1;
                    cboTarjeta.SelectedIndex = -1;
                    txtOperacion.Text = "";
                    txtNc.Text = "";
                    txtMontoPago.Text = "";
                    txtCheque.Text = "";
                    txtOperacion.Enabled = false;
                    txtCheque.Enabled = true;
                    txtMontoPago.Enabled = true;
                    cboNumCta.Enabled = false;
                    cboNumCta.SelectedIndex = -1;
                }
                else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 8)
                {
                    cboTarjeta.Enabled = true;
                    cboBanco.Enabled = false;
                    cboTarjeta.Focus();
                    cboBanco.SelectedIndex = -1;
                    cboTarjeta.SelectedIndex = -1;
                    txtOperacion.Text = "";
                    txtNc.Text = "";
                    txtMontoPago.Text = "";
                    txtCheque.Text = "";
                    txtOperacion.Enabled = true;
                    txtCheque.Enabled = false;
                    txtMontoPago.Enabled = true;
                    cboNumCta.Enabled = false;
                    cboNumCta.SelectedIndex = -1;
                }
                else if (Convert.ToInt32(cmbMetodoPago.SelectedValue) == 10)
                {
                    cboTarjeta.Enabled = false;
                    cboBanco.Enabled = false;
                    cboBanco.SelectedIndex = -1;
                    cboTarjeta.SelectedIndex = -1;
                    txtOperacion.Text = "";
                    txtCheque.Text = "";
                    txtNc.Text = "";
                    txtMontoPago.Text = "";
                    txtOperacion.Enabled = false;
                    txtCheque.Enabled = false;
                    txtNc.Enabled = false;
                    cboNumCta.Enabled = false;
                    txtMontoPago.Enabled = false;
                    cboNumCta.SelectedIndex = -1;
                    if (Application.OpenForms["frmListaNCreditosSinAplicar"] != null)
                    {
                        Application.OpenForms["frmListaNCreditosSinAplicar"].Activate();
                    }
                    else
                    {
                        frmListaNCreditosSinAplicar form = new frmListaNCreditosSinAplicar();
                        form.CodCliente = CodCliente;
                        form.ShowDialog();
                        notaI = form.nota;
                        txtNc.Text = notaI.NumDoc;
                        txtMontoPago.Text = notaI.Total.ToString();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void CargaCtaCte()
        {
            try
            {
                cboNumCta.DataSource = AdmCtaCte.ListaCtasBanco(Convert.ToInt32(cboBanco.SelectedValue),frmLogin.iCodAlmacen);
                cboNumCta.DisplayMember = "cuentaCorriente";
                cboNumCta.ValueMember = "codCuentaCorriente";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }

        }

        private void cboBanco_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbMetodoPago.Text == "DEPOSITO" || cmbMetodoPago.Text == "TRANSFERENCIA")
                {
                    cboNumCta.Enabled = true;
                    CargaCtaCte();
                }
                else
                {
                    cboNumCta.SelectedIndex = -1;
                    cboNumCta.Enabled = false;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void txtOperacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {//Para obligar a que sólo se introduzcan números 
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                    if (Char.IsControl(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void txtCheque_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {   //Para obligar a que sólo se introduzcan números 
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                    if (Char.IsControl(e.KeyChar))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            CRImpresionPago rpt = new CRImpresionPago();
            frmRptImpresionPago frm = new frmRptImpresionPago();
            CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rpt.PrintOptions;
            rptoption.PrinterName = ser.NombreImpresora;//Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3]);
            rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);//(CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3]), "documentoFioviza"); 
            rpt.SetDataSource(ds.ReporteImpresionPago(Pag.CodPago, frmLogin.iCodAlmacen));
            frm.cRVImpresionPago.ReportSource = rpt;
            frm.Show();
        }

        private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
        {
            VentaEnMoneda();
        }

        private void VentaEnMoneda()
        {
            Decimal TipoCambio = 0;

            TipoCambio = Convert.ToDecimal(txtTipoCambio.Text.Trim());
            if (mon == 1)
            {
                if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
                {
                    txtMontoPendiente.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(venta.Pendiente) / TipoCambio);
                }
                else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
                {
                    txtMontoPendiente.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(venta.Pendiente));
                }
            }
            else 
            {
                if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
                {
                    txtMontoPendiente.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(venta.Pendiente));
                }
                else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
                {
                    txtMontoPendiente.Text = String.Format("{0:#,##0.00}", Convert.ToDecimal(venta.Pendiente) * TipoCambio);
                }
            }
            
        }

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.Enabled)
                
                    if (c.SelectedIndex != -1)
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
                
                    if (c.SelectedIndex != -1)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                
            else
                e.IsValid = true;
        }

        private void customValidator4_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.Enabled)
                
                    if (c.SelectedIndex != -1)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                
            else
                e.IsValid = true;
        }

        private void customValidator5_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.Enabled)
                
                    if (c.SelectedIndex != -1)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                
            else
                e.IsValid = true;
        }

        private void customValidator6_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (e.ControlToValidate.Enabled)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator7_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (e.ControlToValidate.Enabled)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void txtSerie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmSerie"] != null)
                {
                    Application.OpenForms["frmSerie"].Activate();
                }
                else
                {
                    frmSerie form = new frmSerie();
                    form.DocSeleccionado = 18;
                    form.Sigla = "RC";
                    form.Proceso = 3;
                    form.ShowDialog();
                    ser = form.ser;
                    CodSerie = ser.CodSerie;
                    txtSerie.Text = ser.Serie;
                    if (CodSerie != 0)
                    {
                        ProcessTabKey(true);
                        if (txtSerie.Text == "") txtSerie.Focus();
                    }
                }
            }
        }

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (BuscaSerie())
                {
                    txtSerie.Text = ser.Serie;
                    if (ser.PreImpreso)
                    {
                        txtNumero.Text = "";
                        txtNumero.Visible = true;
                        txtNumero.Focus();
                    }
                    else
                    {
                        txtNumero.Text = "";
                        txtNumero.Visible = false;
                        txtNumero.Text = ser.Numeracion.ToString();
                    }

                    ProcessTabKey(true);
                }
                else
                {
                    MessageBox.Show("Serie no existe, Presione F1 para consultar la tabla de ayuda",
                                "Cancelar Pago", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private Boolean BuscaSerie()
        {
            ser = Admser.MuestraSerie(CodSerie, frmLogin.iCodAlmacen);

            if (ser != null)
            {
                CodSerie = ser.CodSerie;
                return true;
            }
            else
            {
                CodSerie = 0;
                return false;
            }

        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {
            if (BuscaSerie())
            {
                txtSerie.Text = ser.Serie;
                if (ser.PreImpreso)
                {
                    txtNumero.Text = "";
                    txtNumero.Visible = true;
                    txtNumero.Focus();
                }
                else
                {
                    txtNumero.Text = "";
                    txtNumero.Visible = false;
                    txtNumero.Text = ser.Numeracion.ToString();
                }
            }
        }

        private void txtSerie_TextChanged(object sender, EventArgs e)
        {
            txtNumero.Text = "";
            txtNumero.Visible = false;
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void txtNumero_Leave(object sender, EventArgs e)
        {
            if (txtNumero.Text == "" && txtNumero.Visible)
            {
                txtNumero.Focus();
            }
        }

        

       

        
    }
}
