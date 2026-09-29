namespace FleetManagement
{
    partial class RefuelingReportForm
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
            this.RbVeiMot = new System.Windows.Forms.RadioButton();
            this.RbPeriod = new System.Windows.Forms.RadioButton();
            this.RbDriver = new System.Windows.Forms.RadioButton();
            this.RbVehicle = new System.Windows.Forms.RadioButton();
            this.RbGeral = new System.Windows.Forms.RadioButton();
            this.GbPeriod = new System.Windows.Forms.GroupBox();
            this.CbDriver = new System.Windows.Forms.CheckBox();
            this.CbVehicle = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MktEndDate = new System.Windows.Forms.MaskedTextBox();
            this.MktStartDate = new System.Windows.Forms.MaskedTextBox();
            this.GbDriver = new System.Windows.Forms.GroupBox();
            this.DriverControl = new FleetManagement.DriverControl();
            this.GbVehicle = new System.Windows.Forms.GroupBox();
            this.VehicleControl = new FleetManagement.VehicleControl();
            this.BtnList = new System.Windows.Forms.Button();
            this.GbOpcoes.SuspendLayout();
            this.GbPeriod.SuspendLayout();
            this.GbDriver.SuspendLayout();
            this.GbVehicle.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbOpcoes
            // 
            this.GbOpcoes.Controls.Add(this.RbVeiMot);
            this.GbOpcoes.Controls.Add(this.RbPeriod);
            this.GbOpcoes.Controls.Add(this.RbDriver);
            this.GbOpcoes.Controls.Add(this.RbVehicle);
            this.GbOpcoes.Controls.Add(this.RbGeral);
            this.GbOpcoes.Location = new System.Drawing.Point(12, 12);
            this.GbOpcoes.Name = "GbOpcoes";
            this.GbOpcoes.Size = new System.Drawing.Size(356, 57);
            this.GbOpcoes.TabIndex = 0;
            this.GbOpcoes.TabStop = false;
            this.GbOpcoes.Text = "Opções";
            // 
            // RbVeiMot
            // 
            this.RbVeiMot.AutoSize = true;
            this.RbVeiMot.Location = new System.Drawing.Point(274, 19);
            this.RbVeiMot.Name = "RbVeiMot";
            this.RbVeiMot.Size = new System.Drawing.Size(69, 17);
            this.RbVeiMot.TabIndex = 4;
            this.RbVeiMot.Text = "Vei / Mot";
            this.RbVeiMot.UseVisualStyleBackColor = true;
            this.RbVeiMot.CheckedChanged += new System.EventHandler(this.RbVeiMot_CheckedChanged);
            // 
            // RbPeriod
            // 
            this.RbPeriod.AutoSize = true;
            this.RbPeriod.Location = new System.Drawing.Point(204, 19);
            this.RbPeriod.Name = "RbPeriod";
            this.RbPeriod.Size = new System.Drawing.Size(61, 17);
            this.RbPeriod.TabIndex = 3;
            this.RbPeriod.Text = "Periodo";
            this.RbPeriod.UseVisualStyleBackColor = true;
            this.RbPeriod.CheckedChanged += new System.EventHandler(this.RbPeriod_CheckedChanged);
            // 
            // RbDriver
            // 
            this.RbDriver.AutoSize = true;
            this.RbDriver.Location = new System.Drawing.Point(130, 19);
            this.RbDriver.Name = "RbDriver";
            this.RbDriver.Size = new System.Drawing.Size(68, 17);
            this.RbDriver.TabIndex = 2;
            this.RbDriver.Text = "Motorista";
            this.RbDriver.UseVisualStyleBackColor = true;
            this.RbDriver.CheckedChanged += new System.EventHandler(this.RbDriver_CheckedChanged);
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
            // GbPeriod
            // 
            this.GbPeriod.Controls.Add(this.CbDriver);
            this.GbPeriod.Controls.Add(this.CbVehicle);
            this.GbPeriod.Controls.Add(this.label1);
            this.GbPeriod.Controls.Add(this.MktEndDate);
            this.GbPeriod.Controls.Add(this.MktStartDate);
            this.GbPeriod.Enabled = false;
            this.GbPeriod.Location = new System.Drawing.Point(12, 75);
            this.GbPeriod.Name = "GbPeriod";
            this.GbPeriod.Size = new System.Drawing.Size(356, 77);
            this.GbPeriod.TabIndex = 1;
            this.GbPeriod.TabStop = false;
            this.GbPeriod.Text = "Período";
            // 
            // CbDriver
            // 
            this.CbDriver.AutoSize = true;
            this.CbDriver.Location = new System.Drawing.Point(75, 19);
            this.CbDriver.Name = "CbDriver";
            this.CbDriver.Size = new System.Drawing.Size(69, 17);
            this.CbDriver.TabIndex = 10;
            this.CbDriver.Text = "Motorista";
            this.CbDriver.UseVisualStyleBackColor = true;
            this.CbDriver.CheckedChanged += new System.EventHandler(this.CbDriver_CheckedChanged);
            // 
            // CbVehicle
            // 
            this.CbVehicle.AutoSize = true;
            this.CbVehicle.Location = new System.Drawing.Point(6, 19);
            this.CbVehicle.Name = "CbVehicle";
            this.CbVehicle.Size = new System.Drawing.Size(63, 17);
            this.CbVehicle.TabIndex = 9;
            this.CbVehicle.Text = "Veículo";
            this.CbVehicle.UseVisualStyleBackColor = true;
            this.CbVehicle.CheckedChanged += new System.EventHandler(this.CbVehicle_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "a";
            // 
            // MktEndDate
            // 
            this.MktEndDate.Location = new System.Drawing.Point(131, 42);
            this.MktEndDate.Mask = "00/00/0000";
            this.MktEndDate.Name = "MktEndDate";
            this.MktEndDate.Size = new System.Drawing.Size(100, 20);
            this.MktEndDate.TabIndex = 1;
            this.MktEndDate.ValidatingType = typeof(System.DateTime);
            // 
            // MktStartDate
            // 
            this.MktStartDate.Location = new System.Drawing.Point(6, 42);
            this.MktStartDate.Mask = "00/00/0000";
            this.MktStartDate.Name = "MktStartDate";
            this.MktStartDate.Size = new System.Drawing.Size(100, 20);
            this.MktStartDate.TabIndex = 0;
            this.MktStartDate.ValidatingType = typeof(System.DateTime);
            // 
            // GbDriver
            // 
            this.GbDriver.Controls.Add(this.DriverControl);
            this.GbDriver.Enabled = false;
            this.GbDriver.Location = new System.Drawing.Point(12, 229);
            this.GbDriver.Name = "GbDriver";
            this.GbDriver.Size = new System.Drawing.Size(356, 59);
            this.GbDriver.TabIndex = 3;
            this.GbDriver.TabStop = false;
            // 
            // DriverControl
            // 
            this.DriverControl.AutoSize = true;
            this.DriverControl.Location = new System.Drawing.Point(6, 10);
            this.DriverControl.Name = "DriverControl";
            this.DriverControl.Size = new System.Drawing.Size(272, 41);
            this.DriverControl.TabIndex = 0;
            // 
            // GbVehicle
            // 
            this.GbVehicle.Controls.Add(this.VehicleControl);
            this.GbVehicle.Enabled = false;
            this.GbVehicle.Location = new System.Drawing.Point(12, 158);
            this.GbVehicle.Name = "GbVehicle";
            this.GbVehicle.Size = new System.Drawing.Size(356, 65);
            this.GbVehicle.TabIndex = 2;
            this.GbVehicle.TabStop = false;
            // 
            // VehicleControl
            // 
            this.VehicleControl.Location = new System.Drawing.Point(6, 10);
            this.VehicleControl.Name = "VehicleControl";
            this.VehicleControl.Size = new System.Drawing.Size(268, 38);
            this.VehicleControl.TabIndex = 0;
            // 
            // BtnList
            // 
            this.BtnList.Location = new System.Drawing.Point(374, 12);
            this.BtnList.Name = "BtnList";
            this.BtnList.Size = new System.Drawing.Size(75, 23);
            this.BtnList.TabIndex = 4;
            this.BtnList.Text = "Listar";
            this.BtnList.UseVisualStyleBackColor = true;
            this.BtnList.Click += new System.EventHandler(this.BtnList_Click);
            // 
            // RefuelingReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 303);
            this.Controls.Add(this.BtnList);
            this.Controls.Add(this.GbPeriod);
            this.Controls.Add(this.GbDriver);
            this.Controls.Add(this.GbVehicle);
            this.Controls.Add(this.GbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RefuelingReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Controle Combustível";
            this.GbOpcoes.ResumeLayout(false);
            this.GbOpcoes.PerformLayout();
            this.GbPeriod.ResumeLayout(false);
            this.GbPeriod.PerformLayout();
            this.GbDriver.ResumeLayout(false);
            this.GbDriver.PerformLayout();
            this.GbVehicle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbOpcoes;
        private System.Windows.Forms.RadioButton RbVeiMot;
        private System.Windows.Forms.RadioButton RbPeriod;
        private System.Windows.Forms.RadioButton RbDriver;
        private System.Windows.Forms.RadioButton RbVehicle;
        private System.Windows.Forms.RadioButton RbGeral;
        private System.Windows.Forms.GroupBox GbPeriod;
        private System.Windows.Forms.CheckBox CbDriver;
        private System.Windows.Forms.CheckBox CbVehicle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktEndDate;
        private System.Windows.Forms.MaskedTextBox MktStartDate;
        private System.Windows.Forms.GroupBox GbDriver;
        private DriverControl DriverControl;
        private System.Windows.Forms.GroupBox GbVehicle;
        private VehicleControl VehicleControl;
        private System.Windows.Forms.Button BtnList;
    }
}