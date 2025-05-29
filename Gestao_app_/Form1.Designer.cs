namespace Gestao_app_
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
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelOK = new System.Windows.Forms.Label();
            this.labelFalha = new System.Windows.Forms.Label();
            this.labelMediaTempo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelTotal.Location = new System.Drawing.Point(30, 30);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(140, 21);
            this.labelTotal.Text = "Total de peças: 0";
            // 
            // labelOK
            // 
            this.labelOK.AutoSize = true;
            this.labelOK.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelOK.Location = new System.Drawing.Point(30, 70);
            this.labelOK.Name = "labelOK";
            this.labelOK.Size = new System.Drawing.Size(79, 21);
            this.labelOK.Text = "OK: 0";
            // 
            // labelFalha
            // 
            this.labelFalha.AutoSize = true;
            this.labelFalha.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelFalha.Location = new System.Drawing.Point(30, 110);
            this.labelFalha.Name = "labelFalha";
            this.labelFalha.Size = new System.Drawing.Size(99, 21);
            this.labelFalha.Text = "Falhas: 0";
            // 
            // labelMediaTempo
            // 
            this.labelMediaTempo.AutoSize = true;
            this.labelMediaTempo.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.labelMediaTempo.Location = new System.Drawing.Point(30, 150);
            this.labelMediaTempo.Name = "labelMediaTempo";
            this.labelMediaTempo.Size = new System.Drawing.Size(175, 21);
            this.labelMediaTempo.Text = "Média tempo: 0.0 s";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(300, 220);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.labelOK);
            this.Controls.Add(this.labelFalha);
            this.Controls.Add(this.labelMediaTempo);
            this.Name = "Form1";
            this.Text = "📊 Dashboard de Gestão";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
