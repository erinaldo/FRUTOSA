﻿namespace SIGEFA.Reportes
{
    partial class frmListaNotasCredito
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
            this.crvNotasCredito = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvNotasCredito
            // 
            this.crvNotasCredito.ActiveViewIndex = -1;
            this.crvNotasCredito.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvNotasCredito.DisplayGroupTree = false;
            this.crvNotasCredito.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvNotasCredito.Location = new System.Drawing.Point(0, 0);
            this.crvNotasCredito.Name = "crvNotasCredito";
            this.crvNotasCredito.SelectionFormula = "";
            this.crvNotasCredito.Size = new System.Drawing.Size(284, 262);
            this.crvNotasCredito.TabIndex = 0;
            this.crvNotasCredito.ViewTimeSelectionFormula = "";
            // 
            // frmListaNotasCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.crvNotasCredito);
            this.Name = "frmListaNotasCredito";
            this.Text = "frmListaNotasCredito";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvNotasCredito;

    }
}