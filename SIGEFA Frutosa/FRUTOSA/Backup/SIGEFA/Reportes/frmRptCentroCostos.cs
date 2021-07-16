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
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes
{
    public partial class frmRptCentroCostos : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmEmpresa admEmp = new clsAdmEmpresa();
        clsAdmFormaPago AdmPago = new clsAdmFormaPago();
        clsReporteFrutosa ds = new clsReporteFrutosa();
        clsProducto pro = new clsProducto();
        clsAdmProducto AdmPro = new clsAdmProducto();

        Int32 codnivel = 0, param_impresion = 0, moneda = 0;
        Boolean valorizado = false, param_tipo_costo = false;


        public frmRptCentroCostos()
        {
            InitializeComponent();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            CRptCentroCostos1 rpt;
            CRptCentroCostos2 rpt2;
            frmListaIngresosCamara frm = new frmListaIngresosCamara();

      //      codnivel = cmbVendedor.SelectedIndex + 1;
            if(radioButton3.Checked == true)
                param_impresion = 1;//resumido
            
            if(radioButton4.Checked == true)
                param_impresion = 2;//detallado

           if(rbTodosArt.Checked == true)
                moneda = 1;
           if(rbArt.Checked == true)
                moneda = 2;
          
            if(radioButton1.Checked == true)
                param_tipo_costo = true;

            if(radioButton1.Checked == true)
                param_tipo_costo = false;

            valorizado = checkBox1.Checked;

           
            if (param_impresion == 1)
            {
                rpt2 = new CRptCentroCostos2();
                rpt2.SetDataSource(ds.rptCentroCostos1(1,param_impresion, codnivel, moneda, param_tipo_costo, Int32.Parse(txtUnArt.Text), Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), dtpFecha1.Value.Date, dtpFecha2.Value.Date, valorizado,frmLogin.iCodAlmacen).Tables[0]);
                frm.crvIngresosCamara.ReportSource = rpt2; 
            }
            if (param_impresion == 2)
            {
                rpt = new CRptCentroCostos1();
                rpt.SetDataSource(ds.rptCentroCostos1(1, param_impresion, codnivel, moneda, param_tipo_costo, Int32.Parse(txtUnArt.Text), Int32.Parse(textBox1.Text), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), dtpFecha1.Value.Date, dtpFecha2.Value.Date, valorizado, frmLogin.iCodAlmacen).Tables[0]);
                frm.crvIngresosCamara.ReportSource = rpt;
            }
            frm.Show();
        }

        private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.F1)
            {


                if (codnivel == 1)
                {

                    if (Application.OpenForms["frmCentroCostos"] != null)
                    {
                        Application.OpenForms["frmCentroCostos"].Activate();
                    }
                    else
                    {
                        frmCentroCostos frm = new frmCentroCostos();
                        frm.Proceso = 5;
                        frm.ShowDialog();
                    }
                }


                if (codnivel == 2)
                {

                    if (Application.OpenForms["frmCentroCostos2"] != null)
                    {
                        Application.OpenForms["frmCentroCostos2"].Activate();
                    }
                    else
                    {
                        frmCentroCostos2 frm = new frmCentroCostos2();
                        frm.Proceso = 5;
                        frm.ShowDialog();
                    }
                }

                if (codnivel == 3)
                {

                    if (Application.OpenForms["frmCentroCostos3"] != null)
                    {
                        Application.OpenForms["frmCentroCostos3"].Activate();
                    }
                    else
                    {
                        frmCentroCostos3 frm = new frmCentroCostos3();
                        frm.Proceso = 5;
                        frm.ShowDialog();
                    }
                }

            }
        }

        private void CargaProducto(Int32 Codigo)
        {
            pro = AdmPro.CargaProducto(Codigo, frmLogin.iCodAlmacen);
            txtUnArt.Text = pro.Referencia;
            txtArticulo.Text = pro.Descripcion;
        }

        private void rbArt_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void rbTodosArt_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmProductosLista"] != null)
                {
                    Application.OpenForms["frmProductosLista"].Activate();
                }
                else
                {
                    frmProductosLista frm = new frmProductosLista();
                    frm.Procede = 20;
                    frm.ShowDialog();
                }
            }
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmProductosLista"] != null)
                {
                    Application.OpenForms["frmProductosLista"].Activate();
                }
                else
                {
                    frmProductosLista frm = new frmProductosLista();
                    frm.Procede = 21;
                    frm.ShowDialog();
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (codnivel == 1)
                {

                    if (Application.OpenForms["frmCentroCostos"] != null)
                    {
                        Application.OpenForms["frmCentroCostos"].Activate();
                    }
                    else
                    {
                        frmCentroCostos frm = new frmCentroCostos();
                        frm.Proceso = 6;
                        frm.ShowDialog();
                    }
                }


                if (codnivel == 2)
                {

                    if (Application.OpenForms["frmCentroCostos2"] != null)
                    {
                        Application.OpenForms["frmCentroCostos2"].Activate();
                    }
                    else
                    {
                        frmCentroCostos2 frm = new frmCentroCostos2();
                        frm.Proceso = 6;
                        frm.ShowDialog();
                    }
                }

                if (codnivel == 3)
                {

                    if (Application.OpenForms["frmCentroCostos3"] != null)
                    {
                        Application.OpenForms["frmCentroCostos3"].Activate();
                    }
                    else
                    {
                        frmCentroCostos3 frm = new frmCentroCostos3();
                        frm.Proceso = 6;
                        frm.ShowDialog();
                    }
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                groupBox3.Enabled = true;
            else
                groupBox3.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            codnivel = cmbVendedor.SelectedIndex + 1;
        }

        private void frmRptCentroCostos_Load(object sender, EventArgs e)
        {
            cmbVendedor.SelectedIndex = 0;
        }
    }
}
