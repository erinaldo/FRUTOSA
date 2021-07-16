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
    public partial class frmParamIngMatprima : DevComponents.DotNetBar.OfficeForm
    {
        clsReporteFlujoCaja ds = new clsReporteFlujoCaja();
        public Int32 CodProducto;

        public frmParamIngMatprima()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Visualizar Reporte
            //CRFlujoCaja rpt = new CRFlujoCaja();
            //frmReporteFlujoCaja frm = new frmReporteFlujoCaja();
            //rpt.SetDataSource(ds.ReporteFlujoCaja(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen).Tables[0]);
            ////rpt.SetDataSource(ds.ReportePagosFacturaVenta(dtpFecha1.Value, dtpFecha2.Value, frmLogin.iCodAlmacen).Tables[0]);
            //frm.cRVFlujoCaja.ReportSource = rpt;
            //frm.Show();

            CRLiquidacionCaja rpt1 = new CRLiquidacionCaja();
            frmRptLiquidacionCaja frm1 = new frmRptLiquidacionCaja();
            rpt1.SetDataSource(ds.ReportePagosFacturaVenta(frmLogin.iCodAlmacen).Tables[0]);
            frm1.cRVLiquidacionCaja.ReportSource = rpt1;
            frm1.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
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
                    frm.Procede = 23;
                    frm.ShowDialog();
                }

            }
        }

        private void frmParamIngMatprima_Load(object sender, EventArgs e)
        {

        }
    }
}
