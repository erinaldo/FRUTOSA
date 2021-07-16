﻿namespace SIGEFA.Formularios
{
    partial class frmRequerimientosVigentes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRequerimientosVigentes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvOrdenes = new System.Windows.Forms.DataGridView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAnular = new System.Windows.Forms.Button();
            this.btnIrCotizacion = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.btnConsultar = new System.Windows.Forms.Button();
            this.cmbAlmacen = new System.Windows.Forms.ComboBox();
            this.lbalmacen = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFiltro = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnrechazar = new System.Windows.Forms.Button();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeracion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AlmacenD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codAlmacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.responsable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Atendido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.almacendestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComentarioRechazado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvOrdenes);
            this.groupBox1.Location = new System.Drawing.Point(1, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(911, 360);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dgvOrdenes
            // 
            this.dgvOrdenes.AllowUserToAddRows = false;
            this.dgvOrdenes.AllowUserToDeleteRows = false;
            this.dgvOrdenes.AllowUserToOrderColumns = true;
            this.dgvOrdenes.AllowUserToResizeRows = false;
            this.dgvOrdenes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvOrdenes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.Documento,
            this.serie,
            this.numeracion,
            this.fecha,
            this.Almacen,
            this.AlmacenD,
            this.codAlmacen,
            this.responsable,
            this.Atendido,
            this.Estado,
            this.almacendestino,
            this.ComentarioRechazado});
            this.dgvOrdenes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrdenes.Location = new System.Drawing.Point(3, 16);
            this.dgvOrdenes.MultiSelect = false;
            this.dgvOrdenes.Name = "dgvOrdenes";
            this.dgvOrdenes.ReadOnly = true;
            this.dgvOrdenes.RowHeadersVisible = false;
            this.dgvOrdenes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrdenes.Size = new System.Drawing.Size(905, 341);
            this.dgvOrdenes.TabIndex = 0;
            this.dgvOrdenes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCotizaciones_CellDoubleClick);
            this.dgvOrdenes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvOrdenes_ColumnHeaderMouseClick);
            this.dgvOrdenes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvCotizaciones_RowStateChanged);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "exit.png");
            this.imageList1.Images.SetKeyName(1, "pedido.png");
            this.imageList1.Images.SetKeyName(2, "carrito.png");
            this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
            this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
            this.imageList1.Images.SetKeyName(5, "document_delete.png");
            // 
            // btnAnular
            // 
            this.btnAnular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnular.ImageIndex = 4;
            this.btnAnular.ImageList = this.imageList1;
            this.btnAnular.Location = new System.Drawing.Point(221, 11);
            this.btnAnular.Name = "btnAnular";
            this.btnAnular.Size = new System.Drawing.Size(96, 37);
            this.btnAnular.TabIndex = 4;
            this.btnAnular.Text = "Anular ";
            this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAnular.UseVisualStyleBackColor = true;
            this.btnAnular.Click += new System.EventHandler(this.btnAnular_Click);
            // 
            // btnIrCotizacion
            // 
            this.btnIrCotizacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIrCotizacion.ImageIndex = 1;
            this.btnIrCotizacion.ImageList = this.imageList1;
            this.btnIrCotizacion.Location = new System.Drawing.Point(323, 11);
            this.btnIrCotizacion.Name = "btnIrCotizacion";
            this.btnIrCotizacion.Size = new System.Drawing.Size(111, 37);
            this.btnIrCotizacion.TabIndex = 2;
            this.btnIrCotizacion.Text = "Ir Requerimiento";
            this.btnIrCotizacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnIrCotizacion.UseVisualStyleBackColor = true;
            this.btnIrCotizacion.Click += new System.EventHandler(this.btnIrCotizacion_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.ImageIndex = 0;
            this.btnSalir.ImageList = this.imageList1;
            this.btnSalir.Location = new System.Drawing.Point(443, 10);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 37);
            this.btnSalir.TabIndex = 1;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Add Green Button.png");
            this.imageList2.Images.SetKeyName(1, "Add.png");
            this.imageList2.Images.SetKeyName(2, "Remove.png");
            this.imageList2.Images.SetKeyName(3, "Write Document.png");
            this.imageList2.Images.SetKeyName(4, "New Document.png");
            this.imageList2.Images.SetKeyName(5, "Remove Document.png");
            this.imageList2.Images.SetKeyName(6, "1328102023_Copy.png");
            this.imageList2.Images.SetKeyName(7, "document-print.png");
            this.imageList2.Images.SetKeyName(8, "g-icon-new-update.png");
            this.imageList2.Images.SetKeyName(9, "refresh_256.png");
            this.imageList2.Images.SetKeyName(10, "Refresh-icon.png");
            this.imageList2.Images.SetKeyName(11, "search (1).png");
            this.imageList2.Images.SetKeyName(12, "search (5).png");
            this.imageList2.Images.SetKeyName(13, "search (6).png");
            this.imageList2.Images.SetKeyName(14, "search (8).png");
            this.imageList2.Images.SetKeyName(15, "search_top.png");
            this.imageList2.Images.SetKeyName(16, "icon-47203_640.png");
            this.imageList2.Images.SetKeyName(17, "Folder open.png");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.SteelBlue;
            this.label6.Location = new System.Drawing.Point(494, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Hasta :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(491, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Desde :";
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(544, 8);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(100, 20);
            this.dtpDesde.TabIndex = 15;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(548, 34);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(100, 20);
            this.dtpHasta.TabIndex = 14;
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "Write Document.png");
            this.imageList3.Images.SetKeyName(1, "New Document.png");
            this.imageList3.Images.SetKeyName(2, "Remove Document.png");
            this.imageList3.Images.SetKeyName(3, "document-print.png");
            this.imageList3.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
            this.imageList3.Images.SetKeyName(5, "exit.png");
            this.imageList3.Images.SetKeyName(6, "search (1).png");
            this.imageList3.Images.SetKeyName(7, "Glossy-Open-icon.png");
            this.imageList3.Images.SetKeyName(8, "folder-open-icon (1).png");
            this.imageList3.Images.SetKeyName(9, "document_delete.png");
            this.imageList3.Images.SetKeyName(10, "DeleteRed.png");
            this.imageList3.Images.SetKeyName(11, "OK_Verde.png");
            this.imageList3.Images.SetKeyName(12, "Remove.png");
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConsultar.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConsultar.ImageIndex = 6;
            this.btnConsultar.ImageList = this.imageList3;
            this.btnConsultar.Location = new System.Drawing.Point(707, 19);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(105, 35);
            this.btnConsultar.TabIndex = 19;
            this.btnConsultar.Text = " Consultar";
            this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // cmbAlmacen
            // 
            this.cmbAlmacen.FormattingEnabled = true;
            this.cmbAlmacen.Location = new System.Drawing.Point(208, 8);
            this.cmbAlmacen.Name = "cmbAlmacen";
            this.cmbAlmacen.Size = new System.Drawing.Size(204, 21);
            this.cmbAlmacen.TabIndex = 1;
            // 
            // lbalmacen
            // 
            this.lbalmacen.AutoSize = true;
            this.lbalmacen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbalmacen.ForeColor = System.Drawing.Color.SteelBlue;
            this.lbalmacen.Location = new System.Drawing.Point(0, 11);
            this.lbalmacen.Name = "lbalmacen";
            this.lbalmacen.Size = new System.Drawing.Size(59, 13);
            this.lbalmacen.TabIndex = 2;
            this.lbalmacen.Text = "Almacen:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtFiltro);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lbalmacen);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnConsultar);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.cmbAlmacen);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.dtpHasta);
            this.groupBox2.Controls.Add(this.dtpDesde);
            this.groupBox2.Location = new System.Drawing.Point(12, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(886, 71);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "x";
            this.label2.Visible = false;
            // 
            // txtFiltro
            // 
            this.txtFiltro.Location = new System.Drawing.Point(208, 33);
            this.txtFiltro.Name = "txtFiltro";
            this.txtFiltro.Size = new System.Drawing.Size(204, 20);
            this.txtFiltro.TabIndex = 1;
            this.txtFiltro.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(44, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 39;
            this.label1.Text = "Por :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(79, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 12);
            this.label7.TabIndex = 38;
            this.label7.Text = "X";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.SteelBlue;
            this.label10.Location = new System.Drawing.Point(6, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 12);
            this.label10.TabIndex = 37;
            this.label10.Text = "Filtro";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnrechazar);
            this.groupBox3.Controls.Add(this.btnEnviar);
            this.groupBox3.Controls.Add(this.btnAnular);
            this.groupBox3.Controls.Add(this.btnSalir);
            this.groupBox3.Controls.Add(this.btnIrCotizacion);
            this.groupBox3.Location = new System.Drawing.Point(220, 447);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(524, 54);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // btnrechazar
            // 
            this.btnrechazar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnrechazar.ImageIndex = 12;
            this.btnrechazar.ImageList = this.imageList3;
            this.btnrechazar.Location = new System.Drawing.Point(119, 11);
            this.btnrechazar.Name = "btnrechazar";
            this.btnrechazar.Size = new System.Drawing.Size(96, 37);
            this.btnrechazar.TabIndex = 6;
            this.btnrechazar.Text = "Rechazar";
            this.btnrechazar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnrechazar.UseVisualStyleBackColor = true;
            this.btnrechazar.Click += new System.EventHandler(this.btnrechazar_Click);
            // 
            // btnEnviar
            // 
            this.btnEnviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnviar.ImageIndex = 17;
            this.btnEnviar.ImageList = this.imageList2;
            this.btnEnviar.Location = new System.Drawing.Point(17, 10);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(96, 37);
            this.btnEnviar.TabIndex = 5;
            this.btnEnviar.Text = "Enviar ";
            this.btnEnviar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.button1_Click);
            // 
            // codigo
            // 
            this.codigo.DataPropertyName = "codRequer";
            this.codigo.HeaderText = "Codigo";
            this.codigo.Name = "codigo";
            this.codigo.ReadOnly = true;
            this.codigo.Visible = false;
            this.codigo.Width = 150;
            // 
            // Documento
            // 
            this.Documento.DataPropertyName = "documento";
            this.Documento.HeaderText = "Documento";
            this.Documento.Name = "Documento";
            this.Documento.ReadOnly = true;
            // 
            // serie
            // 
            this.serie.DataPropertyName = "serie";
            this.serie.HeaderText = "Serie";
            this.serie.Name = "serie";
            this.serie.ReadOnly = true;
            this.serie.Visible = false;
            // 
            // numeracion
            // 
            this.numeracion.DataPropertyName = "numeracion";
            this.numeracion.HeaderText = "Numeracion";
            this.numeracion.Name = "numeracion";
            this.numeracion.ReadOnly = true;
            this.numeracion.Visible = false;
            // 
            // fecha
            // 
            this.fecha.DataPropertyName = "fecha";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.fecha.DefaultCellStyle = dataGridViewCellStyle1;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.fecha.Width = 80;
            // 
            // Almacen
            // 
            this.Almacen.DataPropertyName = "nombre";
            this.Almacen.HeaderText = "Almacen Origen";
            this.Almacen.Name = "Almacen";
            this.Almacen.ReadOnly = true;
            this.Almacen.Width = 150;
            // 
            // AlmacenD
            // 
            this.AlmacenD.DataPropertyName = "nombrealmacenDestino";
            this.AlmacenD.HeaderText = "Almacen Destino";
            this.AlmacenD.Name = "AlmacenD";
            this.AlmacenD.ReadOnly = true;
            this.AlmacenD.Width = 150;
            // 
            // codAlmacen
            // 
            this.codAlmacen.DataPropertyName = "codAlmacen";
            this.codAlmacen.HeaderText = "codAlmacen";
            this.codAlmacen.Name = "codAlmacen";
            this.codAlmacen.ReadOnly = true;
            this.codAlmacen.Visible = false;
            // 
            // responsable
            // 
            this.responsable.DataPropertyName = "responsable";
            this.responsable.HeaderText = "Responsable Origen";
            this.responsable.Name = "responsable";
            this.responsable.ReadOnly = true;
            this.responsable.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.responsable.Width = 200;
            // 
            // Atendido
            // 
            this.Atendido.DataPropertyName = "Atendido";
            this.Atendido.HeaderText = "Estado";
            this.Atendido.Name = "Atendido";
            this.Atendido.ReadOnly = true;
            this.Atendido.Width = 220;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "Estado";
            this.Estado.HeaderText = "Anulado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 50;
            // 
            // almacendestino
            // 
            this.almacendestino.DataPropertyName = "almacendestino";
            this.almacendestino.HeaderText = "almacendestino";
            this.almacendestino.Name = "almacendestino";
            this.almacendestino.ReadOnly = true;
            this.almacendestino.Visible = false;
            // 
            // ComentarioRechazado
            // 
            this.ComentarioRechazado.DataPropertyName = "ComentarioRechazado";
            this.ComentarioRechazado.HeaderText = "Razón de Rechazado";
            this.ComentarioRechazado.Name = "ComentarioRechazado";
            this.ComentarioRechazado.ReadOnly = true;
            this.ComentarioRechazado.Width = 250;
            // 
            // frmRequerimientosVigentes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 506);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRequerimientosVigentes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista Requerimientos";
            this.Load += new System.EventHandler(this.frmRequerimientosVigentes_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrdenes)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvOrdenes;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnIrCotizacion;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnAnular;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label lbalmacen;
        private System.Windows.Forms.ComboBox cmbAlmacen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFiltro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Button btnrechazar;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeracion;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Almacen;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlmacenD;
        private System.Windows.Forms.DataGridViewTextBoxColumn codAlmacen;
        private System.Windows.Forms.DataGridViewTextBoxColumn responsable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Atendido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn almacendestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComentarioRechazado;
    }
}