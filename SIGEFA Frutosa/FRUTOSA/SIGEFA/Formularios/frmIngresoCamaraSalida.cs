using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIGEFA.Entidades;
using SIGEFA.Administradores;
namespace SIGEFA.Formularios
{
    public partial class frmIngresoCamaraSalida : DevComponents.DotNetBar.OfficeForm
    {

        public clsIngresoCamara ingreso = new clsIngresoCamara();
        clsAdmIngresoCamara admingreso = new clsAdmIngresoCamara();
        public frmIngresoCamaraSalida()
        {
            InitializeComponent();
        }

        private void ActualizarSalida() {
            try
            {
                if (maskedTextBox1.Text != "")
                {
                    ingreso = admingreso.CargaIngresoCamara(ingreso.CodIngresoCamara);
                    if (ingreso != null)
                    {
                        if (dateTimePicker1.Value >= ingreso.FechaIngreso)
                        {
                            ingreso.CodIngresoCamara = ingreso.CodIngresoCamara;
                            ingreso.FechaSalida = dateTimePicker1.Value;
                            ingreso.HoraSalida = maskedTextBox1.Text;
                            admingreso.UpdateSalida(ingreso);
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Fecha de Salida debe ser Mayor a Fecha de Ingreso", "Salida de Vehiculo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                    }
                }
                else {
                    MessageBox.Show("Ingrese Hora Salida","Salida de Vehiculos",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ex.Message.ToString());
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarSalida();
        }
    }
}
