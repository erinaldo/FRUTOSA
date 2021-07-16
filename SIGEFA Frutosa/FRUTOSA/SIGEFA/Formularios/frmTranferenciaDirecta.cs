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
    public partial class frmTranferenciaDirecta : DevComponents.DotNetBar.OfficeForm
    {
        clsNotaIngreso notaIngreso=new clsNotaIngreso();
        clsAdmNotaIngreso admNotaIngreso = new clsAdmNotaIngreso();
        clsNotaSalida nota=new clsNotaSalida();
        clsAdmNotaSalida admnota=new clsAdmNotaSalida();
        clsAdmGuiaRemision admGuia = new clsAdmGuiaRemision();
        clsGuiaRemision guia = new clsGuiaRemision();
        clsConsultasExternas ext = new clsConsultasExternas();
        TextBox txtedit = new TextBox();
        clsValidar val = new clsValidar();
        clsAdmAlmacen Admalmac = new clsAdmAlmacen();
        //clsReporteFactura ds = new clsReporteFactura();
        clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();
        clsTipoDocumento doc = new clsTipoDocumento();
        clsSerie ser = new clsSerie();
        clsAdmSerie admSerie = new clsAdmSerie();
        public List<Int32> codProd = new List<int>();
        public List<clsDetalleNotaSalida> detalle = new List<clsDetalleNotaSalida>();
        public List<clsDetalleNotaIngreso> detalleingreso = new List<clsDetalleNotaIngreso>();
        public List<clsDetalleGuiaRemision> detalleguia = new List<clsDetalleGuiaRemision>();
        public String CodTransferencia;
        public Int32 CodDocumento;
        public Int32 CodSerie, num;
        public Int32 Tipo, guarda=0;
        Boolean Validacion = true;
        public Int32 Proceso = 0; //(1) Nuevo (2) Editar (3) Consulta
        public Int32 Procede = 0;
        public Int32 Proceso_txd = 0;
        clsGuia ds = new clsGuia();
        public Int32 estado_vigente = 0;

        clsVehiculoTransporte vehiculotransporte = new clsVehiculoTransporte();
        clsAdmVehiculoTransporte admVehiculoTransporte = new clsAdmVehiculoTransporte();
        clsConductor conductor = new clsConductor();
        clsAdmConductor admConductor = new clsAdmConductor();

        clsRequerimiento requerimiento = new clsRequerimiento();
        clsDetalleRequerimiento detallerequerimiento = new clsDetalleRequerimiento();

        clsAdmRequerimiento admrequerimiento = new clsAdmRequerimiento();

        //public DataTable DtValida = new DataTable();

        public DataTable DtAuxiliar_pr = new DataTable();
        public DataTable DtAuxiliar_pasados_pr = new DataTable();

        public frmTranferenciaDirecta()
        {
            InitializeComponent();
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
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
                    form.Procedencia = 1;
                    form.Transaccion = "TD";
                    form.ShowDialog();
                    doc = form.doc;
                    CodDocumento = doc.CodTipoDocumento;
                    txtDocRef.Text = doc.Sigla;
                    txtCodDoc.Text = doc.CodTipoDocumento.ToString();
                    if (CodDocumento != 0) { ProcessTabKey(true); }                    
                }
            }
        }
   
        private void frmNotaSalida_Load(object sender, EventArgs e)
        {
            txtTransaccion.Text = "TD";
            cmbTransferencia.SelectedIndex = 0;
            nota.CodTipoTransaccion = 15;
            notaIngreso.CodTipoTransaccion = 15;
            txtDocRef.Focus();
            cargaAlmacenes();

            cargaAlmacenesDestino();

            CargaTransportista();
            CargaVehiculoTrasnporte();

            if (Proceso == 2)
            {
                CargaTransferencia();
            }
            else if (Proceso == 3)
            {
                CargaTransferencia();
                label20.Visible = false;
                txtRequerimiento.Visible = false;
                dgvDetalle.Columns["stockactual"].Visible = false;
                dgvDetalle.Columns["comentario_usu"].Visible = false;
                sololectura(true);
                sololecturatransferencia(true);
                txtComentario.Focus();
            }
            else if (Proceso == 4)
            {
                CargaTransferencia();
                sololectura(true);
            }

            if (Proceso != 3)
            {

                String cod = "";

                cod = (frmLogin.iCodAlmacen < 10) ? frmLogin.iCodAlmacen.ToString().PadLeft(2, '0') : frmLogin.iCodAlmacen.ToString();

                cmbAlmacen.SelectedValue = cod;
            }
            
        }

        private void sololecturatransferencia(Boolean p)
        {
            btnGuardar.Visible = p;
            txtComentario.Enabled = !p;
            txtComentario.ReadOnly = !p;
            cmbAlmacenDestino.Enabled = !p;
            cmbTransportista.Enabled = !p;
            cmbVehiculo.Enabled = !p;
            txtDocRef.Enabled = !p;
            txtSerie.Enabled = !p;
            dtpEmision.Enabled = !p;
            dtpTraslado.Enabled = !p;
        }

        private void cargaAlmacenes()
        {
            cmbAlmacen.DataSource = Admalmac.ListaAlmacen2();
            cmbAlmacen.DisplayMember = "nombre";
            cmbAlmacen.ValueMember = "codAlmacen";
            //cmbAlmacen.SelectedIndex = -1;

            cmbAlmacen.Enabled = false;

            //MessageBox.Show(frmLogin.iCodAlmacen.ToString());
        }

        private void cargaAlmacenesDestino() 
        {
            cmbAlmacenDestino.DataSource = Admalmac.ListaAlmacen2();
            cmbAlmacenDestino.DisplayMember = "nombre";
            cmbAlmacenDestino.ValueMember = "codAlmacen";
            cmbAlmacenDestino.SelectedIndex = -1;
        }

        private void CargaTransportista() 
        {
            cmbTransportista.DataSource = admConductor.CargaConductores();
            cmbTransportista.DisplayMember = "nombre";
            cmbTransportista.ValueMember = "codConductor";
            cmbTransportista.SelectedIndex = -1;
        }

        private void CargaVehiculoTrasnporte() 
        {
            cmbVehiculo.DataSource = admVehiculoTransporte.CargaVehiculoTransportes();
            cmbVehiculo.DisplayMember = "placa";
            cmbVehiculo.ValueMember = "codVehiculoTransporte";
            cmbVehiculo.SelectedIndex = -1;
        }

        private void CargaTransferencia()
        {
            try
            {
                notaIngreso = admNotaIngreso.CargaNotaIngreso(Convert.ToInt32(CodTransferencia));
                if (nota != null)
                {
                    txtNumDoc.Text = notaIngreso.CodNotaIngreso;
                    cmbTransferencia.Text=notaIngreso.DescripcionTransaccion;
                    txtDocRef.Text = notaIngreso.SiglaDocumento;
                    doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
                    txtSerie.Text = notaIngreso.Serie;
                    txtNumero.Text = notaIngreso.NumDoc;
                    txtcodserie.Text = notaIngreso.CodSerie.ToString();
                    if (notaIngreso.CodAlmacen == 1) cmbAlmacenDestino.SelectedValue = "01";
                    else cmbAlmacenDestino.SelectedValue = notaIngreso.CodAlmacen;
                    if (notaIngreso.codalmacenemisor == 1) cmbAlmacen.SelectedValue="01";
                    else cmbAlmacen.SelectedValue = notaIngreso.codalmacenemisor;
                    txtComentario.Text = notaIngreso.Comentario;
                    cmbVehiculo.SelectedValue = notaIngreso.Codvehiculotransporte;
                    cmbTransportista.SelectedValue = notaIngreso.Codconductor;

                    if (cmbTransferencia.SelectedIndex == 0) 
                    {
                        guia = admGuia.CargaGuiaTransferencia(Convert.ToInt32(notaIngreso.CodNotaIngreso));
                        if (guia != null)
                        {
                            //fechaemision - guia
                            dtpEmision.Value=guia.FechaEmision;
                            //fechatraslado - guia
                            dtpTraslado.Value = guia.FechaTraslado;
                        }
                    }
                    dtpFecha.Value = notaIngreso.FechaIngreso;
                    
                    CargaDetalle();
                    ser = admSerie.BuscaSeriexDocumento(doc.CodTipoDocumento,frmLogin.iCodAlmacen);
                    btnGuardar.Enabled = false;
                    btnEliminar.Enabled = false;
                    btnDetalle.Enabled = false;
                    btnImprimir.Enabled = false;
                    btnAceptarTransferencia.Visible = true;
                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void sololectura(Boolean estado)
        {
            dtpFecha.Enabled = !estado;
            txtDocRef.ReadOnly = estado;
            txtNumero.ReadOnly = estado;
            txtComentario.ReadOnly = estado;
            btnEliminar.Visible = !estado;
            btnGuardar.Visible = !estado;
            btnImprimir.Visible = estado;
        }

        private void CargaDetalle()
        {
            dgvDetalle.DataSource = admNotaIngreso.CargaDetalleTransferencia(Convert.ToInt32(notaIngreso.CodNotaIngreso));
            RecorreDetalleIngreso();
            notaIngreso.Detalle= detalleingreso;
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
                    form.DocSeleccionado = doc.CodTipoDocumento;
                    form.Sigla = doc.Sigla;
                    form.Proceso = 3;
                    form.ShowDialog();
                    ser = form.ser;
                    CodSerie = ser.CodSerie;
                    num = ser.Numeracion;
                    if (CodSerie != 0)
                    {
                        if (ser.PreImpreso)
                        {
                            CodSerie = ser.CodSerie;
                            txtSerie.Text = ser.Serie;
                            txtcodserie.Text = ser.CodSerie.ToString();
                            txtNumero.Enabled = true;
                            txtNumero.Text = "";
                        }
                        else {
                            CodSerie = ser.CodSerie;
                            txtSerie.Text = ser.Serie;
                            txtcodserie.Text = ser.CodSerie.ToString();
                            txtNumero.Enabled = false;
                        }
                        
                    }
                    else
                    {
                        txtDocRef.Focus();}
                    if (CodSerie!= 0) {ProcessTabKey(true); }                    
                }
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            chkReqDet.Checked = false;
            if (!txtDocRef.Text.Equals(""))
            {
                if (Application.OpenForms["frmDetalleGuia"] != null)
                {
                    Application.OpenForms["frmDetalleGuia"].Activate();
                }
                else
                {
                    frmDetalleGuia form = new frmDetalleGuia();

                    if (cmbTransferencia.SelectedIndex == 0)
                    {
                        form.Procede = 9;
                        form.Proceso = 1;
                    }
                    else
                    {
                        //form.Procede = 11;
                        form.Procede = 9;
                        form.Proceso = 1;
                        form.codalmacen = Convert.ToInt32(cmbAlmacen.SelectedValue);
                    }
                    form.Text = "Detalle de Productos";
                    if (dgvDetalle.Rows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dgvDetalle.Rows)
                        {
                            codProd.Add(Convert.ToInt32(row.Cells[codproducto.Name].Value));
                        }
                    }
                    else
                    {
                        codProd.Add(0);
                    }
                    form.ShowDialog();
                }
            }
            else 
            {
                MessageBox.Show("Debe elegir un documento");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Int32 contador_veces = 0;
            Int32 estado_vigente = 2;
            Int32 contador_vigentes = 0;
            Boolean resultado_accion = false;
            Int32 cantidad_ceros = 0;

            if (Proceso_txd == 0)
            {
                if (superValidator1.Validate() && guarda == 0)
                {
                    if (Proceso != 0)
                    {
                        if (Proceso != 3)
                        {
                            cantidad_ceros = recorre_ceros();

                            if (cantidad_ceros == dgvDetalle.Rows.Count)
                            {

                                MessageBox.Show("No se puede enviar el requerimiento");
                            }
                            else
                            {
                                if (chkReqDet.Checked)
                                {
                                    //validamos los datos para actualizar los requerimientos
                                    for (int i = 0; i < DtAuxiliar_pr.Rows.Count; i++)
                                    {
                                        //cogemos la cantidad guardada previamente
                                        //if(DtAuxiliar_pasados_pr.Rows[i]["Cantidad"].ToString())
                                        for (int j = 0; j < DtAuxiliar_pasados_pr.Rows.Count; j++)
                                        {
                                            if (Convert.ToInt32(DtAuxiliar_pr.Rows[i]["coddetalle"].ToString()) == Convert.ToInt32(DtAuxiliar_pasados_pr.Rows[j]["coddetalle"].ToString()))
                                            {
                                                //validamos cantidad
                                                if (Convert.ToDecimal(DtAuxiliar_pr.Rows[i]["Cantidad"].ToString()) <= Convert.ToDecimal(DtAuxiliar_pasados_pr.Rows[j]["Cantidad"].ToString()))
                                                {
                                                    estado_vigente = 3;
                                                    contador_vigentes++;
                                                }
                                                else
                                                {
                                                    estado_vigente = 2;
                                                }
                                                contador_veces++;
                                            }
                                            admrequerimiento.actualiza_det_requerimientos_comentario(Convert.ToInt32(DtAuxiliar_pasados_pr.Rows[j]["coddetalle"].ToString()), DtAuxiliar_pasados_pr.Rows[j]["comentario_usu"].ToString());
                                                
                                        }

                                        if (contador_veces == 0) { estado_vigente = 2; }
                                        //else { estado_vigente = 3; }

                                        detallerequerimiento.CodDetalleRequerimiento = Convert.ToInt32(DtAuxiliar_pr.Rows[i]["coddetalle"].ToString());
                                        detallerequerimiento.CodProducto = Convert.ToInt32(DtAuxiliar_pr.Rows[i]["codProducto"].ToString());
                                        //detallerequerimiento.Cantidad = Convert.ToDouble(DtAuxiliar_pasados_pr.Rows[i]["Cantidad"].ToString());
                                       // detallerequerimiento.Comentario = dgvDetalle.CurrentRow.Cells[comentario_usu.Name].Value.ToString();
                                        detallerequerimiento.EstadoVigente = estado_vigente;

                                        resultado_accion = admrequerimiento.actualiza_det_requerimientos_vigentes(detallerequerimiento);


                                        contador_veces = 0;
                                        //contador_vigentes = 0;
                                        estado_vigente = 2;
                                    }

                                    estado_vigente = (contador_vigentes == DtAuxiliar_pr.Rows.Count) ? 3 : 2;

                                    //actualiamos el requerimiento
                                    requerimiento.CodRequerimiento = Convert.ToInt32(DtAuxiliar_pr.Rows[0]["codrequerimiento"].ToString());
                                    requerimiento.Atendido = estado_vigente;

                                    resultado_accion = admrequerimiento.actualiza_requerimientos_vigentes(requerimiento);
                                }

                                //nota de salida 
                                nota.CodSucursal = frmLogin.iCodSucursal;

                                //nota.CodAlmacen = frmLogin.iCodAlmacen; 
                                nota.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue.ToString());
                                nota.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
                                nota.CodSerie = Convert.ToInt32(txtcodserie.Text);
                                nota.Serie = txtSerie.Text;
                                if (txtNumero.Text!="") nota.NumDoc = txtNumero.Text;
                                else nota.NumDoc = ser.Numeracion.ToString();
                                txtNumero.Text = nota.NumDoc;
                                nota.Comentario = txtComentario.Text;
                                nota.FechaSalida = dtpFecha.Value.Date;
                                nota.CodUser = frmLogin.iCodUser;
                                nota.codConductor = Convert.ToInt32(cmbTransportista.SelectedValue.ToString());
                                nota.codVehiculoTransporte = Convert.ToInt32(cmbVehiculo.SelectedValue.ToString());
                                nota.codalmacenreceptor = Convert.ToInt32(cmbAlmacenDestino.SelectedValue.ToString());
                                nota.Moneda = 2;
                                nota.Estado = 1;

                                if (Proceso == 1)
                                {
                                    // Insert Nota y Detalle - Salida
                                    if (admnota.insert(nota))
                                    {
                                        RecorreDetalle();
                                        if (detalle.Count > 0)
                                        {
                                            foreach (clsDetalleNotaSalida det in detalle)
                                            {
                                                if (det.Cantidad > 0)
                                                    admnota.insertdetalle(det);
                                            }
                                        }
                                    }

                                    //Guia Remision 
                                    guia.CodAlmacen = frmLogin.iCodAlmacen;
                                    guia.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
                                    //nota.CodAlmacen = frmLogin.iCodAlmacen;
                                    guia.CodSerie = Convert.ToInt32(txtcodserie.Text);
                                    guia.Serie = txtSerie.Text;
                                    if (txtNumero.Text != "") guia.NumDoc = txtNumero.Text;
                                    else guia.NumDoc = ser.Numeracion.ToString();
                                    if(cmbTransferencia.SelectedIndex==0)
                                        guia.CodMotivo = 5;
                                    else if(cmbTransferencia.SelectedIndex==1)
                                        guia.CodMotivo = 4;
                                    guia.FechaEmision = dtpEmision.Value.Date;
                                    guia.FechaTraslado = dtpTraslado.Value.Date;
                                    guia.CodCliente = 0;
                                    guia.CodVehiculoTransporte = Convert.ToInt32(cmbVehiculo.SelectedValue);
                                    guia.CodConductor = Convert.ToInt32(cmbTransportista.SelectedValue);
                                    guia.CodEmpresaTransporte = 0;
                                    guia.Facturado = 0;
                                    guia.Comentario = txtComentario.Text;
                                    guia.Estado = 1;
                                    guia.CodUser = frmLogin.iCodUser;
                                    guia.CodFactura = Convert.ToInt32(nota.CodNotaSalida);
                                    // Insert Guia y su Detalle
                                    if (admGuia.insert(guia))
                                    {
                                        RecorreDetalleGuia();
                                        if (detalleguia.Count > 0)
                                        {
                                            foreach (clsDetalleGuiaRemision det in detalleguia)
                                            {
                                                if (det.Cantidad > 0)
                                                    admGuia.insertdetalle(det);
                                            }
                                        }
                                    }

                                    // nota de ingreso
                                    notaIngreso.CodAlmacen = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
                                    notaIngreso.codalmacenemisor = Convert.ToInt32(cmbAlmacen.SelectedValue);
                                    notaIngreso.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
                                    notaIngreso.CodSerie = Convert.ToInt32(txtcodserie.Text);
                                    notaIngreso.Serie = txtSerie.Text;
                                    if (txtNumero.Text != "") notaIngreso.NumDoc = txtNumero.Text;
                                    else notaIngreso.NumDoc = ser.Numeracion.ToString();
                                    notaIngreso.Comentario = txtComentario.Text;
                                    notaIngreso.FechaIngreso = dtpFecha.Value.Date;
                                    notaIngreso.CodUser = frmLogin.iCodUser;
                                    notaIngreso.CodReferencia = Convert.ToInt32(nota.CodNotaSalida);
                                    notaIngreso.Moneda = 2;
                                    notaIngreso.Estado = 1;
                                    // Insert Nota y Detalle - Ingreso
                                    if (admNotaIngreso.insert(notaIngreso))
                                    {
                                        RecorreDetalleIngreso();
                                        if (detalleingreso.Count > 0)
                                        {
                                            foreach (clsDetalleNotaIngreso det in detalleingreso)
                                            {
                                                if (det.Cantidad > 0)
                                                    admNotaIngreso.insertdetalle(det);
                                            }
                                        }

                                        MessageBox.Show("Los datos se guardaron correctamente", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        txtNumDoc.Text = nota.CodNotaSalida.ToString().PadLeft(11, '0');
                                        sololectura(true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //actualizamos la glosa de la transferencia
                            if (Proceso == 3)
                            {
                                notaIngreso.CodNotaIngreso = CodTransferencia;
                                notaIngreso.Comentario = txtComentario.Text;

                                resultado_accion = admNotaIngreso.UpdateComentario(notaIngreso);

                                if (resultado_accion == true)
                                {
                                    DevComponents.DotNetBar.MessageBoxEx.Show("Se actualizo la Nota de Ingreso", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                        }

                    }
                }
            }
            else
            {

                if (superValidator1.Validate())
                {
                     //nota de salida 
                                nota.CodSucursal = frmLogin.iCodSucursal;

                                //nota.CodAlmacen = frmLogin.iCodAlmacen; 
                                nota.CodAlmacen = Convert.ToInt32(cmbAlmacen.SelectedValue.ToString());
                                nota.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
                                nota.CodSerie = Convert.ToInt32(txtcodserie.Text);
                                nota.Serie = txtSerie.Text;
                                nota.NumDoc = ser.Numeracion.ToString();
                                txtNumero.Text = nota.NumDoc;
                                nota.Comentario = txtComentario.Text;
                                nota.FechaSalida = dtpFecha.Value.Date;
                                nota.CodUser = frmLogin.iCodUser;
                                nota.codConductor = 0;
                                nota.codVehiculoTransporte = 0;
                                nota.codalmacenreceptor = 0;
                                nota.Moneda = 2;
                                nota.Estado = 1;

                                if (Proceso == 1)
                                {
                                    // Insert Nota y Detalle - Salida
                                    if (admnota.insert(nota))
                                    {
                                        RecorreDetalle();
                                        if (detalle.Count > 0)
                                        {
                                            foreach (clsDetalleNotaSalida det in detalle)
                                            {
                                                if (det.Cantidad > 0)
                                                    admnota.insertdetalle(det);
                                            }
                                        }
                                    }
                                }

                                // nota de ingreso
                                notaIngreso.CodAlmacen = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
                                notaIngreso.codalmacenemisor = Convert.ToInt32(cmbAlmacen.SelectedValue);
                                notaIngreso.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
                                notaIngreso.CodSerie = Convert.ToInt32(txtcodserie.Text);
                                notaIngreso.Serie = txtSerie.Text;
                                notaIngreso.NumDoc = ser.Numeracion.ToString();
                                txtNumero.Text = nota.NumDoc;
                                notaIngreso.Comentario = txtComentario.Text;
                                notaIngreso.FechaIngreso = dtpFecha.Value.Date;
                                notaIngreso.CodUser = frmLogin.iCodUser;
                                notaIngreso.CodReferencia = Convert.ToInt32(nota.CodNotaSalida);
                                notaIngreso.Moneda = 2;
                                notaIngreso.Estado = 1;

                                // Insert Nota y Detalle - Ingreso
                                if (admNotaIngreso.insert(notaIngreso))
                                {
                                    RecorreDetalleIngreso();
                                    if (detalleingreso.Count > 0)
                                    {
                                        foreach (clsDetalleNotaIngreso det in detalleingreso)
                                        {
                                            if (det.Cantidad > 0)
                                                admNotaIngreso.insertdetalle(det);
                                        }
                                    }

                                    MessageBox.Show("Los datos se guardaron correctamente", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    txtNumDoc.Text = nota.CodNotaSalida.ToString().PadLeft(11, '0');
                                    sololectura(true);
                                }

                }
            }
        }

        private int recorre_ceros()
        {
            Int32 contador = 0;
            Int32 valor = 0;

            if (dgvDetalle.Rows.Count > 0)
            {
                //foreach (DataGridViewRow row in dgvDetalle.SelectedRows)
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    if (Convert.ToUInt32(row.Cells[cantidad.Name].Value) == 0) 
                    {
                        contador++;
                    }
                }
            }

            return contador;
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

        private void RecorreDetalleIngreso()
        {
            detalle.Clear();
            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    añadedetalleIngreso(row);
                }
            }
            //nota.Detalle = detalle;
        }

        private void RecorreDetalleGuia()
        {
            detalle.Clear();
            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    añadedetalleGuia(row);
                }
            }
            //nota.Detalle = detalle;
        }

        private void añadedetalle(DataGridViewRow fila)
        {
            clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
            deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            deta.CodNotaSalida = Convert.ToInt32(nota.CodNotaSalida);
            deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            deta.Cantidad = Convert.ToInt32(fila.Cells[cantidad.Name].Value);
            deta.CodUser = frmLogin.iCodUser;



            deta.ValoReal = Convert.ToDouble(fila.Cells[valorpromedio.Name].Value);
            deta.ValorRealSoles = Convert.ToDouble(fila.Cells[valorpromediosoles.Name].Value);
            deta.PrecioReal = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value);



            deta.CodAlmacen =frmLogin.iCodAlmacen;
            detalle.Add(deta);
        }

        private void añadedetalleIngreso(DataGridViewRow fila)
        {
            clsDetalleNotaIngreso deta = new clsDetalleNotaIngreso();
            deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            deta.CodNotaIngreso = Convert.ToInt32(notaIngreso.CodNotaIngreso);
            deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            deta.Cantidad = Convert.ToInt32(fila.Cells[cantidad.Name].Value);
            deta.CodUser = frmLogin.iCodUser;
            deta.ValoReal = Convert.ToDouble(fila.Cells[valorpromedio.Name].Value);
            deta.ValorrealSoles= Convert.ToDouble(fila.Cells[valorpromediosoles.Name].Value);
            deta.PrecioReal = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value);
            deta.CodAlmacen = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
            deta.CodDetalleRequerimiento = Convert.ToInt32(fila.Cells[coddetalle.Name].Value);
            deta.Bonificacion = false;
            detalleingreso.Add(deta);
        }

        private void añadedetalleGuia(DataGridViewRow fila)
        {
            clsDetalleGuiaRemision deta = new clsDetalleGuiaRemision();
            deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            deta.CodGuiaRemision = Convert.ToInt32(guia.CodGuiaRemision);
            deta.CodAlmacen = frmLogin.iCodAlmacen;
            deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            deta.SerieLote = "";
            deta.Cantidad = Convert.ToInt32(fila.Cells[cantidad.Name].Value);
            deta.CantidadPendiente = 0;
            deta.Peso = 0;
            deta.CodUser = frmLogin.iCodUser;
            //deta.ValoReal = Convert.ToDouble(fila.Cells[valorpromedio.Name].Value);
            //deta.ValorrealSoles = Convert.ToDouble(fila.Cells[valorpromediosoles.Name].Value);
            //deta.PrecioReal = Convert.ToDouble(fila.Cells[preciopromedio.Name].Value);
            detalleguia.Add(deta);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Proceso == 1)
            {
                dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
            }
            else if (Proceso == 2)
            {
                if (dgvDetalle.CurrentRow.Index != -1)
                {
                    DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Detalle Nota Salida", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (admnota.deletedetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[coddetalle.Name].Value)))
                        {
                            MessageBox.Show("Los datos han sido eliminado correctamente", "Detalle Nota Salida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargaDetalle();
                        }
                    }
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmDetalleGuia"] != null)
            {
                Application.OpenForms["frmDetalleGuia"].Activate();
            }
            else
            {
                frmDetalleGuia form = new frmDetalleGuia();
                form.Procede = 1;
                form.Proceso = 1;
                form.ShowDialog();
            }
        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
             try
            {
                ser = admSerie.MuestraSerie(Convert.ToInt32(txtcodserie.Text), frmLogin.iCodAlmacen);
                if (cmbTransferencia.SelectedIndex == 0) 
                {
                    CrystalDecisions.CrystalReports.Engine.ReportDocument rd = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    rd.Load("CRGuiaRemision.rpt");
                    CRGuiaRemision rpt = new CRGuiaRemision();
                    //rd.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(txtNumero.Text), frmLogin.iCodAlmacen, 15));
                    rd.SetDataSource(ds.GuiaRemisionTranferencia(Convert.ToInt32(txtNumero.Text), Convert.ToInt32(cmbAlmacen.SelectedValue), 15));
                    CrystalDecisions.CrystalReports.Engine.PrintOptions rptoption = rd.PrintOptions;
                    rptoption.PrinterName = ser.NombreImpresora;
                    rptoption.PaperSize = (CrystalDecisions.Shared.PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
                    rptoption.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(512, 850, 30, 500));
                    //CrystalDecisions.Shared.PageMargins margenes = rd.PrintOptions.PageMargins;
                    rd.PrintToPrinter(1, false, 1, 1);
                    rd.Close();
                    rd.Dispose();
                }
                else if (cmbTransferencia.SelectedIndex == 1)
                {
                    frmrptCotizacion frm = new frmrptCotizacion();
                    frm.CodCotizacion = Convert.ToInt32(txtNumDoc.Text);//codigo del requerimiento
                    frm.tipo = 16;
                    frm.ShowDialog(); 
                }
            }
             catch (Exception ex)
             {
                 MessageBox.Show("Se encontro el siguiente problema" + ex.Message, "Nota de Salida", MessageBoxButtons.OK, MessageBoxIcon.Information);

             }
        }

     
        private void txtRequerimiento_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1 && Convert.ToInt32(cmbAlmacen.SelectedValue) > 0)
            {
                frmConsolidadoTransferencia form = new frmConsolidadoTransferencia();
                //form.MdiParent = this.MdiParent;
                chkReqDet.Checked = true;
                txtRequerimiento.Text = "";
                form.proceso = 9;
                form.proce = Proceso;
                //form.Alm = Convert.ToInt32(cmbAlmacen.SelectedValue);

                form.Alm = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);
                form.Almaori = Convert.ToInt32(cmbAlmacen.SelectedValue);

                //form.Alm = Convert.ToInt32(cmbAlmacen.SelectedValue);
                //form.Almaori = Convert.ToInt32(cmbAlmacenDestino.SelectedValue);

                dgvDetalle.Rows.Clear();
                //if (Application.OpenForms["frmConsolidado"] != null)
                if (Application.OpenForms["frmConsolidadoTransferencia"] != null)
                {
                    Application.OpenForms["frmConsolidadoTransferencia"].Activate();
                }
                else
                {
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Seleccione el Almacen");
                cmbAlmacen.Focus();
            }
        }

        private void frmTranferenciaDirecta_Shown(object sender, EventArgs e)
        {
            txtDocRef.Focus();
        }

        private void customValidator1_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0 && e.ControlToValidate.Visible)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0 && e.ControlToValidate.Visible)
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
                if (Proceso != 0 && c.Visible)
                    if (c.SelectedIndex != -1)
                        e.IsValid = true;
                    else
                        e.IsValid = false;
                else
                    e.IsValid = true;
            else
                e.IsValid = true;
        }

        private void customValidator4_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (dgvDetalle.Rows.Count > 0)
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void dgvDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvDetalle.CurrentCell.ColumnIndex == cantidad.Index)//CantidadNueva
            {
                val.SOLONumeros(sender,e);
            }
        }

        private void dgvDetalle_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvDetalle.Columns[dgvDetalle.CurrentCell.ColumnIndex].Name == "cantidad" && txtedit.Text != "")
                //CantidadNueva
                {
                    if (Convert.ToDouble(txtedit.Text) >
                        Convert.ToDouble(dgvDetalle.CurrentRow.Cells[stockactual.Name].Value))
                    {
                        MessageBox.Show("Cantidad Debe Ser Menor o Igual al Stock");
                        guarda = 1;
                    }
                    else
                    {
                        guarda = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
        }

        private void dgvDetalle_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            txtedit = e.Control as TextBox;
            if (txtedit != null)
            {
                txtedit.KeyPress -= new KeyPressEventHandler(dgvDetalle_KeyPress);
                txtedit.KeyPress += new KeyPressEventHandler(dgvDetalle_KeyPress);
                txtedit.KeyUp -= new KeyEventHandler(dgvDetalle_KeyUp);
                txtedit.KeyUp += new KeyEventHandler(dgvDetalle_KeyUp);
            }
        }

        private void cmbTransferencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSerie.Text = "";
            txtNumero.Text = "";
            txtRequerimiento.Text = "";
            txtDocRef.Text = "";
            CodDocumento = 0;
            txtTransaccion.Text = "";

            if (cmbTransferencia.SelectedIndex == 0)
            {
                txtSerie.Visible = true;
                txtNumero.Visible = true;
                label3.Visible = true;
                label20.Visible = true;
                txtRequerimiento.Visible = true;
                txtTransaccion.Text = "TD";
                notaIngreso.CodTipoTransaccion = 15;
                comentario_usu.Visible = true;
                Proceso_txd = 0;
            }
            else 
            {
                if (cmbTransferencia.SelectedIndex == 1) 
                {
                    //txtSerie.Visible = false;
                    //txtNumero.Visible = false;
                    //label3.Visible = false;
                    label4.Visible = false;
                    label6.Visible = false;
                    label10.Visible = false;
                    label11.Visible = false;
                    cmbTransportista.Visible = false;
                    cmbVehiculo.Visible=false;
                    dtpEmision.Visible = false;
                    dtpTraslado.Visible = false;
                    label20.Visible = false;
                    txtRequerimiento.Visible = false;
                    txtTransaccion.Text = "TXD";
                    notaIngreso.CodTipoTransaccion = 16;
                    comentario_usu.Visible = false;
                    Proceso_txd = 1;
                }
            }
        }

        private void cmbAlmacenDestino_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbAlmacenDestino.SelectedValue) == frmLogin.iCodAlmacen)
            {
                MessageBox.Show("No se puede Hacer Transferencia en el mismo Almacen!");
                cmbAlmacenDestino.SelectedIndex = -1;
            }
        }

        private void btnAceptarTransferencia_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Esta seguro que desea Aceptar la Transferencia seleccionada", "Transferencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.No)
            {
                return;
            }
            else
            {
                if (admNotaIngreso.atender(Convert.ToInt32(notaIngreso.CodNotaIngreso), notaIngreso.CodSerie, notaIngreso.NumDoc, frmLogin.iCodUser))
                {
                    MessageBox.Show("La Transferencia ha sido Atendida correctamente", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            if (Application.OpenForms["frmTransferencias"] != null)
            {
                Application.OpenForms["frmTransferencias"].Close();
            }
           
                frmTransferencias form=new frmTransferencias();
                form.Show();
           
        }

        private void customValidator1_ValidateValue_1(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0 && e.ControlToValidate.Enabled)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator2_ValidateValue_1(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

    }
}
