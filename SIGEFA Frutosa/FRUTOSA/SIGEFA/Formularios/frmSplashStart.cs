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
    public partial class frmSplashStart : Form
    {
        public String flashopen = "";

        public frmSplashStart()
        {
            InitializeComponent();
            timer1.Enabled = true;
            timer1.Interval = 6000;                        
        }

        private void frmSplashStart_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    axShockwaveFlash1.Movie = Environment.CurrentDirectory + flashopen;
            //}
            //catch (Exception ex)
            //{
            //    DevComponents.DotNetBar.MessageBoxEx.Show("Se encontró el siguiente problema: " + ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();            
        }

        private void frmSplashStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                mdi_Menu frm = new mdi_Menu();                
                frm.Show();
            }
        }
    }
}
