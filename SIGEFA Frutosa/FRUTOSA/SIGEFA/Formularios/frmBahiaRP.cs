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
    public partial class frmBahiaRP : Form
    {
        public DataTable DTable;

        public frmBahiaRP()
        {
            InitializeComponent();
        }

        private void frmBahiaRP_Load(object sender, EventArgs e)
        {
            CRBahia CRep = new CRBahia();
            CRep.Load("CRBahia.rpt");
            CRep.SetDataSource(DTable);
            cRVBahia.ReportSource = CRep;
        }
    }
}
