namespace QuemPegouOVeiculo
{
    partial class FrmAtuControleVeiculo_0
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label2 = new System.Windows.Forms.Label();
            this.MktDtChegada = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtKmFinal = new System.Windows.Forms.TextBox();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Data Chegada";
            // 
            // MktDtChegada
            // 
            this.MktDtChegada.Location = new System.Drawing.Point(15, 30);
            this.MktDtChegada.Mask = "00/00/0000 90:00";
            this.MktDtChegada.Name = "MktDtChegada";
            this.MktDtChegada.Size = new System.Drawing.Size(111, 20);
            this.MktDtChegada.TabIndex = 2;
            this.MktDtChegada.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "KmFinal";
            // 
            // TxtKmFinal
            // 
            this.TxtKmFinal.Location = new System.Drawing.Point(132, 30);
            this.TxtKmFinal.Name = "TxtKmFinal";
            this.TxtKmFinal.Size = new System.Drawing.Size(100, 20);
            this.TxtKmFinal.TabIndex = 4;
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(242, 28);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(75, 23);
            this.BtnGravar.TabIndex = 5;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // FrmAtuControleVeiculo_0
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 64);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.TxtKmFinal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MktDtChegada);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAtuControleVeiculo_0";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle Veículo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAtuControleVeiculo_0_FormClosing);
            this.Load += new System.EventHandler(this.FrmAtuControleVeiculo_0_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox MktDtChegada;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtKmFinal;
        private System.Windows.Forms.Button BtnGravar;
    }
}