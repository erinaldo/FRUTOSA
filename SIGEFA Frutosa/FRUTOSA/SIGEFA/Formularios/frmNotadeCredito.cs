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
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios
{
    public partial class frmNotadeCredito : DevComponents.DotNetBar.OfficeForm
    {
        //clsReporteNotaCredito ds = new clsReporteNotaCredito();
        clsNotasCreditoDebitoVenta ds = new clsNotasCreditoDebitoVenta();
        clsAdmTransaccion AdmTran = new clsAdmTransaccion();
        clsTransaccion tran = new clsTransaccion();
        clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();
        clsTipoDocumento doc = new clsTipoDocumento();
        clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();
        clsTipoCambio tc = new clsTipoCambio();
        clsAdmProveedor AdmProv = new clsAdmProveedor();
        clsProveedor prov = new clsProveedor();
        clsAdmCliente AdmCli = new clsAdmCliente();
        clsCliente cli = new clsCliente();
        clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();
        clsNotaSalida notaS = new clsNotaSalida();
        clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();
        clsNotaIngreso nota = new clsNotaIngreso();
        clsAdmFacturaVenta AdmVenta=new clsAdmFacturaVenta();
        clsFacturaVenta venta=new clsFacturaVenta();
        clsAdmAutorizado AdmAut = new clsAdmAutorizado();
        clsAutorizado aut = new clsAutorizado();
        clsAdmFormaPago AdmPago = new clsAdmFormaPago();
        clsFormaPago fpago = new clsFormaPago();
        clsValidar ok = new clsValidar();
        clsDetalleNotaIngreso detaSelec = new clsDetalleNotaIngreso();
        clsAdmProducto AdmPro = new clsAdmProducto();
        clsAdmSerie AdmSerie = new clsAdmSerie();
        clsSerie ser = new clsSerie();
        clsProducto pro = new clsProducto();
        clsConsultasExternas ext = new clsConsultasExternas();
        public List<Int32> config = new List<Int32>();
        public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();
        public String CodNota;
        public Int32 CodNotaS;
        public Int32 CodTransaccion;
        public Int32 CodProveedor;
        public Int32 CodCliente;
        public Int32 CodDocumento;
        public Int32 CodOrdenCompra;
        public Int32 CodAutorizado;
        Boolean Validacion = true;
        public Int32 Proceso = 0; //(1) Nuevo (2) Editar (3) Consulta
        public Int32 Tipo;
        clsAdmMoneda AdmMon = new clsAdmMoneda();

        //CODIGO NUEVO
        Int32 cantprod = 0;
        Decimal precprod = 0;

        public frmNotadeCredito()
        {
            InitializeComponent();
        }
               

        private void txtTransaccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtTransaccion.ReadOnly == false)
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (Application.OpenForms["frmTransacciones"] != null)
                    {
                        Application.OpenForms["frmTransacciones"].Activate();
                    }
                    else
                    {
                        frmTransacciones form = new frmTransacciones();
                        form.Proceso = 3;
                        form.ShowDialog();
                        tran = form.tran;
                        CodTransaccion = tran.CodTransaccion;
                        txtTransaccion.Text = tran.Sigla;
                        if (CodTransaccion != 0) { CargaTransaccion(); ProcessTabKey(true); } else { BorrarTransaccion(); }
                    }
                }
            }
        }

       

        private void CargaTransaccion() 
        {
            tran = AdmTran.MuestraTransaccion(CodTransaccion);
            tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
            txtTransaccion.Text = tran.Sigla;
            lbNombreTransaccion.Text = tran.Descripcion;
            lbNombreTransaccion.Visible = true;
            foreach (Control t in groupBox1.Controls)
            {
                if (t.Tag != null)
                {
                    Int32 con = Convert.ToInt32(t.Tag);
                    if (tran.Configuracion.Contains(con))
                    {
                        t.Visible = true;
                    }
                    else
                    {
                        t.Visible = false;
                    }
                }
            }
        }

        private void BorrarTransaccion()
        {
            txtTransaccion.Text = "";
            lbNombreTransaccion.Text = "";
            lbNombreTransaccion.Visible = false;
            foreach (Control t in groupBox1.Controls)
            {
                if (t.Tag != null)
                {                   
                    t.Visible = false;                    
                }
            }
        }

       

        private Boolean BuscaTransaccion()
        {
            tran = AdmTran.MuestraTransaccionS(txtTransaccion.Text, 0);
            if (tran != null)
            {
                CodTransaccion = tran.CodTransaccion;
                tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
                txtTransaccion.Text = tran.Sigla;
                lbNombreTransaccion.Text = tran.Descripcion;
                lbNombreTransaccion.Visible = true;
                foreach (Control t in groupBox1.Controls)
                {
                    if (t.Tag != null)
                    {
                        Int32 con = Convert.ToInt32(t.Tag);
                        if (tran.Configuracion.Contains(con))
                        {
                            t.Visible = true;
                        }
                        else
                        {
                            t.Visible = false;
                        }
                    }
                }
                return true;
            }
            else
            {
                lbNombreTransaccion.Text = "";
                lbNombreTransaccion.Visible = false;
                foreach (Control t in groupBox1.Controls)
                {
                    if (t.Tag != null)
                    {
                        t.Visible = false;                        
                    }
                }
                return false;
            }
        }

       

        private Boolean BuscaTipoDocumento()
        {
            doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
            if (doc != null)
            {
                CodDocumento = doc.CodTipoDocumento;
                return true;
            }
            else
            {               
                CodDocumento = 0;
                return false;
            }

        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        public void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtTransaccion.Text != "")
                {
                    if (BuscaTransaccion())
                    {
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("Codigo de transacción no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }
                }
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmDetalleIngreso"] != null)
            {
                Application.OpenForms["frmDetalleIngreso"].Activate();
            }
            else
            {
                frmDetalleIngreso form = new frmDetalleIngreso();
                //form.MdiParent = this;
                form.Procede = 7;
                form.Proceso = 1;
                form.bvalorventa = cbValorVenta.Checked;
                form.ShowDialog();
            }
        }

        private void VerificarCabecera()
        {
            if (CodTransaccion == 0 || CodDocumento == 0)
            {
                Validacion = false;
            }
            
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmDetalleIngreso"] != null)
            {
                Application.OpenForms["frmDetalleIngreso"].Activate();
            }
            else
            {
                frmDetalleIngreso form = new frmDetalleIngreso();
                //form.MdiParent = this;
                form.Procede = 7;
                form.Proceso = 1;
                form.bvalorventa = cbValorVenta.Checked;
                form.ShowDialog();
            }
        }

        private void CargaFormaPagos()
        {
            cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
            cmbFormaPago.DisplayMember = "descripcion";
            cmbFormaPago.ValueMember = "codFormaPago";
            cmbFormaPago.SelectedIndex = 0;

        }
        

        private void frmNotaIngreso_Load(object sender, EventArgs e)
        {
            CargaFormaPagos();
            CargaMoneda();
            tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date,2);
            if (Proceso == 1)
            {
                Bloqueabotones();
            }
            if (Proceso == 2)
            {
                CargaNotaIngreso();
            }
            else if (Proceso == 3)
            {
                CargaNotaIngreso();
                sololectura(true);
            }
            else if (Proceso == 4)
            {
                CargaNotaIngreso();
                sololectura(true);
            }
        }

        private void sololectura(Boolean estado)
        {            
            txtTransaccion.ReadOnly = estado;
            dtpFecha.Enabled = !estado;            
            cmbMoneda.Enabled = !estado;
            cmbFormaPago.Enabled = !estado;
            txtCodCliente.ReadOnly = estado;
            txtCodCliente.Enabled = !estado;
            txtDocRef.ReadOnly = estado;
            txtDocRef.Enabled = !estado;
            txtComentario.ReadOnly = estado;           
            txtBruto.ReadOnly = estado;
            txtDscto.ReadOnly = estado;
            txtValorVenta.ReadOnly = estado;
            txtIGV.ReadOnly = estado;
            txtPrecioVenta.ReadOnly = estado;
            btnNuevo.Visible = !estado;
            btnEditar.Visible = !estado;
            btnEliminar.Visible = !estado;
            btnGuardar.Visible = !estado;
            btnImprimir.Visible = estado;
            btnNuevaGuia.Visible = estado;
            cmbMotivo.Enabled = !estado;
            cbAplicada.Enabled = !estado;
        }

        private void Bloqueabotones()
        {
            //btnNuevo.Visible = false;
            //btnEditar.Visible = false;
            //btnEliminar.Visible = false;
        }

        private void CargaNotaIngreso()
        {
            try
            {
                nota = AdmNota.CargaNotaIngreso(Convert.ToInt32(CodNota));
                ser = AdmSerie.MuestraSerie(nota.CodSerie, frmLogin.iCodAlmacen);
                if (nota != null)
                {
                    if (nota.CodReferencia != 0) { notaS = AdmNotaS.CargaNotaSalidaCreditoVentas(Convert.ToInt32(nota.CodReferencia)); }
                    txtNumDoc.Text = nota.CodNotaIngreso;
                    CodTransaccion = nota.CodTipoTransaccion;
                    CargaTransaccion();
                    
                    CodCliente = notaS.CodCliente;
                    CargaCliente();

                    dtpFecha.Value = nota.FechaIngreso;
                    cmbMoneda.SelectedValue = nota.Moneda;
                    txtTipoCambio.Text = nota.TipoCambio.ToString();
                    cmbFormaPago.SelectedValue = nota.FormaPago;
                    cmbMotivo.SelectedItem = nota.Comentario.ToString();
                    cbAplicada.Checked = Convert.ToBoolean(nota.Aplicada);

                    if (txtDocRef.Enabled)
                    {
                        CodDocumento = nota.CodTipoDocumento;
                        txtDocRef.Text = notaS.SiglaDocumento + " " + notaS.Serie + " " + notaS.NumDoc;                 
                        //BuscaTipoDocumento();
                        //doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
                        //if (doc != null)
                        //{
                        //    CodDocumento = doc.CodTipoDocumento;
                        //}
                    }                   
                    cmbFormaPago.SelectedValue = nota.FormaPago;
                    dtpFechaPago.Value = nota.FechaPago;
                    //txtComentario.Text = nota.Comentario;
                    txtBruto.Text = String.Format("{0:#,##0.00}", nota.MontoBruto);
                    txtDscto.Text = String.Format("{0:#,##0.00}", nota.MontoDscto);
                    txtValorVenta.Text = String.Format("{0:#,##0.00}", nota.Total - nota.Igv);
                    txtIGV.Text = String.Format("{0:#,##0.00}", nota.Igv);
                    txtPrecioVenta.Text = String.Format("{0:#,##0.00}", nota.Total);
                    CargaDetalle();
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void CargaDetalle()
        {
            dgvDetalle.DataSource = AdmNota.CargaDetalle(Convert.ToInt32(nota.CodNotaIngreso));
            RecorreDetalle();
            nota.Detalle = detalle;
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (txtTipoCambio.Visible)
            {
                tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
                if (tc != null)
                {
                    txtTipoCambio.Text = tc.Venta.ToString();
                }
                else
                {
                    MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFecha.Value = DateTime.Now.Date;
                    dtpFecha.Focus();
                }
            }
        }

       

        private void frmNotaIngreso_Shown(object sender, EventArgs e)
        {
            txtTransaccion.Focus();
            txtTransaccion.Text = "NCV";
            KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
            txtTransaccion_KeyPress(txtTransaccion, ee);
            ser = AdmSerie.BuscaSeriexDocumento(4, frmLogin.iCodAlmacen);
            txtCodCliente.Focus();
            if (Proceso == 1)
            {
                if (txtTipoCambio.Visible)
                {
                    if (tc == null)
                    {
                        MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        txtTipoCambio.Text = tc.Venta.ToString();
                    }
                }
            }
        }

        private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmListaDocumentosPorCliente"] != null)
                {
                    Application.OpenForms["frmListaDocumentosPorCliente"].Activate();
                }
                else
                {
                    frmListaDocumentosPorCliente form = new frmListaDocumentosPorCliente();
                    form.Text = "Documentos";
                    form.tipo = 1;
                    form.CodCliente = CodCliente;
                    form.ShowDialog();
                    if (form.venta != null && form.venta.CodFacturaVenta != "") { venta = form.venta; CodNotaS = Convert.ToInt32(venta.CodFacturaVenta); } else { }
                    if (CodNotaS != 0) { CargaNotaSalida(); ProcessTabKey(true); }
                }
            }          
        }

        private void CargaNotaSalida()
        {
            try
            {
                venta = AdmVenta.CargaFacturaVenta(CodNotaS);
                if (venta != null)
                {
                    txtDocRef.Text = venta.SiglaDocumento + " - " + venta.Serie + " - " + venta.NumDoc;

                    if (txtCodCliente.Enabled)
                    {
                        CodCliente = venta.CodCliente;
                        cli = AdmCli.MuestraCliente(CodCliente);
                        txtCodCliente.Text = cli.RucDni;
                        txtNombreCliente.Text = cli.Nombre;
                        cmbFormaPago.SelectedValue = venta.FormaPago;
                    }
                                        
                    CargaDetalleNota();
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void BorrarNota()
        {
            try
            {
                CodNotaS = 0;
                notaS = new clsNotaSalida();
                txtDocRef.Text = "";
                
                DataTable dt = (DataTable)dgvDetalle.DataSource;
                dt.Clear();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void CargaDetalleNota()
        {            
            //dgvDetalle.DataSource = AdmVenta.CargaDetalle(CodNotaS,frmLogin.iCodAlmacen);
            dgvDetalle.DataSource = AdmVenta.CargaDetalleVentaCredito(CodNotaS, frmLogin.iCodAlmacen);
            
            cantprod = Convert.ToInt32(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
            precprod = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);

            dgvDetalle.Columns["stockdisponible"].Visible = false;
            dgvDetalle.Columns["maxPorcDescto"].Visible = false;
        }

        //private void CargaDocumento()
        //{
        //    doc = Admdoc.BuscaTipoDocumento(doc.CodTipoDocumento);
        //    txtDocRef.Text = doc.Sigla;            
        //}

        //private void BorrarDocumento()
        //{
        //    doc = Admdoc.BuscaTipoDocumento(doc.CodTipoDocumento);
        //    txtDocRef.Text = "";
        //}

        private void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void txtDocRef_Leave(object sender, EventArgs e)
        {            
            if (CodNotaS == 0)
            {
                txtDocRef.Focus();
            }
        }

        private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (Proceso == 1)
            {
                CalculaTotales();
            }
        }

        private void CalculaTotales()
        {
            Double bruto = 0;
            Double descuen = 0;
            Double valor = 0;
            Double igvt = 0;
            Double preciot = 0;
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                bruto = bruto + Convert.ToDouble(row.Cells[importe.Name].Value);
                descuen = descuen + Convert.ToDouble(row.Cells[montodscto.Name].Value);
                valor = valor + Convert.ToDouble(row.Cells[valorventa.Name].Value);
                igvt = igvt + Convert.ToDouble(row.Cells[igv.Name].Value);
                preciot = preciot + Convert.ToDouble(row.Cells[precioventa.Name].Value);
            }
            txtBruto.Text = String.Format("{0:#,##0.00}", bruto);
            txtDscto.Text = String.Format("{0:#,##0.00}", descuen);
            txtValorVenta.Text = String.Format("{0:#,##0.00}", valor);
            txtIGV.Text = String.Format("{0:#,##0.00}", igvt);
            txtPrecioVenta.Text = String.Format("{0:#,##0.00}", preciot);
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void dtpFecha_Leave(object sender, EventArgs e)
        {
            if (CodTransaccion == 0)
            {
                dtpFecha.Focus();
            }
        }

        private void cmbMoneda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void cmbMoneda_Leave(object sender, EventArgs e)
        {
            if (CodTransaccion == 0)
            {
                cmbMoneda.Focus();
            }
        }

       

        private void txtNDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        

        private void txtAutorizacion_Leave(object sender, EventArgs e)
        {
            VerificarCabecera();
            if (Validacion && Proceso == 1)
            {
                btnDetalle.Enabled = true;
            }
        }

        private void txtTransaccion_Leave(object sender, EventArgs e)
        {
            if (CodTransaccion == 0)
            {
                txtTransaccion.Focus();
            }
        }

        private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
        {
            if (txtPrecioVenta.Text != "")
            {
                btnGuardar.Enabled = true;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Proceso != 0)
            {
                if (cmbMotivo.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccionar un motivo!", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbMotivo.Focus();
                }
                else
                {
                    if (dgvDetalle.Rows.Count > 0)
                    {
                        nota.CodAlmacen = frmLogin.iCodAlmacen;
                        nota.CodTipoTransaccion = tran.CodTransaccion;
                        nota.CodTipoDocumento = 4; // codigo de documento nota de credito  
                        nota.CodSerie = ser.CodSerie; // codigo de serie de la nota de credito
                        nota.Serie = ser.Serie; // serie de la nota de credito
                        if (CodNotaS != 0) { nota.CodReferencia = CodNotaS; }
                        nota.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                        //if (txtTipoCambio.Visible) { nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text); }
                        nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text); //}
                        nota.FechaIngreso = dtpFecha.Value.Date;
                        nota.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                        nota.FechaPago = dtpFechaPago.Value.Date;
                        if (fpago.Dias == 0)
                        {
                            nota.FechaCancelado = dtpFecha.Value.Date;
                            nota.Cancelado = 1;// Para saber si la nota esta pendiente de pago o esta cancelada
                        }

                        nota.Aplicada = 0;
                        if (cbAplicada.Checked)
                        {
                            nota.Aplicada = 1;
                            nota.CodAplicada = nota.CodReferencia;
                        }
                        //nota.Comentario = txtComentario.Text;
                        //nota.MontoBruto = Convert.ToDouble(txtBruto.Text);
                        nota.MontoBruto = Convert.ToDouble(txtValorVenta.Text);
                        nota.MontoDscto = Convert.ToDouble(txtDscto.Text);
                        nota.Igv = Convert.ToDouble(txtIGV.Text);
                        nota.Total = Convert.ToDouble(txtPrecioVenta.Text);
                        nota.CodUser = frmLogin.iCodUser;
                        nota.Estado = 1;// Para saber si la nota esta activa o anulada. El estado se podra cambiar en una ventana especifica para anular notas
                        nota.Comentario = "";
                        if (Proceso == 1)
                        {
                            if (AdmNota.VerificarNCVentaAplicada(nota))
                            {
                                if (!nota.Comentario.Equals(""))
                                {
                                    DialogResult dlgResult = MessageBox.Show(nota.Comentario, "Nota Crédito Ventas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dlgResult == DialogResult.Yes)
                                    {
                                        nota.Aplicada = 0;
                                        nota.Comentario = cmbMotivo.SelectedItem.ToString();
                                        if (AdmNota.insert(nota))
                                        {
                                            RecorreDetalle();
                                            if (detalle.Count > 0)
                                            {
                                                foreach (clsDetalleNotaIngreso det in detalle)
                                                {
                                                    AdmNota.insertdetalle(det);
                                                }
                                            }
                                            AdmVenta.ActualizaPendienteCreditoVenta(nota.Total, nota.CodReferencia, nota.CodAlmacen);
                                            MessageBox.Show("Los datos se guardaron correctamente", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            CodNota = nota.CodNotaIngreso;
                                            CargaNotaIngreso();
                                            sololectura(true);
                                        }
                                    }
                                }
                                else
                                {
                                    nota.Comentario = cmbMotivo.SelectedItem.ToString();
                                    if (AdmNota.insert(nota))
                                    {
                                        RecorreDetalle();
                                        if (detalle.Count > 0)
                                        {
                                            foreach (clsDetalleNotaIngreso det in detalle)
                                            {
                                                AdmNota.insertdetalle(det);
                                            }
                                        }
                                        AdmVenta.ActualizaPendienteCreditoVenta(nota.Total, nota.CodReferencia, nota.CodAlmacen);
                                        MessageBox.Show("Los datos se guardaron correctamente", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        CodNota = nota.CodNotaIngreso;
                                        CargaNotaIngreso();
                                        sololectura(true);
                                    }
                                }
                            }
                        }
                        else if (Proceso == 2)
                        {
                            if (AdmNota.update(nota))
                            {
                                RecorreDetalle();
                                foreach (clsDetalleNotaIngreso det in nota.Detalle)
                                {
                                    foreach (clsDetalleNotaIngreso det1 in detalle)
                                    {
                                        if (det.CodDetalleIngreso == det1.CodDetalleIngreso)
                                        {
                                            AdmNota.updatedetalle(det1);
                                        }
                                    }
                                    AdmNota.deletedetalle(det.CodDetalleIngreso);
                                }
                                foreach (clsDetalleNotaIngreso deta in detalle)
                                {
                                    if (deta.CodDetalleIngreso == 0)
                                    {
                                        AdmNota.insertdetalle(deta);
                                    }
                                }

                                MessageBox.Show("Los datos se actualizaron correctamente", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        CargaNotaSalida();
                    }
                }
            }
        }

        private void RecorreDetalle()
        {
            detalle.Clear();
            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    añadedetalle(row);
                }
            }
            //nota.Detalle = detalle;
        }
        private void añadedetalle(DataGridViewRow fila)
        {
            clsDetalleNotaIngreso deta = new clsDetalleNotaIngreso();
            deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            deta.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
            deta.CodAlmacen = frmLogin.iCodAlmacen;
            deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
            deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
            deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
            //deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
            deta.Subtotal = Convert.ToDouble(fila.Cells[valorventa.Name].Value);
            deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
            deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
            deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
            deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
            deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
            deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
            deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
            deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
            deta.FechaIngreso = dtpFecha.Value;
            deta.CodUser = frmLogin.iCodUser;
            detalle.Add(deta);
        }

        private void CargaFilaDetalle(DataGridViewRow fila)
        {            
            detaSelec.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            detaSelec.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
            detaSelec.CodAlmacen = frmLogin.iCodAlmacen;
            detaSelec.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
            detaSelec.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            detaSelec.SerieLote = fila.Cells[serielote.Name].Value.ToString();
            detaSelec.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            detaSelec.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
            detaSelec.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
            detaSelec.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
            detaSelec.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
            detaSelec.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
            detaSelec.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
            detaSelec.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
            detaSelec.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
            detaSelec.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
            detaSelec.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
            detaSelec.FechaIngreso = dtpFecha.Value;
            detaSelec.CodUser = frmLogin.iCodUser;            
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0 & dgvDetalle.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDetalle.SelectedRows[0];
                if (Application.OpenForms["frmDetalleIngreso"] != null)
                {
                    Application.OpenForms["frmDetalleIngreso"].Activate();
                }
                else
                {
                    frmDetalleIngreso form = new frmDetalleIngreso();
                    form.Proceso = 2;
                    form.Procede = 7;
                    form.bvalorventa = cbValorVenta.Checked;
                    form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
                    form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
                    form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
                    form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
                    form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
                    form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
                    form.txtDscto2.Text = row.Cells[dscto2.Name].Value.ToString();
                    form.txtDscto3.Text = row.Cells[dscto3.Name].Value.ToString();
                    form.txtPrecioNeto.Text = row.Cells[importe.Name].Value.ToString();
                    form.ShowDialog();
                }
            }            
        }
                
        private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (!this.Visible)
                return;
            if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
            {
                CargaFilaDetalle(e.Row);                
            }            
        }

        

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
            }
        }

        private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (Proceso == 1)
            {
                CalculaTotales();
            }
        }

        private void txtCodCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmClientesLista"] != null)
                {
                    Application.OpenForms["frmClientesLista"].Activate();
                }
                else
                {
                    frmClientesLista form = new frmClientesLista();
                    form.Proceso = 3;
                    //form.Tipo = cmbTipoCodigo.SelectedIndex;
                    form.ShowDialog();
                    cli = form.cli;
                    CodCliente = cli.CodCliente;
                    if (CodCliente != 0) { CargaCliente(); BorrarNota(); ProcessTabKey(true); }
                }
            }
        }

        private void CargaCliente()
        {
            cli = AdmCli.MuestraCliente(CodCliente);
            if (cli != null)
            {
                txtCodCliente.Text = cli.CodigoPersonalizado;
                txtNombreCliente.Text = cli.RazonSocial;
                txtDireccionCliente.Text = cli.DireccionLegal;
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ok.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtCodCliente.Text != "")
                {
                    if (BuscaCliente())
                    {
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("El Cliente no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE SALIDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private Boolean BuscaCliente()
        {
            cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
            if (cli != null)
            {
                txtCodCliente.Text = cli.CodigoPersonalizado;
                txtNombreCliente.Text = cli.RazonSocial;
                CodCliente = cli.CodCliente;               
                return true;
            }
            else
            {
                txtNombreCliente.Text = "";
                CodCliente = 0;
                return false;
            }
        }

        private void txtCodCliente_Leave(object sender, EventArgs e)
        {
            if (CodCliente == 0)
            {
                txtCodCliente.Focus();
            }
        }

        private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
            dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias); 
        }

        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Double cantidad1, precio, bruto, montodescuento, valorventa1, igv1, precioventa1, precioreal1, valorreal, factorigv, dsc1, dsc2, dsc3, preunitario;
            try
            {
                if (cmbMotivo.SelectedIndex == 0)
                {
                    if (cantprod < Convert.ToInt32(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value))
                    {
                        MessageBox.Show("La cantidad del producto debe ser menor");
                        dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = Convert.ToString(cantprod);
                    }
                }

                else if (cmbMotivo.SelectedIndex == 2)
                {
                    if (precprod < Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value))
                    {
                        MessageBox.Show("El precio descuento debe ser menor");
                        dgvDetalle.CurrentRow.Cells[preciounit.Name].Value = Convert.ToString(precprod);
                    }
                }                
                //if (dgvDetalle.Focused && e.ColumnIndex == dgvDetalle.Columns[cantidad.Name].Index)
                if (dgvDetalle.Focused)
                {
                    pro = AdmPro.CargaProducto(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen);
                    cantidad1 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
                    precio = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
                    bruto = cantidad1 * precio;
                    dsc1 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[dscto1.Name].Value);
                    dsc2 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[dscto2.Name].Value);
                    dsc3 = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[dscto3.Name].Value);
                    precioventa1 = bruto * (1 - (dsc1 / 100)) * (1 - (dsc2 / 100)) * (1 - (dsc3 / 100));
                    montodescuento = bruto - precioventa1;
                    if (pro.ConIgv)
                    {
                        //DEBE TOMAR EL DATO DE IGV DE LA CONFIGURACION DEL SISTEMA                        
                        factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                        valorventa1 = precioventa1 / factorigv;
                    }
                    else
                    {
                        valorventa1 = precioventa1;
                    }
                    //precioreal1 = precioventa1 / cantidad1;
                    //valorreal = valorventa1 / cantidad1;
                    igv1 = precioventa1 - valorventa1;

                    dgvDetalle.CurrentRow.Cells[importe.Name].Value = bruto;
                    dgvDetalle.CurrentRow.Cells[montodscto.Name].Value = montodescuento;
                    dgvDetalle.CurrentRow.Cells[valorventa.Name].Value = valorventa1;
                    dgvDetalle.CurrentRow.Cells[igv.Name].Value = igv1;
                    dgvDetalle.CurrentRow.Cells[precioventa.Name].Value = precioventa1;
                    //dgvDetalle.CurrentRow.Cells[precioreal.Name].Value = precioreal1;
                    //dgvDetalle.CurrentRow.Cells[valoreal.Name].Value = valorreal;
                    CalculaTotales();
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                ser = AdmSerie.MuestraSerie(nota.CodSerie, frmLogin.iCodAlmacen);
                CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rd.Load("CRNotaCreditoVenta.rpt");
                CRNotaCreditoVenta rpt = new CRNotaCreditoVenta();
                rd.SetDataSource(ds.ReportNotaCreditoVenta(Convert.ToInt32(nota.CodNotaIngreso), frmLogin.iCodAlmacen));
                CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rd.PrintOptions;
                rptoption.PrinterName = ser.NombreImpresora;
                rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
                rptoption.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(530, 2900, 70, 500));
                //CrystalDecisions.Shared.PageMargins margenes = rd.PrintOptions.PageMargins;
                rd.PrintToPrinter(1, false, 1, 1);
                rd.Close();
                rd.Dispose();


                //CRNotaCreditoVenta rpt = new CRNotaCreditoVenta();
                //frmRptNotaCredito frm = new frmRptNotaCredito();
                //rpt.SetDataSource(ds.ReportNotaCreditoVenta(Convert.ToInt32(nota.CodNotaIngreso), frmLogin.iCodAlmacen).Tables[0]);
                //CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rpt.PrintOptions;
                //rptoption.PrinterName = ser.NombreImpresora;//Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3]);
                //rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
                //rptoption.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(530, 2900, 70, 500));
                //frm.crvNotaCredito.ReportSource = rpt;
                //frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevaGuia_Click(object sender, EventArgs e)
        {
            frmNotadeCredito form2 = new frmNotadeCredito();
            form2.MdiParent = this.MdiParent;
            form2.Proceso = 1;
            form2.Show();
            this.Close();
        }

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                if (Proceso != 3)
                {
                    if (cmbMotivo.SelectedIndex == 1)
                    {
                        dgvDetalle.CurrentRow.Cells[cantidad.Name].ReadOnly = true;
                        dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = true;
                    }
                    btnEliminar.Visible = true;
                }
            }
        }

        private void cmbMotivo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                if (cmbMotivo.SelectedIndex == 0)
                {
                    dgvDetalle.Columns["cantidad"].ReadOnly = false;
                    dgvDetalle.Columns["preciounit"].ReadOnly = true;
                }
                else if (cmbMotivo.SelectedIndex == 2)
                {
                    dgvDetalle.Columns["cantidad"].ReadOnly = true;
                    dgvDetalle.Columns["preciounit"].ReadOnly = false;
                }
                else if (cmbMotivo.SelectedIndex == 1 || cmbMotivo.SelectedIndex == 3)
                {
                    dgvDetalle.Columns["cantidad"].ReadOnly = true;
                    dgvDetalle.Columns["preciounit"].ReadOnly = true;
                }
                CargaNotaSalida();
            }
        }

        private void CargaMoneda()
        {
            cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
            cmbMoneda.DisplayMember = "descripcion";
            cmbMoneda.ValueMember = "codMoneda";
            cmbMoneda.SelectedIndex = 0;
        }
    }
}
