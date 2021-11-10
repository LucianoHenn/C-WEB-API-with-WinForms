using BancoBackend.dominio;
using BancoFrotend.client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace BancoFrotend
{
   
    public partial class FormABMCliente : Form
    {
         Cliente cliente;
        static HttpClient client = new HttpClient();

        public FormABMCliente()
        {
            InitializeComponent();
            cliente = new Cliente();
            Cargar_ComboAsync();
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        public  FormABMCliente(Cliente c)
        {
            InitializeComponent();
            cliente = new Cliente();
            cliente = c;
            Cargar_ComboAsync();
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
            btnGuardar.Enabled = false;
            txtApellido.Text = cliente.Apellido;
            txtNombre.Text = cliente.Nombre;
            txtContrasenia.Text = cliente.Contrasenia;
            txtDNI.Text = cliente.DNI;
            foreach(Cuenta cuenta in c.Cuentas){
                dataGridView1.Rows.Add(new object[] { cuenta.TipoCuenta, cuenta.CBU, cuenta.Saldo, cuenta.TipoCuenta.Nombre }); ;
            }

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
            cboTipocuenta.ValueMember = "id_tipo_cuenta";
            //cboTipocuenta.Items.AddRange(lst.ToString());

        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (ExisteProductoEnGrilla(cboTipocuenta.Text))
            {
                MessageBox.Show("Ya agregaste una cuenta de ese tipo", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            Cuenta cuenta = new Cuenta();

            TipoCuenta tipoCuenta = (TipoCuenta)cboTipocuenta.SelectedItem;
            cuenta.TipoCuenta = tipoCuenta;
            cuenta.CBU = txtCbu.Text;
            cuenta.Saldo = Convert.ToDouble(txtSaldo.Text);
           
            cliente.AgregarCuenta(cuenta);

            dataGridView1.Rows.Add(new object[] { cuenta.TipoCuenta, cuenta.CBU, cuenta.Saldo, cuenta.TipoCuenta.Nombre }); ;
        }

        private bool ExisteProductoEnGrilla(string text)
        {
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                if (fila.Cells["nombre"].Value.Equals(text))
                    return true;
            }
            return false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea volver?Tus cambios no se guardaran", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            cliente.Apellido = txtApellido.Text;
            cliente.Nombre = txtNombre.Text;
            cliente.Contrasenia = txtContrasenia.Text;
            cliente.DNI = txtDNI.Text;
            cliente.EsAdmin = false;

            if (await GrabarAsync())
            {
                MessageBox.Show("Exito!Transaccion realizada exitosamente");
                this.Dispose();
            }
            else
            {
                MessageBox.Show("UPS!Se ha producido un error");
            }
        }

        private async Task<bool> GrabarAsync()
        {
            string url ="https://localhost:"+ @Properties.Resources.port+"/Banco/Clientes";
            string presupuestoJson =
           JsonConvert.SerializeObject(cliente);
            var result = await
           ClienteSingleton.GetInstancia().PostAsync(url, presupuestoJson);
            return result.Equals("Ok");
        }

        private void FormABMCliente_Load(object sender, EventArgs e)
        {

        }

        private async void btnEliminar_Click(object sender, EventArgs e)
        {
            await EliminarCliente();
            this.Dispose();
        }

        public async Task EliminarCliente()
        {


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:" + @Properties.Resources.port + "/Banco/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("Clientes/" + cliente.NroCliente);
                deleteTask.Wait();

                var result = deleteTask.Result;
                MessageBox.Show("Exito, borrado correctamente");
            }
        }

        private async void btnEditar_Click(object sender, EventArgs e)
        {
            await EditarCliente();
            this.Dispose();
        }


        public async Task EditarCliente()
        {
            cliente.Apellido = txtApellido.Text;
            cliente.Nombre = txtNombre.Text;
            cliente.Contrasenia = txtContrasenia.Text;
            cliente.DNI = txtDNI.Text;
            



            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(cliente);
                client.BaseAddress = new Uri("https://localhost:" + @Properties.Resources.port + "/Banco/");
                StringContent content = new StringContent(json, Encoding.UTF8,
             "application/json");
                var response = client.PutAsync("clientes", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Exito");
                }
                else
                    MessageBox.Show("Error");
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                cliente.QuitarCuenta(dataGridView1.CurrentCell.RowIndex);
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }
    }
}
