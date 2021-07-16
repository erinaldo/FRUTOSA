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
    public partial class frmListasPorProducto : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmProducto AdmPro = new clsAdmProducto();
        clsAdmListaPrecio AdmLista = new clsAdmListaPrecio();
        clsDetalleListaPrecio detalle = new clsDetalleListaPrecio();
        clsProducto pro = new clsProducto();
        String initialcellvalue;
        String finalcellvalue;
        public Int32 CodProducto = 0;

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
       
        
       
        public frmListasPorProducto()
        {
            InitializeComponent();
        }

        private void frmListasPorProducto_Load(object sender, EventArgs e)
        {  
            CargaLista();            
        }
        

        private void CargaLista()
        {
            dgvProductos.DataSource = data;
            data.DataSource = AdmLista.PreciosProducto(CodProducto, frmLogin.iCodSucursal,frmLogin.iCodAlmacen);
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvProductos.ClearSelection();            
        }
        
        private void frmListasPorProducto_Shown(object sender, EventArgs e)
        {
            CargaLista();
        }     
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {            
            try
            {
                DialogResult dlgResult = MessageBox.Show("Se recalculará el precio en base al valor real", "Listas de Precios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    CargaLista(); 
                    return;
                }
                else
                {
                    finalcellvalue = dgvProductos.CurrentCell.Value.ToString();
                    if (initialcellvalue != finalcellvalue)
                    {
                        Double dneto, dfinal, drecargo = 0;
                        pro = AdmPro.CargaProducto(CodProducto, frmLogin.iCodAlmacen);
                        if (dgvProductos.Focused && e.ColumnIndex == dgvProductos.Columns[margen.Name].Index)
                        {
                            dneto = Convert.ToDouble(dgvProductos.CurrentRow.Cells[valoractual.Name].Value) * (1 + Convert.ToDouble(dgvProductos.CurrentCell.Value) / 100);
                            if (pro.Igv)
                            {
                                dfinal = dneto * (1 + frmLogin.Configuracion.IGV / 100);
                            }
                            else
                            {
                                dfinal = dneto;
                            }
                            dgvProductos.CurrentRow.Cells[neto.Name].Value = dneto;
                            dgvProductos.CurrentRow.Cells[final.Name].Value = dfinal;
                        }
                        if (dgvProductos.Focused && e.ColumnIndex == dgvProductos.Columns[final.Name].Index)
                        {
                            if (Convert.ToDouble(dgvProductos.CurrentCell.Value) != 0)
                            {
                                if (pro.Igv)
                                {
                                    dneto = Convert.ToDouble(dgvProductos.CurrentCell.Value) / (1 + frmLogin.Configuracion.IGV / 100);
                                }
                                else
                                {
                                    dneto = Convert.ToDouble(dgvProductos.CurrentCell.Value);
                                }
                                if (Convert.ToDouble(dgvProductos.CurrentRow.Cells[valoractual.Name].Value) > 0)
                                {
                                    drecargo = (dneto/Convert.ToDouble(dgvProductos.CurrentRow.Cells[valoractual.Name].Value) - 1)*100;
                                }
                                else
                                {
                                    drecargo = 0;
                                }
                                dgvProductos.CurrentRow.Cells[margen.Name].Value = drecargo;
                            }
                            else
                            {
                                dneto = 0;
                            }
                            dgvProductos.CurrentRow.Cells[neto.Name].Value = dneto;
                        }

                        detalle.CodDetalleLista = Convert.ToInt32(dgvProductos.CurrentRow.Cells[codigo.Name].Value);
                        detalle.CodListaPrecio = Convert.ToInt32(dgvProductos.CurrentRow.Cells[codlista.Name].Value);
                        detalle.Margen = Convert.ToDouble(dgvProductos.CurrentRow.Cells[margen.Name].Value);
                        detalle.PrecioNeto = Convert.ToDouble(dgvProductos.CurrentRow.Cells[neto.Name].Value);
                        detalle.Precio = Convert.ToDouble(dgvProductos.CurrentRow.Cells[final.Name].Value);
                        detalle.CodProducto = CodProducto;

                        if (AdmLista.updatedetalle(detalle))
                        {
                            MessageBox.Show("Se actualizó el precio lista correctamente", "Listas de Precio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargaLista();
                        }
                    }
                    
                }

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvProductos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            initialcellvalue = dgvProductos.CurrentCell.Value.ToString();
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.Rows.Count > 0 && dgvProductos.CurrentRow != null && dgvProductos.CurrentRow.Index != -1)
            {
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[codlista.Name].Value) >= 2 &&
                    Convert.ToInt32(dgvProductos.CurrentRow.Cells[codlista.Name].Value) <= 5 &&
                    Convert.ToDecimal(dgvProductos.CurrentRow.Cells[final.Name].Value) == 0)
                {
                    MessageBox.Show("No hay una lista de precios base. No puede Modificar!!" + Environment.NewLine +
                        "Lista Base: [CONTADO]", "Lista de Precios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvProductos.CurrentRow.Cells[margen.Name].ReadOnly = true;
                    dgvProductos.CurrentRow.Cells[final.Name].ReadOnly = true;
                }

                if (Convert.ToDecimal(dgvProductos.CurrentRow.Cells[valoractual.Name].Value) == 0)
                {
                    dgvProductos.CurrentRow.Cells[margen.Name].ReadOnly = true;
                }

            }
        }

        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.Rows.Count > 0 && dgvProductos.CurrentRow != null && dgvProductos.CurrentRow.Index != -1)
            {
                if (Convert.ToInt32(dgvProductos.CurrentRow.Cells[codlista.Name].Value) >= 2 &&
                    Convert.ToInt32(dgvProductos.CurrentRow.Cells[codlista.Name].Value) <= 5 &&
                    Convert.ToDecimal(dgvProductos.CurrentRow.Cells[final.Name].Value) == 0)
                {
                    MessageBox.Show("No hay una lista de precios base. No puede Modificar!!" + Environment.NewLine +
                        "Lista Base: [CONTADO]", "Lista de Precios", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvProductos.CurrentRow.Cells[margen.Name].ReadOnly = true;
                    dgvProductos.CurrentRow.Cells[final.Name].ReadOnly = true;
                }

                if (Convert.ToDecimal(dgvProductos.CurrentRow.Cells[valoractual.Name].Value) == 0)
                {
                    dgvProductos.CurrentRow.Cells[margen.Name].ReadOnly = true;
                }

            }
        }
       
    }
}
