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
    public partial class frmProveedoresLista : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmProveedor AdmPro = new clsAdmProveedor();        
        clsProveedor pro = new clsProveedor();
        public Int32 Proceso = 0; //(1) Ingreso (2)Salida (3)Relacion
        public Int32 Procede = 0; //(1) Nota de ingreso (2) Letra        
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;


        public Boolean liqcompra;
        public frmProveedoresLista()
        {
            InitializeComponent();
        }

        private void CargaLista()
        {
            dgvProveedor.DataSource = data;
            data.DataSource = AdmPro.RelacionProveedores();
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvProveedor.ClearSelection();
            dgvProveedor.Focus();
        }

        private void frmProveedoresLista_Load(object sender, EventArgs e)
        {   
            CargaLista();
            label2.Text = "RUC";
            label3.Text = "ruc";
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

        private void dgvProveedor_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label2.Text = dgvProveedor.Columns[e.ColumnIndex].HeaderText;
            label3.Text = dgvProveedor.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Procede == 1)
            {
                frmNotaIngresoPorOrden form = (frmNotaIngresoPorOrden)Application.OpenForms["frmNotaIngresoPorOrden"];
                form.CodProveedor = pro.CodProveedor;
                form.txtCodProv.Text = pro.Ruc;
                form.txtNombreProv.Text = pro.RazonSocial;
                form.txtCodProveedor.Text = pro.CodProveedor.ToString();
                this.Close();    
            }
            else if (Procede == 2)
            {
                frmGestionLetra form = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
                form.CodProveedor = pro.CodProveedor;                
                this.Close();
            }
            else if (Procede == 3)
            {
                frmOrdenCompras form = (frmOrdenCompras)Application.OpenForms["frmOrdenCompras"];
                form.CodProveedor = pro.CodProveedor;
                form.txtCodProv.Text = pro.Ruc;
                form.txtNombreProv.Text = pro.RazonSocial;
                this.Close();
            }
            if (Procede == 4)
            {
                frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                form.CodProveedor = pro.CodProveedor;
                form.txtCodProv.Text = pro.Ruc;
                form.txtNombreProv.Text = pro.RazonSocial;
                this.Close();
            }
            if (Procede == 5)
            {
                frmListaPreciosProductos form = (frmListaPreciosProductos)Application.OpenForms["frmListaPreciosProductos"];
                if (pro.RazonSocial == null)
                {
                    form.txtProveedorNomb.Focus();
                }
                else
                {
                    form.txtProveedorCod.Text = pro.CodProveedor.ToString();
                    form.txtProveedorNomb.Text = pro.RazonSocial;
                }
                this.Close();
            }

             //NOTA DE CREDITO POR COMPRA
            //MODIFICADO ALEX 23/02/2015
            else if (Procede == 6)
            {
                frmNotadeCreditoCompra form = (frmNotadeCreditoCompra)Application.OpenForms["frmNotadeCreditoCompra"];
                form.CodProveedor = pro.CodProveedor;
                form.txtCodProveedor.Text = pro.Ruc;
                form.txtNombreProveedor.Text = pro.RazonSocial;
                this.Close();
            }
            //NOTA DE DEBITO POR COMPRA
            //MODIFICADO ALEX 04/03/2015
            else if (Procede == 7)
            {
                frmNotadeDebitoCompra form = (frmNotadeDebitoCompra)Application.OpenForms["frmNotadeDebitoCompra"];
                form.CodProveedor = pro.CodProveedor;
                form.txtCodProveedor.Text = pro.Ruc;
                form.txtNombreProveedor.Text = pro.RazonSocial;
                this.Close();
            }

               //RAGA
            else if (Procede == 8)
            {
                frmIngresoCamaras form = (frmIngresoCamaras)Application.OpenForms["frmIngresoCamaras"];
                form.CodProveedor = pro.CodProveedor;
                form.txtCodProv.Text = pro.Ruc;
                form.txtNombreProv.Text = pro.RazonSocial;
                this.Close();
            }
        }

        private void dgvProveedor_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvProveedor.Rows.Count >= 1 && e.Row.Selected)
            {
                pro.CodProveedor = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
                pro.Ruc = e.Row.Cells[ruc.Name].Value.ToString();
                pro.RazonSocial = e.Row.Cells[razonsocial.Name].Value.ToString();                
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.SelectedRows.Count > 0)
            {
                if (Procede == 1)
                {
                    frmNotaIngresoPorOrden form = (frmNotaIngresoPorOrden)Application.OpenForms["frmNotaIngresoPorOrden"];
                    form.CodProveedor = pro.CodProveedor;
                    form.txtCodProv.Text = pro.Ruc;
                    form.txtNombreProv.Text = pro.RazonSocial;
                    form.txtCodProveedor.Text = pro.CodProveedor.ToString();
                    this.Close();
                }
                else if (Procede == 2)
                {
                    frmGestionLetra form = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
                    form.CodProveedor = pro.CodProveedor;
                    this.Close();
                }
                else if (Procede == 3)
                {
                    frmOrdenCompras form = (frmOrdenCompras)Application.OpenForms["frmOrdenCompras"];
                    form.CodProveedor = pro.CodProveedor;
                    form.txtCodProv.Text = pro.Ruc;
                    form.txtNombreProv.Text = pro.RazonSocial;
                    this.Close();
                }
                if (Procede == 4)
                {
                    frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                    form.CodProveedor = pro.CodProveedor;
                    form.txtCodProv.Text = pro.Ruc;
                    form.txtNombreProv.Text = pro.RazonSocial;
                    this.Close();
                }
                if (Procede == 5)
                {
                    frmListaPreciosProductos form = (frmListaPreciosProductos)Application.OpenForms["frmListaPreciosProductos"];
                    if (pro.RazonSocial == null)
                    {
                        form.txtProveedorNomb.Focus();
                    }
                    else{
                    form.txtProveedorCod.Text = pro.CodProveedor.ToString();
                    form.txtProveedorNomb.Text = pro.RazonSocial;
                    }
                    this.Close();
                }

                 //NOTA DE CREDITO POR COMPRA
                //MODIFICADO ALEX 23/02/2015
                else if (Procede == 6)
                {
                    frmNotadeCreditoCompra form = (frmNotadeCreditoCompra)Application.OpenForms["frmNotadeCreditoCompra"];
                    form.CodProveedor = pro.CodProveedor;
                    form.txtCodProveedor.Text = pro.Ruc;
                    form.txtNombreProveedor.Text = pro.RazonSocial;
                    this.Close();
                }
                //NOTA DE DEBITO POR COMPRA
                //MODIFICADO ALEX 04/03/2015
                else if (Procede == 7)
                {
                    frmNotadeDebitoCompra form = (frmNotadeDebitoCompra)Application.OpenForms["frmNotadeDebitoCompra"];
                    form.CodProveedor = pro.CodProveedor;
                    form.txtCodProveedor.Text = pro.Ruc;
                    form.txtNombreProveedor.Text = pro.RazonSocial;
                    this.Close();
                }

                //RAGA
                else if (Procede == 8)
                {
                    frmIngresoCamaras form = (frmIngresoCamaras)Application.OpenForms["frmIngresoCamaras"];
                    form.CodProveedor = pro.CodProveedor;
                    form.txtCodProv.Text = pro.Ruc;
                    form.txtNombreProv.Text = pro.RazonSocial;
                    this.Close();
                }
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmGestionProveedor frm = new frmGestionProveedor();

            //RAGA
            if (liqcompra == true)
                frm.liqcompra = true; 



            frm.Proceso = 1;
            frm.ShowDialog();
            CargaLista();
        }

        private void dgvProveedor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso == 3)
            {
                btnAceptar.Enabled = true;
            }
        }

        private void dgvProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)Keys.Return)
            //{
            //    if (dgvProveedor.SelectedRows.Count > 0)
            //    {
            //        if (Procede == 1)
            //        {
            //            frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
            //            form.CodProveedor = pro.CodProveedor;
            //            form.txtCodProv.Text = pro.Ruc;
            //            form.txtNombreProv.Text = pro.RazonSocial;
            //            this.Close();
            //        }
            //        else if (Procede == 2)
            //        {
            //            frmGestionLetra form = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
            //            form.CodProveedor = pro.CodProveedor;
            //            this.Close();
            //        }
            //    }
            //}
        }

        private void dgvProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (dgvProveedor.SelectedRows.Count > 0)
                {
                    if (Procede == 1)
                    {
                        frmNotaIngresoPorOrden form = (frmNotaIngresoPorOrden)Application.OpenForms["frmNotaIngresoPorOrden"];
                        form.CodProveedor = pro.CodProveedor;
                        form.txtCodProv.Text = pro.Ruc;
                        form.txtNombreProv.Text = pro.RazonSocial;
                        this.Close();
                    }
                    else if (Procede == 2)
                    {
                        frmGestionLetra form = (frmGestionLetra)Application.OpenForms["frmGestionLetra"];
                        form.CodProveedor = pro.CodProveedor;
                        this.Close();
                    }
                    else if (Procede == 3)
                    {
                        frmOrdenCompras form = (frmOrdenCompras)Application.OpenForms["frmOrdenCompras"];
                        form.CodProveedor = pro.CodProveedor;
                        form.txtCodProv.Text = pro.Ruc;
                        form.txtNombreProv.Text = pro.RazonSocial;
                        this.Close();
                    }
                    if (Procede == 4)
                    {
                        frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
                        form.CodProveedor = pro.CodProveedor;
                        form.txtCodProv.Text = pro.Ruc;
                        form.txtNombreProv.Text = pro.RazonSocial;
                        this.Close();
                    }
                }
            }
        }


    }
}
