using BancoBackend.dominio;
using BancoFrotend.client;
using Newtonsoft.Json;
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

namespace BancoFrotend
{
    public partial class FormLogin : Form
    {
        private Cliente c;
        public FormLogin()
        {
            InitializeComponent();
            c = new Cliente();
        }

     
        private async Task Login()
        {
            
            int nro = int.Parse(txtUsuario.Text);
            string contra = txtContrasenia.Text;
            string url = "https://localhost:44342/Banco/login?nro_cliente=" + nro+"&contrasenia="+contra;
            
            using (var client = new HttpClient())
            {
                var resultado = await client.PostAsync(url, null);
                // PARA LEER SOLO EL STRING
                var json = await resultado.Content.ReadAsStringAsync();

                c = JsonConvert.DeserializeObject<Cliente>(json);


                
            }


            //string text = JsonConvert.DeserializeObject(resultado);



        }

        private async void btnIniciarSesion_Click_1(object sender, EventArgs e)
        {
            await Login();
            if (c.NroCliente.Equals(0))
            {
                MessageBox.Show("CREDENCIALES INCORRECTAS");
                return;
            }


            if (c.EsAdmin)
            {
                this.Hide();
                FormAdmin admin = new FormAdmin();
                admin.ShowDialog();

            }
            else
            {

                FormCliente formCliente = new FormCliente(c);
                formCliente.ShowDialog();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir de la aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
