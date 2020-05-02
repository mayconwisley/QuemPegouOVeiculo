namespace QuemPegouOVeiculo
{
    partial class FrmAtuControleVeiculo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.DgvControleVeiculo)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lista de Veículos que não chegaram";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 384);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "label2";
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
            this.DgvControleVeiculo.Location = new System.Drawing.Point(12, 25);
            this.DgvControleVeiculo.MultiSelect = false;
            this.DgvControleVeiculo.Name = "DgvControleVeiculo";
            this.DgvControleVeiculo.ReadOnly = true;
            this.DgvControleVeiculo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvControleVeiculo.Size = new System.Drawing.Size(805, 356);
            this.DgvControleVeiculo.TabIndex = 12;
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
            this.DataHoraSaida.Width = 105;
            // 
            // Modelo
            // 
            this.Modelo.DataPropertyName = "Modelo";
            this.Modelo.HeaderText = "Modelo";
            this.Modelo.Name = "Modelo";
            this.Modelo.ReadOnly = true;
            this.Modelo.Width = 67;
            // 
            // Nome
            // 
            this.Nome.DataPropertyName = "Nome";
            this.Nome.HeaderText = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.ReadOnly = true;
            this.Nome.Width = 60;
            // 
            // DataHoraChegada
            // 
            this.DataHoraChegada.DataPropertyName = "DataHoraChegada";
            this.DataHoraChegada.HeaderText = "DataHoraChegada";
            this.DataHoraChegada.Name = "DataHoraChegada";
            this.DataHoraChegada.ReadOnly = true;
            this.DataHoraChegada.Width = 121;
            // 
            // Dias
            // 
            this.Dias.DataPropertyName = "Dias";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Dias.DefaultCellStyle = dataGridViewCellStyle9;
            this.Dias.HeaderText = "Dias";
            this.Dias.Name = "Dias";
            this.Dias.ReadOnly = true;
            this.Dias.Width = 53;
            // 
            // Horas
            // 
            this.Horas.DataPropertyName = "Horas";
            this.Horas.HeaderText = "Horas";
            this.Horas.Name = "Horas";
            this.Horas.ReadOnly = true;
            this.Horas.Width = 60;
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descricao";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Width = 80;
            // 
            // KmInicial
            // 
            this.KmInicial.DataPropertyName = "KmInicial";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KmInicial.DefaultCellStyle = dataGridViewCellStyle10;
            this.KmInicial.HeaderText = "KmInicial";
            this.KmInicial.Name = "KmInicial";
            this.KmInicial.ReadOnly = true;
            this.KmInicial.Width = 74;
            // 
            // KmFinal
            // 
            this.KmFinal.DataPropertyName = "KmFinal";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KmFinal.DefaultCellStyle = dataGridViewCellStyle11;
            this.KmFinal.HeaderText = "KmFinal";
            this.KmFinal.Name = "KmFinal";
            this.KmFinal.ReadOnly = true;
            this.KmFinal.Width = 69;
            // 
            // KmTotal
            // 
            this.KmTotal.DataPropertyName = "KmTotal";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.KmTotal.DefaultCellStyle = dataGridViewCellStyle12;
            this.KmTotal.HeaderText = "KmTotal";
            this.KmTotal.Name = "KmTotal";
            this.KmTotal.ReadOnly = true;
            this.KmTotal.Width = 71;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 62;
            // 
            // FrmAtuControleVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 426);
            this.Controls.Add(this.DgvControleVeiculo);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAtuControleVeiculo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualização Controle Veiculo";
            this.Load += new System.EventHandler(this.FrmAtuControleVeiculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvControleVeiculo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView DgvControleVeiculo;
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
    }
}