namespace QuemPegouOVeiculo
{
    partial class USVeiculoMotorista
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CbxMotorista = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CbxVeiculo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CbxMotorista
            // 
            this.CbxMotorista.DisplayMember = "Nome";
            this.CbxMotorista.FormattingEnabled = true;
            this.CbxMotorista.Location = new System.Drawing.Point(0, 57);
            this.CbxMotorista.Name = "CbxMotorista";
            this.CbxMotorista.Size = new System.Drawing.Size(262, 21);
            this.CbxMotorista.TabIndex = 1;
            this.CbxMotorista.ValueMember = "Id";
            this.CbxMotorista.SelectedIndexChanged += new System.EventHandler(this.CbxMotorista_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Motorista";
            // 
            // CbxVeiculo
            // 
            this.CbxVeiculo.DisplayMember = "Modelo";
            this.CbxVeiculo.FormattingEnabled = true;
            this.CbxVeiculo.Location = new System.Drawing.Point(0, 17);
            this.CbxVeiculo.Name = "CbxVeiculo";
            this.CbxVeiculo.Size = new System.Drawing.Size(262, 21);
            this.CbxVeiculo.TabIndex = 0;
            this.CbxVeiculo.ValueMember = "Id";
            this.CbxVeiculo.SelectedIndexChanged += new System.EventHandler(this.CbxVeiculo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Veículo";
            // 
            // USVeiculoMotorista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CbxMotorista);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CbxVeiculo);
            this.Controls.Add(this.label1);
            this.Name = "USVeiculoMotorista";
            this.Size = new System.Drawing.Size(262, 78);
            this.Load += new System.EventHandler(this.USVeiculoMotorista_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox CbxMotorista;
        public System.Windows.Forms.ComboBox CbxVeiculo;
    }
}
