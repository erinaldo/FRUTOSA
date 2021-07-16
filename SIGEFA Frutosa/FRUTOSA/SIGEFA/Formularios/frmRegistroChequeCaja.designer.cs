﻿namespace SIGEFA.Formularios
{
    partial class frmRegistroChequeCaja
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRegistroChequeCaja));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtResponsable = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumCheque = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFechaLiquidacion = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMontoLiquidar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSucursal = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
            this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtResponsable);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtNumCheque);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpFechaLiquidacion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtMontoLiquidar);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbSucursal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(481, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nuevo Registro";
            // 
            // txtResponsable
            // 
            this.txtResponsable.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResponsable.Location = new System.Drawing.Point(132, 134);
            this.txtResponsable.Name = "txtResponsable";
            this.txtResponsable.Size = new System.Drawing.Size(334, 20);
            this.txtResponsable.TabIndex = 9;
            this.superValidator1.SetValidator1(this.txtResponsable, this.customValidator4);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(117, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Responsable de Envio:";
            // 
            // txtNumCheque
            // 
            this.txtNumCheque.Location = new System.Drawing.Point(132, 75);
            this.txtNumCheque.Name = "txtNumCheque";
            this.txtNumCheque.Size = new System.Drawing.Size(118, 20);
            this.txtNumCheque.TabIndex = 7;
            this.superValidator1.SetValidator1(this.txtNumCheque, this.customValidator3);
            this.txtNumCheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumCheque_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "N° Cheque de Liq:";
            // 
            // dtpFechaLiquidacion
            // 
            this.dtpFechaLiquidacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaLiquidacion.Location = new System.Drawing.Point(132, 105);
            this.dtpFechaLiquidacion.Name = "dtpFechaLiquidacion";
            this.dtpFechaLiquidacion.Size = new System.Drawing.Size(118, 20);
            this.dtpFechaLiquidacion.TabIndex = 5;
            this.dtpFechaLiquidacion.Value = new System.DateTime(2015, 4, 1, 9, 10, 20, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha:";
            // 
            // txtMontoLiquidar
            // 
            this.txtMontoLiquidar.Location = new System.Drawing.Point(132, 49);
            this.txtMontoLiquidar.Name = "txtMontoLiquidar";
            this.txtMontoLiquidar.Size = new System.Drawing.Size(118, 20);
            this.txtMontoLiquidar.TabIndex = 3;
            this.superValidator1.SetValidator1(this.txtMontoLiquidar, this.customValidator2);
            this.txtMontoLiquidar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMontoLiquidar_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Monto a Liquidar:";
            // 
            // cmbSucursal
            // 
            this.cmbSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSucursal.FormattingEnabled = true;
            this.cmbSucursal.Location = new System.Drawing.Point(132, 22);
            this.cmbSucursal.Name = "cmbSucursal";
            this.cmbSucursal.Size = new System.Drawing.Size(165, 21);
            this.cmbSucursal.TabIndex = 1;
            this.superValidator1.SetValidator1(this.cmbSucursal, this.customValidator1);
            this.cmbSucursal.SelectionChangeCommitted += new System.EventHandler(this.cmbSucursal_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sucursal:";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "400_F_3572.png");
            this.imageList2.Images.SetKeyName(1, "como-eliminar-el-acne.png");
            this.imageList2.Images.SetKeyName(2, "cancel-148744_640.png");
            this.imageList2.Images.SetKeyName(3, "Filter.png");
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
            this.imageList1.Images.SetKeyName(1, "Add.png");
            this.imageList1.Images.SetKeyName(2, "Remove.png");
            this.imageList1.Images.SetKeyName(3, "Write Document.png");
            this.imageList1.Images.SetKeyName(4, "New Document.png");
            this.imageList1.Images.SetKeyName(5, "Remove Document.png");
            this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
            this.imageList1.Images.SetKeyName(7, "document-print.png");
            this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
            this.imageList1.Images.SetKeyName(9, "refresh_256.png");
            this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
            this.imageList1.Images.SetKeyName(11, "search (1).png");
            this.imageList1.Images.SetKeyName(12, "search (5).png");
            this.imageList1.Images.SetKeyName(13, "search (6).png");
            this.imageList1.Images.SetKeyName(14, "search (8).png");
            this.imageList1.Images.SetKeyName(15, "search_top.png");
            this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
            this.imageList1.Images.SetKeyName(17, "Folder open.png");
            this.imageList1.Images.SetKeyName(18, "por-periodo-de-sesiones-icono-8745-96.png");
            this.imageList1.Images.SetKeyName(19, "egreso.png");
            this.imageList1.Images.SetKeyName(20, "ingreso.png");
            this.imageList1.Images.SetKeyName(21, "icon_shelfs.png");
            this.imageList1.Images.SetKeyName(22, "EXIT2.png");
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.ImageIndex = 3;
            this.btnGuardar.ImageList = this.imageList2;
            this.btnGuardar.Location = new System.Drawing.Point(145, 189);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(89, 34);
            this.btnGuardar.TabIndex = 35;
            this.btnGuardar.Text = "Guardar Registro";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.ImageKey = "icon_shelfs.png";
            this.btnCancelar.ImageList = this.imageList1;
            this.btnCancelar.Location = new System.Drawing.Point(262, 189);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(86, 34);
            this.btnCancelar.TabIndex = 36;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // superValidator1
            // 
            this.superValidator1.ErrorProvider = this.errorProvider1;
            this.superValidator1.Highlighter = this.highlighter1;
            // 
            // customValidator4
            // 
            this.customValidator4.ErrorMessage = "Ingrese Responsable";
            this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator4_ValidateValue);
            // 
            // customValidator3
            // 
            this.customValidator3.ErrorMessage = "Ingrese N° Cheque";
            this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator3_ValidateValue);
            // 
            // customValidator2
            // 
            this.customValidator2.ErrorMessage = "Ingrese un Monto";
            this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator2_ValidateValue);
            // 
            // customValidator1
            // 
            this.customValidator1.ErrorMessage = "Seleccione una Sucursal";
            this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator1_ValidateValue);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider1.Icon")));
            // 
            // highlighter1
            // 
            this.highlighter1.ContainerControl = this;
            // 
            // frmRegistroChequeCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 231);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "frmRegistroChequeCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegitroChequeCaja";
            this.Load += new System.EventHandler(this.frmRegitroChequeCaja_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSucursal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaLiquidacion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMontoLiquidar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumCheque;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtResponsable;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private DevComponents.DotNetBar.Validator.SuperValidator superValidator1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator4;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator3;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator2;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator1;

    }
}