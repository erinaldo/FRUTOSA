using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Formularios;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Reportes
{
    public partial class frmRptCentroCostos2 : DevComponents.DotNetBar.OfficeForm
    {
        clsAdmEmpresa admEmp = new clsAdmEmpresa();
        clsAdmFormaPago AdmPago = new clsAdmFormaPago();
 
        clsProducto pro = new clsProducto();
        clsAdmProducto AdmPro = new clsAdmProducto();

        clsAdmCentro admcentro = new clsAdmCentro();

        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;

        public static BindingSource data0 = new BindingSource();
        String filtro0 = String.Empty;
        
        Int32 codnivel = 0, param_impresion = 0, moneda = 0;
        Boolean valorizado = false, param_tipo_costo = false;
        clsReporteFrutosa ds = new clsReporteFrutosa();

        clsAdmTipoArticulo admtipoa = new clsAdmTipoArticulo();

        clsAdmAlmacen admAlm = new clsAdmAlmacen();

        Int32 codprod,codart;

        Int32 mincc = 0, maxcc = 0, minart = 0, maxart = 0;
        List<Int32> seleccion = new List<Int32>();
        List<Int32> seleccion2 = new List<Int32>();

        public frmRptCentroCostos2()
        {
            InitializeComponent();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            CRptCentroCostos1 rpt;
            CRptCentroCostos2 rpt2;
            frmListaIngresosCamara frm = new frmListaIngresosCamara();

            codnivel = cmbVendedor.SelectedIndex + 1;

            if (radioButton3.Checked == true)
                moneda = 0;

            if (radioButton4.Checked == true)
                moneda = 1;

            if (radioButton5.Checked == true)
                moneda = 2;


            if (radioButton7.Checked == true)
                param_impresion = 2;
            if (radioButton8.Checked == true)
                param_impresion = 1;

            valorizado = true;
            param_tipo_costo = true;



            

          if (param_impresion == 1)
          {
              rpt2 = new CRptCentroCostos2();
              rpt2.SetDataSource(ds.rptCentroCostos1(2, param_impresion, codnivel, moneda, param_tipo_costo, mincc, maxcc, minart, maxart, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, valorizado, frmLogin.iCodAlmacen).Tables[0]);
              frm.crvIngresosCamara.ReportSource = rpt2;
          }
          if (param_impresion == 2)
          {
              rpt = new CRptCentroCostos1();
              rpt.SetDataSource(ds.rptCentroCostos1(2, param_impresion, codnivel, moneda, param_tipo_costo, mincc, maxcc, minart, maxart, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, valorizado, frmLogin.iCodAlmacen).Tables[0]);
              frm.crvIngresosCamara.ReportSource = rpt;
          }

          frm.Show();

            /*
            CRptCentroCostos1 rpt = new CRptCentroCostos1();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds.rptCentroCostos1(2, param_impresion, codnivel, moneda, param_tipo_costo, mincc, maxcc, minart, maxart, dateTimePicker1.Value.Date, dateTimePicker2.Value.Date, valorizado, frmLogin.iCodAlmacen).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
            */
        }

        private void txtUnArt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                frmProductosLista frm = new frmProductosLista();
                frm.Procede = 8; //(8) Procede desde el formulario frmParamVentxVendedor
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    CargaProducto(frm.GetCodigoProducto());
                }
            }
        }

        private void CargaProducto(Int32 Codigo)
        {
            pro = AdmPro.CargaProducto(Codigo, frmLogin.iCodAlmacen);
            txtUnArt.Text = pro.Referencia;
            txtArticulo.Text = pro.Descripcion;
        }

        private void rbArt_CheckedChanged(object sender, EventArgs e)
        {
            txtUnArt.Text = "";
            txtArticulo.Text = "";
            txtUnArt.Enabled = rbArt.Checked;
            txtUnArt.Focus();
        }

        private void frmRptCentroCostos2_Load(object sender, EventArgs e)
        {
            CargaAlmacenes();
            cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
              cmbVendedor.SelectedIndex = 0;
              CargaListaNiveles();
              CargaTipoArticulos();


 
        }
        private void CargaAlmacenes()
        {
            cmbAlmacen.DataSource = admAlm.CargaAlmacenes(frmLogin.iNivelUser, frmLogin.iCodEmpresa, frmLogin.iCodUser);
            cmbAlmacen.DisplayMember = "nombre";
            cmbAlmacen.ValueMember = "codAlmacen";
            cmbAlmacen.SelectedIndex = 0;

        }




        private void CargaListaNiveles()
        {


            dgvAreas.DataSource = data0;
            data0.DataSource = admcentro.ListaCentrosNivel(1);
            data0.Filter = String.Empty;
            filtro0 = String.Empty;

         //   dgvAreas.DataSource = admcentro.ListaCentrosNivel(1);
        }


        public void CargaTipoArticulos()
        {

            dataGridView1.DataSource = data;
            data.DataSource = admtipoa.MuestraTipoArticulosDetallado();
            data.Filter = String.Empty;
            filtro = String.Empty;

        }




        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbVendedor_SelectedIndexChanged(object sender, EventArgs e)
        {


            dgvAreas.DataSource = data0;
            data0.DataSource = admcentro.ListaCentrosNivel(cmbVendedor.SelectedIndex + 1);
            data0.Filter = String.Empty;
            filtro0 = String.Empty;

      //      dgvAreas.DataSource = admcentro.ListaCentrosNivel(cmbVendedor.SelectedIndex + 1);
        
        }

        private void dgvAreas_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvAreas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label4.Text = dgvAreas.Columns[e.ColumnIndex].HeaderText;
            label9.Text = dgvAreas.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text.Length >= 1)
                {
                    data0.Filter = String.Format("[{0}] like '*{1}*'", label9.Text.Trim(), txtFiltro.Text.Trim());
                }
                else
                {
                    data0.Filter = String.Empty;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                dgvAreas.MultiSelect = true;
                foreach (DataGridViewRow r in dgvAreas.Rows)
                {
                    r.Selected = true;
                    seleccion.Add(  Int32.Parse(r.Cells[codigocentrocosto.Name].Value.ToString()));
                }
                mincc = seleccion.Min();
                maxcc = seleccion.Max();
                seleccion.Clear();
            }
            else
            {
                
                foreach (DataGridViewRow r in dgvAreas.Rows)
                    r.Selected = false;
                dgvAreas.MultiSelect = false;
                mincc = Int32.Parse(dgvAreas.CurrentRow.Cells[codigocentrocosto.Name].Value.ToString());
                maxcc = mincc;

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox2.Checked == true)
            {
                dataGridView1.MultiSelect = true;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    r.Selected = true;
                    seleccion2.Add(Int32.Parse(r.Cells[codarticulo.Name].Value.ToString()));
                }
                minart = seleccion2.Min();
                maxart = seleccion2.Max();
                seleccion2.Clear();
            }
            else
            {
              
                foreach (DataGridViewRow r in dataGridView1.Rows)
                    r.Selected = false;

                minart =  Int32.Parse(dataGridView1.CurrentRow.Cells[codarticulo.Name].Value.ToString());
                maxart = minart;

                dataGridView1.MultiSelect = false;
            }
        }

        private void dgvAreas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dgvAreas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
           
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
           
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label10.Text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            label12.Text = dataGridView1.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // data.Filter = String.Format("[gradosbrix0] > '{0}' AND [gradosbrix0] < '{1}'", textBox1.Text, textBox2.Text);

                if (textBox1.Text.Length >= 1)
                {
                    data.Filter = String.Format("[codarticulo] like '{0}'", textBox1.Text.Trim());
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
    }
}
