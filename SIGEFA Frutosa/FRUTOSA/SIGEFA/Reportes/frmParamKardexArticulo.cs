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
    public partial class frmParamKardexArticulo : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmEmpresa admEmp = new clsAdmEmpresa();
        clsAdmFormaPago AdmPago = new clsAdmFormaPago();
        clsReporteKardex ds = new clsReporteKardex();
        clsProducto pro = new clsProducto();
        clsAdmProducto AdmPro = new clsAdmProducto();

        public frmParamKardexArticulo()
        {
            InitializeComponent();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            CRKardex rpt = new CRKardex();
            frmRptKardex frm = new frmRptKardex();
            rpt.SetDataSource(ds.kardex(dtpFecha1.Value, dtpFecha2.Value, rbTodosArt.Checked, txtUnArt.Text).Tables[0]);
            frm.crvKardex.ReportSource = rpt;
            frm.Show();
        }

        private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmProductosLista frm = new frmProductosLista();
                frm.Procede = 8; //(8) Procede desde el formulario frmParamVentxVendedor
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargaProducto(frm.GetCodigoProducto());
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
            txtUnArt.Text = "";
            txtArticulo.Text = "";
            txtUnArt.Enabled = rbArt.Checked;
            txtUnArt.Focus();
        }
    }
}
