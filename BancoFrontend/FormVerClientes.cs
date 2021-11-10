using BancoBackend.dominio;
using BancoFrotend.client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BancoFrotend
{
    public partial class FormVerClientes : Form
    {
        Cliente c;
        public FormVerClientes()
        {
            
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await TraerUnCliente();
            FormABMCliente form = new FormABMCliente(c);
            form.ShowDialog();
        }

        private async Task TraerUnCliente()
        {
            int found = cboClientes.Text.IndexOf("-");
            string nro_cliente = cboClientes.Text.Substring(0, found);
            string url = "https://localhost:"+Properties.Resources.port+"/Banco/clientes/" + nro_cliente;
            var resultado = await ClienteSingleton.GetInstancia().GetAsync(url);
            c = JsonConvert.DeserializeObject<Cliente>(resultado);
        }


        private async void FormVerClientes_Load(object sender, EventArgs e)
        {
            await Cargar_ComboAsync();
        }

        private async Task Cargar_ComboAsync()
        {

            string url = "https://localhost:"+Properties.Resources.port+"/Banco/clientes";
            var resultado = await ClienteSingleton.GetInstancia().GetAsync(url);
            List<string> lst = JsonConvert.DeserializeObject<List<string>>(resultado);

            //source es una lista de objetos
            //cboClientes.DataSource = lst;
            //valueMember y DisplayMember serán las properties de los objetos

            //cboClientes.DisplayMember = "cliente";
            //cboClientes.ValueMember = "cliente";
            cboClientes.Items.AddRange(lst.ToArray());

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea volver?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }
    }
}
