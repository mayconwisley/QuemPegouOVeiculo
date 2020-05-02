namespace QuemPegouOVeiculo
{
    partial class UCVeiculo
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
            this.CbxVeiculo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CbxVeiculo
            // 
            this.CbxVeiculo.DisplayMember = "Modelo";
            this.CbxVeiculo.FormattingEnabled = true;
            this.CbxVeiculo.Location = new System.Drawing.Point(0, 17);
            this.CbxVeiculo.Name = "CbxVeiculo";
            this.CbxVeiculo.Size = new System.Drawing.Size(265, 21);
            this.CbxVeiculo.TabIndex = 24;
            this.CbxVeiculo.ValueMember = "Id";
            this.CbxVeiculo.SelectedIndexChanged += new System.EventHandler(this.CbxVeiculo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Veículo";
            // 
            // UCVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CbxVeiculo);
            this.Controls.Add(this.label1);
            this.Name = "UCVeiculo";
            this.Size = new System.Drawing.Size(265, 38);
            this.Load += new System.EventHandler(this.UCVeiculo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox CbxVeiculo;
    }
}
