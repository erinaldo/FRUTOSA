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

    public partial class frmVentas : DevComponents.DotNetBar.OfficeForm    
    {
        clsAdmNotaSalida AdmNotaS = new clsAdmNotaSalida();
        clsNotaSalida nota = new clsNotaSalida();
        clsAdmFacturaVenta AdmVenta = new clsAdmFacturaVenta();
        clsFacturaVenta venta = new clsFacturaVenta();
        clsPago pag = new clsPago();
        clsAdmPago admPago = new clsAdmPago();

        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;        



        public frmVentas()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargaLista()
        {
            dgvVentas.DataSource = data;
            data.DataSource = AdmVenta.Ventas(frmLogin.iCodAlmacen, dtpDesde.Value, dtpHasta.Value);
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvVentas.ClearSelection();
        }

        private void btnIrPedido_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow != null)
            {
                DataGridViewRow row = dgvVentas.CurrentRow;
                if (dgvVentas.Rows.Count >= 1)
                {
                    frmVenta form = new frmVenta();
                    form.MdiParent = this.MdiParent;
                    form.CodVenta= venta.CodFacturaVenta;
                    form.Proceso = 3;
                    form.Show();
                }
            }
        }

        private void frmPedidosPendientes_Load(object sender, EventArgs e)
        {
            CargaLista();
        }
        

        private void dgvPedidosPendientes_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvVentas.Rows.Count >= 1 && e.Row.Selected)
            {
                venta.CodFacturaVenta= e.Row.Cells[codigo.Name].Value.ToString();               
            }
        }

        private void dgvPedidosPendientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            if (dgvVentas.Rows.Count >= 1 && e.RowIndex != -1)
            {
                frmVenta form = new frmVenta();
                form.MdiParent = this.MdiParent;
                form.CodVenta = venta.CodFacturaVenta;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow != null)
            {
                DataGridViewRow row = dgvVentas.CurrentRow;
                if (btnAnular.Text == "Anular")
                {
                    if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow.Index != -1)
                    {
                        DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {

                            venta = AdmVenta.CargaFacturaVenta(Convert.ToInt32(venta.CodFacturaVenta));
                            pag = admPago.MuestraPagoVenta(frmLogin.iCodAlmacen, Convert.ToInt32(venta.CodFacturaVenta));

                            if (venta.FechaSalida.Date == DateTime.Now.Date && !venta.Impreso)
                            {
                                if (pag != null && Convert.ToInt32(pag.CodNota) != Convert.ToInt32(venta.CodFacturaVenta))
                                {
                                    if (AdmVenta.anular(Convert.ToInt32(venta.CodFacturaVenta)))
                                    {
                                        MessageBox.Show("El documento ha sido anulado correctamente", "Ventas",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        CargaLista();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No se puede Anular Ventas que ya Tienen un Pago", "Ventas",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("No se puede Anular Ventas de Fechas Anteriores ni que Esten Impresas",
                                    "Ventas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }

                }
                else if (btnAnular.Text == "Activar")
                {
                    if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow.Index != -1)
                    {
                        DialogResult dlgResult = MessageBox.Show("Esta seguro que desea activar el documento seleccionado", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (AdmVenta.activar(Convert.ToInt32(venta.CodFacturaVenta)))
                            {
                                MessageBox.Show("El documento ha sido activado correctamente", "Notas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargaLista();
                            }
                        }
                    }
                }
            }
        }

        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvVentas.Rows.Count >= 1 && e.RowIndex != -1)
            {
                btnIrPedido.Enabled = true;
                if (dgvVentas.Rows[e.RowIndex].Cells[estado.Name].Value.ToString() == "ACTIVO")
                {
                    btnAnular.Text = "Anular";
                    btnAnular.Enabled = true;
                    btnAnular.ImageIndex = 4;
                }
                else
                {
                    btnAnular.Text = "Activar";
                    btnAnular.Enabled = true;
                    btnAnular.ImageIndex = 6;
                }
            } 
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("ListaGuias");
            // Columnas
            foreach (DataGridViewColumn column in dgvVentas.Columns)
            {
                DataColumn dc = new DataColumn(column.Name.ToString());
                dt.Columns.Add(dc);
            }
            // Datos
            for (int i = 0; i < dgvVentas.Rows.Count; i++)
            {
                DataGridViewRow row = dgvVentas.Rows[i];
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dgvVentas.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);
            ds.WriteXml("C:\\XML\\ListaVentasRPT.xml", XmlWriteMode.WriteSchema);


            CRListaVentas rpt = new CRListaVentas();
            frmListaVentas frm = new frmListaVentas();
            rpt.SetDataSource(ds);
            frm.crvListaGuias.ReportSource = rpt;
            frm.Show();
        }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void btnVistaSucursales_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow != null)
            {
                if (btnVistaSucursales.Text == "Activar Vista")
                {
                    if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow.Index != -1)
                    {
                        DialogResult dlgResult = MessageBox.Show("¿Esta seguro que desea activar la vista de este documento en otras sucursales?", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgResult == DialogResult.No)
                        {
                            return;
                        }
                        else
                        {
                            if (AdmVenta.VistaSucursal(Convert.ToInt32(venta.CodFacturaVenta), 1))
                            {
                                MessageBox.Show("El documento puede ser visualizado desde cualquier sucursal correctamente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargaLista();
                            }
                        }
                    }
                }
                //else if (btnVistaSucursales.Text == "Desactivar Vista")
                //{
                //    if (dgvVentas.Rows.Count >= 1 && dgvVentas.CurrentRow.Index != -1)
                //    {
                //        DialogResult dlgResult = MessageBox.Show("¿Esta seguro que desea desactivar la vista de este documento en otras sucursales?", "Notas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //        if (dlgResult == DialogResult.No)
                //        {
                //            return;
                //        }
                //        else
                //        {
                //            if (AdmVenta.VistaSucursal(Convert.ToInt32(venta.CodFacturaVenta), 2))
                //            {
                //                MessageBox.Show("El documento puede ser visualizado desde cualquier sucursal correctamente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                CargaLista();
                //            }
                //        }
                //    }
                //}
            }
        }
    }
}
