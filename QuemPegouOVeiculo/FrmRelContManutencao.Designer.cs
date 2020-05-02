namespace QuemPegouOVeiculo
{
    partial class FrmRelContManutencao
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
            this.GbOpcoes = new System.Windows.Forms.GroupBox();
            this.RbGeral = new System.Windows.Forms.RadioButton();
            this.RbVeiculo = new System.Windows.Forms.RadioButton();
            this.RbPeriodo = new System.Windows.Forms.RadioButton();
            this.GbVeiculo = new System.Windows.Forms.GroupBox();
            this.UCVeiculo = new QuemPegouOVeiculo.UCVeiculo();
            this.GbPeriodo = new System.Windows.Forms.GroupBox();
            this.MktDataInicio = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MktDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.BtnListar = new System.Windows.Forms.Button();
            this.GbOpcoes.SuspendLayout();
            this.GbVeiculo.SuspendLayout();
            this.GbPeriodo.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbOpcoes
            // 
            this.GbOpcoes.Controls.Add(this.RbPeriodo);
            this.GbOpcoes.Controls.Add(this.RbVeiculo);
            this.GbOpcoes.Controls.Add(this.RbGeral);
            this.GbOpcoes.Location = new System.Drawing.Point(12, 13);
            this.GbOpcoes.Name = "GbOpcoes";
            this.GbOpcoes.Size = new System.Drawing.Size(281, 56);
            this.GbOpcoes.TabIndex = 0;
            this.GbOpcoes.TabStop = false;
            this.GbOpcoes.Text = "Opções";
            // 
            // RbGeral
            // 
            this.RbGeral.AutoSize = true;
            this.RbGeral.Checked = true;
            this.RbGeral.Location = new System.Drawing.Point(6, 19);
            this.RbGeral.Name = "RbGeral";
            this.RbGeral.Size = new System.Drawing.Size(50, 17);
            this.RbGeral.TabIndex = 0;
            this.RbGeral.TabStop = true;
            this.RbGeral.Text = "Geral";
            this.RbGeral.UseVisualStyleBackColor = true;
            this.RbGeral.CheckedChanged += new System.EventHandler(this.RbGeral_CheckedChanged);
            // 
            // RbVeiculo
            // 
            this.RbVeiculo.AutoSize = true;
            this.RbVeiculo.Location = new System.Drawing.Point(62, 19);
            this.RbVeiculo.Name = "RbVeiculo";
            this.RbVeiculo.Size = new System.Drawing.Size(62, 17);
            this.RbVeiculo.TabIndex = 1;
            this.RbVeiculo.Text = "Veículo";
            this.RbVeiculo.UseVisualStyleBackColor = true;
            this.RbVeiculo.CheckedChanged += new System.EventHandler(this.RbVeiculo_CheckedChanged);
            // 
            // RbPeriodo
            // 
            this.RbPeriodo.AutoSize = true;
            this.RbPeriodo.Location = new System.Drawing.Point(130, 19);
            this.RbPeriodo.Name = "RbPeriodo";
            this.RbPeriodo.Size = new System.Drawing.Size(63, 17);
            this.RbPeriodo.TabIndex = 2;
            this.RbPeriodo.Text = "Período";
            this.RbPeriodo.UseVisualStyleBackColor = true;
            this.RbPeriodo.CheckedChanged += new System.EventHandler(this.RbPeriodo_CheckedChanged);
            // 
            // GbVeiculo
            // 
            this.GbVeiculo.Controls.Add(this.UCVeiculo);
            this.GbVeiculo.Enabled = false;
            this.GbVeiculo.Location = new System.Drawing.Point(12, 75);
            this.GbVeiculo.Name = "GbVeiculo";
            this.GbVeiculo.Size = new System.Drawing.Size(281, 79);
            this.GbVeiculo.TabIndex = 1;
            this.GbVeiculo.TabStop = false;
            // 
            // UCVeiculo
            // 
            this.UCVeiculo.Location = new System.Drawing.Point(6, 19);
            this.UCVeiculo.Name = "UCVeiculo";
            this.UCVeiculo.Size = new System.Drawing.Size(265, 38);
            this.UCVeiculo.TabIndex = 0;
            // 
            // GbPeriodo
            // 
            this.GbPeriodo.Controls.Add(this.MktDataFinal);
            this.GbPeriodo.Controls.Add(this.label1);
            this.GbPeriodo.Controls.Add(this.MktDataInicio);
            this.GbPeriodo.Enabled = false;
            this.GbPeriodo.Location = new System.Drawing.Point(12, 160);
            this.GbPeriodo.Name = "GbPeriodo";
            this.GbPeriodo.Size = new System.Drawing.Size(281, 53);
            this.GbPeriodo.TabIndex = 2;
            this.GbPeriodo.TabStop = false;
            this.GbPeriodo.Text = "Período";
            // 
            // MktDataInicio
            // 
            this.MktDataInicio.Location = new System.Drawing.Point(28, 19);
            this.MktDataInicio.Mask = "00/00/0000";
            this.MktDataInicio.Name = "MktDataInicio";
            this.MktDataInicio.Size = new System.Drawing.Size(100, 20);
            this.MktDataInicio.TabIndex = 0;
            this.MktDataInicio.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "a";
            // 
            // MktDataFinal
            // 
            this.MktDataFinal.Location = new System.Drawing.Point(152, 19);
            this.MktDataFinal.Mask = "00/00/0000";
            this.MktDataFinal.Name = "MktDataFinal";
            this.MktDataFinal.Size = new System.Drawing.Size(100, 20);
            this.MktDataFinal.TabIndex = 2;
            this.MktDataFinal.ValidatingType = typeof(System.DateTime);
            // 
            // BtnListar
            // 
            this.BtnListar.Location = new System.Drawing.Point(299, 13);
            this.BtnListar.Name = "BtnListar";
            this.BtnListar.Size = new System.Drawing.Size(75, 23);
            this.BtnListar.TabIndex = 3;
            this.BtnListar.Text = "Listar";
            this.BtnListar.UseVisualStyleBackColor = true;
            this.BtnListar.Click += new System.EventHandler(this.BtnListar_Click);
            // 
            // FrmRelContManutencao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 227);
            this.Controls.Add(this.BtnListar);
            this.Controls.Add(this.GbPeriodo);
            this.Controls.Add(this.GbVeiculo);
            this.Controls.Add(this.GbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelContManutencao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Controle Manutenção";
            this.GbOpcoes.ResumeLayout(false);
            this.GbOpcoes.PerformLayout();
            this.GbVeiculo.ResumeLayout(false);
            this.GbPeriodo.ResumeLayout(false);
            this.GbPeriodo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbOpcoes;
        private System.Windows.Forms.RadioButton RbPeriodo;
        private System.Windows.Forms.RadioButton RbVeiculo;
        private System.Windows.Forms.RadioButton RbGeral;
        private System.Windows.Forms.GroupBox GbVeiculo;
        private UCVeiculo UCVeiculo;
        private System.Windows.Forms.GroupBox GbPeriodo;
        private System.Windows.Forms.MaskedTextBox MktDataFinal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktDataInicio;
        private System.Windows.Forms.Button BtnListar;
    }
}