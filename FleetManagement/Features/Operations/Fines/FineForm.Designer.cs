namespace FleetManagement
{
    partial class FineForm
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
            this.label8 = new System.Windows.Forms.Label();
            this.MktDate = new System.Windows.Forms.MaskedTextBox();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnSave = new System.Windows.Forms.Button();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LblFines = new System.Windows.Forms.Label();
            this.DgvFines = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Points = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxtPoints = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AmountControl = new FleetManagement.AmountControl();
            this.DescriptionControl = new FleetManagement.DescriptionControl();
            this.VehicleDriverSelector = new FleetManagement.VehicleDriverSelector();
            ((System.ComponentModel.ISupportInitialize)(this.DgvFines)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(220, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "Data";
            // 
            // MktDate
            // 
            this.MktDate.Location = new System.Drawing.Point(220, 124);
            this.MktDate.Mask = "00/00/0000";
            this.MktDate.Name = "MktDate";
            this.MktDate.Size = new System.Drawing.Size(89, 21);
            this.MktDate.TabIndex = 3;
            this.MktDate.ValidatingType = typeof(System.DateTime);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(339, 72);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(87, 23);
            this.BtnDelete.TabIndex = 7;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(339, 43);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(87, 23);
            this.BtnEdit.TabIndex = 6;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(339, 14);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(87, 23);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(13, 239);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(408, 21);
            this.TxtSearch.TabIndex = 8;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 223);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Pesquisa";
            // 
            // LblFines
            // 
            this.LblFines.AutoSize = true;
            this.LblFines.Location = new System.Drawing.Point(13, 265);
            this.LblFines.Name = "LblFines";
            this.LblFines.Size = new System.Drawing.Size(77, 13);
            this.LblFines.TabIndex = 28;
            this.LblFines.Text = "Multas - 000";
            // 
            // DgvFines
            // 
            this.DgvFines.AllowUserToAddRows = false;
            this.DgvFines.AllowUserToDeleteRows = false;
            this.DgvFines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvFines.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvFines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvFines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvFines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.VehicleId,
            this.DriverId,
            this.DateColumn,
            this.Amount,
            this.Points,
            this.DriverNameColumn,
            this.Model,
            this.Description});
            this.DgvFines.Location = new System.Drawing.Point(13, 281);
            this.DgvFines.MultiSelect = false;
            this.DgvFines.Name = "DgvFines";
            this.DgvFines.ReadOnly = true;
            this.DgvFines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvFines.Size = new System.Drawing.Size(408, 150);
            this.DgvFines.TabIndex = 9;
            this.DgvFines.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvFines_CellDoubleClick);
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
            // DateColumn
            // 
            this.DateColumn.DataPropertyName = "Date";
            this.DateColumn.HeaderText = "Data";
            this.DateColumn.Name = "Date";
            this.DateColumn.ReadOnly = true;
            this.DateColumn.Width = 59;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle1;
            this.Amount.HeaderText = "Valor";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 61;
            // 
            // Points
            // 
            this.Points.DataPropertyName = "Points";
            this.Points.HeaderText = "Pontos";
            this.Points.Name = "Points";
            this.Points.ReadOnly = true;
            this.Points.Width = 70;
            // 
            // DriverNameColumn
            // 
            this.DriverNameColumn.DataPropertyName = "Name";
            this.DriverNameColumn.HeaderText = "Nome";
            this.DriverNameColumn.Name = "Name";
            this.DriverNameColumn.ReadOnly = true;
            this.DriverNameColumn.Width = 65;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Modelo";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 72;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Descricao";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 88;
            // 
            // TxtPoints
            // 
            this.TxtPoints.Location = new System.Drawing.Point(13, 124);
            this.TxtPoints.Name = "TxtPoints";
            this.TxtPoints.Size = new System.Drawing.Size(100, 21);
            this.TxtPoints.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Pontos";
            // 
            // AmountControl
            // 
            this.AmountControl.Location = new System.Drawing.Point(119, 107);
            this.AmountControl.Name = "AmountControl";
            this.AmountControl.Size = new System.Drawing.Size(95, 40);
            this.AmountControl.TabIndex = 2;
            // 
            // DescriptionControl
            // 
            this.DescriptionControl.Location = new System.Drawing.Point(13, 151);
            this.DescriptionControl.Name = "DescriptionControl";
            this.DescriptionControl.Size = new System.Drawing.Size(419, 69);
            this.DescriptionControl.TabIndex = 4;
            // 
            // VehicleDriverSelector
            // 
            this.VehicleDriverSelector.Location = new System.Drawing.Point(13, 12);
            this.VehicleDriverSelector.Name = "VehicleDriverSelector";
            this.VehicleDriverSelector.Size = new System.Drawing.Size(318, 88);
            this.VehicleDriverSelector.TabIndex = 0;
            // 
            // FineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 445);
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
            this.Controls.Add(this.LblFines);
            this.Controls.Add(this.DgvFines);
            this.Controls.Add(this.TxtPoints);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FineForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle Multa";
            this.Load += new System.EventHandler(this.FineForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvFines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox MktDate;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label LblFines;
        private System.Windows.Forms.DataGridView DgvFines;
        private System.Windows.Forms.TextBox TxtPoints;
        private System.Windows.Forms.Label label3;
        private VehicleDriverSelector VehicleDriverSelector;
        private DescriptionControl DescriptionControl;
        private AmountControl AmountControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Points;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}