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

    public partial class frmPedidosPendientes : DevComponents.DotNetBar.OfficeForm    
    {
        clsAdmPedido AdmPedido = new clsAdmPedido();
        clsPedido pedido = new clsPedido();        
        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;        



        public frmPedidosPendientes()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargaLista()
        {
            dgvPedidosPendientes.DataSource = data;
            data.DataSource = AdmPedido.MuestraPedidos(frmLogin.iCodAlmacen);
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvPedidosPendientes.ClearSelection();
        }

        private void btnIrPedido_Click(object sender, EventArgs e)
        {
            if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null)
            {
                DataGridViewRow row = dgvPedidosPendientes.CurrentRow;
                frmPedido form = new frmPedido();
                form.MdiParent = this.MdiParent;
                form.CodPedido = pedido.CodPedido;                
                form.Proceso = 3;
                form.Show();            
            }
        }

        private void frmPedidosPendientes_Load(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void btGenVenta_Click(object sender, EventArgs e)
        {
            if (dgvPedidosPendientes.Rows.Count >= 1 && dgvPedidosPendientes.CurrentRow != null)
            {
                if (pedido.CodPedido != "")
                {
                    if (Application.OpenForms["frmVenta"] != null)
                    {
                        Application.OpenForms["frmVenta"].Close();
                    }
                    else
                    {
                        frmVenta form = new frmVenta();
                        form.MdiParent = this.MdiParent;
                        form.Proceso = 1;
                        //form.txtPedido.Text = pedido.CodPedido.ToString();
                        //form.txtPedido.ReadOnly = true;
                        KeyPressEventArgs ee = new KeyPressEventArgs((char)Keys.Return);
                        //form.txtPedido_KeyPress(form.txtPedido, ee);
                        form.Show();
                    }
                }
            }
        }

        private void dgvPedidosPendientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvPedidosPendientes.Rows.Count >= 1 && e.Row.Selected)
            {
                pedido.CodPedido = e.Row.Cells[codigo.Name].Value.ToString();               
            }
        }

        private void dgvPedidosPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (dgvPedidosPendientes.Rows.Count >= 1 && e.RowIndex != -1)
            {
                frmPedido form = new frmPedido();
                form.MdiParent = this.MdiParent;
                form.CodPedido = pedido.CodPedido;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dgvPedidosPendientes.CurrentRow != null && pedido.CodPedido != "")
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el pedido seleccionado", "Pedidos Pendientes", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (AdmPedido.delete(Convert.ToInt32(pedido.CodPedido)))
                    {
                        MessageBox.Show("El pedido ha sido anulado correctamente", "Pedidos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }
    }
}
