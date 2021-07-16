﻿namespace SIGEFA.Formularios
{
    partial class frmBuscarGuias
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBuscarGuias));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnBuscar = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFecha2 = new System.Windows.Forms.DateTimePicker();
            this.dtpFecha1 = new System.Windows.Forms.DateTimePicker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.muestraPagosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.canjearPorLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.nuevaLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modificarLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imprimirLetraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingresoABancoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgvGuiasRemision = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaemision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.facturado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadofacturado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuiasRemision)).BeginInit();
            this.SuspendLayout();
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
            this.imageList1.Images.SetKeyName(16, "folder_open (1).png");
            this.imageList1.Images.SetKeyName(17, "folder-open-icon.png");
            this.imageList1.Images.SetKeyName(18, "Glossy-Open-icon.png");
            this.imageList1.Images.SetKeyName(19, "Ocean Blue Open.png");
            this.imageList1.Images.SetKeyName(20, "Open (1).png");
            this.imageList1.Images.SetKeyName(21, "open_folder_green.png");
            // 
            // btnBuscar
            // 
            this.btnBuscar.ImageIndex = 11;
            this.btnBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.SubItemsExpandWidth = 14;
            this.btnBuscar.Text = "Buscar";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ImageIndex = 11;
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItemsExpandWidth = 14;
            this.buttonItem1.Text = "Buscar";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtFiltro);
            this.groupBox1.Controls.Add(this.btnBusqueda);
            this.groupBox1.Controls.Add(this.btnReporte);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtpFecha2);
            this.groupBox1.Controls.Add(this.dtpFecha1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(957, 59);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label6.Location = new System.Drawing.Point(759, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "x";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(222, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "Por :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(257, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "X";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.SteelBlue;
            this.label10.Location = new System.Drawing.Point(184, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 12);
            this.label10.TabIndex = 34;
            this.label10.Text = "Filtro";
            // 
            // txtFiltro
            // 
            this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtFiltro.Location = new System.Drawing.Point(186, 30);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(207, 20);
            this.txtFiltro.TabIndex = 33;
            this.txtFiltro.TextChanged += new System.EventHandler(this.txtFiltro_TextChanged);
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.ImageIndex = 11;
            this.btnBusqueda.ImageList = this.imageList1;
            this.btnBusqueda.Location = new System.Drawing.Point(789, 17);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(78, 33);
            this.btnBusqueda.TabIndex = 30;
            this.btnBusqueda.Text = "Buscar";
            this.btnBusqueda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // btnReporte
            // 
            this.btnReporte.ImageIndex = 7;
            this.btnReporte.ImageList = this.imageList1;
            this.btnReporte.Location = new System.Drawing.Point(873, 17);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(78, 33);
            this.btnReporte.TabIndex = 29;
            this.btnReporte.Text = "Reporte";
            this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReporte.UseVisualStyleBackColor = true;
            this.btnReporte.Click += new System.EventHandler(this.btnReporte_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.SteelBlue;
            this.label2.Location = new System.Drawing.Point(96, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 26;
            this.label2.Text = "Hasta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(9, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "Desde";
            // 
            // dtpFecha2
            // 
            this.dtpFecha2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha2.Location = new System.Drawing.Point(99, 30);
            this.dtpFecha2.Name = "dtpFecha2";
            this.dtpFecha2.Size = new System.Drawing.Size(81, 20);
            this.dtpFecha2.TabIndex = 22;
            // 
            // dtpFecha1
            // 
            this.dtpFecha1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha1.Location = new System.Drawing.Point(12, 30);
            this.dtpFecha1.Name = "dtpFecha1";
            this.dtpFecha1.Size = new System.Drawing.Size(81, 20);
            this.dtpFecha1.TabIndex = 21;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.muestraPagosToolStripMenuItem,
            this.canjearPorLetraToolStripMenuItem,
            this.toolStripSeparator1,
            this.nuevaLetraToolStripMenuItem,
            this.modificarLetraToolStripMenuItem,
            this.imprimirLetraToolStripMenuItem,
            this.ingresoABancoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(165, 142);
            // 
            // muestraPagosToolStripMenuItem
            // 
            this.muestraPagosToolStripMenuItem.Name = "muestraPagosToolStripMenuItem";
            this.muestraPagosToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.muestraPagosToolStripMenuItem.Text = "Muestra Pagos";
            // 
            // canjearPorLetraToolStripMenuItem
            // 
            this.canjearPorLetraToolStripMenuItem.Name = "canjearPorLetraToolStripMenuItem";
            this.canjearPorLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.canjearPorLetraToolStripMenuItem.Text = "Canjear por Letra";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // nuevaLetraToolStripMenuItem
            // 
            this.nuevaLetraToolStripMenuItem.Name = "nuevaLetraToolStripMenuItem";
            this.nuevaLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.nuevaLetraToolStripMenuItem.Text = "Nueva Letra";
            // 
            // modificarLetraToolStripMenuItem
            // 
            this.modificarLetraToolStripMenuItem.Name = "modificarLetraToolStripMenuItem";
            this.modificarLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.modificarLetraToolStripMenuItem.Text = "Modificar Letra";
            // 
            // imprimirLetraToolStripMenuItem
            // 
            this.imprimirLetraToolStripMenuItem.Name = "imprimirLetraToolStripMenuItem";
            this.imprimirLetraToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.imprimirLetraToolStripMenuItem.Text = "Imprimir Letra";
            // 
            // ingresoABancoToolStripMenuItem
            // 
            this.ingresoABancoToolStripMenuItem.Name = "ingresoABancoToolStripMenuItem";
            this.ingresoABancoToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.ingresoABancoToolStripMenuItem.Text = "Ingreso a banco";
            // 
            // dgvGuiasRemision
            // 
            this.dgvGuiasRemision.AllowUserToAddRows = false;
            this.dgvGuiasRemision.AllowUserToDeleteRows = false;
            this.dgvGuiasRemision.AllowUserToOrderColumns = true;
            this.dgvGuiasRemision.AllowUserToResizeRows = false;
            this.dgvGuiasRemision.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGuiasRemision.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.numdoc,
            this.factura,
            this.cliente,
            this.fecha,
            this.fechaemision,
            this.responsable,
            this.facturado,
            this.estadofacturado});
            this.dgvGuiasRemision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGuiasRemision.Location = new System.Drawing.Point(0, 59);
            this.dgvGuiasRemision.MultiSelect = false;
            this.dgvGuiasRemision.Name = "dgvGuiasRemision";
            this.dgvGuiasRemision.ReadOnly = true;
            this.dgvGuiasRemision.RowHeadersVisible = false;
            this.dgvGuiasRemision.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGuiasRemision.Size = new System.Drawing.Size(957, 415);
            this.dgvGuiasRemision.TabIndex = 8;
            this.dgvGuiasRemision.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGuiasRemision_ColumnHeaderMouseClick);
            this.dgvGuiasRemision.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvGuiasRemision_CellMouseDoubleClick);
            // 
            // codigo
            // 
            this.codigo.DataPropertyName = "codGuiaRemision";
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.codigo.Width = 80;
            // 
            // numdoc
            // 
            this.numdoc.DataPropertyName = "numdoc";
            this.numdoc.HeaderText = "N° Doc.";
            this.numdoc.Name = "numdoc";
            this.numdoc.ReadOnly = true;
            // 
            // factura
            // 
            this.factura.DataPropertyName = "factura";
            this.factura.HeaderText = "Boleta/Factura";
            this.factura.Name = "factura";
            this.factura.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.DataPropertyName = "cliente";
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            this.cliente.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.cliente.Width = 270;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha";
            this.fecha.HeaderText = "F. Traslado";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.fecha.Width = 120;
            // 
            // fechaemision
            // 
            this.fechaemision.DataPropertyName = "fechaemision";
            this.fechaemision.HeaderText = "F. Emision";
            this.fechaemision.Name = "fechaemision";
            this.fechaemision.ReadOnly = true;
            this.fechaemision.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.fechaemision.Width = 120;
            // 
            // responsable
            // 
            this.responsable.DataPropertyName = "responsable";
            this.responsable.HeaderText = "Responsable";
            this.responsable.Name = "responsable";
            this.responsable.ReadOnly = true;
            this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.responsable.Width = 180;
            // 
            // facturado
            // 
            this.facturado.DataPropertyName = "facturado";
            this.facturado.HeaderText = "facturado";
            this.facturado.Name = "facturado";
            this.facturado.ReadOnly = true;
            this.facturado.Visible = false;
            // 
            // estadofacturado
            // 
            this.estadofacturado.DataPropertyName = "estadofacturado";
            this.estadofacturado.HeaderText = "Estado";
            this.estadofacturado.Name = "estadofacturado";
            this.estadofacturado.ReadOnly = true;
            // 
            // frmBuscarGuias
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 474);
            this.Controls.Add(this.dgvGuiasRemision);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "frmBuscarGuias";
            this.ShowInTaskbar = false;
            this.Text = "Buscar Guias";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBuscarGuias_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuiasRemision)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonItem btnBuscar;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFecha2;
        private System.Windows.Forms.DateTimePicker dtpFecha1;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Button btnBusqueda;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem muestraPagosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem canjearPorLetraToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem nuevaLetraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modificarLetraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imprimirLetraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ingresoABancoToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvGuiasRemision;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn numdoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn factura;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaemision;
        private System.Windows.Forms.DataGridViewTextBoxColumn responsable;
        private System.Windows.Forms.DataGridViewTextBoxColumn facturado;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadofacturado;
    }
}