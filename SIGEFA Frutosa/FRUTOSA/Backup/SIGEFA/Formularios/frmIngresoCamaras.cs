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

using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;

namespace SIGEFA.Formularios
{
    public partial class frmIngresoCamaras : DevComponents.DotNetBar.OfficeForm
    {
        //RAGA
        clsAdmProveedor AdmProv = new clsAdmProveedor();
        public Int32 CodProveedor;
        clsAdmEspecie admEspecie = new clsAdmEspecie();
       
        clsValidar ok = new clsValidar();
        public Int32 CodDocumento;
        clsTipoDocumento doc = new clsTipoDocumento();
        List<clsDetalleIngresoCamara> detalle = new List<clsDetalleIngresoCamara>();
        clsProveedor prov = new clsProveedor();

        clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

        clsAdmPesador admpesa = new clsAdmPesador();
        clsIngresoCamara ingreso = new clsIngresoCamara();
        clsAdmIngresoCamara Admingre = new clsAdmIngresoCamara();
        clsAdmBahia Admbahia = new clsAdmBahia();
        clsAdmEspecie Admespecie = new clsAdmEspecie();
        clsAdmEmpresa Admempre = new clsAdmEmpresa();
        clsAdmZona Admzona = new clsAdmZona();
        clsConsultasExternas ext = new clsConsultasExternas();
        public Int32 CodIngresoCamara = 0;
        clsValidar valida = new clsValidar();
        private FilterInfoCollection devices;
        private VideoCaptureDevice fuentes;
        ComboBox combo = new ComboBox();
        clsAdmProveedorGarita Admpro = new clsAdmProveedorGarita();
        clsAdmVehiculoTransporte AdmVeh = new clsAdmVehiculoTransporte();
        clsVehiculoTransporte entveh = new clsVehiculoTransporte();
        clsAdmConductor AdmCond = new clsAdmConductor();
        clsConductor entcond = new clsConductor();
        
        public Int32 Proceso = 0; //(1) Nuevo (2) Editar (3) Consulta
        public Int32 codIngMatPrima;
        public double stockactual;





        public frmIngresoCamaras()
        {
            InitializeComponent();
        }        

        private void btnFotoCamion_Click(object sender, EventArgs e)
        {
            if (btnFotoCamion.Text == "Subir Foto")
            {
                otorgarimagen(label17, openFileDialog1);
            }
            else 
            {
                tomafoto(label17, vspfoto);
                btnFotoCamion.Text = "Subir Foto";
                btnCapturaCamion.Enabled = true;
                ApagarCamara(vspfoto);
            }
        }

        private void CargarProveedor() {
            cboProveedor.DataSource = Admpro.ListaProveedorGarita();
            cboProveedor.ValueMember = "codProveedorGarita";
            cboProveedor.DisplayMember = "razonsocial";
            cboProveedor.SelectedIndex = -1;
        }

        private void CargarConductor() {
            cboConductor.DataSource = AdmCond.MuestraConductores();
            cboConductor.ValueMember = "codConductor";
            cboConductor.DisplayMember = "nombre";
            cboConductor.SelectedIndex = -1;
        }

        private void CargarVehiTransporte() {
            cboVehiculoTrans.DataSource = AdmVeh.MuestraVehiculoTransportes();
            cboVehiculoTrans.ValueMember = "cod";
            cboVehiculoTrans.DisplayMember = "placa";
            cboVehiculoTrans.SelectedIndex = -1;
        }

        private void tomafoto(Label imagen, AForge.Controls.VideoSourcePlayer camara)
        {
            Image img = camara.GetCurrentVideoFrame();
            imageList2.Images.Add(img);
            var pos = imageList2.Images.Count -1;
            imagen.ImageList = imageList2;
            imagen.ImageIndex = pos;            
            imagen.Visible = true;
            img.Dispose();
        }
        private void btnFotoConductor_Click(object sender, EventArgs e)
        {
            if (btnFotoConductor.Text == "Subir Foto")
            {
                otorgarimagen(label18, openFileDialog1);
            }
            else
            {
                tomafoto(label18, vspfoto2);
                btnCapturaConductor.Enabled = true;
                btnFotoConductor.Text = "Subir Foto";
                ApagarCamara(vspfoto2);
            }
        }        

        private void otorgarimagen(Label lab,OpenFileDialog file)
        {
            file.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
            file.FileName = "";
            file.Title = "Carga foto";
            file.InitialDirectory = "C:\\";
            if (file.ShowDialog() == DialogResult.OK)
            {
                String Direccion = file.FileName;
                String key = file.SafeFileName;
                imageList2.Images.Add(key, Image.FromFile(Direccion));
                var pos = imageList2.Images.IndexOfKey(key);
                lab.ImageList = imageList2;
                lab.ImageIndex = pos;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {



            if (superValidator1.Validate()== true && dgvDetalle.Rows.Count > 0)
            {

                ingreso.SNumDoc = textBox1.Text + "-" + textBox2.Text;
                ingreso.ITipoDocumento = CodDocumento;
                ingreso.FechaIngreso = dtpFecha.Value;
                ingreso.ICodProveedor = CodProveedor;
                ingreso.CodPesador = Convert.ToInt32(cboPesador.SelectedValue);
                ingreso.CodProcedencia = Convert.ToInt32(cmbProcedencia.SelectedValue);
                ingreso.Comentario = txtComentario.Text;
                ingreso.CodUser = frmLogin.iCodUser;
                ingreso.CodEspecie = Int32.Parse(comboBox1.SelectedValue.ToString());
                ingreso.DtHora = txthoraingreso.Value;


                if (Proceso == 1)
                {
                    if (Admingre.insertContenedor(ingreso))
                    {

                        RecorreDetalle();
                        if (detalle.Count > 0)
                        {
                            foreach (clsDetalleIngresoCamara det in detalle)
                            {
                                Admingre.insertDetalleContenedor(det);
                            }
                        }


                        MessageBox.Show("Los datos se guardaron correctamente", "Ingreso Camara", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CodIngresoCamara = ingreso.CodIngresoCamara;
                        Proceso = 0;
                        CargaIngresoCamara();
                        sololectura();
                        button5.Visible = true;
                        btnEliminar.Enabled = false;

                    }
                }


            }


            else if (Proceso == 2)
            {
                if (Admingre.update(ingreso))
                {
                    MessageBox.Show("Los datos se guardaron correctamente", "Ingreso Camara", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodIngresoCamara = ingreso.CodIngresoCamara;
                    Proceso = 0;
                    CargaIngresoCamara();
                    sololectura();
                    Close();
                }
            }

            else
            {
                MessageBox.Show("Debe completar todos los campos requeridos(*)", "Ingreso Camara", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }


        private void añadedetalle(DataGridViewRow fila)
        {
            clsDetalleIngresoCamara deta1 = new  clsDetalleIngresoCamara();


            deta1.ICodProductor = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
            deta1.ICodIngresoCamara = ingreso.CodIngresoCamara;
            deta1.ICodUnidIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
            deta1.ICantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
            deta1.DtFechaIngreso = dtpFecha.Value;
            deta1.ICodUser = ingreso.CodUser;
            detalle.Add(deta1);

        }


        private void CargaIngresoCamara()
        {
            try
            {
                ingreso = Admingre.CargaIngresoContenedor(CodIngresoCamara);
                if (ingreso != null)
                {
                    CodProveedor = ingreso.ICodProveedor;
                    CargaProveedor();
                    txtDocRef.Text = ingreso.SSigla;
                    textBox3.Text = ingreso.SNumDoc;

                    txtNumDoc.Text = ingreso.CodIngresoCamara.ToString();
                    dtpFecha.Value = ingreso.FechaIngreso;
                    txtComentario.Text = ingreso.Comentario;
                    cboPesador.SelectedValue = ingreso.CodPesador;
                    cmbProcedencia.SelectedValue = ingreso.CodProcedencia;

                    CantDev.Visible = true;
                    CantEntregada.Visible = true;
                    button5.Visible = true;
                    CodIngresoCamara = ingreso.CodIngresoCamara;
                    comboBox1.SelectedValue = ingreso.CodEspecie;
             
                    txthoraingreso.Value = ingreso.DtHora;
                    dgvDetalle.DataSource = Admingre.ListaDetalleIngresoContenedor(ingreso.CodIngresoCamara);
                   

                }
                else
                {
                    MessageBox.Show("El documento solicitado no existe", "Ingreso Camara", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void sololectura()
        {
            ext.sololectura(groupBox1.Controls);
            ext.sololectura(groupBox2.Controls);
            ext.sololectura(groupBox3.Controls);
            btnGuardar.Enabled = false;
            btnNuevoIngreso.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = true;
            btnEliminar.Visible = false;
        }

        private void customValidator1_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.Enabled)
                if (Proceso != 0)
                    if (c.SelectedIndex != -1)
                    {
                        e.IsValid = true;
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ControlToValidate.Focus();
                    }                       
                else
                    e.IsValid = true;
            else
                e.IsValid = true;
        }

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }

        private void customValidator3_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }

        private void customValidator4_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;  
        }

        private void customValidator5_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;  
        }

        private void customValidator6_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.Enabled)
                if (Proceso != 0)
                    if (c.SelectedIndex != -1)
                    {
                        e.IsValid = true;
                    }
                    else
                    {
                        e.IsValid = false;
                        e.ControlToValidate.Focus();
                    }
                else
                    e.IsValid = true;
            else
                e.IsValid = true;
        }

        private void customValidator7_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }

        private void customValidator8_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }

        private void customValidator9_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }

        private void customValidator10_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }

        private void customValidator11_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }



        public void CargaEspecies()
        {
            comboBox1.DataSource = admEspecie.CargaEspecies();
            comboBox1.DisplayMember = "nombre";
            comboBox1.ValueMember = "codEspecie";
            comboBox1.SelectedIndex = -1;
        }


    






        private void frmIngresoCamaras_Load(object sender, EventArgs e)
        {
           

           // CargaEmpresas();
           // CargaBahias();
            CargaProcedencias();
          //  CargaEspecies();
          //  CargarConductor();
            CargarProveedor();
          //  CargarVehiTransporte();
            CargarPesador();
            CargaEspecies();

            if (Proceso == 1)
            {   
                
            }
            else if (Proceso == 2)
            {
                CargaIngresoCamara();
            }
            else if (Proceso == 3)
            {
                CargaIngresoCamara();
                sololectura();
            }
        }


        //RAGA
        /*
        private void BuscaProveedor()
        {
            prov = AdmProv.MuestraProveedor(CodProveedor);
            if (prov != null)
            {
                txtCodProv.Text = prov.Ruc;
                txtNombreProv.Text = prov.RazonSocial;
                CodProveedor = prov.CodProveedor;
              //  return true;
            }
            else
            {
                txtNombreProv.Text = "";
                CodProveedor = 0;
            //    return false;
            }
        }
        */


        private void CargaCamara(AForge.Controls.VideoSourcePlayer camara)
        {
            devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            combo.Items.Clear();
            foreach (FilterInfo x in devices)
            {
                combo.Items.Add(x.Name);
            }
            if (combo.Items.Count >= 1)
            {
                combo.SelectedIndex = 0;
                IniciarCamara(camara);
            }
        }

        private void IniciarCamara(AForge.Controls.VideoSourcePlayer camara)
        {
            try
            {
                fuentes = new VideoCaptureDevice(devices[combo.SelectedIndex].MonikerString);
                camara.VideoSource = fuentes;
                camara.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un problema con su Camara Web. " + ex.Message, "Utilidad de Camara Web", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApagarCamara(AForge.Controls.VideoSourcePlayer camara)
        {
            try
            {
                camara.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un problema con su Camara Web. " + ex.Message, "Utilidad de Camara Web", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargaEmpresas()
        {
            cmbEmpresa.DataSource = Admempre.CargaEmpresas();
            cmbEmpresa.DisplayMember = "razonsocial";
            cmbEmpresa.ValueMember = "codEmpresa";
            cmbEmpresa.SelectedIndex = -1;
        }

      

        private void CargaBahias()
        {
            cmbBahia.DataSource = Admbahia.CargaBahias();
            cmbBahia.DisplayMember = "nombre";
            cmbBahia.ValueMember = "codBahia";
            cmbBahia.SelectedIndex = -1;
        }

        private void CargaProcedencias()
        {            
            cmbProcedencia.DataSource = Admzona.MuestraZonas();
            cmbProcedencia.DisplayMember = "descripcion";
            cmbProcedencia.ValueMember = "codZona";
            cmbProcedencia.SelectedIndex = -1;
        
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCapturaCamion_Click(object sender, EventArgs e)
        {
            label17.Visible = false;
            btnCapturaCamion.Enabled = false;
            btnFotoCamion.Text = "Capturar";            
            CargaCamara(vspfoto);
        }

        private void btnCapturaConductor_Click(object sender, EventArgs e)
        {
            label18.Visible = false;
            btnCapturaConductor.Enabled = false;
            btnFotoConductor.Text = "Capturar";
            CargaCamara(vspfoto2);
        }

        private void btnNuevoIngreso_Click(object sender, EventArgs e)
        {
            frmIngresoCamaras form2 = new frmIngresoCamaras();
            form2.MdiParent = this.MdiParent;
            form2.Proceso = 1;
            form2.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmProveedorGarita"] != null)
            {
                Application.OpenForms["frmProveedorGarita"].Activate();
            }
            else
            {
                frmProveedorGarita form = new frmProveedorGarita();
                form.ShowDialog();
                CargarProveedor();
            }
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            valida.enteros(e);
        }

        private void cboVehiculoTrans_SelectionChangeCommitted(object sender, EventArgs e)
        {
            entveh = AdmVeh.MuestraVehiculoTransporte(Convert.ToInt32(cboVehiculoTrans.SelectedValue));
            if (entveh != null)
            {
                txtMarca.Text = entveh.Marca;
                label17.Image = entveh.FotoCamion;
            }
        }

        private void cboConductor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            entcond = AdmCond.MuestraConductor(Convert.ToInt32(cboConductor.SelectedValue));
            if (entcond != null)
            {
                txtDni.Text = entcond.Dni;
                txtLicencia.Text = entcond.Licencia;
                label18.Image = entcond.FotoConductor;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmVehiculosTransporte"] != null)
            {
                Application.OpenForms["frmVehiculosTransporte"].Activate();
            }
            else
            {
                frmVehiculoTransporte form = new frmVehiculoTransporte();
                form.ShowDialog();
                CargarVehiTransporte();
                txtMarca.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmConductores"] != null)
            {
                Application.OpenForms["frmConductores"].Activate();
            }
            else
            {
                frmConductores form = new frmConductores();
                form.ShowDialog();
                CargarConductor();
                txtDni.Text = "";
                txtLicencia.Text = "";
            }
        }


        private void txtCajas_KeyPress(object sender, KeyPressEventArgs e)
        {
            valida.enteros(e);
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            valida.NumerosDecimales(e, txtPeso);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmDetalleGuia"] != null)
            {
                Application.OpenForms["frmDetalleGuia"].Activate();
            }
            else
            {

                if (CodProveedor != 0)
                {
                    frmDetalleGuia form = new frmDetalleGuia();
                    form.Procede = 12;
                    form.Proceso = 1;

                    if (codIngMatPrima != 0 && dgvDetalle.Rows.Count > 0)
                    {
                        form.txtCodigo.Text = codIngMatPrima.ToString();
                        form.txtStock.Text = String.Format("{0:#,##0.0000}", stockactual);

                    }

                    form.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Escoger Proveedor");
                }
            }
        }

        private void CargarPesador()
        {
            cboPesador.DataSource = admpesa.ListaPesadores();
            cboPesador.ValueMember = "codPesador";
            cboPesador.DisplayMember = "nombre";
            cboPesador.SelectedIndex = -1;
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
                 //   form.Proceso = 3;
                    form.Procede = 8;
                    form.ShowDialog();
                    if (CodProveedor != 0) { CargaProveedor(); ProcessTabKey(true); } else { BorrarProveedor(); }
                }
            }
        }


        private void CargaProveedor()
        {
            prov = AdmProv.MuestraProveedor(CodProveedor);
            txtCodProv.Text = prov.Ruc;
            txtNombreProv.Text = prov.RazonSocial;
        //    txtCodProveedor.Text = prov.CodProveedor.ToString();
        }

        private void BorrarProveedor()
        {
            prov = AdmProv.MuestraProveedor(CodProveedor);
            txtCodProv.Text = "";
            txtNombreProv.Text = "";
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

        private void txtCodProv_Leave(object sender, EventArgs e)
        {
            if (CodProveedor == 0)
            {
                txtCodProv.Focus();
            }
            else
            {
                /*
                VerificarCabecera();
                if (Validacion)
                {
                    btnDetalle.Enabled = true;
                }

                */
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
                 //   form.Transaccion = txtTransaccion.Text;
                    form.ShowDialog();
                    doc = form.doc;
                    CodDocumento = doc.CodTipoDocumento;
                    txtDocRef.Text = doc.Sigla;
                    if (CodDocumento != 0) { ProcessTabKey(true); } else { txtDocRef.Text = ""; }
                }
            } 
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            clsReporteFrutosa ds = new clsReporteFrutosa();

            CRListaRecepcionInicial rpt = new CRListaRecepcionInicial();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds.getTicketIngresoContenedor(CodIngresoCamara,frmLogin.iCodAlmacen,1).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0)
                  dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
        }

        
    }
}
