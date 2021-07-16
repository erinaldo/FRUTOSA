﻿using System;
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
    public partial class frmDetalleSalida : DevComponents.DotNetBar.OfficeForm
    {
        public static List<Int32> seleccion = new List<Int32>();
        public Int32 Proceso = 0;
        public Int32 Seleccion = 0;
        public Int32 Procede = 0;//(1)nota de salida (2)venta (3)Pedidoventa
        public Int32 Tipo = 0; //(1)Cotizacion (2)Venta
        public Int32 Moneda = 0;
        public Int32 CodProducto = 0;
        public Double tc = 0;
        public List<clsDetalleFacturaVenta> productoscargados = new List<clsDetalleFacturaVenta>(); // relacion de los productos que ya han sido cargado en la nota de salida       
        public List<clsDetalleCotizacion> productoscotizados = new List<clsDetalleCotizacion>(); // relacion de los productos que ya han sido cargado en la cotizacion       
        clsAdmProducto AdmPro = new clsAdmProducto();
        clsProducto pro = new clsProducto();
        public clsDetalleNotaSalida detalle = new clsDetalleNotaSalida();
        clsUnidadEquivalente uniequi = new clsUnidadEquivalente();
        clsValidar ok = new clsValidar();
       
        TextBox manipulado = new TextBox();

        clsAdmListaPrecio AdmLista = new clsAdmListaPrecio();
        public clsListaPrecio listaprecio = new clsListaPrecio();

        Double factorconvert = 0;
        public Int32 Codlista = 0;
        Boolean changeimporte = false;
        public Decimal puInicio = 0;
        private Double precioprod = 0;

        public frmDetalleSalida()
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
            if (/*Seleccion == 2 &&*/ txtCodigo.Text != "")
            {
                pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista);
                listaprecio = AdmLista.CargaListaPrecio(Codlista);
                CodProducto = pro.CodProducto;
                txtReferencia.Text = pro.Referencia;
                txtDescripcion.Text = pro.Descripcion;
                txtUnidad.Text = pro.UnidadDescrip;
                CargaUnidades(cmbUnidad);
                cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                txtStock.Text = pro.StockDisponible.ToString();
                txtControlStock.Text = "";
                txtCantidad.Text = "";
                if (pro.PrecioVariable) { txtPrecio.ReadOnly = false; txtPrecioNeto.ReadOnly = false; } else { /*txtPrecio.ReadOnly = true;*/ txtPrecioNeto.ReadOnly = true; }
                if (Moneda == 2) { txtPrecio.Text = pro.PrecioVenta.ToString(); puInicio = Convert.ToDecimal(pro.PrecioVenta); } else if (Moneda == 1) { txtPrecio.Text = pro.PrecioVentaSoles.ToString(); puInicio = Convert.ToDecimal(pro.PrecioVentaSoles);/*Math.Round((pro.PrecioVenta * tc),listaprecio.Decimales).ToString()*/ }
                if (pro.Oferta) { txtDscto1.Text = pro.PDescuento.ToString(); txtDscto1.ReadOnly = true; } else { txtDscto1.Text = ""; }
                txtDscto2.Text = "";
                txtDscto3.Text = "";
                if (pro.Oferta) { txtPrecioNeto.Text = pro.PrecioOferta.ToString(); } else { txtPrecioNeto.Text = pro.PrecioVenta.ToString(); }
                txtPrecioNeto.Text = "";
                changeimporte = false;
                switch (pro.CodControlStock)
                {
                    case 1: txtControlStock.Enabled = false; txtCantidad.Enabled = true; break;
                    case 2: txtControlStock.Enabled = true; txtCantidad.Enabled = true; break;
                    case 3: txtControlStock.Enabled = true; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                    case 4: txtControlStock.Enabled = false; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                }
            }
        }

        private void txtDscto1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "")
                    {
                        txtDscto1.Text = "0.00";
                    }
                    txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text));
                    //txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)));
                    changeimporte = false;
                }
                ProcessTabKey(true);
            }
        }

        private void txtDscto1_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "")
            {
                if (txtDscto1.Text == "")
                {
                        txtDscto1.Text = "0.00";
                }
                else
                {
                   if (Convert.ToDecimal(txtDscto1.Text) > Convert.ToDecimal(txtDescMax.Text))
                   {
                       MessageBox.Show("Descuento No Permitido, Verifique Dato!!!", "Advertencia",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                       txtDscto1.Focus();
                   }
                   else
                   {
                       if (pro.CodProducto!= 1514)
                       {
                           if (manipulado.Name != txtPrecio.Name)
                           {
                               if (Moneda == 2)
                               {
                                   txtPrecio.Text = String.Format("{0:#,##0.00}",
                                       Convert.ToDecimal(/*pro.PrecioVenta)*/ (Convert.ToDecimal(txtPrecio.Text)) - (Convert.ToDecimal(/*pro.PrecioVenta*/txtPrecio.Text) * Convert.ToDecimal(txtDscto1.Text) / 100)));
                               }
                               else if (Moneda == 1)
                               {
                                   txtPrecio.Text = String.Format("{0:#,##0.00}",
                                       Convert.ToDecimal(/*pro.PrecioVentaSoles*/txtPrecio.Text/*pro.PrecioVenta * tc*/) - (Convert.ToDecimal(/*pro.PrecioVentaSoles*/txtPrecio.Text/*pro.PrecioVenta*tc*/) * Convert.ToDecimal(txtDscto1.Text) / 100));
                               }
                           }

                       }
                   }

                   //txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)));
                   txtPrecioNeto.Text = String.Format("{0:#,##0.00}",
                            Convert.ToDouble(txtPrecio.Text)*Convert.ToDouble(txtCantidad.Text));
                   changeimporte = false;
                   }
                }
            
        }

        private void txtPrecioNeto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            //if (txtPrecioNeto.Text != "")
            //{
            //    if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
            //    if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
            //    if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
            //    if (txtCantidad.Text != "")
            //    {
            //        txtPrecio.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecioNeto.Text) / (1 - (Convert.ToDouble(txtDscto3.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) / Convert.ToDouble(txtCantidad.Text));
            //    }
            //    ProcessTabKey(true);
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
                    if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                    if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                    if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
                    if (txtCantidad.Text != "")
                    {
                        //txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                        txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text));
                    }
                    ProcessTabKey(true);
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
                    ProcessTabKey(true);
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    txtCantidad.Focus();
                    //}
                }

            }
        }


        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            if (Procede == 4 || Procede == 2)
            {
                if (Procede == 2)
                {
                    if (txtCantidad.Text != "")
                    {
                        if (Convert.ToDecimal(txtCantidad.Text) <= Convert.ToDecimal(txtStock.Text))
                        {
                            if (Convert.ToDecimal(txtCantidad.Text) == 0)
                            {
                                txtCantidad.Focus();
                            }

                            if (txtPrecio.Text.Trim() != "")
                            {
                                if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                                if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                                if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }

                                txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                                //txtPrecioNeto.Text = Convert.ToString(Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text));
                                changeimporte = false;
                            }
                        }
                        else
                        {
                            txtCantidad.Focus();
                            MessageBox.Show("La cantidad debe ser menor al stock del producto!");
                        }
                    }
                    else
                    {
                        txtCantidad.Focus();
                    }
                }
                else
                {
                    if (txtCantidad.Text != "")
                    {
                        if (Convert.ToDecimal(txtCantidad.Text) == 0)
                        {
                            txtCantidad.Focus();
                        }

                        if (txtPrecio.Text.Trim() != "")
                        {
                            if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                            if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                            if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }

                            txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                            //txtPrecioNeto.Text = Convert.ToString(Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text));
                            changeimporte = false;
                        }
                    }
                    else
                    {
                        txtCantidad.Focus();
                    }
                }
                //if (txtCantidad.Text != "")
                //{
                //    if (Convert.ToDecimal(txtCantidad.Text) == 0)
                //    {
                //        txtCantidad.Focus();
                //    }

                //    if (txtPrecio.Text.Trim() != "")
                //    {
                //        if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                //        if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                //        if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }

                //        txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                //        //txtPrecioNeto.Text = Convert.ToString(Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text));
                //        changeimporte = false;
                //    }
                //}
                //else
                //{
                //    txtCantidad.Focus();
                //}
            }
            else if (Procede != 4)
            {
                if (txtCantidad.Text == "")
                {
                    txtCantidad.Focus();
                }
                else
                {
                    if (Convert.ToDouble(txtCantidad.Text) <= Convert.ToDouble(txtStock.Text))
                    {

                        if (txtPrecio.Text != "")
                        {
                            if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                            if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                            if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
                            txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                            changeimporte = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCantidad.Focus();
                        txtDscto1.Text = ""; txtDscto2.Text = ""; txtDscto3.Text = ""; txtPrecioNeto.Text = "";
                    }
                }
            }
        }

       

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Decimal bruto, montodescuento, valorventa, igv, precioventa, precioreal, valorreal, factorigv, maxPorcDescto;
            Decimal sumdet = 0;
            if (Procede == 1)//Cuando abre desde nota de salida
            {

                frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
                bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
                //montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
                if (Convert.ToDecimal(txtDscto1.Text) > 0) montodescuento = puInicio - Convert.ToDecimal(txtPrecio.Text);
                else montodescuento = bruto - Convert.ToDecimal(txtPrecioNeto.Text);
                if (pro.ConIgv)
                {
                    //DEBE TOMAR EL DATO DE IGV DE LA CONFIGURACION DEL SISTEMA   
                    precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                    valorventa = precioventa / factorigv;
                }
                else
                {
                    valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    precioventa = valorventa;
                }
                precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
                valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
                igv = precioventa - valorventa;
                maxPorcDescto = Convert.ToDecimal(txtDescMax.Text);

                if (form.dgvDetalle.Rows.Count < 10) // se carga el numero de items que soporta el tamaño del formato
                {
                    if (Proceso == 1)
                    {
                        form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, 
                            txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), "", Convert.ToDouble(txtPrecio.Text), 
                            bruto, Convert.ToDouble(txtDscto1.Text), "", "",  montodescuento, valorventa, igv, precioventa, precioreal,
                            valorreal, maxPorcDescto);
                        limpiarformulario();
                        if (Seleccion == 2)
                        {
                            this.Close();
                        }
                    }
                    else if (Proceso == 2)
                    {
                        form.dgvDetalle.CurrentRow.SetValues("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                             txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), "", Convert.ToDouble(txtPrecio.Text), 
                             bruto, Convert.ToDouble(txtDscto1.Text), "", "", montodescuento, valorventa, igv, precioventa, precioreal,
                             valorreal, maxPorcDescto);
                        limpiarformulario();
                        
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Procede == 2)//Cuando de abre desde Venta
            {
                frmVenta form = (frmVenta)Application.OpenForms["frmVenta"];
                form.btnEditar.Enabled = true;
                form.btnEliminar.Enabled = true;
                bruto = Convert.ToDecimal(txtCantidad.Text) * puInicio;
                //bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                montodescuento = bruto - (bruto * (1 - (Convert.ToDecimal(txtDscto1.Text) / 100)));
                //montodescuento = Convert.ToDouble(txtDscto1.Text);
                // montodescuento = puInicio - Convert.ToDouble(txtPrecio.Text);
                if (pro.ConIgv)
                {
                    //DEBE TOMAR EL DATO DE IGV DE LA CONFIGURACION DEL SISTEMA   
                    precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                    valorventa = precioventa / factorigv;
                }
                else
                {
                    valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    precioventa = valorventa;
                }
                precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
                valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
                igv = precioventa - valorventa;
                maxPorcDescto = Convert.ToDecimal(txtDescMax.Text);
                if (form.dgvDetalle.Rows.Count < 10) // se carga el numero de items que soporta el tamaño del formato
                {
                    if (Proceso == 1)
                    {
                        form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion,
                            cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text,
                            Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(/*txtPrecio.Text*/puInicio), bruto,
                            Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento,
                            valorventa, igv, precioventa, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
                        form.calculatotales();
                        limpiarformulario();
                        if (Seleccion == 2)
                        {
                            this.Close();
                        }

                    }
                    else if (Proceso == 2)
                    {
                        form.dgvDetalle.CurrentRow.SetValues("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                            txtUnidad.Text, txtControlStock.Text, Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(/*txtPrecio.Text*/puInicio), bruto,
                            Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento,
                            valorventa, igv, precioventa, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
                        form.calculatotales();
                        limpiarformulario();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (Procede == 3)//Cuando de abre desde PedidoVenta
            {
                frmPedido form = (frmPedido)Application.OpenForms["frmPedido"];
                bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
                montodescuento = bruto - Convert.ToDecimal(txtPrecioNeto.Text);
                if (pro.ConIgv)
                {
                    //DEBE TOMAR EL DATO DE IGV DE LA CONFIGURACION DEL SISTEMA   
                    precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                    valorventa = precioventa / factorigv;
                }
                else
                {
                    valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    precioventa = valorventa;
                }
                precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
                valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
                igv = precioventa - valorventa;
                
                if (Proceso == 1)
                {
                    form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                        txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                        Convert.ToDouble(txtDscto1.Text), Convert.ToDouble(txtDscto2.Text), Convert.ToDouble(txtDscto3.Text), montodescuento,
                        valorventa, igv, precioventa, precioreal, valorreal);
                    limpiarformulario();
                    if (Seleccion == 2)
                    {
                        this.Close();
                    }
                }
                else if (Proceso == 2)
                {
                    form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                        txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto,
                        Convert.ToDouble(txtDscto1.Text), Convert.ToDouble(txtDscto2.Text), Convert.ToDouble(txtDscto3.Text), montodescuento,
                        valorventa, igv, precioventa, precioreal, valorreal);
                    limpiarformulario();
                    this.Close();
                }
            }
            else if (Procede == 4)//Cuando de abre desde Cotizacion
            {
                frmGestionCotizacion form = (frmGestionCotizacion)Application.OpenForms["frmGestionCotizacion"];
                form.btnEditar.Enabled = true;
                form.btnEliminar.Enabled = true;
                //bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
                bruto = Convert.ToDecimal(txtCantidad.Text) * puInicio;
                //montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
                // montodescuento = puInicio - Convert.ToDouble(txtPrecio.Text);
                montodescuento = bruto - (bruto * (1 - (Convert.ToDecimal(txtDscto1.Text) / 100)));

                if (pro.ConIgv)
                {
                    //DEBE TOMAR EL DATO DE IGV DE LA CONFIGURACION DEL SISTEMA   
                    precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100 + 1);
                    valorventa = precioventa / factorigv;
                }
                else
                {
                    valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
                    precioventa = valorventa;
                }
                precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
                valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
                igv = precioventa - valorventa;
                maxPorcDescto = Convert.ToDecimal(txtDescMax.Text);

                String descripcionPro = "";
                if (pro.CodProducto == 1514)
                {
                    descripcionPro = txtDescripcion.Text;
                }
                else descripcionPro = pro.Descripcion;

                if (Proceso == 1)
                {
                    form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, descripcionPro,
                            cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text,
                            Convert.ToDecimal(txtCantidad.Text), puInicio/* Convert.ToDouble(txtPrecio.Text)*/, bruto,
                            Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento,
                            valorventa, igv, precioventa, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
                    limpiarformulario();
                    if (Seleccion == 2)
                    {
                        this.Close();
                    }
                }
                else if (Proceso == 2)
                {
                    form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue,
                        txtUnidad.Text, txtControlStock.Text, Convert.ToDecimal(txtCantidad.Text), puInicio/*Convert.ToDouble(txtPrecio.Text)*/, bruto,
                        Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento,
                        valorventa, igv, precioventa, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
                    limpiarformulario();
                    form.actualizaimportes();
                    this.Close();
                }
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

        private void txtDscto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                    if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                    if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
                    txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                    changeimporte = false;
                }
                ProcessTabKey(true);
            }
        }

        private void txtDscto2_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "")
            {
                if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
                txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                changeimporte = false;
            }
        }

        private void txtDscto3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.SOLONumeros(sender, e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtPrecio.Text != "")
                {
                    if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                    if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                    if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
                    txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                    changeimporte = false;
                }
                ProcessTabKey(true);
            }
        }

        private void txtDscto3_Leave(object sender, EventArgs e)
        {
            if (txtPrecio.Text != "")
            {
                if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
                txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) * (1 - (Convert.ToDouble(txtDscto3.Text) / 100)));
                changeimporte = false;
            }
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
            if (Seleccion == 2)
            {
                txtCantidad.Focus();
            }
            else
            {
                if (Proceso == 1)
                {
                    txtReferencia.Focus();
                }
                else if (Proceso == 2)
                {
                    txtReferencia.ReadOnly = true;
                    txtCantidad.Focus();
                }
            }
            changeimporte = false;
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
            Double pventa = 0;
            if (txtPrecio.Text != "" && Convert.ToDecimal(txtPrecio.Text) != 0)
            {
                if (txtDscto1.Text == "")
                {
                    txtDscto1.Text = "0.00";
                }
                if (txtCantidad.Text != "" && Convert.ToDecimal(txtCantidad.Text) != 0)
                {
                    txtPrecioNeto.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text));
                    changeimporte = false;
                }
            }
            if (pro != null && txtCantidad.Text != "" && txtPrecio.Text != "" && Convert.ToDecimal(txtCantidad.Text) != 0 && Convert.ToDecimal(txtPrecio.Text) != 0)
            {
                if (Convert.ToDecimal(txtPrecio.Text) < Convert.ToDecimal(txtPrecioDscto.Text))
                {
                    MessageBox.Show("El Descuento del Producto No es Valido!!!", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPrecio.Focus();
                }
                else
                {
                    if (manipulado != txtDscto1)
                    {
                        if (Moneda == 1) { pventa = pro.PrecioVenta * tc; } else { pventa = pro.PrecioVenta; }
                        if (Convert.ToDecimal(txtPrecio.Text) > Convert.ToDecimal(pventa))
                        {
                            txtDscto1.Text = "0.00";
                        }
                        //if (txtPrecioDscto.Text == "") {
                        //    txtPrecioDscto.Text = (Convert.ToDecimal(txtPrecio.Text) + (Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtDescMax.Text) / 100)).ToString();
                        //}
                        else if (Convert.ToDecimal(txtPrecio.Text) >= Convert.ToDecimal(txtPrecioDscto.Text))
                        {
                            //    txtDscto1.Text = "0.00";
                            //}
                            //else
                            //{
                            if (pventa != 0)
                            {
                                txtDscto1.Text = String.Format("{0:#,##0.00}", 100 - (Convert.ToDecimal(txtPrecio.Text) * 100 / Convert.ToDecimal(pventa)));
                            }
                            else
                            {
                                txtDscto1.Text = "0.00";
                            }
                        }
                    }
                    btnGuardar.Enabled = true;
                }
            }
        }
        
        private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmProductosLista frm = new frmProductosLista();
                frm.Proceso = Proceso;
                frm.Procede = Procede;
                frm.CodLista = Codlista;
                frm.tc = tc;
                frm.Moneda = Moneda;
                frm.productosfactura = productoscargados;
                frm.productoscotizacion = productoscotizados;
                frm.ShowDialog();
            }
        }

        private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtReferencia.Text != "")
                {
                    if (Procede == 4)
                    {
                        pro = AdmPro.CargaProductoDetalleR(txtReferencia.Text, frmLogin.iCodAlmacen, 2, Codlista);
                        Int32 valor=0;
                        foreach (clsDetalleCotizacion deta in productoscotizados)
                        {
                            if (deta.Referencia != pro.Referencia) { valor = 1; }
                        }
                        listaprecio = AdmLista.CargaListaPrecio(Codlista);
                        if (pro != null && valor==1)
                        {
                            CodProducto = pro.CodProducto;
                            txtReferencia.Text = pro.Referencia;
                            txtDescripcion.Text = pro.Descripcion;
                            if (txtCodigo.Text=="1514")
                            {
                                txtDescripcion.ReadOnly = false;
                                txtDescripcion.Enabled = true;
                                txtPrecio.ReadOnly = false;
                                //txtDescripcion.Focus();
                            }
                            txtUnidad.Text = pro.UnidadDescrip;
                            CargaUnidades(cmbUnidad);
                            cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                            txtStock.Text = pro.StockDisponible.ToString();
                            txtControlStock.Text = "";
                            txtCantidad.Text = "";
                            if (pro.PrecioVariable) { txtPrecio.ReadOnly = false; txtPrecioNeto.ReadOnly = false; } else { /*txtPrecio.ReadOnly = true;*/ txtPrecioNeto.ReadOnly = true; }
                            if (Moneda == 2) { txtPrecio.Text = pro.PrecioVenta.ToString(); } else if (Moneda == 1) { txtPrecio.Text = pro.PrecioVentaSoles.ToString()/*Math.Round((pro.PrecioVenta * tc), listaprecio.Decimales).ToString();*/ ; }
                            if (pro.Oferta) { txtDscto1.Text = pro.PDescuento.ToString(); txtDscto1.ReadOnly = true; } else { txtDscto1.Text = ""; }
                            txtDscto2.Text = "";
                            txtDscto3.Text = "";
                            txtPrecioNeto.Text = "";
                            changeimporte = false;

                            //*********************************
                            if (Moneda == 2) { txtPrecioDscto.Text = Math.Round((Convert.ToDecimal(pro.PrecioVenta) - (Convert.ToDecimal(pro.PrecioVenta) * (pro.MaxPorcDesc / 100))), listaprecio.Decimales).ToString(); } 
                            else if (Moneda == 1) { txtPrecioDscto.Text = Math.Round((Convert.ToDecimal(pro.PrecioVentaSoles) - (Convert.ToDecimal(pro.PrecioVentaSoles) * (pro.MaxPorcDesc / 100))), listaprecio.Decimales).ToString(); }
                            txtDescMax.Text = pro.MaxPorcDesc.ToString();
                            txtPrecio.Enabled = true;
                            //*********************************
                            switch (pro.CodControlStock)
                            {
                                case 1: txtControlStock.Enabled = false; txtCantidad.Enabled = true; break;
                                case 2: txtControlStock.Enabled = true; txtCantidad.Enabled = true; break;
                                case 3: txtControlStock.Enabled = true; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                                case 4: txtControlStock.Enabled = false; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
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
                            txtPrecio.Text = "";
                            txtPrecioDscto.Text = "";
                            txtDscto1.Text = "";
                            txtDscto2.Text = "";
                            txtDscto3.Text = "";
                            txtPrecioNeto.Text = "";
                            txtPrecioNetoDscto.Text = "";
                        }
                    }
                    else {
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
        }

         

        public Boolean BuscaProducto()
        {
            pro = AdmPro.CargaProductoDetalleR(txtReferencia.Text, frmLogin.iCodAlmacen, 2, Codlista);
            listaprecio = AdmLista.CargaListaPrecio(Codlista);
            if (pro != null)
            {
                CodProducto = pro.CodProducto;
                txtReferencia.Text = pro.Referencia;
                txtDescripcion.Text = pro.Descripcion;
                 if (txtCodigo.Text=="1514")
                 {
                    txtDescripcion.ReadOnly = false;
                    txtDescripcion.Enabled = true;
                    txtPrecio.ReadOnly = false;
                    //txtDescripcion.Focus();
                }
                txtUnidad.Text = pro.UnidadDescrip;
                CargaUnidades(cmbUnidad);
                cmbUnidad.SelectedValue = pro.CodUnidadMedida;
                txtStock.Text = pro.StockDisponible.ToString();
                txtControlStock.Text = "";
                txtCantidad.Text = "";
                if (pro.PrecioVariable) { txtPrecio.ReadOnly = false; txtPrecioNeto.ReadOnly = false; } else { /*txtPrecio.ReadOnly = true;*/ txtPrecioNeto.ReadOnly = true; }
                if (Moneda == 2) { txtPrecio.Text = pro.PrecioVenta.ToString(); } else if (Moneda == 1) { txtPrecio.Text = pro.PrecioVentaSoles.ToString();/*Math.Round((pro.PrecioVenta * tc), listaprecio.Decimales).ToString(); ;*/ }
                if (pro.Oferta) { txtDscto1.Text = pro.PDescuento.ToString(); txtDscto1.ReadOnly = true; } else { txtDscto1.Text = ""; }
                txtDscto2.Text = "";
                txtDscto3.Text = "";
                txtPrecioNeto.Text = "";
                changeimporte = false;

                //*********************************
                if (Moneda == 2) { txtPrecioDscto.Text = Math.Round((Convert.ToDecimal(pro.PrecioVenta) - (Convert.ToDecimal(pro.PrecioVenta) * (pro.MaxPorcDesc / 100))), listaprecio.Decimales).ToString(); } 
                else if (Moneda == 1) { txtPrecioDscto.Text = Math.Round((Convert.ToDecimal(pro.PrecioVentaSoles) - (Convert.ToDecimal(pro.PrecioVentaSoles) * (pro.MaxPorcDesc / 100))),listaprecio.Decimales).ToString(); }
                txtDescMax.Text = pro.MaxPorcDesc.ToString();
                txtPrecio.Enabled = true;
                //*********************************
                switch (pro.CodControlStock)
                {
                    case 1: txtControlStock.Enabled = false; txtCantidad.Enabled = true; break;
                    case 2: txtControlStock.Enabled = true; txtCantidad.Enabled = true; break;
                    case 3: txtControlStock.Enabled = true; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                    case 4: txtControlStock.Enabled = false; txtCantidad.Enabled = false; txtCantidad.Text = "01"; break;
                }
                return true;
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
                txtPrecio.Text = "";
                txtPrecioDscto.Text = "";
                txtDscto1.Text = "";
                txtDscto2.Text = "";
                txtDscto3.Text = "";
                txtPrecioNeto.Text = "";
                txtPrecioNetoDscto.Text = "";
                return false;
            }
        }

        private void frmDetalleSalida_Load(object sender, EventArgs e)
        {            
            if (Procede == 2)
            { 
            
            }
            else if (Procede==4)
            {
            
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

        private void txtReferencia_Leave(object sender, EventArgs e)
        {
            if (txtReferencia.Text != "" && txtReferencia.ReadOnly == false)
            {
                if (Proceso == 1)
                {
                    if (BuscaProducto())
                    {
                        if( txtCodigo.Text=="1514") txtDescripcion.Focus();
                        else  txtCantidad.Focus();
                    }
                    else
                    {
                        MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void txtPrecioNeto_KeyUp(object sender, KeyEventArgs e)
        {
            ////ok.SOLONumeros(sender, e);
            //if (txtPrecioNeto.Text != "")
            //{
            //    if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
            //    if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
            //    if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
            //    if (txtCantidad.Text != "")
            //    {
            //        txtPrecio.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecioNeto.Text) / (1 - (Convert.ToDouble(txtDscto3.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) / Convert.ToDouble(txtCantidad.Text));
            //    }
            //    //ProcessTabKey(true);
            //}
        }

        private void txtPrecioNeto_TextChanged(object sender, EventArgs e)
        {
            changeimporte = true;
        }

        private void txtPrecioNeto_Leave(object sender, EventArgs e)
        {
            //ok.SOLONumeros(sender, e);
            if (txtPrecioNeto.Text != "" && changeimporte)
            {
                if (txtDscto1.Text == "") { txtDscto1.Text = "0.00"; }
                if (txtDscto2.Text == "") { txtDscto2.Text = "0.00"; }
                if (txtDscto3.Text == "") { txtDscto3.Text = "0.00"; }
                if (txtCantidad.Text != "")
                {
                    txtPrecio.Text = String.Format("{0:#,##0.00}", Convert.ToDouble(txtPrecioNeto.Text) / (1 - (Convert.ToDouble(txtDscto3.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto2.Text) / 100)) / (1 - (Convert.ToDouble(txtDscto1.Text) / 100)) / Convert.ToDouble(txtCantidad.Text));
                    changeimporte = false;
                }
                //ProcessTabKey(true);
            }
        }

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
            {
                manipulado = (TextBox)sender;
            }
        }

        private void txtDscto1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
            {
                manipulado = (TextBox)sender;
            }
        }




    }
}
