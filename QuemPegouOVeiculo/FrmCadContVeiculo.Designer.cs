namespace QuemPegouOVeiculo
{
    partial class FrmCadContVeiculo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MktDtSaida = new System.Windows.Forms.MaskedTextBox();
            this.MktDtChegada = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtKmInicial = new System.Windows.Forms.TextBox();
            this.TxtKmFinal = new System.Windows.Forms.TextBox();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.DgvControleVeiculo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Veiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Motorista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataHoraSaida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataHoraChegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Horas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KmInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KmFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KmTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.LblContVeiculo = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LkLblDtChegadaAtual = new System.Windows.Forms.LinkLabel();
            this.LkLblDtSaidaAtual = new System.Windows.Forms.LinkLabel();
            this.UCVeiculoMotorista = new QuemPegouOVeiculo.USVeiculoMotorista();
            this.UCDescricao = new QuemPegouOVeiculo.UCDescricao();
            ((System.ComponentModel.ISupportInitialize)(this.DgvControleVeiculo)).BeginInit();
            this.SuspendLayout();
            // 
            // MktDtSaida
            // 
            this.MktDtSaida.Location = new System.Drawing.Point(12, 124);
            this.MktDtSaida.Mask = "00/00/0000 00:00";
            this.MktDtSaida.Name = "MktDtSaida";
            this.MktDtSaida.Size = new System.Drawing.Size(121, 21);
            this.MktDtSaida.TabIndex = 2;
            // 
            // MktDtChegada
            // 
            this.MktDtChegada.Location = new System.Drawing.Point(168, 124);
            this.MktDtChegada.Mask = "00/00/0000 00:00";
            this.MktDtChegada.Name = "MktDtChegada";
            this.MktDtChegada.Size = new System.Drawing.Size(121, 21);
            this.MktDtChegada.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Dt. Saida";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Dt. Chegada";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Km. Inicio";
            // 
            // TxtKmInicial
            // 
            this.TxtKmInicial.Location = new System.Drawing.Point(12, 183);
            this.TxtKmInicial.Name = "TxtKmInicial";
            this.TxtKmInicial.Size = new System.Drawing.Size(100, 21);
            this.TxtKmInicial.TabIndex = 4;
            // 
            // TxtKmFinal
            // 
            this.TxtKmFinal.Location = new System.Drawing.Point(118, 183);
            this.TxtKmFinal.Name = "TxtKmFinal";
            this.TxtKmFinal.Size = new System.Drawing.Size(100, 21);
            this.TxtKmFinal.TabIndex = 5;
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(356, 17);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(75, 33);
            this.BtnGravar.TabIndex = 7;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(356, 56);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(75, 33);
            this.BtnAlterar.TabIndex = 8;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(356, 95);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(75, 33);
            this.BtnExcluir.TabIndex = 9;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // DgvControleVeiculo
            // 
            this.DgvControleVeiculo.AllowUserToAddRows = false;
            this.DgvControleVeiculo.AllowUserToDeleteRows = false;
            this.DgvControleVeiculo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvControleVeiculo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvControleVeiculo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvControleVeiculo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvControleVeiculo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Id_Veiculo,
            this.Id_Motorista,
            this.DataHoraSaida,
            this.Modelo,
            this.Nome,
            this.DataHoraChegada,
            this.Dias,
            this.Horas,
            this.Descricao,
            this.KmInicial,
            this.KmFinal,
            this.KmTotal,
            this.Status});
            this.DgvControleVeiculo.Location = new System.Drawing.Point(15, 338);
            this.DgvControleVeiculo.MultiSelect = false;
            this.DgvControleVeiculo.Name = "DgvControleVeiculo";
            this.DgvControleVeiculo.ReadOnly = true;
            this.DgvControleVeiculo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvControleVeiculo.Size = new System.Drawing.Size(416, 150);
            this.DgvControleVeiculo.TabIndex = 11;
            this.DgvControleVeiculo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvControleVeiculo_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 41;
            // 
            // Id_Veiculo
            // 
            this.Id_Veiculo.DataPropertyName = "Id_Veiculo";
            this.Id_Veiculo.HeaderText = "Id_Veiculo";
            this.Id_Veiculo.Name = "Id_Veiculo";
            this.Id_Veiculo.ReadOnly = true;
            this.Id_Veiculo.Visible = false;
            this.Id_Veiculo.Width = 82;
            // 
            // Id_Motorista
            // 
            this.Id_Motorista.DataPropertyName = "Id_Motorista";
            this.Id_Motorista.HeaderText = "Id_Motorista";
            this.Id_Motorista.Name = "Id_Motorista";
            this.Id_Motorista.ReadOnly = true;
            this.Id_Motorista.Visible = false;
            this.Id_Motorista.Width = 90;
            // 
            // DataHoraSaida
            // 
            this.DataHoraSaida.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataHoraSaida.DataPropertyName = "DataHoraSaida";
            this.DataHoraSaida.HeaderText = "DataHoraSaida";
            this.DataHoraSaida.Name = "DataHoraSaida";
            this.DataHoraSaida.ReadOnly = true;
            this.DataHoraSaida.Width = 118;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.Width = 72;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 65;
            // 
            // DataHoraChegada
            // 
            this.DataHoraChegada.DataPropertyName = "DataHoraChegada";
            this.DataHoraChegada.HeaderText = "DataHoraChegada";
            this.DataHoraChegada.Name = "DataHoraChegada";
            this.DataHoraChegada.ReadOnly = true;
            this.DataHoraChegada.Width = 137;
            // 
            // Dias
            // 
            this.Dias.DataPropertyName = "Dias";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dias.DefaultCellStyle = dataGridViewCellStyle1;
            this.Dias.HeaderText = "Dias";
            this.Dias.Name = "Dias";
            this.Dias.ReadOnly = true;
            this.Dias.Width = 57;
            // 
            // Horas
            // 
            this.Horas.DataPropertyName = "Horas";
            this.Horas.HeaderText = "Horas";
            this.Horas.Name = "Horas";
            this.Horas.ReadOnly = true;
            this.Horas.Width = 65;
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Width = 88;
            // 
            // KmInicial
            // 
            this.KmInicial.DataPropertyName = "KmInicial";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KmInicial.DefaultCellStyle = dataGridViewCellStyle2;
            this.KmInicial.HeaderText = "KmInicial";
            this.KmInicial.Name = "KmInicial";
            this.KmInicial.ReadOnly = true;
            this.KmInicial.Width = 85;
            // 
            // KmFinal
            // 
            this.KmFinal.DataPropertyName = "KmFinal";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KmFinal.DefaultCellStyle = dataGridViewCellStyle3;
            this.KmFinal.HeaderText = "KmFinal";
            this.KmFinal.Name = "KmFinal";
            this.KmFinal.ReadOnly = true;
            this.KmFinal.Width = 77;
            // 
            // KmTotal
            // 
            this.KmTotal.DataPropertyName = "KmTotal";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KmTotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.KmTotal.HeaderText = "KmTotal";
            this.KmTotal.Name = "KmTotal";
            this.KmTotal.ReadOnly = true;
            this.KmTotal.Width = 78;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 68;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(12, 298);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(419, 21);
            this.TxtPesquisa.TabIndex = 10;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
            // 
            // LblContVeiculo
            // 
            this.LblContVeiculo.AutoSize = true;
            this.LblContVeiculo.Location = new System.Drawing.Point(12, 322);
            this.LblContVeiculo.Name = "LblContVeiculo";
            this.LblContVeiculo.Size = new System.Drawing.Size(134, 13);
            this.LblContVeiculo.TabIndex = 14;
            this.LblContVeiculo.Text = "Controle Veiculo - 000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(115, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Km. Final";
            // 
            // LkLblDtChegadaAtual
            // 
            this.LkLblDtChegadaAtual.AutoSize = true;
            this.LkLblDtChegadaAtual.Location = new System.Drawing.Point(253, 108);
            this.LkLblDtChegadaAtual.Name = "LkLblDtChegadaAtual";
            this.LkLblDtChegadaAtual.Size = new System.Drawing.Size(36, 13);
            this.LkLblDtChegadaAtual.TabIndex = 21;
            this.LkLblDtChegadaAtual.TabStop = true;
            this.LkLblDtChegadaAtual.Text = "Atual";
            this.LkLblDtChegadaAtual.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LkLblDtChegadaAtual_LinkClicked);
            // 
            // LkLblDtSaidaAtual
            // 
            this.LkLblDtSaidaAtual.AutoSize = true;
            this.LkLblDtSaidaAtual.Location = new System.Drawing.Point(97, 108);
            this.LkLblDtSaidaAtual.Name = "LkLblDtSaidaAtual";
            this.LkLblDtSaidaAtual.Size = new System.Drawing.Size(36, 13);
            this.LkLblDtSaidaAtual.TabIndex = 20;
            this.LkLblDtSaidaAtual.TabStop = true;
            this.LkLblDtSaidaAtual.Text = "Atual";
            this.LkLblDtSaidaAtual.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LkLblDtSaidaAtual_LinkClicked);
            // 
            // UCVeiculoMotorista
            // 
            this.UCVeiculoMotorista.AutoSize = true;
            this.UCVeiculoMotorista.Location = new System.Drawing.Point(12, 17);
            this.UCVeiculoMotorista.Name = "UCVeiculoMotorista";
            this.UCVeiculoMotorista.Size = new System.Drawing.Size(308, 81);
            this.UCVeiculoMotorista.TabIndex = 22;
            this.UCVeiculoMotorista.KmFinal += new QuemPegouOVeiculo.USVeiculoMotorista.KmFimVeiculo(this.UCVeiculoMotorista_KmFinal);
            // 
            // UCDescricao
            // 
            this.UCDescricao.AutoSize = true;
            this.UCDescricao.Location = new System.Drawing.Point(12, 210);
            this.UCDescricao.Name = "UCDescricao";
            this.UCDescricao.Size = new System.Drawing.Size(411, 67);
            this.UCDescricao.TabIndex = 6;
            // 
            // FrmCadContVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 504);
            this.Controls.Add(this.UCVeiculoMotorista);
            this.Controls.Add(this.LkLblDtChegadaAtual);
            this.Controls.Add(this.LkLblDtSaidaAtual);
            this.Controls.Add(this.UCDescricao);
            this.Controls.Add(this.LblContVeiculo);
            this.Controls.Add(this.TxtPesquisa);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DgvControleVeiculo);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.TxtKmFinal);
            this.Controls.Add(this.TxtKmInicial);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MktDtChegada);
            this.Controls.Add(this.MktDtSaida);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCadContVeiculo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de Veiculo";
            this.Load += new System.EventHandler(this.FrmCadContVeiculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvControleVeiculo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MaskedTextBox MktDtSaida;
        private System.Windows.Forms.MaskedTextBox MktDtChegada;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtKmFinal;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.DataGridView DgvControleVeiculo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Label LblContVeiculo;
        private System.Windows.Forms.Label label9;
        private UCDescricao UCDescricao;
        private System.Windows.Forms.LinkLabel LkLblDtChegadaAtual;
        private System.Windows.Forms.LinkLabel LkLblDtSaidaAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Veiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Motorista;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataHoraSaida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataHoraChegada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dias;
        private System.Windows.Forms.DataGridViewTextBoxColumn Horas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn KmInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn KmFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn KmTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        public System.Windows.Forms.TextBox TxtKmInicial;
        private USVeiculoMotorista UCVeiculoMotorista;
    }
}