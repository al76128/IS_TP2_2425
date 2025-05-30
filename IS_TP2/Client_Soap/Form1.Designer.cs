namespace Cliente_Soap
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Forms Designer

        private void InitializeComponent()
        {
            this.dateInicio = new System.Windows.Forms.DateTimePicker();
            this.dateFim = new System.Windows.Forms.DateTimePicker();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.btnCusto = new System.Windows.Forms.Button();
            this.btnLucro = new System.Windows.Forms.Button();
            this.btnPrejuizo = new System.Windows.Forms.Button();
            this.btnPeca = new System.Windows.Forms.Button();
            this.btnpreju = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateInicio
            // 
            this.dateInicio.Location = new System.Drawing.Point(12, 12);
            this.dateInicio.Name = "dateInicio";
            this.dateInicio.Size = new System.Drawing.Size(200, 26);
            this.dateInicio.TabIndex = 0;
            // 
            // dateFim
            // 
            this.dateFim.Location = new System.Drawing.Point(228, 12);
            this.dateFim.Name = "dateFim";
            this.dateFim.Size = new System.Drawing.Size(200, 26);
            this.dateFim.TabIndex = 1;
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(12, 48);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(416, 26);
            this.txtCodigo.TabIndex = 2;
            this.txtCodigo.Enter += new System.EventHandler(this.txtCodigo_Enter);
            this.txtCodigo.Leave += new System.EventHandler(this.txtCodigo_Leave);
            // 
            // txtResultado
            // 
            this.txtResultado.Location = new System.Drawing.Point(12, 140);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(416, 200);
            this.txtResultado.TabIndex = 6;
            // 
            // btnCusto
            // 
            this.btnCusto.Location = new System.Drawing.Point(12, 85);
            this.btnCusto.Name = "btnCusto";
            this.btnCusto.Size = new System.Drawing.Size(95, 23);
            this.btnCusto.TabIndex = 3;
            this.btnCusto.Text = "Custo Total";
            this.btnCusto.UseVisualStyleBackColor = true;
            this.btnCusto.Click += new System.EventHandler(this.btnCusto_Click);
            // 
            // btnLucro
            // 
            this.btnLucro.Location = new System.Drawing.Point(113, 85);
            this.btnLucro.Name = "btnLucro";
            this.btnLucro.Size = new System.Drawing.Size(95, 23);
            this.btnLucro.TabIndex = 4;
            this.btnLucro.Text = "Lucro Total";
            this.btnLucro.UseVisualStyleBackColor = true;
            this.btnLucro.Click += new System.EventHandler(this.btnLucro_Click);
            // 
            // btnPrejuizo
            // 
            this.btnPrejuizo.Location = new System.Drawing.Point(214, 85);
            this.btnPrejuizo.Name = "btnPrejuizo";
            this.btnPrejuizo.Size = new System.Drawing.Size(105, 23);
            this.btnPrejuizo.TabIndex = 5;
            this.btnPrejuizo.Text = "Prejuízo por Peça";
            this.btnPrejuizo.UseVisualStyleBackColor = true;
            this.btnPrejuizo.Click += new System.EventHandler(this.btnPrejuizo_Click);
            // 
            // btnPeca
            // 
            this.btnPeca.Location = new System.Drawing.Point(325, 85);
            this.btnPeca.Name = "btnPeca";
            this.btnPeca.Size = new System.Drawing.Size(103, 23);
            this.btnPeca.TabIndex = 6;
            this.btnPeca.Text = "Dados da Peça";
            this.btnPeca.UseVisualStyleBackColor = true;
            this.btnPeca.Click += new System.EventHandler(this.btnPeca_Click);
            // 
            // btnpreju
            // 
            this.btnpreju.Location = new System.Drawing.Point(95, 112);
            this.btnpreju.Name = "btnpreju";
            this.btnpreju.Size = new System.Drawing.Size(243, 22);
            this.btnpreju.TabIndex = 7;
            this.btnpreju.Text = "Peça/s com maior prejuizo";
            this.btnpreju.UseVisualStyleBackColor = true;
            this.btnpreju.Click += new System.EventHandler(this.btnpreju_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(437, 361);
            this.Controls.Add(this.btnpreju);
            this.Controls.Add(this.btnPeca);
            this.Controls.Add(this.btnPrejuizo);
            this.Controls.Add(this.btnLucro);
            this.Controls.Add(this.btnCusto);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.dateFim);
            this.Controls.Add(this.dateInicio);
            this.Name = "Form1";
            this.Text = "Cliente SOAP - Financeiro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateInicio;
        private System.Windows.Forms.DateTimePicker dateFim;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.Button btnCusto;
        private System.Windows.Forms.Button btnLucro;
        private System.Windows.Forms.Button btnPrejuizo;
        private System.Windows.Forms.Button btnPeca;
        private System.Windows.Forms.Button btnpreju;
    }
}
