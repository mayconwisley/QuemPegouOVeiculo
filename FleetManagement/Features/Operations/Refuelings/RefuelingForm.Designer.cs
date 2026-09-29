namespace FleetManagement
{
    partial class RefuelingForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.TxtInitialMileage = new System.Windows.Forms.TextBox();
            this.DgvRefueling = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Liters = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InitialMileage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblRefueling = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.MktDate = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.VehicleDriverSelector = new FleetManagement.VehicleDriverSelector();
            this.DescriptionControl = new FleetManagement.DescriptionControl();
            this.AmountControl = new FleetManagement.AmountControl();
            this.TxtLiters = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvRefueling)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Km. Inicial";
            // 
            // TxtInitialMileage
            // 
            this.TxtInitialMileage.Location = new System.Drawing.Point(19, 128);
            this.TxtInitialMileage.Name = "TxtInitialMileage";
            this.TxtInitialMileage.Size = new System.Drawing.Size(100, 21);
            this.TxtInitialMileage.TabIndex = 1;
            // 
            // DgvRefueling
            // 
            this.DgvRefueling.AllowUserToAddRows = false;
            this.DgvRefueling.AllowUserToDeleteRows = false;
            this.DgvRefueling.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DgvRefueling.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvRefueling.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvRefueling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvRefueling.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.VehicleId,
            this.DriverId,
            this.DateColumn,
            this.Amount,
            this.Liters,
            this.DriverNameColumn,
            this.Model,
            this.InitialMileage,
            this.Description});
            this.DgvRefueling.Location = new System.Drawing.Point(19, 285);
            this.DgvRefueling.MultiSelect = false;
            this.DgvRefueling.Name = "DgvRefueling";
            this.DgvRefueling.ReadOnly = true;
            this.DgvRefueling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvRefueling.Size = new System.Drawing.Size(408, 150);
            this.DgvRefueling.TabIndex = 10;
            this.DgvRefueling.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvRefueling_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // VehicleId
            // 
            this.VehicleId.DataPropertyName = "VehicleId";
            this.VehicleId.HeaderText = "Id_Veiculo";
            this.VehicleId.Name = "VehicleId";
            this.VehicleId.ReadOnly = true;
            this.VehicleId.Visible = false;
            // 
            // DriverId
            // 
            this.DriverId.DataPropertyName = "DriverId";
            this.DriverId.HeaderText = "Id_Motorista";
            this.DriverId.Name = "DriverId";
            this.DriverId.ReadOnly = true;
            this.DriverId.Visible = false;
            // 
            // DateColumn
            // 
            this.DateColumn.DataPropertyName = "Date";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.DateColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.DateColumn.HeaderText = "Data";
            this.DateColumn.Name = "Date";
            this.DateColumn.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle2;
            this.Amount.HeaderText = "Valor";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // Liters
            // 
            this.Liters.DataPropertyName = "Liters";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.Liters.DefaultCellStyle = dataGridViewCellStyle3;
            this.Liters.HeaderText = "Litros";
            this.Liters.Name = "Liters";
            this.Liters.ReadOnly = true;
            // 
            // DriverNameColumn
            // 
            this.DriverNameColumn.DataPropertyName = "Name";
            this.DriverNameColumn.HeaderText = "Nome";
            this.DriverNameColumn.Name = "Name";
            this.DriverNameColumn.ReadOnly = true;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Modelo";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // InitialMileage
            // 
            this.InitialMileage.DataPropertyName = "InitialMileage";
            this.InitialMileage.HeaderText = "KmInicial";
            this.InitialMileage.Name = "InitialMileage";
            this.InitialMileage.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Descricao";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // LblRefueling
            // 
            this.LblRefueling.AutoSize = true;
            this.LblRefueling.Location = new System.Drawing.Point(19, 269);
            this.LblRefueling.Name = "LblRefueling";
            this.LblRefueling.Size = new System.Drawing.Size(125, 13);
            this.LblRefueling.TabIndex = 11;
            this.LblRefueling.Text = "Abastecimento - 000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Pesquisa";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(19, 243);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(408, 21);
            this.TxtSearch.TabIndex = 9;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(346, 13);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(87, 23);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(346, 42);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(87, 23);
            this.BtnEdit.TabIndex = 7;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(346, 71);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(87, 23);
            this.BtnDelete.TabIndex = 8;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // MktDate
            // 
            this.MktDate.Location = new System.Drawing.Point(236, 128);
            this.MktDate.Mask = "00/00/0000";
            this.MktDate.Name = "MktDate";
            this.MktDate.Size = new System.Drawing.Size(101, 21);
            this.MktDate.TabIndex = 3;
            this.MktDate.ValidatingType = typeof(System.DateTime);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(236, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Data";
            // 
            // VehicleDriverSelector
            // 
            this.VehicleDriverSelector.Location = new System.Drawing.Point(19, 13);
            this.VehicleDriverSelector.Name = "VehicleDriverSelector";
            this.VehicleDriverSelector.Size = new System.Drawing.Size(320, 88);
            this.VehicleDriverSelector.TabIndex = 0;
            this.VehicleDriverSelector.FinalMileage += new FleetManagement.VehicleDriverSelector.VehicleEndingMileage(this.VehicleDriverSelector_FinalMileage);
            // 
            // DescriptionControl
            // 
            this.DescriptionControl.Location = new System.Drawing.Point(19, 155);
            this.DescriptionControl.Name = "DescriptionControl";
            this.DescriptionControl.Size = new System.Drawing.Size(408, 69);
            this.DescriptionControl.TabIndex = 5;
            // 
            // AmountControl
            // 
            this.AmountControl.Location = new System.Drawing.Point(127, 111);
            this.AmountControl.Name = "AmountControl";
            this.AmountControl.Size = new System.Drawing.Size(103, 40);
            this.AmountControl.TabIndex = 2;
            // 
            // TxtLiters
            // 
            this.TxtLiters.Location = new System.Drawing.Point(346, 128);
            this.TxtLiters.Name = "TxtLiters";
            this.TxtLiters.Size = new System.Drawing.Size(81, 21);
            this.TxtLiters.TabIndex = 4;
            this.TxtLiters.Text = "0,00";
            this.TxtLiters.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtLiters.TextChanged += new System.EventHandler(this.TxtLiters_TextChanged);
            this.TxtLiters.Enter += new System.EventHandler(this.TxtLiters_Enter);
            this.TxtLiters.Leave += new System.EventHandler(this.TxtLiters_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(343, 112);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Litros";
            // 
            // RefuelingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 449);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtLiters);
            this.Controls.Add(this.AmountControl);
            this.Controls.Add(this.DescriptionControl);
            this.Controls.Add(this.VehicleDriverSelector);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.MktDate);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.LblRefueling);
            this.Controls.Add(this.DgvRefueling);
            this.Controls.Add(this.TxtInitialMileage);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RefuelingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle Combustivel";
            this.Load += new System.EventHandler(this.RefuelingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvRefueling)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtInitialMileage;
        private System.Windows.Forms.DataGridView DgvRefueling;
        private System.Windows.Forms.Label LblRefueling;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.MaskedTextBox MktDate;
        private System.Windows.Forms.Label label8;
        private VehicleDriverSelector VehicleDriverSelector;
        private DescriptionControl DescriptionControl;
        private AmountControl AmountControl;
        private System.Windows.Forms.TextBox TxtLiters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Liters;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn InitialMileage;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}