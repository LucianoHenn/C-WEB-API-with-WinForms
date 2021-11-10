using BancoBackend.negocio;
using Microsoft.Reporting.WinForms;
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
    public partial class FormReporte : Form
    {
        public FormReporte()
        {
            InitializeComponent();
        }

        private void FormReporte_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string desde = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string hasta = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            Aplicacion app = new Aplicacion();
            DataTable tabla = app.CrearReporte(desde, hasta);
            //DATASOURCE
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new
           ReportDataSource("DataSet2", tabla));
            reportViewer1.LocalReport.DataSources.Add(new
           ReportDataSource("DataSet1", tabla));
            reportViewer1.RefreshReport();


        }
    }
}
