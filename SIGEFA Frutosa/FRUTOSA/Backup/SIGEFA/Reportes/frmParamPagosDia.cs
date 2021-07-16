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
    public partial class frmParamPagosDia : DevComponents.DotNetBar.OfficeForm
    {
        clsReportePagosDia ds = new clsReportePagosDia();

        public frmParamPagosDia()
        {
            InitializeComponent();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            CRPagosDia rpt = new CRPagosDia();
            frmRptPagosxDia frm = new frmRptPagosxDia();
            rpt.SetDataSource(ds.Reporte(dtpFecha1.Value,dtpFecha2.Value,frmLogin.iCodAlmacen).Tables[0]);
            frm.crvPagosDia.ReportSource = rpt;
            frm.Show();
        }
    }
}
