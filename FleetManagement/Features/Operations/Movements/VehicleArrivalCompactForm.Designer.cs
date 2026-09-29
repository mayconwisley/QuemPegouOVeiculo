namespace FleetManagement
{
    partial class VehicleArrivalCompactForm
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
            this.MktDtArrival = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtFinalMileage = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Data Chegada";
            // 
            // MktDtArrival
            // 
            this.MktDtArrival.Location = new System.Drawing.Point(15, 30);
            this.MktDtArrival.Mask = "00/00/0000 90:00";
            this.MktDtArrival.Name = "MktDtArrival";
            this.MktDtArrival.Size = new System.Drawing.Size(111, 20);
            this.MktDtArrival.TabIndex = 0;
            this.MktDtArrival.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(129, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Km Final";
            // 
            // TxtFinalMileage
            // 
            this.TxtFinalMileage.Location = new System.Drawing.Point(132, 30);
            this.TxtFinalMileage.Name = "TxtFinalMileage";
            this.TxtFinalMileage.Size = new System.Drawing.Size(100, 20);
            this.TxtFinalMileage.TabIndex = 1;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(242, 28);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // VehicleArrivalCompactForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 64);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TxtFinalMileage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MktDtArrival);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleArrivalCompactForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle Veículo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VehicleArrivalCompactForm_FormClosing);
            this.Load += new System.EventHandler(this.VehicleArrivalCompactForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox MktDtArrival;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtFinalMileage;
        private System.Windows.Forms.Button BtnSave;
    }
}