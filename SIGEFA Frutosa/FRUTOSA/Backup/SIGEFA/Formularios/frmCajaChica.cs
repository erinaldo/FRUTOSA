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
    public partial class frmCajaChica : DevComponents.DotNetBar.OfficeForm
    {
        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        public List<Int32> seleccion = new List<Int32>();
        public List<clsCajaChica> seleccion2 = new List<clsCajaChica>();

        clsCajaChica Caja = new clsCajaChica();
        clsAdmCajaChica AdmCaja = new clsAdmCajaChica();
        
        
        private Decimal Saldo = 0;
        private Decimal Ingresos = 0;
        private Decimal Egresos = 0;

        int FilasChequeadas = 0;
        private Decimal MontoRendido = 0;






        public frmCajaChica()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void biActualizar_Click(object sender, EventArgs e)
        {
            VerificaSaldoCaja();
            ListaCajaChica();
        }

        private void ListaCajaChica()
        {
            dgvMovimientosCajaChica.DataSource = data;
            data.DataSource = AdmCaja.ListaCajaChica(frmLogin.iCodSucursal);
            data.Filter = String.Empty;
            filtro = String.Empty;
            FilasChequeadas = 0;
            MontoRendido = 0;
            seleccion.Clear();
            seleccion2.Clear();
            cboMovimientos.SelectedIndex = 0;
        }

        private void ListaCajaChicaFechas()
        {
            dgvMovimientosCajaChica.DataSource = data;
            data.DataSource = AdmCaja.ListaCajaChicaFechas(frmLogin.iCodAlmacen, dtpfecha1.Value.Date, dtpfecha2.Value.Date);
            data.Filter = String.Empty;
            filtro = String.Empty;
            dgvMovimientosCajaChica.ClearSelection();
        }
        private void VerificaSaldoCaja()
        {
            Saldo = 0;

            Caja = AdmCaja.VerificaSaldoCajaChica(frmLogin.iCodSucursal);
            if (Caja != null)
            {
                Saldo = Caja.MontoDisponible;
                lblIngresos.Text = String.Format("{0:#,##0.00}", Caja.MontoIngresado.ToString());
                lblEgresos.Text = String.Format("{0:#,##0.00}", Caja.MontoEntregado.ToString());
                lblAperturaCaja.Text = String.Format("{0:#,##0.00}", Caja.MontoApertura.ToString());
                lblSaldoCaja.Text = String.Format("{0:#,##0.00}", Caja.MontoDisponible.ToString());
            }
            else
            {
                Saldo = 0;
                lblIngresos.Text = "0.000";
                lblEgresos.Text = "0.000";
                lblAperturaCaja.Text = "0.000";
                lblSaldoCaja.Text = "0.000";
                //*****************************
                biIngreso.Enabled = false;
               biEgreso.Enabled = false;
            }

            if (Saldo > 0)
            {
               biIngreso.Enabled = true;
                biEgreso.Enabled = true;
                biAperturaCajachica.Enabled = false;
            }
            else
            {
                biAperturaCajachica.Enabled = true;
                //**********************************
                biIngreso.Enabled = false;
                biEgreso.Enabled = false;
            }


            //raga
            biIngreso.Enabled = true;
            biEgreso.Enabled = true;

        }

        private void frmCajaChica_Load(object sender, EventArgs e)
        {
            cboMovimientos.SelectedIndex = 0;
            VerificaSaldoCaja();
            ListaCajaChica();
        }

        private void biIngreso_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmCajaChicaRegistro"] != null)
            {
                Application.OpenForms["frmCajaChicaRegistro"].Activate();
            }
            else
            {
                frmCajaChicaRegistro form = new frmCajaChicaRegistro();
                form.Tipo = 1;
                form.Proceso = 1;
                form.ShowDialog();
                VerificaSaldoCaja();
                ListaCajaChica();
            }
        }

        private void biHistorialRendiciones_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmCajaChicaRendicionHistorial"] != null)
            {
                Application.OpenForms["frmCajaChicaRendicionHistorial"].Activate();
            }
            else
            {
                frmCajaChicaRendicionHistorial form = new frmCajaChicaRendicionHistorial();
                form.ShowDialog();
                ListaCajaChica();
            }
        }

        private void biEgreso_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmCajaChicaRegistro"] != null)
            {
                Application.OpenForms["frmCajaChicaRegistro"].Activate();
            }
            else
            {
                frmCajaChicaRegistro form = new frmCajaChicaRegistro();
                form.Tipo = 2;
                form.Proceso = 1;
                form.SaldoCaja = Convert.ToDecimal(lblSaldoCaja.Text.Trim());
                form.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
                form.ShowDialog();
                VerificaSaldoCaja();
                ListaCajaChica();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cboMovimientos.SelectedIndex = 0;
            //dtpfecha1.Value = System.DateTime.Now;
            //dtpfecha2.Value = System.DateTime.Now;
            ListaCajaChica();
        }

        private void dtpfecha1_Leave(object sender, EventArgs e)
        {
            dtpfecha2.MinDate = dtpfecha1.Value;
        }

        private void dtpfecha2_Leave(object sender, EventArgs e)
        {
            dtpfecha1.MaxDate = dtpfecha2.Value;
        }

        private void dtpfecha1_ValueChanged(object sender, EventArgs e)
        {
            ListaCajaChicaFechas();
        }

        private void dtpfecha2_ValueChanged(object sender, EventArgs e)
        {
            ListaCajaChicaFechas();
        }

        private void biEditar_Click(object sender, EventArgs e)
        {
            if (dgvMovimientosCajaChica.Rows.Count >0)
            {
                frmCajaChicaRegistro frm = new frmCajaChicaRegistro();
                if ((dgvMovimientosCajaChica.SelectedRows[0].Cells[tipoMovimiento.Name].Value.ToString()) == "INGRESO")
                { frm.Tipo = 1; }
                else if ((dgvMovimientosCajaChica.SelectedRows[0].Cells[tipoMovimiento.Name].Value.ToString()) == "EGRESO")
                { frm.Tipo = 2; }
                frm.Proceso = 2;
                //frm.Caja = Caja;
                frm.txtCodigo.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[codigo.Name].Value.ToString();
                frm.cboTipoPagoCaja.SelectedValue = Convert.ToInt32(dgvMovimientosCajaChica.SelectedRows[0].Cells[codTipoPagoCaja.Name].Value);
                frm.CodtipoCajaChica = Convert.ToInt32(dgvMovimientosCajaChica.SelectedRows[0].Cells[codTipoPagoCaja.Name].Value);
                frm.txtDescripcion.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[concepto.Name].Value.ToString();
                frm.txtDocumento.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[numDocumento.Name].Value.ToString();
                frm.txtMonto.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[monto.Name].Value.ToString();
                frm.dtpFecha.Value = Convert.ToDateTime(dgvMovimientosCajaChica.SelectedRows[0].Cells[fecha.Name].Value.ToString());
                frm.txtGuiaRemision.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[numGuia.Name].Value.ToString();
                frm.txtReciboLiquidacion.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[numRecLiquidacion.Name].Value.ToString();                
                Int32 TipoTarea = 0;
                if (dgvMovimientosCajaChica.SelectedRows[0].Cells[cargadescarga.Name].Value.ToString() == "")
                { frm.cboTipo.SelectedIndex = 0; }
                else if (dgvMovimientosCajaChica.SelectedRows[0].Cells[cargadescarga.Name].Value.ToString() == "CARGA")
                { frm.cboTipo.SelectedIndex = 1; }
                else if (dgvMovimientosCajaChica.SelectedRows[0].Cells[cargadescarga.Name].Value.ToString() == "DESCARGA")
                { frm.cboTipo.SelectedIndex = 2; }
                frm.txtToneladas.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[toneladas.Name].Value.ToString();
                frm.lblEgreso.Text = dgvMovimientosCajaChica.SelectedRows[0].Cells[monto.Name].Value.ToString();
                frm.lblSaldoCaja.Text = lblSaldoCaja.Text.Trim();
                frm.ShowDialog();
                ListaCajaChica();
                VerificaSaldoCaja();
            }
        }

        private void dgvMovimientosCajaChica_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvMovimientosCajaChica.Rows.Count >= 1 && e.Row.Selected)
            {
                if (e.Row.Cells[apertura.Name].Value.ToString() == "2")
                {
                    biEditar.Enabled = false;
                }
                else
                { biEditar.Enabled = true; }
                //*****************************************************
                if (e.Row.Cells[apertura.Name].Value.ToString() == "2")
                {

                }
            }
        }

        private void dgvMovimientosCajaChica_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                frmCajaChicaRegistro frm = new frmCajaChicaRegistro();
                if ((dgvMovimientosCajaChica.SelectedRows[0].Cells[tipoMovimiento.Name].Value.ToString()) == "INGRESO")
                { frm.Tipo = 1; }
                else if ((dgvMovimientosCajaChica.SelectedRows[0].Cells[tipoMovimiento.Name].Value.ToString()) == "EGRESO")
                { frm.Tipo = 2; }
                frm.Proceso = 3;
                //frm.Caja = Caja;
                frm.ShowDialog();
            }
        }

        private void biEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMovimientosCajaChica.Rows.Count > 0)
            {
                Caja.CodCajaChica = Convert.ToInt32(dgvMovimientosCajaChica.SelectedRows[0].Cells[codigo.Name].Value.ToString());
                if (Caja.CodCajaChica != 0)
                {
                    DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Familia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        if (AdmCaja.delete(Caja.CodCajaChica))
                        {
                            MessageBox.Show("El dato ha sido eliminado correctamente", "Caja Chica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            VerificaSaldoCaja();
                            ListaCajaChica();
                        }
                    }
                }
            }
        }

        private void CalculoSaldo()
        {
            try
            {
                Ingresos = 0;
                Egresos = 0;
                //Saldo = 0;

                foreach (DataGridViewRow row in dgvMovimientosCajaChica.Rows)
                {
                    if (row.Cells[tipoMovimiento.Name].Value.ToString() == "INGRESO")
                    {
                        Ingresos = Ingresos + (Convert.ToDecimal(row.Cells[monto.Name].Value));
                    }
                    else if (row.Cells[tipoMovimiento.Name].Value.ToString() == "EGRESO")
                    {
                        Egresos = Egresos + (Convert.ToDecimal(row.Cells[monto.Name].Value));
                    }
                }
                
                //lblAperturaCaja.Text = AperturaCaja.ToString();
                lblIngresos.Text = String.Format("{0:#,##0.00}", Ingresos.ToString());
                lblEgresos.Text = String.Format("{0:#,##0.00}", Egresos.ToString());
                Decimal SaldoCaja = ((Saldo + Ingresos) - Egresos);
                lblSaldoCaja.Text = String.Format("{0:#,##0.00}", SaldoCaja.ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        private void dgvMovimientosCajaChica_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //CalculoSaldo();
        }

        private void dgvMovimientosCajaChica_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            //CalculoSaldo();
        }

        private void biRendicionesContables_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmCajaChicaRendicion"] != null)
            {
                Application.OpenForms["frmCajaChicaRendicion"].Activate();
            }
            else
            {
                frmCajaChicaRendicion form = new frmCajaChicaRendicion();
                form.MdiParent = this.MdiParent;
                form.Show();
            }
        }

        private void biRencicionCaja_Click(object sender, EventArgs e)
        {
            if (dgvMovimientosCajaChica.RowCount > 0)
            {
                if (FilasChequeadas > 0)
                {
                    DialogResult dlgResult = MessageBox.Show("Desea Rendir Caja Chica", "Gestion de Caja Chica", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dlgResult == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        Caja.Monto = MontoRendido;
                        Caja.FechaRendicion = Convert.ToDateTime(System.DateTime.Now);
                        Caja.CodUser = frmLogin.iCodUser;
                        Caja.CodSucursal = frmLogin.iCodSucursal;
                        MontoRendido = 0;

                        if (Caja != null)
                        {
                            if (AdmCaja.InsertRendicion(Caja))
                            {                                
                                foreach (clsCajaChica clsCaja in seleccion2)
                                {
                                    clsCaja.CodRendicion = Caja.CodRendicion;
                                    AdmCaja.InsertDetalleRendicion(clsCaja);
                                }
                                seleccion2.Clear();
                            }
                        }
                        MessageBox.Show("Rendicion Generada! Puede Consultarla en 'Verificar rendiciones!'", "Gestion de Caja Chica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cboMovimientos.SelectedIndex = 0;
                        //ListaCajaChica(); 
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione Gastos Para Proseguir con la Rendicion de Caja Chica", "Gestion de Caja Chica", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void biVerificarRendicion_Click(object sender, EventArgs e)
        {
            frmCajaChicaRendicionListado frm = new frmCajaChicaRendicionListado();
            frm.ShowDialog();
            VerificaSaldoCaja();
            ListaCajaChica();
        }

        private void dgvMovimientosCajaChica_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMovimientosCajaChica.IsCurrentCellDirty)
            {
                dgvMovimientosCajaChica.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvMovimientosCajaChica_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (dgvMovimientosCajaChica.Columns[e.ColumnIndex].Name == Item.Name)
                {
                    DataGridViewRow row = dgvMovimientosCajaChica.Rows[e.RowIndex];

                    DataGridViewCheckBoxCell cellSelecion = row.Cells[Item.Name] as DataGridViewCheckBoxCell;

                    clsCajaChica CajaChica = new clsCajaChica();
                    if (Convert.ToBoolean(cellSelecion.Value))
                    {
                        FilasChequeadas = FilasChequeadas + 1;
                        MontoRendido = MontoRendido + (Convert.ToDecimal(row.Cells[monto.Name].Value));
                        seleccion.Add(Convert.ToInt32(row.Cells[codigo.Name].Value));
                        //*****************************************************************************
                        CajaChica.CodRendicion = Caja.CodRendicion;
                        CajaChica.CodCajaChica = Convert.ToInt32(row.Cells[codigo.Name].Value);
                        CajaChica.Monto = Convert.ToInt32(row.Cells[monto.Name].Value);
                        CajaChica.CodUser = frmLogin.iCodUser;
                        CajaChica.CodSucursal = frmLogin.iCodSucursal;
                        //*****************************************************************************                                                
                        seleccion2.Add(CajaChica);
                    }
                    else
                    {
                        FilasChequeadas = FilasChequeadas - 1;
                        MontoRendido = MontoRendido - (Convert.ToDecimal(row.Cells[monto.Name].Value));
                        seleccion.Remove(Convert.ToInt32(row.Cells[codigo.Name].Value));
                        //seleccion2.RemoveAll(CajaChica => CajaChica.CodCajaChica == Convert.ToInt32(row.Cells[codigo.Name].Value));
                    }

                    if (FilasChequeadas > 0)
                    { biRencicionCaja.Enabled = true; }
                    else
                    { biRencicionCaja.Enabled = false; }
                }
            }
        }

        private void biBuscar_Click(object sender, EventArgs e)
        {
            lblColumna.Text = "CODIGO";
            lblProperty.Text = "codPersonalizado"; 
            
            if (!expandablePanel1.Expanded)
            {
                expandablePanel1.Expanded = true;
                txtFiltro.Focus();
            }
            else
            {
                expandablePanel1.Expanded = false;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            expandablePanel1.Expanded = false;
        }

        private void dgvMovimientosCajaChica_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvMovimientosCajaChica.Columns[e.ColumnIndex].Index > 0)
            {
                lblColumna.Text = dgvMovimientosCajaChica.Columns[e.ColumnIndex].HeaderText;
                lblProperty.Text = dgvMovimientosCajaChica.Columns[e.ColumnIndex].DataPropertyName;

                if (expandablePanel1.Expanded)
                {
                    txtFiltro.Focus();
                }
            }
        }

        private void frmCajaChica_Shown(object sender, EventArgs e)
        {
            if (Saldo != 0)
            {
                //lblAperturaCaja.Text = Saldo.ToString();
                biIngreso.Enabled = true;
                biEgreso.Enabled = true;
            }
            else
            {
                lblAperturaCaja.Text = "0.000";
                biIngreso.Enabled = false;
                biEgreso.Enabled = false;
                //******************************
                if (Saldo == 0)
                {
                    //if (Application.OpenForms["frmCajaChicaRegistro"] != null)
                    //{
                    //    Application.OpenForms["frmCajaChicaRegistro"].Activate();
                    //}
                    //else
                    //{
                    //    frmCajaChicaRegistro form = new frmCajaChicaRegistro();
                    //    form.Tipo = 1;
                    //    form.Proceso = 1;
                    //    form.AperturaCaja = 1;
                    //    form.ShowDialog();
                    //    ListaCajaChica();
                    //    VerificaSaldoCaja();
                    //}
                    if (Application.OpenForms["frmRendicionesVigentes"] != null)
                    {
                        Application.OpenForms["frmRendicionesVigentes"].Activate();
                    }
                    else
                    {
                        frmRendicionesVigentes form = new frmRendicionesVigentes();
                        //form.Tipo = 1;
                        //form.Proceso = 1;
                        //form.AperturaCaja = 1;
                        form.ShowDialog();
                        ListaCajaChica();
                        VerificaSaldoCaja();
                    }
                }
            }
        }

        private void biAperturaCajachica_Click(object sender, EventArgs e)
        {
            if (Saldo == 0)
            {
                if (Application.OpenForms["frmCajaChicaRegistro"] != null)
                {
                    Application.OpenForms["frmCajaChicaRegistro"].Activate();
                }
                else
                {
                    frmCajaChicaRegistro form = new frmCajaChicaRegistro();
                    form.Tipo = 1;
                    form.Proceso = 1;
                    form.AperturaCaja = 1;
                    form.ShowDialog();
                    ListaCajaChica();
                    VerificaSaldoCaja();
                }
            }
        }

        private void cboMovimientos_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                MontoRendido = 0;
                biRencicionCaja.Enabled = false;
                seleccion.Clear();
                seleccion2.Clear();
                //************************************
                if (cboMovimientos.SelectedIndex == 0)
                {
                    Item.Visible = false;
                    ListaCajaChica();
                }
                else
                {
                    if (cboMovimientos.SelectedIndex == 2)
                    {
                        Item.Visible = true;
                    }
                    else
                    {
                        Item.Visible = false;
                    }
                    //************************************
                    if (cboMovimientos.Items.Count > 0)
                    {
                        data.Filter = String.Format("[{0}] like '*{1}*'", "TipoMovimiento", cboMovimientos.Text);
                    }
                    else
                    {
                        data.Filter = String.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void biRendicionLiquidada_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmRendicionesVigentes"] != null)
            {
                Application.OpenForms["frmRendicionesVigentes"].Activate();
            }
            else
            {
                frmRendicionesVigentes frm = new frmRendicionesVigentes();
                frm.ShowDialog();
                VerificaSaldoCaja();
                ListaCajaChica();
            }
        }
    }
}
