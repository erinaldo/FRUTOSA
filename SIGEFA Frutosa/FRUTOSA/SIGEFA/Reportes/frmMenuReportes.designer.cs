namespace SIGEFA.Reportes
{
    partial class frmMenuReportes
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Ventas Contado/Crédito");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Ventas por Vendedor");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Ventas por Vendedor por Articulo");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Ventas por Cliente");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Ventas x Articulo");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Despacho x N° Documento");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Cobros x Día");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Pagos x Día");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Informes Diarios", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Ventas Mensuales por Artículo");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Ventas Mensuales", new System.Windows.Forms.TreeNode[] {
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("X Artículo");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("X Cliente");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Ranking de Ventas", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Stock de Artículos");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Kardex de Artículos");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Inventario en Unidades", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Despacho x Artículo");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Documentos de Almacen", new System.Windows.Forms.TreeNode[] {
            treeNode18});
            this.tvRptFacturacion = new System.Windows.Forms.TreeView();
            this.tvRptInventarios = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tvRptFacturacion
            // 
            this.tvRptFacturacion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvRptFacturacion.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvRptFacturacion.Location = new System.Drawing.Point(0, 29);
            this.tvRptFacturacion.Name = "tvRptFacturacion";
            treeNode1.Name = "tvInfVentCC";
            treeNode1.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode1.SelectedImageIndex = -2;
            treeNode1.Text = "Ventas Contado/Crédito";
            treeNode2.Name = "tvInfVentVende";
            treeNode2.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode2.Text = "Ventas por Vendedor";
            treeNode3.Name = "tvInfVentVendArt";
            treeNode3.NodeFont = new System.Drawing.Font("Courier New", 9.75F);
            treeNode3.Text = "Ventas por Vendedor por Articulo";
            treeNode4.Name = "tvInfVentasxClient";
            treeNode4.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode4.Text = "Ventas por Cliente";
            treeNode5.Name = "tvVentasxArticulo";
            treeNode5.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode5.Text = "Ventas x Articulo";
            treeNode6.Name = "tvDespachoDocumento";
            treeNode6.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode6.Text = "Despacho x N° Documento";
            treeNode7.Name = "tvCobros";
            treeNode7.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode7.Text = "Cobros x Día";
            treeNode8.Name = "tvPagosxDia";
            treeNode8.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode8.Text = "Pagos x Día";
            treeNode9.Name = "tvInformes";
            treeNode9.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode9.Text = "Informes Diarios";
            treeNode10.Name = "tvVentasMesArticulo";
            treeNode10.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode10.Text = "Ventas Mensuales por Artículo";
            treeNode11.Name = "Node19";
            treeNode11.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode11.Text = "Ventas Mensuales";
            treeNode12.Name = "tvArticulo";
            treeNode12.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode12.Text = "X Artículo";
            treeNode13.Name = "tvCliente";
            treeNode13.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode13.Text = "X Cliente";
            treeNode14.Name = "Node0";
            treeNode14.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode14.Text = "Ranking de Ventas";
            this.tvRptFacturacion.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode11,
            treeNode14});
            this.tvRptFacturacion.Size = new System.Drawing.Size(322, 190);
            this.tvRptFacturacion.TabIndex = 0;
            this.tvRptFacturacion.DoubleClick += new System.EventHandler(this.tvRptFacturacion_DoubleClick);
            // 
            // tvRptInventarios
            // 
            this.tvRptInventarios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tvRptInventarios.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvRptInventarios.Location = new System.Drawing.Point(328, 29);
            this.tvRptInventarios.Name = "tvRptInventarios";
            treeNode15.Name = "tvInvStockArticulos";
            treeNode15.NodeFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode15.SelectedImageIndex = -2;
            treeNode15.Text = "Stock de Artículos";
            treeNode16.Name = "tvKardex";
            treeNode16.NodeFont = new System.Drawing.Font("Courier New", 11.25F);
            treeNode16.Text = "Kardex de Artículos";
            treeNode17.Name = "tvInformes";
            treeNode17.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode17.Text = "Inventario en Unidades";
            treeNode18.Name = "tvDespachoArticulo";
            treeNode18.NodeFont = new System.Drawing.Font("Courier New", 11.25F);
            treeNode18.Text = "Despacho x Artículo";
            treeNode19.Name = "Node28";
            treeNode19.NodeFont = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            treeNode19.Text = "Documentos de Almacen";
            this.tvRptInventarios.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode19});
            this.tvRptInventarios.Size = new System.Drawing.Size(322, 190);
            this.tvRptInventarios.TabIndex = 1;
            this.tvRptInventarios.DoubleClick += new System.EventHandler(this.tvInventarios_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(108, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "FACTURACIÓN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(438, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "INVENTARIOS";
            // 
            // frmMenuReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 222);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvRptInventarios);
            this.Controls.Add(this.tvRptFacturacion);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMenuReportes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reportes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvRptFacturacion;
        private System.Windows.Forms.TreeView tvRptInventarios;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}