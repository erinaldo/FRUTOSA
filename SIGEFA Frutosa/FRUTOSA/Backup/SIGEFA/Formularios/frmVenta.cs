using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios
{
    public partial class frmVenta : DevComponents.DotNetBar.OfficeForm
    {
        clsReporteFactura ds = new clsReporteFactura();
        clsAdmTransaccion AdmTran = new clsAdmTransaccion();
        clsTransaccion tran = new clsTransaccion();
        clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();
        clsTipoDocumento doc = new clsTipoDocumento();
        clsAdmSerie Admser = new clsAdmSerie();
        clsSerie ser = new clsSerie();
        clsAdmPedido Admped = new clsAdmPedido();
        clsPedido pedido = new clsPedido();
        clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();
        clsTipoCambio tc = new clsTipoCambio();
        clsAdmProveedor AdmProv = new clsAdmProveedor();
        clsProveedor prov = new clsProveedor();
        clsAdmCliente AdmCli = new clsAdmCliente();
        clsCliente cli = new clsCliente();
        clsAdmAutorizado AdmAut = new clsAdmAutorizado();
        clsAutorizado aut = new clsAutorizado();
        clsAdmNotaSalida AdmNota = new clsAdmNotaSalida();
        clsNotaSalida nota = new clsNotaSalida();
        clsAdmGuiaRemision AdmGuia = new clsAdmGuiaRemision();
        clsGuiaRemision guia = new clsGuiaRemision();
        clsAdmFormaPago AdmPago = new clsAdmFormaPago();
        clsFormaPago fpago = new clsFormaPago();
        clsListaPrecio Listap = new clsListaPrecio();
        clsAdmVendedor AdmVen = new clsAdmVendedor();
        clsFacturaVenta venta = new clsFacturaVenta();
        clsFacturaVenta factura = new clsFacturaVenta();
        clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();
        clsMoneda moneda = new clsMoneda();
        clsAdmMoneda AdmMon = new clsAdmMoneda();
        clsAdmListaPrecio admLista = new clsAdmListaPrecio();
        clsValidar ok = new clsValidar();
        clsConsultasExternas ext = new clsConsultasExternas();
        clsCotizacion coti = new clsCotizacion();
        clsDetalleCotizacion detaCoti = new clsDetalleCotizacion();
        clsAdmCotizacion AdmCoti = new clsAdmCotizacion();

        public List<Int32> config = new List<Int32>();
        public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();
        public List<clsDetalleFacturaVenta> detalle1 = new List<clsDetalleFacturaVenta>(); 
        public List<clsDetalleGuiaRemision> detalleg = new List<clsDetalleGuiaRemision>();
        public List<Int32> documento = new List<Int32>(); 
        public List<Int32> codsalida = new List<Int32>(); 
        private List<Int32> correlativo = new List<Int32>(); 
        private List<clsFacturaVenta> ltaventa = new List<clsFacturaVenta>();  
        private List<Int32> codpro = new List<Int32>();
        clsFormaPago forma = new clsFormaPago();
        public String CodNota, CodVenta;
        public Int32 CodTransaccion;
        public Int32 CodProveedor;
        public Int32 CodCliente;
        public Int32 CodDocumento;
        public Int32 CodSerie;
        public String numSerie;
        public Int32 CodAutorizado;
        public Int32 CodPedido;
        public Int32 CodGuia;
        public Int32 Tipo;
        public Int32 codForma, codListaP;
        public Int32 Proceso = 0; //(1) Nuevo (2) Editar (3) Consulta
        public Int32 Procede = 0; //(1) Sin Guia (2)Con Guia
        public DataTable datoscarga2 = new DataTable();
        public DataTable datos = new DataTable();

        public static BindingSource data = new BindingSource();

        Int32 CodLista = 0;
        Boolean Validacion = true;
        Decimal TipoCambio = 0, ret=0;
        String Moneda = "";

        private String Salida = "";
        private Int32 codCotizacion;

        private void VentaEnMoneda()
        {
            Decimal TipoCambio = 0;

            TipoCambio = Convert.ToDecimal(txtTipoCambio.Text.Trim());


            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
                {
                    row.Cells[preciounit.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) / TipoCambio;
                    row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) / TipoCambio;
                    row.Cells[montodscto.Name].Value = Convert.ToDecimal(row.Cells[montodscto.Name].Value) / TipoCambio;
                    row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / TipoCambio;
                    row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[igv.Name].Value) / TipoCambio;
                    row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) / TipoCambio;
                }
                else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
                {
                    row.Cells[preciounit.Name].Value = Convert.ToDecimal(row.Cells[preciounit.Name].Value) * TipoCambio;
                    row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) * TipoCambio;
                    row.Cells[montodscto.Name].Value = Convert.ToDecimal(row.Cells[montodscto.Name].Value) * TipoCambio;
                    row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) * TipoCambio;
                    row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[igv.Name].Value) * TipoCambio;
                    row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[precioventa.Name].Value) * TipoCambio;
                }
            }
        }

        public frmVenta()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            RecorreDetalle();
            if (Application.OpenForms["frmDetalleSalida"] != null)
            {
                Application.OpenForms["frmDetalleSalida"].Activate();
            }
            else
            {
                frmDetalleSalida form = new frmDetalleSalida();
                form.Procede = 2;
                form.Proceso = 1;
                form.Tipo = 2;
                form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                form.Codlista = Convert.ToInt32(cbListaPrecios.SelectedValue);
                form.tc = tc.Compra;
                form.productoscargados = detalle1;
                form.ShowDialog();
                
                //dgvDetalle.Rows.Add("", form.detalle.CodProducto, form.detalle.Referencia, form.detalle.Descripcion, form.detalle.CodUnidad
                //    , form.detalle.Unidad, form.detalle.SerieLote, form.detalle.Cantidad, form.detalle.PrecioUnitario, form.detalle.Importe
                //    , form.detalle.Descuento1, form.detalle.Descuento2, form.detalle.Descuento3, form.detalle.MontoDescuento, form.detalle.ValorVenta
                //    , form.detalle.Igv, form.detalle.PrecioVenta, form.detalle.PrecioReal, form.detalle.ValoReal);            
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0 & dgvDetalle.SelectedRows.Count > 0)
            {
                
                    dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
                    calculatotales();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0 & dgvDetalle.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDetalle.SelectedRows[0];
                if (Application.OpenForms["frmDetalleSalida"] != null)
                {
                    Application.OpenForms["frmDetalleSalida"].Activate();
                }
                else
                {
                    frmDetalleSalida form = new frmDetalleSalida();
                    form.Proceso = 2;
                    form.Procede = 2;
                    form.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                    form.tc = Convert.ToDouble(txtTipoCambio.Text);
                    form.Codlista = Convert.ToInt32(cbListaPrecios.SelectedValue);
                    form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
                    form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
                    form.BuscaProducto();
                    form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
                    form.txtCantidad.Text = String.Format("{0:#,##0.00}",row.Cells[cantidad.Name].Value);
                    form.txtPrecio.Text = String.Format("{0:#,##0.00}", row.Cells[preciounit.Name].Value);
                    form.txtDscto1.Text = String.Format("{0:#,##0.00}",row.Cells[dscto1.Name].Value);
                    form.txtPrecioNeto.Text = String.Format("{0:#,##0.00}",row.Cells[importe.Name].Value);
                    form.ShowDialog();
                }
            }
        }

        private void txtTransaccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (!txtTransaccion.ReadOnly)
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
                        form.Proceso = 4;
                        form.ShowDialog();
                        if (CodTransaccion != 0)
                        {
                            CargaTransaccion();
                            ProcessTabKey(true);
                        }
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

        private void CargaFormaPagos()
        {
            cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
            cmbFormaPago.DisplayMember = "descripcion";
            cmbFormaPago.ValueMember = "codFormaPago";
            cmbFormaPago.SelectedIndex = -1;
        }

        private void CargaVendedores()
        {
            cbovendedor.DataSource = AdmVen.MuestraVendedoresDestaque();
            cbovendedor.DisplayMember = "apellido";
            cbovendedor.ValueMember = "codVendedor";
            cbovendedor.SelectedIndex = 0;
        }

        private void CargaListaPrecios(int codForma)
        {
            cbListaPrecios.DataSource = admLista.MuestraListaPrecioxFormaPago(frmLogin.iCodSucursal, codForma);
            cbListaPrecios.DisplayMember = "nombre";
            cbListaPrecios.ValueMember = "codListaPrecio";
            if (cbListaPrecios.Items.Count > 0)
            {
                cbListaPrecios.SelectedIndex = 0;
            }
        }

        //public void llenardetalle2(Int32 codNota)
        //{
        //    data.DataSource = null;
        //    DataTable datoscarga = new DataTable();

        //    datoscarga = AdmVenta.MuestraDetalleGuiaVenta(frmLogin.iCodAlmacen,codNota);
        //    if (datoscarga != null)
        //    {
        //        datoscarga2.Merge(datoscarga);
        //    }

        //    datos = datoscarga2;

        //    for (int i = 0; i < datos.Rows.Count; i++)
        //    {
        //        for (int j = i + 1; j < datos.Rows.Count; j++)
        //        {
        //            if (Convert.ToDouble(datos.Rows[i]["preciounitario"])
        //                    .Equals(Convert.ToDouble(datos.Rows[j]["preciounitario"])) &&
        //                Convert.ToInt32(datos.Rows[i]["codProducto"])
        //                    .Equals(Convert.ToInt32(datos.Rows[j]["codProducto"])))
        //            {
        //                datos.Rows[i]["cantidad"] = Convert.ToDouble(datos.Rows[i]["cantidad"]) +
        //                                            Convert.ToDouble(datos.Rows[j]["cantidad"]);
        //                datos.Rows[j]["codSalida"] = Convert.ToInt32(datos.Rows[j]["codSalida"]);
        //                AdmNota.deletedetalle(Convert.ToInt32(datos.Rows[j]["codDetalle"]));
        //                datos.Rows.RemoveAt(j);
        //            }
        //        }
        //    }
        //    dgvDetalle.DataSource = datos;
        //    recalculadetalle();
        //    dgvDetalle.ClearSelection();
            
        //}

        private void recalculadetalle()
        {
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                //if (Convert.ToInt32(row.Cells[stockPend.Name].Value) != 0)
                //{
                    row.Cells[importe.Name].Value = Convert.ToDecimal(row.Cells[cantidad.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
                    row.Cells[precioventa.Name].Value = Convert.ToDecimal(row.Cells[cantidad.Name].Value) * Convert.ToDecimal(row.Cells[preciounit.Name].Value);
                    row.Cells[valorventa.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) / Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                    row.Cells[precioreal.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
                    row.Cells[valoreal.Name].Value = Convert.ToDecimal(row.Cells[valorventa.Name].Value) / Convert.ToDecimal(row.Cells[cantidad.Name].Value);
                    row.Cells[igv.Name].Value = Convert.ToDecimal(row.Cells[importe.Name].Value) - Convert.ToDecimal(row.Cells[valorventa.Name].Value);
                //}
            }
        }
        
        private void txtTransaccion_Leave(object sender, EventArgs e)
        {
            if (CodTransaccion == 0)
            {
                txtTransaccion.Focus();
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
                        MessageBox.Show("Codigo de transacción no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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

        private void CargaCliente()
        {
            cli = AdmCli.MuestraCliente(CodCliente);
            cli = AdmCli.CargaDeuda(cli);
            if (cli.Cantidad > 0)
            {
                DialogResult dlgResult = MessageBox.Show("El cliente selecionado presenta" + Environment.NewLine + "Facturas pendientes = " + cli.Cantidad + Environment.NewLine + "Deuda Total = " + cli.Deuda + " soles" + Environment.NewLine + "Linea de crédito = " + cli.LineaCredito + Environment.NewLine + " Desea continuar con la venta?", "Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    ret = 1;
                    txtCotizacion.Text = "";
                    return;
                }
                else
                {
                    cargadatoscliente();
                    ret = 0;
                }
            }
            else 
            {
                cargadatoscliente();
                ret = 0;
            }
        }

        //private void cargadatoscliente()
        //{
        //    txtCodCliente.Text = cli.Dni;
        //    if (cli.Ruc != "")
        //    {
        //        txtDocRef.Text = "FT";
        //        KeyPressEventArgs ee = new KeyPressEventArgs((char) Keys.Return);

        //        txtDocRef_KeyPress(txtDocRef, ee);
        //        txtSerie.Text = "001";
        //        txtSerie_KeyPress(txtDocRef, ee);
        //        //txtCodCliente.Text = cli.Ruc;
        //    }
        //    else
        //    {
                
        //        txtDocRef.Text = "BV";
        //        KeyPressEventArgs ee = new KeyPressEventArgs((char) Keys.Return);
        //        txtDocRef_KeyPress(txtDocRef, ee);
        //        txtSerie.Text = "001";
        //        txtSerie_KeyPress(txtDocRef, ee);
        //        //txtCodigoCli.Text = cli.Dni;
        //    }
                
        //    txtNombreCliente.Text = cli.RazonSocial;
        //    txtDireccionCliente.Text = cli.DireccionLegal;
        //    txtCodigoCli.Text = cli.CodCliente.ToString();
        //    if (cli.Moneda == 1)
        //    {
        //        txtLineaCredito.Text = cli.LineaCredito.ToString();
        //        txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
        //        txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
        //        lbLineaCredito.Text = "Línea de Crédito (S/.):";
        //        label23.Text = "Línea Disponible (S/.):";
        //        label25.Text = "Línea C. en Uso (S/.):";
        //    }
        //    else 
        //    {
        //        txtLineaCredito.Text = cli.LineaCredito.ToString();
        //        txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
        //        txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
        //        lbLineaCredito.Text = "Línea de Crédito ($.):";
        //        label23.Text = "Línea Disponible ($.):";
        //        label25.Text = "Línea C. en Uso ($.):";
        //    }
           
        //    cmbFormaPago.SelectedValue = cli.FormaPago;
        //    forma = AdmPago.BuscaFormaPagoVenta(cli.FormaPago);    
        //    if (cli.CodListaPrecio != null) 
        //    { 
        //        cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null); 
        //        cbListaPrecios.SelectedValue = cli.CodListaPrecio; 
        //    }
        //    //cmbFormaPago.SelectedIndex = 0;
        //    if (cli.FormaPago != 0)
        //    {
        //        EventArgs ee = new EventArgs();
        //        cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
               
        //    }
        //    else
        //    {
        //        dtpFechaPago.Value = DateTime.Today;
        //    }
        //    if (cli.CodVendedor != 0)
        //    {
               
        //        cbovendedor.SelectedValue = cli.CodVendedor;
        //    }
        //    txtPDescuento.Text = cli.Descuento.ToString();
        //    cmbMoneda.SelectedValue = cli.Moneda;
        //}

        private void cargadatoscliente()
        {
            txtCodCliente.Text = cli.Dni;
            if (cli.Ruc != "" && cli.Dni == "")
            {

                txtDocRef.Text = "FT";
                KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                txtDocRef_KeyPress(txtDocRef, ee);
                txtSerie.Text = "001";
                txtSerie_KeyPress(txtDocRef, ee);
                txtCodCliente.Text = cli.Ruc;
            }
            else if (cli.Dni != "" && cli.Ruc == "")
            {

                txtDocRef.Text = "BV";
                KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                txtDocRef_KeyPress(txtDocRef, ee);
                txtSerie.Text = "001";
                txtSerie_KeyPress(txtDocRef, ee);
                txtCodigoCli.Text = cli.Dni;
            }
            else if (cli.Ruc != "" && cli.Dni != "")
            {
                if (MessageBox.Show("Si para FT(Factura) o No para BV(Boleta)", "Seleccione Tipo de Doc. Ref.", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    txtDocRef.Text = "BV";
                    KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    txtDocRef_KeyPress(txtDocRef, ee);
                    txtSerie.Text = "001";
                    txtSerie_KeyPress(txtDocRef, ee);
                    txtCodigoCli.Text = cli.Dni;
                }
                else
                {
                    txtDocRef.Text = "FT";
                    KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    txtDocRef_KeyPress(txtDocRef, ee);
                    txtSerie.Text = "001";
                    txtSerie_KeyPress(txtDocRef, ee);
                    txtCodCliente.Text = cli.Ruc;
                }
            }
            txtNombreCliente.Text = cli.RazonSocial;
            txtDireccionCliente.Text = cli.DireccionLegal;
            txtCodigoCli.Text = cli.CodCliente.ToString();
            if (cli.Moneda == 1)
            {
                txtLineaCredito.Text = cli.LineaCredito.ToString();
                txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
                txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
                lbLineaCredito.Text = "Línea de Crédito (S/.):";
                label23.Text = "Línea Disponible (S/.):";
                label25.Text = "Línea C. en Uso (S/.):";
            }
            else
            {
                txtLineaCredito.Text = cli.LineaCredito.ToString();
                txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
                txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();
                lbLineaCredito.Text = "Línea de Crédito ($.):";
                label23.Text = "Línea Disponible ($.):";
                label25.Text = "Línea C. en Uso ($.):";
            }

            cmbFormaPago.SelectedValue = cli.FormaPago;
            forma = AdmPago.BuscaFormaPagoVenta(cli.FormaPago);
            if (cli.CodListaPrecio != null)
            {
                cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null);
                cbListaPrecios.SelectedValue = cli.CodListaPrecio;
            }
            //cmbFormaPago.SelectedIndex = 0;
            if (cli.FormaPago != 0)
            {
                EventArgs ee = new EventArgs();
                cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);

            }
            else
            {
                dtpFechaPago.Value = DateTime.Today;
            }
            if (cli.CodVendedor != 0)
            {

                cbovendedor.SelectedValue = cli.CodVendedor;
            }
            txtPDescuento.Text = cli.Descuento.ToString();
            cmbMoneda.SelectedValue = cli.Moneda;
        }

        private Boolean BuscaCliente()
        {
            cli = AdmCli.BuscaCliente(txtCodCliente.Text, Tipo);
            
            if (cli != null)
            {
                cli = AdmCli.CargaDeuda(cli);
                if (cli.Cantidad > 0)
                {
                    DialogResult dlgResult = MessageBox.Show("El cliente selecionado presenta" + Environment.NewLine + "Facturas pendientes = " + cli.Cantidad + Environment.NewLine + "Deuda Total = " + cli.Deuda + " soles" + Environment.NewLine + "Linea de crédito = " + cli.LineaCredito + Environment.NewLine + " Desea continuar con la venta?", "Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.No)
                    {
                        txtNombreCliente.Text = "";
                        CodCliente = 0;
                        
                        txtPDescuento.Text = "";
                        return false;                       
                    }
                    else
                    {
                        CodCliente = cli.CodCliente;
                        cargadatoscliente();
                        return true;
                    }
                }
                else
                {
                    CodCliente = cli.CodCliente;
                    cargadatoscliente();
                    return true; 
                }
            }
            else
            {
                MessageBox.Show("El Cliente no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodCliente.Text = "";
                txtNombreCliente.Text = "";
                CodCliente = 0;
                
                txtPDescuento.Text = "";
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
                    if (CodCliente != 0) { CargaCliente();
                        btnNuevo.Enabled = true; ProcessTabKey(true); }
                }
            }
        }

        private void txtCodCliente_Leave(object sender, EventArgs e)
        {            
            //if (CodCliente == 0)
            //{
            //    txtCodCliente.Focus();
            //}
            //VerificarCabecera();
            //if (Validacion && Proceso == 1)
            //{
            //    btnGuardar.Enabled = true;
            //}
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
                    //}
                    //else
                    //{
                    //    MessageBox.Show("El Cliente no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE SALIDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            if (txtTipoCambio.Visible)
            {
                tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
                if (tc != null)
                {
                    txtTipoCambio.Text = tc.Compra.ToString();
                }
                else
                {
                    MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpFecha.Value = DateTime.Now.Date;
                    dtpFecha.Focus();
                }
            }
        }

        private void dtpFecha_Leave(object sender, EventArgs e)
        {
            if (CodTransaccion == 0)
            {
                dtpFecha.Focus();
            }
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
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
            if (cmbMoneda.SelectedValue != null)
            {
                if (cmbMoneda.SelectedText.Equals("NUEVOS SOLES"))
                {
                    label8.Visible = false;
                    txtTipoCambio.Visible = false;
                }
            }
        }

        private void txtDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtDocRef.Text != "")
                {
                    if (BuscaTipoDocumento())
                    {
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private Boolean BuscaTipoDocumento()
        {
            doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
            if (doc != null)
            {
                CodDocumento = doc.CodTipoDocumento;
                txtCodDocumento.Text = CodDocumento.ToString();
                return true;
            }
            else
            {
                CodDocumento = 0;
                txtCodDocumento.Text = CodDocumento.ToString();
                return false;
            }
        }

        private Boolean BuscaSerie()
        {
            ser = Admser.BuscaSerie(txtSerie.Text,CodDocumento,frmLogin.iCodAlmacen);
            
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
        
        private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmDocumentos"] != null)
                {
                    Application.OpenForms["frmDocumentos"].Activate();
                }
                else
                {
                    frmDocumentos form = new frmDocumentos();
                    form.Proceso = 3;
                    form.ShowDialog();
                    doc = form.doc;
                    CodDocumento = doc.CodTipoDocumento;
                    txtCodDocumento.Text = CodDocumento.ToString();
                    txtDocRef.Text = doc.Sigla;
                    if (CodDocumento != 0) { ProcessTabKey(true); }                    
                }
            }
        }

        private void VerificarCabecera()
        {
            Validacion = true;
            if (CodTransaccion == 0 || CodDocumento == 0)
            {
                Validacion = false;
            }
            if (txtCodCliente.Visible && CodCliente == 0)
            {
                Validacion = false;
            }
            //if (txtPedido.Visible && CodPedido == 0)
            //{
            //    Validacion = false;
            //}
            if (txtAutorizacion.Visible && CodAutorizado == 0)
            {
                Validacion = false;
            }
            if (Validacion && Proceso == 1)
            {
                btnGuardar.Enabled = true;
            }
        }

        private void sololectura(Boolean estado)
        {
            txtTransaccion.ReadOnly = estado;
            dtpFecha.Enabled = !estado;
            txtCodCliente.ReadOnly = estado;
            cmbMoneda.Enabled = !estado;
            txtDocRef.ReadOnly = estado;
            txtNumero.Visible = estado;
            txtNumero.ReadOnly = estado;
            //txtPedido.ReadOnly = estado;
            txtComentario.ReadOnly = estado;
            txtAutorizacion.ReadOnly = estado;
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
            btnNuevaVenta.Visible = estado;
            ckbguia.Enabled = !estado;
            cbovendedor.Enabled = !estado;
            cbListaPrecios.Enabled = !estado;
            cmbFormaPago.Enabled = !estado;
            txtSerie.ReadOnly = estado;
            txtGuias.Enabled = !estado;
            txtCotizacion.Enabled = !estado;
        }

        private void BloquearEdicion(Boolean estado)// para bloquear la edicion de la factura en caso de cargar datos de una cotizacion vigente
        {
            txtTransaccion.ReadOnly = estado;
            dtpFecha.Enabled = !estado;
            txtCodCliente.ReadOnly = estado;
            cmbMoneda.Enabled = !estado;
            txtDocRef.ReadOnly = estado;
            txtNumero.Visible = estado;
            txtNumero.ReadOnly = estado;
            //txtPedido.ReadOnly = estado;
            txtComentario.ReadOnly = estado;
            txtAutorizacion.ReadOnly = estado;
            txtBruto.ReadOnly = estado;
            txtDscto.ReadOnly = estado;
            txtValorVenta.ReadOnly = estado;
            txtIGV.ReadOnly = estado;
            txtPrecioVenta.ReadOnly = estado;
            btnNuevo.Visible = !estado;
            btnEditar.Visible = !estado;
            btnEliminar.Visible = !estado;
            //btnGuardar.Visible = !estado;
            //btnImprimir.Visible = estado;
            //btnNuevaVenta.Visible = estado;
            //ckbguia.Enabled = !estado;
            cbovendedor.Enabled = !estado;
            cbListaPrecios.Enabled = !estado;
            cmbFormaPago.Enabled = !estado;
            txtSerie.ReadOnly = estado;
            txtGuias.Enabled = !estado;
        }

        private void CargaDetalle()
        {
            dgvDetalle.DataSource = AdmVenta.CargaDetalle(Convert.ToInt32(venta.CodFacturaVenta), frmLogin.iCodAlmacen);
        }

        private void CargaDetalleCotizacion()
        {
            dgvDetalle.DataSource = AdmCoti.CargaDetalle(Convert.ToInt32(coti.CodCotizacion), frmLogin.iCodAlmacen);
        }

        private void CargaDetalleGuia()
        {
            dgvDetalle.DataSource = AdmGuia.CargaDetalle(Convert.ToInt32(guia.CodGuiaRemision));
        }

        private void txtDocRef_Leave(object sender, EventArgs e)
        {
            BuscaTipoDocumento();
           
        }

        private void CargaMoneda()
        {
            cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
            cmbMoneda.DisplayMember = "descripcion";
            cmbMoneda.ValueMember = "codMoneda";
            cmbMoneda.SelectedIndex = 0;
        }

        public DateTime fecha1, fecha2;

        public void frmVenta_Load(object sender, EventArgs e)
        {
            iniciaformulario();

        }

        private void iniciaformulario()
        {
            CargaMoneda();
            dtpFecha.MaxDate = DateTime.Today.Date;
            tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
            CargaFormaPagos();
            CargaVendedores();

            if (Proceso == 2)
            {
                CargaVenta();
            }
            else if (Proceso == 3)
            {
                CargaVenta();
                sololectura(true);
            }
            txtCodigoCli.Visible = false;
        }

        private void frmVenta_Shown(object sender, EventArgs e)
        {
            txtTransaccion.Focus();
            txtTransaccion.Text = "FT";
            KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
            txtTransaccion_KeyPress(txtTransaccion,ee);
            btnNuevo.Focus();
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
                        txtTipoCambio.Text = tc.Compra.ToString();
                    }
                }
            }
        }

        private void CargaVenta()
        {
            try
            {
                venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(CodVenta));
                ser = Admser.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
                guia = AdmGuia.CargaGuiaVenta(Convert.ToInt32(CodVenta));

                if (venta != null)
                {
                    txtNumDoc.Text = venta.CodFacturaVenta;
                    CodTransaccion = venta.CodTipoTransaccion;
                    CargaTransaccion();                    

                    if (txtCodCliente.Enabled)
                    {
                        CodCliente = venta.CodCliente;
                        cli = AdmCli.MuestraCliente(CodCliente);
                        txtCodCliente.Text = venta.DNI;
                        txtNombreCliente.Text = venta.RazonSocialCliente;
                        txtDireccionCliente.Text = venta.Direccion;
                        txtLineaCredito.Text = cli.LineaCredito.ToString();
                        txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
                        txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();                        
                    }
                    dtpFecha.Value = venta.FechaSalida;
                    cmbMoneda.SelectedValue = venta.Moneda;
                    txtTipoCambio.Text = venta.TipoCambio.ToString();
                    if (txtAutorizacion.Enabled)
                    {
                        //se guarda el codigo del autorizado y se cargan los datos de este
                    }
                    //if (txtDocRef.Enabled)
                    //{
                        CodDocumento = venta.CodTipoDocumento;
                        txtCodDocumento.Text = CodDocumento.ToString();
                        txtDocRef.Text = venta.SiglaDocumento;
                        txtSerie.Text = venta.Serie;
                        if (Procede != 4) txtNumero.Text = venta.NumDoc;
                        else txtNumero.Text = numSerie;
                    //}
                  
                    if (cbovendedor.Enabled)
                    {
                        if (venta.CodVendedor != 0)
                        {
                            cbovendedor.SelectedValue = venta.CodVendedor;
                        }
                    }
                    if (guia != null)
                    {
                        if (guia.CodFactura == Convert.ToInt32(venta.CodFacturaVenta))
                        {
                            ckbguia.Checked = true;
                            txtGuias.Text = guia.CodGuiaRemision;
                        }
                        else
                        {
                            ckbguia.Checked = false;
                            txtGuias.Text = "";
                        }
                    }
                    cmbFormaPago.SelectedValue = venta.FormaPago;
                    cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, null);
                    cbListaPrecios.SelectedValue = venta.CodListaPrecio;
                    dtpFechaPago.Value = venta.FechaPago;
                    txtComentario.Text = venta.Comentario;
                    txtBruto.Text = String.Format("{0:#,##0.00}", venta.MontoBruto);
                    txtDscto.Text = String.Format("{0:#,##0.00}", venta.MontoDscto);
                    txtValorVenta.Text = String.Format("{0:#,##0.00}", venta.Total - venta.Igv);
                    txtIGV.Text = String.Format("{0:#,##0.00}", venta.Igv);
                    txtPrecioVenta.Text = String.Format("{0:#,##0.00}", venta.Total);
                    CargaDetalle();
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

       

        private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtSerie.Text != "")
                {
                    if (BuscaSerie())
                    {
                        txtSerie.Text = ser.Serie.ToString();
                        if (ser.PreImpreso)
                        {
                            txtNumero.Visible = true;
                            txtNumero.Enabled = false;
                            ckbguia.Visible = false;
                            txtNumero.Focus();
                            txtNumero.Text = "";
                        }
                        else
                        {
                            txtNumero.Text = "";
                            txtNumero.Enabled = true;
                            txtNumero.Visible = false;
                            txtNumero.Text = ser.Numeracion.ToString();
                        }

                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("Serie no existe, Presione F1 para consultar la tabla de ayuda",
                                "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtSerie_Leave(object sender, EventArgs e)
        {
            if (BuscaSerie2())
            {
                txtSerie.Text = ser.Serie.ToString();
                if (ser.PreImpreso)
                {
                    txtNumero.Visible = true;
                    txtNumero.Text = "";
                    ckbguia.Visible = false;
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
        private Boolean BuscaSerie2()
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

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void txtNumero_Leave(object sender, EventArgs e)
        {
            //if (txtNumero.Text == "")
            //{
            //    txtNumero.Focus();
            //}
            //else
            //{
            //    VerificarCabecera();
               
            //}
        }

        private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (Procede != 2 || Procede != 3)
            {
                if (Proceso == 1)
                {
                    if (txtPDescuento.Text != "")
                    {
                        calculatotales();
                        calculadescuentogeneral();
                    }
                    else
                    {
                        calculatotales();
                    }

                    if (dgvDetalle.RowCount > 0)
                    {
                        int Indice = 0;
                        Indice = dgvDetalle.RowCount - 1;

                        if (Convert.ToInt32(cmbMoneda.SelectedValue) == 1)
                        {
                            if (TipoCambio != 0)
                            {
                                dgvDetalle[8, Indice].Value = Convert.ToDecimal(dgvDetalle[8, Indice].Value)*TipoCambio;
                                dgvDetalle[9, Indice].Value = Convert.ToDecimal(dgvDetalle[9, Indice].Value)*TipoCambio;
                                dgvDetalle[13, Indice].Value = Convert.ToDecimal(dgvDetalle[13, Indice].Value)*
                                                               TipoCambio;
                                dgvDetalle[14, Indice].Value = Convert.ToDecimal(dgvDetalle[14, Indice].Value)*
                                                               TipoCambio;
                                dgvDetalle[15, Indice].Value = Convert.ToDecimal(dgvDetalle[15, Indice].Value)*
                                                               TipoCambio;
                                dgvDetalle[16, Indice].Value = Convert.ToDecimal(dgvDetalle[16, Indice].Value)*
                                                               TipoCambio;
                            }
                        }
                        else if (Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
                        {
                        }
                    }
                }
            }
        }

        public void calculatotales()
        {
            if (Proceso != 0)
            {
                if (Procede != 3)
                {
                    Decimal bruto = 0;
                    Decimal descuen = 0;
                    Decimal valor = 0;
                    Decimal preciovent = 0;
                    Decimal igvt = 0;

                    foreach (DataGridViewRow row in dgvDetalle.Rows)
                    {
                        bruto = bruto + Convert.ToDecimal(row.Cells[importe.Name].Value);
                        descuen = descuen + Convert.ToDecimal(row.Cells[montodscto.Name].Value);
                        valor = valor + Convert.ToDecimal(row.Cells[valorventa.Name].Value);
                        preciovent = preciovent + Convert.ToDecimal(row.Cells[precioventa.Name].Value);
                        igvt = igvt + Convert.ToDecimal(row.Cells[igv.Name].Value);
                    }
                    txtBruto.Text = String.Format("{0:#,##0.00}", bruto);
                    txtDscto.Text = String.Format("{0:#,##0.00}", descuen);
                    txtValorVenta.Text = String.Format("{0:#,##0.00}", valor);

                    //txtIGV.Text = String.Format("{0:#,##0.00}", bruto - descuen - valor);
                    txtIGV.Text = String.Format("{0:#,##0.00}", igvt);
                    //txtPrecioVenta.Text = String.Format("{0:#,##0.00}", bruto);
                    txtPrecioVenta.Text = String.Format("{0:#,##0.00}", preciovent);
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Double totalsoles = 0;
            
            if (superValidator1.Validate())
            {
                if (Convert.ToInt32(cli.Moneda) != Convert.ToInt32(cmbMoneda.SelectedValue))
                {
                    if(Convert.ToInt32(cli.Moneda)==2 || Convert.ToInt32(cmbMoneda.SelectedValue)==1)
                       totalsoles = Convert.ToDouble(txtPrecioVenta.Text) / Convert.ToDouble(txtTipoCambio.Text);
                    else if (Convert.ToInt32(cli.Moneda) == 1 || Convert.ToInt32(cmbMoneda.SelectedValue) == 2)
                        totalsoles = Convert.ToDouble(txtPrecioVenta.Text) * Convert.ToDouble(txtTipoCambio.Text);
                    
                }
                else
                {
                    totalsoles = Convert.ToDouble(txtPrecioVenta.Text);
                }
                if ((totalsoles > Convert.ToDouble(txtLineaCreditoDisponible.Text)) && Convert.ToInt32(cmbFormaPago.SelectedValue) != 6)
                {
                        MessageBox.Show("El Monto Excede a la Línea de Crédito");
                }
                else
                {
                    if (Proceso != 0)
                        {
                            venta.CodSucursal = frmLogin.iCodSucursal;
                            venta.CodAlmacen = frmLogin.iCodAlmacen;
                            venta.CodTipoTransaccion = tran.CodTransaccion;
                            venta.CodCliente = Convert.ToInt32(txtCodigoCli.Text);
                            venta.CodTipoDocumento = doc.CodTipoDocumento;
                            venta.CodSerie = CodSerie;
                            venta.Serie = txtSerie.Text;
                            venta.NumDoc = txtNumero.Text;
                            venta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                            if (txtTipoCambio.Visible)
                            {
                                venta.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
                            }
                            venta.FechaSalida = dtpFecha.Value;
                            venta.FechaPago = dtpFechaPago.Value;
                            venta.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                            venta.CodListaPrecio = Convert.ToInt32(cbListaPrecios.SelectedValue);
                            venta.CodVendedor = Convert.ToInt32(cbovendedor.SelectedValue);
                            venta.Comentario = txtComentario.Text;
                            venta.MontoBruto = Convert.ToDouble(txtBruto.Text);
                            //venta.MontoBruto = Convert.ToDouble(txtValorVenta.Text);
                            venta.MontoDscto = Convert.ToDouble(txtDscto.Text);
                            venta.Igv = Convert.ToDouble(txtIGV.Text);
                            venta.Total = Convert.ToDouble(txtPrecioVenta.Text);
                            venta.CodUser = frmLogin.iCodUser;
                            if (txtCotizacion.Text == "") { venta.CodCotizacion = 0; } else { venta.CodCotizacion = Convert.ToInt32(txtCotizacion.Text); }
                            venta.Estado = 1;
                            
                            factura = AdmVenta.FechaCorrelativoAnterior(venta.CodSerie);
                       
                            if (Proceso == 1)
                            {
                                if (factura.FechaSalida > venta.FechaSalida.Date)
                                {
                                    MessageBox.Show("Error No se puede Registrar los Datos. Verifique Fecha");
                                }
                                else
                                {
                                    if (AdmVenta.insert(venta))
                                    {
                                        RecorreDetalle();
                                        if (detalle1.Count > 0)
                                        {
                                            foreach (clsDetalleFacturaVenta det in detalle1)
                                            {
                                                AdmVenta.insertdetalle(det);
                                                if (det.CodDetalleVenta == 0)
                                                {
                                                    MessageBox.Show("Error No se puede Registrar los Datos. Falta Stock de Productos");
                                                    AdmVenta.rollback(Convert.ToInt32(venta.CodFacturaVenta));
                                                    //break;
                                                    return;
                                                }
                                                
                                            }
                                            
                                            
                                        }
                                        if (ckbguia.Checked)
                                        {
                                            guia.CodAlmacen = frmLogin.iCodAlmacen;
                                            guia.CodTipoDocumento = 11; //codigo de documento Guia de Remision
                                            guia.CodSerie = 10; //codigo de serie 001 de la guia de remision
                                            guia.CodMotivo = 0; //codigo de motivo venta
                                            if (CodPedido != 0)
                                            {
                                                guia.CodPedido = CodPedido;
                                            }
                                            guia.FechaEmision = dtpFecha.Value;
                                            guia.FechaTraslado = venta.FechaSalida;
                                            guia.CodCliente = Convert.ToInt32(txtCodigoCli.Text);
                                            ;
                                            guia.CodVehiculoTransporte = 7; //codigo del vehiculo de la empresa
                                            guia.CodConductor = 6; // codigo del conductor
                                            guia.Facturado = 1;
                                            guia.CodFactura = Convert.ToInt32(venta.CodFacturaVenta);
                                            guia.Comentario = txtComentario.Text;
                                            guia.CodUser = frmLogin.iCodUser;
                                            guia.Estado = 1;
                                            // Para saber si la nota esta activa o anulada. El estado se podra cambiar en una ventana especifica para anular notas

                                            if (AdmGuia.insert(guia))
                                            {
                                                RecorreDetalleGuia();
                                                if (detalleg.Count > 0)
                                                {
                                                    foreach (clsDetalleGuiaRemision detg in detalleg)
                                                    {
                                                        AdmGuia.insertdetalle(detg);
                                                        AdmNota.ActualizaCantidadPendienteVenta(detg.Cantidad, detg.CodProducto, Convert.ToInt32(venta.CodFacturaVenta));
                                                    }
                                                    AdmGuia.insertrelacionguia(Convert.ToInt32(guia.CodGuiaRemision), guia.CodFactura, frmLogin.iCodAlmacen, frmLogin.iCodUser);
                                                }
                                                MessageBox.Show("Se ha generado la guia de remision correspondiente",
                                                    "Guia Remision", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //this.Close();                                
                                            }
                                        }

                                        MessageBox.Show("Los datos se guardaron correctamente", "Venta",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtNumDoc.Text = venta.CodFacturaVenta.PadLeft(11, '0');
                                        ltaventa.Add(venta);
                                        if (fpago.Dias == 0 && venta.CodTipoTransaccion == 7)
                                            //se comprueba que el pago sea al contado y que la trnasaccion sea ingreso por compra
                                        {
                                            ingresarpago();
                                        }
                                        CodVenta = venta.CodFacturaVenta;
                                        Proceso = 0;
                                        CargaVenta();
                                        sololectura(true);
                                    }
                                }
                             
                            }
                            else if (Proceso == 2)
                            {
                                if (AdmVenta.update(venta))
                                {
                                    RecorreDetalle();
                                    foreach (clsDetalleFacturaVenta det in venta.Detalle)
                                    {
                                        foreach (clsDetalleFacturaVenta det1 in detalle1)
                                        {
                                            if (det.Equals(det1))
                                            {
                                                AdmVenta.updatedetalle(det1);
                                                return;
                                            }
                                        }
                                        //AdmVenta.deletedetalle(det.CodDetalleSalida);
                                    }
                                    foreach (clsDetalleFacturaVenta deta in detalle1)
                                    {
                                        if (deta.CodDetalleVenta == 0)
                                        {
                                            AdmVenta.insertdetalle(deta);
                                        }
                                    }

                                    MessageBox.Show("Los datos se actualizaron correctamente", "Venta",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                        }
                }
            }
        }

        private void ingresarpago()
        {
            frmCancelarPago form = new frmCancelarPago();
            form.CodNota = venta.CodFacturaVenta;
            form.tipo = 3;
            form.Monto = venta.Total;
            form.venta = venta;
            form.ShowDialog();
        }


        private void RecorreDetalle()
        {
            detalle.Clear();
            detalle1.Clear();
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
            deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
            //deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
            //deta.Subtotal = Convert.ToDouble(fila.Cells[valorventa.Name].Value);
            deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
            deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
            deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
            //  deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
            deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
            deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
            deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
            deta.CodUser = frmLogin.iCodUser;
            deta.CantidadPendiente = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);

            if (Procede == 3)//cotizacion
            {
                deta.CodDetalleCotizacion = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
            }
            else// venta
            {
                deta.CodDetalleCotizacion = 0;
            }
            detalle1.Add(deta);
        }

        private void RecorreDetalleGuia()
        {
            detalle.Clear();
            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    añadedetalleguia(row);
                }
            }
            //nota.Detalle = detalle;
        }
        private void añadedetalleguia(DataGridViewRow fila)
        {
            clsDetalleGuiaRemision deta = new clsDetalleGuiaRemision();
            deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            deta.CodGuiaRemision = Convert.ToInt32(guia.CodGuiaRemision);
            deta.CodAlmacen = frmLogin.iCodAlmacen;
            deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
            deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            if (Convert.ToBoolean(guia.Facturado)) { deta.CantidadPendiente = 0; deta.Pendiente = false; } else { deta.CantidadPendiente = deta.Cantidad; deta.Pendiente = true; }
            deta.CodUser = frmLogin.iCodUser;
            detalleg.Add(deta);
        }

        private void txtPedido_Leave(object sender, EventArgs e)
        {
            //VerificarCabecera();
            //if (Validacion && Proceso == 1)
            //{
            //    btnDetalle.Enabled = true;
            //}
        }

        private void txtAutorizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmAutorizado"] != null)
                {
                    Application.OpenForms["frmAutorizado"].Activate();
                }
                else
                {
                    frmAutorizado form = new frmAutorizado();
                    form.Proceso = 3;                   
                    form.ShowDialog();
                    aut = form.aut;
                    CodAutorizado = aut.CodAutorizado;
                    if (CodAutorizado != 0) { CargaAutorizado(); ProcessTabKey(true); }
                }
            }
        }

        private void CargaAutorizado()
        {
            aut = AdmAut.MuestraAutorizado(CodAutorizado);
            txtAutorizacion.Text = aut.CodAutorizado.ToString();
            lbAutorizado.Text = aut.Nombre;            
        }

        private void txtAutorizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtAutorizacion.Text != "")
                {
                    if (BuscaAutorizado())
                    {
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("El codigo no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private Boolean BuscaAutorizado()        
        {
            aut = AdmAut.MuestraAutorizado(Convert.ToInt32(txtAutorizacion.Text));
            if (aut != null)
            {
                lbAutorizado.Text = aut.Nombre;
                CodAutorizado = aut.CodAutorizado;
                return true;                
            }
            else
            {
                lbAutorizado.Text = "";
                CodAutorizado = 0;
                return false;
            }
        }

        private void txtAutorizacion_Leave(object sender, EventArgs e)
        {
            //if (CodAutorizado== 0)
            //{
            //    txtAutorizacion.Focus();
            //}
            //VerificarCabecera();
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
                    form.Proceso = 3;
                    form.DocSeleccionado = CodDocumento;
                    form.ShowDialog();
                    ser = form.ser;
                    CodSerie = ser.CodSerie;
                    if (CodSerie != 0)
                    {
                        txtSerie.Text = ser.Serie;
                        //if (Procede != 4) txtNumero.Text = ser.Numeracion.ToString();
                        //else txtNumero.Text = numSerie;
                    }
                    if (CodSerie != 0) { ProcessTabKey(true); }
                }
            }
        }

        public void txtPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Return)
            //{
            //    if (txtPedido.Text != "")
            //    {
            //        if (BuscaPedido())
            //        {
            //            CargaPedido();
            //            ProcessTabKey(true);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Pedido no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE SALIDA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //}
        }

        private void txtCodDocumento_TextChanged(object sender, EventArgs e)
        {   
            txtSerie.Text = "";
            txtNumero.Text = "";
            CodSerie = 0;           
        }

        private void txtComentario_Leave(object sender, EventArgs e)
        {
            //VerificarCabecera();
        }

        public void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Proceso != 3)
            {
                fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
                if (fpago.Dias > forma.Dias)
                {
                    DialogResult result =
                        MessageBox.Show("Esta forma de pago excede a la Forma de Pago del Cliente" + Environment.NewLine +
                                        "Máx.FormaPago del Cliente = " + forma.Descripcion, "Facturación Venta",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (result == DialogResult.OK)
                    {
                        cmbFormaPago.SelectedValue = forma.CodFormaPago;
                        //cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, eeee);
                    }
                }
                else
                {
                    CargaListaPrecios(Convert.ToInt32(cmbFormaPago.SelectedValue));
                    dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);

                    EventArgs eeee = new EventArgs();
                    cbListaPrecios_SelectionChangeCommitted(cbListaPrecios, eeee);
                }
            }
            else
            {
                fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
                CargaListaPrecios(Convert.ToInt32(cmbFormaPago.SelectedValue));
                dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias);

                EventArgs eeee = new EventArgs();
                cbListaPrecios_SelectionChangeCommitted(cbListaPrecios, eeee);
            }


        }

        private void cbListaPrecios_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CodLista = Convert.ToInt32(cbListaPrecios.SelectedValue);
            
                actualizaprecios();
                calculatotales();
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
            
        }

        private void actualizaprecios()
        {
            try
            {

                if (Proceso != 0)
                {
                    if (Procede != 3)
                    {
                        Int32 codProduct = 0;
                        Double precioa, cantidada, brutoa, montodescuentoa, valorventaa, igva, precioventaa, precioreala, valorreala, factorigva;
                        DataTable precios = admLista.CargaListaPrecios(Convert.ToInt32(cbListaPrecios.SelectedValue));

                        foreach (DataGridViewRow row in dgvDetalle.Rows)
                        {
                            codProduct = Convert.ToInt32(row.Cells[codproducto.Name].Value);
                            foreach (DataRow r in precios.Rows)
                            {

                                if (codProduct == Convert.ToInt32(r["codProducto"].ToString()))
                                {
                                    if (cmbMoneda.SelectedIndex == 1)
                                    {
                                        precioa = Convert.ToDouble(r["precio"]);
                                        row.Cells[preciounit.Name].Value = String.Format("{0:#,##0.00}", r["precio"]);
                                        cantidada = Convert.ToDouble(row.Cells[cantidad.Name].Value);
                                        brutoa = cantidada * precioa;
                                        row.Cells[importe.Name].Value = String.Format("{0:#,##0.00}", brutoa);

                                        precioventaa = brutoa * (1 - (Convert.ToDouble(row.Cells[dscto1.Name].Value) / 100)) * (1 - (Convert.ToDouble(row.Cells[dscto2.Name].Value) / 100)) * (1 - (Convert.ToDouble(row.Cells[dscto3.Name].Value) / 100));
                                        montodescuentoa = brutoa - precioventaa;
                                        row.Cells[montodscto.Name].Value = String.Format("{0:#,##0.00}", montodescuentoa);
                                        if (r["precioneto"].ToString().Equals(r["precio"].ToString()))
                                        {
                                            valorventaa = precioventaa;
                                        }
                                        else
                                        {
                                            factorigva = frmLogin.Configuracion.IGV / 100 + 1;
                                            valorventaa = precioventaa / factorigva;
                                        }
                                        igva = precioventaa - valorventaa;
                                        precioreala = precioventaa / cantidada;
                                        valorreala = valorventaa / cantidada;
                                        row.Cells[precioventa.Name].Value = String.Format("{0:#,##0.00}", precioventaa);
                                        row.Cells[valorventa.Name].Value = String.Format("{0:#,##0.00}", valorventaa);
                                        row.Cells[precioreal.Name].Value = String.Format("{0:#,##0.00}", precioreala);
                                        row.Cells[valoreal.Name].Value = String.Format("{0:#,##0.00}", valorreala);
                                        row.Cells[igv.Name].Value = String.Format("{0:#,##0.00}", igva);
                                    }
                                    else
                                    {
                                        precioa = Convert.ToDouble(r["precio"]) * Convert.ToDouble(txtTipoCambio.Text);
                                        //row.Cells[preciounit.Name].Value = String.Format("{0:#,##0.00}", r["precio"]);
                                        row.Cells[preciounit.Name].Value = String.Format("{0:#,##0.00}", precioa);
                                        cantidada = Convert.ToDouble(row.Cells[cantidad.Name].Value);
                                        brutoa = cantidada * precioa;
                                        row.Cells[importe.Name].Value = String.Format("{0:#,##0.00}", brutoa);

                                        precioventaa = brutoa * (1 - (Convert.ToDouble(row.Cells[dscto1.Name].Value) / 100)) * (1 - (Convert.ToDouble(row.Cells[dscto2.Name].Value) / 100)) * (1 - (Convert.ToDouble(row.Cells[dscto3.Name].Value) / 100));
                                        montodescuentoa = brutoa - precioventaa;
                                        row.Cells[montodscto.Name].Value = String.Format("{0:#,##0.00}", montodescuentoa);
                                        if (r["precioneto"].ToString().Equals(r["precio"].ToString()))
                                        {
                                            valorventaa = precioventaa;
                                        }
                                        else
                                        {
                                            factorigva = frmLogin.Configuracion.IGV / 100 + 1;
                                            valorventaa = precioventaa / factorigva;
                                        }
                                        igva = precioventaa - valorventaa;
                                        precioreala = precioventaa / cantidada;
                                        valorreala = valorventaa / cantidada;
                                        row.Cells[precioventa.Name].Value = String.Format("{0:#,##0.00}", precioventaa);
                                        row.Cells[valorventa.Name].Value = String.Format("{0:#,##0.00}", valorventaa);
                                        row.Cells[precioreal.Name].Value = String.Format("{0:#,##0.00}", precioreala);
                                        row.Cells[valoreal.Name].Value = String.Format("{0:#,##0.00}", valorreala);
                                        row.Cells[igv.Name].Value = String.Format("{0:#,##0.00}", igva);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txtPDescuento_TextChanged(object sender, EventArgs e)
        {
            calculadescuentogeneral();
        }

        private void txtPDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                calculadescuentogeneral();             
            }            
        }

        private void calculadescuentogeneral()
        {
            //Double brutodg = 0;
            //Double dsctodg = 0;
            //Double DsctoGlobal = 0;
            //Double precioventadg = 0;
            //Double valorventadg = 0;

            //if (txtBruto.Text != "") { brutodg = Convert.ToDouble(txtBruto.Text); } else { brutodg = 0; }
            //if (txtDscto.Text != "") { dsctodg = Convert.ToDouble(txtDscto.Text); } else { dsctodg = 0; }

            //if (txtPDescuento.Text != "" && txtPrecioVenta.Text != "")
            //{
            //    DsctoGlobal = (Convert.ToDouble(txtBruto.Text) - dsctodg) * (Convert.ToDouble(txtPDescuento.Text) / 100);
            //    txtDsctoGobal.Text = String.Format("{0:#,##0.00}", DsctoGlobal.ToString());
            //    precioventadg = brutodg - dsctodg - DsctoGlobal;
            //    txtPrecioVenta.Text = String.Format("{0:#,##0.00}", precioventadg);
            //    //valorventadg = precioventadg / (1 + (frmLogin.Configuracion.IGV / 100));
            //    txtValorVenta.Text = String.Format("{0:#,##0.00}", valorventadg);
            //    txtIGV.Text = String.Format("{0:#,##0.00}", precioventadg - valorventadg);
            //    //txtValorVenta.Text = String.Format("{0:#,##0.00}", precioventadg * frmLogin.Configuracion.IGV / 100);
            //    //txtIGV.Text = String.Format("{0:#,##0.00}", precioventadg * (1 - (frmLogin.Configuracion.IGV / 100)));
            //}
            //else
            //{
            //    DsctoGlobal = 0;
            //    txtDsctoGobal.Text = String.Format("{0:#,##0.00}", DsctoGlobal.ToString());
            //    calculatotales();
            //}   
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Boolean rpta;
            try
            {
                if (AdmVenta.ActualizaEstadoImpreso(Convert.ToInt32(venta.CodFacturaVenta)))
                {
                    rpta = true;
                }
                else rpta = false;

                ser = Admser.MuestraSerie(venta.CodSerie, frmLogin.iCodAlmacen);
                CRReporteFactura rpt = new CRReporteFactura();
                frmRptFactura frm = new frmRptFactura();
                CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rpt.PrintOptions;
                rptoption.PrinterName = ser.NombreImpresora;//Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3]);
                rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);//(CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3]), "documentoFioviza");           
                rpt.SetDataSource(ds.ReporteFactura2(Convert.ToInt32(venta.CodFacturaVenta)).Tables[0]);
                frm.crvReporteFactura.ReportSource = rpt;
                frm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
 
            }
            //MessageBox.Show("LA IMPRESORA SE LLAMA:" + Convert.ToString(System.Drawing.Printing.PrinterSettings.InstalledPrinters[3])+ ";", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private Boolean BuscaGuia()
        {
            guia = AdmGuia.BuscaGuiaRemision(txtGuias.Text, frmLogin.iCodAlmacen);            
            if (guia != null)
            {
                CodGuia = Convert.ToInt32(guia.CodGuiaRemision);
                return true;
            }
            else
            {
                CodGuia = 0;
                return false;
            }
        }

        private void CargaGuia()
        {
            try
            {
                guia = AdmGuia.CargaGuiaRemision(Convert.ToInt32(CodGuia));
                if (guia != null)
                {
                    txtGuias.Text = guia.CodGuiaRemision;
                    

                    if (txtCodCliente.Enabled)
                    {
                        CodCliente = guia.CodCliente;

                        if (txtCodCliente.Enabled)
                        {
                            CodCliente = guia.CodCliente;
                            cli = AdmCli.MuestraCliente(CodCliente);
                            txtCodCliente.Text = cli.CodigoPersonalizado;
                            if (cli.Ruc != "")
                            {
                                txtDocRef.Text = "FT";
                                KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                                txtTransaccion_KeyPress(txtDocRef, ee);
                                txtSerie.Text = "001";
                                txtTransaccion_KeyPress(txtDocRef, ee);
                            }
                            else
                            {
                                txtDocRef.Text = "BV";
                                KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                                txtTransaccion_KeyPress(txtDocRef, ee);
                                txtSerie.Text = "001";
                                txtTransaccion_KeyPress(txtDocRef, ee);
                            }

                            txtNombreCliente.Text = cli.RazonSocial;
                            //txtDireccion.Text = cli.DireccionLegal;

                            //cbListaPrecios.SelectedValue = cli.CodListaPrecio;
                            //EventArgs eee = new EventArgs();
                            //cbListaPrecios_SelectionChangeCommitted(cbListaPrecios, eee);
                            if (cli.CodListaPrecio != 0)
                            {
                                EventArgs ee = new EventArgs();
                                cbListaPrecios_SelectionChangeCommitted(cbListaPrecios, ee);
                            }
                            else
                            {
                                CodLista = 0;
                            }

                            //cmbFormaPago.SelectedValue = cli.FormaPago;
                            if (cli.FormaPago != 0)
                            {
                                EventArgs ee = new EventArgs();
                                cmbFormaPago_SelectionChangeCommitted(cmbFormaPago, ee);
                            }
                            else
                            {
                                dtpFechaPago.Value = DateTime.Today;
                            }

                            txtPDescuento.Text = cli.Descuento.ToString();

                        }   
                    }
                    //dtpFecha.Value = guia.FechaEmision;                  
                    txtComentario.Text = guia.Comentario;

                    CargaDetalleGuia();
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
 
        }

        private void btnNuevaVenta_Click(object sender, EventArgs e)
        {
            frmVenta form2 = new frmVenta();
            form2.MdiParent = this.MdiParent;
            form2.Proceso = 1;
            form2.Show();
            this.Close();
        }

        private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (Procede == 1)
            {
                if (Proceso == 1)
                {
                    if (txtPDescuento.Text != "")
                    {
                        calculatotales();
                        calculadescuentogeneral();
                    }
                    else
                    {
                        calculatotales();
                    }

                    if (dgvDetalle.RowCount > 0)
                    {
                        int Indice = 0;
                        Indice = dgvDetalle.RowCount - 1;

                        if (cmbMoneda.SelectedIndex == 0)
                        {
                            if (TipoCambio != 0)
                            {
                                dgvDetalle[8, Indice].Value = Convert.ToDecimal(dgvDetalle[8, Indice].Value)*TipoCambio;
                                dgvDetalle[9, Indice].Value = Convert.ToDecimal(dgvDetalle[9, Indice].Value)*TipoCambio;
                                dgvDetalle[13, Indice].Value = Convert.ToDecimal(dgvDetalle[13, Indice].Value)*
                                                               TipoCambio;
                                dgvDetalle[14, Indice].Value = Convert.ToDecimal(dgvDetalle[14, Indice].Value)*
                                                               TipoCambio;
                                dgvDetalle[15, Indice].Value = Convert.ToDecimal(dgvDetalle[15, Indice].Value)*
                                                               TipoCambio;
                                dgvDetalle[16, Indice].Value = Convert.ToDecimal(dgvDetalle[16, Indice].Value)*
                                                               TipoCambio;
                            }
                        }
                        else if (cmbMoneda.SelectedIndex == 1)
                        {
                        }
                    }
                }
            }
        }

        private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (Procede == 1 || Procede == 2)
            {
                if (dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa")
                {
                    if (Proceso == 1 || Proceso == 2)
                    {
                        if (txtPDescuento.Text != "")
                        {
                            calculatotales();
                            calculadescuentogeneral();
                        }
                        else
                        {
                            calculatotales();
                        }
                    }
                }
            }
        }

        private void customValidator4_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            //ComboBox c = (ComboBox)e.ControlToValidate;
            //if (c.Enabled)
            //    if (Proceso != 0)
            //        if (c.SelectedIndex != -1)
            //            e.IsValid = true;
            //        else
            //            e.IsValid = false;
            //    else
            //        e.IsValid = true;
            //else
            //    e.IsValid = true;
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

     

     

    

        private void cmbMoneda_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbMoneda.Text != Moneda)
            {
                if (dgvDetalle.RowCount > 0)
                {
                    VentaEnMoneda();
                }
            }

            Moneda = cmbMoneda.Text;
            calculatotales();
        }

        private void txtGuias_KeyDown(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    AdmVenta.deletedetalleventasalida();
            //    if (txtCodCliente.Text != "")
            //    {
            //        if (e.KeyCode == Keys.F1)
            //        {
            //            if (Application.OpenForms["frmNotaOrdenAlmacen"] != null)
            //            {
            //                Application.OpenForms["frmNotaOrdenAlmacen"].Activate();
            //            }
            //            else
            //            {
            //                frmNotaOrdenAlmacen form = new frmNotaOrdenAlmacen();
            //                form.proceso = 12;
            //                form.ltacodnotasalida = documento;
            //                form.CodCli = Convert.ToInt32(txtCodigoCli.Text);
            //                txtGuias.Focus();
            //                form.CargaGuiaVenta(Convert.ToInt32(txtCodigoCli.Text));
            //                form.textBox1.Visible = true;
            //                form.groupBox1.Location = new Point(0, 12);
            //                form.dgvDetalle2.Columns[0].ReadOnly = false;
            //                form.Show();

            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Seleccione un Cliente");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(" " + ex);
            //}
        }

        private void customValidator5_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (dgvDetalle.Rows.Count > 0)
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        public void txtCotizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtCotizacion.Text != "")
                {
                    if (BuscaCotizacion())
                    {
                        CargaCotizacion();
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("Cotizacion no existe o ya no esta vigente", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ext.limpiar(this.Controls);
                    }
                }
            }
        }

        private Boolean BuscaCotizacion()
        {
            coti = AdmCoti.BuscaCotizacion(txtCotizacion.Text, frmLogin.iCodAlmacen);
            if (coti != null)
            {
                codCotizacion = Convert.ToInt32(coti.CodCotizacion);
                return true;
            }
            else
            {
                codCotizacion = 0;
                return false;
            }

        }

        private void CargaCotizacion()
        {
            try
            {
                coti = AdmCoti.CargaCotizacion(Convert.ToInt32(codCotizacion), frmLogin.iCodAlmacen);
                if (coti != null)
                {
                    txtCotizacion.Text = coti.CodCotizacion;
                    if (txtCodCliente.Enabled)
                    {
                        CodCliente = coti.CodCliente;
                        CargaCliente();
                        //
                        if (ret == 0)
                        {
                            //if (coti.TipoCliente == 0)
                            //{
                                txtCodigoCli.Text = coti.CodCliente.ToString();
                                txtCodCliente.Text = coti.CodigoPersonalizado;
                                txtNombreCliente.Text = coti.Nombre;
                                txtDireccionCliente.Text = coti.Direccion;                            
                                txtLineaCredito.Text = cli.LineaCredito.ToString();
                                txtLineaCreditoDisponible.Text = cli.LineaCreditoDisponible.ToString();
                                txtLineaCreditoUso.Text = cli.LineaCreditoUsado.ToString();

                            //}
                            //else
                            //{

                            //    txtCodCliente.Text = coti.RUCCliente;
                            //    txtNombreCliente.Text = coti.RazonSocialCliente;
                            //}
                            if (coti.RUCCliente != "")// hay que automatizar esto dependiendo de la sucursal la serie varia por sucursal
                            {
                                txtDocRef.Text = "FT";
                                KeyPressEventArgs ee = new KeyPressEventArgs((char) Keys.Return);
                                txtDocRef_KeyPress(txtDocRef, ee);
                                txtSerie.Text = "001";
                                txtSerie_KeyPress(txtDocRef, ee);
                            }
                            else
                            {
                                txtDocRef.Text = "BV";
                                KeyPressEventArgs ee = new KeyPressEventArgs((char) Keys.Return);
                                txtDocRef_KeyPress(txtDocRef, ee);
                                txtSerie.Text = "001";
                                txtSerie_KeyPress(txtDocRef, ee);
                            }
                            //dtpFecha.Value = coti.FechaCotizacion;
                            cmbMoneda.SelectedValue = coti.Moneda;
                            txtTipoCambio.Text = coti.TipoCambio.ToString();
                            cmbFormaPago.SelectedValue = coti.FormaPago;
                            CargaListaPrecios(Convert.ToInt32(cmbFormaPago.SelectedValue));
                            cbListaPrecios.SelectedValue = coti.CodListaPrecio;
                            txtComentario.Text = coti.Comentario;
                            txtBruto.Text = String.Format("{0:#,##0.00}", coti.MontoBruto);
                            txtDscto.Text = String.Format("{0:#,##0.00}", coti.MontoDscto);
                            txtValorVenta.Text = String.Format("{0:#,##0.00}", coti.Total - coti.Igv);
                            txtIGV.Text = String.Format("{0:#,##0.00}", coti.Igv);
                            txtPrecioVenta.Text = String.Format("{0:#,##0.00}", coti.Total);
                            CargaDetalleCotizacion();
                            BloquearEdicion(true);
                        }
                    }
                   
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void txtCotizacion_Leave(object sender, EventArgs e)
        {
            //VerificarCabecera();
        }

        public void txtGuias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtGuias.Text != "")
                {
                    if (BuscaGuia())
                    {
                        CargaGuia();
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("Pedido no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        
        public List<Int32> carga_Correlativos()
        {
            Int32 j = 0;
            datos = AdmVenta.ListaFacturaVenta(frmLogin.iCodAlmacen);
            ser = Admser.MuestraSerie(8, frmLogin.iCodAlmacen);
            correlativo.Clear();
            for (int i = ser.Inicio; i < ser.Numeracion; i++)
            {
                if(j< datos.Rows.Count)
                {
                    if(i == Convert.ToInt32(datos.Rows[j]["numDocumento"]))
                    {
                        j++;
                        fecha1 = Convert.ToDateTime(datos.Rows[j-1]["fechasalida"]);
                    }
                    
                    else
                    {
                        correlativo.Add(i);
                        fecha2 = Convert.ToDateTime(datos.Rows[j]["fechasalida"]);
                    }
                    
                }
            }
            return correlativo;
        }

        private Boolean rpta;

        public Boolean valida_existente(Int32 serie)
        {
            datos = AdmVenta.ListaFacturaVenta(frmLogin.iCodAlmacen);
            for (Int32 j = 0; j < datos.Rows.Count; j++)
            {
                if (serie == Convert.ToInt32(datos.Rows[j]["numDocumento"]))
                {
                    rpta = false;
                }
                else rpta = true;
            }
            return rpta;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmFacturasManuales frm = new frmFacturasManuales();
            carga_Correlativos();
            frm.num_correlativo = carga_Correlativos();
            frm.ShowDialog();
        }

        private void cbListaPrecios_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cmbFormaPago.SelectedValue != null)
            //{
            //    CodLista = Convert.ToInt32(cbListaPrecios.SelectedValue);

            //    actualizaprecios();
            //    calculatotales();
            //    btnNuevo.Enabled = true;
            //    btnEditar.Enabled = true;
            //    btnEliminar.Enabled = true;
            //}
        }

        
    }
}
