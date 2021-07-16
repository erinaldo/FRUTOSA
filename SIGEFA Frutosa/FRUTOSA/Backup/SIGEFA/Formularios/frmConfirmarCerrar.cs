using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SIGEFA.Formularios
{
    public partial class frmConfirmarCerrar : DevComponents.DotNetBar.OfficeForm
    {
        public DateTime fechaFinal;
        public DateTime horaFinal;
        public bool opcion = false;
        public frmConfirmarCerrar()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            fechaFinal = dtFechaFinal.Value;
            horaFinal = dtpHoraFin.Value;
            opcion = true;
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            opcion = false;
            this.Close();
        }

        private void frmConfirmarCerrar_Load(object sender, EventArgs e)
        {
                    
        }
    }
}
