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
    public partial class frmParamCobrosDia : DevComponents.DotNetBar.OfficeForm
    {
        clsVendedor vend = new clsVendedor();
        clsAdmVendedor admVen = new clsAdmVendedor();
        clsReporteCobrosDia ds = new clsReporteCobrosDia();

        public frmParamCobrosDia()
        {
            InitializeComponent();
        }

        private void CargaVendedores()
        {
            cmbVendedor.DataSource = admVen.CargaVendedoresReporte();
            cmbVendedor.DisplayMember = "apellido";
            cmbVendedor.ValueMember = "codVendedor";
            cmbVendedor.SelectedIndex = 0;
        }

        private void frmParamCobrosDia_Load(object sender, EventArgs e)
        {
            CargaVendedores();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            CRCobrosxDia rpt = new CRCobrosxDia();
            frmRptCobrosxDia frm = new frmRptCobrosxDia();
            rpt.SetDataSource(ds.ReporteCobrador(Convert.ToInt32(cmbVendedor.SelectedValue),dtpFecha1.Value,dtpFecha2.Value,
                frmLogin.iCodAlmacen).Tables[0]);
            frm.crvCobros.ReportSource = rpt;
            frm.Show();
        }
    }
}