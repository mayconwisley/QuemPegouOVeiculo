namespace QuemPegouOVeiculo
{
    partial class FrmRelContVeiculo
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
            this.RbVeiMot = new System.Windows.Forms.RadioButton();
            this.RbPeriodo = new System.Windows.Forms.RadioButton();
            this.RbMotorista = new System.Windows.Forms.RadioButton();
            this.RbVeiculo = new System.Windows.Forms.RadioButton();
            this.RbGeral = new System.Windows.Forms.RadioButton();
            this.BtnListar = new System.Windows.Forms.Button();
            this.GbVeiculo = new System.Windows.Forms.GroupBox();
            this.UCVeiculo = new QuemPegouOVeiculo.UCVeiculo();
            this.GbMotorista = new System.Windows.Forms.GroupBox();
            this.UCMotorista = new QuemPegouOVeiculo.UCMotorista();
            this.GbPeriodo = new System.Windows.Forms.GroupBox();
            this.CbMotorista = new System.Windows.Forms.CheckBox();
            this.CbVeiculo = new System.Windows.Forms.CheckBox();
            this.CbListaDataNull = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MktDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.MktDataIncial = new System.Windows.Forms.MaskedTextBox();
            this.RbDtChegada = new System.Windows.Forms.RadioButton();
            this.RbDtSaida = new System.Windows.Forms.RadioButton();
            this.GbOpcoes.SuspendLayout();
            this.GbVeiculo.SuspendLayout();
            this.GbMotorista.SuspendLayout();
            this.GbPeriodo.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbOpcoes
            // 
            this.GbOpcoes.Controls.Add(this.RbVeiMot);
            this.GbOpcoes.Controls.Add(this.RbPeriodo);
            this.GbOpcoes.Controls.Add(this.RbMotorista);
            this.GbOpcoes.Controls.Add(this.RbVeiculo);
            this.GbOpcoes.Controls.Add(this.RbGeral);
            this.GbOpcoes.Location = new System.Drawing.Point(11, 14);
            this.GbOpcoes.Name = "GbOpcoes";
            this.GbOpcoes.Size = new System.Drawing.Size(343, 52);
            this.GbOpcoes.TabIndex = 0;
            this.GbOpcoes.TabStop = false;
            this.GbOpcoes.Text = "Opções";
            // 
            // RbVeiMot
            // 
            this.RbVeiMot.AutoSize = true;
            this.RbVeiMot.Location = new System.Drawing.Point(273, 19);
            this.RbVeiMot.Name = "RbVeiMot";
            this.RbVeiMot.Size = new System.Drawing.Size(63, 17);
            this.RbVeiMot.TabIndex = 5;
            this.RbVeiMot.Text = "Vei/Mot";
            this.RbVeiMot.UseVisualStyleBackColor = true;
            this.RbVeiMot.CheckedChanged += new System.EventHandler(this.RbVeiMot_CheckedChanged);
            // 
            // RbPeriodo
            // 
            this.RbPeriodo.AutoSize = true;
            this.RbPeriodo.Location = new System.Drawing.Point(204, 19);
            this.RbPeriodo.Name = "RbPeriodo";
            this.RbPeriodo.Size = new System.Drawing.Size(63, 17);
            this.RbPeriodo.TabIndex = 4;
            this.RbPeriodo.Text = "Período";
            this.RbPeriodo.UseVisualStyleBackColor = true;
            this.RbPeriodo.CheckedChanged += new System.EventHandler(this.RbPeriodo_CheckedChanged);
            // 
            // RbMotorista
            // 
            this.RbMotorista.AutoSize = true;
            this.RbMotorista.Location = new System.Drawing.Point(130, 19);
            this.RbMotorista.Name = "RbMotorista";
            this.RbMotorista.Size = new System.Drawing.Size(68, 17);
            this.RbMotorista.TabIndex = 3;
            this.RbMotorista.Text = "Motorista";
            this.RbMotorista.UseVisualStyleBackColor = true;
            this.RbMotorista.CheckedChanged += new System.EventHandler(this.RbMotorista_CheckedChanged);
            // 
            // RbVeiculo
            // 
            this.RbVeiculo.AutoSize = true;
            this.RbVeiculo.Location = new System.Drawing.Point(62, 19);
            this.RbVeiculo.Name = "RbVeiculo";
            this.RbVeiculo.Size = new System.Drawing.Size(62, 17);
            this.RbVeiculo.TabIndex = 2;
            this.RbVeiculo.Text = "Veículo";
            this.RbVeiculo.UseVisualStyleBackColor = true;
            this.RbVeiculo.CheckedChanged += new System.EventHandler(this.RbVeiculo_CheckedChanged);
            // 
            // RbGeral
            // 
            this.RbGeral.AutoSize = true;
            this.RbGeral.Checked = true;
            this.RbGeral.Location = new System.Drawing.Point(6, 19);
            this.RbGeral.Name = "RbGeral";
            this.RbGeral.Size = new System.Drawing.Size(50, 17);
            this.RbGeral.TabIndex = 1;
            this.RbGeral.TabStop = true;
            this.RbGeral.Text = "Geral";
            this.RbGeral.UseVisualStyleBackColor = true;
            this.RbGeral.CheckedChanged += new System.EventHandler(this.RbGeral_CheckedChanged);
            // 
            // BtnListar
            // 
            this.BtnListar.Location = new System.Drawing.Point(360, 14);
            this.BtnListar.Name = "BtnListar";
            this.BtnListar.Size = new System.Drawing.Size(75, 23);
            this.BtnListar.TabIndex = 4;
            this.BtnListar.Text = "Listar";
            this.BtnListar.UseVisualStyleBackColor = true;
            this.BtnListar.Click += new System.EventHandler(this.BtnListar_Click);
            // 
            // GbVeiculo
            // 
            this.GbVeiculo.Controls.Add(this.UCVeiculo);
            this.GbVeiculo.Enabled = false;
            this.GbVeiculo.Location = new System.Drawing.Point(11, 193);
            this.GbVeiculo.Name = "GbVeiculo";
            this.GbVeiculo.Size = new System.Drawing.Size(343, 65);
            this.GbVeiculo.TabIndex = 2;
            this.GbVeiculo.TabStop = false;
            // 
            // UCVeiculo
            // 
            this.UCVeiculo.Location = new System.Drawing.Point(6, 10);
            this.UCVeiculo.Name = "UCVeiculo";
            this.UCVeiculo.Size = new System.Drawing.Size(268, 38);
            this.UCVeiculo.TabIndex = 0;
            // 
            // GbMotorista
            // 
            this.GbMotorista.Controls.Add(this.UCMotorista);
            this.GbMotorista.Enabled = false;
            this.GbMotorista.Location = new System.Drawing.Point(11, 257);
            this.GbMotorista.Name = "GbMotorista";
            this.GbMotorista.Size = new System.Drawing.Size(343, 59);
            this.GbMotorista.TabIndex = 3;
            this.GbMotorista.TabStop = false;
            // 
            // UCMotorista
            // 
            this.UCMotorista.AutoSize = true;
            this.UCMotorista.Location = new System.Drawing.Point(6, 10);
            this.UCMotorista.Name = "UCMotorista";
            this.UCMotorista.Size = new System.Drawing.Size(272, 41);
            this.UCMotorista.TabIndex = 0;
            // 
            // GbPeriodo
            // 
            this.GbPeriodo.Controls.Add(this.CbMotorista);
            this.GbPeriodo.Controls.Add(this.CbVeiculo);
            this.GbPeriodo.Controls.Add(this.CbListaDataNull);
            this.GbPeriodo.Controls.Add(this.label1);
            this.GbPeriodo.Controls.Add(this.MktDataFinal);
            this.GbPeriodo.Controls.Add(this.MktDataIncial);
            this.GbPeriodo.Controls.Add(this.RbDtChegada);
            this.GbPeriodo.Controls.Add(this.RbDtSaida);
            this.GbPeriodo.Enabled = false;
            this.GbPeriodo.Location = new System.Drawing.Point(11, 75);
            this.GbPeriodo.Name = "GbPeriodo";
            this.GbPeriodo.Size = new System.Drawing.Size(343, 112);
            this.GbPeriodo.TabIndex = 1;
            this.GbPeriodo.TabStop = false;
            this.GbPeriodo.Text = "Período";
            // 
            // CbMotorista
            // 
            this.CbMotorista.AutoSize = true;
            this.CbMotorista.Location = new System.Drawing.Point(257, 16);
            this.CbMotorista.Name = "CbMotorista";
            this.CbMotorista.Size = new System.Drawing.Size(69, 17);
            this.CbMotorista.TabIndex = 3;
            this.CbMotorista.Text = "Motorista";
            this.CbMotorista.UseVisualStyleBackColor = true;
            this.CbMotorista.CheckedChanged += new System.EventHandler(this.CbMotorista_CheckedChanged);
            // 
            // CbVeiculo
            // 
            this.CbVeiculo.AutoSize = true;
            this.CbVeiculo.Location = new System.Drawing.Point(188, 16);
            this.CbVeiculo.Name = "CbVeiculo";
            this.CbVeiculo.Size = new System.Drawing.Size(63, 17);
            this.CbVeiculo.TabIndex = 2;
            this.CbVeiculo.Text = "Veículo";
            this.CbVeiculo.UseVisualStyleBackColor = true;
            this.CbVeiculo.CheckedChanged += new System.EventHandler(this.CbVeiculo_CheckedChanged);
            // 
            // CbListaDataNull
            // 
            this.CbListaDataNull.AutoSize = true;
            this.CbListaDataNull.Location = new System.Drawing.Point(6, 89);
            this.CbListaDataNull.Name = "CbListaDataNull";
            this.CbListaDataNull.Size = new System.Drawing.Size(124, 17);
            this.CbListaDataNull.TabIndex = 6;
            this.CbListaDataNull.Text = "Data Chegada Vazia";
            this.CbListaDataNull.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "a";
            // 
            // MktDataFinal
            // 
            this.MktDataFinal.Location = new System.Drawing.Point(174, 48);
            this.MktDataFinal.Mask = "00/00/0000";
            this.MktDataFinal.Name = "MktDataFinal";
            this.MktDataFinal.Size = new System.Drawing.Size(100, 20);
            this.MktDataFinal.TabIndex = 5;
            this.MktDataFinal.ValidatingType = typeof(System.DateTime);
            // 
            // MktDataIncial
            // 
            this.MktDataIncial.Location = new System.Drawing.Point(49, 48);
            this.MktDataIncial.Mask = "00/00/0000";
            this.MktDataIncial.Name = "MktDataIncial";
            this.MktDataIncial.Size = new System.Drawing.Size(100, 20);
            this.MktDataIncial.TabIndex = 4;
            this.MktDataIncial.ValidatingType = typeof(System.DateTime);
            // 
            // RbDtChegada
            // 
            this.RbDtChegada.AutoSize = true;
            this.RbDtChegada.Location = new System.Drawing.Point(88, 15);
            this.RbDtChegada.Name = "RbDtChegada";
            this.RbDtChegada.Size = new System.Drawing.Size(94, 17);
            this.RbDtChegada.TabIndex = 1;
            this.RbDtChegada.Text = "Data Chegada";
            this.RbDtChegada.UseVisualStyleBackColor = true;
            // 
            // RbDtSaida
            // 
            this.RbDtSaida.AutoSize = true;
            this.RbDtSaida.Checked = true;
            this.RbDtSaida.Location = new System.Drawing.Point(4, 15);
            this.RbDtSaida.Name = "RbDtSaida";
            this.RbDtSaida.Size = new System.Drawing.Size(80, 17);
            this.RbDtSaida.TabIndex = 0;
            this.RbDtSaida.TabStop = true;
            this.RbDtSaida.Text = "Data Saída";
            this.RbDtSaida.UseVisualStyleBackColor = true;
            // 
            // FrmRelContVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 330);
            this.Controls.Add(this.GbPeriodo);
            this.Controls.Add(this.GbMotorista);
            this.Controls.Add(this.GbVeiculo);
            this.Controls.Add(this.BtnListar);
            this.Controls.Add(this.GbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelContVeiculo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Controle Veículo";
            this.GbOpcoes.ResumeLayout(false);
            this.GbOpcoes.PerformLayout();
            this.GbVeiculo.ResumeLayout(false);
            this.GbMotorista.ResumeLayout(false);
            this.GbMotorista.PerformLayout();
            this.GbPeriodo.ResumeLayout(false);
            this.GbPeriodo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbOpcoes;
        private System.Windows.Forms.RadioButton RbGeral;
        private System.Windows.Forms.RadioButton RbMotorista;
        private System.Windows.Forms.RadioButton RbVeiculo;
        private System.Windows.Forms.RadioButton RbPeriodo;
        private System.Windows.Forms.Button BtnListar;
        private System.Windows.Forms.GroupBox GbVeiculo;
        private System.Windows.Forms.GroupBox GbMotorista;
        private System.Windows.Forms.GroupBox GbPeriodo;
        private System.Windows.Forms.RadioButton RbDtChegada;
        private System.Windows.Forms.RadioButton RbDtSaida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktDataFinal;
        private System.Windows.Forms.MaskedTextBox MktDataIncial;
        private UCVeiculo UCVeiculo;
        private UCMotorista UCMotorista;
        private System.Windows.Forms.CheckBox CbListaDataNull;
        private System.Windows.Forms.CheckBox CbMotorista;
        private System.Windows.Forms.CheckBox CbVeiculo;
        private System.Windows.Forms.RadioButton RbVeiMot;
    }
}