using Cliente_Soap.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Cliente_Soap
{
    public partial class Form1 : Form
    {
        WebService1SoapClient client = new WebService1SoapClient();
 
        public Form1()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.Form1_Load);
        }

        //Evento executado quando o formulário é carregado
        private void Form1_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = "Insira o código da peça...";
            txtCodigo.ForeColor = Color.Gray; //Simular placeholder
        }


        //Evento acionado quando o utilizador clica ou entra na txtCodigo
        private void txtCodigo_Enter(object sender, EventArgs e)
        {
            if (txtCodigo.Text == "Insira o código da peça...")
            {
                txtCodigo.Text = "";
                txtCodigo.ForeColor = Color.Black;
            }
        }

        //Evento acionado quando o utilizador sai da txtCodigo
        private void txtCodigo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                txtCodigo.Text = "Insira o código da peça...";
                txtCodigo.ForeColor = Color.Gray;
            }
        }


        //-------------------------------------------

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

            //txtResultado.Text = dados.Replace("€", "EUR ");
            txtResultado.Text = dados.Replace("|", Environment.NewLine);
        }

        private void btnpreju_Click(object sender, EventArgs e)
        {
            var cod = client.GetPecaMaiorPrejuizo();

            //txtResultado.Text = $"Peça/s com maior prejuizo de sempre: \n{cod}";
            txtResultado.Text = cod.Replace("|", Environment.NewLine);
        }

    }
}
