namespace QuemPegouOVeiculo
{
    partial class UCMotorista
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
            this.SuspendLayout();
            // 
            // CbxMotorista
            // 
            this.CbxMotorista.DisplayMember = "Nome";
            this.CbxMotorista.FormattingEnabled = true;
            this.CbxMotorista.Location = new System.Drawing.Point(0, 17);
            this.CbxMotorista.Name = "CbxMotorista";
            this.CbxMotorista.Size = new System.Drawing.Size(265, 21);
            this.CbxMotorista.TabIndex = 26;
            this.CbxMotorista.ValueMember = "Id";
            this.CbxMotorista.SelectedIndexChanged += new System.EventHandler(this.CbxMotorista_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Motorista";
            // 
            // UCMotorista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CbxMotorista);
            this.Controls.Add(this.label2);
            this.Name = "UCMotorista";
            this.Size = new System.Drawing.Size(266, 38);
            this.Load += new System.EventHandler(this.UCMotorista_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox CbxMotorista;
    }
}
