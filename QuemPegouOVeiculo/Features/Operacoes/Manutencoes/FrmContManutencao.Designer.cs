namespace QuemPegouOVeiculo
{
    partial class FrmContManutencao
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
            this.UCVeiculo = new QuemPegouOVeiculo.UCVeiculo();
            this.label1 = new System.Windows.Forms.Label();
            this.MktData = new System.Windows.Forms.MaskedTextBox();
            this.UCValor = new QuemPegouOVeiculo.UCValor();
            this.UCDescricao = new QuemPegouOVeiculo.UCDescricao();
            this.BtnGravar = new System.Windows.Forms.Button();
            this.BtnAlterar = new System.Windows.Forms.Button();
            this.BtnExcluir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtPesquisar = new System.Windows.Forms.TextBox();
            this.DgvManutencao = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id_Veiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblManutencao = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvManutencao)).BeginInit();
            this.SuspendLayout();
            // 
            // UCVeiculo
            // 
            this.UCVeiculo.Location = new System.Drawing.Point(14, 12);
            this.UCVeiculo.Name = "UCVeiculo";
            this.UCVeiculo.Size = new System.Drawing.Size(314, 45);
            this.UCVeiculo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data";
            // 
            // MktData
            // 
            this.MktData.Location = new System.Drawing.Point(14, 76);
            this.MktData.Mask = "00/00/0000";
            this.MktData.Name = "MktData";
            this.MktData.Size = new System.Drawing.Size(100, 21);
            this.MktData.TabIndex = 1;
            this.MktData.ValidatingType = typeof(System.DateTime);
            // 
            // UCValor
            // 
            this.UCValor.Location = new System.Drawing.Point(141, 58);
            this.UCValor.Name = "UCValor";
            this.UCValor.Size = new System.Drawing.Size(120, 40);
            this.UCValor.TabIndex = 2;
            // 
            // UCDescricao
            // 
            this.UCDescricao.Location = new System.Drawing.Point(14, 118);
            this.UCDescricao.Name = "UCDescricao";
            this.UCDescricao.Size = new System.Drawing.Size(420, 69);
            this.UCDescricao.TabIndex = 3;
            // 
            // BtnGravar
            // 
            this.BtnGravar.Location = new System.Drawing.Point(348, 12);
            this.BtnGravar.Name = "BtnGravar";
            this.BtnGravar.Size = new System.Drawing.Size(87, 23);
            this.BtnGravar.TabIndex = 4;
            this.BtnGravar.Text = "&Gravar";
            this.BtnGravar.UseVisualStyleBackColor = true;
            this.BtnGravar.Click += new System.EventHandler(this.BtnGravar_Click);
            // 
            // BtnAlterar
            // 
            this.BtnAlterar.Enabled = false;
            this.BtnAlterar.Location = new System.Drawing.Point(348, 41);
            this.BtnAlterar.Name = "BtnAlterar";
            this.BtnAlterar.Size = new System.Drawing.Size(87, 23);
            this.BtnAlterar.TabIndex = 5;
            this.BtnAlterar.Text = "&Alterar";
            this.BtnAlterar.UseVisualStyleBackColor = true;
            this.BtnAlterar.Click += new System.EventHandler(this.BtnAlterar_Click);
            // 
            // BtnExcluir
            // 
            this.BtnExcluir.Enabled = false;
            this.BtnExcluir.Location = new System.Drawing.Point(348, 70);
            this.BtnExcluir.Name = "BtnExcluir";
            this.BtnExcluir.Size = new System.Drawing.Size(87, 23);
            this.BtnExcluir.TabIndex = 6;
            this.BtnExcluir.Text = "&Excluir";
            this.BtnExcluir.UseVisualStyleBackColor = true;
            this.BtnExcluir.Click += new System.EventHandler(this.BtnExcluir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pesquisar";
            // 
            // TxtPesquisar
            // 
            this.TxtPesquisar.Location = new System.Drawing.Point(14, 206);
            this.TxtPesquisar.Name = "TxtPesquisar";
            this.TxtPesquisar.Size = new System.Drawing.Size(420, 21);
            this.TxtPesquisar.TabIndex = 7;
            this.TxtPesquisar.TextChanged += new System.EventHandler(this.TxtPesquisar_TextChanged);
            // 
            // DgvManutencao
            // 
            this.DgvManutencao.AllowUserToAddRows = false;
            this.DgvManutencao.AllowUserToDeleteRows = false;
            this.DgvManutencao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvManutencao.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvManutencao.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvManutencao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvManutencao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Id_Veiculo,
            this.Data,
            this.Valor,
            this.Descricao,
            this.Modelo});
            this.DgvManutencao.Location = new System.Drawing.Point(14, 256);
            this.DgvManutencao.MultiSelect = false;
            this.DgvManutencao.Name = "DgvManutencao";
            this.DgvManutencao.ReadOnly = true;
            this.DgvManutencao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvManutencao.Size = new System.Drawing.Size(421, 150);
            this.DgvManutencao.TabIndex = 8;
            this.DgvManutencao.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvManutencao_CellDoubleClick);
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
            // Data
            // 
            this.Data.DataPropertyName = "Data";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.Data.DefaultCellStyle = dataGridViewCellStyle1;
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.ReadOnly = true;
            this.Data.Width = 59;
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.Valor.DefaultCellStyle = dataGridViewCellStyle2;
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            this.Valor.Width = 61;
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Width = 88;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.Width = 72;
            // 
            // LblManutencao
            // 
            this.LblManutencao.AutoSize = true;
            this.LblManutencao.Location = new System.Drawing.Point(14, 240);
            this.LblManutencao.Name = "LblManutencao";
            this.LblManutencao.Size = new System.Drawing.Size(109, 13);
            this.LblManutencao.TabIndex = 9;
            this.LblManutencao.Text = "Manutenção - 000";
            // 
            // FrmContManutencao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 422);
            this.Controls.Add(this.LblManutencao);
            this.Controls.Add(this.DgvManutencao);
            this.Controls.Add(this.TxtPesquisar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnExcluir);
            this.Controls.Add(this.BtnAlterar);
            this.Controls.Add(this.BtnGravar);
            this.Controls.Add(this.UCDescricao);
            this.Controls.Add(this.UCValor);
            this.Controls.Add(this.MktData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UCVeiculo);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmContManutencao";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de Manutenção";
            this.Load += new System.EventHandler(this.FrmContManutencao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvManutencao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UCVeiculo UCVeiculo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktData;
        private UCValor UCValor;
        private UCDescricao UCDescricao;
        private System.Windows.Forms.Button BtnGravar;
        private System.Windows.Forms.Button BtnAlterar;
        private System.Windows.Forms.Button BtnExcluir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtPesquisar;
        private System.Windows.Forms.DataGridView DgvManutencao;
        private System.Windows.Forms.Label LblManutencao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id_Veiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Modelo;
    }
}