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
    public partial class frmDetalleIngreso : DevComponents.DotNetBar.OfficeForm
    {
        public static List<Int32> seleccion = new List<Int32>();
        public Int32 Proceso = 0,repetido=0, proce=0;
        public Int32 Procede = 0;
        public Int32 Seleccion = 0;
        public Int32 CodProducto = 0, codproveedor=0;
        public Boolean bvalorventa = false;
        clsAdmProducto AdmPro = new clsAdmProducto();
        clsProducto pro = new clsProducto();
        clsProducto pro1 = new clsProducto();
        public Int32 CodLista = 0;
        clsValidar ok = new clsValidar();
        public List<clsDetalleNotaIngreso> productoscargados = new List<clsDetalleNotaIngreso>(); // relacion de los productos que ya han sido cargado en la nota de ingreso       
        public DataTable data = new DataTable();

        //RAGA
        public Int32 Procede2 = 0;
        public Int32 CodUnidad;

        public Int32 Codlista;



        public frmDetalleIngreso()
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
            if (/*Seleccion == 2 && */txtCodigo.Text != "")
            {
                pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 1,CodLista);
                //pro1 = AdmPro.CargaDatosProductoOrden(pro.CodProducto, frmLogin.iCodAlmacen, frmLogin.iCodUser);
                if (Procede == 8)
                {
                    frmOrdenCompras form = (frmOrdenCompras)Application.OpenForms["frmOrdenCompras"];
                    if (form.codProd.Contains(pro.CodProducto))
                    {
                        MessageBox.Show("El Producto ya existe");
                        repetido = 1;
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
                        switch (pro.CodControlStock)
                        {
                            case 1: txtControlStock.Enabled = false; txtCantidad.Enabled = true; break;
                            case 2: txtControlStock.Enabled = true; txtCantidad.Enabled = true; break;
                            case 3: txtControlStock.Enabled = true; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                            case 4: txtControlStock.Enabled = false; txtCantidad.Enabled = false; txtCantidad.Text = ""; break;
                        }
                    }
                }
                else if (Procede == 6 || Procede == 11 )
                {
                    frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                    if (form.codProd.Contains(pro.CodProducto))
                    {
                        MessageBox.Show("El Producto ya existe");
                        repetido = 1;
                    }
                    else
                    {
                        CodProducto = pro.CodProducto;
                        txtReferencia.Text = pro.Referencia;
                        txtDescripcion.Text = pro.Descripcion;
                        txtUnidad.Text = pro.UnidadDescrip;

                        CargaUnidades(cmbUnidad);
                        cmbUnidad.SelectedValue = pro.CodUnidadMedida;
           //             txtStock.Text = pro.StockDisponible.ToString();
                        switch (pro.CodControlStock)
                        {
                            case 1: txtControlStock.Enabled = false; txtCantidad.Enabled = true; break;
                            case 2: txtControlStock.Enabled = true; txtCantidad.Enabled = true; break;
                            case 3: txtControlStock.Enabled = true; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                            case 4: txtControlStock.Enabled = false; txtCantidad.Enabled = false; txtCantidad.Text = ""; break;
                        }
                        txtCantidad.Focus();

                        
                    }
                }

                else if (Procede == 9)
                {
                    /*
                    frmIngresoCamaras form = (frmIngresoCamaras)Application.OpenForms["frmIngresoCamaras"];
                    if (form.codProd.Contains(pro.CodProducto))
                    {
                        MessageBox.Show("El Producto ya existe");
                        repetido = 1;
                    }
                        */
                 //   else
                  //  {
                        CodProducto = pro.CodProducto;
                        txtReferencia.Text = pro.Referencia;
                        txtDescripcion.Text = pro.Descripcion;
                        txtUnidad.Text = pro.UnidadDescrip;

                        CargaUnidades(cmbUnidad);
                        cmbUnidad.SelectedValue = pro.CodUnidadMedida;
           //             txtStock.Text = pro.StockDisponible.ToString();
                        switch (pro.CodControlStock)
                        {
                            case 1: txtControlStock.Enabled = false; txtCantidad.Enabled = true; break;
                            case 2: txtControlStock.Enabled = true; txtCantidad.Enabled = true; break;
                            case 3: txtControlStock.Enabled = true; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                            case 4: txtControlStock.Enabled = false; txtCantidad.Enabled = false; txtCantidad.Text = ""; break;
                        }
                   // }
                }
                    /*
                else if (CodProducto == 10 || CodProducto == 11)
                {
                    txtStock.Text = AdmPro.CargaStockProducto(pro.CodProducto, frmLogin.iCodAlmacen, 5).StockDisponible.ToString();

                }
                */







            }
        }       

        private void txtDscto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "")
                    {
                        txtDscto1.Text = "0.000";
                    }
                    txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)));
                }
                ProcessTabKey(true);
            }
        }

        private void txtDscto_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "")
            {
                if (txtDscto1.Text == "")
                {
                    txtDscto1.Text = "0.000";
                }
                txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)));
            }             
        }

        private void txtPrecioNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            //if( txtPrecioNeto.Text != "")
            //{
            //    if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
            //    if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
            //    if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
            //    if (txtCantidad.Text != "")
            //    {
            //        txtPrecio.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecioNeto.Text) / (1 - (Convert.ToDouble(txtDscto3.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) / Convert.ToDouble(txtCantidad.Text));
            //    }               
            //}
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "") { txtDscto1.Text = "0.000"; }
                    if (txtDscto2.Text == "") { txtDscto2.Text = "0.000"; }
                    if (txtDscto3.Text == "") { txtDscto3.Text = "0.000"; }
                    if (txtCantidad.Text != "")
                    {
                        txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                    }
                }
                ProcessTabKey(true);
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "") { txtDscto1.Text = "0.000"; }
                    if (txtDscto2.Text == "") { txtDscto2.Text = "0.000"; }
                    if (txtDscto3.Text == "") { txtDscto3.Text = "0.000"; }
                    if (txtCantidad.Text != "" || Convert.ToDecimal(txtCantidad.Text) != 0)
                    {
                        txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                    }
                    else { txtCantidad.Focus(); }
                }
                if (txtCantidad.Text != "")
                    if (Convert.ToDecimal(txtCantidad.Text) != 0)
                        txtPrecio.Focus();
                    else txtCantidad.Focus();
                else txtCantidad.Focus();

                //ProcessTabKey(true);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


            Double bruto, montodescuento, valorventa, igv, precioventa, precioreal, valorreal, factorigv, dsc1, dsc2, dsc3, preunitario;
                 


            if (txtCantidad.Text != "")
            {
                if (Convert.ToDecimal(txtCantidad.Text) != 0)
                {
                    /*
                     * 6:   COMPRAS INSUMOS Y SUMINISTROS
                     * 8:   ORDEN DE COMPRA
                     * 10:  INGRESO MATERIA PRIMA + ING.CAMARA
                     * 11:  INGRESO MATERIA PRIMA 
                     */


                    if (Procede == 6)//COMPRAS DIRECTAS : INSUMOS Y SUMINISTROS
                    {
                        frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                        if (bvalorventa) //VALOR DE EVENTA
                        {
                            bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                            montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
                            if (pro.ConIgv)
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                                precioventa = valorventa * factorigv;
                            }
                            else
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                precioventa = valorventa;
                            }
                            precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                            valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                            igv = precioventa - valorventa;
                            if (txtDscto1.Text != "") { dsc1 = Convert.ToDouble(txtDscto1.Text); } else { dsc1 = 0.00; }
                            if (txtDscto2.Text != "") { dsc2 = Convert.ToDouble(txtDscto2.Text); } else { dsc2 = 0.00; }
                            if (txtDscto3.Text != "") { dsc3 = Convert.ToDouble(txtDscto3.Text); } else { dsc3 = 0.00; }

                            if (Proceso == 1)
                            {
                                form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();

                                if (Seleccion == 2)
                                {
                                    this.Close();
                                }
                            }
                            else if (Proceso == 2)
                            {
                                form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();
                                this.Close();
                            }
                        }
                        else //PRECIO DE EVENTA
                        {
                            bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                            montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);

                            if (pro.ConIgv)
                            {
                                precioventa = Convert.ToDouble(txtPrecioNeto.Text);
                                factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                                valorventa = precioventa / factorigv;
                            }
                            else
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                precioventa = valorventa;
                            }

                            precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                            valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                            igv = precioventa - valorventa;
                            if (txtDscto1.Text != "") { dsc1 = Convert.ToDouble(txtDscto1.Text); } else { dsc1 = 0.00; }
                            if (txtDscto2.Text != "") { dsc2 = Convert.ToDouble(txtDscto2.Text); } else { dsc2 = 0.00; }
                            if (txtDscto3.Text != "") { dsc3 = Convert.ToDouble(txtDscto3.Text); } else { dsc3 = 0.00; }

                            if (Proceso == 1)
                            {
                                form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();

                                if (Seleccion == 2)
                                {
                                    this.Close();
                                }
                            }
                            else if (Proceso == 2)
                            {
                                form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();
                                this.Close();
                            }
                        }
                    }



                    if (Procede == 8)//ORDEN DE COMPRA
                    {

                        frmOrdenCompras form = (frmOrdenCompras)Application.OpenForms["frmOrdenCompras"];

                        if (bvalorventa)
                        {
                            bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                            montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
                            if (pro.ConIgv)
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text) - montodescuento;
                                // precioventa = Convert.ToDouble(txtPrecioNeto.Text);
                                factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                                //valorventa = precioventa / factorigv;
                                precioventa = valorventa * factorigv;
                            }
                            else
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                precioventa = valorventa;
                            }
                            precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                            valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                            igv = precioventa - valorventa;
                            if (txtDscto1.Text != "") { dsc1 = Convert.ToDouble(txtDscto1.Text); } else { dsc1 = 0.00; }
                            if (txtDscto2.Text != "") { dsc2 = Convert.ToDouble(txtDscto2.Text); } else { dsc2 = 0.00; }
                            if (txtDscto3.Text != "") { dsc3 = Convert.ToDouble(txtDscto3.Text); } else { dsc3 = 0.00; }

                            if (Proceso == 1 || Proceso == 3)
                            {
                                if (form.Proce == 2)
                                {
                                    form.dgvDetalle.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, 0,
                                       cmbUnidad.Text, "0", Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                       dsc1, dsc2, dsc3, montodescuento, valorventa,
                                       valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, DateTime.Now, frmLogin.iCodUser, DateTime.Now, pro1.Porllegar, pro1.PorAtender, pro1.PorCompletar);

                                }
                                else if (form.Proce == 1)
                                {
                                    if (form.dgvDetalle.DataSource == null)
                                    {
                                        form.dgvDetalle.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, 0,
                                       cmbUnidad.Text, "0", Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                       dsc1, dsc2, dsc3, montodescuento, valorventa,
                                       valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, DateTime.Now, frmLogin.iCodUser, DateTime.Now, pro1.Porllegar, pro1.PorAtender, pro1.PorCompletar);

                                    }
                                    else
                                    {
                                        data = (DataTable)form.dgvDetalle.DataSource;
                                        data.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, frmLogin.iCodUser, DateTime.Now,
                                           cmbUnidad.Text, "0", Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                           dsc1, dsc2, dsc3, montodescuento, valorventa,
                                           valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, DateTime.Now, 0, pro1.Porllegar, pro1.PorAtender, pro1.PorCompletar);
                                        form.dgvDetalle.DataSource = data;
                                    }
                                }
                                limpiarformulario();
                                if (Seleccion == 2)
                                {
                                    this.Close();
                                }
                            }
                        }
                    }



                    if (Procede == 10)//INGRESO MATERIA PRIMA + INGRESO CÁMARA
                    {

                        frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                        if (bvalorventa) //VALOR DE EVENTA
                        {
                            bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                            montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
                            if (pro.ConIgv)
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                                precioventa = valorventa * factorigv;
                            }
                            else
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                precioventa = valorventa;
                            }
                            precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                            valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                            igv = precioventa - valorventa;
                            if (txtDscto1.Text != "") { dsc1 = Convert.ToDouble(txtDscto1.Text); } else { dsc1 = 0.00; }
                            if (txtDscto2.Text != "") { dsc2 = Convert.ToDouble(txtDscto2.Text); } else { dsc2 = 0.00; }
                            if (txtDscto3.Text != "") { dsc3 = Convert.ToDouble(txtDscto3.Text); } else { dsc3 = 0.00; }

                            if (Proceso == 1)
                            {
                                form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();

                                if (Seleccion == 2)
                                {
                                    this.Close();
                                }
                            }
                            else if (Proceso == 2)
                            {
                                form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();
                                this.Close();
                            }
                        }
                        else //PRECIO DE EVENTA
                        {
                            bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                            montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);

                            if (pro.ConIgv)
                            {
                                precioventa = Convert.ToDouble(txtPrecioNeto.Text);
                                factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                                valorventa = precioventa / factorigv;
                            }
                            else
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                precioventa = valorventa;
                            }

                            precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                            valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                            igv = precioventa - valorventa;
                            if (txtDscto1.Text != "") { dsc1 = Convert.ToDouble(txtDscto1.Text); } else { dsc1 = 0.00; }
                            if (txtDscto2.Text != "") { dsc2 = Convert.ToDouble(txtDscto2.Text); } else { dsc2 = 0.00; }
                            if (txtDscto3.Text != "") { dsc3 = Convert.ToDouble(txtDscto3.Text); } else { dsc3 = 0.00; }

                            if (Proceso == 1)
                            {
                                form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();

                                if (Seleccion == 2)
                                {
                                    this.Close();
                                }
                            }
                            else if (Proceso == 2)
                            {
                                form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();
                                this.Close();
                            }
                        }
                    }


                    if (Procede == 11)//INGRESO MATERIA PRIMA DESDE UNA GUIA(RECEPCIÓN )
                    {

                        frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                        if (bvalorventa) //VALOR DE EVENTA
                        {
                            bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                            montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
                            if (pro.ConIgv)
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                                precioventa = valorventa * factorigv;
                            }
                            else
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                precioventa = valorventa;
                            }
                            precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                            valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                            igv = precioventa - valorventa;
                            if (txtDscto1.Text != "") { dsc1 = Convert.ToDouble(txtDscto1.Text); } else { dsc1 = 0.00; }
                            if (txtDscto2.Text != "") { dsc2 = Convert.ToDouble(txtDscto2.Text); } else { dsc2 = 0.00; }
                            if (txtDscto3.Text != "") { dsc3 = Convert.ToDouble(txtDscto3.Text); } else { dsc3 = 0.00; }

                            if (Proceso == 1)
                            {
                                form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();

                                if (Seleccion == 2)
                                {
                                    this.Close();
                                }
                            }
                            else if (Proceso == 2)
                            {
                                form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();
                                this.Close();
                            }
                        }
                        else //PRECIO DE EVENTA
                        {
                            bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                            montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);

                            if (pro.ConIgv)
                            {
                                precioventa = Convert.ToDouble(txtPrecioNeto.Text);
                                factorigv = frmLogin.Configuracion.IGV / 100 + 1;
                                valorventa = precioventa / factorigv;
                            }
                            else
                            {
                                valorventa = Convert.ToDouble(txtPrecioNeto.Text);
                                precioventa = valorventa;
                            }

                            precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
                            valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
                            igv = precioventa - valorventa;
                            if (txtDscto1.Text != "") { dsc1 = Convert.ToDouble(txtDscto1.Text); } else { dsc1 = 0.00; }
                            if (txtDscto2.Text != "") { dsc2 = Convert.ToDouble(txtDscto2.Text); } else { dsc2 = 0.00; }
                            if (txtDscto3.Text != "") { dsc3 = Convert.ToDouble(txtDscto3.Text); } else { dsc3 = 0.00; }

                            if (Proceso == 1)
                            {
                                form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();

                                if (Seleccion == 2)
                                {
                                    this.Close();
                                }
                            }
                            else if (Proceso == 2)
                            {
                                form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "",
                                    cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                                    dsc1, dsc2, dsc3, montodescuento, valorventa,
                                    valorventa, igv, 0.00, precioventa, precioventa, precioreal, valorreal, "", "", "");
                                limpiarformulario();
                                this.Close();
                            }
                        }
                    }
                  






                }
                else { txtCantidad.Focus(); }
            }
            else { txtCantidad.Focus(); } 
        }

        private void txtDscto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "") { txtDscto1.Text = "0.000"; }
                    if (txtDscto2.Text == "") { txtDscto2.Text = "0.000"; }
                    if (txtDscto3.Text == "") { txtDscto3.Text = "0.000"; }
                    txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                }
                ProcessTabKey(true);
            }
        }

        private void txtDscto2_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "")
            {
                if (txtDscto1.Text == "") { txtDscto1.Text = "0.000"; }
                if (txtDscto2.Text == "") { txtDscto2.Text = "0.000"; }
                if (txtDscto3.Text == "") { txtDscto3.Text = "0.000"; }
                txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
            }
        }

        private void txtDscto3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "") { txtDscto1.Text = "0.000"; }
                    if (txtDscto2.Text == "") { txtDscto2.Text = "0.000"; }
                    if (txtDscto3.Text == "") { txtDscto3.Text = "0.000"; }
                    txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                }
                ProcessTabKey(true);
            }
        }

        private void txtDscto3_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "")
            {
                if (txtDscto1.Text == "") { txtDscto1.Text = "0.000"; }
                if (txtDscto2.Text == "") { txtDscto2.Text = "0.000"; }
                if (txtDscto3.Text == "") { txtDscto3.Text = "0.000"; }
                txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
            }           
        }

        private void txtControlStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                ProcessTabKey(true);
            }
        }

        private void frmDetalleIngreso_Shown(object sender, EventArgs e)
        {
            if (Seleccion == 2)
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
            txtReferencia.Focus();
     
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            
            if (txtPrecio.Text != "")
            {
                if (txtDscto1.Text == "")
                {
                    txtDscto1.Text = "0.000";
                }
                if (txtCantidad.Text != "")
                {
                    txtPrecioNeto.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)));
                }
            }
            if (pro != null && txtCantidad.Text != "" && txtPrecio.Text != "")
            {
                if (Convert.ToDouble(txtCantidad.Text) > 0 && Convert.ToDouble(txtPrecio.Text) > 0)
                {
                    btnGuardar.Enabled = true;
                }
                else
                {
                    btnGuardar.Enabled = false;
                }

            }
            else
            {
                btnGuardar.Enabled = false;
            }
            
        }

        private void txtReferencia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmProductosLista frm = new frmProductosLista();
                frm.Proceso = Proceso;
                frm.Procede = Procede;
                frm.codproveedor = codproveedor;
                frm.bvalorventa = bvalorventa;
                frm.productoscargados = productoscargados;
                frm.ShowDialog();                
            }
            else if(e.KeyCode == Keys.F2)
            {
                frmRegistroProducto frm = new frmRegistroProducto();
                frm.ShowDialog();
            }
        }

        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (e.KeyChar == (char)Keys.Return)
            {
                
                if (txtReferencia.Text != "")
                {                    
                    if (BuscaProducto())
                    {                        
                        ProcessTabKey(true);
                    }
                    //else
                    //{
                    //    MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
                    //}
                }
                
            }
        */

        }

        private Boolean verificaproductoscargados()
        {
            foreach (clsDetalleNotaIngreso det in productoscargados)
            {
                if(det.CodProEquals(pro.CodProducto))
                {
                    return false;
                    break;
                }
            }
            return true;
        }

        private Boolean BuscaProducto()
        {
            pro = AdmPro.CargaProductoDetalleR(txtReferencia.Text, frmLogin.iCodAlmacen,1, CodLista);
            if (pro != null)
            {
                if (verificaproductoscargados())
                {
                    if (Procede == 8)
                    {
                        frmOrdenCompras form = (frmOrdenCompras)Application.OpenForms["frmOrdenCompras"];
                        if (form.codProd.Contains(pro.CodProducto))
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
                            txtPrecio.Text = "";
                            txtDscto1.Text = "";
                            txtDscto2.Text = "";
                            txtDscto3.Text = "";
                            txtPrecioNeto.Text = "";
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
                                    break;
                                case 4:
                                    txtControlStock.Enabled = false;
                                    txtCantidad.Enabled = false;
                                    break;
                            }
                            return true;
                        }
                    }else if (Procede == 6)
                    {
                        frmNotaIngreso form = (frmNotaIngreso) Application.OpenForms["frmNotaIngreso"];
                        if (form.codProd.Contains(pro.CodProducto))
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
                            txtPrecio.Text = "";
                            txtDscto1.Text = "";
                            txtDscto2.Text = "";
                            txtDscto3.Text = "";
                            txtPrecioNeto.Text = "";
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
                                    break;
                                case 4:
                                    txtControlStock.Enabled = false;
                                    txtCantidad.Enabled = false;
                                    break;
                            }
                            return true;
                        }
                    }


                        //raga

                    else if (Procede == 9)
                    {
                     
                            txtReferencia.Text = pro.Referencia;
                            txtDescripcion.Text = pro.Descripcion;
                            txtUnidad.Text = pro.UnidadDescrip;
                            CargaUnidades(cmbUnidad);
                            cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                            txtStock.Text = pro.StockDisponible.ToString();
                            txtControlStock.Text = "";
                            txtCantidad.Text = "";
                            txtPrecio.Text = "";
                            txtDscto1.Text = "";
                            txtDscto2.Text = "";
                            txtDscto3.Text = "";
                            txtPrecioNeto.Text = "";
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
                                    break;
                                case 4:
                                    txtControlStock.Enabled = false;
                                    txtCantidad.Enabled = false;
                                    break;
                            }
                            return true;
                        
                    }











                    else
                    {
                        return false;}
                    
                }
                else 
                {
                    MessageBox.Show("El producto ya ha sido seleccionado", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CodProducto = 0;
                    txtDescripcion.Text = "";
                    txtUnidad.Text = "";
                    cmbUnidad.SelectedIndex = -1;
                    txtStock.Text = "";
                    txtControlStock.Text = "";
                    txtCantidad.Text = "";
                    txtPrecio.Text = "";
                    txtDscto1.Text = "";
                    txtDscto2.Text = "";
                    txtDscto3.Text = "";
                    txtPrecioNeto.Text = "";
                    return false;
                }                
            }
            else
            {
                MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);                       

                CodProducto = 0;                
                txtDescripcion.Text = "";
                txtUnidad.Text = "";
                cmbUnidad.SelectedIndex = -1;
                txtStock.Text = "";
                txtControlStock.Text = ""; 
                txtCantidad.Text = "";
                txtPrecio.Text = "";
                txtDscto1.Text = "";
                txtDscto2.Text = "";
                txtDscto3.Text = "";
                txtPrecioNeto.Text = "";
                return false;
            }
        }

        private void frmDetalleIngreso_Load(object sender, EventArgs e)
        {
            //RAGA
            if (Procede == 10)
            {
                groupBox1.Enabled = false;
                label12.Visible = true;
                label13.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox2.Focus();
            }
            else
            {
                label12.Visible = false;
                label13.Visible = false;
                textBox1.Visible = false;
                textBox2.Visible = false;
                txtReferencia.Focus();

            }
        }

        private void CargaUnidades(ComboBox combo)
        {           
            combo.DataSource = AdmPro.CargaUnidadesEquivalentes(pro.CodProducto);
            combo.DisplayMember = "descripcion";
            combo.ValueMember = "codUnidadMedida";
            combo.SelectedValue = pro.CodUnidadMedida;
        }

        private void txtReferencia_Leave(object sender, EventArgs e)
        {
            /*
            if (txtReferencia.Text != "" && txtReferencia.ReadOnly == false)
            {
                if (BuscaProducto())
                {
                    txtCantidad.Focus();
                }
                else
                {
                    txtReferencia.Focus();
                }
            }

            */
        }

        private void txtPrecioNeto_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPrecioNeto.Text != "")
            {
                if (txtDscto1.Text == "") { txtDscto1.Text = "0.000"; }
                if (txtDscto2.Text == "") { txtDscto2.Text = "0.000"; }
                if (txtDscto3.Text == "") { txtDscto3.Text = "0.000"; }
                if (txtCantidad.Text != "")
                {
                    if (Convert.ToDecimal(txtDscto1.Text) == 100) { /*txtPrecio.Text = String.Format("{0:#,##0.0000}", 0); */}
                    else { txtPrecio.Text = String.Format("{0:#,##0.0000}", Convert.ToDouble(txtPrecioNeto.Text) / (1 - (Convert.ToDouble(txtDscto3.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) / Convert.ToDouble(txtCantidad.Text)); }
                }
            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "" && Procede == 8)
            {
                pro1 = AdmPro.CargaDatosProductoOrden(pro.CodProducto, frmLogin.iCodAlmacen, frmLogin.iCodUser, Convert.ToDecimal(txtCantidad.Text));
                //RAGA
                if (pro1.PorAtender < 0)
                    pro1.PorAtender = 0;

                if (pro1.PorCompletar < 0)
                    pro1.PorCompletar = 0;

                if (pro1.Porllegar < 0)
                    pro1.Porllegar = 0;

            }
        
       }

        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "")
                if (Convert.ToDecimal(txtCantidad.Text) != 0)
                    txtPrecio.Focus();
                else txtCantidad.Focus();
            else txtCantidad.Focus(); 
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            /*
            if (textBox2.Text != "")
            {

                if (Convert.ToDecimal(textBox2.Text) > Convert.ToDecimal(textBox1.Text))
                {
                    MessageBox.Show("La cantidad Devuelta no puede ser Mayor a la Ingresada");
                    txtCantidad.ReadOnly = false;
                }
                else
                {
                    groupBox1.Enabled = true;
                    txtCantidad.Text = String.Format("{0:#,##0.0000}", Convert.ToDecimal(textBox1.Text) - Convert.ToDecimal(textBox2.Text));

                    if (cmbUnidad.Items.Count <= 0)
                    {
                        CargaUnidades(cmbUnidad);
                        cmbUnidad.SelectedValue = CodUnidad;
                    }
                    txtPrecio.Focus();
                    txtCantidad.ReadOnly = true;
                }

            }
            else textBox2.Focus(); 
            */

            /*
            if (textBox2.Text != "")


                if (Convert.ToDecimal(textBox2.Text) != 0)
                {

                    if (Convert.ToDecimal(textBox2.Text) >= Convert.ToDecimal(textBox1.Text))
                        MessageBox.Show("La cantidad Devuelta no puede ser Mayor a la Ingresada");
                    else
                    {
                        groupBox1.Enabled = true;
                        txtCantidad.Text = String.Format("{0:#,##0.0000}", Convert.ToDecimal(textBox1.Text) - Convert.ToDecimal(textBox2.Text));

                        if (cmbUnidad.Items.Count <= 0)
                        {
                            CargaUnidades(cmbUnidad);
                            cmbUnidad.SelectedValue = CodUnidad;
                        }
                        txtPrecio.Focus();
                        txtCantidad.ReadOnly = true;
                    }

                }
                else textBox2.Focus();
            else textBox2.Focus(); 

            */
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (textBox2.Text != "")
                {

                    if (Convert.ToDecimal(textBox2.Text) > Convert.ToDecimal(textBox1.Text))
                    {
                        MessageBox.Show("La cantidad Devuelta no puede ser Mayor a la Ingresada");
                        txtCantidad.ReadOnly = false;
                    }
                    else
                    {
                        groupBox1.Enabled = true;
                        txtCantidad.Text = String.Format("{0:#,##0.0000}", Convert.ToDecimal(textBox1.Text) - Convert.ToDecimal(textBox2.Text));

                        if (cmbUnidad.Items.Count <= 0)
                        {
                            CargaUnidades(cmbUnidad);
                            cmbUnidad.SelectedValue = CodUnidad;
                        }
                        txtPrecio.Focus();
                        txtCantidad.ReadOnly = true;
                    }

                }
                else textBox2.Focus(); 
              //  ProcessTabKey(true);
            }

        }

        
    }
}
