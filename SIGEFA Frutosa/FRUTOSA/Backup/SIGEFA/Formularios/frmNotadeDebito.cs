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
    public partial class frmNotadeDebito : DevComponents.DotNetBar.OfficeForm
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
        clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();
        clsFacturaVenta venta = new clsFacturaVenta();
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
        //public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();
        public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();
        public List<clsDetalleFacturaVenta> detalle1 = new List<clsDetalleFacturaVenta>();
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

        //CODIGO NUEVO
        Int32 cantprod = 0;
        Decimal precprod = 0;
        DataTable dt1 = new DataTable();
        clsAdmMoneda AdmMon = new clsAdmMoneda();

        public frmNotadeDebito()
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

        private void txtTransaccion_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTransaccion_Leave(object sender, EventArgs e)
        {
            if (CodTransaccion == 0)
            {
                txtTransaccion.Focus();
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
                    if (CodCliente != 0) { CargaCliente(); BorrarNota();
                    cmbMotivo.Focus();
                        ProcessTabKey(true); 
                         }
                }
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
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

        private void txtCodCliente_Leave(object sender, EventArgs e)
        {
            if (CodCliente == 0)
            {
                txtCodCliente.Focus();
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
                    form.CodCliente = CodCliente;
                    form.tipo = 2;
                    form.ShowDialog();
                    if (form.venta != null && form.venta.CodFacturaVenta != "") { venta = form.venta; CodNotaS = Convert.ToInt32(venta.CodFacturaVenta); } else { }
                    if (CodNotaS != 0) { CargaNotaSalida(); ProcessTabKey(true); }
                }
            }       
        }

        private void txtDocRef_Leave(object sender, EventArgs e)
        {
            if (CodNotaS == 0)
            {
                txtDocRef.Focus();
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


        private void CargaDetalleNota()
        {
            dt1.Clear();
            if (cmbMotivo.SelectedIndex != 1)
            {
                //dgvDetalle.DataSource = AdmNotaS.CargaDetalleNotaCredito(CodNotaS);
                dt1 = AdmVenta.CargaDetalle(CodNotaS, frmLogin.iCodAlmacen);
                dgvDetalle.DataSource = dt1;
                //dgvDetalle.DataSource = AdmVenta.CargaDetalle(CodNotaS, frmLogin.iCodAlmacen);
                dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
                dgvDetalle.Columns["stockdisponible"].Visible = false;
                dgvDetalle.Columns["maxPorcDescto"].Visible = false;
                btnEliminar.Visible = true;
            }
            else
            {
                //BUSCAR INTERES POR VENTA CREDITO
                //dgvDetalle.DataSource = AdmPro.BuscarProducto(1515);
                dt1 = AdmPro.BuscarProducto(1515);
                dgvDetalle.DataSource = dt1;
                ////dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = 1;
                ////dgvDetalle.Rows[0].Cells[8].Value = 1;
                dgvDetalle.CurrentRow.Cells[preciounit.Name].ReadOnly = false;
                dgvDetalle.Columns["stockdisponible"].Visible = false;
                dgvDetalle.Columns["maxPorcDescto"].Visible = false;
                btnEliminar.Visible = false;    
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

        private void frmNotadeDebito_Shown(object sender, EventArgs e)
        {
            //MODIFICAR NUEVAEMNTE
            txtTransaccion.Focus();
            txtTransaccion.Text = "NDV";
            KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
            txtTransaccion_KeyPress(txtTransaccion, ee);
            ser = AdmSerie.BuscaSeriexDocumento(6, frmLogin.iCodAlmacen);
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Proceso != 0)
            {
                if (cmbMotivo.SelectedIndex == -1)
                {
                    MessageBox.Show("Porfavor seleccionar un motivo!");
                    cmbMotivo.Focus();
                }
                else
                {
                    if (dgvDetalle.Rows.Count > 0)
                    {
                        venta.CodSucursal = frmLogin.iCodSucursal;
                        venta.CodAlmacen = frmLogin.iCodAlmacen;
                        venta.CodTipoTransaccion = tran.CodTransaccion;
                        venta.CodCliente = cli.CodCliente;
                        venta.CodTipoDocumento = 6;
                        venta.CodSerie = ser.CodSerie;
                        venta.Serie = ser.Serie;
                        venta.NumDoc = ser.Numeracion.ToString();
                        venta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                        if (txtTipoCambio.Visible)
                        {
                            venta.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
                        }
                        venta.FechaSalida = dtpFecha.Value.Date;
                        venta.FechaPago = dtpFechaPago.Value.Date;
                        venta.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                        //venta.CodListaPrecio = Convert.ToInt32(cbListaPrecios.SelectedValue);
                        //venta.CodVendedor = Convert.ToInt32(cbovendedor.SelectedValue);
                        //venta.Comentario = txtComentario.Text;
                        venta.Comentario = cmbMotivo.SelectedItem.ToString();
                        //venta.MontoBruto = Convert.ToDouble(txtBruto.Text);
                        venta.MontoBruto = Convert.ToDouble(txtValorVenta.Text);
                        venta.MontoDscto = Convert.ToDouble(txtDscto.Text);
                        venta.Igv = Convert.ToDouble(txtIGV.Text);
                        venta.Total = Convert.ToDouble(txtPrecioVenta.Text);
                        venta.CodUser = frmLogin.iCodUser;
                        if (CodNotaS != 0) { venta.DocumentoReferencia = CodNotaS.ToString(); }
                        if (venta.Total != 0)
                        {
                            if (AdmVenta.insert(venta))
                            {
                                RecorreDetalle();
                                if (detalle1.Count > 0)
                                {
                                    foreach (clsDetalleFacturaVenta det in detalle1)
                                    {
                                        AdmVenta.insertdetalle(det);
                                    }
                                }
                                MessageBox.Show("Los datos se guardaron correctamente", "Nota de Débito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtNumDoc.Text = venta.CodFacturaVenta.PadLeft(11, '0');
                                sololectura(true);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El " + venta.Comentario + " ingresado no es correcto!", "Nota de Debito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        CargaNotaSalida();
                    }
                }                
            }
        }

        private void CargaMoneda()
        {
            cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
            cmbMoneda.DisplayMember = "descripcion";
            cmbMoneda.ValueMember = "codMoneda";
            cmbMoneda.SelectedIndex = 0;
        }

        private void frmNotadeDebito_Load(object sender, EventArgs e)
        {
            //cmbMoneda.SelectedIndex = 0;
            CargaFormaPagos();
            CargaMoneda();
            tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
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

        private void CargaFormaPagos()
        {
            cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
            cmbFormaPago.DisplayMember = "descripcion";
            cmbFormaPago.ValueMember = "codFormaPago";
            cmbFormaPago.SelectedIndex = 0;

        }

        private void CargaNotaIngreso()
        {
            try
            {
                venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodNota));
                ser = AdmSerie.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
                if (venta != null)
                {
                    if (venta.DocumentoReferencia != null) { notaS = AdmNotaS.CargaNotaSalidaDebitoVentas(Convert.ToInt32(venta.CodFacturaVenta)); }

                    txtNumDoc.Text = venta.CodFacturaVenta;
                    CodNotaS = Convert.ToInt32(venta.CodFacturaVenta);
                    CodTransaccion = venta.CodTipoTransaccion;
                    CargaTransaccion();

                    CodCliente = notaS.CodCliente;
                    CargaCliente();

                    dtpFecha.Value = venta.FechaSalida;
                    cmbMoneda.SelectedValue = venta.Moneda;
                    txtTipoCambio.Text = venta.TipoCambio.ToString();
                    cmbFormaPago.SelectedValue = venta.FormaPago;
                    cmbMotivo.SelectedItem = venta.Comentario.ToString();

                    if (txtDocRef.Enabled)
                    {
                        CodDocumento = venta.CodTipoDocumento;
                        txtDocRef.Text = notaS.SiglaDocumento + " " + notaS.Serie + "-" + notaS.NumDoc;  
                        //BuscaTipoDocumento();
                        //doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
                        //if (doc != null)
                        //{
                        //    CodDocumento = doc.CodTipoDocumento;
                        //}
                    }
                    cmbFormaPago.SelectedValue = venta.FormaPago;
                    dtpFechaPago.Value = venta.FechaPago;
                    //txtComentario.Text = venta.Comentario;
                    txtBruto.Text = String.Format("{0:#,##0.00}", venta.MontoBruto);
                    txtDscto.Text = String.Format("{0:#,##0.00}", venta.MontoDscto);
                    txtValorVenta.Text = String.Format("{0:#,##0.00}", venta.Total - venta.Igv);
                    txtIGV.Text = String.Format("{0:#,##0.00}", venta.Igv);
                    txtPrecioVenta.Text = String.Format("{0:#,##0.00}", venta.Total);
                    DetalleNota();
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

        private void DetalleNota()
        {
            dgvDetalle.DataSource = AdmVenta.CargaDetalleVentaCredito(CodNotaS, frmLogin.iCodAlmacen);
        }

        private void Bloqueabotones()
        {
            //btnNuevo.Visible = false;
            //btnEditar.Visible = false;
            //btnEliminar.Visible = false;
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
            clsDetalleFacturaVenta deta = new clsDetalleFacturaVenta();
            deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            deta.CodVenta = Convert.ToInt32(venta.CodFacturaVenta);
            deta.CodAlmacen = frmLogin.iCodAlmacen;
            deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
            deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
            //deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
            deta.Subtotal = Convert.ToDouble(fila.Cells[valorventa.Name].Value);
            deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
            deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
            deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
            deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
            deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
            deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
            deta.CodUser = frmLogin.iCodUser;
            deta.CantidadPendiente = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
            deta.CodDetalleCotizacion = 0;
            detalle1.Add(deta);

        }

        private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Double cantidad1, precio, bruto, montodescuento, valorventa1, igv1, precioventa1, precioreal1, valorreal, factorigv, dsc1, dsc2, dsc3, preunitario;
            try
            {
                //if (cmbMotivo.SelectedIndex == 0)
                //{
                //    if (precprod > Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value))
                //    {
                //        MessageBox.Show("La variacion del precio es incorrecta");
                //        dgvDetalle.CurrentRow.Cells[preciounit.Name].Value = Convert.ToString(precprod);
                //    }
                //    else
                //    {
                //        dgvDetalle.CurrentRow.Cells[preciounit.Name].Value = Convert.ToDecimal(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value) - precprod;
                //    }
                //}
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
                    btnGuardar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgvDetalle.Rows.Count > 0)
            //{
            //    if (Proceso != 3)
            //    {
            //        //CargaDetalleNota();
            //        if (cmbMotivo.SelectedIndex == 0)
            //        {
            //            dgvDetalle.CurrentRow.Cells["preciounit"].ReadOnly = false;                        
            //        }
            //        else
            //        {
            //            dgvDetalle.CurrentRow.Cells["preciounit"].ReadOnly = false;                        
            //        }
            //    }
            //}
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

        private void cmbMotivo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                CargaDetalleNota();
                if (cmbMotivo.SelectedIndex == 0 )
                {
                    dgvDetalle.Columns[preciounit.Name].HeaderText = "P. Unit."; 
                }
                else  
                {
                    dgvDetalle.Columns[preciounit.Name].HeaderText = "Interés"; 
                }
            }
            if (cmbMotivo.SelectedIndex == 0)
            {
                dgvDetalle.Columns[preciounit.Name].HeaderText = "P. Unit."; 
            }
            else
            {
                dgvDetalle.Columns[preciounit.Name].HeaderText = "Interés"; 
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
            {
                dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                ser = AdmSerie.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
                CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                rd.Load("CRNotaDebitoVenta.rpt");
                CRNotaDebitoVenta rpt = new CRNotaDebitoVenta();
                rd.SetDataSource(ds.ReportNotaDebitoVenta(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen));
                CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rd.PrintOptions;
                rptoption.PrinterName = ser.NombreImpresora;
                rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
                rptoption.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(1100, 1850, 200, 1300));
                //CrystalDecisions.Shared.PageMargins margenes = rd.PrintOptions.PageMargins;
                rd.PrintToPrinter(1, false, 1, 1);
                rd.Close();
                rd.Dispose();


                //CRNotaDebitoVenta rpt = new CRNotaDebitoVenta();
                //frmRptNotaCredito frm = new frmRptNotaCredito();
                //rpt.SetDataSource(ds.ReportNotaDebitoVenta(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen).Tables[0]);
                //CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rpt.PrintOptions;
                //rptoption.PrinterName = ser.NombreImpresora;//Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3]);
                //rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
                //rptoption.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(1100, 1850, 200, 1300));
                //frm.crvNotaCredito.ReportSource = rpt;
                //frm.Show();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Nota Débito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
