using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.IO;
using System.Drawing.Imaging;
using System.Reflection;

namespace SIGEFA.Formularios
{
    public partial class frmPesadores : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 Proceso = 0; //(1) Nuevo (2)Editar (3)Consulta
        clsConsultasExternas ext = new clsConsultasExternas();
        clsPesador pesa = new clsPesador();
        clsAdmPesador admpesa = new clsAdmPesador();
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        clsValidar val = new clsValidar();
        private FilterInfoCollection devices;
        private VideoCaptureDevice fuentes;
        ComboBox combo = new ComboBox();

        public frmPesadores()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Proceso != 0)
                {


                    if (txtDni.Text.Length == 8 && txNombreApellido.Text.Length >= 5)
                    {

                        pesa.Nombre = txNombreApellido.Text;
                        pesa.Dni = txtDni.Text;
                        pesa.Nombre = txNombreApellido.Text;
                        pesa.Telefono = txtTelefono.Text;
                        pesa.Direccion = txtDireccion.Text;
                        pesa.Fotopesador = label18.Image;
                        if (chkExterno.Checked == true)
                        {
                            // Externo
                            pesa.Tipo = 2;
                        }
                        else
                        {
                            // Interno
                            pesa.Tipo = 1;
                        }

                        if (Proceso == 1)
                        {
                            pesa.CodUser = frmLogin.iCodUser;
                            if (admpesa.Insert(pesa))
                            {
                                MessageBox.Show("Los datos se guardaron correctamente", "Gestion Pesador", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CambiarEstados(true);
                                CargaLista();
                                Proceso = 0;
                            }
                        }
                        else if (Proceso == 2)
                        {
                            if (admpesa.Update(pesa))
                            {
                                MessageBox.Show("Los datos se guardaron correctamente", "Gestion Conductor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CambiarEstados(true);
                                CargaLista();
                                Proceso = 0;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Verificar DNI ingresado y/o Nombres ");
                    }
                    //Proceso = 0;
                }
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible)
            {
                ApagarCamara(vspfoto2);
                this.Close();
            }
            else
            {
                Proceso = 0;
                CambiarEstados(true);
                ApagarCamara(vspfoto2);
            }
        }
        private void CargaPesador()
        {
            pesa = admpesa.CargaPesador(pesa.CodPesador);
            if (pesa != null)
            {
                txtCodigo.Text = pesa.CodPesador.ToString();
                txtDni.Text = pesa.Dni;
                txNombreApellido.Text = pesa.Nombre;
                txtTelefono.Text = pesa.Telefono;
                txtDireccion.Text = pesa.Direccion;
                if (pesa.Tipo == 1)
                {
                    chkExterno.Checked = false;
                }
                else
                {
                    chkExterno.Checked = true;
                }
                label18.Image = pesa.Fotopesador;
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            ext.limpiar(groupBox2.Controls);
            CambiarEstados(false);
            groupBox2.Text = "Editar Registro";
            Proceso = 2;
            CargaPesador();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvPesadores.CurrentRow.Index != -1 && pesa.CodPesador != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Conductor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admpesa.Delete(pesa.CodPesador))
                    {
                        MessageBox.Show("Los datos han sido eliminado correctamente", "Conductor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void CargaLista()
        {
            dgvPesadores.DataSource = data;
            data.DataSource = admpesa.ListaPesadores();
            data.Filter = String.Empty;
            filtro = String.Empty;
        }

        private void frmPesadores_Load(object sender, EventArgs e)
        {
            CargaLista();
            label2.Text = "DNI";
            label3.Text = "dni";
        }

        private void dgvPesadores_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (dgvPesadores.Rows.Count > 0 && e.Row.Selected)
                {
                    pesa.CodPesador = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
            }
        }

        private void dgvPesadores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnEliminar.Enabled  = true;
        }

        private void txtDni_KeyPress(object sender, KeyPressEventArgs e)
        {
            val.Numeros(e);
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
                MessageBox.Show("Error: " + ex.Message.ToString());
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
                MessageBox.Show("Error: " + ex.Message.ToString());
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

        //public AForge.Controls.VideoSourcePlayer vspfoto2 { get; set; }

        private void dgvPesadores_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                label2.Text = dgvPesadores.Columns[e.ColumnIndex].HeaderText;
                label3.Text = dgvPesadores.Columns[e.ColumnIndex].DataPropertyName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
            }
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

        private void btnReporte_Click(object sender, EventArgs e)
        {

            /*
                clsReporteFrutosa ds = new clsReporteFrutosa();
                CRNotaEntregaRecepcion rpt = new CRNotaEntregaRecepcion();
                frmListaIngresosCamara frm = new frmListaIngresosCamara();
                rpt.SetDataSource(ds.getReportePesadores().Tables[0]);
                frm.crvIngresosCamara.ReportSource = rpt;
                frm.Show();
            */

            
            DataTable dt = new DataTable("Pesadores");
            // Columnas
            foreach (DataGridViewColumn column in dgvPesadores.Columns)
            {
                DataColumn dc = new DataColumn(column.Name.ToString());
                dt.Columns.Add(dc);
            }
            // Datos
            for (int i = 0; i < dgvPesadores.Rows.Count; i++)
            {
                DataGridViewRow row = dgvPesadores.Rows[i];
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dgvPesadores.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                dt.Rows.Add(dr);
            }

            frmPesadoresRP frm = new frmPesadoresRP();
            frm.DTable = dt;
            frm.Show();

            
        }
    }
}
