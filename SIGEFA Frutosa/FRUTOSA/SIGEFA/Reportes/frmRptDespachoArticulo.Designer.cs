﻿namespace SIGEFA.Reportes
{
    partial class frmRptDespachoArticulo
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
            this.crvDespacho = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvDespacho
            // 
            this.crvDespacho.ActiveViewIndex = -1;
            this.crvDespacho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvDespacho.DisplayGroupTree = false;
            this.crvDespacho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvDespacho.Location = new System.Drawing.Point(0, 0);
            this.crvDespacho.Name = "crvDespacho";
            this.crvDespacho.SelectionFormula = "";
            this.crvDespacho.Size = new System.Drawing.Size(284, 262);
            this.crvDespacho.TabIndex = 0;
            this.crvDespacho.ViewTimeSelectionFormula = "";
            // 
            // frmRptDespachoArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.crvDespacho);
            this.Name = "frmRptDespachoArticulo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmRptDespachoArticulo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer crvDespacho;
    }
}