namespace SIGEFA.Formularios
{
    partial class frmEspecieRP
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
            this.cRVEspecie = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cRVEspecie
            // 
            this.cRVEspecie.ActiveViewIndex = -1;
            this.cRVEspecie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cRVEspecie.DisplayGroupTree = false;
            this.cRVEspecie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cRVEspecie.Location = new System.Drawing.Point(0, 0);
            this.cRVEspecie.Name = "cRVEspecie";
            this.cRVEspecie.SelectionFormula = "";
            this.cRVEspecie.Size = new System.Drawing.Size(769, 334);
            this.cRVEspecie.TabIndex = 0;
            this.cRVEspecie.ViewTimeSelectionFormula = "";
            // 
            // frmEspecieRP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 334);
            this.Controls.Add(this.cRVEspecie);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEspecieRP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEspecieRP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer cRVEspecie;
    }
}