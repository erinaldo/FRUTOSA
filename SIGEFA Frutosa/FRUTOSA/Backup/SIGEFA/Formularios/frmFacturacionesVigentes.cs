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

    public partial class frmFacturacionesVigentes : DevComponents.DotNetBar.OfficeForm    
    {
        clsAdmFactura Admfac = new clsAdmFactura();
        clsFactura fac = new clsFactura();        
        public Int32 Proce = 0; //
        public Int32 Proceso = 0; //

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;

        public Int32 OrdCom;

        public clsProveedor deta = new clsProveedor();

        public frmFacturacionesVigentes()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargaLista()
        {
                dgvOrdenes.DataSource = data;
                data.DataSource = Admfac.MuestraFactura(dtpDesde.Value.Date, dtpHasta.Value.Date,frmLogin.iCodAlmacen);
                data.Filter = String.Empty;
                filtro = String.Empty;
        }


        private void CargaListaLiquidaciones()
        {
            dgvOrdenes.DataSource = data;
            data.DataSource = Admfac.MuestraLiquidacion(dtpDesde.Value.Date, dtpHasta.Value.Date, frmLogin.iCodAlmacen);
            data.Filter = String.Empty;
            filtro = String.Empty;
        }


        private void btnIrCotizacion_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.Rows.Count >= 1 && dgvOrdenes.CurrentRow != null)
            {
                DataGridViewRow row = dgvOrdenes.CurrentRow;
                frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
                form.MdiParent = this.MdiParent;
                form.codFac = fac.CodFactura;


                if(Proceso == 1)
                form.Proceso = 3;

                if (Proceso == 2)
                    form.Proceso = 8;


                form.Show();
            }
        }

        private void frmOrdenesVigentes_Load(object sender, EventArgs e)
        {
            //RAGA
            if (Proceso == 1)
            {
                CargaLista();
                btnIrCotizacion.Text = "Ir Facturación";
            }
            else if (Proceso == 2)
            {
                CargaListaLiquidaciones();
                btnIrCotizacion.Text = "Ir Liquidación";
            }
        }

        private void dgvCotizaciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvOrdenes.Rows.Count >= 1 && e.Row.Selected)
            {
                fac.CodFactura = Convert.ToInt32(e.Row.Cells[codfactura.Name].Value);
            }
        }

        private void dgvCotizaciones_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrdenes.Rows.Count >= 1 && e.RowIndex != -1)
            {
                frmNotaIngresoPorOrden form = new frmNotaIngresoPorOrden();
                form.MdiParent = this.MdiParent;
                form.codFac = fac.CodFactura;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dgvOrdenes.CurrentRow != null)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular la Factura seleccionada", "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (Admfac.anular(fac.CodFactura))
                    {
                        MessageBox.Show("La Factura ha sido anulada correctamente", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }


        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //RAGA
            if (Proceso == 1)
            {
                CargaLista();
              
            }
            else if (Proceso == 2)
            {
                CargaListaLiquidaciones();
             
            }       
        }


    }
}
