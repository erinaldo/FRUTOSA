﻿namespace SIGEFA.Reportes
{
    partial class frmParamVentxCliente
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbMoneda = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbFormaPago = new System.Windows.Forms.ComboBox();
            this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
            this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCodCli = new System.Windows.Forms.TextBox();
            this.txtCliente = new System.Windows.Forms.TextBox();
            this.txtUnCli = new System.Windows.Forms.TextBox();
            this.rbCli = new System.Windows.Forms.RadioButton();
            this.rbTodosCli = new System.Windows.Forms.RadioButton();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtCodProd = new System.Windows.Forms.TextBox();
            this.txtArticulo = new System.Windows.Forms.TextBox();
            this.txtUnArt = new System.Windows.Forms.TextBox();
            this.rbArt = new System.Windows.Forms.RadioButton();
            this.rbTodosArt = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cmbMoneda);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbFormaPago);
            this.groupBox1.Controls.Add(this.dtpFecha2);
            this.groupBox1.Controls.Add(this.dtpFecha1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 102);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(17, 58);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 12);
            this.label7.TabIndex = 47;
            this.label7.Text = "Moneda";
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(19, 73);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(99, 20);
            this.cmbMoneda.TabIndex = 46;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.SteelBlue;
            this.label8.Location = new System.Drawing.Point(235, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 45;
            this.label8.Text = "Hasta";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(235, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "Forma de pago";
            // 
            // cmbFormaPago
            // 
            this.cmbFormaPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFormaPago.FormattingEnabled = true;
            this.cmbFormaPago.Location = new System.Drawing.Point(237, 73);
            this.cmbFormaPago.Name = "cmbFormaPago";
            this.cmbFormaPago.Size = new System.Drawing.Size(239, 20);
            this.cmbFormaPago.TabIndex = 42;
            // 
            // dtpFecha2
            // 
            this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha2.Location = new System.Drawing.Point(237, 31);
            this.dtpFecha2.Name = "dtpFecha2";
            this.dtpFecha2.Size = new System.Drawing.Size(99, 20);
            this.dtpFecha2.TabIndex = 28;
            // 
            // dtpFecha1
            // 
            this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha1.Location = new System.Drawing.Point(19, 31);
            this.dtpFecha1.Name = "dtpFecha1";
            this.dtpFecha1.Size = new System.Drawing.Size(99, 20);
            this.dtpFecha1.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "Desde";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodCli);
            this.groupBox2.Controls.Add(this.txtCliente);
            this.groupBox2.Controls.Add(this.txtUnCli);
            this.groupBox2.Controls.Add(this.rbCli);
            this.groupBox2.Controls.Add(this.rbTodosCli);
            this.groupBox2.Location = new System.Drawing.Point(12, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(482, 67);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Por Cliente";
            // 
            // txtCodCli
            // 
            this.txtCodCli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodCli.Enabled = false;
            this.txtCodCli.Location = new System.Drawing.Point(446, 39);
            this.txtCodCli.Name = "txtCodCli";
            this.txtCodCli.Size = new System.Drawing.Size(30, 20);
            this.txtCodCli.TabIndex = 69;
            this.txtCodCli.Visible = false;
            // 
            // txtCliente
            // 
            this.txtCliente.Enabled = false;
            this.txtCliente.Location = new System.Drawing.Point(194, 39);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.Size = new System.Drawing.Size(248, 20);
            this.txtCliente.TabIndex = 62;
            // 
            // txtUnCli
            // 
            this.txtUnCli.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnCli.Enabled = false;
            this.txtUnCli.Location = new System.Drawing.Point(104, 39);
            this.txtUnCli.Name = "txtUnCli";
            this.txtUnCli.Size = new System.Drawing.Size(84, 20);
            this.txtUnCli.TabIndex = 61;
            this.txtUnCli.TextChanged += new System.EventHandler(this.txtUnCli_TextChanged);
            this.txtUnCli.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnCli_KeyDown);
            this.txtUnCli.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnCli_KeyPress);
            // 
            // rbCli
            // 
            this.rbCli.AutoSize = true;
            this.rbCli.BackColor = System.Drawing.Color.Transparent;
            this.rbCli.Location = new System.Drawing.Point(19, 42);
            this.rbCli.Name = "rbCli";
            this.rbCli.Size = new System.Drawing.Size(74, 17);
            this.rbCli.TabIndex = 57;
            this.rbCli.Text = "Un Cliente";
            this.rbCli.UseVisualStyleBackColor = false;
            this.rbCli.CheckedChanged += new System.EventHandler(this.rbCli_CheckedChanged);
            // 
            // rbTodosCli
            // 
            this.rbTodosCli.AutoSize = true;
            this.rbTodosCli.BackColor = System.Drawing.Color.Transparent;
            this.rbTodosCli.Checked = true;
            this.rbTodosCli.Location = new System.Drawing.Point(19, 19);
            this.rbTodosCli.Name = "rbTodosCli";
            this.rbTodosCli.Size = new System.Drawing.Size(110, 17);
            this.rbTodosCli.TabIndex = 54;
            this.rbTodosCli.TabStop = true;
            this.rbTodosCli.Text = "Todos los clientes";
            this.rbTodosCli.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.ImageIndex = 0;
            this.btnCancelar.Location = new System.Drawing.Point(389, 266);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnReporte
            // 
            this.btnReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReporte.ImageIndex = 1;
            this.btnReporte.Location = new System.Drawing.Point(308, 266);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(75, 23);
            this.btnReporte.TabIndex = 13;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCodProd);
            this.groupBox4.Controls.Add(this.txtArticulo);
            this.groupBox4.Controls.Add(this.txtUnArt);
            this.groupBox4.Controls.Add(this.rbArt);
            this.groupBox4.Controls.Add(this.rbTodosArt);
            this.groupBox4.Location = new System.Drawing.Point(12, 193);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(482, 67);
            this.groupBox4.TabIndex = 62;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Por Artículo";
            // 
            // txtCodProd
            // 
            this.txtCodProd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodProd.Enabled = false;
            this.txtCodProd.Location = new System.Drawing.Point(446, 39);
            this.txtCodProd.Name = "txtCodProd";
            this.txtCodProd.Size = new System.Drawing.Size(30, 20);
            this.txtCodProd.TabIndex = 69;
            this.txtCodProd.Visible = false;
            // 
            // txtArticulo
            // 
            this.txtArticulo.Enabled = false;
            this.txtArticulo.Location = new System.Drawing.Point(194, 39);
            this.txtArticulo.Name = "txtArticulo";
            this.txtArticulo.Size = new System.Drawing.Size(248, 20);
            this.txtArticulo.TabIndex = 63;
            // 
            // txtUnArt
            // 
            this.txtUnArt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUnArt.Enabled = false;
            this.txtUnArt.Location = new System.Drawing.Point(104, 39);
            this.txtUnArt.Name = "txtUnArt";
            this.txtUnArt.Size = new System.Drawing.Size(84, 20);
            this.txtUnArt.TabIndex = 61;
            this.txtUnArt.TextChanged += new System.EventHandler(this.txtUnArt_TextChanged);
            this.txtUnArt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnArt_KeyDown);
            this.txtUnArt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnArt_KeyPress);
            // 
            // rbArt
            // 
            this.rbArt.AutoSize = true;
            this.rbArt.BackColor = System.Drawing.Color.Transparent;
            this.rbArt.Location = new System.Drawing.Point(19, 42);
            this.rbArt.Name = "rbArt";
            this.rbArt.Size = new System.Drawing.Size(79, 17);
            this.rbArt.TabIndex = 57;
            this.rbArt.Text = "Un Artículo";
            this.rbArt.UseVisualStyleBackColor = false;
            this.rbArt.CheckedChanged += new System.EventHandler(this.rbArt_CheckedChanged);
            // 
            // rbTodosArt
            // 
            this.rbTodosArt.AutoSize = true;
            this.rbTodosArt.BackColor = System.Drawing.Color.Transparent;
            this.rbTodosArt.Checked = true;
            this.rbTodosArt.Location = new System.Drawing.Point(19, 19);
            this.rbTodosArt.Name = "rbTodosArt";
            this.rbTodosArt.Size = new System.Drawing.Size(115, 17);
            this.rbTodosArt.TabIndex = 54;
            this.rbTodosArt.TabStop = true;
            this.rbTodosArt.Text = "Todos los artículos";
            this.rbTodosArt.UseVisualStyleBackColor = false;
            // 
            // frmParamVentxCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(506, 294);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnReporte);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmParamVentxCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reporte de Ventas por Cliente / Articulo";
            this.Load += new System.EventHandler(this.frmParamVentxCliente_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFormaPago;
        private System.Windows.Forms.DateTimePicker dtpFecha1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpFecha2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbMoneda;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.RadioButton rbTodosCli;
        private System.Windows.Forms.RadioButton rbCli;
        public System.Windows.Forms.TextBox txtUnCli;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.TextBox txtUnArt;
        private System.Windows.Forms.RadioButton rbArt;
        private System.Windows.Forms.RadioButton rbTodosArt;
        public System.Windows.Forms.TextBox txtCliente;
        public System.Windows.Forms.TextBox txtArticulo;
        public System.Windows.Forms.TextBox txtCodCli;
        public System.Windows.Forms.TextBox txtCodProd;
    }
}