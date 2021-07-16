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
using SIGEFA.Reportes;

namespace SIGEFA.Formularios
{
    public partial class frmProductosLista : DevComponents.DotNetBar.OfficeForm
    {

        //RAGA
        clsAdmAlmacen dalm = new clsAdmAlmacen();
        clsAlmacen alm;


        clsAdmProducto AdmPro = new clsAdmProducto();
        clsAdmTipoArticulo AdmTip = new clsAdmTipoArticulo();
        clsProducto pro = new clsProducto();
        public Int32 CodLista = 0, codproveedor=0;
        public Boolean bvalorventa = false;
        public Int32 Proceso = 0; //(1) Ingreso (2)Salida (3)Relacion (4)Guia
        public Int32 Procede = 0; //(1)Nota de Salida (2)Venta       
        public Int32 Moneda = 0;
        public Double tc = 0;
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        public List<Int32> seleccion = new List<Int32>();
        public List<clsDetalleNotaIngreso> productoscargados = new List<clsDetalleNotaIngreso>();// relacion de los productos que ya han sido cargado en la nota de ingreso
        public List<clsDetalleFacturaVenta> productosfactura = new List<clsDetalleFacturaVenta>();// relacion de los productos seleccionados para la venta
        public List<clsDetalleCotizacion> productoscotizacion = new List<clsDetalleCotizacion>();// relacion de los productos seleccionados para la cotizacion

        public Int32 codalmacen = 0;

        public Int32 codigoPro;
        public String referenciaPro, descripcionPro;
        public Boolean liqcompra;


        public frmProductosLista()
        {
            InitializeComponent();
        }

        public int GetCodigoProducto()
        {
            return pro.CodProducto = Convert.ToInt32(dgvProductos.CurrentRow.Cells[codigo.Name].Value); ;
        }

        private void frmProductosLista_Load(object sender, EventArgs e)
        {


            CargaTipoArticulos();

            //RAGA
            alm = dalm.CargaAlmacen(frmLogin.iCodAlmacen);
            cbTipoArticulo.SelectedIndex = 0;


            if (alm.ITipoAlm == 0)//SUMINISTROS DIVERSOS
                cbTipoArticulo.SelectedValue = 7;
            if (alm.ITipoAlm == 1)//MP
            {
                cbTipoArticulo.SelectedValue = 5;
                preciosoles.HeaderText = "Proveedor";
                preciodolares.HeaderText = "PrecioUnitario";
                preciooferta.HeaderText = "Zona";
                precioventa.HeaderText = "Fecha-Ingreso";
                CodLista = 1;
                if(Procede == 12)//IngresoCamara
                    stockdisponible.Visible = false;
                else
                    stockdisponible.Visible = true;
            }


            if (alm.ITipoAlm == 2)//INSUMOS
                cbTipoArticulo.SelectedValue = 6;

            if (alm.ITipoAlm == 3)//PT
            {
                cbTipoArticulo.SelectedValue = 8;
                stockdisponible.Visible = true;
                preciooferta.Visible = true;

                stockdisponible.HeaderText = "Nro Unidades";
                preciooferta.HeaderText = "Peso Disponible(K.g)";

            }
            




            //cbTipoArticulo.SelectedIndex = 0;
            CargaLista(Procede);
            label2.Text = "Descripcion";
            label3.Text = "descripcion";
        }

        private void dgvProductos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            try
            {
                if (dgvProductos.Rows.Count >= 1 && e.Row.Selected && e.Row != null)
                {
                    pro.CodProducto = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                    pro.Referencia = e.Row.Cells[referencia.Name].Value.ToString();
                    pro.Descripcion = e.Row.Cells[descripcion.Name].Value.ToString();
                    pro.StockDisponible = Convert.ToDouble(e.Row.Cells[stockdisponible.Name].Value.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargaLista(Int32 proce)
        {
            if (proce == 6 || proce == 8 || proce == 10)//6) nota de ingreso por compra rapida, 8)orden compra 10)Nota de ingreso por orden compra
            {
                dgvProductos.DataSource = data;
                //data.DataSource = AdmPro.RelacionIngresoPorProveedor(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen,codproveedor);
                data.DataSource = AdmPro.RelacionIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
                DepurarLista();
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();
                stockdisponible.Visible = false;
                preciooferta.Visible = false;
                precioventa.Visible = false;
                preciodolares.Visible = false;
                preciosoles.Visible = false;
            }
            else if (proce == 7)// 7)nota de credito
            {
                dgvProductos.DataSource = data;
                data.DataSource = AdmPro.RelacionIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue),frmLogin.iCodAlmacen);
                DepurarLista();
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();
                stockdisponible.Visible = false;
                preciooferta.Visible = false;
                precioventa.Visible = false;
            }
            else if (proce == 4 )//Cotizacion
            {
                cbTipoArticulo.SelectedValue = 1;//selecciona MERCADERIAS
                dgvProductos.DataSource = data;
                data.DataSource = AdmPro.RelacionCotizacion(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen,CodLista);
                DepurarLista3();
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();
                //stockdisponible.Visible = false;
                preciooferta.Visible = false;
                precioventa.Visible = false;
            }
            else if (proce == 1 || proce == 2 || proce == 3 /*|| proce == 4*/ || proce == 5 || proce == 9 || proce == 17)// 1)Nota de salida, 2) Venta, 3)Pedido Venta, 4)Cotizacion,5)Guia
            {
                //cbTipoArticulo.SelectedValue = 1;//selecciona MERCADERIAS
                dgvProductos.DataSource = data;
                if (codalmacen != 0)
                {
                    data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), codalmacen, CodLista);
                }
                //data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen, CodLista);
                else
                {
                    data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen, CodLista);
                }
                DepurarLista2();
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();
                preciooferta.Visible = false;
                precioventa.Visible = false;
            }
            else if (Procede == 11 || Procede == 12 || Procede == 13)// 11)Requerimiento 12) OrdenCompra
            {
                dgvProductos.DataSource = data;
                data.DataSource = AdmPro.RelacionIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();
                precioventa.Visible = false;
                preciooferta.Visible = false;
                stockdisponible.Visible = false;
                preciodolares.Visible = false;
                preciosoles.Visible = false;
            }



            else if (Procede == 14 || Procede == 15 || Procede == 19 || Procede == 22)// 14)Prod Terminado  15) Stock Prod Term
            {
                dgvProductos.DataSource = data;
                data.DataSource = AdmPro.RelacionProdTermIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();
                precioventa.Visible = false;
                preciooferta.Visible = false;
                stockdisponible.Visible = false;
                preciodolares.Visible = false;
                preciosoles.Visible = false;
            }

            else if (Procede == 16 || Procede == 18)// 16) Materia Prima Produccion
            {
                dgvProductos.DataSource = data;
                data.DataSource = AdmPro.RelacionMateriaPrima(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();
                precioventa.Visible = false;
                preciooferta.Visible = false;
                stockdisponible.Visible = false;
                preciodolares.Visible = true;
                preciosoles.Visible = false;
            }

            else if (Procede == 20 || Procede == 21)// frmRptCentroCostos
            {

                dgvProductos.DataSource = data;
                if (alm.ITipoAlm == 1)//MP
                    data.DataSource = AdmPro.ListaProductosxArticulo(5);

                /*
                 if (alm.ITipoAlm == 0 || alm.ITipoAlm == 2)//Sum Diversos e Insumos
                    data.DataSource = AdmPro.RelacionIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
                */
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvProductos.ClearSelection();

                stockdisponible.Visible = false;
                precioventa.Visible = false;
                preciooferta.Visible = false;
                stockdisponible.Visible = false;
                preciodolares.Visible = false;
                preciosoles.Visible = false;
            }















        }

        private void DepurarLista()
        {       
            foreach (clsDetalleNotaIngreso deta in productoscargados)
            {
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta.CodProducto)
                    {
                        dgvProductos.Rows.Remove(row);
                    }
                }
            }
        }

        private void DepurarLista2()
        {
            foreach (clsDetalleFacturaVenta deta in productosfactura)
            {
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta.CodProducto)
                    {
                        dgvProductos.Rows.Remove(row);
                    }
                }
            }
        }

        private void DepurarLista3()
        {
            foreach (clsDetalleCotizacion deta in productoscotizacion)
            {
                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta.CodProducto)
                    {
                        dgvProductos.Rows.Remove(row);
                    }
                }
            }
        }

        private void CargaTipoArticulos()
        {
            try
            {
                cbTipoArticulo.DataSource = AdmTip.MuestraTipoArticulos();
                cbTipoArticulo.DisplayMember = "descripcion";
                cbTipoArticulo.ValueMember = "codTipoArticulo";
                cbTipoArticulo.SelectedIndex = 3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void frmProductosLista_Shown(object sender, EventArgs e)
        {
            //CargaLista(Procede);
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text.Length >= 2)
                {
                    data.Filter = String.Format("[{0}] like '*{1}*'", label3.Text.Trim(), txtFiltro.Text.Trim());
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

        private void dgvProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dgvProductos.Columns[e.ColumnIndex].HeaderText;
            label3.Text = dgvProductos.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            recorrelista();
            if (Procede == 6 || Procede == 7 || Procede == 8 || Procede == 9 || Procede == 10  ||    Procede == 11)
            {
                foreach (int cod in seleccion)
                {
                    
                    if (Application.OpenForms["frmDetalleIngreso"] != null)
                    {
                        Application.OpenForms["frmDetalleIngreso"].Close();
                    }
                    frmDetalleIngreso form = new frmDetalleIngreso();
                    form.txtStock.Text = pro.StockDisponible.ToString();
                    form.Proceso = Proceso;
                    form.Seleccion = 2;
                    form.Procede = Procede;
                    form.Codlista = CodLista;
                    form.bvalorventa = bvalorventa;
                    form.txtCodigo.Text = cod.ToString();
                    if (form.repetido == 1) { form.Close(); this.Close(); }
                    else
                    {
                        form.txtCantidad.Focus();
                        form.ShowDialog();
                    }
                    form.txtStock.Text = pro.StockDisponible.ToString();
                }
            }

            else if (Procede == 1 || Procede == 2 || Procede == 3 || Procede == 4)
            {
                foreach (int cod in seleccion)
                {
                    /*
                    if (Application.OpenForms["frmDetalleSalida"] != null)
                    {
                        Application.OpenForms["frmDetalleSalida"].Close();
                    }*/
                    frmDetalleSalida form = new frmDetalleSalida();
                    form.Seleccion = 2;
                    form.Proceso = Proceso;
                    form.Codlista = CodLista;
                    form.Procede = Procede;
                    form.Moneda = Moneda;
                    form.tc = tc;
                    form.txtCodigo.Text = cod.ToString();
                    form.txtPrecio.ReadOnly = true;
                    form.ShowDialog();
                }
            }
            else if (Procede == 5  || Procede == 12 || Procede == 13 || Procede == 14 || Procede == 9 || Procede == 10 || Procede == 16 || Procede == 17)
            {
                foreach (int cod in seleccion)
                {


                    if (Application.OpenForms["frmDetalleGuia"] != null)
                    {
                        Application.OpenForms["frmDetalleGuia"].Close();
                    }

                    frmDetalleGuia form = new frmDetalleGuia();





                    if (Procede == 16)
                        form.codIngreso = pro.CodProducto;


                    form.txtStock.Text = pro.StockDisponible.ToString();
                    form.txtCantidad.Focus();
                    form.Seleccion = 2;
                    form.Proceso = Proceso;
                    form.Procede = Procede;
                    form.Codlista = CodLista;
                    if (Procede == 10) form.chBonificacion.Visible = true;
                    form.txtCodigo.Text = cod.ToString();
                    if (form.repetido == 1) { form.Close(); this.Close(); }
                    else
                    {
                        form.txtCantidad.Focus();
                        form.ShowDialog();
                    }
               //     form.txtStock.Text = pro.StockDisponible.ToString();

                }
            }
            else if (Procede == 13)
            {
                codigoPro = pro.CodProducto;
                referenciaPro = pro.Referencia;
                descripcionPro = pro.Descripcion;

            }
            else if (Procede == 14)
            {
                codigoPro = pro.CodProducto;
                referenciaPro = pro.Referencia;
                descripcionPro = pro.Descripcion;

            }
            else if (Procede == 15)//Stock Producto Term
            {

                frmStockProducto form = (frmStockProducto)Application.OpenForms["frmStockProducto"];
                form.txtReferencia.Text = pro.Referencia;
                form.txtNombre.Text = pro.Descripcion;
                form.codProducto = pro.CodProducto;

            }

            else if (Procede == 18 || Procede == 19)//frmRegistroProducto
            {

                frmRegistroProducto form = (frmRegistroProducto)Application.OpenForms["frmRegistroProducto"];
                form.textBox3.Text = pro.Referencia;
                form.textBox2.Text = pro.Descripcion;
                form.codProductoReferencia = pro.CodProducto;
            }


            else if (Procede == 20)//frmRptCentroCostos
            {
                frmRptCentroCostos form = (frmRptCentroCostos)Application.OpenForms["frmRptCentroCostos"];
                form.textBox2.Text = pro.Referencia;//Desde
            }
            else if (Procede == 21)//frmRptCentroCostos
            {
                frmRptCentroCostos form = (frmRptCentroCostos)Application.OpenForms["frmRptCentroCostos"];
                form.textBox3.Text = pro.Referencia;//Hasta
            }
            else if (Procede == 22)//frmSalidaArticulo
            {
                frmSalidaArticulo form = (frmSalidaArticulo)Application.OpenForms["frmSalidaArticulo"];
                form.txtCodigo.Text = pro.CodProducto.ToString();
                form.txtStock.Text = pro.StockDisponible.ToString();
            }
            this.Close();


        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {             
            recorrelista();
            if (Procede == 6 || Procede == 7 || Procede == 8 || Procede == 9 ||  Procede == 10 ||  Procede == 11)
            {
                foreach (int cod in seleccion)
                {

                    if (Application.OpenForms["frmDetalleIngreso"] != null)
                    {
                        Application.OpenForms["frmDetalleIngreso"].Close();
                    }
                    frmDetalleIngreso form = new frmDetalleIngreso();

                    form.txtStock.Text = pro.StockDisponible.ToString();
                    form.Proceso = Proceso;
                    form.Seleccion = 2;
                    form.Procede = Procede;
                    form.Codlista = CodLista;
                    form.bvalorventa = bvalorventa;
                    form.txtCodigo.Text = cod.ToString();
                    if (form.repetido == 1) { form.Close(); this.Close(); }
                    else
                    {
                        form.txtCantidad.Focus();
                        form.ShowDialog();
                    }
                 // form.txtStock.Text = pro.StockDisponible.ToString();
                }
            }
            
            else if (Procede == 1 || Procede == 2 || Procede == 3 || Procede == 4 )
            {
                foreach (int cod in seleccion)
                {
                    /*
                    if (Application.OpenForms["frmDetalleSalida"] != null)
                    {
                        Application.OpenForms["frmDetalleSalida"].Close();
                    }
                    */
              
                    frmDetalleSalida form = (frmDetalleSalida)Application.OpenForms["frmDetalleSalida"];
                    form.Seleccion = 2;
                    form.Proceso = Proceso;
                    form.Codlista = CodLista;
                    form.Procede = Procede;
                    form.Moneda = Moneda;
                    form.tc = tc;
                    form.txtCodigo.Text = cod.ToString();
                    form.txtPrecio.ReadOnly = true;
                    form.ShowDialog();
                }
            }
            else if (Procede == 5 || Procede == 12 || Procede == 13 || Procede == 14 || Procede == 9 || Procede == 10 || Procede == 16 || Procede == 17 )
            {
                foreach (int cod in seleccion)
                {

                    
                    if (Application.OpenForms["frmDetalleGuia"] != null)
                    {
                        Application.OpenForms["frmDetalleGuia"].Close();
                    }
                    
                   frmDetalleGuia form = new frmDetalleGuia();



             //      frmDetalleGuia form = (frmDetalleGuia)Application.OpenForms["frmDetalleGuia"];


                    if (Procede == 16)
                        form.codIngreso = pro.CodProducto;


                    form.txtStock.Text = pro.StockDisponible.ToString();
                    form.txtCantidad.Focus();
                    form.Seleccion = 2;
                    form.Proceso = Proceso;
                    form.Procede = Procede;
                    form.Codlista = CodLista;
                    if (Procede == 10) form.chBonificacion.Visible = true;
                    form.txtCodigo.Text = cod.ToString();
                    if (form.repetido == 1) { form.Close(); this.Close(); }
                    else
                    {
                        form.txtCantidad.Focus();
                        form.ShowDialog();
                    }
                    //     form.txtStock.Text = pro.StockDisponible.ToString();

                }
            }
            else if (Procede == 13)
            {
                codigoPro = pro.CodProducto;
                referenciaPro = pro.Referencia;
                descripcionPro = pro.Descripcion;

            }
            else if (Procede == 14)
            {
                codigoPro = pro.CodProducto;
                referenciaPro = pro.Referencia;
                descripcionPro = pro.Descripcion;

            }
            else if (Procede == 15)//Stock Producto Term
            {

                frmStockProducto form = (frmStockProducto)Application.OpenForms["frmStockProducto"];
                form.txtReferencia.Text = pro.Referencia;
                form.txtNombre.Text = pro.Descripcion;
                form.codProducto = pro.CodProducto;
            
            }

            else if (Procede == 18 || Procede == 19)//frmRegistroProducto
            {

                frmRegistroProducto form = (frmRegistroProducto)Application.OpenForms["frmRegistroProducto"];
                form.textBox3.Text = pro.Referencia;
                form.textBox2.Text = pro.Descripcion;
                form.codProductoReferencia = pro.CodProducto;
            }


            else if (Procede == 20)//frmRptCentroCostos
            {
                frmRptCentroCostos form = (frmRptCentroCostos)Application.OpenForms["frmRptCentroCostos"];
                form.textBox2.Text = pro.Referencia;//Desde
            }
            else if ( Procede == 21)//frmRptCentroCostos
            {
                frmRptCentroCostos form = (frmRptCentroCostos)Application.OpenForms["frmRptCentroCostos"];
                form.textBox3.Text = pro.Referencia;//Hasta
            }
            else if (Procede == 22)//frmSalidaArticulo
            {
                frmSalidaArticulo form = (frmSalidaArticulo)Application.OpenForms["frmSalidaArticulo"];
                form.txtCodigo.Text = pro.CodProducto.ToString();
                form.txtStock.Text = pro.StockDisponible.ToString();
            } 
           this.Close();
            
        }

        private void recorrelista()
        {
            seleccion.Clear();
            if (dgvProductos.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProductos.SelectedRows)
                {
                    seleccion.Add(Convert.ToInt32(row.Cells[codigo.Name].Value));
                }
            }
        }

        private void cbTipoArticulo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CargaLista(Procede);
        }

        private void frmProductosLista_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
