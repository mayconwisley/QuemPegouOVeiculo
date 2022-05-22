namespace QuemPegouOVeiculo
{
    partial class FrmCadVencCNH
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
            this.MktDtVencimento = new System.Windows.Forms.MaskedTextBox();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.CbVencida = new System.Windows.Forms.CheckBox();
            this.DgvVencCNH = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Motorista = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nome = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblVencimento = new System.Windows.Forms.Label();
            this.UCMotorista = new QuemPegouOVeiculo.UCMotorista();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVencCNH)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dt. Vencimento";
            // 
            // MktDtVencimento
            // 
            this.MktDtVencimento.Location = new System.Drawing.Point(18, 89);
            this.MktDtVencimento.Mask = "00/00/0000";
            this.MktDtVencimento.Name = "MktDtVencimento";
            this.MktDtVencimento.Size = new System.Drawing.Size(100, 21);
            this.MktDtVencimento.TabIndex = 1;
            this.MktDtVencimento.ValidatingType = typeof(System.DateTime);
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(346, 10);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(75, 23);
            this.BtnGravar.TabIndex = 3;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(346, 39);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(75, 23);
            this.BtnAlterar.TabIndex = 4;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(346, 68);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(75, 23);
            this.BtnExcluir.TabIndex = 5;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(18, 141);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(403, 21);
            this.TxtPesquisa.TabIndex = 6;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
            // 
            // CbVencida
            // 
            this.CbVencida.AutoSize = true;
            this.CbVencida.Location = new System.Drawing.Point(135, 91);
            this.CbVencida.Name = "CbVencida";
            this.CbVencida.Size = new System.Drawing.Size(70, 17);
            this.CbVencida.TabIndex = 2;
            this.CbVencida.Text = "Vencida";
            this.CbVencida.UseVisualStyleBackColor = true;
            // 
            // DgvVencCNH
            // 
            this.DgvVencCNH.AllowUserToAddRows = false;
            this.DgvVencCNH.AllowUserToDeleteRows = false;
            this.DgvVencCNH.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvVencCNH.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvVencCNH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvVencCNH.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvVencCNH.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Id_Motorista,
            this.Nome,
            this.Data,
            this.Status});
            this.DgvVencCNH.Location = new System.Drawing.Point(18, 192);
            this.DgvVencCNH.MultiSelect = false;
            this.DgvVencCNH.Name = "DgvVencCNH";
            this.DgvVencCNH.ReadOnly = true;
            this.DgvVencCNH.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVencCNH.Size = new System.Drawing.Size(403, 150);
            this.DgvVencCNH.TabIndex = 7;
            this.DgvVencCNH.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVencCNH_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Id_Motorista
            // 
            this.Id_Motorista.DataPropertyName = "Id_Motorista";
            this.Id_Motorista.HeaderText = "Id_Motorista";
            this.Id_Motorista.Name = "Id_Motorista";
            this.Id_Motorista.ReadOnly = true;
            this.Id_Motorista.Visible = false;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 65;
            // 
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Width = 59;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 68;
            // 
            // LblVencimento
            // 
            this.LblVencimento.AutoSize = true;
            this.LblVencimento.Location = new System.Drawing.Point(18, 176);
            this.LblVencimento.Name = "LblVencimento";
            this.LblVencimento.Size = new System.Drawing.Size(107, 13);
            this.LblVencimento.TabIndex = 11;
            this.LblVencimento.Text = "Vencimento - 000";
            // 
            // UCMotorista
            // 
            this.UCMotorista.Location = new System.Drawing.Point(18, 10);
            this.UCMotorista.Name = "UCMotorista";
            this.UCMotorista.Size = new System.Drawing.Size(322, 44);
            this.UCMotorista.TabIndex = 0;
            // 
            // FrmCadVencCNH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 353);
            this.Controls.Add(this.UCMotorista);
            this.Controls.Add(this.LblVencimento);
            this.Controls.Add(this.DgvVencCNH);
            this.Controls.Add(this.CbVencida);
            this.Controls.Add(this.TxtPesquisa);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.MktDtVencimento);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCadVencCNH";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Vencimento CNH";
            this.Load += new System.EventHandler(this.FrmCadVencCNH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVencCNH)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox MktDtVencimento;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.CheckBox CbVencida;
        private System.Windows.Forms.DataGridView DgvVencCNH;
        private System.Windows.Forms.Label LblVencimento;
        private UCMotorista UCMotorista;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Motorista;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nome;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}