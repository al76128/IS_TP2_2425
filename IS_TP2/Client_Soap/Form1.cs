using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Cliente_Soap.ServiceReference1;

namespace Cliente_Soap
{
    public partial class Form1 : Form
    {
        WebService1SoapClient client = new WebService1SoapClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCusto_Click(object sender, EventArgs e)
        {
            var custo = client.ObterCustosTotaisPorPeriodo(dateInicio.Value, dateFim.Value);
            txtResultado.Text = $"Custo total: EUR {custo}";
        }

        private void btnLucro_Click(object sender, EventArgs e)
        {
            var lucro = client.ObterLucroTotalPorPeriodo(dateInicio.Value, dateFim.Value);
            txtResultado.Text = $"Lucro total: EUR {lucro}";
        }

        private void btnPrejuizo_Click(object sender, EventArgs e)
        {
            var lista = client.ObterPrejuizoTotalPorPeca(dateInicio.Value, dateFim.Value);
            txtResultado.Clear();
            foreach (var item in lista)
            {
                txtResultado.AppendText(item.Replace("€", "EUR ") + Environment.NewLine);
            }
        }

        private void btnPeca_Click(object sender, EventArgs e)
        {
            var codigo = txtCodigo.Text;
            var dados = client.ObterDadosFinanceirosPorPeca(codigo);
            txtResultado.Text = dados.Replace("€", "EUR ");
        }
    }
}
