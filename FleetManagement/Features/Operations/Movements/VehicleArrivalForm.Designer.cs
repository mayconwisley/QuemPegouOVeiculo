namespace FleetManagement
{
    partial class VehicleArrivalForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.DgvVehicleMovements = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartureAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArrivalAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Days = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InitialMileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinalMileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalMileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVehicleMovements)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lista de Veículos que não chegaram";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(12, 384);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(35, 13);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "label2";
            // 
            // DgvVehicleMovements
            // 
            this.DgvVehicleMovements.AllowUserToAddRows = false;
            this.DgvVehicleMovements.AllowUserToDeleteRows = false;
            this.DgvVehicleMovements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvVehicleMovements.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvVehicleMovements.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvVehicleMovements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvVehicleMovements.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.VehicleId,
            this.DriverId,
            this.DepartureAt,
            this.Model,
            this.DriverNameColumn,
            this.ArrivalAt,
            this.Days,
            this.Hours,
            this.Description,
            this.InitialMileage,
            this.FinalMileage,
            this.TotalMileage,
            this.Status});
            this.DgvVehicleMovements.Location = new System.Drawing.Point(12, 25);
            this.DgvVehicleMovements.MultiSelect = false;
            this.DgvVehicleMovements.Name = "DgvVehicleMovements";
            this.DgvVehicleMovements.ReadOnly = true;
            this.DgvVehicleMovements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVehicleMovements.Size = new System.Drawing.Size(805, 356);
            this.DgvVehicleMovements.TabIndex = 12;
            this.DgvVehicleMovements.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVehicleMovements_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 41;
            // 
            // VehicleId
            // 
            this.VehicleId.DataPropertyName = "VehicleId";
            this.VehicleId.HeaderText = "Id_Veiculo";
            this.VehicleId.Name = "VehicleId";
            this.VehicleId.ReadOnly = true;
            this.VehicleId.Visible = false;
            this.VehicleId.Width = 82;
            // 
            // DriverId
            // 
            this.DriverId.DataPropertyName = "DriverId";
            this.DriverId.HeaderText = "Id_Motorista";
            this.DriverId.Name = "DriverId";
            this.DriverId.ReadOnly = true;
            this.DriverId.Visible = false;
            this.DriverId.Width = 90;
            // 
            // DepartureAt
            // 
            this.DepartureAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DepartureAt.DataPropertyName = "DepartureAt";
            this.DepartureAt.HeaderText = "Data_Hora_Saída";
            this.DepartureAt.Name = "DepartureAt";
            this.DepartureAt.ReadOnly = true;
            this.DepartureAt.Width = 119;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Modelo";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 67;
            // 
            // DriverNameColumn
            // 
            this.DriverNameColumn.DataPropertyName = "Name";
            this.DriverNameColumn.HeaderText = "Nome";
            this.DriverNameColumn.Name = "Name";
            this.DriverNameColumn.ReadOnly = true;
            this.DriverNameColumn.Width = 60;
            // 
            // ArrivalAt
            // 
            this.ArrivalAt.DataPropertyName = "ArrivalAt";
            this.ArrivalAt.HeaderText = "Data_Hora_Chegada";
            this.ArrivalAt.Name = "ArrivalAt";
            this.ArrivalAt.ReadOnly = true;
            this.ArrivalAt.Width = 133;
            // 
            // Days
            // 
            this.Days.DataPropertyName = "Days";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Days.DefaultCellStyle = dataGridViewCellStyle1;
            this.Days.HeaderText = "Dias";
            this.Days.Name = "Days";
            this.Days.ReadOnly = true;
            this.Days.Width = 53;
            // 
            // Hours
            // 
            this.Hours.DataPropertyName = "Hours";
            this.Hours.HeaderText = "Horas";
            this.Hours.Name = "Hours";
            this.Hours.ReadOnly = true;
            this.Hours.Width = 60;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Descrição";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 80;
            // 
            // InitialMileage
            // 
            this.InitialMileage.DataPropertyName = "InitialMileage";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InitialMileage.DefaultCellStyle = dataGridViewCellStyle2;
            this.InitialMileage.HeaderText = "Km_Inicial";
            this.InitialMileage.Name = "InitialMileage";
            this.InitialMileage.ReadOnly = true;
            this.InitialMileage.Width = 80;
            // 
            // FinalMileage
            // 
            this.FinalMileage.DataPropertyName = "FinalMileage";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FinalMileage.DefaultCellStyle = dataGridViewCellStyle3;
            this.FinalMileage.HeaderText = "Km_Final";
            this.FinalMileage.Name = "FinalMileage";
            this.FinalMileage.ReadOnly = true;
            this.FinalMileage.Width = 75;
            // 
            // TotalMileage
            // 
            this.TotalMileage.DataPropertyName = "TotalMileage";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalMileage.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalMileage.HeaderText = "Km_Total";
            this.TotalMileage.Name = "TotalMileage";
            this.TotalMileage.ReadOnly = true;
            this.TotalMileage.Width = 77;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 62;
            // 
            // VehicleArrivalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 426);
            this.Controls.Add(this.DgvVehicleMovements);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleArrivalForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Atualização Controle Veículo";
            this.Load += new System.EventHandler(this.VehicleArrivalForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVehicleMovements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.DataGridView DgvVehicleMovements;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartureAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArrivalAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Days;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hours;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn InitialMileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinalMileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalMileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}