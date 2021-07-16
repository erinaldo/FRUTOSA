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
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios
{
    public partial class frmLoteProduccion : DevComponents.DotNetBar.OfficeForm
    {
        public clsLoteProduccion loteProduccion = new clsLoteProduccion();
        clsAdmLoteProduccion admLoteProduccion = new clsAdmLoteProduccion();
        clsAdmEspecie admEspecie = new clsAdmEspecie();
        clsAdmAutorizado AdmAut = new clsAdmAutorizado();
        clsAutorizado aut = new clsAutorizado();
        clsValidar ok = new clsValidar();
        clsAdmTipoCambio admTipoCambio = new clsAdmTipoCambio();
        public Int32 CodAutorizado;
        public Int32 Proceso;
        public Int32 CodLoteProduccion;


        //RAGA
        public Int32 codProducto;
        public Int32 codMatprima;
        decimal pesoMP = 0, pesoPt = 0,salidaRP=0,ingresoRP=0;

        public frmLoteProduccion()
        {
            InitializeComponent();
        }

        private void frmLoteProduccion_Load(object sender, EventArgs e)
        {
            
           CargaProceso();
            if (Proceso == 3)
            {
                CargarDatos();
            }
        }

        public void CargarDatos()
        {


            CargarLoteProduccion();
            CargaMateriaPrima();
            CargaInsumo();
            CargaSuministro();
            CargaProductoTerminado();

            PrepararReporte();
            CargaPesosyRendimiento();
            CargaReproceso();

            HabilitarDatos(false);
            HabilitarBotones(false);

        }




        public void PrepararReporte()
        {

            //RAGA
            if (dgvMateriaPrima.RowCount == 0)
                return;
            if (dgvProductoTerminado.RowCount == 0)
                return;

            button3.Enabled = true;
            for (int i = 0; i < dgvMateriaPrima.RowCount; i++)
                codMatprima = Convert.ToInt32(dgvMateriaPrima.Rows[i].Cells[CodigoMatPrima.Name].Value);

            for (int i = 0; i < dgvProductoTerminado.RowCount; i++)
                codProducto = Convert.ToInt32(dgvProductoTerminado.Rows[i].Cells[CodigoProducto.Name].Value);

        }



        public void cargaReproceso()
        {
            //RAGA
            if (dgvMateriaPrima.RowCount == 0)
                return;
            if (dgvProductoTerminado.RowCount == 0)
                return;


            button3.Enabled = true;

            for (int i = 0; i < dataGridView1.RowCount; i++)
                codMatprima = Convert.ToInt32(dataGridView1.Rows[i].Cells[CodigoMatPrima.Name].Value);



            for (int i = 0; i < dataGridView2.RowCount; i++)
                codProducto = Convert.ToInt32(dataGridView2.Rows[i].Cells[CodigoProducto.Name].Value);



            decimal pesoMP = 0, pesoPt = 0;
            pesoMP = SumaCantidadesMateriaPrima();
            pesoPt = SumaCantidadesProductoTerminado();
            txtPesoMP.Text = (decimal.Round(pesoMP, 2)).ToString();

            txtPesoPT.Text = (decimal.Round(pesoPt, 2)).ToString();



            if (pesoPt != 0)
            {
                txtRendimiento.Text = (decimal.Round((pesoPt / pesoMP) * 100, 2)).ToString();
            }


        }




        public void CargarLoteProduccion()
        {
            loteProduccion = admLoteProduccion.CargaLote(CodLoteProduccion);


            CodAutorizado = loteProduccion.CodAutorizado;
            CargaAutorizado();
            cmbEspecie.SelectedValue = loteProduccion.CodEspecie;
            txtCodLote.Text = loteProduccion.CodLoteProduccion.ToString();
            txtObservaciones.Text = loteProduccion.Observacion;
            dtpFecha.Value = loteProduccion.Fecha;
            if (loteProduccion.IEstadoLote)
            {                
                btnCerrarLote.Visible = true;
                btnReabrirLote.Visible = false;
            }
            else 
            {
                btnCerrarLote.Visible = false;
                btnReabrirLote.Visible = true;
            }
            dtpHoraInicio.Value = loteProduccion.HoraInicio;
            dtpHoraFin.Value = loteProduccion.HoraFinal;
            txtNumDoc.Text = loteProduccion.Numeroorden;

        }

        

        public void CargaProceso()
        {
            cmbEspecie.DataSource = admEspecie.CargaEspecies();
            cmbEspecie.DisplayMember = "nombre";
            cmbEspecie.ValueMember = "codEspecie";
            cmbEspecie.SelectedIndex = -1;

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCerrarLote_Click(object sender, EventArgs e)
        {

            if (txtRendimiento.Text != "" && dgvInsumos.Rows.Count > 0 && dgvSuministros.Rows.Count > 0)
            {
                frmConfirmarCerrar frm = new frmConfirmarCerrar();
                frm.ShowDialog();
                if (frm.opcion)
                {
                    CerrarLote(frm.fechaFinal, frm.horaFinal);
                }
                btnCerrarLote.Enabled = false;

            }
            else
            {
                MessageBox.Show("Lote de producción incompleto");
            }


        }

        private void CerrarLote(DateTime fechaFinal, DateTime horaFinal)
        {
            if (admLoteProduccion.ActualizarEstado(loteProduccion.CodLoteProduccion, 0) && admLoteProduccion.ActualizarFechayHoraFinal(loteProduccion.CodLoteProduccion, fechaFinal, horaFinal))
            {
                MessageBox.Show("Lote Cerrado Correctamente", "Lote Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
            }
            else
            {
                MessageBox.Show("Problemas al cerrar lote", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtAutorizacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                if (Application.OpenForms["frmAutorizado"] != null)
                {
                    Application.OpenForms["frmAutorizado"].Activate();
                }
                else
                {
                    frmAutorizado form = new frmAutorizado();
                    form.Proceso = 3;
                    form.ShowDialog();
                    aut = form.aut;
                    CodAutorizado = aut.CodAutorizado;
                    if (CodAutorizado != 0) { CargaAutorizado(); ProcessTabKey(true); }
                }
            }
        }

        private void CargaAutorizado()
        {
            try
            {
                aut = AdmAut.MuestraAutorizado(CodAutorizado);
                txtAutorizacion.Text = aut.CodAutorizado.ToString();
                lbAutorizado.Text = aut.Nombre;
                lbAutorizado.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void txtAutorizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ok.enteros(e);
            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtAutorizacion.Text != "")
                {
                    if (BuscaAutorizado())
                    {
                        ProcessTabKey(true);
                    }
                    else
                    {
                        MessageBox.Show("El codigo no existe, Presione F1 para consultar la tabla de ayuda", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private Boolean BuscaAutorizado()
        {
            
            try
            {
                aut = AdmAut.MuestraAutorizado(Convert.ToInt32(txtAutorizacion.Text));
                if (aut != null)
                {
                    lbAutorizado.Text = aut.Nombre;
                    CodAutorizado = aut.CodAutorizado;
                    lbAutorizado.Visible = true;
                    return true;
                }
                else
                {
                    lbAutorizado.Text = "";
                    CodAutorizado = 0;
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (superValidator1.Validate())
            {
                PreparLoteProduccion();
                GuardarLoteProduccion();
            }
            else
            {
                MessageBox.Show("Debe completar todos los campos requeridos(*)", "Ingreso Camara", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PreparLoteProduccion()
        {
            try
            {
                loteProduccion.CodAutorizado = CodAutorizado;
                loteProduccion.CodEspecie = Convert.ToInt32(cmbEspecie.SelectedValue);
                loteProduccion.Fecha = dtpFecha.Value;
                loteProduccion.HoraInicio = dtpHoraInicio.Value;
                loteProduccion.HoraFinal = dtpHoraFin.Value;
                loteProduccion.Observacion = txtObservaciones.Text;
                loteProduccion.CodUser = frmLogin.iCodUser;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        public void GuardarLoteProduccion()
        {
            if (admLoteProduccion.insert(loteProduccion))
            {               
                //txtCodLote.Text = loteProduccion.CodLoteProduccion.ToString();
                //HabilitarDatos(false);
                //HabilitarBotones(false);
                CodLoteProduccion = loteProduccion.CodLoteProduccion;
                CargarDatos();
                MessageBox.Show("Los datos se guardaron correctamente", "Lote Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                MessageBox.Show("Problemas al guardar los datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public void HabilitarDatos(bool estado)
        {
            txtCodLote.Enabled = estado;
            txtObservaciones.Enabled = !estado;
            cmbEspecie.Enabled = estado;
            txtAutorizacion.Enabled = estado;
            dtpFecha.Enabled = estado;
            dtpHoraInicio.Enabled = estado;
            dtpHoraFin.Enabled = estado;
            dgvInsumos.ReadOnly = estado;
            dgvMateriaPrima.ReadOnly = estado;
            dgvSuministros.ReadOnly = estado;            
        }

        public void HabilitarBotones(bool estado)
        {
            btnGuardar.Enabled = estado;
            btnNuevo.Visible = !estado;
            //btnCerrarLote.Visible = !estado;
            //btnReabrirLote.Visible = !estado;
        }

        public void CargaProductoTerminado()
        {
            try
            {
                dgvProductoTerminado.DataSource = admLoteProduccion.CargaProductoTerminadoXlote(loteProduccion.CodLoteProduccion, 1);
                dgvProductoTerminado.ClearSelection();
                txtTotalProducto.Text = SumaPrecioTotalProductoTerminado().ToString();



                if (dgvProductoTerminado.Rows.Count > 0)
                {
                    
                    checkBox1.Visible = true;
                    button1.Visible = true;
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void CargaReproceso()
        {
            try
            {
                dataGridView1.DataSource = admLoteProduccion.CargaProductoTerminadoXReproceso(loteProduccion.CodLoteProduccion, 2,1);
                dataGridView1.ClearSelection();

                dataGridView2.DataSource = admLoteProduccion.CargaProductoTerminadoXReproceso(loteProduccion.CodLoteProduccion,1,1);
                dataGridView2.ClearSelection();


                if (dataGridView1.Rows.Count > 0 && dataGridView2.Rows.Count > 0)
                {
                    salidaRP = SumaCantidadesSalidaReproceso();
                    ingresoRP = SumaCantidadesIngresoReproceso();
                    textBox1.Text = (decimal.Round(salidaRP, 2)).ToString();
                    textBox2.Text = (decimal.Round(ingresoRP, 2)).ToString();

                    if (ingresoRP >= 0 && salidaRP >= 0)
                        textBox3.Text = (decimal.Round((ingresoRP / salidaRP) * 100, 2)).ToString();
                }

               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        public void CargaMateriaPrima()
        {
            try
            {
                dgvMateriaPrima.DataSource = admLoteProduccion.CargaDetalleProductosXLote(loteProduccion.CodLoteProduccion, 5);
                dgvMateriaPrima.ClearSelection();
                txtTotal.Text =  SumaPrecioTotalMateriaPrima().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        public void CargaInsumo()
        {
            try
            {
                dgvInsumos.DataSource = admLoteProduccion.CargaDetalleProductosXLote(loteProduccion.CodLoteProduccion, 6);
                dgvInsumos.ClearSelection();
                txtTotalInsumo.Text = SumaPrecioTotalInsumo().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


        }
        public decimal SumaPrecioTotalInsumo()
        {
            decimal suma = 0;
            for (int i = 0; i < dgvInsumos.RowCount; i++)
            {
                suma = suma + Convert.ToDecimal(dgvInsumos.Rows[i].Cells[totalinsumo.Name].Value);

            }
            return suma;
        }
        public decimal SumaPrecioTotalSuministro()
        {
            decimal suma = 0;
            for (int i = 0; i < dgvSuministros.RowCount; i++)
            {
                suma = suma + Convert.ToDecimal(dgvSuministros.Rows[i].Cells[totalsuministro.Name].Value);

            }
            return suma;

        }
        public void CargaSuministro()
        {
            try
            {
                dgvSuministros.DataSource = admLoteProduccion.CargaDetalleProductosXLote(loteProduccion.CodLoteProduccion, 7);
                dgvSuministros.ClearSelection();
                txtTotalSuministro.Text = SumaPrecioTotalSuministro().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(true);
            HabilitarDatos(true);
            Limpiar();
        }
        public void Limpiar() {
            CodAutorizado = 0;
            CodLoteProduccion = 0;
            Proceso = 1;
            txtCodLote.Text = "";
            txtObservaciones.Text = "";
            dgvMateriaPrima.DataSource = new DataTable();
            dgvInsumos.DataSource = new DataTable();
            dgvSuministros.DataSource = new DataTable();
            cmbEspecie.SelectedIndex = -1;
            txtAutorizacion.Text = "";
            lbAutorizado.Text = "";
            lbAutorizado.Visible = false;
        }
        public decimal SumaPrecioTotalMateriaPrima()
        {
            decimal suma = 0;
            for (int i = 0; i < dgvMateriaPrima.RowCount; i++)
            {
                suma = suma + Convert.ToDecimal(dgvMateriaPrima.Rows[i].Cells[total.Name].Value);

            }
            return suma;
        }
        public decimal SumaPrecioTotalProductoTerminado()
        {
            return 0;
            /*
            decimal suma = 0;
            for (int i = 0; i < dgvProductoTerminado.RowCount; i++)
            {
                suma = suma + Convert.ToDecimal(dgvProductoTerminado.Rows[i].Cells[totalproducto.Name].Value);

            }
            return suma;
            */

        }


        private void customValidator1_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;  
        }

        private void customValidator2_ValidateValue(object sender, DevComponents.DotNetBar.Validator.ValidateValueEventArgs e)
        {
            if (Proceso != 0)
                if (e.ControlToValidate.Text != "")
                {
                    e.IsValid = true;
                }
                else
                {
                    e.IsValid = false;
                    e.ControlToValidate.Focus();
                }
            else
                e.IsValid = true;
        }

        private void btnReabrirLote_Click(object sender, EventArgs e)
        {
            AbrirLote();
        }

        private void CargaPesosyRendimiento()
        {
          
            pesoMP = SumaCantidadesMateriaPrima();
            pesoPt = SumaCantidadesProductoTerminado();
            txtPesoMP.Text = (decimal.Round(pesoMP, 2)).ToString();
            txtPesoPT.Text = (decimal.Round(pesoPt, 2)).ToString();

            if (pesoPt != 0)
                txtRendimiento.Text = (decimal.Round((pesoPt / pesoMP) * 100, 2)).ToString();
            

        }

        private void CargaCostoPesosSoles()
        {
            /*
            decimal tonProTer = Convert.ToDecimal(txtPesoPT.Text) / 1000;
            if (tonProTer != 0)
            {
                txtCoToMPSoles.Text = (decimal.Round((Convert.ToDecimal(txtCostoMP.Text) / tonProTer), 2)).ToString();
                txtCoToInSoles.Text = (decimal.Round((Convert.ToDecimal(txtCostoInsumo.Text) / tonProTer), 2)).ToString();
                txtCoToMoSoles.Text = (decimal.Round((Convert.ToDecimal(txtCostoMO.Text) / tonProTer), 2)).ToString();
                txtCoToSuSoles.Text = (decimal.Round((Convert.ToDecimal(txtCostoSuministro.Text) / tonProTer), 2)).ToString();
                txtCoToSoles.Text = (Convert.ToDecimal(txtCoToMPSoles.Text) + Convert.ToDecimal(txtCoToInSoles.Text) + Convert.ToDecimal(txtCoToMoSoles.Text) + Convert.ToDecimal(txtCoToSuSoles.Text)).ToString();
            }
            */
        }


        private void CargaCostoPesosDolares()
        {
            /*
            clsTipoCambio tipoCambio = admTipoCambio.CargaTipoCambio(DateTime.Now.Date, 2);
            decimal tonProTer = Convert.ToDecimal(txtPesoPT.Text) / 1000;
            if (tonProTer != 0)
            {
                txtCoToMPDolares.Text = (decimal.Round(((Convert.ToDecimal(txtCostoMP.Text) / Convert.ToDecimal(tipoCambio.Venta)) / tonProTer), 2)).ToString();
                txtCoToInDolares.Text = (decimal.Round(((Convert.ToDecimal(txtCostoInsumo.Text) / Convert.ToDecimal(tipoCambio.Venta)) / tonProTer), 2)).ToString();
                txtCoToMoDolares.Text = (decimal.Round(((Convert.ToDecimal(txtCostoMO.Text) / Convert.ToDecimal(tipoCambio.Venta)) / tonProTer), 2)).ToString();
                txtCoToSuDolares.Text = (decimal.Round(((Convert.ToDecimal(txtCostoSuministro.Text) / Convert.ToDecimal(tipoCambio.Venta)) / tonProTer), 2)).ToString();
                txtCoToDolares.Text = (Convert.ToDecimal(txtCoToMPDolares.Text) + Convert.ToDecimal(txtCoToInDolares.Text) + Convert.ToDecimal(txtCoToMoDolares.Text) + Convert.ToDecimal(txtCoToSuDolares.Text)).ToString();
            }
            */
        }
        private void tabMateria_Click(object sender, EventArgs e)
        {

        }

        private void tabInsumo_Click(object sender, EventArgs e)
        {

        }

      
        private void CargaCostos()
        {
            /*
            txtCostoMP.Text = txtTotal.Text;
            txtCostoInsumo.Text = txtTotalInsumo.Text;
            txtCostoSuministro.Text = txtTotalSuministro.Text;
            txtCostoMO.Text = "0";
            txtCostoTotal.Text = (Convert.ToDecimal(txtTotal.Text) + Convert.ToDecimal(txtTotalInsumo.Text) + Convert.ToDecimal(txtTotalSuministro.Text)).ToString();
            */
        }



        private void tabSuministro_Click(object sender, EventArgs e)
        {

        }

        private void tabProducto_Click(object sender, EventArgs e)
        {

        }

        private void AbrirLote()
        {

            if (admLoteProduccion.ActualizarEstado(loteProduccion.CodLoteProduccion, 1))
            {
                MessageBox.Show("Lote Abierto Correctamente", "Lote Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
            }
            else
            {
                MessageBox.Show("Problemas al Abrir lote", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private decimal SumaCantidadesProductoTerminado()
           
        {
            decimal suma = 0;
            for (int i = 0; i < dgvProductoTerminado.RowCount; i++)
            {
              //  suma = suma + Convert.ToDecimal(dgvProductoTerminado.Rows[i].Cells[pesoneto.Name].Value) * Convert.ToDecimal(dgvProductoTerminado.Rows[i].Cells[cantidad1.Name].Value);
                suma = suma + Convert.ToDecimal(dgvProductoTerminado.Rows[i].Cells[pesoneto.Name].Value) ;
            }
            return suma;
           
       
        }
        private decimal SumaCantidadesMateriaPrima()
        {
            decimal suma = 0;
            for (int i = 0; i < dgvMateriaPrima.RowCount; i++)
            {
                suma = suma + Convert.ToDecimal(dgvMateriaPrima.Rows[i].Cells[cantidad.Name].Value);

            }
            return suma;

        }




        private decimal SumaCantidadesIngresoReproceso()
        {

            

            decimal suma = 0;
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                suma = suma + Convert.ToDecimal(dataGridView2.Rows[i].Cells[dataGridViewTextBoxColumn18.Name].Value);

            }
            return suma;

        }


        private decimal SumaCantidadesSalidaReproceso()
        {
            decimal suma = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                suma = suma + Convert.ToDecimal(dataGridView1.Rows[i].Cells[dataGridViewTextBoxColumn11.Name].Value);

            }
            return suma;

        }







        private void button1_Click(object sender, EventArgs e)
        {
            clsReporteFrutosa ds = new clsReporteFrutosa();
            CRNotaEntregaRecepcion rpt = new CRNotaEntregaRecepcion();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds.getNotaEntrega(CodLoteProduccion).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {/*
            clsReporteFrutosa ds = new clsReporteFrutosa();
            CRListaRecepcionNotaIngreso rpt = new CRListaRecepcionNotaIngreso();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds.getTicketIngresoContenedor(CodIngresoCamara).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
            */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //CRParteProduccion
            clsReporteFrutosa ds = new clsReporteFrutosa();
            CRParteProduccion rpt = new CRParteProduccion();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            //txtRendimiento
            rpt.SetDataSource(ds.getReporteParteDiario(frmLogin.iCodEmpresa, codProducto, CodLoteProduccion, codMatprima,  Convert.ToDecimal(txtRendimiento.Text)).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
        }

        private void tabControlPanel8_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
               dgvProductoTerminado.DataSource = admLoteProduccion.CargaProductoTerminadoXlote(loteProduccion.CodLoteProduccion,1);
            else
                dgvProductoTerminado.DataSource = admLoteProduccion.CargaProductoTerminadoXlote(loteProduccion.CodLoteProduccion,2);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            clsReporteFrutosa ds = new clsReporteFrutosa();
            CRListaRecepcionNotaIngreso rpt = new CRListaRecepcionNotaIngreso();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
      //      rpt.SetDataSource(ds.getTicketIngresoContenedor(CodIngresoCamara, frmLogin.iCodAlmacen, 2).Tables[0]);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
        }

        private void btnadd_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
        
    }
}
