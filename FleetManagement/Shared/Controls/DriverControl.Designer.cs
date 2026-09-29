namespace FleetManagement
{
    partial class DriverControl
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
            this.CbxDriver = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CbxDriver
            // 
            this.CbxDriver.DisplayMember = "Name";
            this.CbxDriver.FormattingEnabled = true;
            this.CbxDriver.Location = new System.Drawing.Point(0, 17);
            this.CbxDriver.Name = "CbxDriver";
            this.CbxDriver.Size = new System.Drawing.Size(265, 21);
            this.CbxDriver.TabIndex = 26;
            this.CbxDriver.ValueMember = "Id";
            this.CbxDriver.SelectedIndexChanged += new System.EventHandler(this.CbxDriver_SelectedIndexChanged);
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
            // DriverControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CbxDriver);
            this.Controls.Add(this.label2);
            this.Name = "DriverControl";
            this.Size = new System.Drawing.Size(266, 38);
            this.Load += new System.EventHandler(this.UCDriver_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox CbxDriver;
    }
}
