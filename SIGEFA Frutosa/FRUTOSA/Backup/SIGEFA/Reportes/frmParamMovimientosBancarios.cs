using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Formularios;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes
{
    public partial class frmParamMovimientosBancarios : DevComponents.DotNetBar.OfficeForm
    {
        clsReporteFlujoCaja ds = new clsReporteFlujoCaja();

        public frmParamMovimientosBancarios()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnvisualizar_Click(object sender, EventArgs e)
        {
            CRMovimientosBancarios rpt = new CRMovimientosBancarios();
            frmRptMovimientosBancario frm = new frmRptMovimientosBancario();
            rpt.SetDataSource(ds.ReporteMovimientosBancarios(dtpFecha1.Value.Date, dtpFecha2.Value.Date, frmLogin.iCodAlmacen).Tables[0]);
            frm.cRVMovimientosBancarios.ReportSource = rpt;
            frm.Show();
        }

        
        

     

        
    }
}
