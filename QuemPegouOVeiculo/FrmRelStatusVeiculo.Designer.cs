namespace QuemPegouOVeiculo
{
    partial class FrmRelStatusVeiculo
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
            this.RbPeriodo = new System.Windows.Forms.RadioButton();
            this.RbVeiculo = new System.Windows.Forms.RadioButton();
            this.RbTodos = new System.Windows.Forms.RadioButton();
            this.GbPeriodo = new System.Windows.Forms.GroupBox();
            this.RbDataInicio = new System.Windows.Forms.RadioButton();
            this.RbDataFinal = new System.Windows.Forms.RadioButton();
            this.CbListaDataNull = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MktDataFinal = new System.Windows.Forms.MaskedTextBox();
            this.MktDataInicio = new System.Windows.Forms.MaskedTextBox();
            this.BtnListar = new System.Windows.Forms.Button();
            this.GbVeiculo = new System.Windows.Forms.GroupBox();
            this.UCVeiculo = new QuemPegouOVeiculo.UCVeiculo();
            this.GbOpcoes.SuspendLayout();
            this.GbPeriodo.SuspendLayout();
            this.GbVeiculo.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbOpcoes
            // 
            this.GbOpcoes.Controls.Add(this.RbPeriodo);
            this.GbOpcoes.Controls.Add(this.RbVeiculo);
            this.GbOpcoes.Controls.Add(this.RbTodos);
            this.GbOpcoes.Location = new System.Drawing.Point(9, 10);
            this.GbOpcoes.Name = "GbOpcoes";
            this.GbOpcoes.Size = new System.Drawing.Size(277, 48);
            this.GbOpcoes.TabIndex = 0;
            this.GbOpcoes.TabStop = false;
            this.GbOpcoes.Text = "Opção";
            // 
            // RbPeriodo
            // 
            this.RbPeriodo.AutoSize = true;
            this.RbPeriodo.Location = new System.Drawing.Point(133, 19);
            this.RbPeriodo.Name = "RbPeriodo";
            this.RbPeriodo.Size = new System.Drawing.Size(63, 17);
            this.RbPeriodo.TabIndex = 3;
            this.RbPeriodo.Text = "Período";
            this.RbPeriodo.UseVisualStyleBackColor = true;
            this.RbPeriodo.CheckedChanged += new System.EventHandler(this.RbPeriodo_CheckedChanged);
            // 
            // RbVeiculo
            // 
            this.RbVeiculo.AutoSize = true;
            this.RbVeiculo.Location = new System.Drawing.Point(67, 19);
            this.RbVeiculo.Name = "RbVeiculo";
            this.RbVeiculo.Size = new System.Drawing.Size(62, 17);
            this.RbVeiculo.TabIndex = 2;
            this.RbVeiculo.Text = "Veículo";
            this.RbVeiculo.UseVisualStyleBackColor = true;
            this.RbVeiculo.CheckedChanged += new System.EventHandler(this.RbVeiculo_CheckedChanged);
            // 
            // RbTodos
            // 
            this.RbTodos.AutoSize = true;
            this.RbTodos.Checked = true;
            this.RbTodos.Location = new System.Drawing.Point(6, 19);
            this.RbTodos.Name = "RbTodos";
            this.RbTodos.Size = new System.Drawing.Size(55, 17);
            this.RbTodos.TabIndex = 1;
            this.RbTodos.Text = "Todos";
            this.RbTodos.UseVisualStyleBackColor = true;
            this.RbTodos.CheckedChanged += new System.EventHandler(this.RbTodos_CheckedChanged);
            // 
            // GbPeriodo
            // 
            this.GbPeriodo.Controls.Add(this.RbDataInicio);
            this.GbPeriodo.Controls.Add(this.RbDataFinal);
            this.GbPeriodo.Controls.Add(this.CbListaDataNull);
            this.GbPeriodo.Controls.Add(this.label1);
            this.GbPeriodo.Controls.Add(this.MktDataFinal);
            this.GbPeriodo.Controls.Add(this.MktDataInicio);
            this.GbPeriodo.Enabled = false;
            this.GbPeriodo.Location = new System.Drawing.Point(9, 127);
            this.GbPeriodo.Name = "GbPeriodo";
            this.GbPeriodo.Size = new System.Drawing.Size(277, 93);
            this.GbPeriodo.TabIndex = 6;
            this.GbPeriodo.TabStop = false;
            this.GbPeriodo.Text = "Período";
            // 
            // RbDataInicio
            // 
            this.RbDataInicio.AutoSize = true;
            this.RbDataInicio.Checked = true;
            this.RbDataInicio.Location = new System.Drawing.Point(6, 19);
            this.RbDataInicio.Name = "RbDataInicio";
            this.RbDataInicio.Size = new System.Drawing.Size(78, 17);
            this.RbDataInicio.TabIndex = 7;
            this.RbDataInicio.Text = "Data Inicial";
            this.RbDataInicio.UseVisualStyleBackColor = true;
            this.RbDataInicio.CheckedChanged += new System.EventHandler(this.RbDataInicio_CheckedChanged);
            // 
            // RbDataFinal
            // 
            this.RbDataFinal.AutoSize = true;
            this.RbDataFinal.Location = new System.Drawing.Point(90, 19);
            this.RbDataFinal.Name = "RbDataFinal";
            this.RbDataFinal.Size = new System.Drawing.Size(73, 17);
            this.RbDataFinal.TabIndex = 8;
            this.RbDataFinal.Text = "Data Final";
            this.RbDataFinal.UseVisualStyleBackColor = true;
            this.RbDataFinal.CheckedChanged += new System.EventHandler(this.RbDataFinal_CheckedChanged);
            // 
            // CbListaDataNull
            // 
            this.CbListaDataNull.AutoSize = true;
            this.CbListaDataNull.Location = new System.Drawing.Point(6, 65);
            this.CbListaDataNull.Name = "CbListaDataNull";
            this.CbListaDataNull.Size = new System.Drawing.Size(136, 17);
            this.CbListaDataNull.TabIndex = 11;
            this.CbListaDataNull.Text = "Listar Data Final Vazias";
            this.CbListaDataNull.UseVisualStyleBackColor = true;
            this.CbListaDataNull.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "a";
            // 
            // MktDataFinal
            // 
            this.MktDataFinal.Location = new System.Drawing.Point(131, 42);
            this.MktDataFinal.Mask = "00/00/0000";
            this.MktDataFinal.Name = "MktDataFinal";
            this.MktDataFinal.Size = new System.Drawing.Size(100, 20);
            this.MktDataFinal.TabIndex = 10;
            this.MktDataFinal.ValidatingType = typeof(System.DateTime);
            // 
            // MktDataInicio
            // 
            this.MktDataInicio.Location = new System.Drawing.Point(6, 42);
            this.MktDataInicio.Mask = "00/00/0000";
            this.MktDataInicio.Name = "MktDataInicio";
            this.MktDataInicio.Size = new System.Drawing.Size(100, 20);
            this.MktDataInicio.TabIndex = 9;
            this.MktDataInicio.ValidatingType = typeof(System.DateTime);
            // 
            // BtnListar
            // 
            this.BtnListar.Location = new System.Drawing.Point(306, 10);
            this.BtnListar.Name = "BtnListar";
            this.BtnListar.Size = new System.Drawing.Size(75, 23);
            this.BtnListar.TabIndex = 12;
            this.BtnListar.Text = "Listar";
            this.BtnListar.UseVisualStyleBackColor = true;
            this.BtnListar.Click += new System.EventHandler(this.BtnListar_Click);
            // 
            // GbVeiculo
            // 
            this.GbVeiculo.Controls.Add(this.UCVeiculo);
            this.GbVeiculo.Enabled = false;
            this.GbVeiculo.Location = new System.Drawing.Point(9, 64);
            this.GbVeiculo.Name = "GbVeiculo";
            this.GbVeiculo.Size = new System.Drawing.Size(277, 57);
            this.GbVeiculo.TabIndex = 4;
            this.GbVeiculo.TabStop = false;
            // 
            // UCVeiculo
            // 
            this.UCVeiculo.Location = new System.Drawing.Point(6, 10);
            this.UCVeiculo.Name = "UCVeiculo";
            this.UCVeiculo.Size = new System.Drawing.Size(265, 38);
            this.UCVeiculo.TabIndex = 5;
            // 
            // FrmRelStatusVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 229);
            this.Controls.Add(this.GbVeiculo);
            this.Controls.Add(this.BtnListar);
            this.Controls.Add(this.GbPeriodo);
            this.Controls.Add(this.GbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRelStatusVeiculo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Status Veículo";
            this.GbOpcoes.ResumeLayout(false);
            this.GbOpcoes.PerformLayout();
            this.GbPeriodo.ResumeLayout(false);
            this.GbPeriodo.PerformLayout();
            this.GbVeiculo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbOpcoes;
        private System.Windows.Forms.RadioButton RbVeiculo;
        private System.Windows.Forms.RadioButton RbTodos;
        private UCVeiculo UCVeiculo;
        private System.Windows.Forms.RadioButton RbPeriodo;
        private System.Windows.Forms.GroupBox GbPeriodo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktDataFinal;
        private System.Windows.Forms.MaskedTextBox MktDataInicio;
        private System.Windows.Forms.Button BtnListar;
        private System.Windows.Forms.CheckBox CbListaDataNull;
        private System.Windows.Forms.RadioButton RbDataInicio;
        private System.Windows.Forms.RadioButton RbDataFinal;
        private System.Windows.Forms.GroupBox GbVeiculo;
    }
}