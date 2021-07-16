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

using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;

namespace SIGEFA.Formularios
{
    public partial class frmConductores : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmConductor AdmCon = new clsAdmConductor();
        clsValidar valida = new clsValidar();
        clsConsultasExternas ext = new clsConsultasExternas();
        public clsConductor cond = new clsConductor();
        public Int32 Proceso = 0; //(1) Nuevo (2)Editar (3)Consulta
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;

        private FilterInfoCollection devices;
        private VideoCaptureDevice fuentes;
        ComboBox combo = new ComboBox();
        

        public frmConductores()
        {
            InitializeComponent();
        }

        private void CargaLista()
        {
            dgvConductors.DataSource = data;
            data.DataSource = AdmCon.MuestraConductores();
            data.Filter = String.Empty;
            filtro = String.Empty;
        }

        private void CambiarEstados(Boolean Estado)
        {
            groupBox1.Visible = Estado;
            groupBox2.Visible = !Estado;
            btnGuardar.Enabled = !Estado;                       
            btnNuevo.Enabled = Estado;
            btnEditar.Enabled = Estado;
            btnEliminar.Enabled = Estado;
            btnReporte.Enabled = Estado;
            txtCodigo.Text = "";
            txNombreApellido.Text = "";
            label18.Visible = !Estado;
            btnCapturaConductor.Visible = !Estado;
            btnFotoConductor.Visible = !Estado;
            superValidator1.Validate();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            CambiarEstados(false);
            groupBox2.Text = "Registro Nuevo";
            Proceso = 1;
            ext.limpiar(groupBox2.Controls);
            label18.ImageIndex = 0;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ext.limpiar(groupBox2.Controls);
            CambiarEstados(false);
            groupBox2.Text = "Editar Registro";
            Proceso = 2;
            cargaconductor();
        }

        private void cargaconductor()
        {
            cond = AdmCon.MuestraConductor(cond.CodConductor);
            if (cond != null)
            {
                txtCodigo.Text = cond.CodConductor.ToString();
                txtDni.Text = cond.Dni;
                txtRuc.Text = cond.Ruc;
                txNombreApellido.Text = cond.Nombre;
                txtLicencia.Text = cond.Licencia;
                txtTelefono.Text = cond.Telefono;
                txtDireccion.Text = cond.Direccion;
                label18.Image = cond.FotoConductor;
                if (cond.Tipo == 1)
                {
                    cbExterno.Checked = false;
                }
                else {
                    cbExterno.Checked = true;
                }
            }
        }

        private void frmConductor_Load(object sender, EventArgs e)
        {
            CargaLista();
            label2.Text = "Codigo";
            label3.Text = "codConductor";
            if (Proceso == 3)
            {
                bloquearbotones();
            }
        }

        private void bloquearbotones()
        {
            btnNuevo.Visible = false;
            btnEditar.Visible = false;
            btnEliminar.Visible = false;
            btnReporte.Visible = false;
            btnGuardar.Text = "Aceptar";
            btnGuardar.ImageIndex = 6;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvConductors.CurrentRow.Index != -1 && cond.CodConductor != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Conductor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (AdmCon.delete(cond.CodConductor))
                    {
                        MessageBox.Show("Los datos han sido eliminado correctamente", "Conductor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                this.Close();
            }
            else
            {
                Proceso = 0;
                CambiarEstados(true);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                if (Proceso != 0)
                {
                    cond.Nombre = txNombreApellido.Text;
                    cond.Dni = txtDni.Text;
                    cond.Ruc = txtRuc.Text;
                    cond.Nombre = txNombreApellido.Text;
                    cond.Licencia = txtLicencia.Text;
                    cond.Telefono = txtTelefono.Text;
                    cond.Direccion = txtDireccion.Text;
                    cond.FotoConductor = label18.Image;
                    if (cbExterno.Checked == true)
                    {
                        cond.Tipo = 2;
                    }
                    else {
                        cond.Tipo = 1;
                     }
                    if (Proceso == 1)
                    {
                        cond.CodUser = frmLogin.iCodUser;
                        if (AdmCon.insert(cond))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Conductor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                            Proceso = 0;
                        }
                    }
                    else if (Proceso == 2)
                    {
                        if (AdmCon.update(cond))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Conductor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CambiarEstados(true);
                            CargaLista();
                            Proceso = 0;
                        }
                    }
                    //Proceso = 0;
                }
            }
        }

        private void dgvConductors_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvConductors.Rows.Count >= 1 && e.Row.Selected)
            {
                cond.CodConductor = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                //cond.Nombre = e.Row.Cells[nombre.Name].Value.ToString();
                //cond.CodUser = Convert.ToInt32(e.Row.Cells[coduser.Name].Value);
                //cond.FechaRegistro = Convert.ToDateTime(e.Row.Cells[fecha.Name].Value);
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;               
            }
            else if(dgvConductors.Rows.Count == 0)
            {
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
            }
        }

        private void dgvConductors_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dgvConductors.Columns[e.ColumnIndex].HeaderText;
            label3.Text = dgvConductors.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text.Length >= 2)
                {
                    data.Filter = String.Format("[{0}] like '*{1}*'", label3.Text.Trim(), txtFiltro.Text.Trim());
                }
                else
                {
                    data.Filter = String.Empty;
                }
            }
            catch (Exception ex)
            {
                return;
            }
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

        private void dgvConductors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso == 3)
            {               
                this.Close();
            }
        }

        private void dgvConductors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso == 3)
                btnGuardar.Enabled = true;
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            frmrptCotizacion frm = new frmrptCotizacion();
            frm.tipo = 10;
            frm.ShowDialog();
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            valida.enteros(e);
        }

        private void txtRuc_KeyPress(object sender, KeyPressEventArgs e)
        {
            valida.enteros(e);
        }

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void customValidator3_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                    e.IsValid = true;
                else
                    e.IsValid = false;
            else
                e.IsValid = true;
        }

        private void otorgarimagen(Label lab, OpenFileDialog file)
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
        private void btnFotoConductor_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
            }
           
        }

        private void tomafoto(Label imagen, AForge.Controls.VideoSourcePlayer camara)
        {
            Image img = camara.GetCurrentVideoFrame();
            imageList2.Images.Add(img);
            var pos = imageList2.Images.Count - 1;
            imagen.ImageList = imageList2;
            imagen.ImageIndex = pos;
            imagen.Visible = true;
            img.Dispose();
        }
        private void btnCapturaConductor_Click(object sender, EventArgs e)
        {
            try
            {
                label18.Visible = false;
                btnCapturaConductor.Enabled = false;
                btnFotoConductor.Text = "Capturar";
                CargaCamara(vspfoto2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
            }
           
        }

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

        public AForge.Controls.VideoSourcePlayer vspfoto2 { get; set; }
    }
}
