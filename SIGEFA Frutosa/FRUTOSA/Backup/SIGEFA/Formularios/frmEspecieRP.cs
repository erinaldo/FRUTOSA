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
    public partial class frmEspecieRP : Form
    {
        public DataTable DTable;
        public frmEspecieRP()
        {
            InitializeComponent();
        }

        private void frmEspecieRP_Load(object sender, EventArgs e)
        {
            CREspecie CRep = new CREspecie();
            CRep.Load("CREspecie.rpt");
            CRep.SetDataSource(DTable);
            cRVEspecie.ReportSource = CRep;
        }
    }
}
