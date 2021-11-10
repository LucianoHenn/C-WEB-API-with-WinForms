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
    public partial class FormABMTipoCuenta : Form
    {
        

        public FormABMTipoCuenta()
        {
            InitializeComponent();
            Cargar_ComboAsync();

        }

        private async Task Cargar_ComboAsync()
        {

            string url = "https://localhost:44342/Banco/tipoCuenta";
            var resultado = await ClienteSingleton.GetInstancia().GetAsync(url);
            List<TipoCuenta> lst = JsonConvert.DeserializeObject<List<TipoCuenta>>(resultado);

            //source es una lista de objetos
            cboTipocuenta.DataSource = lst;
            //valueMember y DisplayMember serán las properties de los objetos

            cboTipocuenta.DisplayMember = "nombre";
            cboTipocuenta.ValueMember = "IdTipoCuenta";
            //cboTipocuenta.Items.AddRange(lst.ToString().ToArray());

        }

        private void FormABMTipoCuenta_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string myValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo tipo de cuenta", "CREAR TIPO DE CUENTA", "");
            await (crearTipoCuenta(myValue));
            this.Controls.Clear();
            this.InitializeComponent();
            Cargar_ComboAsync();
        }

        private async Task crearTipoCuenta(string myValue)
        {

            using (var client = new HttpClient())
            {
                string url = "https://localhost:" + @Properties.Resources.port + "/Banco/tipoCuenta/?tipo=" + myValue;

                
               
                StringContent content = new StringContent(myValue, Encoding.UTF8,
            "application/json");
                var result = await client.PostAsync(url, content);
               
                if (result.IsSuccessStatusCode)
                    MessageBox.Show("Exito, tipo de cuenta agregado correctamente");




            }

        }
        public async Task EliminarTipoCuenta()
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:" + @Properties.Resources.port + "/Banco/");

                //HTTP DELETE
                
                var deleteTask = client.DeleteAsync("tipoCuenta/"+cboTipocuenta.SelectedValue.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                MessageBox.Show("Exito, borrado correctamente");
            }
        }
        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await EliminarTipoCuenta();
            this.Controls.Clear();
            this.InitializeComponent();
            Cargar_ComboAsync();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea volver?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            await editarTipoCuenta();
        }

        private async Task editarTipoCuenta()
        {
            string myValue = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo tipo de cuenta", "EDITAR TIPO DE CUENTA", "");
            using (var client = new HttpClient())
            {
               
                client.BaseAddress = new Uri("https://localhost:" + @Properties.Resources.port + "/Banco/");
                StringContent content = new StringContent(myValue, Encoding.UTF8,
              "application/json");
               
                var response = client.PutAsync("tipoCuenta/"+ cboTipocuenta.SelectedValue.ToString() + "?tipo="+ myValue, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Exito");
                    this.Controls.Clear();
                    this.InitializeComponent();
                    Cargar_ComboAsync();
                }
                else
                    MessageBox.Show("Error");
            }
        }
    }
}
