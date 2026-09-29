namespace FleetManagement
{
    partial class VehicleMovementForm
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
            this.MktDtDeparture = new System.Windows.Forms.MaskedTextBox();
            this.MktDtArrival = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtInitialMileage = new System.Windows.Forms.TextBox();
            this.TxtFinalMileage = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.DgvVehicleMovements = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.LblVehicleMovementCount = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.LkLblDtArrivalCurrent = new System.Windows.Forms.LinkLabel();
            this.LkLblDtDepartureCurrent = new System.Windows.Forms.LinkLabel();
            this.VehicleDriverSelector = new FleetManagement.VehicleDriverSelector();
            this.DescriptionControl = new FleetManagement.DescriptionControl();
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
            // MktDtDeparture
            // 
            this.MktDtDeparture.Location = new System.Drawing.Point(12, 124);
            this.MktDtDeparture.Mask = "00/00/0000 00:00";
            this.MktDtDeparture.Name = "MktDtDeparture";
            this.MktDtDeparture.Size = new System.Drawing.Size(121, 21);
            this.MktDtDeparture.TabIndex = 1;
            // 
            // MktDtArrival
            // 
            this.MktDtArrival.Location = new System.Drawing.Point(168, 124);
            this.MktDtArrival.Mask = "00/00/0000 00:00";
            this.MktDtArrival.Name = "MktDtArrival";
            this.MktDtArrival.Size = new System.Drawing.Size(121, 21);
            this.MktDtArrival.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Dt. Saída";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Dt. Chegada";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Km. Inicio";
            // 
            // TxtInitialMileage
            // 
            this.TxtInitialMileage.Location = new System.Drawing.Point(12, 183);
            this.TxtInitialMileage.Name = "TxtInitialMileage";
            this.TxtInitialMileage.Size = new System.Drawing.Size(100, 21);
            this.TxtInitialMileage.TabIndex = 3;
            // 
            // TxtFinalMileage
            // 
            this.TxtFinalMileage.Location = new System.Drawing.Point(118, 183);
            this.TxtFinalMileage.Name = "TxtFinalMileage";
            this.TxtFinalMileage.Size = new System.Drawing.Size(100, 21);
            this.TxtFinalMileage.TabIndex = 4;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(356, 17);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 33);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(356, 56);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 33);
            this.BtnEdit.TabIndex = 7;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(356, 95);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 33);
            this.BtnDelete.TabIndex = 8;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
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
            this.DgvVehicleMovements.Location = new System.Drawing.Point(15, 338);
            this.DgvVehicleMovements.MultiSelect = false;
            this.DgvVehicleMovements.Name = "DgvVehicleMovements";
            this.DgvVehicleMovements.ReadOnly = true;
            this.DgvVehicleMovements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVehicleMovements.Size = new System.Drawing.Size(416, 150);
            this.DgvVehicleMovements.TabIndex = 10;
            this.DgvVehicleMovements.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVehicleMovements_CellDoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 282);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Pesquisa";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(12, 298);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(419, 21);
            this.TxtSearch.TabIndex = 9;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // LblVehicleMovementCount
            // 
            this.LblVehicleMovementCount.AutoSize = true;
            this.LblVehicleMovementCount.Location = new System.Drawing.Point(12, 322);
            this.LblVehicleMovementCount.Name = "LblVehicleMovementCount";
            this.LblVehicleMovementCount.Size = new System.Drawing.Size(134, 13);
            this.LblVehicleMovementCount.TabIndex = 14;
            this.LblVehicleMovementCount.Text = "Controle Veículo - 000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(115, 167);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Km. Final";
            // 
            // LkLblDtArrivalCurrent
            // 
            this.LkLblDtArrivalCurrent.AutoSize = true;
            this.LkLblDtArrivalCurrent.Location = new System.Drawing.Point(253, 108);
            this.LkLblDtArrivalCurrent.Name = "LkLblDtArrivalCurrent";
            this.LkLblDtArrivalCurrent.Size = new System.Drawing.Size(36, 13);
            this.LkLblDtArrivalCurrent.TabIndex = 21;
            this.LkLblDtArrivalCurrent.TabStop = true;
            this.LkLblDtArrivalCurrent.Text = "Atual";
            this.LkLblDtArrivalCurrent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LkLblDtArrivalCurrent_LinkClicked);
            // 
            // LkLblDtDepartureCurrent
            // 
            this.LkLblDtDepartureCurrent.AutoSize = true;
            this.LkLblDtDepartureCurrent.Location = new System.Drawing.Point(97, 108);
            this.LkLblDtDepartureCurrent.Name = "LkLblDtDepartureCurrent";
            this.LkLblDtDepartureCurrent.Size = new System.Drawing.Size(36, 13);
            this.LkLblDtDepartureCurrent.TabIndex = 20;
            this.LkLblDtDepartureCurrent.TabStop = true;
            this.LkLblDtDepartureCurrent.Text = "Atual";
            this.LkLblDtDepartureCurrent.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LkLblDtDepartureCurrent_LinkClicked);
            // 
            // VehicleDriverSelector
            // 
            this.VehicleDriverSelector.AutoSize = true;
            this.VehicleDriverSelector.Location = new System.Drawing.Point(12, 17);
            this.VehicleDriverSelector.Name = "VehicleDriverSelector";
            this.VehicleDriverSelector.Size = new System.Drawing.Size(309, 81);
            this.VehicleDriverSelector.TabIndex = 0;
            this.VehicleDriverSelector.FinalMileage += new FleetManagement.VehicleDriverSelector.VehicleEndingMileage(this.VehicleDriverSelector_FinalMileage);
            // 
            // DescriptionControl
            // 
            this.DescriptionControl.AutoSize = true;
            this.DescriptionControl.Location = new System.Drawing.Point(12, 210);
            this.DescriptionControl.Name = "DescriptionControl";
            this.DescriptionControl.Size = new System.Drawing.Size(412, 67);
            this.DescriptionControl.TabIndex = 5;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 44;
            // 
            // VehicleId
            // 
            this.VehicleId.DataPropertyName = "VehicleId";
            this.VehicleId.HeaderText = "Id_Veiculo";
            this.VehicleId.Name = "VehicleId";
            this.VehicleId.ReadOnly = true;
            this.VehicleId.Visible = false;
            this.VehicleId.Width = 91;
            // 
            // DriverId
            // 
            this.DriverId.DataPropertyName = "DriverId";
            this.DriverId.HeaderText = "Id_Motorista";
            this.DriverId.Name = "DriverId";
            this.DriverId.ReadOnly = true;
            this.DriverId.Visible = false;
            this.DriverId.Width = 103;
            // 
            // DepartureAt
            // 
            this.DepartureAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.DepartureAt.DataPropertyName = "DepartureAt";
            this.DepartureAt.HeaderText = "Data_Hora_Saída";
            this.DepartureAt.Name = "DepartureAt";
            this.DepartureAt.ReadOnly = true;
            this.DepartureAt.Width = 132;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Modelo";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 72;
            // 
            // DriverNameColumn
            // 
            this.DriverNameColumn.DataPropertyName = "Name";
            this.DriverNameColumn.HeaderText = "Nome";
            this.DriverNameColumn.Name = "Name";
            this.DriverNameColumn.ReadOnly = true;
            this.DriverNameColumn.Width = 65;
            // 
            // ArrivalAt
            // 
            this.ArrivalAt.DataPropertyName = "ArrivalAt";
            this.ArrivalAt.HeaderText = "Data_Hora_Chegada";
            this.ArrivalAt.Name = "ArrivalAt";
            this.ArrivalAt.ReadOnly = true;
            this.ArrivalAt.Width = 151;
            // 
            // Days
            // 
            this.Days.DataPropertyName = "Days";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Days.DefaultCellStyle = dataGridViewCellStyle1;
            this.Days.HeaderText = "Dias";
            this.Days.Name = "Days";
            this.Days.ReadOnly = true;
            this.Days.Width = 57;
            // 
            // Hours
            // 
            this.Hours.DataPropertyName = "Hours";
            this.Hours.HeaderText = "Horas";
            this.Hours.Name = "Hours";
            this.Hours.ReadOnly = true;
            this.Hours.Width = 65;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Descrição";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 88;
            // 
            // InitialMileage
            // 
            this.InitialMileage.DataPropertyName = "InitialMileage";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.InitialMileage.DefaultCellStyle = dataGridViewCellStyle2;
            this.InitialMileage.HeaderText = "Km_Inicial";
            this.InitialMileage.Name = "InitialMileage";
            this.InitialMileage.ReadOnly = true;
            this.InitialMileage.Width = 92;
            // 
            // FinalMileage
            // 
            this.FinalMileage.DataPropertyName = "FinalMileage";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.FinalMileage.DefaultCellStyle = dataGridViewCellStyle3;
            this.FinalMileage.HeaderText = "Km_Final";
            this.FinalMileage.Name = "FinalMileage";
            this.FinalMileage.ReadOnly = true;
            this.FinalMileage.Width = 84;
            // 
            // TotalMileage
            // 
            this.TotalMileage.DataPropertyName = "TotalMileage";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.TotalMileage.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalMileage.HeaderText = "Km_Total";
            this.TotalMileage.Name = "TotalMileage";
            this.TotalMileage.ReadOnly = true;
            this.TotalMileage.Width = 85;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 68;
            // 
            // VehicleMovementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 504);
            this.Controls.Add(this.VehicleDriverSelector);
            this.Controls.Add(this.LkLblDtArrivalCurrent);
            this.Controls.Add(this.LkLblDtDepartureCurrent);
            this.Controls.Add(this.DescriptionControl);
            this.Controls.Add(this.LblVehicleMovementCount);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DgvVehicleMovements);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TxtFinalMileage);
            this.Controls.Add(this.TxtInitialMileage);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MktDtArrival);
            this.Controls.Add(this.MktDtDeparture);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleMovementForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de Veículo";
            this.Load += new System.EventHandler(this.VehicleMovementForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVehicleMovements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MaskedTextBox MktDtDeparture;
        private System.Windows.Forms.MaskedTextBox MktDtArrival;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtFinalMileage;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.DataGridView DgvVehicleMovements;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label LblVehicleMovementCount;
        private System.Windows.Forms.Label label9;
        private DescriptionControl DescriptionControl;
        private System.Windows.Forms.LinkLabel LkLblDtArrivalCurrent;
        private System.Windows.Forms.LinkLabel LkLblDtDepartureCurrent;
        public System.Windows.Forms.TextBox TxtInitialMileage;
        private VehicleDriverSelector VehicleDriverSelector;
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