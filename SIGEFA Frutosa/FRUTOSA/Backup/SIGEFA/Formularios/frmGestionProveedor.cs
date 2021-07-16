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
using System.Net.NetworkInformation;
using SIGEFA.net.miasoftware.webservice;

namespace SIGEFA.Formularios
{
    public partial class frmGestionProveedor : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 Proceso = 0; //(1) Nuevo Proveedor (2)Editar Proveedor (3)Nota Ingreso
        clsAdmProveedor admProv = new clsAdmProveedor();
        public clsProveedor prov = new clsProveedor();
        clsConsultasExternas ext = new clsConsultasExternas();
        clsAdmListaPrecio AdmLista = new clsAdmListaPrecio();
        clsListaPrecio lista = new clsListaPrecio();
        Boolean margechange = false; // variable para validar si se ha realizado algun cambio en el margen de ganancia del proveedor
        clsLocalidad local = new clsLocalidad();
        SunatPersona sunat  = new SunatPersona();
        ReniecPersona reniec = new ReniecPersona();


        //RAGA

        String dni;
        public Boolean liqcompra;

        //PARA HACER PING A SERVIDOR
        Ping pinguin = new Ping();
        Int32 timeout = 4;
        //

        public frmGestionProveedor()
        {
            InitializeComponent();
            CargarImagen();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Proceso != 0 && txtRUC.Text != "")
            {                
                prov.Ruc = txtRUC.Text;
                prov.RazonSocial = txtRazonSocial.Text;
                prov.Direccion = txtDireccion.Text;
                prov.Telefono = txtTelefono.Text;
                prov.Fax = txtFax.Text;
                prov.Representante = txtRepresentante.Text;
                prov.Mail = txtmail.Text;
                prov.Contacto = txtContacto.Text;
                prov.TelefonoContacto = txtTelCon.Text;
                if (txtVisita.Text != "") { prov.FrecuenciaVisita = Convert.ToInt32(txtVisita.Text); }
                if (txtRecargo.Text != "") { prov.Margen = Convert.ToDouble(txtRecargo.Text); } else { prov.Margen = 0; }
                prov.Banco = txtBanco.Text;
                prov.CtaCte = txtCtaCte.Text;
                prov.Comentario = txtComentario.Text;
                prov.CodUser = frmLogin.iCodUser;
                prov.Estado = cbActivo.Checked;


                //RAGA
                if (checkBox1.Checked == true)
                    prov.ITipoDocumento = 1;
                else
                    prov.ITipoDocumento = 2;

                if (checkBox2.Checked == true)
                    prov.BTipoPersoneria = true;
                else
                    prov.BTipoPersoneria = false;


                if (textBox1.Text == "ACTIVO")//ESTADO
                    prov.BEstadoContribuyente = true;
                else
                    prov.BEstadoContribuyente = false;

                if (textBox2.Text == "HABIDO")//CONDICION
                    prov.BSituacion = true;
                else
                    prov.BSituacion = false;




                if (Proceso == 1 || Proceso == 3 || Proceso == 4)
                {
                    if (admProv.insert(prov))
                    {
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);


                        if (Proceso == 4)
                        {
                            frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                            form.txtCodProv.Text = txtRUC.Text;
                            form.txtNombreProv.Text = txtRazonSocial.Text;
                            form.CodProveedor = prov.CodProveedorNuevo;
                        }
                        
                        this.Close();
                    }
                }
                else if (Proceso == 2)
                {
                    if (admProv.update(prov))
                    {
                        if (margechange)
                        {
                            DialogResult dlgResult = MessageBox.Show("Desea recalcular la listas de precios con el margen actual", "Proveedores", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dlgResult == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                foreach (Int32 codlista in AdmLista.MuestraListasProveedor(frmLogin.iCodAlmacen))
                                {
                                    lista = AdmLista.CargaListaPrecio(codlista);
                                    if (AdmLista.GeneraListaProveedor(lista.CodListaPrecio, frmLogin.iCodAlmacen, lista.Decimales, prov.CodProveedor))
                                    {
                                        MessageBox.Show("Se actualizo la lista " + lista.Nombre + " ", "Gestion Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Los datos se guardaron correctamente", "Gestion Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }





            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


















        private void frmGestionProveedor_Load(object sender, EventArgs e)
        {
            try
            {
                CargaLocalidades(cbDepartamento, "000000", 1);
                if (Proceso == 2)
                {
                    cargaproveedor();
                }
                else if (Proceso == 3)
                {
                    cargaproveedor();
                    ext.sololectura(groupBox1.Controls);
                    btnAceptar.Visible = false;
                    btnCancelar.Text = "Aceptar";
                    btnCancelar.ImageIndex = 1;
                }
                else if (Proceso == 4)
                {


                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void cbDepartamento_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CargaLocalidades(cbProvincia, cbDepartamento.SelectedValue.ToString(), 2);
                cbProvincia.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbProvincia_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CargaLocalidades(cbDistrito, cbProvincia.SelectedValue.ToString(), 3);
                cbDistrito.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargaLocalidades(ComboBox Combo, String Padre, Int32 Nivel)
        {
            try
            {
                Combo.DataSource = local.CargaLocalidades(Padre, Nivel);
                Combo.DisplayMember = "nombre";
                Combo.ValueMember = "codLocalidad";
                Combo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cargaproveedor()
        {
            prov = admProv.MuestraProveedor(prov.CodProveedor);
            txtRUC.Text = prov.Ruc;
            txtRazonSocial.Text = prov.RazonSocial;
            txtDireccion.Text = prov.Direccion;
            txtTelefono.Text = prov.Telefono;
            txtFax.Text = prov.Fax;
            txtRepresentante.Text = prov.Representante;
            txtmail.Text = prov.Mail;
            txtContacto.Text = prov.Contacto;
            txtTelCon.Text = prov.TelefonoContacto;
            txtVisita.Text = prov.FrecuenciaVisita.ToString();
            txtRecargo.Text = prov.Margen.ToString();
            txtBanco.Text = prov.Banco;
            txtCtaCte.Text = prov.CtaCte;
            txtComentario.Text = prov.Comentario;
            cbActivo.Checked = prov.Estado;
            cbDepartamento.SelectedValue = prov.Departamento;
            if (prov.Departamento != "")
            {
                cbDepartamento.SelectedValue = prov.Departamento;
                CargaLocalidades(cbProvincia, prov.Departamento.ToString(), 2);
                cbProvincia.Enabled = true;

                if (prov.Provincia != "")
                {
                    cbProvincia.SelectedValue = prov.Provincia;
                    CargaLocalidades(cbDistrito, prov.Provincia.ToString(), 3);
                    cbDistrito.Enabled = true;
                    cbDistrito.SelectedValue = prov.Distrito;
                }
            }
        }

        private void txtRUC_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F2)
            //{
            //    //ruc(txtRUC.Text);

            //    if (ext.rucsunat(txtRUC.Text))
            //    {
            //        txtRazonSocial.Text = ext.RazonSocial;
            //        txtDireccion.Text = ext.DireccionLegal;
            //    }
            //    else
            //    {
            //        ext.limpiar(this.Controls);
            //    }
            //}
        }

        private void txtRUC_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtRecargo_TextChanged(object sender, EventArgs e)
        {
            margechange = true;
        }

        private void frmGestionProveedor_Shown(object sender, EventArgs e)
        {
            margechange = false;
        }

        private bool MandaPing(string ips)
        {
          //  return false;  Cuando No hay Internet
            
            /*
            try
            {
                if (pinguin.Send(ips, timeout).Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No existe conexion a internet para hacer busquedas web", "ATENCION");
                return false;
            }
            */
            return true;
        }

        private void txtRUC_Leave(object sender, EventArgs e)
        {

           
            
            
        }



        public void limpiar()
        {

           txtRUC.Text = "";
           txtRazonSocial.Text = "";
           txtDireccion.Text = "";
           textBox1.Text = "";
           textBox2.Text = "";
           txtcaptcha.Text = "";
        }


        private void CargarImagen()
        {
            try
            {
               
                if (checkBox2.Checked == true)
                {
                    CargarImagenSunat();
                }
                else
                {
                    CargarImagenReniec();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarImagenReniec()
        {
            try
            {
                if (reniec == null)
                    reniec = new ReniecPersona();
                pbcaptcha.Image = reniec.GetCapcha;
                txtcaptcha.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarImagenSunat()
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


        private void txtcaptcha_KeyDown(object sender, KeyEventArgs e)
        {
         
           /*
            if (sunat == null)
                return;

            



            //Consultar SUNAT
            if (e.KeyCode == Keys.F2 && (txtcaptcha.Text.Length  )== 4)
            {
                if (txtRUC.TextLength == 11 && txtRUC.Text != "")
                {
                    sunat.GetInfo(txtRUC.Text, txtcaptcha.Text);
                    if (string.IsNullOrEmpty(sunat.Error))
                    {
                        txtRazonSocial.Text = sunat.RazonSocial;
                        txtDireccion.Text = sunat.DomicilioFiscal;
                        textBox1.Text = sunat.EstadoContribuyente;
                        textBox2.Text = sunat.CondicionContribuyente;
                    }
                }

                if (txtRUC.TextLength == 8 && txtRUC.Text != "")
                {
                    dni = txtRUC.Text;
                    sunat.GetInfoDNI(txtRUC.Text, txtcaptcha.Text);

                    if (sunat.Ruc == null)
                    {
                        MessageBox.Show("El Sistema RUC NO REGISTRA un número de RUC para el DNI número " + dni + " consultado");
                      //  txtRUC.Text = "";
                        txtRazonSocial.Text = "";
                        textBox1.Text = "";
                        txtDireccion.Text = "";
                        return;
                    }
                    if (sunat.Ruc == "")
                    {
                        MessageBox.Show("El codigo CAPTCHA ingresado es incorrecto ");
                     //   txtRUC.Text = "";
                        txtRazonSocial.Text = "";
                        textBox1.Text = "";
                        txtDireccion.Text = "";
                        return;
                    }

                        if (sunat.Ruc.Length == 11)
                        {

                            MessageBox.Show("El DNI : " + dni + " tiene como RUC:  " + sunat.Ruc);
                            checkBox1.Checked = true;
                            txtRUC.Text = sunat.Ruc;
                            txtRazonSocial.Text = sunat.Pnatural;
                            textBox1.Text = sunat.EstadoContribuyente;
                            txtDireccion.Text = sunat.DomicilioFiscal;
                        }
                      
                    
    
                }

            }

            //Consultar RENIEC
            if (e.KeyCode == Keys.F3 && (txtcaptcha.Text.Length ) == 4)
            {
                if (txtRUC.TextLength == 8 && txtRUC.Text != "")
                {
                    reniec.GetInfo(txtRUC.Text, txtcaptcha.Text);
                    txtRazonSocial.Text = reniec.Nombres + " " + reniec.ApePaterno + " " + reniec.ApeMaterno;

                }
            }
          

            */

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox1.Text = "RUC";
                label3.Text = "R.U.C. :";
                label2.Text = "Razon Social:";
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox1.Text = "DNI";
                label3.Text = "D.N.I :";
                label2.Text = "Nomb y Apell:";
                checkBox2.Enabled = true;
            }
            limpiar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            limpiar();
        //    CargarImagen();

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked == true)
            {
                checkBox2.Text = "SUNAT";
                pbcaptcha.Visible = false;
                txtcaptcha.Visible = false;
                button2.Visible = false;
                CargarImagen();

            }
            else
            {
                checkBox2.Text = "RENIEC";
                checkBox1.Checked = true;
                checkBox1.Enabled = true;


                pbcaptcha.Visible = true;
                txtcaptcha.Visible = true;
                button2.Visible = true;
                CargarImagenReniec();

            }

         //   limpiar();
           // CargarImagen();
        }

        private void txtcaptcha_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            CargarImagen();
            
        }



        




        private void button3_Click(object sender, EventArgs e)
        {
            if (sunat.Existeconexion == true)
            {

         //   if (txtcaptcha.Text.Length == 4)
         //   {

                //CONSULTAR SUNAT
                if (checkBox2.Checked == true)
                {


                    if (txtRUC.TextLength == 11 && txtRUC.Text != "")
                    {
                        sunat.GetInfo(txtRUC.Text, sunat.UseTesseract());
                        if (string.IsNullOrEmpty(sunat.Error))
                        {
                            txtRazonSocial.Text = sunat.RazonSocial;
                            txtDireccion.Text = sunat.DomicilioFiscal;
                            textBox1.Text = sunat.EstadoContribuyente;
                            textBox2.Text = sunat.CondicionContribuyente;
                        }
                    }

                    if (txtRUC.TextLength == 8 && txtRUC.Text != "")
                    {
                        dni = txtRUC.Text;
                         sunat.GetInfoDNI(txtRUC.Text, sunat.UseTesseract());

                     //   sunat.GetInfo2(txtRUC.Text, sunat.UseTesseract());


                        if (sunat.Ruc == null)
                        {
                            MessageBox.Show("El Sistema RUC NO REGISTRA un número de RUC para el DNI número " + dni + " consultado");
                            //  txtRUC.Text = "";
                            txtRazonSocial.Text = "";
                            textBox1.Text = "";
                            txtDireccion.Text = "";
                            return;
                        }
                        if (sunat.Ruc == "")
                        {
                            MessageBox.Show("El codigo CAPTCHA ingresado es incorrecto ");
                            //   txtRUC.Text = "";
                            txtRazonSocial.Text = "";
                            textBox1.Text = "";
                            txtDireccion.Text = "";
                            return;
                        }

                        if (sunat.Ruc.Length == 11)
                        {

                            MessageBox.Show("El DNI : " + dni + " tiene como RUC:  " + sunat.Ruc);


                            sunat.GetInfo(sunat.Ruc, sunat.UseTesseract());
                            if (string.IsNullOrEmpty(sunat.Error))
                            {
                                txtRUC.Text = sunat.Ruc;
                                txtRazonSocial.Text = sunat.RazonSocial;
                                txtDireccion.Text = sunat.DomicilioFiscal;
                                textBox1.Text = sunat.EstadoContribuyente;
                                textBox2.Text = sunat.CondicionContribuyente;
                            }
                            
                            
                            /*
                            checkBox1.Checked = true;
                            txtRUC.Text = sunat.Ruc;
                            txtRazonSocial.Text = sunat.RazonSocial;
                            textBox1.Text = sunat.EstadoContribuyente;
                            txtDireccion.Text = sunat.DomicilioFiscal;
                            
                            */
                        }



                    }




                }
                else if (checkBox2.Checked == false)
                {
                    //CONSULTAR RENIEC
                    if (txtRUC.TextLength == 8 && txtRUC.Text != "")
                    {
                        reniec.GetInfo(txtRUC.Text, txtcaptcha.Text);
                        txtRazonSocial.Text = reniec.Nombres + " " + reniec.ApePaterno + " " + reniec.ApeMaterno;

                    }

                }



           // }
        }

        }

        private void txtcaptcha_KeyPress(object sender, KeyPressEventArgs e)
        {
            //CargarImagen();
        }

        private void txtcaptcha_Leave(object sender, EventArgs e)
        {
          //  CargarImagen();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServiceConsultaRUC servicio = new ServiceConsultaRUC();
            String data = servicio.consultaRUC(txtRUC.Text, "cali1210@hotmail.com", "", "");
            //consultaANEXOAsync
            //consultaANEXO
            //consultaRUCAsync
            MessageBox.Show(data);
        }
    }
}
