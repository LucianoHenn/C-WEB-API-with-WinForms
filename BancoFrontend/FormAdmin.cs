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
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Seguro que desea salir de la aplicación?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void verTodosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVerClientes form = new FormVerClientes();
            form.ShowDialog();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormABMCliente form = new FormABMCliente();
            form.ShowDialog();
        }

        private void autoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAutores form = new FormAutores();
            form.ShowDialog();
        }

        private void nuevoReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormReporte form = new FormReporte();
            form.ShowDialog();
        }

        private void soporteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void vERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormABMTipoCuenta form = new FormABMTipoCuenta();
            form.ShowDialog();
        }
    }
}
