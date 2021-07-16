using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.Editors;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.net.miasoftware.webservice;

namespace SIGEFA.Formularios
{
    public partial class frmNotaIngreso : DevComponents.DotNetBar.OfficeForm
    {




        clsDetalleNotaIngreso deta;
        clsDetalleFactura detafac ;



        private bool bguia;
        private bool bfactura;

        ServiceConsultaRUC  servicio = new ServiceConsultaRUC();
        String dato;
        SunatPersona sunat; //= new SunatPersona();





        //RAGA
        public clsCentroCosto centro = new clsCentroCosto();
        public Int32 codCentro;
  //      Int32 serielote1;

        public bool serie1 = false;

        public double stockactual;
        clsAdmProducto AdmPro = new clsAdmProducto();
        clsFactura fac = new clsFactura();
        clsAdmFactura AdmFact = new clsAdmFactura();
        clsAdmMoneda AdmMon = new clsAdmMoneda();
        clsAdmTransaccion AdmTran = new clsAdmTransaccion();
        public clsTransaccion tran = new clsTransaccion();
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
        clsAdmAutorizado AdmAut = new clsAdmAutorizado();
        clsAutorizado aut = new clsAutorizado();
        clsAdmFormaPago AdmPago = new clsAdmFormaPago();
        clsFormaPago fpago = new clsFormaPago();
        clsValidar ok = new clsValidar();
        clsDetalleNotaIngreso detaSelec = new clsDetalleNotaIngreso();
        clsAdmProducto Admpro = new clsAdmProducto();
       public clsProducto prod = new clsProducto();
        clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();
        clsOrdenCompra Orde = new clsOrdenCompra();
        clsAdmIngresoCamara AdmIngre = new clsAdmIngresoCamara();
        clsIngresoCamara ingreso = new clsIngresoCamara();
        clsAdmLoteProduccion AdmLote = new clsAdmLoteProduccion();
        clsLoteProduccion lote = new clsLoteProduccion();
        clsPesador pesa = new clsPesador();
        clsAdmPesador admpesa = new clsAdmPesador();
        private Decimal Qnueva = 0, QIngresado = 0, QPorAtender = 0;
        public List<Int32> codProd = new List<int>();
        public List<Int32> config = new List<Int32>();
        public List<clsDetalleNotaIngreso> detalle = new List<clsDetalleNotaIngreso>();
        public List<clsDetalleFactura> detalleFactura= new List<clsDetalleFactura>();
        public String CodNota;
        public Int32 CodTransaccion, codOrdenCompra_nota=0, CodAlmacenOrden=0;
        public Int32 CodProveedor;
        public Int32 CodCliente;
        public Int32 CodDocumento;
        public Int32 CodOrdenCompra;
        public Int32 CodIngresoCamara;
        public Int32 CodLote;
        public Int32 CodAutorizado;
        Boolean Validacion = true;
        public Int32 Proceso = 0; //(1) Nuevo (2) Editar (3) Consulta
        public Int32 Tipo;
        private Int32 proce = 0; //(1) Nota IngresoxCompra.
        TextBox txtedit = new TextBox();
        clsValidar val = new clsValidar();
        public DataTable data = new DataTable();
        clsAdmAnalisisOrganoleptico admana = new clsAdmAnalisisOrganoleptico();

        clsAdmIngresoCamara Admingre = new clsAdmIngresoCamara();


        public Int32 codFamilia;


        //RAGA
        public Int32 Procede2 = 0;
        clsDetalleIngresoCamara dIngresoCamara;
        double cantidadsum;
        public Int32 iniserie = 0;
        public Int32 finSerie = 0;
        public Int32 codProductoTerm;
        public Int32 nroBatch = 0;
        public Int32 codMatPrima;


        public frmNotaIngreso()
        {
            InitializeComponent();
            cargataggrilla();
         //   CargarImagenSunat();
        }

        private void cargataggrilla()
        {

                referencia.Tag = 34;
                descripcion.Tag = 35;
                unidad.Tag = 36;
                serielote.Tag = 37;
                cantidad.Tag = 38;
                preciounit.Tag = 40;
                dscto1.Tag = 41;
                dscto2.Tag = 41;
                dscto3.Tag = 41;
                montodscto.Tag = 42;
                valorventa.Tag = 43;
                igv.Tag = 44;
                precioventa.Tag = 45;
            

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

        private void CargaFormaPagos()
        {
            cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(0);
            cmbFormaPago.DisplayMember = "descripcion";
            cmbFormaPago.ValueMember = "codFormaPago";
            cmbFormaPago.SelectedIndex = -1;
            
        }

        private void CargaTransaccion() 
        {
            tran = AdmTran.MuestraTransaccion(CodTransaccion);
            tran.Configuracion = AdmTran.MuestraConfiguracion(tran.CodTransaccion);
            txtTransaccion.Text = tran.Sigla;
            lbNombreTransaccion.Text = tran.Descripcion;
            lbNombreTransaccion.Visible = true;
            if (tran != null)
            {
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
                foreach (Control t in groupBox2.Controls)
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
                foreach (Control t in groupBox3.Controls)
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



                foreach (DataGridViewColumn t in dgvDetalle.Columns)
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
                foreach (Control t in groupBox2.Controls)
                {
                    if (t.Tag != null)
                    {
                        t.Visible = false;
                    }
                }
                foreach (DataGridViewColumn t in dgvDetalle.Columns)
                {
                    if (t.Tag != null)
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

        private void CargaProveedor()
        {
            prov = AdmProv.MuestraProveedor(CodProveedor);            
            txtCodProv.Text = prov.Ruc;
            txtNombreProv.Text = prov.RazonSocial;
            txtCodProveedor.Text = prov.CodProveedor.ToString();




                
            /*
            if (prov.BSituacion == false && prov.BTipoPersoneria == true)
                 MessageBox.Show("RUC DADO DE BAJA");

            if (prov.BTipoPersoneria == false)
                    MessageBox.Show("PROVEEDOR CON DNI");
                 btnDetalle.Visible = true;
            */
        }

        private void BorrarProveedor()
        {
            prov = AdmProv.MuestraProveedor(CodProveedor);
            txtCodProv.Text = "";
            txtNombreProv.Text = "";
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
                foreach (Control t in groupBox2.Controls)
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
                foreach (DataGridViewColumn t in dgvDetalle.Columns)
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
                foreach (Control t in groupBox2.Controls)
                {
                    if (t.Tag != null)
                    {
                        t.Visible = false;
                    }
                }
                foreach (DataGridViewColumn t in dgvDetalle.Columns)
                {
                    if (t.Tag != null)
                    {
                        t.Visible = false;
                    }
                }
                return false;
            }
        }

        private Boolean BuscaProveedor()
        {
            prov = AdmProv.BuscaProveedor(txtCodProv.Text);
            if (prov != null)
            {
                txtNombreProv.Text = prov.RazonSocial;
                CodProveedor = prov.CodProveedor;
                return true;
            }
            else
            {
                txtNombreProv.Text = "";
                CodProveedor = 0;
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

        private void bloqueaBotones2()
        {
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label19.Visible = false;
            label14.Visible = false;
            label13.Visible = false;
            label12.Visible = false;
            label11.Visible = false;
            label10.Visible = false;
            cmbFormaPago.Visible = false;
            cmbMoneda.Visible = false;
            txtDscto.Visible = false;
            txtTipoCambio.Visible = false;
            cbValorVenta.Visible = false;
            txtValorVenta.Visible = false;
            txtPrecioVenta.Visible = false;
            txtBruto.Visible = false;
            txtFlete.Visible = false;
            txtIGV.Visible = false;
            dtpFechaPago.Visible = false;
            // txtCodProv.Enabled = false;
            txtOrdenCompra.Focus();
            dgvDetalle.Visible = false;
            dgvDetalle2.Visible = true;
            btnNuevo.Enabled = false;
            btnEditar.Enabled = false;
            //btnDetalle.Visible = false;
            btnGuardar.Enabled = true;
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


                /*
                if (tran.CodTransaccion == 14)//ingreso por orden de compra
                {
                    bloqueaBotones2();
                    proce = 1;
                    txtDocRef.Focus();
                    btnDetalle.Visible = true;
                }
                if (tran.CodTransaccion == 1)//ingreso por compra directa
                {
                    proce = 2;                    
                    btnDetalle.Visible = true;
                }
                if (tran.CodTransaccion == 22)//ingreso de materia prima
                {                    
                    btnDetalle.Visible = true;
                }
                */




            }


        }



        public void irDetalleIngreso()
        {
            if (Application.OpenForms["frmDetalleIngreso"] != null)
            {
                Application.OpenForms["frmDetalleIngreso"].Activate();
            }
            else
            {
                frmDetalleIngreso form = new frmDetalleIngreso();
                form.Procede = 11;


                  if (dgvDetalle.Rows.Count == 0)
                    form.Proceso = 1;
                  
                  else if (dgvDetalle.Rows.Count >= 0)
                      form.Proceso = 2;

                form.ShowDialog();
            }
        }



        private void btnDetalle_Click(object sender, EventArgs e)
        {
            if (CodTransaccion == 22 && CodProveedor != 0)//MATERIA PRIMA
            {
                if(txtIngreso.Text != "")
                    editaCantDev();//Procede = 10
                else
                    irDetalleIngreso();//Procede=11
            }
            else if (CodTransaccion == 24  || CodTransaccion == 27)//PRODUCTO TERMINADO  ó REPROCESO
            {
                detalleclickingprodterm();
            }
            else  if (CodTransaccion == 1)//INGRESO POR COMPRA
            {
                compraInsumosSuministros();//Procede=6
             // detalleclick();
            }
           
        }




        public void compraInsumosSuministros()
        {
            if (txtCodProveedor.Text != "")
            {
                codProd.Clear();
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
                if (Application.OpenForms["frmDetalleIngreso"] != null)
                {
                    Application.OpenForms["frmDetalleIngreso"].Activate();
                }
                else
                {
                    frmDetalleIngreso form = new frmDetalleIngreso();
                    //form.MdiParent = this;
                    form.Procede = 6;
                    form.Proceso = 1;
                    form.codproveedor = Convert.ToInt32(txtCodProveedor.Text);
                    form.bvalorventa = cbValorVenta.Checked;
                    form.productoscargados = detalle;
                    form.ShowDialog();
                    serielote.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("Ingrese Proveedor");
                txtCodProv.Focus();
            }

        }




        public void detalleclick()
        {
            RecorreDetalle();
            if (  CodTransaccion == 22 ||    CodTransaccion == 24)
            {


                if (txtCodProveedor.Text != "")
                {
                    if (Application.OpenForms["frmDetalleGuia"] != null)
                    {
                        Application.OpenForms["frmDetalleGuia"].Activate();
                    }
                    else
                    {
                        frmDetalleGuia form = new frmDetalleGuia();
                        form.Procede = 10;
                        //RAGA
                        if (CodTransaccion == 22)
                        {
                            form.Procede = 16;
                        }
                        //RAGA
                        if (CodTransaccion == 24)
                        {
                            form.Procede = 13;
                        }

                        form.Proceso = 1;
                        form.codproveedor = Convert.ToInt32(txtCodProveedor.Text);
                        form.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Proveedor");
                    txtCodProv.Focus();
                }

            }
            else
            {
                if (txtCodProveedor.Text != "")
                {
                    codProd.Clear();
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
                    if (Application.OpenForms["frmDetalleIngreso"] != null)
                    {
                        Application.OpenForms["frmDetalleIngreso"].Activate();
                    }
                    else
                    {
                        frmDetalleIngreso form = new frmDetalleIngreso();
                        //form.MdiParent = this;
                        form.Procede = 6;
                        form.Proceso = 1;
                        form.codproveedor = Convert.ToInt32(txtCodProveedor.Text);
                        form.bvalorventa = cbValorVenta.Checked;
                        form.productoscargados = detalle;
                        form.ShowDialog();
                        serielote.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese Proveedor");
                    txtCodProv.Focus();
                }
            }
        }
        public void detalleclickingprodterm()
        {
         //   RecorreDetalle();
            if (Application.OpenForms["frmDetalleGuia"] != null)
            {
                Application.OpenForms["frmDetalleGuia"].Activate();
            }
            else
            {
                frmDetalleGuia form = new frmDetalleGuia();
                form.Procede = 14;
                form.Proceso = 1;

                //RAGA 2do Batch


                if (codProductoTerm != 0)
                {
                    form.txtCodigo.Text = codProductoTerm.ToString();
                    form.textBox6.Text = (finSerie + 1).ToString();
                    form.textBox1.Text = (finSerie + 1).ToString();
                    form.txtReferencia.Enabled = false;
                    form.txtStock.Text = String.Format("{0:#,##0.0000}", stockactual);


                    if (codFamilia == 7)
                    {
                        gradosbrix.Visible = false;
                    }
                    else if (codFamilia == 8)
                    {
                        gradosbrix.Visible = true;
                    }


                    if (nroBatch > 0)
                        form.textBox4.Text = (nroBatch + 1).ToString();
                }

                form.ShowDialog();

                clsProducto prod1 =  Admpro.CargaProducto(codProductoTerm, frmLogin.iCodAlmacen);

              if (prod1 != null)
              {
                  if (lote.CodMateriaPrima != prod1.ICodProductoReferencia)
                  {
                //      MessageBox.Show("LA MATERIA PRIMA DEL LOTE NO CORRESPONDE AL DEL PRODUCTO SELECCIONADO");
                      
                  }
              }

            }



        }


        private void VerificarCabecera()
        {
            Validacion = true;
            if (CodTransaccion == 0)
            {
                Validacion = false;
            }
            if (txtCodProv.Visible && CodProveedor == 0)
            {
                Validacion = false;
            }
            if (txtOrdenCompra.Visible && CodOrdenCompra == 0)
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
                form.Procede = 6;
                form.Proceso = 1;
                form.bvalorventa = cbValorVenta.Checked;
                form.productoscargados = detalle;
                form.ShowDialog();
            }
        }

        private void txtCodProv_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtCodProv.Text != "")
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



       




        
        private void frmNotaIngreso_Load(object sender, EventArgs e)
        {            
            cargaMoneda();
            CargaFormaPagos();
            CargarPesador();
          
            tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2); 
            if (Proceso == 1)
            {
                txtIngreso.Focus();
                btnDetalle.Visible = false;
                btnNuevo.Visible = false;
              //  btnEditar.Visible = false;
            //    btnEliminar.Visible = false;
                Bloqueabotones();
                txtOrdenCompra.Visible = false;
            }
            if (Proceso == 2)
            {
                CargaNotaIngreso();
              //  CargarIngresoCamaraNI();
            }
            else if (Proceso == 3)
            {
                CargaNotaIngreso();
             //   CargarIngresoCamaraNI();
                sololectura(true);
                button5.Visible = true;
                
            }
            else if (Proceso == 4)
            {
                CargaNotaIngreso();
                sololectura(true);
            }
            else if (Proceso == 5)
            {
                CargaNotaIngreso();
                sololectura(true);
            }
            //RAGA
            else if (Proceso == 6)
            {
                txtIngreso.Focus();
                btnDetalle.Visible = false;
                btnNuevo.Visible = false;
                btnEditar.Visible = false;
                btnEliminar.Visible = false;
             //   textBox1.Visible = true;
                Bloqueabotones();
            }

            CargaPorTransaccion();
          
           
        }



        public void CargaPorTransaccion()
        {

         //   CodTransaccion = tran.CodTransaccion;

            if (CodTransaccion == 1)//ingreso por compra directa   : INSUMOS y SUMINISTROS  
            {

                txtDocRef.Focus();
                btnDetalle.Visible = true;
                proce = 1; //dgvDetalle
                txtLoteProduccion.Text = "0";
            }

            if (CodTransaccion == 14)//ingreso por orden de compra
            {
                bloqueaBotones2();
                txtDocRef.Focus();
                btnDetalle.Visible = true;
                proce = 2;//dgvDetalle2
            }
            if (CodTransaccion == 22)//Ingreso MP
            {
                txtLoteProduccion.Text = "0";
                txtDocRef.Focus();
                btnDetalle.Visible = true;
                label30.Visible = true;
                //textBox1.Visible = true;
                proce = 1;//dgvDetalle

                CantDev.Visible = false;
                CantEntregada.Visible = false;

            }

            //RAGA
            if (CodTransaccion == 24 || CodTransaccion == 27)//Ingreso  PT
            {
                gradosbrix.Visible = true;
                nrobatch1.Visible = true;
                pneto.Visible = true;

                //   txtNDocRef.ReadOnly = true;
                btnEliminar.Visible = true;
                btnEditar.Visible = true;
                button1.Visible = false;
                proce = 1;//dgvDetalle
                btnDetalle.Visible = true;

                btnEditar.Visible = false;
                txtCodProv.Text = "0";
                label25.Visible = false;
                button1.Visible = false;
                cboPesador.Visible = false;

            }


        }






        private void CargarImagenSunat()
        {
            if (button2.Visible == true)
            {

                try
                {
                    if (sunat == null)
                        sunat = new SunatPersona();
                    pbcaptcha.Image = sunat.GetCapcha;
                    txtcaptcha.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }




       
        private void cargaMoneda()
        {
            cmbMoneda.DataSource = AdmMon.ListaMonedas();
            cmbMoneda.DisplayMember = "descripcion";
            cmbMoneda.ValueMember = "codMoneda";
            cmbMoneda.SelectedIndex =0;

        }
        private void sololectura(Boolean estado)
        {            
            txtTransaccion.ReadOnly = estado;
            dtpFecha.Enabled = !estado;
            txtCodProv.ReadOnly = estado;
            cmbMoneda.Enabled = !estado;
            cmbFormaPago.Enabled = !estado;
            txtDocRef.ReadOnly = estado;
            txtNDocRef.ReadOnly = estado;
            txtOrdenCompra.ReadOnly = estado;
            txtComentario.ReadOnly = estado;
            txtAutorizacion.ReadOnly = estado;
            txtBruto.ReadOnly = estado;
            txtDscto.ReadOnly = estado;
            txtValorVenta.ReadOnly = estado;
            txtIGV.ReadOnly = estado;
            txtPrecioVenta.ReadOnly = estado;
            txtFlete.ReadOnly = estado;
            btnNuevo.Visible = !estado;
            btnEditar.Visible = !estado;
            btnEliminar.Visible = !estado;
            btnGuardar.Visible = !estado;
            dtpHoraInicio.Enabled  = !estado;
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
    

               
                if (nota != null)
                {
                    txtNumDoc.Text = nota.CodNotaIngreso;
                    CodTransaccion = nota.CodTipoTransaccion;
                    CargaTransaccion();
                    if (txtCodProv.Enabled)
                    {
                        CodProveedor = nota.CodProveedor;
                       
                        txtCodProv.Text = nota.RUCProveedor;                        
                        txtNombreProv.Text = nota.RazonSocialProveedor;
                        BuscaProveedor();
                    }
                    dtpFecha.Value = nota.FechaIngreso;
                    //cmbMoneda.SelectedIndex = nota.Moneda;
                    txtTipoCambio.Text = nota.TipoCambio.ToString();
                    txtTipoCambio.Visible = true;
                    label16.Visible = true;
            //        txtIngreso.Text = ingreso.NumeroOrden.ToString();


                    
                    if (txtAutorizacion != null) 
                    { 
                        if (txtAutorizacion.Enabled)
                        {
                            //se guarda el codigo del autorizado y se cargan los datos de este
                        }
                    }
                    CodDocumento = nota.CodTipoDocumento;
                    txtDocRef.Text = nota.SiglaDocumento;
                    txtNDocRef.Text = nota.NumDoc;
                    BuscaTipoDocumento();
                  
                    if (txtDocRef.Enabled)
                    {
                        CodDocumento = nota.CodTipoDocumento;
                        txtDocRef.Text = nota.SiglaDocumento;
                        txtNDocRef.Text = nota.NumDoc;
                        BuscaTipoDocumento();
                        validaDocumento();

                        //doc = Admdoc.BuscaTipoDocumento(txtDocRef.Text);
                        //if (doc != null)
                        //{
                        //    CodDocumento = doc.CodTipoDocumento;
                        //}
                    }

                    if (txtOrdenCompra.Enabled)
                    {
                        //se carga el codigo de la orden de compra
                        //txtOrdenCompra.Text = 
                    }

                    cmbFormaPago.SelectedValue = nota.FormaPago;
                    dtpFechaPago.Value = nota.FechaPago;
                    txtComentario.Text = nota.Comentario;
                    txtBruto.Text = String.Format("{0:#,##0.0000}", nota.MontoBruto);
                    txtDscto.Text = String.Format("{0:#,##0.0000}", nota.MontoDscto);
                    txtFlete.Text = String.Format("{0:#,##0.0000}", nota.Flete);
                    txtValorVenta.Text = String.Format("{0:#,##0.0000}", nota.Total - nota.Igv);
                    txtIGV.Text = String.Format("{0:#,##0.0000}", nota.Igv);
                    txtPrecioVenta.Text = String.Format("{0:#,##0.0000}", nota.Total);
                    txtCantidad.Text = String.Format("{0:#,##0.0000}", cantidadsum);
                    dtpHoraInicio.Value = nota.HoraInicio;
                    txtLoteProduccion.Text = nota.CodLoteProduccion.ToString();
                    CodLote = nota.CodLoteProduccion;
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
            tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date,2);
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

        private void txtCodProv_KeyDown(object sender, KeyEventArgs e)
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
                    form.Procede = 4;

                    txtDocRef.Text = "";

                   


                    form.ShowDialog();
                    if (CodProveedor != 0) { CargaProveedor(); ProcessTabKey(true); } else { BorrarProveedor(); }
                }
            }
        }

        private void frmNotaIngreso_Shown(object sender, EventArgs e)
        {
            if ( (Proceso == 1 || Proceso == 3) /*&& txtTransaccion.Text == "FT"*/)
            {
                if (tc == null)
                {
                    MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    txtTipoCambio.Text = tc.Venta.ToString();
                    txtTipoCambio.Visible = true;
                    label16.Visible = true;
                    txtTipoCambio.ReadOnly = false;
                   // txtOrdenCompra.Visible = false;
                }

                CargaPorTransaccion();



            }
            if (txtTransaccion.Text == "FT")
            {
                cmbFormaPago.Visible = true;
                label17.Visible = true;
                dtpFechaPago.Visible = true;
            }
        }

        private void txtDocRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmDocumentos"] != null)
                {
                    Application.OpenForms["frmDocumentos"].Close();
                }
                else
                {
                    frmDocumentos form = new frmDocumentos();
                    form.Proceso = 3;
                    form.Procedencia = 1;
                    form.Transaccion = txtTransaccion.Text;
                    form.ShowDialog();
                    doc = form.doc;
                    CodDocumento = doc.CodTipoDocumento;
                    txtDocRef.Text = doc.Sigla;
                    if (CodDocumento != 0) { ProcessTabKey(true); } else { txtDocRef.Text = ""; }
                    validaDocumento();


                }
            }             
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
                        MessageBox.Show("Codigo de Documento no existe, Presione F1 para consultar la tabla de ayuda", "NOTA DE INGRESO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }




        public void validaDocumento()
        {
            //Factura de Compra
            if (CodDocumento == 2 )
            {

                

                CargarImagenSunat();
                if (txtCodProv.Text.Length != 11)//DNI
                {
                    MessageBox.Show("EL PROVEEDOR NO CUENTA CON RUC PARA EMITIR UNA FACTURA");
                    txtDocRef.Text = "";
                }
                //Valida si el RUC esta Activo y Habido
                else if (txtCodProv.Text.Length == 11)//RUC
                {

                    if (sunat.Existeconexion == true)
                    {
                        sunat.GetInfo(txtCodProv.Text, sunat.UseTesseract());
                        if (txtCodProv.Text == sunat.Ruc)
                            MessageBox.Show("El RUC: " + sunat.Ruc + " Cuenta con Estado de Contribuyente :  " + sunat.EstadoContribuyente + "  y Condición :" + sunat.CondicionContribuyente);

                        if (sunat.EstadoContribuyente != "ACTIVO")
                        {
                            CodProveedor = 0;
                            txtCodProv.Text = "";
                            txtNombreProv.Text = "";
                        }
                        
                        /*
                        if (string.IsNullOrEmpty(sunat.Error))
                        {
                            if (txtCodProv.Text == sunat.Ruc)
                                MessageBox.Show("Estado de Contribuyente :" + sunat.EstadoContribuyente +  "Condición :" + sunat.CondicionContribuyente);

                        }
                        */

                    }
                    else
                        MessageBox.Show("No existe coneccion a internet para Consultar SUNAT en Linea");
                }

                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                txtValorVenta.Visible = true;
                txtIGV.Visible = true;
                txtPrecioVenta.Visible = true;




            }
            //Liquidación de Compra
            else if (CodDocumento == 19)
            {
                CargarImagenSunat();

                if (txtCodProv.Text.Length == 8)//DNI
                {
                    //Permite LC
                    if (sunat.Existeconexion == true)
                    {
                        sunat.GetInfoDNI(txtCodProv.Text, sunat.UseTesseract());
                        //    if (string.IsNullOrEmpty(sunat.Error))
                        //      {
                        if (sunat.Ruc != null)
                            MessageBox.Show("El Proveedor cuenta con RUC :" + sunat.Ruc + " y su RUC esta " + sunat.EstadoContribuyente);
                        else
                            MessageBox.Show("El proveedor no Cuenta con RUC");

                        //     }

                    }
                    else
                        MessageBox.Show("No existe coneccion a internet para Consultar SUNAT en Linea");

                }
                else if (txtCodProv.Text.Length == 11)//RUC
                {

                    if (sunat.Existeconexion == true)
                    {

                        sunat.GetInfo(txtCodProv.Text, sunat.UseTesseract());
                        if (txtCodProv.Text == sunat.Ruc)
                        {
                            MessageBox.Show("El RUC: " + sunat.Ruc + " Cuenta con Estado de Contribuyente :  " + sunat.EstadoContribuyente + "  y Condición :" + sunat.CondicionContribuyente);


                        }


                        /*
                        if (string.IsNullOrEmpty(sunat.Error))
                        {
                            if (txtCodProv.Text == sunat.Ruc)
                                MessageBox.Show("Estado de Contribuyente :" + sunat.EstadoContribuyente +  "Condición :" + sunat.CondicionContribuyente);

                        }
                        */



                    }
                    else
                        MessageBox.Show("No existe coneccion a internet para Consultar SUNAT en Linea");
                }


                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                txtValorVenta.Visible = true;
                txtIGV.Visible = true;
                txtPrecioVenta.Visible = true;


            }
            else
            {
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                txtValorVenta.Visible = false;
                txtIGV.Visible = false;
                txtPrecioVenta.Visible = false;


            }


        }




        private void txtDocRef_Leave(object sender, EventArgs e)
        {
            BuscaTipoDocumento();
            VerificarCabecera();
            if (Validacion)
            {
                btnDetalle.Enabled = true;
            }

       
        }

        private void dgvDetalle_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (Proceso == 1)
            {
                calculatotales();
            }
        }

        private void calculatotales()
        {

            Double bruto = 0;
            Double descuen = 0;
            Double valor = 0;
            Double igvt = 0;
            Double preciot = 0;
            Decimal cantidadt = 0;
            Decimal pnetop = 0;

            if (proce == 1)
            {


                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    bruto = bruto + Convert.ToDouble(row.Cells[importe.Name].Value);
                    descuen = descuen + Convert.ToDouble(row.Cells[montodscto.Name].Value);
                    valor = valor + Convert.ToDouble(row.Cells[valorventa.Name].Value);
                    igvt = igvt + Convert.ToDouble(row.Cells[igv.Name].Value);
                    preciot = preciot + Convert.ToDouble(row.Cells[precioventa.Name].Value);
                    cantidadt = cantidadt + Convert.ToDecimal(row.Cells[cantidad.Name].Value);
                    if (row.Cells[pneto.Name].Value != null)
                    {
                        pnetop = pnetop + Convert.ToDecimal(row.Cells[pneto.Name].Value);
                    }
                    
                }
                txtBruto.Text = String.Format("{0:#,##0.0000}", bruto);
                txtDscto.Text = String.Format("{0:#,##0.0000}", descuen);
                txtValorVenta.Text = String.Format("{0:#,##0.0000}", valor);
                txtIGV.Text = String.Format("{0:#,##0.0000}", igvt);
                txtPrecioVenta.Text = String.Format("{0:#,##0.0000}", preciot);
                txtCantidad.Text = String.Format("{0:#,##0.00}", cantidadt);
                textBox1.Text = String.Format("{0:#,##0.00}", pnetop);

            }
            else if (proce == 2)
            {
               
         
                foreach (DataGridViewRow row in dgvDetalle2.Rows)
                {
                    bruto = bruto + Convert.ToDouble(row.Cells[importe1.Name].Value);
                    descuen = descuen + Convert.ToDouble(row.Cells[montodscto1.Name].Value);
                    valor = valor + Convert.ToDouble(row.Cells[subtotal.Name].Value);
                    igvt = igvt + Convert.ToDouble(row.Cells[igv1.Name].Value);
                    preciot = preciot + Convert.ToDouble(row.Cells[valorventa1.Name].Value);
                    cantidadt = cantidadt + Convert.ToDecimal(row.Cells[cant.Name].Value);
                    if (row.Cells[pneto.Name].Value != null)
                    {
                        pnetop = pnetop + Convert.ToDecimal(row.Cells[pneto.Name].Value);
                    }



                }
                txtBruto.Text = String.Format("{0:#,##0.0000}", bruto);
                txtDscto.Text = String.Format("{0:#,##0.0000}", descuen);
                txtValorVenta.Text = String.Format("{0:#,##0.0000}", valor);
                txtIGV.Text = String.Format("{0:#,##0.0000}", igvt);
                txtPrecioVenta.Text = String.Format("{0:#,##0.0000}", preciot);
                txtCantidad.Text = String.Format("{0:#,##0.00}", cantidadt);
                textBox1.Text = String.Format("{0:#,##0.00}", pnetop);
            }
            
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

        private void txtCodProv_Leave(object sender, EventArgs e)
        {
            if (CodProveedor == 0)
            {
             //   txtCodProv.Focus();
            }
            else
            {
                VerificarCabecera();
                if (Validacion)
                {
                    btnDetalle.Enabled = true;
                }
            }
        }

        private void txtNDocRef_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void txtNDocRef_Leave(object sender, EventArgs e)
        {
            if (txtNDocRef.Text == "")
            {
                
            }
            else
            {
                VerificarCabecera();
                if (Validacion)
                {
                    btnDetalle.Enabled = true;
                }
            }
        }

        private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void txtOrdenCompra_Leave(object sender, EventArgs e)
        {
            VerificarCabecera();
            if (Validacion && Proceso == 1)
            {
                btnDetalle.Enabled = true;
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


            CargaPorTransaccion();

        }

        private void txtPrecioVenta_TextChanged(object sender, EventArgs e)
        {
            if (txtPrecioVenta.Text != "")
            {
                btnGuardar.Enabled = true;
            }
        }

        private Int32 verificarCamposVacios()
        {
            if (proce == 1)
            {
                Int32 valor = 0;
                foreach (DataGridViewRow row in dgvDetalle2.Rows)
                {
                    String canti = "";
                    String cantin = "";
                    

                    canti = Convert.ToString(Convert.ToInt32(row.Cells[cant.Name].Value));
                    cantin = Convert.ToString(Convert.ToInt32(row.Cells[cantn.Name].Value));
                    if (Convert.ToInt32(row.Cells[codetord.Name].Value)!=0)
                    {
                        if (canti == "" || cantin == "" || cantin == "0")
                        {
                            valor = 1;
                        }
                    }
                    
                   

                }
                return valor;
            }
            else
            {
                Int32 valor = 1;
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    String cant = "";
                    String precio = "";
                    String impor = "";
                    String IG = "";
                    String MontDes = "";
                    String d1 = "";
                    String d2 = "";
                    String d3 = "";

                    cant = Convert.ToString(Convert.ToInt32(row.Cells[cantidad.Name].Value));
                    impor = Convert.ToString(row.Cells[importe.Name]);
                    IG = Convert.ToString(row.Cells[igv.Name]);
                    MontDes = Convert.ToString(row.Cells[montodscto.Name]);
                    precio = Convert.ToString(row.Cells[preciounit.Name].Value);
                    d1 = Convert.ToString(row.Cells[dscto1.Name].Value);
                    d2 = Convert.ToString(row.Cells[dscto2.Name].Value);
                    d3 = Convert.ToString(row.Cells[dscto3.Name].Value);

                    if (d1 != "" || d2 != "" || d3 != "")
                    {
                        calculatotales();
                    }

                    if (cant == "" || precio == "" || impor == "" || IG == "" || cant == "0")
                    {
                        valor = 1;
                    }
                    else
                    {
                        valor = 0;
                    }

                }
                return valor;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (superValidator1.Validate() == true && dgvDetalle.Rows.Count > 0)
            {

                if (verificarCamposVacios() == 1 && Convert.ToDecimal(txtPrecioVenta.Text) > 0)
                {
                    MessageBox.Show("Debe completar Detalle de Nota, Datos Vacios;Cantidades y/o Precios");
                }
                else
                {
                    if (proce == 1)//SIN ORDEN DE COMPRA
                    {
                        
                            nota.ICentroCostos = codCentro;
                            nota.CodAlmacen = frmLogin.iCodAlmacen;
                            nota.CodTipoTransaccion = tran.CodTransaccion;
                            nota.CodProveedor = prov.CodProveedor;
                            nota.CodTipoDocumento = doc.CodTipoDocumento;
                            nota.NumDoc = txtNDocRef.Text;
                            nota.FechaIngreso = dtpFecha.Value.Date;
                            nota.Comentario = txtComentario.Text;
                            nota.CodUser = frmLogin.iCodUser;
                            nota.CodOrdenCompra = codOrdenCompra_nota;
                            nota.codalmacenemisor = 0;
                            if (txtFlete.Text == "")
                                nota.Flete = 0;
                            else
                                nota.Flete = Convert.ToDouble(txtFlete.Text);

                            nota.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                            if (txtTipoCambio.Text != "")
                                nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
                            else nota.TipoCambio = 0;
                            if (cmbFormaPago.SelectedValue == null)
                                nota.FormaPago = 0;
                            else
                                nota.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                            nota.FechaPago = dtpFecha.Value.Date;


                           
                            if (txtFlete.Text != "")
                                nota.Flete = Convert.ToDouble(txtFlete.Text);
                            else nota.Flete = 0;
                           
                            nota.CodUser = frmLogin.iCodUser;
                            nota.Estado = 1;
                            // Para saber si la nota esta activa o anulada. El estado se podra cambiar en una ventana especifica para anular notas
                            if (txtLoteProduccion.Text != "")
                                nota.CodLoteProduccion = Convert.ToInt32(txtLoteProduccion.Text);
                            else
                                nota.CodLoteProduccion = 0;
                            nota.CodPesador = Convert.ToInt32(cboPesador.SelectedValue);
                            if (CodIngresoCamara != 0)
                                nota.CodIngresoCamara = CodIngresoCamara;
                            nota.HoraInicio = dtpHoraInicio.Value;

                            if (txtDscto.Text != "")
                                nota.MontoDscto = Convert.ToDouble(txtDscto.Text);
                            else nota.MontoDscto = 0;
                            nota.MontoBruto = Convert.ToDouble(txtBruto.Text);
                            nota.Igv = Convert.ToDouble(txtIGV.Text);
                            nota.Total = Convert.ToDouble(txtPrecioVenta.Text);



                            if (Proceso == 1)
                            {
                                //Ingresar Guia
                                if (AdmNota.insert(nota))
                                {

                                    if (CodTransaccion == 22)
                                    {
                                        recorrerdetalle();
                                        if (detalle.Count > 0)
                                        {
                                            foreach (clsDetalleNotaIngreso det in detalle)
                                            {
                                                if (AdmNota.insertdetalle(det))
                                                {

                                                    dIngresoCamara = new clsDetalleIngresoCamara();
                                                    dIngresoCamara.ICodDetalleIngresoCamara = det.CodDetalleIngreso;
                                                    dIngresoCamara.ICodIngresoCamara = CodIngresoCamara;
                                                    dIngresoCamara.ICodProductor = det.CodProducto;
                                                    AdmIngre.UpdateDetalleIngContenedor(dIngresoCamara);

                                                }
                                            }
                                        }
                                        MessageBox.Show("Se Guardaron los Datos Correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        button5.Visible = true;
                                        btnGuardar.Enabled = false;
                                        txtNDocRef.Text = nota.CodNotaIngreso;
                                        CodNota = nota.CodNotaIngreso;
                                    }

                                    else if (CodTransaccion == 24 || tran.CodTransaccion == 27)
                                    {

                                        recorrerdetalle();
                                        if (detalle.Count > 0)
                                        {
                                            foreach (clsDetalleNotaIngreso det in detalle)
                                            {
                                                //  if (AdmNota.insertdetalle(det))
                                                AdmNota.insertdetalle(det);
                                            }
                                        }
                                        MessageBox.Show("Se Guardaron los Datos Correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        button5.Visible = true;
                                        btnGuardar.Enabled = false;
                                        txtNDocRef.Text = nota.CodNotaIngreso;
                                        if (CodTransaccion == 24)
                                            AdmPro.ActualizaLoteProducto(8, CodLote, codProductoTerm);



                                    }
                                    else if (CodTransaccion == 1)
                                    {
                                        recorrerdetalle();
                                        if (detalle.Count > 0)
                                        {
                                            foreach (clsDetalleNotaIngreso det in detalle)
                                            {
                                                AdmNota.insertdetalle(det);
                                            }
                                        }
                                        MessageBox.Show("Se Guardaron los Datos Correctamente", "Nota de Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        button5.Visible = true;
                                        btnGuardar.Enabled = false;
                                        txtNDocRef.Text = nota.CodNotaIngreso;

                                    }

                                }

                            }
                            if (Proceso == 2)//Actualizar
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
                                    MessageBox.Show("Los datos se actualizaron correctamente", "Nota de Ingreso",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }

                            }
                            


                            //Incluye  Factura o Liquidación de Compra
                            if (CodDocumento == 2 || CodDocumento == 19)
                            {

                                fac.CodAlmacen = frmLogin.iCodAlmacen;
                                fac.CodTipoTransaccion = tran.CodTransaccion;
                                fac.CodProveedor = prov.CodProveedor;
                                fac.CodTipoDocumento = CodDocumento;
                                fac.DocumentoFactura = txtNDocRef.Text;
                                fac.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                                if (txtTipoCambio.Visible)
                                {
                                    fac.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
                                    nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
                                }
                                fac.FechaIngreso = dtpFecha.Value.Date;
                                fac.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                                fac.FechaPago = dtpFechaPago.Value.Date;
                                if (fpago.Dias == 0)
                                {
                                    nota.FechaCancelado = dtpFecha.Value.Date;
                                    fac.FechaCancelado = dtpFecha.Value.Date;
                                }
                                fac.Cancelado = 0;// Para saber si la nota esta pendiente de pago o esta cancelada 
                                fac.Comentario = txtComentario.Text;
                             

                                if (txtFlete.Text != "")
                                {
                                    nota.Flete = Convert.ToDouble(txtFlete.Text);
                                    fac.Flete = Convert.ToDouble(txtFlete.Text);
                                }

                                fac.MontoBruto = Convert.ToDouble(txtBruto.Text);
                                fac.MontoDscto = Convert.ToDouble(txtDscto.Text);
                                fac.Igv = Convert.ToDouble(txtIGV.Text);
                                fac.Total = Convert.ToDouble(txtPrecioVenta.Text);


                            //    fac.CodReferencia = Int32.Parse(CodNota);
                                fac.CodUser = frmLogin.iCodUser;
                                fac.Estado = 1;


                                if (Proceso == 1)
                                {

                                    if (AdmFact.insert(fac))
                                    {
                                        recorrerdetallefactura();

                                        if (detalleFactura.Count > 0)
                                        {
                                            foreach (clsDetalleFactura det in detalleFactura)
                                            {
                                                AdmFact.insertdetalle(det);
                                            }
                                        }


                                        if (CodDocumento == 2)
                                            MessageBox.Show("Se elaboró la Factura de Compra ", "Facturación",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);


                                        if (CodDocumento == 19)
                                            MessageBox.Show("Se elaboró la Liquidación de Compra ", "Facturación",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        if (fpago.Dias == 0 && nota.CodTipoTransaccion == 1)  //se comprueba que el pago sea al contado y que la trnasaccion sea ingreso por compra
                                            ingresarpago();
                                       
                                        //Actualiza ReferenciaNotaIngreso

                             //           AdmNota.actualizarNotaIngresoReferencia(Int32.Parse(CodNota), fac.CodFactura);
                                       

                                    }

                                }
                                else if (Proceso == 2)
                                {
                                    if (AdmFact.update(fac))
                                    {
                                        recorrerdetallefactura();

                                        if (detalleFactura.Count > 0)
                                        {
                                            foreach (clsDetalleFactura det in detalleFactura)
                                            {
                                                AdmFact.updatedetalle(det);
                                            }
                                        }


                                        if (CodDocumento == 2)
                                            MessageBox.Show("Se actualizo la Factura de Compra ", "Facturación",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);


                                        if (CodDocumento == 19)
                                            MessageBox.Show("Se actualizo la Liquidación de Compra ", "Facturación",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                              

                                    }


                                }

                            }


                    }
                    else if (proce == 2)//CON ORDEN DE COMPRA
                    {

                        nota.ICentroCostos = codCentro;
                        nota.CodAlmacen = frmLogin.iCodAlmacen;
                        nota.CodTipoTransaccion = tran.CodTransaccion;
                        nota.CodProveedor = prov.CodProveedor;
                        nota.CodTipoDocumento = doc.CodTipoDocumento;
                        nota.NumDoc = txtNDocRef.Text;
                        nota.FechaIngreso = dtpFecha.Value.Date;
                        nota.Comentario = txtComentario.Text;
                        nota.CodUser = frmLogin.iCodUser;
                        nota.CodOrdenCompra = codOrdenCompra_nota;
                        nota.codalmacenemisor = 0;
                        if (txtFlete.Text == "")
                            nota.Flete = 0;
                        else
                            nota.Flete = Convert.ToDouble(txtFlete.Text);


                        if (codOrdenCompra_nota != 0)
                        {
                            Orde = AdmOrd.BuscaCabeceraOrden(codOrdenCompra_nota);
                            nota.Moneda = Orde.Moneda;
                            nota.TipoCambio = Convert.ToDouble(Orde.Tipocambio);
                            nota.FormaPago = Convert.ToInt32(Orde.Formapago);
                            nota.FechaPago = Orde.Fechapago;
                        }

                        else
                        {
                            nota.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                            if (txtTipoCambio.Text != "")
                                nota.TipoCambio = Convert.ToDouble(txtTipoCambio.Text);
                            else nota.TipoCambio = 0;
                            if (cmbFormaPago.SelectedValue == null)
                                nota.FormaPago = 0;
                            else
                                nota.FormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                            nota.FechaPago = dtpFecha.Value.Date;
                        }

                        nota.MontoBruto = Convert.ToDouble(txtBruto.Text);
                        if (txtDscto.Text != "")
                            nota.MontoDscto = Convert.ToDouble(txtDscto.Text);
                        else nota.MontoDscto = 0;
                        if (txtFlete.Text != "")
                            nota.Flete = Convert.ToDouble(txtFlete.Text);
                        else nota.Flete = 0;
                        nota.Igv = Convert.ToDouble(txtIGV.Text);
                        nota.Total = Convert.ToDouble(txtPrecioVenta.Text);
                        nota.CodUser = frmLogin.iCodUser;
                        nota.Estado = 1;
                        // Para saber si la nota esta activa o anulada. El estado se podra cambiar en una ventana especifica para anular notas
                        if (txtLoteProduccion.Text != "")
                            nota.CodLoteProduccion = Convert.ToInt32(txtLoteProduccion.Text);
                        else
                            nota.CodLoteProduccion = 0;
                        nota.CodPesador = Convert.ToInt32(cboPesador.SelectedValue);
                        if (CodIngresoCamara != 0)
                            nota.CodIngresoCamara = CodIngresoCamara;

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

                        }


                    }
                }






            }
             else
            {
                MessageBox.Show("Debe completar todos los campos requeridos(*)", "Ingreso Materia Prima", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





           

        }






        private void ingresarpago()
        {   
            frmCancelarPago form = new frmCancelarPago();
            form.CodNota = fac.CodFacturaNueva.ToString();
            form.tipo = 1; // (1)pago de nota de salida (2) pago de letra
            form.ShowDialog();  
        }

        private void RecorreDetalle()
        {

            if (CodTransaccion == 14)//Orden de Compra
            {
                detalle.Clear();
                if (dgvDetalle2.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dgvDetalle2.Rows)
                    {
                        añadedetalle2(row);
                    }
                }
            }

        }




      





        private void recorrerdetalle()
        {

            
            detalle.Clear();

            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvDetalle.Rows)
                {
                    deta = new clsDetalleNotaIngreso();
                    deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
                    deta.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
                    deta.CodAlmacen = frmLogin.iCodAlmacen;
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
                    deta.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
                    deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
                    deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
                    deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
                    deta.FechaIngreso = dtpFecha.Value;

                    deta.Bonificacion = false;
                    if (codOrdenCompra_nota != 0)
                    {
                        Orde = AdmOrd.BuscaCabeceraOrden(codOrdenCompra_nota);
                        deta.Moneda = Orde.Moneda;
                    }
                    else
                    {
                        deta.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                    }

                    if (tran.CodTransaccion == 22)
                        deta.DCantidadDev = Convert.ToDouble(fila.Cells[CantDev.Name].Value);
                    //RAGA
                    if (tran.CodTransaccion == 24 || tran.CodTransaccion == 27)
                    {
                        deta.IBatch = Convert.ToInt32(fila.Cells[nrobatch1.Name].Value);
                        deta.DNroBrix = Convert.ToDouble(fila.Cells[gradosbrix.Name].Value);
                        deta.DPesoneto = Convert.ToDouble(fila.Cells[pneto.Name].Value);
                        deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
                        //serielote
                    }

                    deta.CodUser = frmLogin.iCodUser;
                    detalle.Add(deta);
                }
            }



        }


        private void recorrerdetallefactura()
        {


          
            detalleFactura.Clear();


            if (dgvDetalle.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in dgvDetalle.Rows)
                {
                    detafac = new clsDetalleFactura();
                    detafac.CodFactura = fac.CodFacturaNueva;
                    detafac.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
                    detafac.CodNotaIngreso = nota.CodNotaIngreso;
                    detafac.CodAlmacen = frmLogin.iCodAlmacen;
                    detafac.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
                    detafac.SerieLote = "0";
                    detafac.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
                    detafac.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
                    detafac.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
                    detafac.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
                    detafac.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
                    detafac.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
                    detafac.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
                    detafac.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
                    detafac.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
                    detafac.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
                    detafac.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
                    detafac.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
                    detafac.FechaIngreso = dtpFecha.Value;
                    if (codOrdenCompra_nota != 0)
                    {
                        Orde = AdmOrd.BuscaCabeceraOrden(codOrdenCompra_nota);
                        detafac.Moneda = Orde.Moneda;
                    }
                    else
                    {
                        detafac.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
                    }

                    detafac.CodUser = frmLogin.iCodUser;
                    detafac.CodProveedor = CodProveedor;
                    detalleFactura.Add(detafac);

                }
            }
        }

        private void añadedetalle2(DataGridViewRow fila)
        {

            clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
            deta1.CodProducto = Convert.ToInt32(fila.Cells[coprod.Name].Value);
            deta1.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
            deta1.CodAlmacen = frmLogin.iCodAlmacen;
            deta1.UnidadIngresada = Convert.ToInt32(fila.Cells[coduni.Name].Value);
            deta1.SerieLote = "0"; // esta pendiente
            deta1.Cantidad = Convert.ToDouble(fila.Cells[cantn.Name].Value);
            deta1.FechaIngreso = dtpFecha.Value;
            deta1.CodUser = frmLogin.iCodUser;
            deta1.CodProveedor = Convert.ToInt32(txtCodProveedor.Text);
            deta1.CoddetalleOrden = Convert.ToInt32(fila.Cells[codetord.Name].Value);
            deta1.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounitario.Name].Value);
            deta1.Subtotal = Convert.ToDouble(fila.Cells[subtotal.Name].Value);
            deta1.Descuento1 = Convert.ToDouble(fila.Cells[descuento1.Name].Value);
            deta1.Descuento2 = Convert.ToDouble(fila.Cells[descuento2.Name].Value);
            deta1.Descuento3 = Convert.ToDouble(fila.Cells[descuento3.Name].Value);
            deta1.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto1.Name].Value);
            deta1.Igv = Convert.ToDouble(fila.Cells[igv1.Name].Value);
            deta1.Importe = Convert.ToDouble(fila.Cells[importe1.Name].Value);
            deta1.PrecioReal = Convert.ToDouble(fila.Cells[precioreal1.Name].Value);
            deta1.ValoReal = Convert.ToDouble(fila.Cells[valoreal1.Name].Value);
            deta1.Flete = Convert.ToDouble(fila.Cells[flete1.Name].Value);
            deta1.Bonificacion = Convert.ToBoolean(fila.Cells[Bonificacion.Name].Value);
            if (codOrdenCompra_nota != 0)
            {
                Orde = AdmOrd.BuscaCabeceraOrden(codOrdenCompra_nota);
                deta1.Moneda = Orde.Moneda;
            }
            else
            {
                deta1.Moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
            }

            detalle.Add(deta1);


        }




        private void CargaFilaDetalle(DataGridViewRow fila)
        {
            if (proce == 1)
            {
                detaSelec.CodProducto = Convert.ToInt32(fila.Cells[coprod.Name].Value);
                detaSelec.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
                detaSelec.CodAlmacen = frmLogin.iCodAlmacen;
                detaSelec.UnidadIngresada = Convert.ToInt32(fila.Cells[coduni.Name].Value);
                detaSelec.Flete = Convert.ToDouble(fila.Cells[flete1.Name].Value);
                //detaSelec.SerieLote = fila.Cells[serielote.Name].Value.ToString();
                if (fila.Cells[cantn.Name].Value != DBNull.Value)
                {
                    detaSelec.Cantidad = Convert.ToDouble(fila.Cells[cantn.Name].Value);
                }
                else
                {
                    detaSelec.Cantidad = 0;
                }
                detaSelec.FechaIngreso = dtpFecha.Value;
                detaSelec.CodUser = frmLogin.iCodUser;
            }
            else
            {
                detaSelec.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
                detaSelec.CodNotaIngreso = Convert.ToInt32(nota.CodNotaIngreso);
                detaSelec.CodAlmacen = frmLogin.iCodAlmacen;
                detaSelec.Moneda = cmbMoneda.SelectedIndex;
                detaSelec.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
                detaSelec.SerieLote = fila.Cells[serielote.Name].Value.ToString();
                if (fila.Cells[cantidad.Name].Value != DBNull.Value)
                {
                    detaSelec.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
                }
                else
                {
                    detaSelec.Cantidad = 0;
                }
                detaSelec.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
                detaSelec.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
                detaSelec.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
                detaSelec.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
                detaSelec.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
                detaSelec.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
                detaSelec.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
                detaSelec.Flete = Convert.ToDouble(fila.Cells[flete.Name].Value);
                detaSelec.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
                detaSelec.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
                detaSelec.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
                detaSelec.FechaIngreso = dtpFecha.Value;
                detaSelec.CodUser = frmLogin.iCodUser;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {





            if (  (CodTransaccion == 24 || CodTransaccion  == 27)   && dgvDetalle.Rows.Count > 0)
            {

          
                if (Application.OpenForms["frmDetalleGuia"] != null)
                {
                    Application.OpenForms["frmDetalleGuia"].Activate();
                }
                else
                {
                    frmDetalleGuia form = new frmDetalleGuia();
                    form.Procede = 14;
                    form.Proceso = 2;

                    if (codProductoTerm != 0)
                        form.txtCodigo.Text = codProductoTerm.ToString();

                    if (nroBatch > 0)
                        form.textBox4.Text = nroBatch.ToString();

                    form.textBox6.Text = iniserie.ToString();
                    form.textBox2.Text = finSerie.ToString();
                    form.txtCantidad.Text = (finSerie - iniserie).ToString();
                    form.txtReferencia.Enabled = false;

                    form.ShowDialog();
                }
               
            }


            else{


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
                    form.Procede = 6;
                    form.bvalorventa = cbValorVenta.Checked;
                    form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
                    form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
                    form.txtReferencia.ReadOnly = true;
                    form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
                    form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
                    form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
                    form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
                    form.txtDscto2.Text = row.Cells[dscto2.Name].Value.ToString();
                    form.txtDscto3.Text = row.Cells[dscto3.Name].Value.ToString();
                    form.txtPrecioNeto.Text = row.Cells[importe.Name].Value.ToString();
                    form.txtCantidad.Focus();
                    form.ShowDialog();                    
                }
            }     
       


        }
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

        private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
           {
               detaSelec.CodProducto = Convert.ToInt32(e.Row.Cells[codproducto.Name].Value);
           }          
        }

        private void cmbFormaPago_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fpago = AdmPago.CargaFormaPago(Convert.ToInt32(cmbFormaPago.SelectedValue));
            dtpFechaPago.Value = dtpFecha.Value.AddDays(fpago.Dias); 
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {


            if (CodTransaccion == 1 || CodTransaccion == 22)
            {

                if (dgvDetalle.SelectedRows.Count > 0)
                {
                    dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
                    codProd.Remove(detaSelec.CodProducto);
                }

            }


            if (CodTransaccion == 14)
            {
                if (dgvDetalle2.SelectedRows.Count > 0)
                {
                    dgvDetalle2.Rows.Remove(dgvDetalle2.CurrentRow);
                }
                if (dgvDetalle2.Rows.Count == 0)
                {
                     data.Clear();
                }
                dgvDetalle2.Refresh();
            }

            if (CodTransaccion == 24 || CodTransaccion == 27)
            {


                DialogResult dlgResult = MessageBox.Show("Esta seguro que eliminar los batchs ingresados", "Ingreso de camaras", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    dgvDetalle.Rows.Clear();
                    codProductoTerm = 0;
                }

              }
         
        }

        private void dgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (Proceso == 1)
            {
                //Double bruto = 0;
                //Double descuen = 0;
                //Double valor = 0;
                //foreach (DataGridViewRow row in dgvDetalle.Rows)
                //{
                //    bruto = bruto + Convert.ToDouble(row.Cells[importe.Name].Value);
                //    descuen = descuen + Convert.ToDouble(row.Cells[montodscto.Name].Value);
                //    valor = valor + Convert.ToDouble(row.Cells[valorventa.Name].Value);
                //}
                //txtBruto.Text = String.Format("{0:#,##0.0000}", bruto);
                //txtDscto.Text = String.Format("{0:#,##0.0000}", descuen);
                //txtValorVenta.Text = String.Format("{0:#,##0.0000}", valor);
                //txtIGV.Text = String.Format("{0:#,##0.0000}", bruto - descuen - valor);
                //txtPrecioVenta.Text = String.Format("{0:#,##0.0000}", bruto - descuen);
                calculatotales();
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
                    if (CodCliente != 0) { CargaCliente(); ProcessTabKey(true); }
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
            }
        }

        private void txtCodCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
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
            if (Proceso != 0 && e.ControlToValidate.Visible)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator4_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0 && e.ControlToValidate.Visible)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator5_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0 && e.ControlToValidate.Visible)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator6_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0 && e.ControlToValidate.Visible)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator7_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
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

        private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalle.Columns[e.ColumnIndex].Name == "precioventa")
            {
                if (Proceso == 1)
                {
                    calculatotales();
                }
            }
        }

        private void txtPDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                //prorrateodeflete();
                //recalculadetalle();
                //calculatotales();
            }   
        }

        private void prorrateodeflete()
        {
            if (txtFlete.Text != "" && dgvDetalle.Rows.Count >= 1)
            {
                Double precior = 0;
                Double percentr = 0;
                Double fleter = 0;
                Double totalr = 0;
                Double dflete = Convert.ToDouble(txtFlete.Text);

                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {                   
                    totalr = totalr + Convert.ToDouble(row.Cells[precioventa.Name].Value);
                }
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    precior = Convert.ToDouble(row.Cells[precioventa.Name].Value);
                    percentr = precior / totalr;
                    fleter = dflete * percentr;
                    row.Cells[flete.Name].Value = String.Format("{0:#,##0.0000}", fleter);
                }
            } 
        }

        private void recalculadetalle()
        {
            if (proce == 1)
            {
                Decimal vvflete = 0;
                Decimal pvflete = 0;
                foreach (DataGridViewRow row in dgvDetalle2.Rows)
                {
                    if (Convert.ToDecimal(row.Cells[cantn.Name].Value) != 0)
                    {
                        vvflete = Convert.ToDecimal(row.Cells[valorventa1.Name].Value) +
                                  Convert.ToDecimal(row.Cells[flete1.Name].Value);
                        pvflete = Convert.ToDecimal(row.Cells[importe1.Name].Value) +
                                  Convert.ToDecimal(row.Cells[flete1.Name].Value);
                        if (Convert.ToDouble(row.Cells[flete1.Name].Value) > 0.00 &&
                            row.Cells[flete1.Name].Value.ToString() != "")
                        {
                            row.Cells[valoreal1.Name].Value = vvflete/Convert.ToDecimal(row.Cells[cantn.Name].Value);
                            row.Cells[precioreal1.Name].Value = pvflete/Convert.ToDecimal(row.Cells[cantn.Name].Value);
                        }
                        else
                        {
                            row.Cells[valoreal1.Name].Value = Convert.ToDecimal(row.Cells[valorventa1.Name].Value)/
                                                              Convert.ToDecimal(row.Cells[cantn.Name].Value);
                            row.Cells[precioreal1.Name].Value = Convert.ToDecimal(row.Cells[importe1.Name].Value)/
                                                                Convert.ToDecimal(row.Cells[cantn.Name].Value);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvDetalle.Rows)
                {
                    row.Cells[valorventaconflete.Name].Value = Convert.ToDouble(row.Cells[valorventa.Name].Value) + Convert.ToDouble(row.Cells[flete.Name].Value);
                    row.Cells[pvconflete.Name].Value = Convert.ToDouble(row.Cells[precioventa.Name].Value) + Convert.ToDouble(row.Cells[flete.Name].Value);
                    if (Convert.ToDouble(row.Cells[flete.Name].Value) > 0.00 && row.Cells[flete.Name].Value.ToString() != "")
                    {
                        row.Cells[valoreal.Name].Value = Convert.ToDouble(row.Cells[valorventaconflete.Name].Value) / Convert.ToDouble(row.Cells[cantidad.Name].Value);
                        row.Cells[precioreal.Name].Value = Convert.ToDouble(row.Cells[pvconflete.Name].Value) / Convert.ToDouble(row.Cells[cantidad.Name].Value);
                    }
                    else
                    {
                        row.Cells[valoreal.Name].Value = Convert.ToDouble(row.Cells[valorventa.Name].Value) / Convert.ToDouble(row.Cells[cantidad.Name].Value);
                        row.Cells[precioreal.Name].Value = Convert.ToDouble(row.Cells[precioventa.Name].Value) / Convert.ToDouble(row.Cells[cantidad.Name].Value);
                    }
                }
            }

        }

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!this.Visible)
                return;
            if (dgvDetalle.Rows.Count >= 1 && dgvDetalle.CurrentRow.Index == e.RowIndex && e.RowIndex != -1)
            {
                CargaFilaDetalle(dgvDetalle.CurrentRow);
            }            
        }

        private void txtOrdenCompra_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (txtCodProveedor.Text != "")
                    {
                        if (Application.OpenForms["frmOrdenCompraVigentes"] != null)
                        {
                            Application.OpenForms["frmOrdenCompraVigentes"].Activate();
                        }
                        else
                        {
                            frmOrdenCompraVigentes form = new frmOrdenCompraVigentes();
                            form.Proce = 5;
                            form.codproveedor = Convert.ToInt32(txtCodProveedor.Text);
                            form.ShowDialog();
                            CodAlmacenOrden=form.CodAlmacenOrden ;
                            if (form.OrdCom == 0)
                            {
                                txtOrdenCompra.Focus();
                            }
                            else
                            {
                                codOrdenCompra_nota = form.OrdCom;
                                txtOrdenCompra.Text = form.Docum;
                                //prov = form.deta;
                                //txtCodProv.Text = prov.Ruc;
                                //txtCodProveedor.Text = prov.CodProveedor.ToString();
                                //txtNombreProv.Text = prov.RazonSocial.ToString();
                                txtFlete.Text = form.Flete;
                                // btnDetalle.Visible = true;
                                llenardetalle();
                                cmbMoneda.SelectedValue = AdmOrd.BuscaCabeceraOrden(codOrdenCompra_nota).Moneda;
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese Proveedor");
                        txtCodProv.Focus();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
        }

        private void llenardetalle()
        {
            dgvDetalle2.DataSource = AdmOrd.MuestraDetalleOrden_Nota(codOrdenCompra_nota);
            data = (DataTable) dgvDetalle2.DataSource;
            dgvDetalle2.ClearSelection();
        }

        private void dgvDetalle2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            txtedit = e.Control as TextBox;
            if (txtedit != null)
            {
                txtedit.KeyPress -= new KeyPressEventHandler(dgvDetalle2_KeyPress);
                txtedit.KeyPress += new KeyPressEventHandler(dgvDetalle2_KeyPress);
                txtedit.KeyUp -= new KeyEventHandler(dgvDetalle2_KeyUp);
                txtedit.KeyUp += new KeyEventHandler(dgvDetalle2_KeyUp);
            }
        }

        private void dgvDetalle2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgvDetalle2.CurrentCell.ColumnIndex == 7)//CantidadNueva
            {
                val.SOLONumeros(sender,e);
            }
        }

        private void dgvDetalle2_KeyUp(object sender, KeyEventArgs e)
        {

            //try
            //{
            //    if (dgvDetalle2.Columns[dgvDetalle2.CurrentCell.ColumnIndex].Name == "cantn" && txtedit.Text != "")
            //    //CantidadNueva
            //    {
            //        //if (Convert.ToDouble(txtedit.Text) >
            //        //    Convert.ToDouble(dgvDetalle2.CurrentRow.Cells[cant.Name].Value))
            //        //{
            //        //    MessageBox.Show("Cantidad Nueva Debe Ser Menor o Igual que la Cantidad de la Orden");
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(" " + ex);
            //}
        }

        private void dgvDetalle2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvDetalle2_CellEndEdit(sender, e);
            if (!this.Visible)
                return;
            if (dgvDetalle2.Rows.Count >= 1 && dgvDetalle2.CurrentRow.Index == e.RowIndex && e.RowIndex != -1)
            {
                CargaFilaDetalle(dgvDetalle2.CurrentRow);
            } 
            
        }

        private void dgvDetalle2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                QIngresado = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[SaldoIngresado1.Name].Value);
                QPorAtender = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantidadPendiente1.Name].Value);

                if (dgvDetalle2.Columns[dgvDetalle2.CurrentCell.ColumnIndex].Name == "cantn" && txtedit.Text != "" &&
                    Convert.ToInt32(dgvDetalle2.CurrentRow.Cells[codetord.Name].Value) != 0)
                {
                    if (Convert.ToDouble(txtedit.Text) >
                        Convert.ToDouble(dgvDetalle2.CurrentRow.Cells[cantidadPendiente1.Name].Value))
                    {
                        MessageBox.Show("Cantidad Nueva Debe Ser Menor o Igual que la Cantidad de la Orden");
                        dgvDetalle2.CurrentRow.Cells[cantn.Name].Value = 0.00;
                    }
                    else
                    {
                        Qnueva = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantn.Name].Value);

                        dgvDetalle2.CurrentRow.Cells[SaldoIngresado.Name].Value = (QIngresado + Qnueva);
                        dgvDetalle2.CurrentRow.Cells[cantidadPendiente.Name].Value = (QPorAtender - Qnueva);

                        dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value =
                            Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantn.Name].Value)*
                            Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[preciounitario.Name].Value);
                        importes();
                        calculatotales();
                    }

                }
                else if (dgvDetalle2.Columns[dgvDetalle2.CurrentCell.ColumnIndex].Name == "cantn" && txtedit.Text != "" &&
                    Convert.ToInt32(dgvDetalle2.CurrentRow.Cells[codetord.Name].Value) == 0)
                {
                    
                    dgvDetalle2.CurrentRow.Cells[SaldoIngresado.Name].Value = txtedit.Text;
                    dgvDetalle2.CurrentRow.Cells[SaldoIngresado1.Name].Value = txtedit.Text;
                    dgvDetalle2.CurrentRow.Cells[cant.Name].Value = txtedit.Text;
                    dgvDetalle2.CurrentRow.Cells[cantn.Name].Value = txtedit.Text;
                    
                    dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value =
                        Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[cantn.Name].Value) *
                        Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[preciounitario.Name].Value);
                    importes();
                    calculatotales();
                  
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void importes()
        {
            Decimal precio = 0;
            Decimal valor = 0;
            Decimal Igv = 0;
            prod = Admpro.CargaProductoDetalle(Convert.ToInt32(dgvDetalle2.CurrentRow.Cells[coprod.Name].Value), frmLogin.iCodAlmacen, 1, 0);
            //if (cbValorVenta.Checked)
            //{
                if (prod.ConIgv)
                {

                    precio = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value);
                    valor = precio / Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                    Igv = precio - valor;
                }
                else
                {
                    precio = Convert.ToDecimal(dgvDetalle2.CurrentRow.Cells[subtotal.Name].Value);
                    valor = precio;
                    Igv = precio - valor;

                }
                dgvDetalle2.CurrentRow.Cells[importe1.Name].Value = precio;
                dgvDetalle2.CurrentRow.Cells[valorventa1.Name].Value = valor;
                dgvDetalle2.CurrentRow.Cells[igv1.Name].Value = Igv;
            //}
            recalculadetalle();
        }

        private void dgvDetalle2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //dgvDetalle2_CellEndEdit(sender, e);
        }

        private void dgvDetalle2_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {            
        }

        private void txtDocRef_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvDetalle2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (Proceso == 1)
            {
                calculatotales();
            }
        }

        private void dgvDetalle2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (Proceso == 1)
            {
                calculatotales();
            }
        }

        private void customValidator8_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0 && e.ControlToValidate.Visible)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {                    
                    if (Application.OpenForms["frmIngresosCamara"] != null)
                    {
                        Application.OpenForms["frmIngresosCamara"].Activate();
                    }
                    else
                    {

                        frmIngresosCamara form = new frmIngresosCamara();
                        form.Proceso = 4;
                        form.WindowState = System.Windows.Forms.FormWindowState.Normal; 
                        form.ShowDialog();


                     if (form.bcamara == true){


                        btnEditar.Visible = false;
                        btnEliminar.Visible = false;
                        CargaProveedor();

                        dataGridView1.DataSource = Admingre.ListaDetalleIngresoContenedor(CodIngresoCamara);
                        if (dataGridView1.Rows.Count > 0)
                        {

                            
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                dgvDetalle.Rows.Add("", Convert.ToInt32(row.Cells[codProducto2.Name].Value), row.Cells[referencia2.Name].Value.ToString(), row.Cells[producto2.Name].Value.ToString(), Convert.ToInt32(row.Cells[codUnidadMedida2.Name].Value), "",
                                     row.Cells[unidad2.Name].Value.ToString(), "0", Convert.ToDouble(row.Cells[cantidad2.Name].Value), 0.00, 0.00,
                                      0.00, 0.00, 0.00, 0.00, 0.00,
                                      0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, "", "", "", Convert.ToDouble(row.Cells[CantidadDev.Name].Value), Convert.ToDouble(row.Cells[CantidadTot.Name].Value));
                            }
                            
                   //     MessageBox.Show("Editar Cantidad Devuelta");
                        editaCantDev0();
                        }
                    }



                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
        }



        //Editar CantDev y CantTotal
        public void editaCantDev()
        {

            //Editar 1ra Cantidad
            DataGridViewRow row = dgvDetalle.CurrentRow;
            if (Application.OpenForms["frmDetalleIngreso"] != null)
            {
                Application.OpenForms["frmDetalleIngreso"].Activate();
            }
            else
            {
                frmDetalleIngreso form = new frmDetalleIngreso();
                if (dgvDetalle.Rows.Count == 0)
                {
                    form.Proceso = 2;
                    form.Procede = 10;
                }
                else if (dgvDetalle.Rows.Count >= 0)
                {
                    form.Proceso = 2;
                    form.Procede = 10;
                    //       form.Procede2 = 1;
                    form.bvalorventa = cbValorVenta.Checked;
                    form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
                    form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
                    form.txtReferencia.ReadOnly = true;
                    form.txtDescripcion.Text = row.Cells[descripcion.Name].Value.ToString();
                    form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
                    form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
                    form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
                    form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
                    form.txtDscto2.Text = row.Cells[dscto2.Name].Value.ToString();
                    form.txtDscto3.Text = row.Cells[dscto3.Name].Value.ToString();
                    form.txtPrecioNeto.Text = row.Cells[importe.Name].Value.ToString();
                    form.textBox1.Text = row.Cells[CantEntregada.Name].Value.ToString();
                    form.textBox2.Text = row.Cells[CantDev.Name].Value.ToString();
                    form.CodUnidad = Int32.Parse(row.Cells[codunidad.Name].Value.ToString());
                    //Stock Actual
                    form.txtStock.Text = Admpro.CargaStockProducto(Convert.ToInt32(form.txtCodigo.Text), frmLogin.iCodAlmacen, 5).StockDisponible.ToString();
                    //  form.txtCantidad.Focus();
                }
                form.ShowDialog();
              
            }

        }

        public void editaCantDev0()
        {


            DialogResult dlgResult = MessageBox.Show("¿ Desea editar la cantidad a devolver ?", "Lotes de Produccion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.No)
            {
                return;
            }
            else
            {
                //Editar 1ra Cantidad
                DataGridViewRow row = dgvDetalle.SelectedRows[0];
                if (Application.OpenForms["frmDetalleIngreso"] != null)
                {
                    Application.OpenForms["frmDetalleIngreso"].Activate();
                }
                else
                {
                    frmDetalleIngreso form = new frmDetalleIngreso();


                    //Desabilitar Suprimir

                    form.Proceso = 2;
                    form.Procede = 10;
                    form.bvalorventa = cbValorVenta.Checked;
                    form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
                    form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
                    form.txtReferencia.ReadOnly = true;
                    form.txtDescripcion.Text = row.Cells[descripcion.Name].Value.ToString();
                    form.txtControlStock.Text = row.Cells[serielote.Name].Value.ToString();
                    form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
                    form.txtPrecio.Text = row.Cells[preciounit.Name].Value.ToString();
                    form.txtDscto1.Text = row.Cells[dscto1.Name].Value.ToString();
                    form.txtDscto2.Text = row.Cells[dscto2.Name].Value.ToString();
                    form.txtDscto3.Text = row.Cells[dscto3.Name].Value.ToString();
                    form.txtPrecioNeto.Text = row.Cells[importe.Name].Value.ToString();
                    form.textBox1.Text = row.Cells[CantEntregada.Name].Value.ToString();
                    form.CodUnidad = Int32.Parse(row.Cells[codunidad.Name].Value.ToString());

                    form.txtStock.Text = Admpro.CargaStockProducto(Convert.ToInt32(row.Cells[codproducto.Name].Value.ToString()), frmLogin.iCodAlmacen, 5).StockDisponible.ToString();



                    //            form.txtCantidad.Focus();
                    form.ShowDialog();
                }




            }



        }


        private void txtLoteProduccion_KeyDown(object sender, KeyEventArgs e)
        {            
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (Application.OpenForms["frmLotes"] != null)
                    {
                        Application.OpenForms["frmLotes"].Activate();
                    }
                    else
                    {
                        frmLotes form = new frmLotes();
                        form.Proceso = 4;
                        form.WindowState = System.Windows.Forms.FormWindowState.Normal; 
                        form.ShowDialog();
                        CodLote= form.CodLote;
                        if (form.CodLote == 0)
                        {
                            txtLoteProduccion.Focus();
                        }
                        else
                        {
                            CodLote = form.CodLote;
                            lote = AdmLote.CargaLote(CodLote);

                            /*
                            MessageBox.Show("Lote de Producción :  " + lote.CodLoteProduccion
                                + "   Especie :  " + lote.Especie + "   Materia Prima:  " + lote.Matprima);
                            */


                            label23.Text =  "   Especie :  " + lote.Especie + "   Materia Prima:  " + lote.Matprima;


                            txtLoteProduccion.Text = lote.CodLoteProduccion.ToString();
                            ProcessTabKey(true);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
        }

        private void CargarPesador() {
            cboPesador.DataSource = admpesa.ListaPesadores();
            cboPesador.ValueMember = "codPesador";
            cboPesador.DisplayMember = "nombre";
            cboPesador.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmPesadores"] != null)
            {
                Application.OpenForms["frmPesadores"].Activate();
            }
            else
            {
                frmPesadores form = new frmPesadores();
                form.ShowDialog();
                CargarPesador();
            }
        }

        private void txtNumTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmAnalisisOrganoleptico"] != null)
            {
                Application.OpenForms["frmAnalisisOrganoleptico"].Activate();
            }
            else
            {
                frmAnalisisOrganoleptico form = new frmAnalisisOrganoleptico();
                form.ShowDialog();
            }
        }

        private void CargarIngresoCamaraNI()
        {
            try
            {
                ingreso = AdmIngre.CargaIngresoCamaraNotaI(CodIngresoCamara);
                if (ingreso != null)
                {
                   
                    cboPesador.SelectedValue = ingreso.CodPesador;
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void dgvDetalle_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if (!this.Visible)
                return;
            if (dgvDetalle.Rows.Count >= 1 && dgvDetalle.CurrentRow.Index == e.RowIndex && e.RowIndex != -1)
            {
                CargaFilaDetalle(dgvDetalle.CurrentRow);
            } 
            */
        }

        private void dgvDetalle_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {




            if (CodTransaccion == 24 || CodTransaccion == 27)
            {
                int posicion=0;
               // double valor = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[gradosbrix.Name].Value.ToString());

                try
                {
                    if (dgvDetalle.Columns[e.ColumnIndex].Name == "gradosbrix")
                    {
                        if (Convert.ToDouble(dgvDetalle.CurrentRow.Cells[gradosbrix.Name].Value.ToString()) <= 0)
                        {
                            MessageBox.Show("Ingresar GRADOS BRIX  Válido");
                            dgvDetalle.CurrentRow.Cells[gradosbrix.Name].Value = dgvDetalle.CurrentRow.Cells[gradosbrix2.Name].Value;
                        //    dgvDetalle.CurrentRow.Cells[pneto.Name].Value = dgvDetalle.CurrentRow.Cells[pneto2.Name].Value;
                        }
                        else
                        {

                            foreach (DataGridViewRow row in dgvDetalle.Rows)
                            {
                                if (  Int32.Parse(row.Cells[nrobatch1.Name].Value.ToString()) ==  Int32.Parse(dgvDetalle.CurrentRow.Cells[nrobatch1.Name].Value.ToString())   )
                                {
                                    row.Cells[gradosbrix.Name].Value = dgvDetalle.CurrentRow.Cells[gradosbrix.Name].Value;
                                }
                            }
                        }


                    }
                    if (dgvDetalle.Columns[e.ColumnIndex].Name == "pneto")
                    {
                        if (Convert.ToDouble(dgvDetalle.CurrentRow.Cells[pneto.Name].Value.ToString()) <= 0)
                        {
                            MessageBox.Show("Ingresar PESO NETO  Válido");
                        //    dgvDetalle.CurrentRow.Cells[gradosbrix.Name].Value = dgvDetalle.CurrentRow.Cells[gradosbrix2.Name].Value;
                            dgvDetalle.CurrentRow.Cells[pneto.Name].Value = dgvDetalle.CurrentRow.Cells[pneto2.Name].Value;
                        }
                        else
                        {
                            foreach (DataGridViewRow row in dgvDetalle.Rows)
                            {

                                if (Int32.Parse(row.Cells[nrobatch1.Name].Value.ToString()) == Int32.Parse(dgvDetalle.CurrentRow.Cells[nrobatch1.Name].Value.ToString()))
                                {
                                    row.Cells[pneto.Name].Value = dgvDetalle.CurrentRow.Cells[pneto.Name].Value;
                                }

                            }
                            calculatotales();

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datos ingresado incorrectos");
                    dgvDetalle.CurrentRow.Cells[gradosbrix.Name].Value = dgvDetalle.CurrentRow.Cells[gradosbrix2.Name].Value;
                    dgvDetalle.CurrentRow.Cells[pneto.Name].Value = dgvDetalle.CurrentRow.Cells[pneto2.Name].Value;
                }


            }
            else if (CodTransaccion == 22)
            {
                if (Proceso == 1)
                {
                    calculatotales();
                }
            }
            else 
            {
                //preciounit
                if (dgvDetalle.Columns[e.ColumnIndex].Name == "preciounit")
                {
                    if (Proceso == 1)
                    {
                        calculatotales();
                    }
                }

                if (dgvDetalle.Columns[e.ColumnIndex].Name == "CantDev2")
                {

                    if (Convert.ToDouble(dgvDetalle.CurrentRow.Cells[CantDev.Name].Value) >= Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value))
                    {
                        MessageBox.Show("La cantidad Devuelta o no puede ser Mayor o Igual a la Cantidad Total");
                        dgvDetalle.CurrentRow.Cells[CantDev.Name].Value = 0;
                    }
                    else
                    {
                        dgvDetalle.CurrentRow.Cells[CantEntregada.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value) - Convert.ToDouble(dgvDetalle.CurrentRow.Cells[CantDev.Name].Value);
                        dgvDetalle.CurrentRow.Cells[cantidad.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value);
                        dgvDetalle.CurrentRow.Cells[CantDev.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[CantDev.Name].Value);
                    }
                }
                if (dgvDetalle.Columns[e.ColumnIndex].Name == "preciounit")
                {
                    dgvDetalle.CurrentRow.Cells[preciounit.Name].Value = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
                    clsProducto pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value), frmLogin.iCodAlmacen, 1, 0);

                    double bruto, montodescuento, valorventa, factorigv, precioventa, precioreal, valorreal, igv1;

                    bruto = Convert.ToDouble(dgvDetalle.CurrentRow.Cells[cantidad.Name].Value) * Convert.ToDouble(dgvDetalle.CurrentRow.Cells[preciounit.Name].Value);
                    montodescuento = bruto;
                    if (pro.ConIgv)
                    {
                        valorventa = bruto;
                        factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                        precioventa = bruto * factorigv;
                    }
                    else
                    {
                        valorventa = bruto;
                        precioventa = valorventa;
                    }
                    precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                    valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                    igv1 = precioventa - valorventa;
                }

            }

        }

        private void dgvDetalle_RowsAdded_1(object sender, DataGridViewRowsAddedEventArgs e)
        {

            if (Proceso == 1  || Proceso == 3)
            {
                calculatotales();
            }
            /*
            if (CodTransaccion == 24)
                btnDetalle.Visible = false;*/
        }

        private void dgvDetalle_RowsRemoved_1(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (Proceso == 1)
            {
                //Double bruto = 0;
                //Double descuen = 0;
                //Double valor = 0;
                //foreach (DataGridViewRow row in dgvDetalle.Rows)
                //{
                //    bruto = bruto + Convert.ToDouble(row.Cells[importe.Name].Value);
                //    descuen = descuen + Convert.ToDouble(row.Cells[montodscto.Name].Value);
                //    valor = valor + Convert.ToDouble(row.Cells[valorventa.Name].Value);
                //}
                //txtBruto.Text = String.Format("{0:#,##0.0000}", bruto);
                //txtDscto.Text = String.Format("{0:#,##0.0000}", descuen);
                //txtValorVenta.Text = String.Format("{0:#,##0.0000}", valor);
                //txtIGV.Text = String.Format("{0:#,##0.0000}", bruto - descuen - valor);
                //txtPrecioVenta.Text = String.Format("{0:#,##0.0000}", bruto - descuen);
                calculatotales();
            }
        }

        private void dgvDetalle_RowStateChanged_1(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            /*
            if (dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
            {
                detaSelec.CodProducto = Convert.ToInt32(e.Row.Cells[codproducto.Name].Value);
            } 
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //RAGA
            if(CodTransaccion == 22)
            editaCantDev();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //CRNotaEntregaRecepcion


            if (CodTransaccion == 22)
            {
                clsReporteFrutosa ds = new clsReporteFrutosa();
                CRListaRecepcionNotaIngreso rpt = new CRListaRecepcionNotaIngreso();
                frmListaIngresosCamara frm = new frmListaIngresosCamara();
                rpt.SetDataSource(ds.getTicketIngresoContenedor(Int32.Parse(CodNota), frmLogin.iCodAlmacen,2).Tables[0]);
                frm.crvIngresosCamara.ReportSource = rpt; 
                frm.Show();

            }
            if (CodTransaccion == 24 || CodTransaccion == 27)
            {
                clsReporteFrutosa ds = new clsReporteFrutosa();
                CRNotaEntregaRecepcion rpt = new CRNotaEntregaRecepcion();
                frmListaIngresosCamara frm = new frmListaIngresosCamara();
                rpt.SetDataSource(ds.getNotaEntrega(CodLote).Tables[0]);
                frm.crvIngresosCamara.ReportSource = rpt;
                frm.Show();
            }

            if (CodDocumento == 19)
            {
                //Emitir Liquidación de Compra

            }



            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    if (Application.OpenForms["frmIngresosCamara"] != null)
                    {
                        Application.OpenForms["frmIngresosCamara"].Activate();
                    }
                    else
                    {

                        frmIngresosCamara form = new frmIngresosCamara();
                        form.Proceso = 4;
                        form.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        form.ShowDialog();


                        if (form.bcamara == false)
                            return;


                        //    CodIngresoCamara = form.CodIngresoCamara;

                        CantDev.Visible = true;
                        CantEntregada.Visible = true;
                   //     button3.Visible = true;

                        CargaProveedor();


                        dataGridView1.DataSource = Admingre.ListaDetalleIngresoContenedor(CodIngresoCamara);





                        //    detalle.Clear();
                        if (dataGridView1.Rows.Count > 0)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                dgvDetalle.Rows.Add("", Convert.ToInt32(row.Cells[codProducto2.Name].Value), row.Cells[referencia2.Name].Value.ToString(), row.Cells[producto2.Name].Value.ToString(), Convert.ToInt32(row.Cells[codUnidadMedida2.Name].Value), "",
                                     row.Cells[unidad2.Name].Value.ToString(), "0", Convert.ToDouble(row.Cells[cantidad2.Name].Value), 0.00, 0.00,
                                      0.00, 0.00, 0.00, 0.00, 0.00,
                                      0.00, 0.00, 0.00, 0.00, 0.00, 0.00, 0.00, "", "", "", Convert.ToDouble(row.Cells[CantidadDev.Name].Value), Convert.ToDouble(row.Cells[CantidadTot.Name].Value));
                            }
                        }

                        MessageBox.Show("Editar Cantidad Devuelta");
                        editaCantDev0();



                        /*
                      
                        if (form.CodIngresoCamara == 0)
                        {
                            CargaProveedor();
                            txtIngreso.Focus();
                        }
                        else
                        {                           
                            ProcessTabKey(true);    
                        }
                        */


                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(" " + ex);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmCentroCostos"] != null)
                {
                    Application.OpenForms["frmCentroCostos"].Activate();
                }
                else
                {
                    frmCentroCostos frm = new frmCentroCostos();
                    frm.Proceso = 3;
                    frm.ShowDialog();
                    codCentro = centro.Codcentro;
                    textBox2.Text = centro.Codcentro.ToString();
                   // label22.Text = centro.Nombre;
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmGestionProveedor frm = new frmGestionProveedor();
            frm.Proceso = 4;
            frm.ShowDialog();
        }

        private void txtNDocRef_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow dt in dgvDetalle.Rows)
            {

            }


     //       dgvDetalle.Rows.Add("0", prod.CodProducto, prod.Referencia, prod.Descripcion,prod.CodUnidadMedida, "",prod.UnidadDescrip, valor.ToString(), Convert.ToDouble("1"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new DateTime(), 0, new DateTime(), 0, 0, prod.DGradosBrix, prod.InroBatch,prod.Dpneto);





         
        }

        private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dgvDetalle.Rows)
            {
                codProductoTerm = Int32.Parse(row.Cells[codproducto.Name].Value.ToString());
            }
        }

        private void customValidator1_ValidateValue_1(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {

        }

        private void cbValorVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (cbValorVenta.Checked == true)
            {
                cbValorVenta.Text = "Valor Venta";

            }
            else
            {
                cbValorVenta.Text = "Precio Venta";
            }
        }

    }
}
