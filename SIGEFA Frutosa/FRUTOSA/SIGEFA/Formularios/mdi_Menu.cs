using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using SIGEFA.Formularios;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Conexion;
using SIGEFA.Reportes;

namespace SIGEFA
{
    public partial class mdi_Menu : DevComponents.DotNetBar.Office2007RibbonForm
    {
        private int childFormNumber = 0;
        public static Boolean Cambio = false;
        clsAdmAcceso AdmAcce = new clsAdmAcceso();
        Boolean FormEncontrado;
        clsAperturaCierre aper = new clsAperturaCierre();
        clsAdmAperturaCierre AdmAper = new clsAdmAperturaCierre();
        clsAdmAlmacen admalm = new clsAdmAlmacen();
        clsAdmTransaccion admTrans = new clsAdmTransaccion();
        clsConexionMysql con = new clsConexionMysql();
        clsAdmTipoCambio tc=new clsAdmTipoCambio();
        clsTipoCambio clstc=new clsTipoCambio();
        clsAdmCotizacion admcot=new clsAdmCotizacion();
        private Boolean rpta;
        List<DevComponents.DotNetBar.ButtonItem> ListaControles = new List<DevComponents.DotNetBar.ButtonItem>();
        public Double tc_hoy = 0;
        public Int32 tcvalida;

        private void GenerarLista()
        {   
            ListaControles.Add(biVenta);
            ListaControles.Add(biMuestraVentas);
            ListaControles.Add(biVentaRapida);
            ListaControles.Add(biGuia);
            ListaControles.Add(biGuias);
            ListaControles.Add(biNotaCredito);
            ListaControles.Add(ciNotasdeCredito);
            ListaControles.Add(biCobros);
            ListaControles.Add(biComision2);
            ListaControles.Add(biComisionVendedores);
            ListaControles.Add(biComisionVentas);
            ListaControles.Add(biCotizacion);
            ListaControles.Add(biCotizacionesVigentes);
            ListaControles.Add(biPedidoVenta);
            ListaControles.Add(biPedidosPendientes);
            ListaControles.Add(biPedidoCompra);
            ListaControles.Add(biPagos);
            ListaControles.Add(btnRequerimiento);
            ListaControles.Add(buttonItem9);//Requerimiento Pendientes
            ListaControles.Add(biHistorialRequerimiento);
            ListaControles.Add(biOrdenCompra);
            ListaControles.Add(biOrdenesCompras);
            ListaControles.Add(BiHistorialOrdenes);
            ListaControles.Add(biCompraOrden);
            ListaControles.Add(biHistorialFacturaciones);
            ListaControles.Add(biConsolidado);
            ListaControles.Add(biGuiasSinFacturar);
            ListaControles.Add(biNotadeIngreso);
            ListaControles.Add(biNotadeSalida);
            ListaControles.Add(biTransferencia);
            ListaControles.Add(biConsulta);
            ListaControles.Add(biModificar);
            ListaControles.Add(biAnular);
            ListaControles.Add(biEliminar);
            ListaControles.Add(btArqueo);
            ListaControles.Add(biProductos);
            ListaControles.Add(biCatalogo);
            ListaControles.Add(biClientes);
            ListaControles.Add(biProveedores);
            ListaControles.Add(biInventario);
            ListaControles.Add(biKardex);
            ListaControles.Add(btnReporte);
            ListaControles.Add(biEmpresa);
            ListaControles.Add(biAlmacen);
            ListaControles.Add(biUsuarios);
            ListaControles.Add(biTablas);
            ListaControles.Add(biParametros);
            ListaControles.Add(biVigenciaCotizaciones);
            ListaControles.Add(biBackup);
            ListaControles.Add(biImport);
            ListaControles.Add(biUnidades);
            ListaControles.Add(biFamilias);
            ListaControles.Add(biMarcas);
            ListaControles.Add(biTipoArticulo);
            ListaControles.Add(biCaracteristica);
            ListaControles.Add(biDocumentos);
            ListaControles.Add(biTransacciones);
            ListaControles.Add(biTipoCambio);
            ListaControles.Add(biAutorizado);
            ListaControles.Add(biFormaPago);
            ListaControles.Add(biMetodoPago);
            ListaControles.Add(biListasPrecios);
            ListaControles.Add(biVehiculosTransporte);
            ListaControles.Add(biConductores);
            ListaControles.Add(biEmpresasTransporte);
            ListaControles.Add(biZonas);
            ListaControles.Add(biVendedores);
            ListaControles.Add(biDestaques);
            ListaControles.Add(biBancos);
            ListaControles.Add(biCuentasCorrientes);
            ListaControles.Add(biTarjetaPago);
            ListaControles.Add(biPedidosPendientes);
            ListaControles.Add(biSucursal);
            ListaControles.Add(BiCaja);
            ListaControles.Add(BiAperturaCaja);
            ListaControles.Add(BiAprobacionPago);
            ListaControles.Add(biMovimientosCaja);
            ListaControles.Add(biCajaChica);
            ListaControles.Add(biMovimientosBancarios);
            ListaControles.Add(biIngresos);
            ListaControles.Add(biStockAlmacenes);
            ListaControles.Add(biNotaCreditoCompra);
            ListaControles.Add(btnNotaDebitoC);
            ListaControles.Add(biNotasCreditoCompras);
            ListaControles.Add(biTipoEgresoCaja);
            ListaControles.Add(biRegistraChequeCaja);
        }
                
        public mdi_Menu()
        {
            InitializeComponent();
            GenerarLista();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
               

        
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void buttonItem21_Click(object sender, EventArgs e)
        {
            frmUsuarios form = new frmUsuarios();
            form.MdiParent = this;                       
            form.Show();
        }

        private void mdi_Menu_Load(object sender, EventArgs e)
        {
            tabStrip1.Hide();
            //frmLogin.iCodAlmacen = 2;
            //frmLogin.AlmacenLogin = admalm.CargaAlmacen(frmLogin.iCodAlmacen);
            //frmLogin.sAlmacen = frmLogin.AlmacenLogin.Nombre;
            rpta = admcot.CotizacionesVencidas();
            frmSeleccionarAlmacen frm = new frmSeleccionarAlmacen();
            frm.ShowDialog();
            frmLogin.AcesosUsuario = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);                        
            sUsuario.Text = "Usuario : " + frmLogin.sNombreUser + " " + frmLogin.sApellidoUSer;
            sEmpresa.Text = "Empresa : " + frmLogin.sEmpresa;
            sAlmacen.Text = "Almacen : " + frmLogin.sAlmacen;
            sIP.Text = "IP : " + frmLogin.DirecIp;
            crearDirectorio();
            if (tc.VerificaTCFecha(DateTime.Now) == true)
            {
                tcvalida = 1;
                clstc = tc.CargaTipoCambio(DateTime.Now, 2);
                tc_hoy = clstc.Venta;
            }
            else
            {
                MessageBox.Show("Ingresa Tipo de Cambio de Hoy");
                if (Application.OpenForms["frmTipoCambio"] != null)
                {
                    Application.OpenForms["frmTipoCambio"].Activate();
                }
                else
                {
                    frmTipoCambio form = new frmTipoCambio();
                    //form.MdiParent = this;
                    form.btnNuevo_Click(sender, e);
                    form.ShowDialog();
                }
            }
        }

        private void crearDirectorio()
        {
            //crear las rutas
            
            string XML = @"C:\XML";

            try
            {//si no existe la carpeta temporal la creamos                
                if (!(System.IO.Directory.Exists(XML)))
                {
                    System.IO.Directory.CreateDirectory(XML);
                }
            }
            catch (Exception errorC)
            {
                MessageBox.Show(errorC.Message,
                         "Error al crear fichero temporal",
                         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }



        private void buttonItem22_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms["frmEmpresas"] != null)
            {
                Application.OpenForms["frmEmpresas"].Activate();
            }
            else
            {
                frmEmpresas form = new frmEmpresas();
                form.MdiParent = this;
                form.Show();
            }
           
        }

        //private void buttonItem24_Click(object sender, EventArgs e)
        //{
        //    if (Application.OpenForms["frmUsuarios"] != null)
        //    {
        //        Application.OpenForms["frmUsuarios"].Activate();
        //    }
        //    else
        //    {
        //        frmUsuarios form = new frmUsuarios();
        //        form.MdiParent = this;
        //        form.Show();
        //    }
        //}

        private void buttonItem1_Click(object sender, EventArgs e)
        {
             if (tcvalida==1){
                if (Application.OpenForms["frmProductos"] != null)
                {
                    Application.OpenForms["frmProductos"].Activate();
                }
                else
                {
                    frmProductos form = new frmProductos();
                    form.MdiParent = this;
                //    form.Dock = DockStyle.Fill;
                    form.tc_hoy = tc_hoy;
                    form.Show();
                }
             }
        }

        private void buttonItem26_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmUnidades"] != null)
            {
                Application.OpenForms["frmUnidades"].Activate();
            }
            else
            {
                frmUnidades form = new frmUnidades();
                //form.MdiParent = this;
                form.ShowDialog();
            }
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmFamilias"] != null)
            {
                Application.OpenForms["frmFamilias"].Activate();
            }
            else
            {
                frmFamilias form = new frmFamilias();
                //form.MdiParent = this;
                form.ShowDialog();
                //form.Show();
            }
        }

        private void buttonItem28_Click(object sender, EventArgs e)
        {
        
            if (Application.OpenForms["frmMarcas"] != null)
            {
                Application.OpenForms["frmMarcas"].Activate();
            }
            else
            {
                frmMarcas form = new frmMarcas();
                //form.MdiParent = this;
                form.ShowDialog();
            }
            
        }

        private void buttonItem29_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms["frmTipoArticulos"] != null)
            {
                Application.OpenForms["frmTipoArticulos"].Activate();
            }
            else
            {
                frmTipoArticulos form = new frmTipoArticulos();
                //form.MdiParent = this;
                form.ShowDialog();
            }
            
        }

        private void buttonItem30_Click(object sender, EventArgs e)
        {
            
            if (Application.OpenForms["frmCaracteristicas"] != null)
            {
                Application.OpenForms["frmCaracteristicas"].Activate();
            }
            else
            {
                frmCaracteristicas form = new frmCaracteristicas();
                //form.MdiParent = this;
                form.ShowDialog();
            }
            
        }

        private void mdi_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
           // Application.Exit();

            this.Hide();
            frmLogin frm = new frmLogin();
            frm.Show();  

            /*
            DialogResult dlgResult = MessageBox.Show("Esta seguro que desea cerrar session", "SIGEFA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                this.Hide();
                frmLogin frm = new frmLogin();
                frm.Show();  
            }
            else
            {
                return;
            }
         */

        }

        private void mdi_Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                frmSeleccionarAlmacen frm = new frmSeleccionarAlmacen();
                frm.ShowDialog();                
                if (Cambio)
                {
                    foreach (Form childForm in MdiChildren)
                    {
                        childForm.Close();
                    }
                    frmLogin.AcesosUsuario = AdmAcce.MuestraAccesos(frmLogin.iCodUser, frmLogin.iCodAlmacen);
                    OtorgarAccesos(ListaControles);
                    sAlmacen.Text = "Almacen : " + frmLogin.sAlmacen;                    
                }
                Cambio = false;
            }
        }

        private void mdi_Menu_Shown(object sender, EventArgs e)
        {
            OtorgarAccesos(ListaControles);
            tabStrip1.Hide();
            if (tc.VerificaTCFecha(DateTime.Now) == true)
            {
                tcvalida = 1;
                clstc = tc.CargaTipoCambio(DateTime.Now, 2);
                tc_hoy = clstc.Venta;

            }
            else
            {
                MessageBox.Show("Ingresa Tipo de Cambio de Hoy");
                if (Application.OpenForms["frmTipoCambio"] != null)
                {
                    Application.OpenForms["frmTipoCambio"].Activate();
                }
                else
                {
                    frmTipoCambio form = new frmTipoCambio();
                    //form.MdiParent = this;
                    form.btnNuevo_Click(sender, e);
                    form.ShowDialog();
                }
            }
        }

        private void buttonItem23_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmAlmacenes"] != null)
            {
                Application.OpenForms["frmAlmacenes"].Activate();
            }
            else
            {
                frmAlmacenes form = new frmAlmacenes();
                form.MdiParent = this;
                form.Show();
            }
           
        }


        private void OtorgarAccesos(List<DevComponents.DotNetBar.ButtonItem> Lista)
        {
            if (frmLogin.iNivelUser == 0)
            {
                foreach (DevComponents.DotNetBar.ButtonItem item in Lista)
                {
                    if (frmLogin.AcesosUsuario.Contains(Convert.ToInt32(item.Tag)))
                    {
                        if (item.Parent != null)
                        {
                            item.Parent.Enabled = true;
                        }
                        item.Enabled = true;
                    }
                    else
                    {
                        item.Enabled = false;
                    }

                }
            }
        }

        //private void OtorgarAccesos(Control.ControlCollection Coleccion)
        //{   
        //    Int32 cod;
        //    if (frmLogin.iNivelUser == 0)
        //    {
        //        foreach (Control c in Coleccion)
        //        {
        //            cod = Convert.ToInt32(c.Tag);
        //            if (frmLogin.AcesosUsuario.Contains(Convert.ToInt32(c.Tag)))
        //            {
        //                c.Enabled = true;
        //            }
        //            else
        //            {
        //                c.Enabled = false;
        //            }   
        //            if (c.Controls.Count > 0)
        //            {
        //                OtorgarAccesos(c.Controls);
        //            }
                    
        //        }
        //    }
        //}

        private void biNotadeIngreso_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {

                if (Application.OpenForms["frmNotaIngreso"] != null)
                {
                    Application.OpenForms["frmNotaIngreso"].Activate();
                }
                else
                {
                    frmNotaIngreso form = new frmNotaIngreso();
                    form.MdiParent = this;
                //    form.Dock = DockStyle.Fill;
                    
                    form.Proceso = 1;
                    form.Show();
                }
            }
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
             if (Application.OpenForms["frmUsuarios"] != null)
            {
                Application.OpenForms["frmUsuarios"].Activate();
            }
            else
            {
                frmUsuarios form = new frmUsuarios();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void biProveedores_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmProveedores"] != null)
            {
                Application.OpenForms["frmProveedores"].Activate();
            }
            else
            {
                frmProveedores form = new frmProveedores();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void biClienteSimple_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmClientesSimples"] != null)
            {
                Application.OpenForms["frmClientesSimples"].Activate();
            }
            else
            {
                frmClientesSimples form = new frmClientesSimples();
                form.MdiParent = this;
                form.Tipo = 0;
                form.Show();
            }
        }

        private void biClienteCompleto_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmClientesCompletos"] != null)
            {
                Application.OpenForms["frmClientesCompletos"].Activate();
            }
            else
            {
                frmClientesCompletos form = new frmClientesCompletos();
                form.MdiParent = this;
                form.Tipo = 1;
                form.Show();
            }
        }

        private void biClienteEmpresa_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmClientesCorporativos"] != null)
            {
                Application.OpenForms["frmClientesCorporativos"].Activate();
            }
            else
            {
                frmClientesCorporativos form = new frmClientesCorporativos();
                form.MdiParent = this;
                form.Tipo = 2;
                form.Show();
            }
        }

        private void biDocumentos_Click(object sender, EventArgs e)
        {

            if (Application.OpenForms["frmDocumentos"] != null)
            {
                Application.OpenForms["frmDocumentos"].Activate();
            }
            else
            {
                frmDocumentos form = new frmDocumentos();
                //form.MdiParent = this;
                form.ShowDialog();
            }
          
        }

        private void biTransacciones_Click(object sender, EventArgs e)
        {
        
            if (Application.OpenForms["frmTransacciones"] != null)
            {
                Application.OpenForms["frmTransacciones"].Activate();
            }
            else
            {
                frmTransacciones form = new frmTransacciones();
                //form.MdiParent = this;
                form.ShowDialog();
            }
            
        }

        private void biVenta_Click(object sender, EventArgs e)
        {
            if (tcvalida==1 && frmLogin.AcesosUsuario.Contains(Convert.ToInt32(biVenta.Tag)))
            {
                if (Application.OpenForms["frmVenta"] != null)
                {
                    Application.OpenForms["frmVenta"].Activate();
                }
                else
                {
                    frmVenta form1 = new frmVenta();
                    form1.MdiParent = this;
                    form1.Proceso = 1;
                    form1.Show();
                }
            }
        }

        private void biTipoCambio_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmTipoCambio"] != null)
            {
                Application.OpenForms["frmTipoCambio"].Activate();
            }
            else
            {
                frmTipoCambio form = new frmTipoCambio();
                //form.MdiParent = this;
                form.ShowDialog();
            }
        }

        private void biAutorizado_Click(object sender, EventArgs e)
        {
           
            if (Application.OpenForms["frmAutorizado"] != null)
            {
                Application.OpenForms["frmAutorizado"].Activate();
            }
            else
            {
                frmAutorizado form = new frmAutorizado();
                //form.MdiParent = this;
                form.ShowDialog();
            }
           
        }

        private void BuscaFormulario(Int32 Proceso)
        {
            foreach (Form formu in Application.OpenForms)
            {
                String typeform = formu.GetType().Name;
                if (typeform == "frmNotas")
                {
                    frmNotas fo = (frmNotas)formu;
                    if (fo.Proceso == Proceso)
                    {
                        fo.Activate();
                        fo.WindowState = FormWindowState.Maximized;
                        FormEncontrado = true;
                        return;
                    }
                    else
                    {
                        FormEncontrado = false;
                    }
                }
            }
        }

        private void biConsulta_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmNotas"] != null)
                {
                    BuscaFormulario(3);
                    if (!FormEncontrado)
                    {
                        frmNotas form1 = new frmNotas();
                        form1.MdiParent = this;
                        form1.Proceso = 3;
                        form1.Text += " - CONSULTA";
                        //form.ShowDialog();                
                        form1.Show();
                    }
                }
                else
                {
                    frmNotas form1 = new frmNotas();
                    form1.MdiParent = this;
                    form1.Proceso = 3;
                    form1.Text += " - CONSULTA";
                    //form.ShowDialog();                
                    form1.Show();
                }
           }
        }

        private void biModificar_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmNotas"] != null)
                {
                    BuscaFormulario(2);
                    if (!FormEncontrado)
                    {
                        frmNotas form = new frmNotas();
                        form.MdiParent = this;
                        form.Proceso = 2;
                        form.Text += " - MODIFICAR";
                        //form.ShowDialog();                
                        form.Show();
                    }
                }
                else
                {
                    frmNotas form2 = new frmNotas();
                    form2.MdiParent = this;
                    form2.Proceso = 2;
                    form2.Text += " - MODIFICAR";
                    //form.ShowDialog();                
                    form2.Show();
                }
           }
        }

        private void biEliminar_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmNotas"] != null)
                {
                    BuscaFormulario(4);
                    if (!FormEncontrado)
                    {
                        frmNotas form3 = new frmNotas();
                        form3.MdiParent = this;
                        form3.Proceso = 4;
                        form3.Text += " - ELIMINAR";
                        //form.ShowDialog();                
                        form3.Show();
                    }
                }
                else
                {
                    frmNotas form3 = new frmNotas();
                    form3.MdiParent = this;
                    form3.Proceso = 4;
                    form3.Text += " - ELIMINAR";
                    //form.ShowDialog();                
                    form3.Show();
                }
             }
        }

        private void biNotadeSalida_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmNotaSalida"] != null)
                {
                    Application.OpenForms["frmNotaSalida"].Activate();
                }
                else
                {
                    frmNotaSalida form = new frmNotaSalida();
                    form.MdiParent = this;
                    form.Proceso = 1;
                    form.Show();
                }
             }
        }

        private void biParametros_Click(object sender, EventArgs e)
        {
             if (tcvalida==1){
                if (Application.OpenForms["frmParametros"] != null)
                {
                    Application.OpenForms["frmParametros"].Activate();
                }
                else
                {
                    frmParametros form = new frmParametros();
                    form.MdiParent = this;                
                    form.Show();
                }
             }
        }

        private void biUsuarios_Click(object sender, EventArgs e)
        {   
            if (Application.OpenForms["frmUsuarios"] != null)
            {
                Application.OpenForms["frmUsuarios"].Activate();
            }
            else
            {
                frmUsuarios form = new frmUsuarios();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void biPedidoCompra_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotaIngreso"] != null)
                {
                    Application.OpenForms["frmNotaIngreso"].Activate();
                }
                else
                {
                    frmNotaIngreso form = new frmNotaIngreso();
                    form.MdiParent = this;

                    form.Text = "Compra Directa";
                    form.Proceso = 1;
                    form.txtTransaccion.Text = "FT";
                    form.txtTransaccion.ReadOnly = true;
                    KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    form.txtTransaccion_KeyPress(form.txtTransaccion, ee);
                    form.txtCodProv.Focus();
                    form.Show();
                    //form.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void biFormaPago_Click(object sender, EventArgs e)
        {
            
            if (Application.OpenForms["frmFormaPago"] != null)
            {
                Application.OpenForms["frmFormaPago"].Activate();
            }
            else
            {
                frmFormaPago form = new frmFormaPago();
                //form.MdiParent = this;
                form.ShowDialog();
            }
           
        }
        
        private void biPedidosPendientes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmPedidosPendientes"] != null)
            {
                Application.OpenForms["frmPedidosPendientes"].Activate();
            }
            else
            {
                frmPedidosPendientes form = new frmPedidosPendientes();
                form.MdiParent = this;
                //form.Proceso = 1;
                //KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                //form.txtDocRef.Focus();
                form.Show();
            }
        }

        private void biPedidoVenta_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){

                if (Application.OpenForms["frmPedido"] != null)
                {
                    Application.OpenForms["frmPedido"].Activate();
                }
                else
                {
                    frmPedido form = new frmPedido();
                    form.MdiParent = this;
                    form.Proceso = 1;                
                    form.txtDocRef.Focus();
                    form.Show();
                }
            }
        }

        private void biInventario_Click(object sender, EventArgs e)
        {
             if (tcvalida==1){
                if (Application.OpenForms["ReporteInventario"] != null)
                {
                    Application.OpenForms["ReporteInventario"].Activate();
                }
                else
                {
                    frmReporteInventario form = new frmReporteInventario();
                    form.MdiParent = this;
                    form.Show();
                }
             }
        }

        private void biBackup_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                saveFileDialog1.ShowDialog();
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (tcvalida==1){
            con.GeneraraBackup(saveFileDialog1.FileName);
            }
        }

        private void biImport_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
            openFileDialog1.ShowDialog();
            }      
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            con.ImportarBackup(openFileDialog1.FileName);
        }

        private void biTransferencia_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmTranferenciaDirecta"] != null)
                {
                    Application.OpenForms["frmTranferenciaDirecta"].Activate();
                }
                else
                {
                    frmTranferenciaDirecta form = new frmTranferenciaDirecta();
                    form.MdiParent = this;
                    form.Proceso = 1;
                    form.txtDocRef.Focus();
                    form.Show();
                }
           }
        }

        private void biCobros_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){

                if (Application.OpenForms["frmCobros"] != null)
                {
                    Application.OpenForms["frmCobros"].Activate();
                }
                else
                {
                    frmCobros form = new frmCobros();
                    form.MdiParent = this;
                    //form.Proceso = 1;
                    //KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    //form.txtDocRef.Focus();
                    form.Show();
                }
            }
        }

        private void biPagos_Click(object sender, EventArgs e)
        {
             if (tcvalida==1){
                if (Application.OpenForms["frmPagos"] != null)
                {
                    Application.OpenForms["frmPagos"].Activate();
                }
                else
                {
                    frmPagos form = new frmPagos();
                    form.MdiParent = this;
                    //form.Proceso = 1;
                    //KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    //form.txtDocRef.Focus();
                    form.Show();
                }
             }
        }

        private void biMetodoPago_Click(object sender, EventArgs e)
        {
        
            if (Application.OpenForms["frmMetodoPago"] != null)
            {
                Application.OpenForms["frmMetodoPago"].Activate();
            }
            else
            {
                frmMetodoPago form = new frmMetodoPago();
                //form.MdiParent = this;
                form.ShowDialog();
            }
           
        }

        private void biCotizacion_Click(object sender, EventArgs e)
        {
            
            
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmGestionCotizacion"] != null)
                {
                    Application.OpenForms["frmGestionCotizacion"].Activate();
                }
                else
                {
                    frmGestionCotizacion form = new frmGestionCotizacion();
                    form.MdiParent = this;
                    form.Proceso = 1;
                    form.txtDocRef.Text = "CT";
                    form.txtDocRef.ReadOnly = true;
                    KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    form.txtDocRef_KeyPress(form.txtDocRef, ee);
                    form.txtCodCliente.Focus();
                    form.Show();
                }
            }
        }        

        private void biCotizacionesVigentes_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmCotizacionesVigentes"] != null)
                {
                    Application.OpenForms["frmCotizacionesVigentes"].Activate();
                }
                else
                {
                    frmCotizacionesVigentes form = new frmCotizacionesVigentes();
                    form.MdiParent = this;
                    //form.Proceso = 1;
                    //KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    //form.txtDocRef.Focus();
                    form.Show();
                }
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmMenuReportes"] != null)
                {
                    Application.OpenForms["frmMenuReportes"].Activate();
                }
                else
                {
                    frmMenuReportes form = new frmMenuReportes();
                    //form.MdiParent = this;
                    //form.Proceso = 1;
                    //KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    //form.txtDocRef.Focus();
                    form.ShowDialog();
                }
            }
        }

        private void biClientes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmClientesCompletos"] != null)
            {
                Application.OpenForms["frmClientesCompletos"].Activate();
            }
            else
            {
                frmClientesCompletos form = new frmClientesCompletos();
                form.MdiParent = this;
                form.Tipo = 1;
                form.Show();
            }
             
        }

        private void biListasPrecios_Click(object sender, EventArgs e)
        {
             if (tcvalida == 1){
                 if (Application.OpenForms["frmGestionListaPrecios"] != null)
                {
                    Application.OpenForms["frmGestionListaPrecios"].Activate();
                }
                else
                {
                    frmGestionListaPrecios form = new frmGestionListaPrecios();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
             }
        }

        private void biVehiculosTransporte_Click(object sender, EventArgs e)
        {
       
            if (Application.OpenForms["frmVehiculosTransporte"] != null)
            {
                Application.OpenForms["frmVehiculosTransporte"].Activate();
            }
            else
            {
                frmVehiculoTransporte form = new frmVehiculoTransporte();
                //form.MdiParent = this;
                form.ShowDialog();
            }
           
        }

        private void biConductores_Click(object sender, EventArgs e)
        {
             
                if (Application.OpenForms["frmConductores"] != null)
                {
                    Application.OpenForms["frmConductores"].Activate();
                }
                else
                {
                    frmConductores form = new frmConductores();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
            
        }

        private void biEmpresasTransporte_Click(object sender, EventArgs e)
        {
           
                if (Application.OpenForms["frmEmpresaTransporte"] != null)
                {
                    Application.OpenForms["frmEmpresaTransporte"].Activate();
                }
                else
                {
                    frmEmpresaTransporte form = new frmEmpresaTransporte();
                    form.MdiParent = this;
                    form.Show();
                }
           
        }

        private void biGuia_Click(object sender, EventArgs e)
        {
           if (tcvalida==1){
                if (Application.OpenForms["frmGuiaRemision"] != null)
                {
                    Application.OpenForms["frmGuiaRemision"].Activate();
                }
                else
                {
                    frmGuiaRemision form1 = new frmGuiaRemision();
                    form1.MdiParent = this;
                    form1.Proceso = 1;
                    form1.Show();
                }
           }        
        }
            

        private void biZonas_Click(object sender, EventArgs e)
        {
            
                if (Application.OpenForms["frmZonas"] != null)
                {
                    Application.OpenForms["frmZonas"].Activate();
                }
                else
                {
                    frmZonas form = new frmZonas();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
           
        }

        private void biVendedores_Click(object sender, EventArgs e)
        {
           
                if (Application.OpenForms["frmVendedores"] != null)
                {
                    Application.OpenForms["frmVendedores"].Activate();
                }
                else
                {
                    frmVendedores form = new frmVendedores();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
            
        }

        private void biDestaques_Click(object sender, EventArgs e)
        {
             
                if (Application.OpenForms["frmDestaques"] != null)
                {
                    Application.OpenForms["frmDestaques"].Activate();
                }
                else
                {
                    frmDestaques form = new frmDestaques();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
            
        }

        private void btArqueo_Click(object sender, EventArgs e)
        {
             if (tcvalida==1){
                if (Application.OpenForms["frmArqueos"] != null)
                {
                    Application.OpenForms["frmArqueos"].Activate();
                }
                else
                {
                    frmArqueos form = new frmArqueos();
                    form.MdiParent = this;
                    //form.Proceso = 1;
                    form.Show();
                }
          }
        }

        

        private void biComisionVentas_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmCacularComision"] != null)
                {
                    Application.OpenForms["frmCacularComision"].Activate();
                }
                else
                {
                    frmCacularComision form = new frmCacularComision();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }       

        private void biComisionVentas_Click_1(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmComsionPorDocumento"] != null)
                {
                    Application.OpenForms["frmComsionPorDocumento"].Activate();
                }
                else
                {
                    frmComsionPorDocumento form = new frmComsionPorDocumento();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biGuias_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmGuiasRemision"] != null)
                {
                    Application.OpenForms["frmGuiasRemision"].Activate();
                }
                else
                {
                    frmGuiasRemision form = new frmGuiasRemision();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biAnular_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmNotas"] != null)
                {
                    BuscaFormulario(5);
                    if (!FormEncontrado)
                    {
                        frmNotas form3 = new frmNotas();
                        form3.MdiParent = this;
                        form3.Proceso = 5;
                        form3.Text += " - ANULAR";
                        //form.ShowDialog();                
                        form3.Show();
                    }
                }
                else
                {
                    frmNotas form3 = new frmNotas();
                    form3.MdiParent = this;
                    form3.Proceso = 5;
                    form3.Text += " - ANULAR";
                    //form.ShowDialog();                
                    form3.Show();
                }
            }
        }

        private void biNotaCredito_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotadeCredito"] != null)
                {
                    Application.OpenForms["frmNotadeCredito"].Activate();
                }
                else
                {
                    frmNotadeCredito form = new frmNotadeCredito();
                    form.MdiParent = this;
                    form.Proceso = 1;
                    form.Show();
                }
            }        
         }

        private void ciNotasdeCredito_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotasCredito"] != null)
                {
                    Application.OpenForms["frmNotasCredito"].Activate();
                }
                else
                {
                    frmNotasCredito form1 = new frmNotasCredito();
                    form1.MdiParent = this;
                    form1.Proceso = 1;
                    form1.Show();
                }
            }
        }

        private void biMuestraVentas_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmVentas"] != null)
                {
                    Application.OpenForms["frmVentas"].Activate();
                }
                else
                {
                    frmVentas form = new frmVentas();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biCatalogo_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmCatalogo"] != null)
            {
                Application.OpenForms["frmCatalogo"].Activate();
            }
            else
            {
                frmCatalogo form = new frmCatalogo();
                form.tc_hoy = tc_hoy;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void biKardex_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
            frmParamKardexArticulo form = new frmParamKardexArticulo();
           // form.criterio = 0; // (0) ARTICULO
            form.ShowDialog();
            }
        }

        private void biBancos_Click(object sender, EventArgs e)
        {
             
                if (Application.OpenForms["frmBancos"] != null)
                {
                    Application.OpenForms["frmBancos"].Activate();
                }
                else
                {
                    frmBancos form = new frmBancos();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
            
        }

        private void tabItem1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void tabStrip1_SelectedTabChanged(object sender, DevComponents.DotNetBar.TabStripTabChangedEventArgs e)
        {
            //foreach (DevComponents.DotNetBar.TabItem tabit in tabStrip1.Tabs)
            //{
            //    Form ventana = (Form)tabit.AttachedControl;
            //    ventana.WindowState = FormWindowState.Maximized;
            //}
            if (tabStrip1.SelectedTab != null)
            {
                Form ventana = (Form)tabStrip1.SelectedTab.AttachedControl;
                if (ventana.WindowState == FormWindowState.Maximized)
                {
                    ventana.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void mdi_Menu_MdiChildActivate(object sender, EventArgs e)
        {
            //if (this.ActiveMdiChild != null)
            //{
            //    this.ActiveMdiChild.WindowState =
            //    FormWindowState.Maximized;
            //}
        }

        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            //if (tcvalida==1){
            //    if (Application.OpenForms["frmTermometroVentas"] != null)
            //    {
            //        Application.OpenForms["frmTermometroVentas"].Activate();
            //    }
            //    else
            //    {
            //        frmTermometroVentas form = new frmTermometroVentas();
            //        form.MdiParent = this;
            //        form.Show();
            //    }
            //}
        }

        private void biBuscarGuia_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmBuscarGuias"] != null)
                {
                    Application.OpenForms["frmBuscarGuias"].Activate();
                }
                else
                {
                    frmBuscarGuias form = new frmBuscarGuias();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void btnRequerimiento_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1 && frmLogin.AcesosUsuario.Contains(Convert.ToInt32(btnRequerimiento.Tag)))
            {
                if (Application.OpenForms["frmRequerimiento"] != null)
                {
                    Application.OpenForms["frmRequerimiento"].Activate();
                }
                else
                {
                    frmRequerimiento form = new frmRequerimiento();
                    form.MdiParent = this;
                    form.txtSerie.Focus();
                    form.Procede = 10;
                    form.Proceso = 1;
                    form.Show();
                }
             }
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmRequerimientosVigentes"] != null)
                {
                    Application.OpenForms["frmRequerimientosVigentes"].Activate();
                }
                else
                {
                    frmRequerimientosVigentes form = new frmRequerimientosVigentes();
                    form.MdiParent = this;
                    form.tipo = 1;
                    form.Show();
                }
            }
        }

        private void biOrdenCompra_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmOrdenCompras"] != null)
                {
                    Application.OpenForms["frmOrdenCompras"].Activate();
                }
                else
                {
                    frmOrdenCompras form = new frmOrdenCompras();
                    form.MdiParent = this;
                    form.Dock = DockStyle.Fill;
                    form.Proceso = 1;
                    form.Show();
                }
            }
        }

        private void biOrdenesCompras_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmOrdenCompraVigentes"] != null)
                {
                    Application.OpenForms["frmOrdenCompraVigentes"].Activate();
                }
                else
                {
                    frmOrdenCompraVigentes form = new frmOrdenCompraVigentes();
                    form.MdiParent = this;
                    form.Proce = 0;
                    form.btnAceptar.Visible = false;
                    form.btnEnviar.Visible = true;
                    form.Dock = DockStyle.Fill;
                    form.btnrechazar.Visible = false;
                    form.btnTerminada.Visible = false;
                    form.button1.Visible = false;
                    form.btnVistaSucursales.Visible = false;
                    form.btnAnular.Visible = true;
                    form.Show();
                }
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmNotaIngresoPorOrden"] != null)
                {
                    Application.OpenForms["frmNotaIngresoPorOrden"].Activate();
                }
                else
                {
                    frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
                    form.MdiParent = this;
                    form.txtOrdenCompra.Focus();
                    form.Dock = DockStyle.Fill;
                    form.Proceso = 1;
                    form.Show();
                    //form.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void BiMoneda_Click(object sender, EventArgs e)
        {
             if (tcvalida == 1){
                if (Application.OpenForms["frmMoneda"] != null)
                {
                    Application.OpenForms["frmMoneda"].Activate();
                }
                else
                {
                    frmMoneda form = new frmMoneda();
                    form.MdiParent = this;
                    form.Show();
                }
             }
        }

        private void biNotasOrden_Click(object sender, EventArgs e)
        {
            if (tcvalida==1){
                if (Application.OpenForms["frmNotasOrden"] != null)
                {
                    BuscaFormulario(3);
                    if (!FormEncontrado)
                    {
                        frmNotasOrden form1 = new frmNotasOrden();
                        form1.MdiParent = this;
                        form1.Proceso = 3;
                        form1.Text += " - CONSULTA";
                        //form.ShowDialog();                
                        form1.Show();
                    }
                }
                else
                {
                    frmNotasOrden form1 = new frmNotasOrden();
                    form1.MdiParent = this;
                    form1.Proceso = 3;
                    form1.Text += " - CONSULTA";
                    //form.ShowDialog();                
                    form1.Show();
                }
           }
        }

        private void biSucursal_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmSucursales"] != null)
            {
                Application.OpenForms["frmSucursales"].Activate();
            }
            else
            {
                frmSucursales form = new frmSucursales();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void biTransferenciasPendientes_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmTransferencias"] != null)
                {
                    Application.OpenForms["frmTransferencias"].Activate();
                }
                else
                {
                    frmTransferencias form = new frmTransferencias();
                    form.MdiParent = this;
                    form.tipo = 1;
                    form.Show();
                }
            }
        }

        private void biHistorialRequerimiento_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmOrdenesVigentes"] != null)
                {
                    Application.OpenForms["frmOrdenesVigentes"].Activate();
                }
                else
                {
                    frmRequerimientosVigentes form = new frmRequerimientosVigentes();
                    form.MdiParent = this;
                    form.tipo = 2;
                    form.Show();
                }
            }
        }

        private void biIngresos_Click(object sender, EventArgs e)
        {
             if (tcvalida == 1)
            {
                if (Application.OpenForms["frmTesoreria"] != null)
                {
                    Application.OpenForms["frmTesoreria"].Activate();
                }
                else
                {
                    frmTesoreria form = new frmTesoreria();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biCajaChica_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmCajaChica"] != null)
                {
                    Application.OpenForms["frmCajaChica"].Activate();
                }
                else
                {
                    frmCajaChica form = new frmCajaChica();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void BiAperturaCaja_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmAperturaCaja"] != null)
                {
                    Application.OpenForms["frmAperturaCaja"].Activate();
                }
                else
                {
                    frmAperturaCaja form = new frmAperturaCaja();
                    form.ShowDialog();
                }
            }
        }

        private void BiCaja_Click(object sender, EventArgs e)
        {

            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmCaja"] != null)
                {
                    Application.OpenForms["frmCaja"].Activate();
                }
                else
                {
                    frmCaja form = new frmCaja();
                    form.ShowDialog();
                }
            }
        }

        private void biMovimientosCaja_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                try
                {
                    aper = AdmAper.ValidarAperturaCierre(frmLogin.iCodAlmacen);
                    if (aper != null)
                    {
                        if (aper.FechaRegistro.Date == DateTime.Now.Date)
                        {
                            if (Application.OpenForms["frmFlujoCaja"] != null)
                            {
                                Application.OpenForms["frmFlujoCaja"].Activate();
                            }
                            else
                            {
                                frmFlujoCaja form = new frmFlujoCaja();
                                form.MdiParent = this;
                                form.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Debe Aperturar Caja", "Apertura Caja", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe Aperturar Caja", "Apertura Caja", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:  " + ex.Message);
                }
            }
        }

        private void biMovimientosBancarios_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmMovimientos"] != null)
                {
                    Application.OpenForms["frmMovimientos"].Activate();
                }
                else
                {
                    frmMovimientos form = new frmMovimientos();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void BiHistorialOrdenes_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmOrdenCompraVigentes"] != null)
                {
                    Application.OpenForms["frmOrdenCompraVigentes"].Activate();
                }
                else
                {
                    frmOrdenCompraVigentes form = new frmOrdenCompraVigentes();
                    form.MdiParent = this;
                    form.Proce = 2;
                    form.btnAceptar.Visible = false;
                    form.btnEnviar.Visible = false;
                    form.btnrechazar.Visible = true;
                    form.btnTerminada.Visible = true;
                    form.btnAnular.Visible = false;
                    form.Show();
                }
            }
        }

        private void biHistorialFacturaciones_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmFacturacionesVigentes"] != null)
                {
                    Application.OpenForms["frmFacturacionesVigentes"].Activate();
                }
                else
                {
                    frmFacturacionesVigentes form = new frmFacturacionesVigentes();
                    form.MdiParent = this;
                    form.Dock = DockStyle.Fill;
                    form.Proceso = 1;
                    form.Show();
                }
            }
        }

        private void biCotizacionesAprobadas_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmCotizacionesAprobadas"] != null)
                {
                    Application.OpenForms["frmCotizacionesAprobadas"].Activate();
                }
                else
                {
                    frmCotizacionesAprobadas form = new frmCotizacionesAprobadas();
                    form.MdiParent = this;
                    //form.Proceso = 1;
                    //KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    //form.txtDocRef.Focus();
                    form.Show();
                }
            }
        }

        private void biConsolidado_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmConsolidado"] != null)
                {
                    Application.OpenForms["frmConsolidado"].Activate();
                }
                else
                {
                    frmConsolidado form = new frmConsolidado();
                    form.MdiParent = this;
                    form.proceso = 3;
                    form.Show();
                }
            }
        }

        private void biCuentasCorrientes_Click(object sender, EventArgs e)
        {
            
                if (Application.OpenForms["frmCuentasCte"] != null)
                {
                    Application.OpenForms["frmCuentasCte"].Activate();
                }
                else
                {
                    frmCuentasCte form = new frmCuentasCte();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
            
        }

        private void biTarjetaPago_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmTarjetasPago"] != null)
                {
                    Application.OpenForms["frmTarjetasPago"].Activate();
                }
                else
                {
                    frmTarjetasPago form = new frmTarjetasPago();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
            }
        }

        private void biVigenciaCotizaciones_Click(object sender, EventArgs e)
        {
          
            if (Application.OpenForms["frmVigenciaCotizacion"] != null)
            {
                Application.OpenForms["frmVigenciaCotizacion"].Activate();
            }
            else
            {
                frmVigenciaCotizacion form = new frmVigenciaCotizacion();
                form.MdiParent = this;
                form.Show();
            }
          
        }

        private void biGuiasSinFacturar_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotaOrdenAlmacen"] != null)
                {
                    Application.OpenForms["frmNotaOrdenAlmacen"].Activate();
                }
                else
                {
                    frmNotaOrdenAlmacen form = new frmNotaOrdenAlmacen();
                    form.proceso = 1;
                    form.MdiParent = this;
                    form.Show();
                }
            }
             
        }

        private void rtAdministrador_Click(object sender, EventArgs e)
        {

        }

        private void biStockAlmacenes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmStockAlmacenes"] != null)
            {
                Application.OpenForms["frmStockAlmacenes"].Activate();
            }
            else
            {
                frmStockAlmacenes form = new frmStockAlmacenes();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void biParametros_Click_1(object sender, EventArgs e)
        {
            if (frmLogin.AcesosUsuario.Contains(Convert.ToInt32(biParametros.Tag)))
            {
                if (Application.OpenForms["frmParametros"] != null)
                {
                    Application.OpenForms["frmParametros"].Activate();
                }
                else
                {
                    frmParametros form = new frmParametros();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void btnNotaDebitoC_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotadeDebitoCompra"] != null)
                {
                    Application.OpenForms["frmNotadeDebitoCompra"].Activate();
                }
                else
                {
                    frmNotadeDebitoCompra form = new frmNotadeDebitoCompra();
                    form.Proceso = 1;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {

        }

        private void biNotaCreditoCompra_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotadeCreditoCompra"] != null)
                {
                    Application.OpenForms["frmNotadeCreditoCompra"].Activate();
                }
                else
                {
                    frmNotadeCreditoCompra form = new frmNotadeCreditoCompra();
                    form.Proceso = 1;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biNotasCreditoCompras_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotasCreditoCompras"] != null)
                {
                    Application.OpenForms["frmNotasCreditoCompras"].Activate();
                }
                else
                {
                    frmNotasCreditoCompras form = new frmNotasCreditoCompras();
                    form.Proceso = 1;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

       

        private void biTipoEgresoCaja_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmGestionTipoEgreso"] != null)
                {
                    Application.OpenForms["frmGestionTipoEgreso"].Activate();
                }
                else
                {
                    frmGestionTipoEgreso form = new frmGestionTipoEgreso();
                    //form.MdiParent = this;
                    form.ShowDialog();
                }
            }
        }

        private void biRegistraChequeCaja_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmRegistroChequeCaja"] != null)
                {
                    Application.OpenForms["frmRegistroChequeCaja"].Activate();
                }
                else
                {
                    frmRegistroChequeCaja form = new frmRegistroChequeCaja();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void BiAprobacionPago_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmAprobacionPagos"] != null)
                {
                    Application.OpenForms["frmAprobacionPagos"].Activate();
                }
                else
                {
                    frmAprobacionPagos form = new frmAprobacionPagos();
                    // form.Proceso = 1;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biNotaDebito_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotadeDebito"] != null)
                {
                    Application.OpenForms["frmNotadeDebito"].Activate();
                }
                else
                {
                    frmNotadeDebito form1 = new frmNotadeDebito();
                    form1.MdiParent = this;
                    form1.Proceso = 1;
                    form1.Show();
                }
            }
        }

        private void ciNotasdeDebito_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotasDebitoVentas"] != null)
                {
                    Application.OpenForms["frmNotasDebitoVentas"].Activate();
                }
                else
                {
                    frmNotasDebitoVentas form1 = new frmNotasDebitoVentas();
                    form1.MdiParent = this;
                    form1.Proceso = 1;
                    form1.Show();
                }
            }
        }

        private void biAnularPago_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmTesoreriaAnuPag"] != null)
                {
                    Application.OpenForms["frmTesoreriaAnuPag"].Activate();
                }
                else
                {
                    frmTesoreriaAnuPag form = new frmTesoreriaAnuPag();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biIngresoCamara_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmIngresoCamaras"] != null && frmLogin.AcesosUsuario.Contains(Convert.ToInt32(biIngresoCamara.Tag)))
                {
                    Application.OpenForms["frmIngresoCamaras"].Activate();
                }
                else
                {
                    frmIngresoCamaras form = new frmIngresoCamaras();
                    form.MdiParent = this;
              //      form.Dock = DockStyle.Fill;
                    form.Proceso = 1;
                    form.Show();
                }
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmBahia"] != null)
            {
                Application.OpenForms["frmBahia"].Activate();
            }
            else
            {
                frmBahia form = new frmBahia();
                //form.MdiParent = this;
                form.ShowDialog();
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmEspecie"] != null)
            {
                Application.OpenForms["frmEspecie"].Activate();
            }
            else
            {
                frmEspecie form = new frmEspecie();
                //form.MdiParent = this;
                form.ShowDialog();
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmIngresosCamara"] != null)
                {
                    Application.OpenForms["frmIngresosCamara"].Activate();
                }
                else
                {
                    frmIngresosCamara form = new frmIngresosCamara();
                    form.MdiParent = this;
                    form.Proceso = 1;
                    form.Show();
                }
            }
        }

        private void tabStrip1_TabItemOpen(object sender, EventArgs e)
        {
            //foreach (DevComponents.DotNetBar.TabItem tabit in tabStrip1.Tabs)
            //{
            //    Form ventana = (Form)tabit.AttachedControl;
            //    ventana.WindowState = FormWindowState.Maximized;
            //}
            if (tabStrip1.SelectedTab != null)
            {
                Form ventana = (Form)tabStrip1.SelectedTab.AttachedControl;
                if (ventana.WindowState == FormWindowState.Maximized)
                {
                    ventana.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {

                if (Application.OpenForms["frmNotaIngreso"] != null)
                {
                    Application.OpenForms["frmNotaIngreso"].Activate();
                }
                else
                {                   
                    frmNotaIngreso form = new frmNotaIngreso();
                    form.MdiParent = this;
                    form.Text = "Ingreso Materia Prima";
                 //   form.Dock = DockStyle.Fill;
                    form.Proceso = 1;
                    form.CodTransaccion = 22; // INGRESO DE MATERIA PRIMA
                    form.txtTransaccion.Text = admTrans.MuestraTransaccion(22).Sigla;
                    form.txtTransaccion.ReadOnly = true;
                    KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    form.txtTransaccion_KeyPress(null, ee);
                    form.dtpFecha.Focus();
                    form.Show();
                }
            }
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmRecepcionCamaras"] != null)
                {
                    Application.OpenForms["frmRecepcionCamaras"].Activate();
                }
                else
                {
                    frmRecepcionCamaras form = new frmRecepcionCamaras();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void buttonItem11_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmConfigurarMateria"] != null)
                {
                    Application.OpenForms["frmConfigurarMateria"].Activate();
                }
                else
                {
                    frmConfigurarMateria form = new frmConfigurarMateria();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmLoteProduccion"] != null)
                {
                    Application.OpenForms["frmLoteProduccion"].Activate();
                }
                else
                {
                    frmLoteProduccion form = new frmLoteProduccion();
                  //  form.Dock = DockStyle.Fill;
                    form.Proceso = 1;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void buttonItem12_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmLotes"] != null)
                {
                    Application.OpenForms["frmLotes"].Activate();
                }
                else
                {
                    frmLotes form = new frmLotes();
               //     form.Dock = DockStyle.Fill;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biPesador_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmPesadores"] != null)
                {
                    Application.OpenForms["frmPesadores"].Activate();
                }
                else
                {
                    frmPesadores form = new frmPesadores();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void biAreas_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmarea"] != null)
                {
                    Application.OpenForms["frmarea"].Activate();
                }
                else
                {
                    frmarea form = new frmarea();
                    form.Param = 1;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void rtVentas_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem41_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotaIngresoPorOrden"] != null)
                {
                    Application.OpenForms["frmNotaIngresoPorOrden"].Activate();
                }
                else
                {
                    frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
                    form.MdiParent = this;
                    form.txtOrdenCompra.Focus();
                    form.Dock = DockStyle.Fill;
                    form.Text = "Ingreso Por Liquidacion De Compra";
                    form.TitleText = "Ingreso Por Liquidacion De Compra"; ;
                    form.Proceso = 7;
                    form.Show();
                    //form.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void buttonItem45_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmFacturacionesVigentes"] != null)
                {
                    Application.OpenForms["frmFacturacionesVigentes"].Activate();
                }
                else
                {
                    frmFacturacionesVigentes form = new frmFacturacionesVigentes();
                    form.MdiParent = this;
                    form.Proceso = 2;
                    form.Show();
                }
            }
        }

        private void buttonItem46_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmStockProducto"] != null)
                {
                    Application.OpenForms["frmStockProducto"].Activate();
                }
                else
                {
                    frmStockProducto form = new frmStockProducto();
                    form.MdiParent = this;
           
                    form.Show();
                }
            }
        }

        private void buttonItem47_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmNotaOrdenAlmacen"] != null)
            {
                Application.OpenForms["frmNotaOrdenAlmacen"].Activate();
            }
            else
            {
                frmNotaOrdenAlmacen form = new frmNotaOrdenAlmacen();
                form.proceso = 1;
                form.MdiParent = this;
                form.Show();
            }
        }

        private void buttonItem49_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmNotaSalida"] != null)
                {
                    Application.OpenForms["frmNotaSalida"].Activate();
                }
                else
                {
                    frmNotaSalida form = new frmNotaSalida();
                    form.MdiParent = this;
                    form.Text = "Salida a Producción";
                    //   form.Dock = DockStyle.Fill;
                    form.Proceso = 1;
                    form.CodTransaccion = 23; // SALIDA PARA PRODUCCION
                    form.txtTransaccion.Text = admTrans.MuestraTransaccion(23).Sigla;
                    form.txtTransaccion.ReadOnly = true;
                    KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                    form.txtTransaccion_KeyPress(null, ee);
                    form.dtpFecha.Focus();
                    form.Show();
                }
            }


        }

        private void biVentaRapida_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem48_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem48_Click_1(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmCostos"] != null)
                {
                    Application.OpenForms["frmCostos"].Activate();
                }
                else
                {
                    frmCostos form = new frmCostos();
                    form.MdiParent = this;
                    form.Dock = DockStyle.Fill;

                    form.Show();
                }
            }
        }

        private void buttonItem50_Click(object sender, EventArgs e)
        {
            frmCompras form1 = new frmCompras();
            form1.MdiParent = this;
        //    form1.Dock = DockStyle.Fill;
          //  form1.Proceso = 3;
            form1.Text = "COMPRAS";               
            form1.Show();
        }

        private void buttonItem51_Click(object sender, EventArgs e)
        {

        }

        private void Cen_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {


                if (Application.OpenForms["frmCentroCostos"] != null)
                {
                    Application.OpenForms["frmCentroCostos"].Activate();
                }
                else
                {
                    frmCentroCostos form = new frmCentroCostos();
                    form.Proceso = 1;
               //     form.Param = 0;
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void buttonItem52_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmRptCentroCostos"] != null)
            {
                Application.OpenForms["frmRptCentroCostos"].Activate();
            }
            else
            {
                frmRptCentroCostos form = new frmRptCentroCostos();
          //      form.MdiParent = this;
                form.ShowDialog();
            }
        }

        private void buttonItem51_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonItem53_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmRptCentroCostos2"] != null)
            {
                Application.OpenForms["frmRptCentroCostos2"].Activate();
            }
            else
            {
                frmRptCentroCostos2 form = new frmRptCentroCostos2();
                //      form.MdiParent = this;
                form.ShowDialog();
            }
        }

        private void buttonItem56_Click(object sender, EventArgs e)
        {
            if (tcvalida == 1)
            {
                if (Application.OpenForms["frmCostos"] != null)
                {
                    Application.OpenForms["frmCostos"].Activate();
                }
                else
                {
                    frmCostos form = new frmCostos();
                    form.MdiParent = this;
          //          form.Dock = DockStyle.Fill;

                    form.Show();
                }
            }
        }

        private void buttonItem11_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonItem54_Click(object sender, EventArgs e)
        {
            frmIngresoProd form1 = new frmIngresoProd();
            form1.MdiParent = this;
            //    form1.Dock = DockStyle.Fill;
            //  form1.Proceso = 3;
            form1.Text = "INGRESOS A PRODUCCIÓN";
            form1.Show();
        }

        private void buttonItem48_Click_2(object sender, EventArgs e)
        {
            frmSalidasProd form1 = new frmSalidasProd();
            form1.MdiParent = this;
            //    form1.Dock = DockStyle.Fill;
            //  form1.Proceso = 3;
            form1.Text = "SALIDAS A PRODUCCIÓN";
            form1.Show();
        }

        private void buttonItem55_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmParamIngMatprima"] != null)
            {
                Application.OpenForms["frmParamIngMatprima"].Activate();
            }
            else
            {
                frmParamIngMatprima form = new frmParamIngMatprima();
                //      form.MdiParent = this;
                form.ShowDialog();
            }
        }
    }
}
