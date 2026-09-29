namespace FleetManagement
{
    partial class MaintenanceReportForm
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
            this.RbPeriod = new System.Windows.Forms.RadioButton();
            this.RbVehicle = new System.Windows.Forms.RadioButton();
            this.RbGeral = new System.Windows.Forms.RadioButton();
            this.GbVehicle = new System.Windows.Forms.GroupBox();
            this.VehicleControl = new FleetManagement.VehicleControl();
            this.GbPeriod = new System.Windows.Forms.GroupBox();
            this.MktEndDate = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MktStartDate = new System.Windows.Forms.MaskedTextBox();
            this.BtnList = new System.Windows.Forms.Button();
            this.GbOpcoes.SuspendLayout();
            this.GbVehicle.SuspendLayout();
            this.GbPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbOpcoes
            // 
            this.GbOpcoes.Controls.Add(this.RbPeriod);
            this.GbOpcoes.Controls.Add(this.RbVehicle);
            this.GbOpcoes.Controls.Add(this.RbGeral);
            this.GbOpcoes.Location = new System.Drawing.Point(12, 13);
            this.GbOpcoes.Name = "GbOpcoes";
            this.GbOpcoes.Size = new System.Drawing.Size(281, 56);
            this.GbOpcoes.TabIndex = 0;
            this.GbOpcoes.TabStop = false;
            this.GbOpcoes.Text = "Opções";
            // 
            // RbPeriod
            // 
            this.RbPeriod.AutoSize = true;
            this.RbPeriod.Location = new System.Drawing.Point(130, 19);
            this.RbPeriod.Name = "RbPeriod";
            this.RbPeriod.Size = new System.Drawing.Size(63, 17);
            this.RbPeriod.TabIndex = 2;
            this.RbPeriod.Text = "Período";
            this.RbPeriod.UseVisualStyleBackColor = true;
            this.RbPeriod.CheckedChanged += new System.EventHandler(this.RbPeriod_CheckedChanged);
            // 
            // RbVehicle
            // 
            this.RbVehicle.AutoSize = true;
            this.RbVehicle.Location = new System.Drawing.Point(62, 19);
            this.RbVehicle.Name = "RbVehicle";
            this.RbVehicle.Size = new System.Drawing.Size(62, 17);
            this.RbVehicle.TabIndex = 1;
            this.RbVehicle.Text = "Veículo";
            this.RbVehicle.UseVisualStyleBackColor = true;
            this.RbVehicle.CheckedChanged += new System.EventHandler(this.RbVehicle_CheckedChanged);
            // 
            // RbGeral
            // 
            this.RbGeral.AutoSize = true;
            this.RbGeral.Checked = true;
            this.RbGeral.Location = new System.Drawing.Point(6, 19);
            this.RbGeral.Name = "RbGeral";
            this.RbGeral.Size = new System.Drawing.Size(50, 17);
            this.RbGeral.TabIndex = 0;
            this.RbGeral.TabStop = true;
            this.RbGeral.Text = "Geral";
            this.RbGeral.UseVisualStyleBackColor = true;
            this.RbGeral.CheckedChanged += new System.EventHandler(this.RbGeral_CheckedChanged);
            // 
            // GbVehicle
            // 
            this.GbVehicle.Controls.Add(this.VehicleControl);
            this.GbVehicle.Enabled = false;
            this.GbVehicle.Location = new System.Drawing.Point(12, 75);
            this.GbVehicle.Name = "GbVehicle";
            this.GbVehicle.Size = new System.Drawing.Size(281, 79);
            this.GbVehicle.TabIndex = 1;
            this.GbVehicle.TabStop = false;
            // 
            // VehicleControl
            // 
            this.VehicleControl.Location = new System.Drawing.Point(6, 19);
            this.VehicleControl.Name = "VehicleControl";
            this.VehicleControl.Size = new System.Drawing.Size(265, 38);
            this.VehicleControl.TabIndex = 0;
            // 
            // GbPeriod
            // 
            this.GbPeriod.Controls.Add(this.MktEndDate);
            this.GbPeriod.Controls.Add(this.label1);
            this.GbPeriod.Controls.Add(this.MktStartDate);
            this.GbPeriod.Enabled = false;
            this.GbPeriod.Location = new System.Drawing.Point(12, 160);
            this.GbPeriod.Name = "GbPeriod";
            this.GbPeriod.Size = new System.Drawing.Size(281, 53);
            this.GbPeriod.TabIndex = 2;
            this.GbPeriod.TabStop = false;
            this.GbPeriod.Text = "Período";
            // 
            // MktEndDate
            // 
            this.MktEndDate.Location = new System.Drawing.Point(152, 19);
            this.MktEndDate.Mask = "00/00/0000";
            this.MktEndDate.Name = "MktEndDate";
            this.MktEndDate.Size = new System.Drawing.Size(100, 20);
            this.MktEndDate.TabIndex = 1;
            this.MktEndDate.ValidatingType = typeof(System.DateTime);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "a";
            // 
            // MktStartDate
            // 
            this.MktStartDate.Location = new System.Drawing.Point(28, 19);
            this.MktStartDate.Mask = "00/00/0000";
            this.MktStartDate.Name = "MktStartDate";
            this.MktStartDate.Size = new System.Drawing.Size(100, 20);
            this.MktStartDate.TabIndex = 0;
            this.MktStartDate.ValidatingType = typeof(System.DateTime);
            // 
            // BtnList
            // 
            this.BtnList.Location = new System.Drawing.Point(299, 13);
            this.BtnList.Name = "BtnList";
            this.BtnList.Size = new System.Drawing.Size(75, 23);
            this.BtnList.TabIndex = 3;
            this.BtnList.Text = "Listar";
            this.BtnList.UseVisualStyleBackColor = true;
            this.BtnList.Click += new System.EventHandler(this.BtnList_Click);
            // 
            // MaintenanceReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 227);
            this.Controls.Add(this.BtnList);
            this.Controls.Add(this.GbPeriod);
            this.Controls.Add(this.GbVehicle);
            this.Controls.Add(this.GbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MaintenanceReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Controle Manutenção";
            this.GbOpcoes.ResumeLayout(false);
            this.GbOpcoes.PerformLayout();
            this.GbVehicle.ResumeLayout(false);
            this.GbPeriod.ResumeLayout(false);
            this.GbPeriod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbOpcoes;
        private System.Windows.Forms.RadioButton RbPeriod;
        private System.Windows.Forms.RadioButton RbVehicle;
        private System.Windows.Forms.RadioButton RbGeral;
        private System.Windows.Forms.GroupBox GbVehicle;
        private VehicleControl VehicleControl;
        private System.Windows.Forms.GroupBox GbPeriod;
        private System.Windows.Forms.MaskedTextBox MktEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktStartDate;
        private System.Windows.Forms.Button BtnList;
    }
}