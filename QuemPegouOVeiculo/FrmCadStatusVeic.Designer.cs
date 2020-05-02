namespace QuemPegouOVeiculo
{
    partial class FrmCadStatusVeic
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
            this.label2 = new System.Windows.Forms.Label();
            this.MktDtInicio = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MktDtFinal = new System.Windows.Forms.MaskedTextBox();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtPesquisa = new System.Windows.Forms.TextBox();
            this.DgvVeiculo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Veiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataHoraInicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataHoraFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LkLblDtInicioAtual = new System.Windows.Forms.LinkLabel();
            this.LkLblDtFinalAtual = new System.Windows.Forms.LinkLabel();
            this.UCDescricao = new QuemPegouOVeiculo.UCDescricao();
            this.UCVeiculo = new QuemPegouOVeiculo.UCVeiculo();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVeiculo)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dt. Inicio";
            // 
            // MktDtInicio
            // 
            this.MktDtInicio.Location = new System.Drawing.Point(13, 95);
            this.MktDtInicio.Mask = "00/00/0000 00:00";
            this.MktDtInicio.Name = "MktDtInicio";
            this.MktDtInicio.Size = new System.Drawing.Size(121, 21);
            this.MktDtInicio.TabIndex = 2;
            this.MktDtInicio.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dt. Final";
            // 
            // MktDtFinal
            // 
            this.MktDtFinal.Location = new System.Drawing.Point(202, 95);
            this.MktDtFinal.Mask = "00/00/0000 00:00";
            this.MktDtFinal.Name = "MktDtFinal";
            this.MktDtFinal.Size = new System.Drawing.Size(121, 21);
            this.MktDtFinal.TabIndex = 3;
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(352, 12);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(75, 23);
            this.BtnGravar.TabIndex = 5;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(352, 41);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(75, 23);
            this.BtnAlterar.TabIndex = 6;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(352, 70);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(75, 23);
            this.BtnExcluir.TabIndex = 7;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Pesquisa";
            // 
            // TxtPesquisa
            // 
            this.TxtPesquisa.Location = new System.Drawing.Point(13, 210);
            this.TxtPesquisa.Name = "TxtPesquisa";
            this.TxtPesquisa.Size = new System.Drawing.Size(414, 21);
            this.TxtPesquisa.TabIndex = 8;
            this.TxtPesquisa.TextChanged += new System.EventHandler(this.TxtPesquisa_TextChanged);
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
            this.Id_Veiculo,
            this.Modelo,
            this.Descricao,
            this.DataHoraInicio,
            this.DataHoraFinal});
            this.DgvVeiculo.Location = new System.Drawing.Point(13, 260);
            this.DgvVeiculo.MultiSelect = false;
            this.DgvVeiculo.Name = "DgvVeiculo";
            this.DgvVeiculo.ReadOnly = true;
            this.DgvVeiculo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVeiculo.Size = new System.Drawing.Size(414, 150);
            this.DgvVeiculo.TabIndex = 9;
            this.DgvVeiculo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVeiculo_CellDoubleClick);
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
            // Modelo
            // 
            this.Modelo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.Width = 72;
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descrição";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Width = 88;
            // 
            // DataHoraInicio
            // 
            this.DataHoraInicio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataHoraInicio.DataPropertyName = "DataHoraInicio";
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            this.DataHoraInicio.DefaultCellStyle = dataGridViewCellStyle1;
            this.DataHoraInicio.HeaderText = "Data_Hora_Inicio";
            this.DataHoraInicio.Name = "DataHoraInicio";
            this.DataHoraInicio.ReadOnly = true;
            this.DataHoraInicio.Width = 131;
            // 
            // DataHoraFinal
            // 
            this.DataHoraFinal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DataHoraFinal.DataPropertyName = "DataHoraFinal";
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            this.DataHoraFinal.DefaultCellStyle = dataGridViewCellStyle2;
            this.DataHoraFinal.HeaderText = "Data_Hora_Final";
            this.DataHoraFinal.Name = "DataHoraFinal";
            this.DataHoraFinal.ReadOnly = true;
            this.DataHoraFinal.Width = 126;
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Location = new System.Drawing.Point(13, 244);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(77, 13);
            this.LblStatus.TabIndex = 14;
            this.LblStatus.Text = "Status - 000";
            // 
            // LkLblDtInicioAtual
            // 
            this.LkLblDtInicioAtual.AutoSize = true;
            this.LkLblDtInicioAtual.Location = new System.Drawing.Point(78, 79);
            this.LkLblDtInicioAtual.Name = "LkLblDtInicioAtual";
            this.LkLblDtInicioAtual.Size = new System.Drawing.Size(36, 13);
            this.LkLblDtInicioAtual.TabIndex = 18;
            this.LkLblDtInicioAtual.TabStop = true;
            this.LkLblDtInicioAtual.Text = "Atual";
            this.LkLblDtInicioAtual.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LkLblDtInicioAtual_LinkClicked);
            // 
            // LkLblDtFinalAtual
            // 
            this.LkLblDtFinalAtual.AutoSize = true;
            this.LkLblDtFinalAtual.Location = new System.Drawing.Point(259, 79);
            this.LkLblDtFinalAtual.Name = "LkLblDtFinalAtual";
            this.LkLblDtFinalAtual.Size = new System.Drawing.Size(36, 13);
            this.LkLblDtFinalAtual.TabIndex = 19;
            this.LkLblDtFinalAtual.TabStop = true;
            this.LkLblDtFinalAtual.Text = "Atual";
            this.LkLblDtFinalAtual.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LkLblDtFinalAtual_LinkClicked);
            // 
            // UCDescricao
            // 
            this.UCDescricao.Location = new System.Drawing.Point(13, 122);
            this.UCDescricao.Name = "UCDescricao";
            this.UCDescricao.Size = new System.Drawing.Size(414, 69);
            this.UCDescricao.TabIndex = 4;
            // 
            // UCVeiculo
            // 
            this.UCVeiculo.Location = new System.Drawing.Point(13, 12);
            this.UCVeiculo.Name = "UCVeiculo";
            this.UCVeiculo.Size = new System.Drawing.Size(310, 45);
            this.UCVeiculo.TabIndex = 1;
            // 
            // FrmCadStatusVeic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 423);
            this.Controls.Add(this.LkLblDtFinalAtual);
            this.Controls.Add(this.LkLblDtInicioAtual);
            this.Controls.Add(this.UCDescricao);
            this.Controls.Add(this.UCVeiculo);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.DgvVeiculo);
            this.Controls.Add(this.TxtPesquisa);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.MktDtFinal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MktDtInicio);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCadStatusVeic";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Status Veículo";
            this.Load += new System.EventHandler(this.FrmCadStatusVeic_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVeiculo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox MktDtInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox MktDtFinal;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtPesquisa;
        private System.Windows.Forms.DataGridView DgvVeiculo;
        private System.Windows.Forms.Label LblStatus;
        private UCVeiculo UCVeiculo;
        private UCDescricao UCDescricao;
        private System.Windows.Forms.LinkLabel LkLblDtInicioAtual;
        private System.Windows.Forms.LinkLabel LkLblDtFinalAtual;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Veiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataHoraInicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataHoraFinal;
    }
}