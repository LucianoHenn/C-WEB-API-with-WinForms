using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BancoBackend.dominio;
using Microsoft.VisualBasic;

namespace BancoFrotend
{
    public partial class FormCliente : Form
    {
        private Cliente c;
        private bool transferenciaRealizada;
        public FormCliente(Cliente c)
        {
            InitializeComponent();
            this.c = c;
            lblCliente.Text = lblCliente.Text + " " + c.Apellido + ", " + c.Nombre;
            Dictionary<int, string> test = new Dictionary<int, string>();
            foreach(Cuenta cu in c.Cuentas)
            {
                test.Add(cu.TipoCuenta.IdTipoCuenta, cu.TipoCuenta.Nombre);
                cboCuentas.DataSource = new BindingSource(test, null);
                cboCuentas.DisplayMember = "Value";
                cboCuentas.ValueMember = "Key";

            }
          



        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void FormCliente_Load(object sender, EventArgs e)
        {

        }

        private void cboCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cboCuentas.SelectedIndex;
            txtSaldo.Text = c.Cuentas[index].Saldo.ToString();
            txtActividad.Text = c.Cuentas[index].UltimoMovimiento.ToString();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            transferenciaRealizada = false;
            double monto;
            string myValue  = Microsoft.VisualBasic.Interaction.InputBox("Cuanto dinero quieres depositar?", "DEPOSITO", "100");
            try
            {
                monto = double.Parse(myValue);
                if(monto<10)
                    MessageBox.Show("POR FAVOR INGRESE UN MONTO MAYOR A $10");
              
                else
                {
                    await transferir(monto);
                    c.Cuentas[cboCuentas.SelectedIndex].Saldo += monto; 
                    MessageBox.Show("EXITO!TRANSACCION REALIZADA CORRECTAMENTE");
                    txtSaldo.Text = c.Cuentas[cboCuentas.SelectedIndex].Saldo.ToString();
                    txtActividad.Text = DateTime.Now.ToString();
                }
                    
            }
            catch
            {
                MessageBox.Show("POR FAVOR INGRESE UN MONTO REAL");
            }
            
           
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            transferenciaRealizada = false;
            string myValue = Microsoft.VisualBasic.Interaction.InputBox("Cuanto dinero quieres extraer?", "EXTRACCION", "100");
            double monto = 0;

            try
            {
                monto -= double.Parse(myValue);
                if (monto > -10)
                    MessageBox.Show("POR FAVOR INGRESE UN MONTO MAYOR A $10");
                else if(-monto > c.Cuentas[cboCuentas.SelectedIndex].Saldo)
                    MessageBox.Show("ERROR! SALDO INSUFICIENTE");
                else
                {
                    await transferir(monto);
                    c.Cuentas[cboCuentas.SelectedIndex].Saldo += monto;
                    MessageBox.Show("EXITO!TRANSACCION REALIZADA CORRECTAMENTE");
                    txtSaldo.Text = c.Cuentas[cboCuentas.SelectedIndex].Saldo.ToString();
                    txtActividad.Text = DateTime.Now.ToString();
                }

            }
            catch
            {
                MessageBox.Show("POR FAVOR INGRESE UN MONTO REAL");
            }


        }

        private async Task transferir(double monto)
        {
            string url = "https://localhost:44342/Banco/cuentas/"+ c.Cuentas[cboCuentas.SelectedIndex].CBU+ "?monto="+monto;

            using (var client = new HttpClient())
            {
                var resultado = await client.PostAsync(url, null);
                // PARA LEER SOLO EL STRING
                var json = await resultado.Content.ReadAsStringAsync();

                transferenciaRealizada = json=="true" ? true : false ;



            }
        }

    }
}
