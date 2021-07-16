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

    public partial class frmCostos : DevComponents.DotNetBar.OfficeForm    
    {

        clsTipoCambio tc = new clsTipoCambio();
        clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();
        clsAdmProducto Admprducto = new clsAdmProducto();
        DataTable dt_pro;
        bool logrado = false;

        Int32 NroArticulos;


        clsLoteProduccion lotes = new clsLoteProduccion();
        clsAdmLoteProduccion admLotes = new clsAdmLoteProduccion();
        public Int32 Proceso = 0; //(1)Eliminar (2)Editar (3)Consulta (4)Captura de datos
        public Int32 CodLote = 0;


        public bool ejecuta = false;
        decimal sumavertical = 0;
        decimal sumahorizontal = 0;


        decimal summatprima = 0;
        decimal sumprodterm = 0;
        decimal sumrendimiento = 0;
        decimal sumproporcion = 0;


        public static BindingSource data = new BindingSource();
        String filtro = String.Empty;
        public List<Int32> tipoarticulos = new List<Int32>();
        int cuenta = 0;
        decimal valor;
        public frmCostos()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        public void inicial()
        {


            //----------------- dataGridView2 ----------------------------------

            //Llenado de Columnas
            dataGridView2.Columns.Add("CONCEPTO", "CONCEPTO");
            dataGridView2.Columns[0].Frozen = true;
            dataGridView2.Columns[0].DefaultCellStyle.BackColor = Color.LightSalmon;
            dataGridView2.Columns[0].Width = 200;
            

            dataGridView2.Columns.Add("TOTAL", "TOTAL");
            dataGridView2.Columns[1].Frozen = true;
            dataGridView2.Columns[1].DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView2.Columns[1].Width = 100;

            dt_pro = Admprducto.ListaProductosxArticulo(8);
            this.NroArticulos = dt_pro.Rows.Count;
            foreach (DataRow row in dt_pro.Rows)
            {
                dataGridView2.Columns.Add(row["descripcion"].ToString(), row["descripcion"].ToString());
                tipoarticulos.Add(Int32.Parse(row["codProducto"].ToString()));
            }



            //Llenado de Filas
            dataGridView2.Rows.Add("MATERIA PRIMA (KG)");
            dataGridView2.Rows.Add("PRODUCTO TERMINADO (KG)");
            dataGridView2.Rows.Add("RENDIMIENTO %");
            dataGridView2.Rows.Add("PROPORCION %");
     //       dataGridView2.Rows[3].Visible = false;


            //----------------- dataGridView 1 ----------------------------------

            int j = 2;
            dataGridView1.ReadOnly = false;
            //Columns

            dataGridView1.Columns.Add("CONCEPTO", "CONCEPTO");
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.LightSalmon;
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[0].ReadOnly = true;


            dataGridView1.Columns.Add("TOTAL", "TOTAL");
            dataGridView1.Columns[1].Frozen = true;
            dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.GreenYellow;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].ReadOnly = false;

            dt_pro = Admprducto.ListaProductosxArticulo(8);
            foreach (DataRow row in dt_pro.Rows)
            {
                dataGridView1.Columns.Add(row["descripcion"].ToString(), row["descripcion"].ToString());
                dataGridView1.Columns[j].ReadOnly = true;
                j++;

            }

            //Rows 13 conceptos
            dataGridView1.Rows.Add("MATERIA PRIMA");
            dataGridView1.Rows.Add("INSUMOS");
            dataGridView1.Rows.Add("SUMINISTROS");
            dataGridView1.Rows.Add("MANO OBRA DIRECTA");
            dataGridView1.Rows.Add("MANO OBRA INDIRECTA");
            dataGridView1.Rows.Add("TRASPORTE Y MOVILIDAD");
            dataGridView1.Rows.Add("ENERGIA");
            dataGridView1.Rows.Add("MANTENIMIENTO");
            dataGridView1.Rows.Add("AGUA");
            dataGridView1.Rows.Add("GASTOS DIVERSOS PRODUCCION");
            dataGridView1.Rows.Add("SANEAMIENTO");
            dataGridView1.Rows.Add("DEPRESIACION");
            dataGridView1.Rows.Add("TOTALES");

            dataGridView1.Rows[12].DefaultCellStyle.BackColor = Color.Peru;


            //Columna Total ReadOnly
            dataGridView1.Rows[0].Cells[1].ReadOnly = true;//MateriaPrima 
            dataGridView1.Rows[1].Cells[1].ReadOnly = true;//Insumos
            dataGridView1.Rows[2].Cells[1].ReadOnly = true;//Suministros
            dataGridView1.Rows[12].Cells[1].ReadOnly = true;



            //----------------- dataGridView 3 ----------------------------------
     
            dataGridView3.ReadOnly = true;
            //Columns

            dataGridView3.Columns.Add("CONCEPTO", "CONCEPTO");
            dataGridView3.Columns[0].Frozen = true;
            dataGridView3.Columns[0].DefaultCellStyle.BackColor = Color.LightSalmon;
            dataGridView3.Columns[0].Width = 200;

            dataGridView3.Columns.Add("", "");
            dataGridView3.Columns[1].Frozen = true;
      //      dataGridView3.Columns[1].DefaultCellStyle.BackColor = Color.GreenYellow;
         //   dataGridView3.Columns[1].Width = 100;
            dataGridView3.Columns[1].Visible = true;


        //    dataGridView3.Columns.Add("", "");
         //   dataGridView3.Columns[2].Frozen = true;
      //      dataGridView3.Columns[2].DefaultCellStyle.BackColor = Color.GreenYellow;
      //      dataGridView3.Columns[2].Width = 100;
       //     dataGridView3.Columns[2].Visible = false;


            dt_pro = Admprducto.ListaProductosxArticulo(8);
            foreach (DataRow row in dt_pro.Rows)
                dataGridView3.Columns.Add(row["descripcion"].ToString(), row["descripcion"].ToString());

            

            //Rows
            dataGridView3.Rows.Add("MATERIA PRIMA");
            dataGridView3.Rows.Add("INSUMOS");
            dataGridView3.Rows.Add("SUMINISTROS");
            dataGridView3.Rows.Add("MANO OBRA DIRECTA");
            dataGridView3.Rows.Add("MANO OBRA INDIRECTA");
            dataGridView3.Rows.Add("TRASPORTE Y MOVILIDAD");
            dataGridView3.Rows.Add("ENERGIA");
            dataGridView3.Rows.Add("MANTENIMIENTO");
            dataGridView3.Rows.Add("AGUA");
            dataGridView3.Rows.Add("GASTOS DIVERSOS PRODUCCION");
            dataGridView3.Rows.Add("SANEAMIENTO");
            dataGridView3.Rows.Add("DEPRESIACION");
            dataGridView3.Rows.Add("TOTALES  S/.");
            dataGridView3.Rows.Add("TOTALES  $ ");

            dataGridView3.Rows[12].DefaultCellStyle.BackColor = Color.Peru;
            dataGridView3.Rows[13].DefaultCellStyle.BackColor = Color.Peru;

        }

        public void llenado()
        {
            //----------------- dataGridView2 ----------------------------------


            //Rows
            dataGridView2.Rows.Add("MATERIA PRIMA (KG)");
            dataGridView2.Rows.Add("PRODUCTO TERMINADO (KG)");
            dataGridView2.Rows.Add("RENDIMIENTO %");
            dataGridView2.Rows.Add("PROPORCION %");


            //----------------- dataGridView 1 ----------------------------------


            //Rows
            dataGridView1.Rows.Add("MATERIA PRIMA");
            dataGridView1.Rows.Add("INSUMOS");
            dataGridView1.Rows.Add("SUMINISTROS");
            dataGridView1.Rows.Add("MANO OBRA DIRECTA");
            dataGridView1.Rows.Add("MANO OBRA INDIRECTA");
            dataGridView1.Rows.Add("TRASPORTE Y MOVILIDAD");
            dataGridView1.Rows.Add("ENERGIA");
            dataGridView1.Rows.Add("MANTENIMIENTO");
            dataGridView1.Rows.Add("AGUA");
            dataGridView1.Rows.Add("GASTOS DIVERSOS PRODUCCION");
            dataGridView1.Rows.Add("SANEAMIENTO");
            dataGridView1.Rows.Add("DEPRESIACION");
            dataGridView1.Rows.Add("TOTALES");

            dataGridView1.Rows[12].DefaultCellStyle.BackColor = Color.Peru;


            //Columna Total ReadOnly
            dataGridView1.Rows[0].Cells[1].ReadOnly = true;
            dataGridView1.Rows[1].Cells[1].ReadOnly = true;
            dataGridView1.Rows[2].Cells[1].ReadOnly = true;
            dataGridView1.Rows[12].Cells[1].ReadOnly = true;


            //----------------- dataGridView 3 ----------------------------------


            //Rows
            dataGridView3.Rows.Add("MATERIA PRIMA");
            dataGridView3.Rows.Add("INSUMOS");
            dataGridView3.Rows.Add("SUMINISTROS");
            dataGridView3.Rows.Add("MANO OBRA DIRECTA");
            dataGridView3.Rows.Add("MANO OBRA INDIRECTA");
            dataGridView3.Rows.Add("TRASPORTE Y MOVILIDAD");
            dataGridView3.Rows.Add("ENERGIA");
            dataGridView3.Rows.Add("MANTENIMIENTO");
            dataGridView3.Rows.Add("AGUA");
            dataGridView3.Rows.Add("GASTOS DIVERSOS PRODUCCION");
            dataGridView3.Rows.Add("SANEAMIENTO");
            dataGridView3.Rows.Add("DEPRESIACION");
            dataGridView3.Rows.Add("TOTALES  S/.");
            dataGridView3.Rows.Add("TOTALES  $ ");

            dataGridView3.Rows[12].DefaultCellStyle.BackColor = Color.Peru;
            dataGridView3.Rows[13].DefaultCellStyle.BackColor = Color.Peru;
        }

        public void limpia()
        {
             sumavertical = 0;
             sumahorizontal = 0;
             summatprima = 0;
             sumprodterm = 0;
             sumrendimiento = 0;
             sumproporcion = 0;
             cuenta = 0;
             ejecuta = false;

        }

        public void limpia2()
        {
            sumavertical = 0;
            sumahorizontal = 0;
            summatprima = 0;
            sumprodterm = 0;
            sumrendimiento = 0;
            sumproporcion = 0;
            cuenta = 0;

        }

        private void LlenarColumnasRendimiento()
        {
       

           
          
            try
            {

                //Materia Prima
                foreach (int x in tipoarticulos)//productos
                {

                   valor = admLotes.GetRendimientoCosto(dtpDesde.Value, dtpHasta.Value, x, 1);
                   dataGridView2.Rows[0].Cells[cuenta + 2].Value = String.Format("{0:#,##0.00}", valor); 
                   summatprima = summatprima + valor;

                   cuenta++;
                   if (cuenta == tipoarticulos.Count)
                   {
                       cuenta = 0;
                       break;
                   }
                }


                //Valida Materia Prima
                if (summatprima == 0)
                    return;




                //Producto Terminado
                foreach (int x in tipoarticulos)//productos
                {
                    valor = admLotes.GetRendimientoCosto(dtpDesde.Value, dtpHasta.Value, x, 2);
                    dataGridView2.Rows[1].Cells[cuenta + 2].Value = String.Format("{0:#,##0.00}", valor);
                    sumprodterm = sumprodterm + valor;

                    cuenta++;
                    if (cuenta == tipoarticulos.Count)
                    {
                        cuenta = 0;
                        break;
                    }
                }

                //Valida Producto Terminado
                if (sumprodterm == 0)
                    return;
                


              
                //Rendimiento
                foreach (int x in tipoarticulos)
                {

                    if (Convert.ToDecimal(dataGridView2.Rows[0].Cells[cuenta + 2].Value) != 0)//Cant Materia prima no debe ser 0
                        dataGridView2.Rows[2].Cells[cuenta + 2].Value = decimal.Round(   (Convert.ToDecimal(dataGridView2.Rows[1].Cells[cuenta + 2].Value) / Convert.ToDecimal(dataGridView2.Rows[0].Cells[cuenta + 2].Value)   ) * 100,2);
                    else
                        dataGridView2.Rows[2].Cells[cuenta +2].Value = 0;


                    sumrendimiento = sumrendimiento + Convert.ToDecimal(dataGridView2.Rows[2].Cells[cuenta + 2].Value);
                    cuenta++;
                    if (cuenta == tipoarticulos.Count)
                    {
                        cuenta = 0;
                        break;
                    }
                }

                //Proporcion
                foreach (int x in tipoarticulos)
                {
                    dataGridView2.Rows[3].Cells[cuenta + 2].Value = decimal.Round( Convert.ToDecimal(dataGridView2.Rows[1].Cells[cuenta + 2].Value) /sumprodterm,4)*100;
                    sumproporcion = sumproporcion + Convert.ToDecimal(dataGridView2.Rows[1].Cells[cuenta + 2].Value) / sumprodterm;
                    
                    cuenta++;
                    if (cuenta == tipoarticulos.Count)
                    {
                        cuenta = 0;
                        break;
                    }
                }

               
                /*
                dataGridView2.Rows[0].Cells[1].Value = decimal.Round(summatprima, 2);
                dataGridView2.Rows[1].Cells[1].Value = decimal.Round(sumprodterm,2);
                dataGridView2.Rows[2].Cells[1].Value = decimal.Round(sumrendimiento, 2); ;
                dataGridView2.Rows[3].Cells[1].Value = decimal.Round(sumproporcion, 2);
                */
                dataGridView2.Rows[0].Cells[1].Value = String.Format("{0:#,##0.00}", summatprima);
                dataGridView2.Rows[1].Cells[1].Value = String.Format("{0:#,##0.00}", sumprodterm);

              //  dataGridView2.Rows[2].Cells[1].Value = decimal.Round(sumrendimiento, 2); 



                //Calculo Rendimiento Total

                dataGridView2.Rows[2].Cells[1].Value = decimal.Round(Convert.ToDecimal(dataGridView2.Rows[1].Cells[1].Value) / Convert.ToDecimal(dataGridView2.Rows[0].Cells[1].Value), 2) * 100;



              //  dataGridView2.Rows[2].Cells[1].Value = decimal.Round(1, 2); 
                dataGridView2.Rows[3].Cells[1].Value = decimal.Round(sumproporcion, 2)*100;



              



            }
            catch (Exception ex)
            {
                return;
               // MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }


        private void LlenarColumnasCostos()
        {

            cuenta = 0;
            try
            {

                //Materia Prima

                foreach (int x in tipoarticulos)//productos
                {
                   
                    valor = admLotes.GetRendimientoCosto(dtpDesde.Value, dtpHasta.Value, x, 3);
                    dataGridView1.Rows[0].Cells[cuenta + 2].Value = String.Format("{0:#,##0.00}", valor);
                    sumahorizontal = sumahorizontal + valor;                  
                    
                    cuenta++;
                    if (cuenta == NroArticulos)
                    {
                        cuenta = 0;
                        dataGridView1.Rows[0].Cells[cuenta + 1].Value = String.Format("{0:#,##0.00}", sumahorizontal);
                        sumahorizontal = 0;
                        break;
                    }
                }


                //Insumos
                foreach (int x in tipoarticulos)//productos
                {
                    valor = admLotes.GetRendimientoCosto(dtpDesde.Value, dtpHasta.Value, x, 5);
                    dataGridView1.Rows[1].Cells[cuenta + 2].Value = String.Format("{0:#,##0.00}", valor);
                    sumahorizontal = sumahorizontal + valor;     
                    
                    cuenta++;
                    if (cuenta == tipoarticulos.Count)
                    {
                        cuenta = 0;
                        dataGridView1.Rows[1].Cells[cuenta + 1].Value = String.Format("{0:#,##0.00}", sumahorizontal); 
                        sumahorizontal = 0;
                        break;
                    }
                }


                //Suministros
                foreach (int x in tipoarticulos)//productos
                {

                    valor = admLotes.GetRendimientoCosto(dtpDesde.Value, dtpHasta.Value, x, 4);
                    dataGridView1.Rows[2].Cells[cuenta + 2].Value = String.Format("{0:#,##0.00}", valor);
                    sumahorizontal = sumahorizontal + valor;     
                    
                    cuenta++;
                    if (cuenta == tipoarticulos.Count)
                    {
                        cuenta = 0;
                        dataGridView1.Rows[2].Cells[cuenta + 1].Value = String.Format("{0:#,##0.00}", sumahorizontal);
                        sumahorizontal = 0;
                        break;
                    }
                }


                
                //Suma Vertical
                for (int h = 1; h < tipoarticulos.Count + 2; h++) //productos
                {

                    for (int i = 0; i < 12; i++) //conceptos
                    {
                        if (dataGridView1.Rows[i].Cells[h].Value == null)
                            valor = 0;
                        else
                            valor = Convert.ToDecimal(dataGridView1.Rows[i].Cells[h].Value);
                        

                        sumavertical = sumavertical + valor;

                    }

                //    dataGridView1.Rows[11].Cells[h].Value = Decimal.Round(sumavertical, 2);
                    dataGridView1.Rows[12].Cells[h].Value = String.Format("{0:#,##0.00}", sumavertical);
                    sumavertical = 0;
                }
                


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }

        private void LlenarColumnasCostosTM()
        {

            decimal cantprodterm=0;
            decimal valorconcepto = 0;
            decimal valortm = 0;

            sumahorizontal = 0;
            sumavertical = 0;
            cuenta = 2;


            //Calcula Costo TM  -   Suma Horizontales
            for (int i=0; i < 12; i++) {//conceptos
                for (int h = 2; h < tipoarticulos.Count + 2; h++) //productos
                {


                        if (dataGridView2.Rows[1].Cells[h].Value == null)
                            cantprodterm = 0;
                        else
                            cantprodterm = Convert.ToDecimal(dataGridView2.Rows[1].Cells[h].Value)/1000;


                        if (cantprodterm != 0)
                        {
                            valorconcepto = Convert.ToDecimal(dataGridView1.Rows[i].Cells[h].Value);
                            valortm = Decimal.Round(valorconcepto / cantprodterm, 2);
                        }
                        else
                        {
                            valortm = 0;
                        }

                       cuenta++;

                        sumahorizontal = sumahorizontal + valortm;
                   //     dataGridView3.Rows[i].Cells[h].Value = valortm;
                        dataGridView3.Rows[i].Cells[h].Value = String.Format("{0:#,##0.00}", valortm);

                     }

         //       dataGridView3.Rows[i].Cells[1].Value = sumahorizontal;
                dataGridView3.Rows[i].Cells[1].Value = String.Format("{0:#,##0.00}", sumahorizontal);
                sumahorizontal = 0;

           }






            

            //Suma Verticales
            for (int h = 1; h < tipoarticulos.Count + 2; h++) //productos
            {

                for (int i = 0; i < 12; i++) //conceptos
                {
                    if (dataGridView3.Rows[i].Cells[h].Value == null)
                        valor = 0;
                    else
                        valor = Convert.ToDecimal(dataGridView3.Rows[i].Cells[h].Value);


                    sumavertical = sumavertical + valor;

                }


            //    dataGridView3.Rows[11].Cells[h].Value = Decimal.Round(sumavertical, 2);
                dataGridView3.Rows[12].Cells[h].Value = String.Format("{0:#,##0.00}", sumavertical);



                //Tipo Cambio
             //   dataGridView3.Rows[12].Cells[h].Value = Decimal.Round( sumavertical/ (decimal)tc.Venta, 2);
                dataGridView3.Rows[13].Cells[h].Value = String.Format("{0:#,##0.00}", sumavertical / (decimal)tc.Venta); 
                sumavertical = 0;
            }



            for (int i = 0; i < 14; i++)
                dataGridView3.Rows[i].Cells[1].Value = "";

    
        }

        private void CargaLista()
        {
            //int estao = 0;
            //if (Convert.ToInt32(cmbEstado.SelectedIndex) == 0)
            //{
            //    estao = 2;
            //}
                dgvLotes.DataSource = data;
                data.DataSource = admLotes.CargaLoteProduccionPorEstado(Convert.ToInt32(cmbEstado.SelectedIndex), dtpDesde.Value, dtpHasta.Value);//camaras en espera de la empresa logeada
                data.Filter = String.Empty;
                filtro = String.Empty;
                dgvLotes.ClearSelection();

            
        }

        private void btnIrPedido_Click(object sender, EventArgs e)
        {            
            if (dgvLotes.Rows.Count >= 1 && dgvLotes.CurrentRow != null)
            {
                DataGridViewRow row = dgvLotes.CurrentRow;
                frmLoteProduccion form = new frmLoteProduccion();
                form.MdiParent = this.MdiParent;
                form.CodLoteProduccion = lotes.CodLoteProduccion;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void frmPedidosPendientes_Load(object sender, EventArgs e)
        {
            dtpDesde.Value = new DateTime(dtpDesde.Value.Year, dtpDesde.Value.Month, 1);

          //  inicial();


            tc = AdmTc.CargaTipoCambio(dtpHasta.Value.Date, 2);
            if (tc == null)
            {
                MessageBox.Show("Debe registrar el tipo de cambio del día", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                txtTipoCambio.Text = tc.Venta.ToString();
                inicial();
            }
            
           


        }

        
        private void btnAnular_Click(object sender, EventArgs e)
        {
            if (dgvLotes.CurrentRow != null && lotes.CodLoteProduccion != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el lote seleccionado", "Lotes de Produccion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admLotes.Delete(lotes.CodLoteProduccion))
                    {
                        MessageBox.Show("El lote se ha anulado correctamente", "Lotes de Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
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

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFiltro.Text.Length >= 2)
                {
                    data.Filter = String.Format("[{0}] like '*{1}*'", label6.Text.Trim(), txtFiltro.Text.Trim());
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

        private void btnReporte_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("ListaLotesProduccion");
            // Columnas
            foreach (DataGridViewColumn column in dgvLotes.Columns)
            {
                DataColumn dc = new DataColumn(column.Name.ToString());
                dt.Columns.Add(dc);
            }
            // Datos
            for (int i = 0; i < dgvLotes.Rows.Count; i++)
            {
                DataGridViewRow row = dgvLotes.Rows[i];
                DataRow dr = dt.NewRow();
                for (int j = 0; j < dgvLotes.Columns.Count; j++)
                {
                    dr[j] = (row.Cells[j].Value == null) ? "" : row.Cells[j].Value.ToString();
                }
                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);
            ds.WriteXml("C:\\XML\\ListaLotes.xml", XmlWriteMode.WriteSchema);


            CRListaLotes rpt = new CRListaLotes();
            frmListaIngresosCamara frm = new frmListaIngresosCamara();
            rpt.SetDataSource(ds);
            frm.crvIngresosCamara.ReportSource = rpt;
            frm.Show();
        }

        private void dgvIngresosCamara_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            label7.Text = dgvLotes.Columns[e.ColumnIndex].HeaderText;
            label6.Text = dgvLotes.Columns[e.ColumnIndex].DataPropertyName;
        }

        private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
        {
          //  CargaLista();
        }

        private void dtpHasta_ValueChanged_1(object sender, EventArgs e)
        {

            tc = AdmTc.CargaTipoCambio(dtpHasta.Value.Date, 2);
            if (tc != null)
            {
                txtTipoCambio.Text = tc.Venta.ToString();
            }
            else
            {
                MessageBox.Show("No existe tipo de cambio registrado en esta fecha", "Tipo de Cambio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dtpHasta.Value = DateTime.Now.Date;
                dtpHasta.Focus();
            }




         //   CargaLista();
        }

        private void dgvIngresosCamara_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && e.Row.Selected)
            {
                lotes.CodLoteProduccion = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
            }
        }

        private void dgvIngresosCamara_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Proceso != 4)
            {
                if (dgvLotes.Rows.Count >= 1 && e.RowIndex != -1)
                {
                    frmLoteProduccion form = new frmLoteProduccion();
                    form.MdiParent = this.MdiParent;
                    form.CodLoteProduccion = lotes.CodLoteProduccion;
                    form.Proceso = 3;
                    form.Show();
                }
            }
            else 
            {
                CodLote = lotes.CodLoteProduccion;
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && dgvLotes.CurrentRow != null)
            {
                DataGridViewRow row = dgvLotes.CurrentRow;
                frmLoteProduccion form = new frmLoteProduccion();
                form.MdiParent = this.MdiParent;
                form.CodLoteProduccion = lotes.CodLoteProduccion;
                form.Proceso = 3;
                form.Show();
            }
        }

        private void dgvIngresosCamara_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dgvLotes.ContextMenuStrip = new ContextMenuStrip();
            if (e.RowIndex != -1)
            {
                dgvLotes.Rows[e.RowIndex].Selected = true;
                if (e.Button == MouseButtons.Right && e.RowIndex != -1)
                {
                    if (dgvLotes.SelectedCells.Count > 0)
                    {
                        dgvLotes.ContextMenuStrip = contextMenuStrip1;                        
                    }
                }
            }
        }

        private void dgvIngresosCamara_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && e.RowIndex != -1 && dgvLotes.CurrentRow.Index == e.RowIndex)
            {
                DataGridViewRow Row = dgvLotes.Rows[e.RowIndex];
                lotes.CodLoteProduccion = Convert.ToInt32(Row.Cells[codigo.Name].Value); 
            }
        }

        private void frmIngresosCamara_Shown(object sender, EventArgs e)
        {
            CargaLista();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLotes.Rows.Count >= 1 && dgvLotes.CurrentRow != null)
            {
                DataGridViewRow row = dgvLotes.CurrentRow;
                frmLoteProduccion form = new frmLoteProduccion();
                form.MdiParent = this.MdiParent;
                form.CodLoteProduccion = lotes.CodLoteProduccion;
                form.Proceso = 2;
                form.Show();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvLotes.CurrentRow != null && lotes.CodLoteProduccion != 0)
            {
                DialogResult dlgResult = MessageBox.Show("Esta seguro que desea anular el lote seleccionado", "Lotes de Produccion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlgResult == DialogResult.No)
                {
                    return;
                }
                else
                {
                    if (admLotes.Delete(lotes.CodLoteProduccion))
                    {
                        MessageBox.Show("El Lote se ha anulado correctamente", "Lotes de Produccion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargaLista();
                    }
                }
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargaLista();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


            LlenarColumnasRendimiento();
            LlenarColumnasCostos();
            ejecuta = true;


          //  dataGridView2.Rows[3].Visible = true;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {


            

            if (ejecuta == false || logrado == true)
                return;

            

            if (dataGridView1.Columns[1].Name == "TOTAL")
            {


                try
                {
                    while (cuenta < NroArticulos)
                    {
                        //CurrentRow
                   //     dataGridView1.CurrentRow.Cells[cuenta + 2].Value =   Decimal.Round(   Convert.ToDecimal(dataGridView1.CurrentRow.Cells[1].Value) *  (Convert.ToDecimal(dataGridView2.Rows[3].Cells[cuenta + 2].Value) /100)    , 2  );
                        dataGridView1.CurrentRow.Cells[cuenta + 2].Value = String.Format("{0:#,##0.00}", Decimal.Round(Convert.ToDecimal(dataGridView1.CurrentRow.Cells[1].Value) * (Convert.ToDecimal(dataGridView2.Rows[3].Cells[cuenta + 2].Value) / 100), 2));
                        //dataGridView1.Rows[4].Cells[cuenta + 1].Value = 20;
                        cuenta++;

                        if (cuenta == NroArticulos)
                        {
                            cuenta = 0;
                            logrado = true;
                            break;
                        }
                    }

                    //Suma Vertical
                    sumavertical = 0;
                    for (int c = 1; c < NroArticulos + 2; c++) //productos
                    {

                        for (int i = 0; i < 12; i++) //conceptos
                        {
                            if (dataGridView1.Rows[i].Cells[c].Value == null)
                                valor = 0;
                            else
                                valor = Convert.ToDecimal(dataGridView1.Rows[i].Cells[c].Value);


                            sumavertical = sumavertical + valor;
                        }

                  //      dataGridView1.Rows[12].Cells[c].Value = Decimal.Round(sumavertical, 2);
                        dataGridView1.Rows[12].Cells[c].Value = String.Format("{0:#,##0.00}", Decimal.Round(sumavertical, 2));
                        sumavertical = 0;
                        valor = 0;
                    }

                    //Por Tonelada
                    cuenta = 0;
                    int posicion = dataGridView1.CurrentRow.Index;
                    while (cuenta < NroArticulos)
                    {
                        //CurrentRow


                        if (Convert.ToDecimal(dataGridView2.Rows[1].Cells[cuenta + 2].Value) == 0)
                            dataGridView3.Rows[posicion].Cells[cuenta + 2].Value = 0;
                        else
                            //  dataGridView3.Rows[posicion].Cells[cuenta + 2].Value = Decimal.Round(Convert.ToDecimal(dataGridView1.Rows[posicion].Cells[cuenta + 2].Value) / (Convert.ToDecimal(dataGridView2.Rows[1].Cells[cuenta + 2].Value) / 1000), 4);
                            dataGridView3.Rows[posicion].Cells[cuenta + 2].Value = String.Format("{0:#,##0.00}", Decimal.Round(Convert.ToDecimal(dataGridView1.Rows[posicion].Cells[cuenta + 2].Value) / (Convert.ToDecimal(dataGridView2.Rows[1].Cells[cuenta + 2].Value) / 1000), 4));
  
                        //dataGridView1.Rows[4].Cells[cuenta + 1].Value = 20;
                        cuenta++;
                        if (cuenta == NroArticulos)
                        {
                            cuenta = 0;
                            break;
                        }
                    }

                    //Suma Vertical Por Tonelada
                    sumavertical = 0;
                    for (int c = 2; c < NroArticulos + 2; c++) //productos
                    {

                        for (int i = 0; i < 12; i++) //conceptos
                        {
                            if (dataGridView3.Rows[i].Cells[c].Value == null)
                                valor = 0;
                            else
                                valor = Convert.ToDecimal(dataGridView3.Rows[i].Cells[c].Value);


                            sumavertical = sumavertical + valor;
                        }

                        dataGridView3.Rows[12].Cells[c].Value =   String.Format("{0:#,##0.00}", Decimal.Round(sumavertical, 2));
                        dataGridView3.Rows[13].Cells[c].Value = String.Format("{0:#,##0.00}", sumavertical / (decimal)tc.Venta); 
                        sumavertical = 0;
                        valor = 0;
                    }
                    logrado = false;
                    limpia2();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            limpia();
            LlenarColumnasRendimiento();

            if (summatprima == 0 || sumprodterm == 0)
            {
                MessageBox.Show("No se encontraron resultados");
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                llenado();
                return;

            }

            LlenarColumnasCostos();
            LlenarColumnasCostosTM();
            ejecuta = true;
            limpia2();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
