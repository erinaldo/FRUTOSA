namespace SIGEFA.Formularios
{
    partial class frmConfigurarMateria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfigurarMateria));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnremove = new System.Windows.Forms.Button();
            this.btnadd = new System.Windows.Forms.Button();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.txtDescripcionHijo = new System.Windows.Forms.TextBox();
            this.txtHijo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSubMateria = new System.Windows.Forms.DataGridView();
            this.codConfi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.caracteristica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtDescripcionPadre = new System.Windows.Forms.TextBox();
            this.txtPadre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
            this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubMateria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 388);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAceptar.ImageIndex = 1;
            this.btnAceptar.ImageList = this.imageList1;
            this.btnAceptar.Location = new System.Drawing.Point(425, 13);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
            this.imageList1.Images.SetKeyName(3, "Donate.ico");
            this.imageList1.Images.SetKeyName(4, "Add.png");
            this.imageList1.Images.SetKeyName(5, "Remove.png");
            this.imageList1.Images.SetKeyName(6, "Write Document.png");
            this.imageList1.Images.SetKeyName(7, "Save-icon.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnremove);
            this.groupBox2.Controls.Add(this.btnadd);
            this.groupBox2.Controls.Add(this.txtPorcentaje);
            this.groupBox2.Controls.Add(this.txtDescripcionHijo);
            this.groupBox2.Controls.Add(this.txtHijo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dgvSubMateria);
            this.groupBox2.Controls.Add(this.txtDescripcionPadre);
            this.groupBox2.Controls.Add(this.txtPadre);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(506, 388);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Obtencion de materia prima";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(436, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "%";
            this.label4.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(287, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Porcentaje";
            this.label3.Visible = false;
            // 
            // btnremove
            // 
            this.btnremove.ImageIndex = 5;
            this.btnremove.ImageList = this.imageList1;
            this.btnremove.Location = new System.Drawing.Point(470, 46);
            this.btnremove.Name = "btnremove";
            this.btnremove.Size = new System.Drawing.Size(24, 24);
            this.btnremove.TabIndex = 6;
            this.btnremove.UseVisualStyleBackColor = true;
            this.btnremove.Click += new System.EventHandler(this.btnremove_Click);
            // 
            // btnadd
            // 
            this.btnadd.ImageIndex = 4;
            this.btnadd.ImageList = this.imageList1;
            this.btnadd.Location = new System.Drawing.Point(470, 19);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(24, 24);
            this.btnadd.TabIndex = 5;
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Location = new System.Drawing.Point(351, 45);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(79, 20);
            this.txtPorcentaje.TabIndex = 4;
            this.superValidator1.SetValidator1(this.txtPorcentaje, this.customValidator3);
            this.txtPorcentaje.Visible = false;
            // 
            // txtDescripcionHijo
            // 
            this.txtDescripcionHijo.Location = new System.Drawing.Point(53, 69);
            this.txtDescripcionHijo.Name = "txtDescripcionHijo";
            this.txtDescripcionHijo.ReadOnly = true;
            this.txtDescripcionHijo.Size = new System.Drawing.Size(398, 20);
            this.txtDescripcionHijo.TabIndex = 3;
            this.txtDescripcionHijo.Visible = false;
            // 
            // txtHijo
            // 
            this.txtHijo.Location = new System.Drawing.Point(53, 45);
            this.txtHijo.Name = "txtHijo";
            this.txtHijo.ReadOnly = true;
            this.txtHijo.Size = new System.Drawing.Size(100, 20);
            this.txtHijo.TabIndex = 2;
            this.superValidator1.SetValidator1(this.txtHijo, this.customValidator2);
            this.txtHijo.Visible = false;
            this.txtHijo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHijo_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Hijo";
            this.label2.Visible = false;
            // 
            // dgvSubMateria
            // 
            this.dgvSubMateria.AllowUserToAddRows = false;
            this.dgvSubMateria.AllowUserToDeleteRows = false;
            this.dgvSubMateria.AllowUserToResizeColumns = false;
            this.dgvSubMateria.AllowUserToResizeRows = false;
            this.dgvSubMateria.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSubMateria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSubMateria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codConfi,
            this.codproducto,
            this.caracteristica,
            this.valor});
            this.dgvSubMateria.Location = new System.Drawing.Point(6, 100);
            this.dgvSubMateria.Name = "dgvSubMateria";
            this.dgvSubMateria.ReadOnly = true;
            this.dgvSubMateria.RowHeadersVisible = false;
            this.dgvSubMateria.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSubMateria.Size = new System.Drawing.Size(499, 285);
            this.dgvSubMateria.TabIndex = 7;
            this.dgvSubMateria.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSubMateria_CellClick);
            // 
            // codConfi
            // 
            this.codConfi.DataPropertyName = "codConfiguracionMP";
            this.codConfi.HeaderText = "Codigo";
            this.codConfi.Name = "codConfi";
            this.codConfi.ReadOnly = true;
            this.codConfi.Visible = false;
            // 
            // codproducto
            // 
            this.codproducto.DataPropertyName = "codProducto";
            this.codproducto.HeaderText = "CodProducto";
            this.codproducto.Name = "codproducto";
            this.codproducto.ReadOnly = true;
            this.codproducto.Visible = false;
            // 
            // caracteristica
            // 
            this.caracteristica.DataPropertyName = "descripcion";
            this.caracteristica.HeaderText = "Materia";
            this.caracteristica.Name = "caracteristica";
            this.caracteristica.ReadOnly = true;
            this.caracteristica.Width = 200;
            // 
            // valor
            // 
            this.valor.DataPropertyName = "porcentaje";
            this.valor.HeaderText = "Porcentaje (%)";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            this.valor.Width = 150;
            // 
            // txtDescripcionPadre
            // 
            this.txtDescripcionPadre.Location = new System.Drawing.Point(159, 19);
            this.txtDescripcionPadre.Name = "txtDescripcionPadre";
            this.txtDescripcionPadre.ReadOnly = true;
            this.txtDescripcionPadre.Size = new System.Drawing.Size(292, 20);
            this.txtDescripcionPadre.TabIndex = 1;
            // 
            // txtPadre
            // 
            this.txtPadre.Location = new System.Drawing.Point(53, 19);
            this.txtPadre.Name = "txtPadre";
            this.txtPadre.ReadOnly = true;
            this.txtPadre.Size = new System.Drawing.Size(100, 20);
            this.txtPadre.TabIndex = 0;
            this.superValidator1.SetValidator1(this.txtPadre, this.customValidator1);
            this.txtPadre.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPadre_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Padre";
            // 
            // superValidator1
            // 
            this.superValidator1.ContainerControl = this;
            this.superValidator1.ErrorProvider = this.errorProvider1;
            this.superValidator1.Highlighter = this.highlighter1;
            // 
            // customValidator3
            // 
            this.customValidator3.ErrorMessage = "Ingrese el porcentaje";
            this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator3_ValidateValue);
            // 
            // customValidator2
            // 
            this.customValidator2.ErrorMessage = "Seleccione Materia Prima hijo";
            this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
            this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(this.customValidator2_ValidateValue);
            // 
            // customValidator1
            // 
            this.customValidator1.ErrorMessage = "Seleccione Materia prima padre.";
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
            // frmConfigurarMateria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 433);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmConfigurarMateria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurar Materia Prima";
            this.Load += new System.EventHandler(this.frmConfigurarMateria_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSubMateria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDescripcionPadre;
        private System.Windows.Forms.TextBox txtPadre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSubMateria;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.TextBox txtDescripcionHijo;
        private System.Windows.Forms.TextBox txtHijo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnremove;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAceptar;
        private DevComponents.DotNetBar.Validator.SuperValidator superValidator1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private DevComponents.DotNetBar.Validator.Highlighter highlighter1;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator3;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator2;
        private DevComponents.DotNetBar.Validator.CustomValidator customValidator1;
        private System.Windows.Forms.DataGridViewTextBoxColumn codConfi;
        private System.Windows.Forms.DataGridViewTextBoxColumn codproducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn caracteristica;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
    }
}