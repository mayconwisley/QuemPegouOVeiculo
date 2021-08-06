namespace QuemPegouOVeiculo
{
    partial class FrmCadMotorista
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
            this.TxtNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtCNH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MktVencCNH = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtCategCNH = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtRG = new System.Windows.Forms.TextBox();
            this.CbAtivo = new System.Windows.Forms.CheckBox();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.DgvMotoristas = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumCNH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VencimentoCNH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoriaCNH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ativo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblMotorista = new System.Windows.Forms.Label();
            this.MktCPF = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMotoristas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome";
            // 
            // TxtNome
            // 
            this.TxtNome.Location = new System.Drawing.Point(12, 25);
            this.TxtNome.MaxLength = 200;
            this.TxtNome.Name = "TxtNome";
            this.TxtNome.Size = new System.Drawing.Size(248, 21);
            this.TxtNome.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Num. CNH";
            // 
            // TxtCNH
            // 
            this.TxtCNH.Location = new System.Drawing.Point(12, 65);
            this.TxtCNH.MaxLength = 11;
            this.TxtCNH.Name = "TxtCNH";
            this.TxtCNH.Size = new System.Drawing.Size(142, 21);
            this.TxtCNH.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Vencimento";
            // 
            // MktVencCNH
            // 
            this.MktVencCNH.Location = new System.Drawing.Point(160, 65);
            this.MktVencCNH.Mask = "00/00/0000";
            this.MktVencCNH.Name = "MktVencCNH";
            this.MktVencCNH.Size = new System.Drawing.Size(100, 21);
            this.MktVencCNH.TabIndex = 2;
            this.MktVencCNH.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Categ.";
            // 
            // TxtCategCNH
            // 
            this.TxtCategCNH.Location = new System.Drawing.Point(12, 105);
            this.TxtCategCNH.MaxLength = 2;
            this.TxtCategCNH.Name = "TxtCategCNH";
            this.TxtCategCNH.Size = new System.Drawing.Size(41, 21);
            this.TxtCategCNH.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "CPF";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "RG";
            // 
            // TxtRG
            // 
            this.TxtRG.Location = new System.Drawing.Point(140, 145);
            this.TxtRG.MaxLength = 20;
            this.TxtRG.Name = "TxtRG";
            this.TxtRG.Size = new System.Drawing.Size(120, 21);
            this.TxtRG.TabIndex = 6;
            // 
            // CbAtivo
            // 
            this.CbAtivo.AutoSize = true;
            this.CbAtivo.Checked = true;
            this.CbAtivo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbAtivo.Location = new System.Drawing.Point(90, 107);
            this.CbAtivo.Name = "CbAtivo";
            this.CbAtivo.Size = new System.Drawing.Size(55, 17);
            this.CbAtivo.TabIndex = 4;
            this.CbAtivo.Text = "Ativo";
            this.CbAtivo.UseVisualStyleBackColor = true;
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(294, 23);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(75, 23);
            this.BtnGravar.TabIndex = 7;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(294, 52);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(75, 23);
            this.BtnAlterar.TabIndex = 8;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(294, 81);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(75, 23);
            this.BtnExcluir.TabIndex = 9;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(12, 211);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(357, 21);
            this.TxtPesquisa.TabIndex = 10;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
            // 
            // DgvMotoristas
            // 
            this.DgvMotoristas.AllowUserToAddRows = false;
            this.DgvMotoristas.AllowUserToDeleteRows = false;
            this.DgvMotoristas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvMotoristas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvMotoristas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvMotoristas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvMotoristas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Nome,
            this.NumCNH,
            this.VencimentoCNH,
            this.CategoriaCNH,
            this.CPF,
            this.RG,
            this.Ativo});
            this.DgvMotoristas.Location = new System.Drawing.Point(12, 257);
            this.DgvMotoristas.MultiSelect = false;
            this.DgvMotoristas.Name = "DgvMotoristas";
            this.DgvMotoristas.ReadOnly = true;
            this.DgvMotoristas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvMotoristas.Size = new System.Drawing.Size(357, 150);
            this.DgvMotoristas.TabIndex = 11;
            this.DgvMotoristas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMotoristas_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 65;
            // 
            // NumCNH
            // 
            this.NumCNH.DataPropertyName = "NumCNH";
            this.NumCNH.HeaderText = "Num_CNH";
            this.NumCNH.Name = "NumCNH";
            this.NumCNH.ReadOnly = true;
            this.NumCNH.Width = 90;
            // 
            // VencimentoCNH
            // 
            this.VencimentoCNH.DataPropertyName = "VencimentoCNH";
            this.VencimentoCNH.HeaderText = "Vencimento_CNH";
            this.VencimentoCNH.Name = "VencimentoCNH";
            this.VencimentoCNH.ReadOnly = true;
            this.VencimentoCNH.Width = 130;
            // 
            // CategoriaCNH
            // 
            this.CategoriaCNH.DataPropertyName = "CategoriaCNH";
            this.CategoriaCNH.HeaderText = "Categoria_CNH";
            this.CategoriaCNH.Name = "CategoriaCNH";
            this.CategoriaCNH.ReadOnly = true;
            this.CategoriaCNH.Width = 120;
            // 
            // CPF
            // 
            this.CPF.DataPropertyName = "CPF";
            this.CPF.HeaderText = "CPF";
            this.CPF.Name = "CPF";
            this.CPF.ReadOnly = true;
            this.CPF.Width = 54;
            // 
            // RG
            // 
            this.RG.DataPropertyName = "RG";
            this.RG.HeaderText = "RG";
            this.RG.Name = "RG";
            this.RG.ReadOnly = true;
            this.RG.Width = 49;
            // 
            // Ativo
            // 
            this.Ativo.DataPropertyName = "Ativo";
            this.Ativo.HeaderText = "Ativo";
            this.Ativo.Name = "Ativo";
            this.Ativo.ReadOnly = true;
            this.Ativo.Width = 61;
            // 
            // LblMotorista
            // 
            this.LblMotorista.AutoSize = true;
            this.LblMotorista.Location = new System.Drawing.Point(12, 241);
            this.LblMotorista.Name = "LblMotorista";
            this.LblMotorista.Size = new System.Drawing.Size(99, 13);
            this.LblMotorista.TabIndex = 19;
            this.LblMotorista.Text = "Motoristas - 000";
            // 
            // MktCPF
            // 
            this.MktCPF.Location = new System.Drawing.Point(12, 145);
            this.MktCPF.Mask = "000\\.000\\.000-00";
            this.MktCPF.Name = "MktCPF";
            this.MktCPF.Size = new System.Drawing.Size(122, 21);
            this.MktCPF.TabIndex = 5;
            // 
            // FrmCadMotorista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 426);
            this.Controls.Add(this.MktCPF);
            this.Controls.Add(this.LblMotorista);
            this.Controls.Add(this.DgvMotoristas);
            this.Controls.Add(this.TxtPesquisa);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.CbAtivo);
            this.Controls.Add(this.TxtRG);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtCategCNH);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MktVencCNH);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtCNH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtNome);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCadMotorista";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Motorista";
            this.Load += new System.EventHandler(this.FrmCadMotorista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvMotoristas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtNome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtCNH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox MktVencCNH;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtCategCNH;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtRG;
        private System.Windows.Forms.CheckBox CbAtivo;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.DataGridView DgvMotoristas;
        private System.Windows.Forms.Label LblMotorista;
        private System.Windows.Forms.MaskedTextBox MktCPF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumCNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn VencimentoCNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoriaCNH;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPF;
        private System.Windows.Forms.DataGridViewTextBoxColumn RG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ativo;
    }
}