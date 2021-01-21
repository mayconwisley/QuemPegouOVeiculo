namespace QuemPegouOVeiculo
{
    partial class FrmContCombustivel
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtKmInicial = new System.Windows.Forms.TextBox();
            this.DgvAbastecimento = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Veiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Motorista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KmInicial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblAbastecimento = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.MktData = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.UCVeiculoMotorista = new QuemPegouOVeiculo.USVeiculoMotorista();
            this.UCDescricao = new QuemPegouOVeiculo.UCDescricao();
            this.UCValor = new QuemPegouOVeiculo.UCValor();
            this.TxtLitros = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvAbastecimento)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Km. Inicial";
            // 
            // TxtKmInicial
            // 
            this.TxtKmInicial.Location = new System.Drawing.Point(19, 128);
            this.TxtKmInicial.Name = "TxtKmInicial";
            this.TxtKmInicial.Size = new System.Drawing.Size(100, 21);
            this.TxtKmInicial.TabIndex = 2;
            // 
            // DgvAbastecimento
            // 
            this.DgvAbastecimento.AllowUserToAddRows = false;
            this.DgvAbastecimento.AllowUserToDeleteRows = false;
            this.DgvAbastecimento.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvAbastecimento.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvAbastecimento.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvAbastecimento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvAbastecimento.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Id_Veiculo,
            this.Id_Motorista,
            this.Data,
            this.Valor,
            this.Nome,
            this.Modelo,
            this.KmInicial,
            this.Descricao});
            this.DgvAbastecimento.Location = new System.Drawing.Point(19, 285);
            this.DgvAbastecimento.MultiSelect = false;
            this.DgvAbastecimento.Name = "DgvAbastecimento";
            this.DgvAbastecimento.ReadOnly = true;
            this.DgvAbastecimento.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvAbastecimento.Size = new System.Drawing.Size(408, 150);
            this.DgvAbastecimento.TabIndex = 10;
            this.DgvAbastecimento.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAbastecimento_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Id_Veiculo
            // 
            this.Id_Veiculo.DataPropertyName = "Id_Veiculo";
            this.Id_Veiculo.HeaderText = "Id_Veiculo";
            this.Id_Veiculo.Name = "Id_Veiculo";
            this.Id_Veiculo.ReadOnly = true;
            this.Id_Veiculo.Visible = false;
            // 
            // Id_Motorista
            // 
            this.Id_Motorista.DataPropertyName = "Id_Motorista";
            this.Id_Motorista.HeaderText = "Id_Motorista";
            this.Id_Motorista.Name = "Id_Motorista";
            this.Id_Motorista.ReadOnly = true;
            this.Id_Motorista.Visible = false;
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            dataGridViewCellStyle11.Format = "d";
            dataGridViewCellStyle11.NullValue = null;
            this.Data.DefaultCellStyle = dataGridViewCellStyle11;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N2";
            dataGridViewCellStyle12.NullValue = null;
            this.Valor.DefaultCellStyle = dataGridViewCellStyle12;
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            // 
            // KmInicial
            // 
            this.KmInicial.DataPropertyName = "KmInicial";
            this.KmInicial.HeaderText = "KmInicial";
            this.KmInicial.Name = "KmInicial";
            this.KmInicial.ReadOnly = true;
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            // 
            // LblAbastecimento
            // 
            this.LblAbastecimento.AutoSize = true;
            this.LblAbastecimento.Location = new System.Drawing.Point(19, 269);
            this.LblAbastecimento.Name = "LblAbastecimento";
            this.LblAbastecimento.Size = new System.Drawing.Size(125, 13);
            this.LblAbastecimento.TabIndex = 11;
            this.LblAbastecimento.Text = "Abastecimento - 000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(19, 243);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(408, 21);
            this.TxtPesquisa.TabIndex = 9;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(346, 13);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(87, 23);
            this.BtnGravar.TabIndex = 6;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(346, 42);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(87, 23);
            this.BtnAlterar.TabIndex = 7;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(346, 71);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(87, 23);
            this.BtnExcluir.TabIndex = 8;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // MktData
            // 
            this.MktData.Location = new System.Drawing.Point(236, 128);
            this.MktData.Mask = "00/00/0000";
            this.MktData.Name = "MktData";
            this.MktData.Size = new System.Drawing.Size(101, 21);
            this.MktData.TabIndex = 4;
            this.MktData.ValidatingType = typeof(System.DateTime);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(236, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Data";
            // 
            // UCVeiculoMotorista
            // 
            this.UCVeiculoMotorista.Location = new System.Drawing.Point(19, 13);
            this.UCVeiculoMotorista.Name = "UCVeiculoMotorista";
            this.UCVeiculoMotorista.Size = new System.Drawing.Size(320, 88);
            this.UCVeiculoMotorista.TabIndex = 1;
            this.UCVeiculoMotorista.KmFinal += new QuemPegouOVeiculo.USVeiculoMotorista.KmFimVeiculo(this.UCVeiculoMotorista_KmFinal);
            // 
            // UCDescricao
            // 
            this.UCDescricao.Location = new System.Drawing.Point(19, 155);
            this.UCDescricao.Name = "UCDescricao";
            this.UCDescricao.Size = new System.Drawing.Size(408, 69);
            this.UCDescricao.TabIndex = 5;
            // 
            // UCValor
            // 
            this.UCValor.Location = new System.Drawing.Point(127, 111);
            this.UCValor.Name = "UCValor";
            this.UCValor.Size = new System.Drawing.Size(103, 40);
            this.UCValor.TabIndex = 3;
            // 
            // TxtLitros
            // 
            this.TxtLitros.Location = new System.Drawing.Point(346, 128);
            this.TxtLitros.Name = "TxtLitros";
            this.TxtLitros.Size = new System.Drawing.Size(81, 21);
            this.TxtLitros.TabIndex = 17;
            this.TxtLitros.Text = "0,00";
            this.TxtLitros.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtLitros.TextChanged += new System.EventHandler(this.TxtLitros_TextChanged);
            this.TxtLitros.Enter += new System.EventHandler(this.TxtLitros_Enter);
            this.TxtLitros.Leave += new System.EventHandler(this.TxtLitros_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Litros";
            // 
            // FrmContCombustivel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 449);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtLitros);
            this.Controls.Add(this.UCValor);
            this.Controls.Add(this.UCDescricao);
            this.Controls.Add(this.UCVeiculoMotorista);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.MktData);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.TxtPesquisa);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LblAbastecimento);
            this.Controls.Add(this.DgvAbastecimento);
            this.Controls.Add(this.TxtKmInicial);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContCombustivel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle Combustivel";
            this.Load += new System.EventHandler(this.FrmContCombustivel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvAbastecimento)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtKmInicial;
        private System.Windows.Forms.DataGridView DgvAbastecimento;
        private System.Windows.Forms.Label LblAbastecimento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.MaskedTextBox MktData;
        private System.Windows.Forms.Label label8;
        private USVeiculoMotorista UCVeiculoMotorista;
        private UCDescricao UCDescricao;
        private UCValor UCValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Veiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Motorista;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn KmInicial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.TextBox TxtLitros;
        private System.Windows.Forms.Label label1;
    }
}