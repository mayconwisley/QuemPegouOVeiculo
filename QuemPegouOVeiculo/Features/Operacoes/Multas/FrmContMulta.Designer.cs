namespace QuemPegouOVeiculo
{
    partial class FrmContMulta
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
            this.label8 = new System.Windows.Forms.Label();
            this.MktData = new System.Windows.Forms.MaskedTextBox();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LblMultas = new System.Windows.Forms.Label();
            this.DgvMultas = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Veiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Motorista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pontos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxtPontos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UCValor = new QuemPegouOVeiculo.UCValor();
            this.UCDescricao = new QuemPegouOVeiculo.UCDescricao();
            this.UCVeiculoMotorista = new QuemPegouOVeiculo.USVeiculoMotorista();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMultas)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(220, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Data";
            // 
            // MktData
            // 
            this.MktData.Location = new System.Drawing.Point(220, 124);
            this.MktData.Mask = "00/00/0000";
            this.MktData.Name = "MktData";
            this.MktData.Size = new System.Drawing.Size(89, 21);
            this.MktData.TabIndex = 3;
            this.MktData.ValidatingType = typeof(System.DateTime);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(339, 72);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(87, 23);
            this.BtnExcluir.TabIndex = 7;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(339, 43);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(87, 23);
            this.BtnAlterar.TabIndex = 6;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(339, 14);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(87, 23);
            this.BtnGravar.TabIndex = 5;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(13, 239);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(408, 21);
            this.TxtPesquisa.TabIndex = 8;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Pesquisa";
            // 
            // LblMultas
            // 
            this.LblMultas.AutoSize = true;
            this.LblMultas.Location = new System.Drawing.Point(13, 265);
            this.LblMultas.Name = "LblMultas";
            this.LblMultas.Size = new System.Drawing.Size(77, 13);
            this.LblMultas.TabIndex = 28;
            this.LblMultas.Text = "Multas - 000";
            // 
            // DgvMultas
            // 
            this.DgvMultas.AllowUserToAddRows = false;
            this.DgvMultas.AllowUserToDeleteRows = false;
            this.DgvMultas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvMultas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvMultas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvMultas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvMultas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Id_Veiculo,
            this.Id_Motorista,
            this.Data,
            this.Valor,
            this.Pontos,
            this.Nome,
            this.Modelo,
            this.Descricao});
            this.DgvMultas.Location = new System.Drawing.Point(13, 281);
            this.DgvMultas.MultiSelect = false;
            this.DgvMultas.Name = "DgvMultas";
            this.DgvMultas.ReadOnly = true;
            this.DgvMultas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvMultas.Size = new System.Drawing.Size(408, 150);
            this.DgvMultas.TabIndex = 9;
            this.DgvMultas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMultas_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 44;
            // 
            // Id_Veiculo
            // 
            this.Id_Veiculo.DataPropertyName = "Id_Veiculo";
            this.Id_Veiculo.HeaderText = "Id_Veiculo";
            this.Id_Veiculo.Name = "Id_Veiculo";
            this.Id_Veiculo.ReadOnly = true;
            this.Id_Veiculo.Visible = false;
            this.Id_Veiculo.Width = 91;
            // 
            // Id_Motorista
            // 
            this.Id_Motorista.DataPropertyName = "Id_Motorista";
            this.Id_Motorista.HeaderText = "Id_Motorista";
            this.Id_Motorista.Name = "Id_Motorista";
            this.Id_Motorista.ReadOnly = true;
            this.Id_Motorista.Visible = false;
            this.Id_Motorista.Width = 103;
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Width = 59;
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Valor.DefaultCellStyle = dataGridViewCellStyle1;
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            this.Valor.Width = 61;
            // 
            // Pontos
            // 
            this.Pontos.DataPropertyName = "Pontos";
            this.Pontos.HeaderText = "Pontos";
            this.Pontos.Name = "Pontos";
            this.Pontos.ReadOnly = true;
            this.Pontos.Width = 70;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 65;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.Width = 72;
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Width = 88;
            // 
            // TxtPontos
            // 
            this.TxtPontos.Location = new System.Drawing.Point(13, 124);
            this.TxtPontos.Name = "TxtPontos";
            this.TxtPontos.Size = new System.Drawing.Size(100, 21);
            this.TxtPontos.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Pontos";
            // 
            // UCValor
            // 
            this.UCValor.Location = new System.Drawing.Point(119, 107);
            this.UCValor.Name = "UCValor";
            this.UCValor.Size = new System.Drawing.Size(95, 40);
            this.UCValor.TabIndex = 2;
            // 
            // UCDescricao
            // 
            this.UCDescricao.Location = new System.Drawing.Point(13, 151);
            this.UCDescricao.Name = "UCDescricao";
            this.UCDescricao.Size = new System.Drawing.Size(419, 69);
            this.UCDescricao.TabIndex = 4;
            // 
            // UCVeiculoMotorista
            // 
            this.UCVeiculoMotorista.Location = new System.Drawing.Point(13, 12);
            this.UCVeiculoMotorista.Name = "UCVeiculoMotorista";
            this.UCVeiculoMotorista.Size = new System.Drawing.Size(318, 88);
            this.UCVeiculoMotorista.TabIndex = 0;
            // 
            // FrmContMulta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 445);
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
            this.Controls.Add(this.LblMultas);
            this.Controls.Add(this.DgvMultas);
            this.Controls.Add(this.TxtPontos);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContMulta";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle Multa";
            this.Load += new System.EventHandler(this.FrmContMulta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvMultas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox MktData;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblMultas;
        private System.Windows.Forms.DataGridView DgvMultas;
        private System.Windows.Forms.TextBox TxtPontos;
        private System.Windows.Forms.Label label3;
        private USVeiculoMotorista UCVeiculoMotorista;
        private UCDescricao UCDescricao;
        private UCValor UCValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Veiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Motorista;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pontos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
    }
}