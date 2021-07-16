namespace SIGEFA.Formularios
{
    partial class frmNotaOrdenAlmacen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotaOrdenAlmacen));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.escoge = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.codnoting = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codOrdenC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codDocumento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecing = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecreg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codUsu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codDetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codprov = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ruc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docOrden = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDetalle2 = new System.Windows.Forms.DataGridView();
            this.codnotasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechasalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipodoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numdoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.docref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnSalir = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnconsultar = new System.Windows.Forms.Button();
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cmbTransaccion = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.codNota1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codDocumento1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.documento1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.razonsocial1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaingreso1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codProveedor1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle2)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvDetalle);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.dgvDetalle2);
            this.groupBox1.Location = new System.Drawing.Point(0, 58);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 341);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle";
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.AllowUserToDeleteRows = false;
            this.dgvDetalle.AllowUserToOrderColumns = true;
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.escoge,
            this.codnoting,
            this.codOrdenC,
            this.codDocumento,
            this.documento,
            this.razonSocial,
            this.fecing,
            this.fecreg,
            this.codUsu,
            this.codDetalle,
            this.codprov,
            this.ruc,
            this.docOrden});
            this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.RowHeadersVisible = false;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.Size = new System.Drawing.Size(479, 322);
            this.dgvDetalle.TabIndex = 0;
            this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellEndEdit);
            this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvDetalle_RowStateChanged);
            // 
            // escoge
            // 
            this.escoge.HeaderText = "Elige";
            this.escoge.Name = "escoge";
            // 
            // codnoting
            // 
            this.codnoting.DataPropertyName = "codNota";
            this.codnoting.HeaderText = "Nota Ingreso";
            this.codnoting.Name = "codnoting";
            this.codnoting.Visible = false;
            // 
            // codOrdenC
            // 
            this.codOrdenC.DataPropertyName = "ordenCompra";
            this.codOrdenC.HeaderText = "codOrdenCompra";
            this.codOrdenC.Name = "codOrdenC";
            this.codOrdenC.Visible = false;
            // 
            // codDocumento
            // 
            this.codDocumento.DataPropertyName = "codDocumento";
            this.codDocumento.HeaderText = "codDocumento";
            this.codDocumento.Name = "codDocumento";
            this.codDocumento.Visible = false;
            // 
            // documento
            // 
            this.documento.DataPropertyName = "documento";
            this.documento.HeaderText = "Documento";
            this.documento.Name = "documento";
            this.documento.ReadOnly = true;
            this.documento.Width = 150;
            // 
            // razonSocial
            // 
            this.razonSocial.DataPropertyName = "razonsocial";
            this.razonSocial.HeaderText = "razonSocial";
            this.razonSocial.Name = "razonSocial";
            this.razonSocial.Visible = false;
            // 
            // fecing
            // 
            this.fecing.DataPropertyName = "fechaingreso";
            this.fecing.HeaderText = "FechaIngreso";
            this.fecing.Name = "fecing";
            this.fecing.ReadOnly = true;
            // 
            // fecreg
            // 
            this.fecreg.DataPropertyName = "fecharegistro";
            this.fecreg.HeaderText = "FechaRegistro";
            this.fecreg.Name = "fecreg";
            this.fecreg.ReadOnly = true;
            this.fecreg.Visible = false;
            // 
            // codUsu
            // 
            this.codUsu.DataPropertyName = "codUser";
            this.codUsu.HeaderText = "codUsuario";
            this.codUsu.Name = "codUsu";
            this.codUsu.Visible = false;
            // 
            // codDetalle
            // 
            this.codDetalle.DataPropertyName = "codDetalleIngreso";
            this.codDetalle.HeaderText = "codDetalle";
            this.codDetalle.Name = "codDetalle";
            this.codDetalle.Visible = false;
            // 
            // codprov
            // 
            this.codprov.DataPropertyName = "codProveedor";
            this.codprov.HeaderText = "codProveedor";
            this.codprov.Name = "codprov";
            this.codprov.Visible = false;
            // 
            // ruc
            // 
            this.ruc.DataPropertyName = "ruc";
            this.ruc.HeaderText = "Ruc";
            this.ruc.Name = "ruc";
            this.ruc.Visible = false;
            // 
            // docOrden
            // 
            this.docOrden.DataPropertyName = "docOrden";
            this.docOrden.HeaderText = "Orden";
            this.docOrden.Name = "docOrden";
            this.docOrden.ReadOnly = true;
            this.docOrden.Width = 120;
            // 
            // dgvDetalle2
            // 
            this.dgvDetalle2.AllowUserToAddRows = false;
            this.dgvDetalle2.AllowUserToDeleteRows = false;
            this.dgvDetalle2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codnotasalida,
            this.fechasalida,
            this.tipodoc,
            this.serie,
            this.numdoc,
            this.almacen,
            this.docref});
            this.dgvDetalle2.Location = new System.Drawing.Point(3, 16);
            this.dgvDetalle2.MultiSelect = false;
            this.dgvDetalle2.Name = "dgvDetalle2";
            this.dgvDetalle2.ReadOnly = true;
            this.dgvDetalle2.RowHeadersVisible = false;
            this.dgvDetalle2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle2.Size = new System.Drawing.Size(479, 322);
            this.dgvDetalle2.TabIndex = 3;
            this.dgvDetalle2.Visible = false;
            this.dgvDetalle2.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle2_CellDoubleClick);
            this.dgvDetalle2.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvDetalle2_CellMouseDoubleClick);
            // 
            // codnotasalida
            // 
            this.codnotasalida.DataPropertyName = "codNotaSalida";
            this.codnotasalida.HeaderText = "CodNotaSalida";
            this.codnotasalida.Name = "codnotasalida";
            this.codnotasalida.ReadOnly = true;
            this.codnotasalida.Visible = false;
            // 
            // fechasalida
            // 
            this.fechasalida.DataPropertyName = "fechasalida";
            this.fechasalida.HeaderText = "Fecha Salida";
            this.fechasalida.Name = "fechasalida";
            this.fechasalida.ReadOnly = true;
            // 
            // tipodoc
            // 
            this.tipodoc.DataPropertyName = "sigla";
            this.tipodoc.HeaderText = "Tipo Doc.";
            this.tipodoc.Name = "tipodoc";
            this.tipodoc.ReadOnly = true;
            this.tipodoc.Width = 80;
            // 
            // serie
            // 
            this.serie.DataPropertyName = "serie";
            this.serie.HeaderText = "Serie";
            this.serie.Name = "serie";
            this.serie.ReadOnly = true;
            this.serie.Width = 70;
            // 
            // numdoc
            // 
            this.numdoc.DataPropertyName = "numdoc";
            this.numdoc.HeaderText = "N° Doc.";
            this.numdoc.Name = "numdoc";
            this.numdoc.ReadOnly = true;
            this.numdoc.Width = 90;
            // 
            // almacen
            // 
            this.almacen.DataPropertyName = "nomalmacen";
            this.almacen.HeaderText = "Almacen";
            this.almacen.Name = "almacen";
            this.almacen.ReadOnly = true;
            this.almacen.Width = 135;
            // 
            // docref
            // 
            this.docref.DataPropertyName = "documentoreferencia";
            this.docref.HeaderText = "Doc.Ref";
            this.docref.Name = "docref";
            this.docref.ReadOnly = true;
            this.docref.Visible = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.ImageIndex = 1;
            this.btnAceptar.ImageList = this.imageList1;
            this.btnAceptar.Location = new System.Drawing.Point(12, 9);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(90, 30);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "cross.png");
            this.imageList1.Images.SetKeyName(1, "tick.png");
            this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
            // 
            // btnSalir
            // 
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.ImageIndex = 5;
            this.btnSalir.ImageList = this.imageList2;
            this.btnSalir.Location = new System.Drawing.Point(286, 9);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(90, 30);
            this.btnSalir.TabIndex = 2;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "Write Document.png");
            this.imageList2.Images.SetKeyName(1, "New Document.png");
            this.imageList2.Images.SetKeyName(2, "Remove Document.png");
            this.imageList2.Images.SetKeyName(3, "document-print.png");
            this.imageList2.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
            this.imageList2.Images.SetKeyName(5, "exit.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnconsultar);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.btnSalir);
            this.groupBox2.Controls.Add(this.btnAceptar);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 406);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // btnconsultar
            // 
            this.btnconsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnconsultar.ImageIndex = 1;
            this.btnconsultar.ImageList = this.imageList3;
            this.btnconsultar.Location = new System.Drawing.Point(190, 9);
            this.btnconsultar.Name = "btnconsultar";
            this.btnconsultar.Size = new System.Drawing.Size(90, 30);
            this.btnconsultar.TabIndex = 9;
            this.btnconsultar.Text = "Ir a Guia";
            this.btnconsultar.UseVisualStyleBackColor = true;
            this.btnconsultar.Visible = false;
            this.btnconsultar.Click += new System.EventHandler(this.btnconsultar_Click);
            // 
            // imageList3
            // 
            this.imageList3.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList3.ImageStream")));
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList3.Images.SetKeyName(0, "exit.png");
            this.imageList3.Images.SetKeyName(1, "pedido.png");
            this.imageList3.Images.SetKeyName(2, "carrito.png");
            this.imageList3.Images.SetKeyName(3, "delete-file-icon.png");
            this.imageList3.Images.SetKeyName(4, "DeleteRed.png");
            this.imageList3.Images.SetKeyName(5, "document_delete.png");
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(601, 15);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(36, 20);
            this.textBox4.TabIndex = 8;
            this.textBox4.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(560, 15);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(35, 20);
            this.textBox3.TabIndex = 7;
            this.textBox3.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(392, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(36, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(519, 15);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(35, 20);
            this.textBox2.TabIndex = 5;
            this.textBox2.Visible = false;
            // 
            // cmbTransaccion
            // 
            this.cmbTransaccion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTransaccion.FormattingEnabled = true;
            this.cmbTransaccion.Items.AddRange(new object[] {
            "SIN ORDEN DE COMPRA",
            "CON ORDEN COMPRA "});
            this.cmbTransaccion.Location = new System.Drawing.Point(86, 18);
            this.cmbTransaccion.Name = "cmbTransaccion";
            this.cmbTransaccion.Size = new System.Drawing.Size(269, 21);
            this.cmbTransaccion.TabIndex = 28;
            this.cmbTransaccion.Tag = "14";
            this.cmbTransaccion.SelectedIndexChanged += new System.EventHandler(this.cmbTransaccion_SelectedIndexChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(19, 21);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(23, 13);
            this.label17.TabIndex = 29;
            this.label17.Tag = "14";
            this.label17.Text = "Por";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.cmbTransaccion);
            this.groupBox3.Location = new System.Drawing.Point(4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(478, 49);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Busqueda";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codNota1,
            this.codDocumento1,
            this.documento1,
            this.razonsocial1,
            this.fechaingreso1,
            this.codProveedor1});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(479, 322);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // codNota1
            // 
            this.codNota1.DataPropertyName = "codNota1";
            this.codNota1.HeaderText = "Nota Ingreso";
            this.codNota1.Name = "codNota1";
            this.codNota1.Visible = false;
            // 
            // codDocumento1
            // 
            this.codDocumento1.DataPropertyName = "codDocumento1";
            this.codDocumento1.HeaderText = "codDocumento";
            this.codDocumento1.Name = "codDocumento1";
            this.codDocumento1.Visible = false;
            // 
            // documento1
            // 
            this.documento1.DataPropertyName = "documento1";
            this.documento1.HeaderText = "Documento";
            this.documento1.Name = "documento1";
            this.documento1.ReadOnly = true;
            this.documento1.Width = 150;
            // 
            // razonsocial1
            // 
            this.razonsocial1.DataPropertyName = "razonsocial1";
            this.razonsocial1.HeaderText = "razonSocial";
            this.razonsocial1.Name = "razonsocial1";
            // 
            // fechaingreso1
            // 
            this.fechaingreso1.DataPropertyName = "fechaingreso1";
            this.fechaingreso1.HeaderText = "FechaIngreso";
            this.fechaingreso1.Name = "fechaingreso1";
            this.fechaingreso1.ReadOnly = true;
            // 
            // codProveedor1
            // 
            this.codProveedor1.DataPropertyName = "codProveedor1";
            this.codProveedor1.HeaderText = "codProveedor";
            this.codProveedor1.Name = "codProveedor1";
            this.codProveedor1.Visible = false;
            // 
            // frmNotaOrdenAlmacen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(483, 451);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmNotaOrdenAlmacen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guias de Compra";
            this.Load += new System.EventHandler(this.NotaOrdenAlmacen_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn moneda;
        public System.Windows.Forms.TextBox textBox2;
        public System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button btnconsultar;
        private System.Windows.Forms.ImageList imageList3;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.DataGridViewCheckBoxColumn escoge;
        private System.Windows.Forms.DataGridViewTextBoxColumn codnoting;
        private System.Windows.Forms.DataGridViewTextBoxColumn codOrdenC;
        private System.Windows.Forms.DataGridViewTextBoxColumn codDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn razonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecing;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecreg;
        private System.Windows.Forms.DataGridViewTextBoxColumn codUsu;
        private System.Windows.Forms.DataGridViewTextBoxColumn codDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn codprov;
        private System.Windows.Forms.DataGridViewTextBoxColumn ruc;
        private System.Windows.Forms.DataGridViewTextBoxColumn docOrden;
        public System.Windows.Forms.DataGridView dgvDetalle2;
        private System.Windows.Forms.DataGridViewTextBoxColumn codnotasalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechasalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipodoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn numdoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn almacen;
        private System.Windows.Forms.DataGridViewTextBoxColumn docref;
        public System.Windows.Forms.ComboBox cmbTransaccion;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn codNota1;
        private System.Windows.Forms.DataGridViewTextBoxColumn codDocumento1;
        private System.Windows.Forms.DataGridViewTextBoxColumn documento1;
        private System.Windows.Forms.DataGridViewTextBoxColumn razonsocial1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaingreso1;
        private System.Windows.Forms.DataGridViewTextBoxColumn codProveedor1;
    }
}