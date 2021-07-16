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
    public partial class frmNotadeCreditoCompra : DevComponents.DotNetBar.OfficeForm
    {
        clsReporteNotaCredito ds = new clsReporteNotaCredito();
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
        clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();
        clsNotaIngreso nota = new clsNotaIngreso();
        clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();
        clsNotaSalida notaS = new clsNotaSalida();
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
        public List<clsDetalleNotaSalida> detalleS = new List<clsDetalleNotaSalida>();
        public String CodNota;
        public Int32 CodNotaS;
        public Int32 CodNotaI;
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
        clsAdmFactura AdmFactura = new clsAdmFactura();
        clsFactura factur = new clsFactura();
        public Int32 CodFactura;

        public frmNotadeCreditoCompra()
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
            label3.Visible = true;


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
            tran = AdmTran.MuestraTransaccionS(txtTransaccion.Text, 1);
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
                label3.Visible = true;
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
            cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(0);
            cmbFormaPago.DisplayMember = "descripcion";
            cmbFormaPago.ValueMember = "codFormaPago";
            cmbFormaPago.SelectedIndex = 0;

        }
        private void CargaMoneda()
        {
            cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
            cmbMoneda.DisplayMember = "descripcion";
            cmbMoneda.ValueMember = "codMoneda";
            //cmbMoneda.SelectedIndex = 0;
            cmbMoneda.SelectedValue = 1;
        }  

        private void frmNotaIngreso_Load(object sender, EventArgs e)
        {
            //cmbMoneda.SelectedValue = 1;
            CargaMoneda();
            CargaFormaPagos();
            
            tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, Convert.ToInt32(cmbMoneda.SelectedValue));
            if (Proceso == 1)
            {
                Bloqueabotones();
            }
            if (Proceso == 2)
            {
                CargaNotaSalida();
                //CargaFacturaGrid();
            }
            else if (Proceso == 3)
            {
                //CargaNotaIngreso();
                CargaNotaSalida();
                //CargaFacturaGrid();
                sololectura(true);
            }
            else if (Proceso == 4)
            {
                CargaNotaSalida();
                //CargaFacturaGrid();
                sololectura(true);
            }
        }

        

        private void sololectura(Boolean estado)
        {            
            txtTransaccion.ReadOnly = estado;
            dtpFecha.Enabled = !estado;            
            cmbMoneda.Enabled = !estado;
            cmbFormaPago.Enabled = !estado;
            txtCodProveedor.ReadOnly = estado;
            txtDocRef.ReadOnly = !estado;  
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
        }

        private void Bloqueabotones()
        {
            //txtDocRef = false;
            //btnEditar.Visible = false;
            //btnEliminar.Visible = false;
        }

        private void CargaNotaIngreso()
        {
            try
            {
                nota = AdmNota.CargaNotaIngreso(Convert.ToInt32(CodNota));
                ser = AdmSerie.MuestraSerie(nota.CodSerie,frmLogin.iCodAlmacen);
                if (nota != null)
                {
                    if (nota.CodReferencia != 0) { notaS = AdmNotaS.CargaNotaSalida(nota.CodReferencia); }

                    txtNumDoc.Text = nota.CodNotaIngreso;
                    CodTransaccion = nota.CodTipoTransaccion;
                    CargaTransaccion();
                    
                    CodCliente = notaS.CodCliente;
                    CargaCliente();

                    dtpFecha.Value = nota.FechaIngreso;
                    cmbMoneda.SelectedIndex = nota.Moneda;
                    txtTipoCambio.Text = nota.TipoCambio.ToString();
                    cmbFormaPago.SelectedValue = nota.FormaPago;
                    
                    if (txtDocRef.Enabled)
                    {
                        CodDocumento = nota.CodTipoDocumento;
                        txtDocRef.Text = nota.SiglaDocumento;                       
                        BuscaTipoDocumento();
                        //doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
                        //if (doc != null)
                        //{
                        //    CodDocumento = doc.CodTipoDocumento;
                        //}
                    }                   
                    cmbFormaPago.SelectedValue = nota.FormaPago;
                    dtpFechaPago.Value = nota.FechaPago;
                    txtComentario.Text = nota.Comentario;
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
                tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, Convert.ToInt32(cmbMoneda.SelectedValue));
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
            txtTransaccion.Text = "NCC";
            KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
            txtTransaccion_KeyPress(txtTransaccion, ee);
            txtCodProveedor.Focus();
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
                if (Application.OpenForms["frmListaFacturasPorProveedor"] != null)
                {
                    Application.OpenForms["frmListaFacturasPorProveedor"].Activate();
                }
                else
                {
                    frmListaFacturasPorProveedor form = new frmListaFacturasPorProveedor();
                    form.CodProveedor = CodProveedor;
                    form.ShowDialog();
                    //if (form.nota != null && form.nota.CodNotaIngreso != "") { nota = form.nota; CodNotaI = Convert.ToInt32(nota.CodNotaIngreso); } else { }
                    if (form.factura != null && form.factura.CodFactura != 0) { factur = form.factura; CodFactura = Convert.ToInt32(factur.CodFactura); } else { }
                    //if (CodNotaI != 0) { CargaNotaIngresoGrid(); ProcessTabKey(true); }
                    if (CodFactura != 0) { CargaFacturaGrid(); ProcessTabKey(true); }
                }
            }          
        }

        private void CargaFacturaGrid()
        {
            try
            {
                //nota = AdmNota.CargaNotaIngreso(CodNotaI);
                factur = AdmFactura.CargaFactura(CodFactura);
                //if (nota != null)
                if (factur != null)
                {
                    //txtDocRef.Text = nota.SiglaDocumento + " - " + nota.NumDoc;
                    txtDocRef.Text = factur.Serie+"-"+ factur.DocumentoFactura;

                    if (txtCodProveedor.Enabled)
                    {
                        //CodCliente = notaS.CodCliente;
                        //cli = AdmCli.MuestraCliente(CodCliente);
                        //txtCodProveedor.Text = cli.CodigoPersonalizado;
                        //txtNombreProveedor.Text = cli.Nombre;
                        cmbFormaPago.SelectedValue = factur.FormaPago;
                    }

                    //CargaDetalleNota();
                    CargaDetalleFactura();
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

        private void CargaDetalleFactura()
        {
            dgvDetalle.DataSource = AdmFactura.CargaDetalle(Convert.ToInt32(factur.CodFactura));
            CalculaTotales();
        }

        private void CargaNotaSalida()
        {
            try
            {
                notaS = AdmNotaS.CargaNotaSalidaCredito(CodNotaS);
                if (notaS != null)
                {
                    //txtDocRef.Text = notaS.SiglaDocumento + " - " + notaS.Serie + " - " + notaS.NumDoc;
                    txtDocRef.Text = notaS.Docref ;
                    txtSerie.Text = notaS.Serie;
                    txtNumDoc.Text = notaS.NumDoc;
                    cmbMoneda.SelectedValue = notaS.Moneda;
                    txtCodProveedor.Text = notaS.RUCCliente;
                    txtNombreProveedor.Text = notaS.RazonSocialCliente;
                    cmbMotivo.SelectedItem = notaS.Comentario;


                    if (txtCodProveedor.Enabled)
                    {
                        //CodCliente = notaS.CodCliente;
                        //cli = AdmCli.MuestraCliente(CodCliente);
                        //txtCodProveedor.Text = cli.CodigoPersonalizado;
                        //txtNombreProveedor.Text = cli.Nombre;
                        cmbFormaPago.SelectedValue = notaS.FormaPago;
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
        private void CargaNotaIngresoGrid()
        {
            try
            {
                nota = AdmNota.CargaNotaIngreso(CodNotaI);
                if (nota != null)
                {
                    txtDocRef.Text = nota.SiglaDocumento +" - " + nota.NumDoc;

                    if (txtCodProveedor.Enabled)
                    {
                        //CodCliente = notaS.CodCliente;
                        //cli = AdmCli.MuestraCliente(CodCliente);
                        //txtCodProveedor.Text = cli.CodigoPersonalizado;
                        //txtNombreProveedor.Text = cli.Nombre;
                        cmbFormaPago.SelectedValue = nota.FormaPago;
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
            dgvDetalle.DataSource = AdmNotaS.CargaDetalle(Convert.ToInt32(notaS.CodNotaSalida));
            CalculaTotales();
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
            if (CodFactura == 0)
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
            //MODIFICACION ALEX
            else if (CodTransaccion != 7)
            {
                txtNumDoc.Enabled = true;
                txtNumDoc.ReadOnly = false;
                txtSerie.Enabled = true;
                txtSerie.ReadOnly = false;
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
            if (Proceso != 0 )
            {
                if (cmbMotivo.SelectedIndex==-1)
                {
                    MessageBox.Show("Porfavor seleccionar un motivo!");
                    cmbMotivo.Focus();
                }
                else
                {
                    
                    notaS.CodAlmacen = frmLogin.iCodAlmacen;
                    notaS.CodSucursal = frmLogin.iCodSucursal;
                    notaS.CodTipoTransaccion = tran.CodTransaccion;
                    notaS.CodTipoDocumento = 4; // codigo de documento nota de credito  

                    //TRAE NUMERACION
                    if (CodTransaccion != 7)
                    {
                        notaS.CodSerie = 0;
                        notaS.Serie = txtSerie.Text; // serie de la nota de credito
                        notaS.NumDoc = txtNumDoc.Text;
                        //notaS.DocumentoReferencia = txtDocRef.Text;
                        //notaS.DocumentoReferencia = CodNotaI;
                        notaS.DocumentoReferencia = CodFactura;
                    }
                    else
                    {
                        notaS.CodSerie = AdmSerie.traeCodSerie(frmLogin.iCodAlmacen, 4);
                        notaS.DocumentoReferencia = 0;
                    }

                    //


                    if (CodNotaI != 0)
                    { //notaS.DocumentoReferencia = CodNotaI.ToString(); 
                    }
                    notaS.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                    if (txtTipoCambio.Visible) { notaS.TipoCambio = Convert.ToDouble(txtTipoCambio.Text); }
                    notaS.FechaSalida = dtpFecha.Value.Date;
                    notaS.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                    notaS.FechaPago = dtpFechaPago.Value.Date;
                    //if (fpago.Dias == 0)
                    //{
                    //    notaS.FechaCancelado = dtpFecha.Value.Date;
                    //    notaS.Cancelado = 1;// Para saber si la nota esta pendiente de pago o esta cancelada
                    //}
                    notaS.Comentario = cmbMotivo.SelectedItem.ToString();// + " / " + txtComentario.Text;
                    
                    notaS.MontoBruto = Convert.ToDouble(txtBruto.Text);
                    notaS.MontoDscto = Convert.ToDouble(txtDscto.Text);
                    notaS.Igv = Convert.ToDouble(txtIGV.Text);
                    notaS.Total = Convert.ToDouble(txtPrecioVenta.Text);
                    notaS.CodUser = frmLogin.iCodUser;
                    notaS.Estado = 1;// Para saber si la nota esta activa o anulada. El estado se podra cambiar en una ventana especifica para anular notas

                    if (Proceso == 1)
                    {
                        if (txtSerie.Text !="" && txtNumDoc.Text !="")
                        {
                            if (AdmNotaS.insert(notaS))
                            {
                            RecorreDetalleS();
                            if (detalleS.Count > 0)
                            {
                                foreach (clsDetalleNotaSalida detS in detalleS)
                                {
                                    AdmNotaS.insertdetalle(detS);
                                }
                                
                            }
                            MessageBox.Show("Los datos se guardaron correctamente", "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.Close();
                            CodNotaS = Convert.ToInt32(notaS.CodNotaSalida);
                            CargaNotaSalida();
                            dgvDetalle.Enabled = false;
                            sololectura(true);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Porfavor ingrese Serie y Correlativo del Documento");
                            txtSerie.Focus();
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
            deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
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

        private void RecorreDetalleS()
        {
            detalle.Clear();
            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    añadedetalleS(row);
                }
            }
            //nota.Detalle = detalle;
        }

        private void añadedetalleS(DataGridViewRow fila)
        {
            clsDetalleNotaSalida detaS = new clsDetalleNotaSalida();
            detaS.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            detaS.CodNotaSalida = Convert.ToInt32(notaS.CodNotaSalida);
            detaS.CodAlmacen = frmLogin.iCodAlmacen;
            //deta.Moneda = cmbMoneda.SelectedIndex;
            detaS.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            detaS.SerieLote = fila.Cells[serielote.Name].Value.ToString();
            detaS.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            detaS.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
            detaS.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
            detaS.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
            detaS.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
            detaS.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
            detaS.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
            detaS.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
            detaS.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
            detaS.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
            detaS.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
            detaS.FechaSalida = dtpFecha.Value;
            detaS.CodUser = frmLogin.iCodUser;
            detalleS.Add(detaS);
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
            dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
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
                if (Application.OpenForms["frmProveedoresLista"] != null)
                {
                    Application.OpenForms["frmProveedoresLista"].Activate();
                }
                else
                {
                    frmProveedoresLista form = new frmProveedoresLista();
                    form.Proceso = 3;
                    form.Procede = 6;   //MODIFICACION ALEX PARA QUE REGRESE
                    form.ShowDialog();
                    if (CodProveedor != 0) { CargaProveedor(); ProcessTabKey(true); } else { BorrarProveedor(); }
                }
            }
        }

        private void CargaProveedor()
        {
            prov = AdmProv.MuestraProveedor(CodProveedor);
            txtCodProveedor.Text = prov.Ruc;
            txtNombreProveedor.Text = prov.RazonSocial;
        }

        private void BorrarProveedor()
        {
            prov = AdmProv.MuestraProveedor(CodProveedor);
            txtCodProveedor.Text = "";
            txtNombreProveedor.Text = "";
        }

        private void CargaCliente()
        {
            cli = AdmCli.MuestraCliente(CodCliente);
            if (cli != null)
            {
                txtCodProveedor.Text = cli.CodigoPersonalizado;
                txtNombreProveedor.Text = cli.RazonSocial;                    
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtCodProveedor.Text != "")
                {
                    if (BuscaProveedor())
                    {
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("El proveedor no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private Boolean BuscaProveedor()
        {
            prov = AdmProv.BuscaProveedor(txtCodProveedor.Text);
            if (prov != null)
            {
                txtNombreProveedor.Text = prov.RazonSocial;
                CodProveedor = prov.CodProveedor;
                return true;
            }
            else
            {
                txtNombreProveedor.Text = "";
                CodProveedor = 0;
                return false;
            }
        }


        private Boolean BuscaCliente()
        {
            cli = AdmCli.BuscaCliente(txtCodProveedor.Text, Tipo);
            if (cli != null)
            {
                txtCodProveedor.Text = cli.CodigoPersonalizado;
                txtNombreProveedor.Text = cli.RazonSocial;
                CodCliente = cli.CodCliente;               
                return true;
            }
            else
            {
                txtNombreProveedor.Text = "";
                CodCliente = 0;
                return false;
            }
        }

        private void txtCodCliente_Leave(object sender, EventArgs e)
        {
            if (CodProveedor == 0)
            {
                txtCodProveedor.Focus();
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

                if (dgvDetalle.Focused && e.ColumnIndex == dgvDetalle.Columns[cantidad.Name].Index)
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
                CRNotaCredito rpt = new CRNotaCredito();
                frmRptNotaCredito frm = new frmRptNotaCredito();
                CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rpt.PrintOptions;
                rptoption.PrinterName = ser.NombreImpresora;//Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3]);
                rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);

                rpt.SetDataSource(ds.NotaCredito(Convert.ToInt32(CodNota), frmLogin.iCodAlmacen).Tables[0]);
                frm.crvNotaCredito.ReportSource = rpt;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Nota de Credito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNuevaGuia_Click(object sender, EventArgs e)
        {
            frmNotadeCreditoCompra form2 = new frmNotadeCreditoCompra();
            form2.MdiParent = this.MdiParent;
            form2.Proceso = 1;
            form2.Show();
            this.Close();
        }

        private void txtCodProveedor_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbMotivo.SelectedIndex ==1)
                {
                    dgvDetalle.Enabled = false;
                }
                else
                {
                    dgvDetalle.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {
            if (txtSerie.Text !="")
            {
                txtNumDoc.Focus();
            }
            else
            {
                txtSerie.Focus();
            }
        }

        private void txtNumDoc_Leave(object sender, EventArgs e)
        {
            if (txtNumDoc.Text != "")
            {
                //txtNumDoc.Focus();
            }
            else
            {
                txtNumDoc.Focus();
            }
        }

       
      

       


    
    }
}
