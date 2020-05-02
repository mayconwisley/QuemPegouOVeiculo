namespace QuemPegouOVeiculo
{
    partial class FrmCadVeiculo
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtPlaca = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtModelo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtChassi = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtRenavam = new System.Windows.Forms.TextBox();
            this.CbAtivo = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.DgvVeiculo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Placa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chassi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Renavam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblVeiculos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVeiculo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Placa";
            // 
            // TxtPlaca
            // 
            this.TxtPlaca.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPlaca.Location = new System.Drawing.Point(14, 26);
            this.TxtPlaca.MaxLength = 8;
            this.TxtPlaca.Name = "TxtPlaca";
            this.TxtPlaca.Size = new System.Drawing.Size(111, 21);
            this.TxtPlaca.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Modelo";
            // 
            // TxtModelo
            // 
            this.TxtModelo.Location = new System.Drawing.Point(14, 66);
            this.TxtModelo.MaxLength = 255;
            this.TxtModelo.Name = "TxtModelo";
            this.TxtModelo.Size = new System.Drawing.Size(186, 21);
            this.TxtModelo.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Chassi";
            // 
            // TxtChassi
            // 
            this.TxtChassi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtChassi.Location = new System.Drawing.Point(14, 106);
            this.TxtChassi.MaxLength = 22;
            this.TxtChassi.Name = "TxtChassi";
            this.TxtChassi.Size = new System.Drawing.Size(221, 21);
            this.TxtChassi.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Renavam";
            // 
            // TxtRenavam
            // 
            this.TxtRenavam.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtRenavam.Location = new System.Drawing.Point(241, 106);
            this.TxtRenavam.MaxLength = 12;
            this.TxtRenavam.Name = "TxtRenavam";
            this.TxtRenavam.Size = new System.Drawing.Size(120, 21);
            this.TxtRenavam.TabIndex = 4;
            // 
            // CbAtivo
            // 
            this.CbAtivo.AutoSize = true;
            this.CbAtivo.Checked = true;
            this.CbAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbAtivo.Location = new System.Drawing.Point(17, 143);
            this.CbAtivo.Name = "CbAtivo";
            this.CbAtivo.Size = new System.Drawing.Size(55, 17);
            this.CbAtivo.TabIndex = 5;
            this.CbAtivo.Text = "Ativo";
            this.CbAtivo.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(14, 193);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(453, 21);
            this.TxtPesquisa.TabIndex = 9;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(392, 13);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(75, 23);
            this.BtnGravar.TabIndex = 6;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(392, 42);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(75, 23);
            this.BtnAlterar.TabIndex = 7;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(392, 71);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(75, 23);
            this.BtnExcluir.TabIndex = 8;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // DgvVeiculo
            // 
            this.DgvVeiculo.AllowUserToAddRows = false;
            this.DgvVeiculo.AllowUserToDeleteRows = false;
            this.DgvVeiculo.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvVeiculo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvVeiculo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvVeiculo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Placa,
            this.Modelo,
            this.Chassi,
            this.Renavam,
            this.Status});
            this.DgvVeiculo.Location = new System.Drawing.Point(14, 243);
            this.DgvVeiculo.MultiSelect = false;
            this.DgvVeiculo.Name = "DgvVeiculo";
            this.DgvVeiculo.ReadOnly = true;
            this.DgvVeiculo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVeiculo.Size = new System.Drawing.Size(453, 165);
            this.DgvVeiculo.TabIndex = 10;
            this.DgvVeiculo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVeiculo_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Placa
            // 
            this.Placa.DataPropertyName = "Placa";
            this.Placa.HeaderText = "Placa";
            this.Placa.Name = "Placa";
            this.Placa.ReadOnly = true;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            // 
            // Chassi
            // 
            this.Chassi.DataPropertyName = "Chassi";
            this.Chassi.HeaderText = "Chassi";
            this.Chassi.Name = "Chassi";
            this.Chassi.ReadOnly = true;
            // 
            // Renavam
            // 
            this.Renavam.DataPropertyName = "Renavam";
            this.Renavam.HeaderText = "Renavam";
            this.Renavam.Name = "Renavam";
            this.Renavam.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // LblVeiculos
            // 
            this.LblVeiculos.AutoSize = true;
            this.LblVeiculos.Location = new System.Drawing.Point(14, 227);
            this.LblVeiculos.Name = "LblVeiculos";
            this.LblVeiculos.Size = new System.Drawing.Size(87, 13);
            this.LblVeiculos.TabIndex = 15;
            this.LblVeiculos.Text = "Veiculos - 000";
            // 
            // FrmCadVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 419);
            this.Controls.Add(this.LblVeiculos);
            this.Controls.Add(this.DgvVeiculo);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.TxtPesquisa);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CbAtivo);
            this.Controls.Add(this.TxtRenavam);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtChassi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtModelo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtPlaca);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCadVeiculo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Veículo";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmCadVeiculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVeiculo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPlaca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtModelo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtChassi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtRenavam;
        private System.Windows.Forms.CheckBox CbAtivo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.DataGridView DgvVeiculo;
        private System.Windows.Forms.Label LblVeiculos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Placa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Chassi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Renavam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}