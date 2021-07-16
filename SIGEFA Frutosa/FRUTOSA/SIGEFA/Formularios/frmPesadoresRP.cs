using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Reportes;

namespace SIGEFA.Formularios
{
    public partial class frmPesadoresRP : Form
    {
        public DataTable DTable;

        public frmPesadoresRP()
        {
            InitializeComponent();
        }

        private void frmPesadoresRP_Load(object sender, EventArgs e)
        {
            CRPesadores CRep = new CRPesadores();
            CRep.Load("CRPesadores.rpt");
            CRep.SetDataSource(DTable);
            cRVPesadores.ReportSource = CRep;
        }

       
    }
}
