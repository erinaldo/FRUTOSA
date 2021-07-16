namespace SIGEFA.Formularios
{
    partial class frmPesadoresRP
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
            this.cRVPesadores = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // cRVPesadores
            // 
            this.cRVPesadores.ActiveViewIndex = -1;
            this.cRVPesadores.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cRVPesadores.Cursor = System.Windows.Forms.Cursors.Default;
            this.cRVPesadores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cRVPesadores.Location = new System.Drawing.Point(0, 0);
            this.cRVPesadores.Name = "cRVPesadores";
            this.cRVPesadores.SelectionFormula = "";
            this.cRVPesadores.Size = new System.Drawing.Size(655, 313);
            this.cRVPesadores.TabIndex = 0;
            this.cRVPesadores.ViewTimeSelectionFormula = "";
            // 
            // frmPesadoresRP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 313);
            this.Controls.Add(this.cRVPesadores);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmPesadoresRP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPesadoresRP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPesadoresRP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer cRVPesadores;
    }
}