namespace SIGEFA.Formularios
{
    partial class frmBahiaRP
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
            this.cRVBahia = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cRVBahia
            // 
            this.cRVBahia.ActiveViewIndex = -1;
            this.cRVBahia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cRVBahia.DisplayGroupTree = false;
            this.cRVBahia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cRVBahia.Location = new System.Drawing.Point(0, 0);
            this.cRVBahia.Name = "cRVBahia";
            this.cRVBahia.SelectionFormula = "";
            this.cRVBahia.Size = new System.Drawing.Size(769, 334);
            this.cRVBahia.TabIndex = 0;
            this.cRVBahia.ViewTimeSelectionFormula = "";
            // 
            // frmBahiaRP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 334);
            this.Controls.Add(this.cRVBahia);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBahiaRP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBahiaRP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer cRVBahia;
    }
}