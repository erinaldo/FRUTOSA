namespace SIGEFA.Reportes
{
    partial class frmListaIngresosCamara
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.crvIngresosCamara = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvIngresosCamara
            // 
            this.crvIngresosCamara.ActiveViewIndex = -1;
            this.crvIngresosCamara.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvIngresosCamara.DisplayGroupTree = false;
            this.crvIngresosCamara.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvIngresosCamara.Location = new System.Drawing.Point(0, 0);
            this.crvIngresosCamara.Name = "crvIngresosCamara";
            this.crvIngresosCamara.SelectionFormula = "";
            this.crvIngresosCamara.Size = new System.Drawing.Size(284, 262);
            this.crvIngresosCamara.TabIndex = 0;
            this.crvIngresosCamara.ViewTimeSelectionFormula = "";
            // 
            // frmListaIngresosCamara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.crvIngresosCamara);
            this.Name = "frmListaIngresosCamara";
            this.Text = "Ingresos de Camaras";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmListaNotasCredito_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvIngresosCamara;
       // public CrystalDecisions.Windows.Forms.CrystalReportViewer crvICamara;

    }
}