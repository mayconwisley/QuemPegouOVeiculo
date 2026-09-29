namespace FleetManagement
{
    partial class VehicleStatusReportForm
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
            this.RbTodos = new System.Windows.Forms.RadioButton();
            this.GbPeriod = new System.Windows.Forms.GroupBox();
            this.RbStartDate = new System.Windows.Forms.RadioButton();
            this.RbEndDate = new System.Windows.Forms.RadioButton();
            this.CbOpenOnly = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.MktEndDate = new System.Windows.Forms.MaskedTextBox();
            this.MktStartDate = new System.Windows.Forms.MaskedTextBox();
            this.BtnList = new System.Windows.Forms.Button();
            this.GbVehicle = new System.Windows.Forms.GroupBox();
            this.VehicleControl = new FleetManagement.VehicleControl();
            this.GbOpcoes.SuspendLayout();
            this.GbPeriod.SuspendLayout();
            this.GbVehicle.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbOpcoes
            // 
            this.GbOpcoes.Controls.Add(this.RbPeriod);
            this.GbOpcoes.Controls.Add(this.RbVehicle);
            this.GbOpcoes.Controls.Add(this.RbTodos);
            this.GbOpcoes.Location = new System.Drawing.Point(9, 10);
            this.GbOpcoes.Name = "GbOpcoes";
            this.GbOpcoes.Size = new System.Drawing.Size(277, 48);
            this.GbOpcoes.TabIndex = 0;
            this.GbOpcoes.TabStop = false;
            this.GbOpcoes.Text = "Opção";
            // 
            // RbPeriod
            // 
            this.RbPeriod.AutoSize = true;
            this.RbPeriod.Location = new System.Drawing.Point(133, 19);
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
            this.RbVehicle.Location = new System.Drawing.Point(67, 19);
            this.RbVehicle.Name = "RbVehicle";
            this.RbVehicle.Size = new System.Drawing.Size(62, 17);
            this.RbVehicle.TabIndex = 1;
            this.RbVehicle.Text = "Veículo";
            this.RbVehicle.UseVisualStyleBackColor = true;
            this.RbVehicle.CheckedChanged += new System.EventHandler(this.RbVehicle_CheckedChanged);
            // 
            // RbTodos
            // 
            this.RbTodos.AutoSize = true;
            this.RbTodos.Checked = true;
            this.RbTodos.Location = new System.Drawing.Point(6, 19);
            this.RbTodos.Name = "RbTodos";
            this.RbTodos.Size = new System.Drawing.Size(55, 17);
            this.RbTodos.TabIndex = 0;
            this.RbTodos.TabStop = true;
            this.RbTodos.Text = "Todos";
            this.RbTodos.UseVisualStyleBackColor = true;
            this.RbTodos.CheckedChanged += new System.EventHandler(this.RbTodos_CheckedChanged);
            // 
            // GbPeriod
            // 
            this.GbPeriod.Controls.Add(this.RbStartDate);
            this.GbPeriod.Controls.Add(this.RbEndDate);
            this.GbPeriod.Controls.Add(this.CbOpenOnly);
            this.GbPeriod.Controls.Add(this.label1);
            this.GbPeriod.Controls.Add(this.MktEndDate);
            this.GbPeriod.Controls.Add(this.MktStartDate);
            this.GbPeriod.Enabled = false;
            this.GbPeriod.Location = new System.Drawing.Point(9, 127);
            this.GbPeriod.Name = "GbPeriod";
            this.GbPeriod.Size = new System.Drawing.Size(277, 93);
            this.GbPeriod.TabIndex = 2;
            this.GbPeriod.TabStop = false;
            this.GbPeriod.Text = "Período";
            // 
            // RbStartDate
            // 
            this.RbStartDate.AutoSize = true;
            this.RbStartDate.Checked = true;
            this.RbStartDate.Location = new System.Drawing.Point(6, 19);
            this.RbStartDate.Name = "RbStartDate";
            this.RbStartDate.Size = new System.Drawing.Size(78, 17);
            this.RbStartDate.TabIndex = 7;
            this.RbStartDate.TabStop = true;
            this.RbStartDate.Text = "Data Inicial";
            this.RbStartDate.UseVisualStyleBackColor = true;
            this.RbStartDate.CheckedChanged += new System.EventHandler(this.RbStartDate_CheckedChanged);
            // 
            // RbEndDate
            // 
            this.RbEndDate.AutoSize = true;
            this.RbEndDate.Location = new System.Drawing.Point(90, 19);
            this.RbEndDate.Name = "RbEndDate";
            this.RbEndDate.Size = new System.Drawing.Size(73, 17);
            this.RbEndDate.TabIndex = 8;
            this.RbEndDate.Text = "Data Final";
            this.RbEndDate.UseVisualStyleBackColor = true;
            this.RbEndDate.CheckedChanged += new System.EventHandler(this.RbEndDate_CheckedChanged);
            // 
            // CbOpenOnly
            // 
            this.CbOpenOnly.AutoSize = true;
            this.CbOpenOnly.Location = new System.Drawing.Point(6, 65);
            this.CbOpenOnly.Name = "CbOpenOnly";
            this.CbOpenOnly.Size = new System.Drawing.Size(136, 17);
            this.CbOpenOnly.TabIndex = 2;
            this.CbOpenOnly.Text = "Listar Data Final Vazias";
            this.CbOpenOnly.UseVisualStyleBackColor = true;
            this.CbOpenOnly.Visible = false;
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
            // BtnList
            // 
            this.BtnList.Location = new System.Drawing.Point(306, 10);
            this.BtnList.Name = "BtnList";
            this.BtnList.Size = new System.Drawing.Size(75, 23);
            this.BtnList.TabIndex = 3;
            this.BtnList.Text = "Listar";
            this.BtnList.UseVisualStyleBackColor = true;
            this.BtnList.Click += new System.EventHandler(this.BtnList_Click);
            // 
            // GbVehicle
            // 
            this.GbVehicle.Controls.Add(this.VehicleControl);
            this.GbVehicle.Enabled = false;
            this.GbVehicle.Location = new System.Drawing.Point(9, 64);
            this.GbVehicle.Name = "GbVehicle";
            this.GbVehicle.Size = new System.Drawing.Size(277, 57);
            this.GbVehicle.TabIndex = 1;
            this.GbVehicle.TabStop = false;
            // 
            // VehicleControl
            // 
            this.VehicleControl.Location = new System.Drawing.Point(6, 10);
            this.VehicleControl.Name = "VehicleControl";
            this.VehicleControl.Size = new System.Drawing.Size(265, 38);
            this.VehicleControl.TabIndex = 0;
            // 
            // VehicleStatusReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 229);
            this.Controls.Add(this.GbVehicle);
            this.Controls.Add(this.BtnList);
            this.Controls.Add(this.GbPeriod);
            this.Controls.Add(this.GbOpcoes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleStatusReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Status Veículo";
            this.GbOpcoes.ResumeLayout(false);
            this.GbOpcoes.PerformLayout();
            this.GbPeriod.ResumeLayout(false);
            this.GbPeriod.PerformLayout();
            this.GbVehicle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GbOpcoes;
        private System.Windows.Forms.RadioButton RbVehicle;
        private System.Windows.Forms.RadioButton RbTodos;
        private VehicleControl VehicleControl;
        private System.Windows.Forms.RadioButton RbPeriod;
        private System.Windows.Forms.GroupBox GbPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktEndDate;
        private System.Windows.Forms.MaskedTextBox MktStartDate;
        private System.Windows.Forms.Button BtnList;
        private System.Windows.Forms.CheckBox CbOpenOnly;
        private System.Windows.Forms.RadioButton RbStartDate;
        private System.Windows.Forms.RadioButton RbEndDate;
        private System.Windows.Forms.GroupBox GbVehicle;
    }
}