namespace FleetManagement
{
    partial class VehicleMovementReportForm
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
            this.BtnList = new System.Windows.Forms.Button();
            this.GbVehicle = new System.Windows.Forms.GroupBox();
            this.VehicleControl = new FleetManagement.VehicleControl();
            this.GbDriver = new System.Windows.Forms.GroupBox();
            this.DriverControl = new FleetManagement.DriverControl();
            this.GbPeriod = new System.Windows.Forms.GroupBox();
            this.CbDriver = new System.Windows.Forms.CheckBox();
            this.CbVehicle = new System.Windows.Forms.CheckBox();
            this.CbOpenOnly = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MktEndDate = new System.Windows.Forms.MaskedTextBox();
            this.MktStartDate = new System.Windows.Forms.MaskedTextBox();
            this.RbArrivalDate = new System.Windows.Forms.RadioButton();
            this.RbDepartureDate = new System.Windows.Forms.RadioButton();
            this.GbOpcoes.SuspendLayout();
            this.GbVehicle.SuspendLayout();
            this.GbDriver.SuspendLayout();
            this.GbPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbOpcoes
            // 
            this.GbOpcoes.Controls.Add(this.RbVeiMot);
            this.GbOpcoes.Controls.Add(this.RbPeriod);
            this.GbOpcoes.Controls.Add(this.RbDriver);
            this.GbOpcoes.Controls.Add(this.RbVehicle);
            this.GbOpcoes.Controls.Add(this.RbGeral);
            this.GbOpcoes.Location = new System.Drawing.Point(11, 14);
            this.GbOpcoes.Name = "GbOpcoes";
            this.GbOpcoes.Size = new System.Drawing.Size(343, 52);
            this.GbOpcoes.TabIndex = 0;
            this.GbOpcoes.TabStop = false;
            this.GbOpcoes.Text = "Opções";
            // 
            // RbVeiMot
            // 
            this.RbVeiMot.AutoSize = true;
            this.RbVeiMot.Location = new System.Drawing.Point(273, 19);
            this.RbVeiMot.Name = "RbVeiMot";
            this.RbVeiMot.Size = new System.Drawing.Size(63, 17);
            this.RbVeiMot.TabIndex = 5;
            this.RbVeiMot.Text = "Vei/Mot";
            this.RbVeiMot.UseVisualStyleBackColor = true;
            this.RbVeiMot.CheckedChanged += new System.EventHandler(this.RbVeiMot_CheckedChanged);
            // 
            // RbPeriod
            // 
            this.RbPeriod.AutoSize = true;
            this.RbPeriod.Location = new System.Drawing.Point(204, 19);
            this.RbPeriod.Name = "RbPeriod";
            this.RbPeriod.Size = new System.Drawing.Size(63, 17);
            this.RbPeriod.TabIndex = 4;
            this.RbPeriod.Text = "Período";
            this.RbPeriod.UseVisualStyleBackColor = true;
            this.RbPeriod.CheckedChanged += new System.EventHandler(this.RbPeriod_CheckedChanged);
            // 
            // RbDriver
            // 
            this.RbDriver.AutoSize = true;
            this.RbDriver.Location = new System.Drawing.Point(130, 19);
            this.RbDriver.Name = "RbDriver";
            this.RbDriver.Size = new System.Drawing.Size(68, 17);
            this.RbDriver.TabIndex = 3;
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
            this.RbVehicle.TabIndex = 2;
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
            this.RbGeral.TabIndex = 1;
            this.RbGeral.TabStop = true;
            this.RbGeral.Text = "Geral";
            this.RbGeral.UseVisualStyleBackColor = true;
            this.RbGeral.CheckedChanged += new System.EventHandler(this.RbGeral_CheckedChanged);
            // 
            // BtnList
            // 
            this.BtnList.Location = new System.Drawing.Point(360, 14);
            this.BtnList.Name = "BtnList";
            this.BtnList.Size = new System.Drawing.Size(75, 23);
            this.BtnList.TabIndex = 4;
            this.BtnList.Text = "Listar";
            this.BtnList.UseVisualStyleBackColor = true;
            this.BtnList.Click += new System.EventHandler(this.BtnList_Click);
            // 
            // GbVehicle
            // 
            this.GbVehicle.Controls.Add(this.VehicleControl);
            this.GbVehicle.Enabled = false;
            this.GbVehicle.Location = new System.Drawing.Point(11, 193);
            this.GbVehicle.Name = "GbVehicle";
            this.GbVehicle.Size = new System.Drawing.Size(343, 65);
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
            // GbDriver
            // 
            this.GbDriver.Controls.Add(this.DriverControl);
            this.GbDriver.Enabled = false;
            this.GbDriver.Location = new System.Drawing.Point(11, 257);
            this.GbDriver.Name = "GbDriver";
            this.GbDriver.Size = new System.Drawing.Size(343, 59);
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
            // GbPeriod
            // 
            this.GbPeriod.Controls.Add(this.CbDriver);
            this.GbPeriod.Controls.Add(this.CbVehicle);
            this.GbPeriod.Controls.Add(this.CbOpenOnly);
            this.GbPeriod.Controls.Add(this.label1);
            this.GbPeriod.Controls.Add(this.MktEndDate);
            this.GbPeriod.Controls.Add(this.MktStartDate);
            this.GbPeriod.Controls.Add(this.RbArrivalDate);
            this.GbPeriod.Controls.Add(this.RbDepartureDate);
            this.GbPeriod.Enabled = false;
            this.GbPeriod.Location = new System.Drawing.Point(11, 75);
            this.GbPeriod.Name = "GbPeriod";
            this.GbPeriod.Size = new System.Drawing.Size(343, 112);
            this.GbPeriod.TabIndex = 1;
            this.GbPeriod.TabStop = false;
            this.GbPeriod.Text = "Período";
            // 
            // CbDriver
            // 
            this.CbDriver.AutoSize = true;
            this.CbDriver.Location = new System.Drawing.Point(257, 16);
            this.CbDriver.Name = "CbDriver";
            this.CbDriver.Size = new System.Drawing.Size(69, 17);
            this.CbDriver.TabIndex = 3;
            this.CbDriver.Text = "Motorista";
            this.CbDriver.UseVisualStyleBackColor = true;
            this.CbDriver.CheckedChanged += new System.EventHandler(this.CbDriver_CheckedChanged);
            // 
            // CbVehicle
            // 
            this.CbVehicle.AutoSize = true;
            this.CbVehicle.Location = new System.Drawing.Point(188, 16);
            this.CbVehicle.Name = "CbVehicle";
            this.CbVehicle.Size = new System.Drawing.Size(63, 17);
            this.CbVehicle.TabIndex = 2;
            this.CbVehicle.Text = "Veículo";
            this.CbVehicle.UseVisualStyleBackColor = true;
            this.CbVehicle.CheckedChanged += new System.EventHandler(this.CbVehicle_CheckedChanged);
            // 
            // CbOpenOnly
            // 
            this.CbOpenOnly.AutoSize = true;
            this.CbOpenOnly.Location = new System.Drawing.Point(6, 89);
            this.CbOpenOnly.Name = "CbOpenOnly";
            this.CbOpenOnly.Size = new System.Drawing.Size(124, 17);
            this.CbOpenOnly.TabIndex = 6;
            this.CbOpenOnly.Text = "Data Chegada Vazia";
            this.CbOpenOnly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "a";
            // 
            // MktEndDate
            // 
            this.MktEndDate.Location = new System.Drawing.Point(174, 48);
            this.MktEndDate.Mask = "00/00/0000";
            this.MktEndDate.Name = "MktEndDate";
            this.MktEndDate.Size = new System.Drawing.Size(100, 20);
            this.MktEndDate.TabIndex = 5;
            this.MktEndDate.ValidatingType = typeof(System.DateTime);
            // 
            // MktStartDate
            // 
            this.MktStartDate.Location = new System.Drawing.Point(49, 48);
            this.MktStartDate.Mask = "00/00/0000";
            this.MktStartDate.Name = "MktStartDate";
            this.MktStartDate.Size = new System.Drawing.Size(100, 20);
            this.MktStartDate.TabIndex = 4;
            this.MktStartDate.ValidatingType = typeof(System.DateTime);
            // 
            // RbArrivalDate
            // 
            this.RbArrivalDate.AutoSize = true;
            this.RbArrivalDate.Location = new System.Drawing.Point(88, 15);
            this.RbArrivalDate.Name = "RbArrivalDate";
            this.RbArrivalDate.Size = new System.Drawing.Size(94, 17);
            this.RbArrivalDate.TabIndex = 1;
            this.RbArrivalDate.Text = "Data Chegada";
            this.RbArrivalDate.UseVisualStyleBackColor = true;
            // 
            // RbDepartureDate
            // 
            this.RbDepartureDate.AutoSize = true;
            this.RbDepartureDate.Checked = true;
            this.RbDepartureDate.Location = new System.Drawing.Point(4, 15);
            this.RbDepartureDate.Name = "RbDepartureDate";
            this.RbDepartureDate.Size = new System.Drawing.Size(80, 17);
            this.RbDepartureDate.TabIndex = 0;
            this.RbDepartureDate.TabStop = true;
            this.RbDepartureDate.Text = "Data Saída";
            this.RbDepartureDate.UseVisualStyleBackColor = true;
            // 
            // VehicleMovementReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 330);
            this.Controls.Add(this.GbPeriod);
            this.Controls.Add(this.GbDriver);
            this.Controls.Add(this.GbVehicle);
            this.Controls.Add(this.BtnList);
            this.Controls.Add(this.GbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleMovementReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Controle Veículo";
            this.GbOpcoes.ResumeLayout(false);
            this.GbOpcoes.PerformLayout();
            this.GbVehicle.ResumeLayout(false);
            this.GbDriver.ResumeLayout(false);
            this.GbDriver.PerformLayout();
            this.GbPeriod.ResumeLayout(false);
            this.GbPeriod.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbOpcoes;
        private System.Windows.Forms.RadioButton RbGeral;
        private System.Windows.Forms.RadioButton RbDriver;
        private System.Windows.Forms.RadioButton RbVehicle;
        private System.Windows.Forms.RadioButton RbPeriod;
        private System.Windows.Forms.Button BtnList;
        private System.Windows.Forms.GroupBox GbVehicle;
        private System.Windows.Forms.GroupBox GbDriver;
        private System.Windows.Forms.GroupBox GbPeriod;
        private System.Windows.Forms.RadioButton RbArrivalDate;
        private System.Windows.Forms.RadioButton RbDepartureDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktEndDate;
        private System.Windows.Forms.MaskedTextBox MktStartDate;
        private VehicleControl VehicleControl;
        private DriverControl DriverControl;
        private System.Windows.Forms.CheckBox CbOpenOnly;
        private System.Windows.Forms.CheckBox CbDriver;
        private System.Windows.Forms.CheckBox CbVehicle;
        private System.Windows.Forms.RadioButton RbVeiMot;
    }
}