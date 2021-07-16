﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios
{
    public partial class frmGestionAlmacen : DevComponents.DotNetBar.OfficeForm
    {
        public Int32 Proceso = 0; //(1) Nuevo (2) Editar (3) Consulta
        clsAdmEmpresa admEmp = new clsAdmEmpresa();
        clsAdmSucursal AdmSuc = new clsAdmSucursal();
        clsAdmAlmacen admAlm = new clsAdmAlmacen();
        public clsAlmacen alm = new clsAlmacen();

        clsValidar ok = new clsValidar();
        clsConsultasExternas ext = new clsConsultasExternas();

        clsAdmTipoArticulo AdmTip = new clsAdmTipoArticulo();

        public frmGestionAlmacen()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                if (Proceso != 0 && txtNombre.Text != "")
                {
                    Double CodAlm = 0;
                    if (txtCodAlmacen.Text == "")
                    {
                        CodAlm = 0;
                    }
                    else
                    {
                        CodAlm = Convert.ToDouble(txtCodAlmacen.Text);
                    }
                    alm.Nombre = txtNombre.Text;
                    alm.Ubicacion = txtUbicacion.Text;
                    alm.Telefono = txtTelefono.Text;
                    alm.Descripcion = txtDescripcion.Text;
                    alm.Estado = cbActivo.Checked;
                    alm.CodEmpresa =frmLogin.iCodEmpresa;
                    alm.CodUser = frmLogin.iCodUser;
                    alm.CodSuc =  Convert.ToInt32(cboSucursal.SelectedValue);
                    alm.EstadoPrincipal = cbPrincipal.Checked;
                    alm.ITipoAlm = Convert.ToInt32(cbTipoArticulo.SelectedValue);

                    if (Proceso == 1)
                    {
                        if (admAlm.insert(alm))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Almacen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    else if (Proceso == 2)
                    {
                        if (admAlm.update(alm))
                        {
                            MessageBox.Show("Los datos se guardaron correctamente", "Gestion Almacen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                }
            }
        }







        private void CargaTipoArticulos()
        {
            try
            {
                cbTipoArticulo.DataSource = AdmTip.MuestraTipoArticulos();
                cbTipoArticulo.DisplayMember = "descripcion";
                cbTipoArticulo.ValueMember = "codTipoArticulo";
                cbTipoArticulo.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }









        private void frmGestionAlmacen_Load(object sender, EventArgs e)
        {
            CargaTipoArticulos();
            CargaSucursales();
            if (Proceso == 1)
            {
                cboSucursal.SelectedValue = frmLogin.iCodEmpresa;
            }
            else if (Proceso == 2)
            {
                CargaAlmacen();
            }
            else if (Proceso == 3)
            {
                CargaAlmacen();
                ext.sololectura(groupBox1.Controls);
                btnAceptar.Visible = false;
                btnCancelar.Text = "Aceptar";
                btnCancelar.ImageIndex = 1;
            }
        }

        private void CargaAlmacen()
        {
            alm = admAlm.CargaAlmacen(alm.CodAlmacen);
            //cboSucursal.SelectedValue = alm.CodAlmacen;
            cboSucursal.SelectedValue = alm.CodSuc;
            txtCodAlmacen.Text = alm.CodAlmacen.ToString();
            txtNombre.Text = alm.Nombre;
            txtUbicacion.Text = alm.Ubicacion;
            txtTelefono.Text = alm.Telefono;
            txtDescripcion.Text = alm.Descripcion;
            cbActivo.Checked = alm.Estado;
            cbPrincipal.Checked = alm.EstadoPrincipal;
            cbTipoArticulo.SelectedValue = alm.ITipoAlm;

        }

        private void CargaSucursales()
        {
            cboSucursal.DataSource = AdmSuc.CargaSucursales(frmLogin.iCodEmpresa);
            cboSucursal.DisplayMember = "nombre";
            cboSucursal.ValueMember = "codSucursal";
            cboSucursal.SelectedIndex = 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customValidator1_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (e.ControlToValidate.Text != "")
                e.IsValid = true;
            else
                e.IsValid = false;
        }

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (e.ControlToValidate.Text != "")
                e.IsValid = true;
            else
                e.IsValid = false;
        }

        private void customValidator3_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (e.ControlToValidate.Text != "")
                e.IsValid = true;
            else
                e.IsValid = false;
        }

       
    }
}
