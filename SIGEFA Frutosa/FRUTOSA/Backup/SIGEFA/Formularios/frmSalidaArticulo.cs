using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios
{
    public partial class frmSalidaArticulo : DevComponents.DotNetBar.OfficeForm
    {
        public static List<Int32> seleccion = new List<Int32>();
        public Int32 Proceso = 0,repetido=0;
        public Int32 Seleccion = 0;
        public Int32 Procede = 0;//(1)nota de salida (2)venta (3)Pedidoventa
        public Int32 CodProducto = 0;

        public Int32 CodLote = 0;

        clsAdmProducto AdmPro = new clsAdmProducto();
        clsProducto pro = new clsProducto();
        clsProducto prod = new clsProducto();
        public clsDetalleRequerimiento detalle;
        clsUnidadEquivalente uniequi = new clsUnidadEquivalente();
        clsUnidadMedida unidadMed = new clsUnidadMedida();
        clsAdmUnidad Unid = new clsAdmUnidad();
        Double factorconvert = 0;
        clsValidar ok = new clsValidar();
       // public DataTable data = new DataTable();
        public Int32 Codlista = 0, codproveedor=0;
        public Int32 codalmacen = 0;
        clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();

        //RAGA Ing MatPrima
        public decimal preciounit;
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;   

        public frmSalidaArticulo()
        {
            InitializeComponent();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmProductosLista frm = new frmProductosLista();
                frm.ShowDialog();
            }
        }

        //private void ingresarseleccion()
        //{
        //    foreach (Int32 cod in seleccion)
        //    {
        //        txtCodigo.Text = cod.ToString();
        //    }
        //}

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {

                pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista);

                CodProducto = pro.CodProducto;
                txtReferencia.Text = pro.Referencia;
                txtDescripcion.Text = pro.Descripcion;
               
                CargaUnidades(cmbUnidad);
                cmbUnidad.SelectedValue = pro.CodUnidadMedida;
         //       txtStock.Text = pro.StockDisponible.ToString();
                unidadMed = Unid.CargaUnidad(Convert.ToInt32(cmbUnidad.SelectedValue));
                txtUnidad.Text = unidadMed.Descripcion;
                txtControlStock.Text = "";



                dgvProductoTerminado.DataSource = data;
                data.DataSource = AdmPro.BuscaProductosTerminadosReproceso(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, CodLote);
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductoTerminado.ClearSelection();

            }
            
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtCantidad.Text != "") 
                {
                    //if(Convert.ToDouble(txtCantidad.Text) <= pro.StockDisponible)
                    //{
                    //    if (txtPrecio.Text != "")
                    //    {
                    //        if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                    //        if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                    //        if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }                       
                    //        txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                            if (pro != null)
                            {
                                btnGuardar.Enabled = true;
                            } 
                           ProcessTabKey(true);
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtCantidad.Focus();
                    //}
                }
                if (Procede == 9)
                {
                    if (Convert.ToDouble(txtCantidad.Text) > pro.StockDisponible)
                    {
                        MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Focus();
                    }
                }
            }
        }


        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (txtCantidad.Text == "")
            {
                txtCantidad.Focus();
            }
            else
            {
                if (Convert.ToDouble(txtCantidad.Text) > 0)
                {

                  
                  


                }
                else
                {
                    MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCantidad.Text = "";
                    txtCantidad.Focus();
                }
            }
            if (pro != null && txtCantidad.Text != "")
            {
                btnGuardar.Enabled = true;
                btnGuardar.Focus();
            }
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            if (Procede == 22)//Cuando abre desde frmNotaSalida
            {
                frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];



                    foreach (DataGridViewRow row1 in dgvProductoTerminado.Rows)
                    {
                        DataGridViewCheckBoxCell cellSelecion1 = row1.Cells["Escoge"] as DataGridViewCheckBoxCell;

                        if (Convert.ToString(cellSelecion1.Value) == "T")
                        {
                            //   Int32.Parse(row1.Cells[codNotaSalida.Name].Value.ToString());
                            form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                              txtUnidad.Text, row1.Cells[serielote.Name].Value.ToString(), Convert.ToDouble(txtCantidad.Text), form.cmbMoneda.SelectedValue, preciounit,
                              0, "", "", "", 0, 0, 0, 0, 0, 0, 0, frmLogin.iCodUser, new DateTime(), 0, 0, 0, 0, 0, row1.Cells[gradosbrix.Name].Value.ToString(), 0, row1.Cells[pesoneto.Name].Value.ToString());
                        }
                    }
                    this.Close();
            }



        }

        private void CargaFilaDetalle()
        {
            //detalle.CodDetalleSalida = 0;
            //detalle.CodProducto = pro.CodProducto;
            //detalle.Referencia = pro.Referencia;
            //detalle.Descripcion = pro.Descripcion;
            //detalle.UnidadIngresada = Convert.ToInt32(cmbUnidad.SelectedValue);
            //detalle.Unidad = cmbUnidad.Text;
            //detalle.SerieLote = txtControlStock.Text;
            //detalle.Cantidad = Convert.ToDouble(txtCantidad.Text);
            //detalle.PrecioUnitario = Convert.ToDouble(txtPrecio.Text);
            //detalle.Importe = bruto;
            //detalle.Descuento1 = Convert.ToDouble(txtDscto1.Text);
            //detalle.Descuento2 = Convert.ToDouble(txtDscto2.Text);
            //detalle.Descuento3 = Convert.ToDouble(txtDscto3.Text);
            //detalle.MontoDescuento = montodescuento;
            //detalle.ValorVenta = valorventa;
            //detalle.Igv = igv;
            //detalle.PrecioVenta = precioventa;
            //detalle.PrecioReal = precioreal;
            //detalle.ValoReal = valorreal;
        }

       

        private void txtControlStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void frmDetalleSalida_Shown(object sender, EventArgs e)
        {
            if (txtReferencia.Text.Trim() == "")
            {
                txtReferencia.Focus();
            }
            if (Proceso == 2)
            {
                txtCantidad.Focus();
            }
            else if ((Proceso == 3 && txtDescripcion.Text.Trim() != "") || (Proceso == 1 && txtDescripcion.Text.Trim() != ""))
            {
                txtCantidad.Focus();
            }
        }
        private void limpiarformulario()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is TextBox)
                {
                    c.Text = "";
                }
            }

        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        
        private void txtReferencia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmProductosLista"] != null)
                {
                    Application.OpenForms["frmProductosLista"].Activate();
                }
                else
                {
                    frmProductosLista frm = new frmProductosLista();
                    frm.Procede = Procede;
                    //if (Procede == 11){frm.Procede = 11;}else
                    //{
                    //    frm.Procede = 9;
                        if (codalmacen != 0)
                            frm.codalmacen = codalmacen;
                    //}
                    frm.Proceso = Proceso;
                    frm.codproveedor = codproveedor;
                    frm.CodLista = Codlista;
                    frm.ShowDialog();
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                frmRegistroProducto frm = new frmRegistroProducto();
                frm.ShowDialog();
            }
            
        }

        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtReferencia.Text != "")
                {
                    if (BuscaProducto())
                    {
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private Boolean BuscaProducto()
        {
            pro = AdmPro.CargaProductoDetalleR(txtReferencia.Text, frmLogin.iCodAlmacen, 2, Codlista);
            if (pro != null)
            {
                if (Procede == 11) {
                    frmRequerimiento form = (frmRequerimiento)Application.OpenForms["frmRequerimiento"];
                    if (form.codProd.Contains(pro.CodProducto) && Proceso == 1)
                    {
                        MessageBox.Show("El Producto ya existe");
                        repetido = 1;
                        return false;
                    }
                    else
                    {
                        CodProducto = pro.CodProducto;
                        txtReferencia.Text = pro.Referencia;
                        txtDescripcion.Text = pro.Descripcion;
                        txtUnidad.Text = pro.UnidadDescrip;
                        CargaUnidades(cmbUnidad);
                        cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                        txtStock.Text = pro.StockDisponible.ToString();
                        txtControlStock.Text = "";
                        txtCantidad.Text = "";
                        switch (pro.CodControlStock)
                        {
                            case 1:
                                txtControlStock.Enabled = false;
                                txtCantidad.Enabled = true;
                                break;
                            case 2:
                                txtControlStock.Enabled = true;
                                txtCantidad.Enabled = true;
                                break;
                            case 3:
                                txtControlStock.Enabled = true;
                                txtCantidad.Enabled = false;
                                txtCantidad.Text = "01";
                                break;
                            case 4:
                                txtControlStock.Enabled = false;
                                txtCantidad.Enabled = false;
                                txtCantidad.Text = "";
                                break;
                        }
                        return true;
                    }
                }
                else if (Procede == 9)
                {
                    frmTranferenciaDirecta form = (frmTranferenciaDirecta)Application.OpenForms["frmTranferenciaDirecta"];
                    if (form.codProd.Contains(pro.CodProducto) && Proceso == 1)
                    {
                        MessageBox.Show("El Producto ya existe");
                        repetido = 1;
                        return false;
                    }
                    else
                    {
                        CodProducto = pro.CodProducto;
                        txtReferencia.Text = pro.Referencia;
                        txtDescripcion.Text = pro.Descripcion;
                        txtUnidad.Text = pro.UnidadDescrip;
                        CargaUnidades(cmbUnidad);
                        cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                        txtStock.Text = pro.StockDisponible.ToString();
                        txtControlStock.Text = "";
                        txtCantidad.Text = "";
                        switch (pro.CodControlStock)
                        {
                            case 1:
                                txtControlStock.Enabled = false;
                                txtCantidad.Enabled = true;
                                break;
                            case 2:
                                txtControlStock.Enabled = true;
                                txtCantidad.Enabled = true;
                                break;
                            case 3:
                                txtControlStock.Enabled = true;
                                txtCantidad.Enabled = false;
                                txtCantidad.Text = "01";
                                break;
                            case 4:
                                txtControlStock.Enabled = false;
                                txtCantidad.Enabled = false;
                                txtCantidad.Text = "";
                                break;
                        }
                        return true;
                    }
                }
                else {
                    CodProducto = pro.CodProducto;
                    txtReferencia.Text = pro.Referencia;
                    txtDescripcion.Text = pro.Descripcion;
                    txtUnidad.Text = pro.UnidadDescrip;
                    CargaUnidades(cmbUnidad);
                    cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                    txtStock.Text = pro.StockDisponible.ToString();
                    txtControlStock.Text = "";
                    txtCantidad.Text = "";
                    switch (pro.CodControlStock)
                    {
                        case 1:
                            txtControlStock.Enabled = false;
                            txtCantidad.Enabled = true;
                            break;
                        case 2:
                            txtControlStock.Enabled = true;
                            txtCantidad.Enabled = true;
                            break;
                        case 3:
                            txtControlStock.Enabled = true;
                            txtCantidad.Enabled = false;
                            txtCantidad.Text = "01";
                            break;
                        case 4:
                            txtControlStock.Enabled = false;
                            txtCantidad.Enabled = false;
                            txtCantidad.Text = "";
                            break;
                    }
                    return true;
                }
            }
            else
            {
                CodProducto = 0;
                txtDescripcion.Text = "";
                txtUnidad.Text = "";
                cmbUnidad.SelectedIndex = -1;
                txtStock.Text = "";
                txtControlStock.Text = "";
                txtCantidad.Text = "";                
                return false;
            }
        }

        private void CargaUnidades(ComboBox combo)
        {
            combo.DataSource = AdmPro.CargaUnidadesEquivalentes(pro.CodProducto);
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "codUnidadMedida";
            combo.SelectedValue = pro.CodUnidadMedida;
        }

        private void cmbUnidad_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbUnidad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbUnidad.SelectedValue) == pro.CodUnidadMedida)
            {
                txtStock.Text = pro.StockDisponible.ToString();
            }
            else
            {
                uniequi = AdmPro.CargaUnidadEquivalente(Convert.ToInt32(cmbUnidad.SelectedValue), CodProducto);
                factorconvert = uniequi.Factor;
                txtStock.Text = Convert.ToString(pro.StockDisponible / factorconvert);
            }
        }

        private void frmSalidaArticulo_Load(object sender, EventArgs e)
        {
            CargaUnidades(cmbUnidad);

            label10.Text = "Serie";
            label7.Text = "serielote";



        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

      

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //textBox3.Text = String.Format("{0:#,##0.00}", textBox3.Text);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
                   //    textBox5.Text = String.Format("{0:#,##0.0000}", textBox5.Text); 
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

           
            //RAGA

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dgvProductoTerminado_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label10.Text = dgvProductoTerminado.Columns[e.ColumnIndex].HeaderText;
            label7.Text = dgvProductoTerminado.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text.Length >= 0)
                {
                    //data.Filter = String.Format("[gradosbrix0] > '{0}' AND [gradosbrix0] < '{1}'", textBox1.Text, textBox2.Text);
                    // data.Filter = String.Format("[{0}] like '*{1}*'", label7.Text.Trim(), txtFiltro.Text.Trim());
                    data.Filter = String.Format("[{0}] = '{1}' ", label7.Text.Trim(), txtFiltro.Text.Trim());
                  
                }
                else
                {
                    data.Filter = String.Empty;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void dgvProductoTerminado_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProductoTerminado.IsCurrentCellDirty)
            {
                dgvProductoTerminado.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvProductoTerminado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
         

        }

        

    }
}
