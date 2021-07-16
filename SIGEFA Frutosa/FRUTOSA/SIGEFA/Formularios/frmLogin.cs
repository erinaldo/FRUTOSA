using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Formularios;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Conexion;

namespace SIGEFA.Formularios
{
    public partial class frmLogin : DevComponents.DotNetBar.OfficeForm
    {
        public static Int32 iCodUser;
        public static Int32 iCodEmpresa;
        public static Int32 iCodSucursal;
        public static String sEmpresa;
        public static Int32 iCodAlmacen;
        public static String sAlmacen;
        public static String sUsuario = "";
        public static String sNombreUser = "";
        public static String sApellidoUSer = "";
        public static Int32 iNivelUser;
        public static String DirecIp = "";
        public static Int32 iTipoAlmacen;

        clsAdmUsuario AdmUser = new clsAdmUsuario();
        clsAdmSucursal AdmSuc = new clsAdmSucursal();
        clsAdmEmpresa AdmEmp = new clsAdmEmpresa();
        clsAdmAlmacen AdmAlm = new clsAdmAlmacen();
        

        clsUsuario Login = new clsUsuario();
        clsEmpresa EmpreLogin = new clsEmpresa();
        clsSucursal SucurLogin = new clsSucursal();

        
        public static clsAlmacen AlmacenLogin = new clsAlmacen();
        public static clsParametros Configuracion = new clsParametros();

        clsConexionMysql con = new clsConexionMysql();
        Int32 iContador;
        
        public static List<Int32> AcesosUsuario = new List<Int32>();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
           // this.Close();
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                Login.Usuario = txtUsuario.Text;
                Login.Contraseña = txtContra.Text;
                Login.CodEmpresaLogin = Convert.ToInt32(cmbEmpresa.SelectedValue);
                Login.Nivel = cmbNivel.SelectedIndex;
                //Login.CodSucursalLogin = Convert.ToInt32(cmbSucursal.SelectedValue);

                if (AdmUser.EstableceLogin(Login))
                {
                    iCodUser = Login.CodUsuario;
                    sUsuario = Login.Usuario;
                    sNombreUser = Login.Nombre;
                    sApellidoUSer = Login.Apellido;
                    iNivelUser = Login.Nivel;
                    iCodEmpresa = Login.CodEmpresaLogin;
                    //iCodSucursal = Login.CodSucursalLogin;
                    EmpreLogin = AdmEmp.CargaEmpresa(iCodEmpresa);
                    SucurLogin = AdmSuc.CargaSucursal(iCodSucursal);
                    Configuracion = AdmEmp.CargaConfiguracion();
                    sEmpresa = EmpreLogin.RazonSocial;
                    DirecIp = con.LocalIPAddress();
                   // Inicio con presentacion del logo de la empresa
                    frmSplashStart ss = new frmSplashStart();
                    if (iCodEmpresa == 1) { ss.flashopen = "\\logo.swf"; } else if (iCodEmpresa == 2) { ss.flashopen = "\\colan.swf"; };
                    this.Hide();
                    ss.Show();
                   // Inicio sin presentacion
                    //mdi_Menu frm = new mdi_Menu();
                    //this.Hide();
                    //frm.Show();
                }
                else
                {
                    iContador += 1;
                    if (iContador == 3)
                    {
                        MessageBox.Show("Ha realizado 3 intentos de Logueo Erróneos, consulte con el Área de TI", "Logueo Fallido!");
                        Application.Exit();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o Conraseña no coinciden", "Logueo Fallido!");
                    }
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            CargaEmpresas();
            cmbNivel.SelectedIndex = 1; // para inicio rápido
            cmbEmpresa.SelectedIndex = 0; // para inicio rápi
        }

        private void cmbNivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                btnLogin.PerformClick();
            }
        }
        private void CargaEmpresas()
        {
            try
            {
                cmbEmpresa.DataSource = AdmEmp.CargaEmpresas();
                cmbEmpresa.DisplayMember = "razonsocial";
                cmbEmpresa.ValueMember = "codEmpresa";
                cmbEmpresa.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //private void CargaSucursales(int CodEmpre)
        //{
        //    cmbSucursal.DataSource = AdmSuc.CargaSucursales(CodEmpre);
        //    cmbSucursal.DisplayMember = "nombre";
        //    cmbSucursal.ValueMember = "codSucursal";
        //    //cmbSucursal.SelectedIndex = -1;
        //}

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
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.SelectedIndex != -1)
                e.IsValid = true;
            else
                e.IsValid = false;
            
        }

        private void customValidator4_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            ComboBox c = (ComboBox)e.ControlToValidate;
            if (c.SelectedIndex != -1)
                e.IsValid = true;
            else
                e.IsValid = false;
        }

        private void frmLogin_Shown(object sender, EventArgs e)
        {
            CargaEmpresas();            
            if (cmbEmpresa.Items.Count > 0)
            {
                cmbNivel.SelectedIndex = 1; // para inicio rápido
                cmbEmpresa.SelectedIndex = 0; // para inicio rápi

                //CargaSucursales(Convert.ToInt32(cmbEmpresa.SelectedValue));
            }

            //if (cmbSucursal.Items.Count > 0)
            //{
            //    cmbSucursal.SelectedIndex = 0;
            //}

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
