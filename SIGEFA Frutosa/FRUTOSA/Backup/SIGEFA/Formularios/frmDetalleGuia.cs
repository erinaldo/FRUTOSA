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
    public partial class frmDetalleGuia : DevComponents.DotNetBar.OfficeForm
    {

        public Int32 codNotaIngreso;
        public Int32 CodTransaccion;
        public static List<Int32> seleccion = new List<Int32>();
        public Int32 Proceso = 0,repetido=0;
        public Int32 Seleccion = 0;
        public Int32 Procede = 0;//(1)nota de salida (2)venta (3)Pedidoventa
        public Int32 CodProducto = 0;
        clsAdmProducto AdmPro = new clsAdmProducto();
        clsProducto pro = new clsProducto();
        clsProducto prod = new clsProducto();
        public clsDetalleRequerimiento detalle;
        clsUnidadEquivalente uniequi = new clsUnidadEquivalente();
        clsUnidadMedida unidadMed = new clsUnidadMedida();
        clsAdmUnidad Unid = new clsAdmUnidad();
        Double factorconvert = 0;
        clsValidar ok = new clsValidar();
        public DataTable data = new DataTable();
        public Int32 Codlista = 0, codproveedor=0;
        public Int32 codalmacen = 0;
        clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();

        //RAGA Ing MatPrima
        public decimal preciounit;
        public Int32 codIngreso;
        public Int32 codIngMatPrima;


        List<clsProducto> lista = new List<clsProducto>();


        public frmDetalleGuia()
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


                //RAGA
                if (Procede == 16)//Salida Produccion MP
                {
                    pro = AdmPro.CargaProductoDetalleIngMatPrima(frmLogin.iCodAlmacen, Convert.ToInt32(txtCodigo.Text));

                    CodProducto = pro.CodProducto;
                    txtReferencia.Text = pro.Referencia;
                    txtDescripcion.Text = pro.Descripcion;
                    txtUnidad.Text = pro.UnidadDescrip;
                    CargaUnidades(cmbUnidad);
                    cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                  //  txtStock.Text = pro.StockDisponible.ToString();
                    unidadMed = Unid.CargaUnidad(Convert.ToInt32(cmbUnidad.SelectedValue));
                    txtUnidad.Text = unidadMed.Descripcion;
                    txtControlStock.Text = "";
                    txtCantidad.Text = "";
                    btnGuardar.Enabled = true;
                    preciounit = (decimal)pro.PrecioVenta;
                    //Control Stock 5
                    txtControlStock.Enabled = false;
                    txtCantidad.Enabled = true;
                    codNotaIngreso = pro.ICodProductoReferencia;
                  

                }
                else  if (Procede == 14 || Procede == 22){

                    pro = AdmPro.CargaProductoTermDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen,1);
                    CodProducto = pro.CodProducto;
                    txtReferencia.Text = pro.Referencia;
                    txtDescripcion.Text = pro.Descripcion;
                    txtUnidad.Text = pro.UnidadDescrip;
                    CargaUnidades(cmbUnidad);
                    cmbUnidad.SelectedValue = pro.CodUnidadMedida;
               //     txtStock.Text = pro.StockDisponible.ToString();
                    unidadMed = Unid.CargaUnidad(Convert.ToInt32(cmbUnidad.SelectedValue));
                    txtUnidad.Text = unidadMed.Descripcion;
                    txtControlStock.Text = "";
                    txtCantidad.Text = "";
                    btnGuardar.Enabled = true;

                    if (textBox6.Text == "")
                    {
                        textBox1.Text = Convert.ToInt32(pro.Serieactual + 1).ToString();
                        textBox6.Text = Convert.ToInt32(pro.Serieactual + 1).ToString();
                    }

                    if (textBox4.Text == "")
                        textBox4.Text = "1";

                }
                else
                {
                    pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista);
                    CodProducto = pro.CodProducto;
                    txtReferencia.Text = pro.Referencia;
                    txtDescripcion.Text = pro.Descripcion;
                    txtUnidad.Text = pro.UnidadDescrip;
                    CargaUnidades(cmbUnidad);
                    cmbUnidad.SelectedValue = pro.CodUnidadMedida;
          //          txtStock.Text = pro.StockDisponible.ToString();
                    unidadMed = Unid.CargaUnidad(Convert.ToInt32(cmbUnidad.SelectedValue));
                    txtUnidad.Text = unidadMed.Descripcion;
                    txtControlStock.Text = "";
                    txtCantidad.Text = "";
                    btnGuardar.Enabled = true;

                    if (Procede == 12)
                        txtStock.Text = AdmPro.CargaStockProducto(pro.CodProducto, frmLogin.iCodAlmacen, 5).StockDisponible.ToString();

                    




                }

                switch (pro.CodControlStock)
                {
                    case 1:
                        txtControlStock.Enabled = false;
                        txtCantidad.Enabled = true;
                        break;
                    case 2:
                        txtControlStock.Enabled = true;
                        txtCantidad.Enabled = true;
                        txtCantidad.Text = "1";
                        break;
                    case 3:
                        txtControlStock.Enabled = true;
                        txtCantidad.Enabled = false;
                        txtCantidad.Text = "1";
                        break;
                    case 4:
                        txtControlStock.Enabled = false;
                        txtCantidad.Enabled = false;
                        txtCantidad.Text = "1";
                        break;
                }


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

                /*
                if (Procede == 9 || Procede == 12)
                {
                    if (Convert.ToDouble(txtCantidad.Text) > pro.StockDisponible)
                    {
                        MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Focus();
                    }
                }
                */
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

                  //RAGA
                    if (checkBox1.Checked == true && (Procede == 14 || Procede == 22))
                    {
                        textBox2.Text = (Convert.ToInt32(txtCantidad.Text) + Convert.ToInt32(textBox6.Text) - 1).ToString();
                    }
                    else if (Procede == 16 || Procede == 17)
                    {
                        //Verificar Stock 
                        if (Convert.ToDouble(txtCantidad.Text) >= Convert.ToDouble(txtStock.Text))
                        {
                            MessageBox.Show("Cantidad mayor al Stock ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtCantidad.Text = "";
                            // return;
                        }
                    }
                        /*
                    else
                    {
                        MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    */
                 
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
            //Double bruto, montodescuento, valorventa, igv, precioventa, precioreal, valorreal, factorigv;



            //GUIA DE REMISIÓN
            if (Procede == 5)
            {
                frmGuiaRemision form = (frmGuiaRemision)Application.OpenForms["frmGuiaRemision"];

                if (txtCantidad.Text == "")
                {
                    MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtCantidad.Focus();
                }
                else
                {
                    if (form.dgvDetalle.Rows.Count < 10) // se carga el numero de items que soporta el tamaño del formato
                    {
                        if (Proceso == 1)
                            form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                                txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), "", "",
                                "", "", "", "", "", "", "", "", "");

                        limpiarformulario();
                        if (Seleccion == 2)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
          


            //INGRESO DE MATERIA PRIMA
            if (Procede == 10) 
            {

                frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                if (txtCantidad.Text == "" || Convert.ToInt32(txtCantidad.Text) == 0)
                {
                    MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtCantidad.Focus();
                }
                else
                {
                    Decimal Bonificacion, Precio, bruto, montodescuento, valorventa, igv, precioventa, precioreal, valorreal, factorigv, dsc1, dsc2, dsc3, preunitario;

                    if (chBonificacion.Checked)
                    {
                        Precio = 0;
                        Bonificacion = 1;
                    }
                    else
                    {
                        Precio = AdmPro.CargaPrecioProducto(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, Convert.ToInt32(form.cmbMoneda.SelectedValue));
                        Bonificacion = 0;
                    }

                    //if (form.dgvDetalle2.Rows.Count < 10)
                    //// se carga el numero de items que soporta el tamaño del formato
                    //{

                    bruto = Convert.ToDecimal(txtCantidad.Text) * Precio;

                    if (pro.ConIgv)
                    {
                        precioventa = bruto;
                        factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                        valorventa = precioventa / factorigv;
                    }
                    else
                    {
                        valorventa = Precio;
                        precioventa = valorventa;
                    }
                    precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
                    valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
                    igv = precioventa - valorventa;

                    if (Proceso == 1)
                    {
                        data = (DataTable)form.dgvDetalle2.DataSource;
                        if (form.dgvDetalle2.Rows.Count <= 0)
                        {
                            form.dgvDetalle2.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, txtUnidad.Text, cmbUnidad.SelectedValue, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(Precio), bruto,
                               0, 0, 0, 0, valorventa, igv, precioventa, precioreal, valorreal, 0, Convert.ToDouble(txtCantidad.Text), 0, Convert.ToDouble(txtCantidad.Text), 0, Bonificacion);
                            //form.dgvDetalle2.DataSource = data;
                        }
                        else
                        {
                            if (data == null)
                            {
                                form.dgvDetalle2.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, txtUnidad.Text, cmbUnidad.SelectedValue, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(Precio), bruto,
                                  0, 0, 0, 0, valorventa, igv, precioventa, precioreal, valorreal, 0, Convert.ToDouble(txtCantidad.Text), 0, Convert.ToDouble(txtCantidad.Text), 0, Bonificacion);

                            }
                            else
                            {
                                data.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, txtUnidad.Text, cmbUnidad.SelectedValue, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(Precio), bruto,
                               0, 0, 0, 0, valorventa, igv, precioventa, precioreal, valorreal, 0, Convert.ToDouble(txtCantidad.Text), 0, Convert.ToDouble(txtCantidad.Text), 0, Bonificacion);
                                form.dgvDetalle2.DataSource = data;
                            }

                        }

                        limpiarformulario();
                        if (Seleccion == 2)
                        {
                            this.Close();
                        }
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }    
    
  
            //RAGA

            if (Procede == 12) //RECEPCION DE MATERIA PRIMA
            {

                frmIngresoCamaras form = (frmIngresoCamaras)Application.OpenForms["frmIngresoCamaras"];
                if (txtCantidad.Text == "" || Convert.ToInt32(txtCantidad.Text) == 0)
                {
                    MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtCantidad.Focus();
                }
                else
                {
      
                    if (Proceso == 1)
                    {
                        if (form.dgvDetalle.Rows.Count == 0)
                            form.dgvDetalle.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion,cmbUnidad.SelectedValue,txtUnidad.Text,"",Convert.ToDouble(txtCantidad.Text));
                        
                        else if (form.dgvDetalle.Rows.Count > 0)
                            form.dgvDetalle.CurrentRow.SetValues("0", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, "", Convert.ToDouble(txtCantidad.Text));
                        form.codIngMatPrima = pro.CodProducto;
                        form.stockactual = Convert.ToDouble(txtStock.Text) /*+ Convert.ToDouble(txtCantidad.Text)*/;
                        limpiarformulario();
                        if (Seleccion == 2)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }    
    
              //PRODUCTO TERMINADO - REPROCESO
            if (Procede == 14) 
            {

                int inicio;
                int fin;
                int cuenta;
                int valor;



                frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                if (txtCantidad.Text == "" || Convert.ToInt32(txtCantidad.Text) == 0 || textBox3.Text == "" || textBox5.Text == "")
                {
                    MessageBox.Show("Verificar datos", "DETALLE DE ARTICULO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtCantidad.Focus();
                }
                else
                {

                    if (Proceso == 1)
                    {

                        if (checkBox1.Checked == true)
                        {
                            //2do BATCH Prod Terminado
                            form.finSerie = Convert.ToInt32(textBox2.Text);
                          //  form.iniserie = Convert.ToInt32(textBox6.Text);
                            form.codProductoTerm = Convert.ToInt32(txtCodigo.Text);
                            form.nroBatch = Convert.ToInt32(textBox4.Text);
                            Seleccion = 2;

                            inicio = Convert.ToInt32(textBox6.Text);
                            fin = Convert.ToInt32(textBox2.Text);
                            cuenta = fin - inicio + 1;


                            prod.UnidadDescrip = txtUnidad.Text;
                            prod.DGradosBrix = Convert.ToDouble(textBox3.Text);
                            prod.InroBatch = Convert.ToInt32(textBox4.Text);
                            prod.Dpneto = Convert.ToDouble(textBox5.Text);
                            prod.CodUnidadMedida =  Int32.Parse( cmbUnidad.SelectedValue.ToString());


                            for (int i = 0; i < cuenta; i++)
                            {
                                valor = inicio + i;
                                //Valida que no este repetido en el datagridview
                                data = (DataTable)form.dgvDetalle.DataSource;
                                if (form.dgvDetalle.Rows.Count > 0)
                                {
                                    foreach (DataGridViewRow row in form.dgvDetalle.Rows)
                                    {
                                        if (valor == Convert.ToInt32(row.Cells[form.serielote.Name].Value))
                                        {
                                            MessageBox.Show("Código repetido :" + valor );
                                            return;
                                        }
                                    }
                                }

                                form.dgvDetalle.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", txtUnidad.Text, valor.ToString(), Convert.ToDouble("1"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new DateTime(), 0, new DateTime(), 0, 0, Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox4.Text), Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox5.Text));
                                lista.Add(pro);
                            
                            }

                        }
                        else
                        {

                            form.codProductoTerm = Convert.ToInt32(txtCodigo.Text);
                            form.nroBatch = Convert.ToInt32(textBox4.Text);
                            form.finSerie = Convert.ToInt32(textBox1.Text);
                            Seleccion = 2;
                            //Valida que no este repetido en el datagridview
                            data = (DataTable)form.dgvDetalle.DataSource;

                            prod.UnidadDescrip = txtUnidad.Text;
                            prod.DGradosBrix = Convert.ToDouble(textBox3.Text);
                            prod.InroBatch = Convert.ToInt32(textBox4.Text);
                            prod.Dpneto = Convert.ToDouble(textBox5.Text);
                            prod.CodUnidadMedida = Int32.Parse(cmbUnidad.SelectedValue.ToString());



                            if (form.dgvDetalle.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow row in form.dgvDetalle.Rows)
                                {
                                    if (Convert.ToInt32(textBox1.Text) == Convert.ToInt32(row.Cells[form.serielote.Name].Value))
                                    {
                                        MessageBox.Show("Código repetido: " + Convert.ToInt32(textBox1.Text));
                                        return;
                                    }
                                }
                            }

                            form.dgvDetalle.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", txtUnidad.Text, textBox1.Text, Convert.ToDouble("1"), 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new DateTime(), 0, new DateTime(), 0, 0, Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox4.Text), Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox3.Text), Convert.ToDouble(textBox5.Text));
                        }

                        

                  //      form.prod = this.prod;
                        form.stockactual = Convert.ToDouble(txtStock.Text);

                        limpiarformulario();
                        if (Seleccion == 2)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }   
            //RAGA
            if (Procede == 16)//
            {
                Decimal bruto, valorventa, igv, precioventa, precioreal, valorreal, factorigv, dsc1, dsc2, dsc3, preunitario;

                frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
              //  Precio = AdmPro.CargaPrecioProducto(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, Convert.ToInt32(form.cmbMoneda.SelectedValue));


                if (txtCantidad.Text == "")
                {
                    MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    txtCantidad.Focus();
                }
                else
                {
                    bruto = Convert.ToDecimal(txtCantidad.Text) * preciounit;

                    if (pro.ConIgv)
                    {
                        precioventa = bruto;
                        factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                        valorventa = precioventa / factorigv;
                    }
                    else
                    {
                        valorventa = preciounit;
                        precioventa = valorventa;
                    }
                    precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
                    valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
                    igv = precioventa - valorventa;


                    if (Proceso == 1)
                    {
                        form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                                    txtUnidad.Text, codIngreso, Convert.ToDouble(txtCantidad.Text), form.cmbMoneda.SelectedValue, preciounit,
                                    bruto, "", "", "", 0, valorventa, igv, precioventa, precioreal,
                                    valorreal, 0, frmLogin.iCodUser);
                        form.codMatPrima = codNotaIngreso;
                        form.stockDisponible = Convert.ToDecimal(txtStock.Text) - Convert.ToDecimal(txtCantidad.Text);


                         if (CodProducto == pro.CodProducto)
                         {





                         }

                    }
                    else if (Proceso == 2)
                    {
                        Double sumcantidad=0;
                        foreach (DataGridViewRow row in form.dgvDetalle.Rows)
                            sumcantidad = sumcantidad + Convert.ToDouble(row.Cells[form.cantidad.Name].Value.ToString());


                        form.codMatPrima = codNotaIngreso;
                        form.stockDisponible = Convert.ToDecimal(txtStock.Text) - Convert.ToDecimal(txtCantidad.Text);

                        sumcantidad = sumcantidad + Convert.ToDouble(txtCantidad.Text);
                        form.dgvDetalle.CurrentRow.SetValues("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                                  txtUnidad.Text, codIngreso, sumcantidad, form.cmbMoneda.SelectedValue, preciounit,
                                  bruto, "", "", "", 0, valorventa, igv, precioventa, precioreal,
                                  valorreal, 0, frmLogin.iCodUser);
                    }



                    form.CodProducto = pro.CodProducto;
                    this.Close();
                }
            }


            //RAGA
            if (Procede == 17)//
            {
                Decimal bruto, valorventa, igv, precioventa, precioreal, valorreal, factorigv, dsc1, dsc2, dsc3, preunitario;

                frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
                preciounit = AdmPro.CargaPrecioProducto(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, Convert.ToInt32(form.cmbMoneda.SelectedValue));


                if (txtCantidad.Text == "")
                {
                    MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                    txtCantidad.Focus();
                }
                else
                {
                    bruto = Convert.ToDecimal(txtCantidad.Text) * preciounit;

                    if (pro.ConIgv)
                    {
                        precioventa = bruto;
                        factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                        valorventa = precioventa / factorigv;
                    }
                    else
                    {
                        valorventa = preciounit;
                        precioventa = valorventa;
                    }
                    precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
                    valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
                    igv = precioventa - valorventa;
                    form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                                txtUnidad.Text, pro.Serieactual, Convert.ToDouble(txtCantidad.Text), form.cmbMoneda.SelectedValue, preciounit,
                                bruto, "", "", "", 0, valorventa, igv, precioventa, precioreal,
                                valorreal, 0, frmLogin.iCodUser);


                    
                    form.CodProducto = pro.CodProducto;
                    this.Close();
                }

                
            }


            if (Procede == 22) //Cuando abre desde Salida por Reproceso
            {

                int inicio;
                int fin;
                int cuenta;
                int valor;


                if (textBox3.Text == "")
                    return;
                if (textBox5.Text == "")
                    return;




                frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
                if (txtCantidad.Text == "" || Convert.ToInt32(txtCantidad.Text) == 0)
                {
                    MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    txtCantidad.Focus();
                }
                else
                {

                    if (Proceso == 1)
                    {




                        if (checkBox1.Checked == true)
                        {


                            //2do BATCH Prod Terminado
                            form.finSerie = Convert.ToInt32(textBox2.Text);
                            //  form.iniserie = Convert.ToInt32(textBox6.Text);
                            form.codProductoTerm = Convert.ToInt32(txtCodigo.Text);
                            form.nroBatch = Convert.ToInt32(textBox4.Text);
                            Seleccion = 2;

                            inicio = Convert.ToInt32(textBox6.Text);
                            fin = Convert.ToInt32(textBox2.Text);
                            cuenta = fin - inicio + 1;

                            for (int i = 0; i < cuenta; i++)
                            {
                                valor = inicio + i;

                                //Valida que no este repetido en el datagridview
                                data = (DataTable)form.dgvDetalle.DataSource;
                                if (form.dgvDetalle.Rows.Count > 0)
                                {
                                    foreach (DataGridViewRow row in form.dgvDetalle.Rows)
                                    {
                                        if (valor == Convert.ToInt32(row.Cells[form.serielote.Name].Value))
                                        {
                                            MessageBox.Show("Código repetido :" + valor);
                                            return;
                                        }
                                    }
                                }



                                form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                                  txtUnidad.Text, valor, Convert.ToDouble(txtCantidad.Text), form.cmbMoneda.SelectedValue, preciounit,
                                  0, "", "", "", 0, 0, 0, 0, 0, 0, 0, frmLogin.iCodUser, new DateTime(), 0, 0, 0, 0, 0, Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox4.Text), Convert.ToDouble(textBox5.Text));
              
                            
                            }

                        }
                        else
                        {

                            form.codProductoTerm = Convert.ToInt32(txtCodigo.Text);
                            form.nroBatch = Convert.ToInt32(textBox4.Text);
                            form.finSerie = Convert.ToInt32(textBox1.Text);
                            Seleccion = 2;


                            //Valida que no este repetido en el datagridview
                            data = (DataTable)form.dgvDetalle.DataSource;
                            if (form.dgvDetalle.Rows.Count > 0)
                            {
                                foreach (DataGridViewRow row in form.dgvDetalle.Rows)
                                {
                                    if (Convert.ToInt32(textBox1.Text) == Convert.ToInt32(row.Cells[form.serielote.Name].Value))
                                    {
                                        MessageBox.Show("Código repetido: " + Convert.ToInt32(textBox1.Text));
                                        return;
                                    }
                                }
                            }



                            form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                                  txtUnidad.Text, textBox1.Text, Convert.ToDouble(txtCantidad.Text), form.cmbMoneda.SelectedValue, preciounit,
                                  0, "", "", "", 0, 0, 0, 0, 0, 0, 0, frmLogin.iCodUser, new DateTime(), 0, 0, 0, 0, 0, Convert.ToDouble(textBox3.Text), Convert.ToInt32(textBox4.Text), Convert.ToDouble(textBox5.Text));
              
                        
                        
                        }


                        limpiarformulario();
                        if (Seleccion == 2)
                        {
                            this.Close();
                        }
                        //}
                    }
                    else
                    {
                        MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

            }






            this.Close();

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
           //     txtCantidad.Focus();
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

        private void frmDetalleGuia_Load(object sender, EventArgs e)
        {
            CargaUnidades(cmbUnidad);
            txtReferencia.Focus();
            if (Procede == 14 || Procede == 22)
            {

                label14.Visible = true;
                textBox3.Visible = true;
                label15.Visible = true;
                textBox4.Visible = true;
                label16.Visible = true;
                textBox5.Visible = true;
                textBox1.Visible = true;
                label7.Visible = true;
                checkBox1.Visible = true;
             
            }



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
            if (checkBox1.Checked == true)
            {
                label9.Visible = true;
                textBox6.Visible = true;
                label8.Visible = true;
                textBox2.Visible = true;

                label7.Visible = false;
                textBox1.Visible = false;
                txtCantidad.ReadOnly = false;
                txtCantidad.Enabled = true;
                txtCantidad.Text = "";
                txtCantidad.Focus();

            }
            else
            {
                label9.Visible = false;
                textBox6.Visible = false;
                label8.Visible = false;
                textBox2.Visible = false;

                label7.Visible = true;
                textBox1.Visible = true;
                txtCantidad.Text = String.Format("{0:#,##0.00}", "1");
                txtCantidad.ReadOnly = true;
                txtCantidad.Enabled = false;
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {

            if (textBox5.Text != "")
            {
                if (Convert.ToDouble(textBox5.Text) > 0)
                    textBox5.Text = String.Format("{0:#,##0.0000}", textBox5.Text);
                else
                    MessageBox.Show("PESO NETO no puede ser 0");

            }

        //    textBox5.Text = String.Format("{0:#,##0.0000}", textBox5.Text); 
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                if (Convert.ToDouble(textBox3.Text) > 0)
                    textBox3.Text = String.Format("{0:#,##0.0000}", textBox3.Text);
                else
                    MessageBox.Show("Grados BRIX no puede ser 0");
            }

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

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                if (Convert.ToDouble(textBox4.Text) == 0)
                {
                    MessageBox.Show("NRO BATCH no puede ser 0");
                    textBox4.Text = "";
                }

            }
        }

        

    }
}
