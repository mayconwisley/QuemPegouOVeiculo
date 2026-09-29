namespace FleetManagement
{
    partial class VehicleControl
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
            this.CbxVehicle = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CbxVehicle
            // 
            this.CbxVehicle.DisplayMember = "Model";
            this.CbxVehicle.FormattingEnabled = true;
            this.CbxVehicle.Location = new System.Drawing.Point(0, 17);
            this.CbxVehicle.Name = "CbxVehicle";
            this.CbxVehicle.Size = new System.Drawing.Size(265, 21);
            this.CbxVehicle.TabIndex = 24;
            this.CbxVehicle.ValueMember = "Id";
            this.CbxVehicle.SelectedIndexChanged += new System.EventHandler(this.CbxVehicle_SelectedIndexChanged);
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
            // VehicleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CbxVehicle);
            this.Controls.Add(this.label1);
            this.Name = "VehicleControl";
            this.Size = new System.Drawing.Size(265, 38);
            this.Load += new System.EventHandler(this.UCVehicle_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox CbxVehicle;
    }
}
