namespace QuemPegouOVeiculo
{
    partial class FrmRelMotorista
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
            this.BtnListar = new System.Windows.Forms.Button();
            this.CbxListMotorista = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnListar
            // 
            this.BtnListar.Location = new System.Drawing.Point(181, 36);
            this.BtnListar.Name = "BtnListar";
            this.BtnListar.Size = new System.Drawing.Size(75, 23);
            this.BtnListar.TabIndex = 5;
            this.BtnListar.Text = "Listar";
            this.BtnListar.UseVisualStyleBackColor = true;
            this.BtnListar.Click += new System.EventHandler(this.BtnListar_Click);
            // 
            // CbxListMotorista
            // 
            this.CbxListMotorista.FormattingEnabled = true;
            this.CbxListMotorista.Items.AddRange(new object[] {
            "Todos",
            "Ativos",
            "Desativados"});
            this.CbxListMotorista.Location = new System.Drawing.Point(15, 36);
            this.CbxListMotorista.Name = "CbxListMotorista";
            this.CbxListMotorista.Size = new System.Drawing.Size(137, 21);
            this.CbxListMotorista.TabIndex = 4;
            this.CbxListMotorista.SelectedIndexChanged += new System.EventHandler(this.CbxListMotorista_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Listar motoristas";
            // 
            // FrmRelMotorista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 78);
            this.Controls.Add(this.BtnListar);
            this.Controls.Add(this.CbxListMotorista);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelMotorista";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Motorista";
            this.Load += new System.EventHandler(this.FrmRelMotorista_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnListar;
        private System.Windows.Forms.ComboBox CbxListMotorista;
        private System.Windows.Forms.Label label1;
    }
}