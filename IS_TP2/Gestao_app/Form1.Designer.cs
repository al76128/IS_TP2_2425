namespace Gestao_app
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelOK;
        private System.Windows.Forms.Label labelFalha;
        private System.Windows.Forms.Label labelMediaTempo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTotal = new Label();
            labelOK = new Label();
            labelFalha = new Label();
            labelMediaTempo = new Label();
            SuspendLayout();
            // 
            // labelTotal
            // 
            labelTotal.AutoSize = true;
            labelTotal.Font = new Font("Segoe UI", 12F);
            labelTotal.Location = new Point(30, 30);
            labelTotal.Name = "labelTotal";
            labelTotal.Size = new Size(191, 32);
            labelTotal.TabIndex = 0;
            labelTotal.Text = "Total de peças: 0";
            // 
            // labelOK
            // 
            labelOK.AutoSize = true;
            labelOK.Font = new Font("Segoe UI", 12F);
            labelOK.Location = new Point(30, 70);
            labelOK.Name = "labelOK";
            labelOK.Size = new Size(71, 32);
            labelOK.TabIndex = 1;
            labelOK.Text = "OK: 0";
            // 
            // labelFalha
            // 
            labelFalha.AutoSize = true;
            labelFalha.Font = new Font("Segoe UI", 12F);
            labelFalha.Location = new Point(30, 110);
            labelFalha.Name = "labelFalha";
            labelFalha.Size = new Size(104, 32);
            labelFalha.TabIndex = 2;
            labelFalha.Text = "Falhas: 0";
            // 
            // labelMediaTempo
            // 
            labelMediaTempo.AutoSize = true;
            labelMediaTempo.Font = new Font("Segoe UI", 12F);
            labelMediaTempo.Location = new Point(30, 150);
            labelMediaTempo.Name = "labelMediaTempo";
            labelMediaTempo.Size = new Size(218, 32);
            labelMediaTempo.TabIndex = 3;
            labelMediaTempo.Text = "Média tempo: 0.0 s";
            // 
            // Form1
            // 
            ClientSize = new Size(545, 213);
            Controls.Add(labelTotal);
            Controls.Add(labelOK);
            Controls.Add(labelFalha);
            Controls.Add(labelMediaTempo);
            Name = "Form1";
            Text = "Dashboard de Gestão";
            ResumeLayout(false);
            PerformLayout();

        }
    }
}
