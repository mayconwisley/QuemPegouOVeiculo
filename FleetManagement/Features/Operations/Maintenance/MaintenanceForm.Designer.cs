namespace FleetManagement
{
    partial class MaintenanceForm
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
            this.VehicleControl = new FleetManagement.VehicleControl();
            this.label1 = new System.Windows.Forms.Label();
            this.MktDate = new System.Windows.Forms.MaskedTextBox();
            this.AmountControl = new FleetManagement.AmountControl();
            this.DescriptionControl = new FleetManagement.DescriptionControl();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.DgvMaintenance = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblMaintenance = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvMaintenance)).BeginInit();
            this.SuspendLayout();
            // 
            // VehicleControl
            // 
            this.VehicleControl.Location = new System.Drawing.Point(14, 12);
            this.VehicleControl.Name = "VehicleControl";
            this.VehicleControl.Size = new System.Drawing.Size(314, 45);
            this.VehicleControl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Data";
            // 
            // MktDate
            // 
            this.MktDate.Location = new System.Drawing.Point(14, 76);
            this.MktDate.Mask = "00/00/0000";
            this.MktDate.Name = "MktDate";
            this.MktDate.Size = new System.Drawing.Size(100, 21);
            this.MktDate.TabIndex = 1;
            this.MktDate.ValidatingType = typeof(System.DateTime);
            // 
            // AmountControl
            // 
            this.AmountControl.Location = new System.Drawing.Point(141, 58);
            this.AmountControl.Name = "AmountControl";
            this.AmountControl.Size = new System.Drawing.Size(120, 40);
            this.AmountControl.TabIndex = 2;
            // 
            // DescriptionControl
            // 
            this.DescriptionControl.Location = new System.Drawing.Point(14, 118);
            this.DescriptionControl.Name = "DescriptionControl";
            this.DescriptionControl.Size = new System.Drawing.Size(420, 69);
            this.DescriptionControl.TabIndex = 3;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(348, 12);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(87, 23);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(348, 41);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(87, 23);
            this.BtnEdit.TabIndex = 5;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(348, 70);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(87, 23);
            this.BtnDelete.TabIndex = 6;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Pesquisar";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(14, 206);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(420, 21);
            this.TxtSearch.TabIndex = 7;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // DgvMaintenance
            // 
            this.DgvMaintenance.AllowUserToAddRows = false;
            this.DgvMaintenance.AllowUserToDeleteRows = false;
            this.DgvMaintenance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvMaintenance.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvMaintenance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvMaintenance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvMaintenance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.VehicleId,
            this.DateColumn,
            this.Amount,
            this.Description,
            this.Model});
            this.DgvMaintenance.Location = new System.Drawing.Point(14, 256);
            this.DgvMaintenance.MultiSelect = false;
            this.DgvMaintenance.Name = "DgvMaintenance";
            this.DgvMaintenance.ReadOnly = true;
            this.DgvMaintenance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvMaintenance.Size = new System.Drawing.Size(421, 150);
            this.DgvMaintenance.TabIndex = 8;
            this.DgvMaintenance.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvMaintenance_CellDoubleClick);
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
            // DateColumn
            // 
            this.DateColumn.DataPropertyName = "Date";
            dataGridViewCellStyle1.Format = "d";
            dataGridViewCellStyle1.NullValue = null;
            this.DateColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.DateColumn.HeaderText = "Data";
            this.DateColumn.Name = "Date";
            this.DateColumn.ReadOnly = true;
            this.DateColumn.Width = 59;
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
            this.Amount.Width = 61;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Descricao";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 88;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Modelo";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 72;
            // 
            // LblMaintenance
            // 
            this.LblMaintenance.AutoSize = true;
            this.LblMaintenance.Location = new System.Drawing.Point(14, 240);
            this.LblMaintenance.Name = "LblMaintenance";
            this.LblMaintenance.Size = new System.Drawing.Size(109, 13);
            this.LblMaintenance.TabIndex = 9;
            this.LblMaintenance.Text = "Manutenção - 000";
            // 
            // MaintenanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 422);
            this.Controls.Add(this.LblMaintenance);
            this.Controls.Add(this.DgvMaintenance);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.DescriptionControl);
            this.Controls.Add(this.AmountControl);
            this.Controls.Add(this.MktDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.VehicleControl);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MaintenanceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Controle de Manutenção";
            this.Load += new System.EventHandler(this.MaintenanceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvMaintenance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VehicleControl VehicleControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox MktDate;
        private AmountControl AmountControl;
        private DescriptionControl DescriptionControl;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.DataGridView DgvMaintenance;
        private System.Windows.Forms.Label LblMaintenance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
    }
}