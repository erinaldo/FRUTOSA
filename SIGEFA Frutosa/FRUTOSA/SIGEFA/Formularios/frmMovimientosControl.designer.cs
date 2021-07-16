namespace SIGEFA.Formularios
{
    partial class frmMovimientosControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMovimientosControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCodTransaccion = new System.Windows.Forms.TextBox();
            this.txtTotalCuenta = new System.Windows.Forms.TextBox();
            this.txtmonto = new System.Windows.Forms.TextBox();
            this.txtCambioVen = new System.Windows.Forms.TextBox();
            this.txtCambioCom = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.cmbTipo = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbMoneda = new System.Windows.Forms.ComboBox();
            this.txtTipoCta = new System.Windows.Forms.TextBox();
            this.cmbtipopagoser = new System.Windows.Forms.ComboBox();
            this.cmbCuenta = new System.Windows.Forms.ComboBox();
            this.cmbBanco = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnGuardar2 = new System.Windows.Forms.Button();
            this.btnNuevo2 = new System.Windows.Forms.Button();
            this.btnReporte2 = new System.Windows.Forms.Button();
            this.btnEditar2 = new System.Windows.Forms.Button();
            this.btnEliminar2 = new System.Windows.Forms.Button();
            this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
            this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(2, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dtpFecha);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtCodTransaccion);
            this.groupBox3.Controls.Add(this.txtTotalCuenta);
            this.groupBox3.Controls.Add(this.txtmonto);
            this.groupBox3.Controls.Add(this.txtCambioVen);
            this.groupBox3.Controls.Add(this.txtCambioCom);
            this.groupBox3.Controls.Add(this.txtDescripcion);
            this.groupBox3.Controls.Add(this.cmbTipo);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(350, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(431, 176);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DATOS DE LOS MOVIMIENTOS";
            // 
            // dtpFecha
            // 
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFecha.Location = new System.Drawing.Point(76, 84);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(142, 20);
            this.dtpFecha.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 87);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "Fecha:";
            // 
            // txtCodTransaccion
            // 
            this.txtCodTransaccion.Location = new System.Drawing.Point(107, 147);
            this.txtCodTransaccion.Name = "txtCodTransaccion";
            this.txtCodTransaccion.Size = new System.Drawing.Size(111, 20);
            this.txtCodTransaccion.TabIndex = 13;
            this.superValidator1.SetValidator1(this.txtCodTransaccion, this.customValidator7);
            this.txtCodTransaccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodTransaccion_KeyPress);
            // 
            // txtTotalCuenta
            // 
            this.txtTotalCuenta.Location = new System.Drawing.Point(324, 147);
            this.txtTotalCuenta.Name = "txtTotalCuenta";
            this.txtTotalCuenta.ReadOnly = true;
            this.txtTotalCuenta.Size = new System.Drawing.Size(100, 20);
            this.txtTotalCuenta.TabIndex = 12;
            // 
            // txtmonto
            // 
            this.txtmonto.Location = new System.Drawing.Point(325, 114);
            this.txtmonto.Name = "txtmonto";
            this.txtmonto.Size = new System.Drawing.Size(100, 20);
            this.txtmonto.TabIndex = 11;
            this.superValidator1.SetValidator1(this.txtmonto, this.customValidator6);
            this.txtmonto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtmonto_KeyPress);
            // 
            // txtCambioVen
            // 
            this.txtCambioVen.Location = new System.Drawing.Point(324, 84);
            this.txtCambioVen.Name = "txtCambioVen";
            this.txtCambioVen.ReadOnly = true;
            this.txtCambioVen.Size = new System.Drawing.Size(100, 20);
            this.txtCambioVen.TabIndex = 10;
            // 
            // txtCambioCom
            // 
            this.txtCambioCom.Location = new System.Drawing.Point(325, 53);
            this.txtCambioCom.Name = "txtCambioCom";
            this.txtCambioCom.ReadOnly = true;
            this.txtCambioCom.Size = new System.Drawing.Size(100, 20);
            this.txtCambioCom.TabIndex = 9;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(76, 23);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(348, 20);
            this.txtDescripcion.TabIndex = 8;
            this.superValidator1.SetValidator1(this.txtDescripcion, this.customValidator4);
            this.txtDescripcion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescripcion_KeyPress);
            // 
            // cmbTipo
            // 
            this.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipo.FormattingEnabled = true;
            this.cmbTipo.Items.AddRange(new object[] {
            "INGRESO",
            "EGRESO"});
            this.cmbTipo.Location = new System.Drawing.Point(76, 53);
            this.cmbTipo.Name = "cmbTipo";
            this.cmbTipo.Size = new System.Drawing.Size(142, 21);
            this.cmbTipo.TabIndex = 7;
            this.superValidator1.SetValidator1(this.cmbTipo, this.customValidator5);
            this.cmbTipo.SelectionChangeCommitted += new System.EventHandler(this.cmbTipo_SelectionChangeCommitted);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(242, 150);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Total - Cuenta:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 150);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Nro de Operación:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(279, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Monto:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(239, 88);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "T. Cambio Ven:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(239, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "T.Cambio Com:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Tipo:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Descripción:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbMoneda);
            this.groupBox2.Controls.Add(this.txtTipoCta);
            this.groupBox2.Controls.Add(this.cmbtipopagoser);
            this.groupBox2.Controls.Add(this.cmbCuenta);
            this.groupBox2.Controls.Add(this.cmbBanco);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(7, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 176);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "DATOS FINANCIEROS";
            // 
            // cmbMoneda
            // 
            this.cmbMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoneda.FormattingEnabled = true;
            this.cmbMoneda.Location = new System.Drawing.Point(110, 114);
            this.cmbMoneda.Name = "cmbMoneda";
            this.cmbMoneda.Size = new System.Drawing.Size(210, 21);
            this.cmbMoneda.TabIndex = 15;
            this.superValidator1.SetValidator1(this.cmbMoneda, this.customValidator3);
            this.cmbMoneda.SelectionChangeCommitted += new System.EventHandler(this.cmbMoneda_SelectionChangeCommitted);
            // 
            // txtTipoCta
            // 
            this.txtTipoCta.Location = new System.Drawing.Point(109, 84);
            this.txtTipoCta.Name = "txtTipoCta";
            this.txtTipoCta.ReadOnly = true;
            this.txtTipoCta.Size = new System.Drawing.Size(211, 20);
            this.txtTipoCta.TabIndex = 14;
            // 
            // cmbtipopagoser
            // 
            this.cmbtipopagoser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtipopagoser.FormattingEnabled = true;
            this.cmbtipopagoser.Location = new System.Drawing.Point(109, 147);
            this.cmbtipopagoser.Name = "cmbtipopagoser";
            this.cmbtipopagoser.Size = new System.Drawing.Size(211, 21);
            this.cmbtipopagoser.TabIndex = 12;
            // 
            // cmbCuenta
            // 
            this.cmbCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCuenta.FormattingEnabled = true;
            this.cmbCuenta.Location = new System.Drawing.Point(110, 53);
            this.cmbCuenta.Name = "cmbCuenta";
            this.cmbCuenta.Size = new System.Drawing.Size(210, 21);
            this.cmbCuenta.TabIndex = 11;
            this.superValidator1.SetValidator1(this.cmbCuenta, this.customValidator2);
            this.cmbCuenta.SelectionChangeCommitted += new System.EventHandler(this.cmbCuenta_SelectionChangeCommitted);
            // 
            // cmbBanco
            // 
            this.cmbBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBanco.FormattingEnabled = true;
            this.cmbBanco.Location = new System.Drawing.Point(110, 23);
            this.cmbBanco.Name = "cmbBanco";
            this.cmbBanco.Size = new System.Drawing.Size(210, 21);
            this.cmbBanco.TabIndex = 10;
            this.superValidator1.SetValidator1(this.cmbBanco, this.customValidator1);
            this.cmbBanco.SelectionChangeCommitted += new System.EventHandler(this.cmbBanco_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tipo Pago Servicio:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Moneda:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tipo Cta:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Cta. Corriente:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Banco:";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox4.Controls.Add(this.btnSalir);
            this.groupBox4.Controls.Add(this.btnGuardar2);
            this.groupBox4.Controls.Add(this.btnNuevo2);
            this.groupBox4.Controls.Add(this.btnReporte2);
            this.groupBox4.Controls.Add(this.btnEditar2);
            this.groupBox4.Controls.Add(this.btnEliminar2);
            this.groupBox4.Location = new System.Drawing.Point(2, 213);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(802, 52);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            // 
            // btnSalir
            // 
            this.btnSalir.ImageIndex = 5;
            this.btnSalir.ImageList = this.imageList1;
            this.btnSalir.Location = new System.Drawing.Point(700, 13);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(61, 32);
            this.btnSalir.TabIndex = 61;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Write Document.png");
            this.imageList1.Images.SetKeyName(1, "New Document.png");
            this.imageList1.Images.SetKeyName(2, "Remove Document.png");
            this.imageList1.Images.SetKeyName(3, "document-print.png");
            this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
            this.imageList1.Images.SetKeyName(5, "exit.png");
            // 
            // btnGuardar2
            // 
            this.btnGuardar2.Enabled = false;
            this.btnGuardar2.ImageIndex = 4;
            this.btnGuardar2.ImageList = this.imageList1;
            this.btnGuardar2.Location = new System.Drawing.Point(595, 13);
            this.btnGuardar2.Name = "btnGuardar2";
            this.btnGuardar2.Size = new System.Drawing.Size(86, 32);
            this.btnGuardar2.TabIndex = 60;
            this.btnGuardar2.Text = "Guardar";
            this.btnGuardar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar2.UseVisualStyleBackColor = true;
            this.btnGuardar2.Click += new System.EventHandler(this.btnGuardar2_Click);
            // 
            // btnNuevo2
            // 
            this.btnNuevo2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnNuevo2.ImageIndex = 1;
            this.btnNuevo2.ImageList = this.imageList1;
            this.btnNuevo2.Location = new System.Drawing.Point(8, 13);
            this.btnNuevo2.Name = "btnNuevo2";
            this.btnNuevo2.Size = new System.Drawing.Size(71, 32);
            this.btnNuevo2.TabIndex = 56;
            this.btnNuevo2.Text = "Nuevo";
            this.btnNuevo2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnNuevo2.UseVisualStyleBackColor = true;
            this.btnNuevo2.Visible = false;
            this.btnNuevo2.Click += new System.EventHandler(this.btnNuevo2_Click);
            // 
            // btnReporte2
            // 
            this.btnReporte2.ImageIndex = 3;
            this.btnReporte2.ImageList = this.imageList1;
            this.btnReporte2.Location = new System.Drawing.Point(373, 13);
            this.btnReporte2.Name = "btnReporte2";
            this.btnReporte2.Size = new System.Drawing.Size(78, 32);
            this.btnReporte2.TabIndex = 59;
            this.btnReporte2.Text = "Reporte";
            this.btnReporte2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnReporte2.UseVisualStyleBackColor = true;
            this.btnReporte2.Visible = false;
            this.btnReporte2.Click += new System.EventHandler(this.btnReporte2_Click);
            // 
            // btnEditar2
            // 
            this.btnEditar2.Enabled = false;
            this.btnEditar2.ImageIndex = 0;
            this.btnEditar2.ImageList = this.imageList1;
            this.btnEditar2.Location = new System.Drawing.Point(85, 13);
            this.btnEditar2.Name = "btnEditar2";
            this.btnEditar2.Size = new System.Drawing.Size(66, 32);
            this.btnEditar2.TabIndex = 57;
            this.btnEditar2.Text = "Editar";
            this.btnEditar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEditar2.UseVisualStyleBackColor = true;
            this.btnEditar2.Visible = false;
            this.btnEditar2.Click += new System.EventHandler(this.btnEditar2_Click);
            // 
            // btnEliminar2
            // 
            this.btnEliminar2.Enabled = false;
            this.btnEliminar2.ImageIndex = 2;
            this.btnEliminar2.ImageList = this.imageList1;
            this.btnEliminar2.Location = new System.Drawing.Point(157, 13);
            this.btnEliminar2.Name = "btnEliminar2";
            this.btnEliminar2.Size = new System.Drawing.Size(75, 32);
            this.btnEliminar2.TabIndex = 58;
            this.btnEliminar2.Text = "Eliminar";
            this.btnEliminar2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnEliminar2.UseVisualStyleBackColor = true;
            this.btnEliminar2.Visible = false;
            this.btnEliminar2.Click += new System.EventHandler(this.btnEliminar2_Click);
            // 
            // customValidator1
            // 
            this.customValidator1.ErrorMessage = "Seleccione Banco";
            this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator1_ValidateValue);
            // 
            // customValidator2
            // 
            this.customValidator2.ErrorMessage = "Seleccione Cuenta";
            this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator2_ValidateValue);
            // 
            // customValidator3
            // 
            this.customValidator3.ErrorMessage = "Seleccione Moneda";
            this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator3_ValidateValue);
            // 
            // customValidator4
            // 
            this.customValidator4.ErrorMessage = "Ingrese Descripcion";
            this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator4_ValidateValue);
            // 
            // customValidator5
            // 
            this.customValidator5.ErrorMessage = "Seleccione Tipo";
            this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator5_ValidateValue);
            // 
            // customValidator6
            // 
            this.customValidator6.ErrorMessage = "Ingrese Monto";
            this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator6_ValidateValue);
            // 
            // customValidator7
            // 
            this.customValidator7.ErrorMessage = "Ingrese Nro Operación";
            this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator7_ValidateValue);
            // 
            // frmMovimientosControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(814, 275);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMovimientosControl";
            this.Text = "frmMovimientosControl";
            this.Load += new System.EventHandler(this.frmMovimientosControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnGuardar2;
        private System.Windows.Forms.Button btnNuevo2;
        private System.Windows.Forms.Button btnReporte2;
        private System.Windows.Forms.Button btnEditar2;
        private System.Windows.Forms.Button btnEliminar2;
        private System.Windows.Forms.Label label13;
        public System.Windows.Forms.TextBox txtTipoCta;
        public System.Windows.Forms.ComboBox cmbtipopagoser;
        public System.Windows.Forms.ComboBox cmbCuenta;
        public System.Windows.Forms.ComboBox cmbBanco;
        public System.Windows.Forms.ComboBox cmbTipo;
        public System.Windows.Forms.TextBox txtTotalCuenta;
        public System.Windows.Forms.TextBox txtmonto;
        public System.Windows.Forms.TextBox txtCambioVen;
        public System.Windows.Forms.TextBox txtCambioCom;
        public System.Windows.Forms.TextBox txtDescripcion;
        public System.Windows.Forms.TextBox txtCodTransaccion;
        public System.Windows.Forms.ComboBox cmbMoneda;
        public System.Windows.Forms.DateTimePicker dtpFecha;
        private DevComponents.DotNetBar.Validator.SuperValidator superValidator1;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator7;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator6;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator4;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator5;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator3;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator2;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator1;
    }
}