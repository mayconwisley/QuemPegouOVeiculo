namespace FleetManagement
{
    partial class DriverForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtLicenseNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MktLicenseExpiration = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtLicenseCategory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtRG = new System.Windows.Forms.TextBox();
            this.CbActive = new System.Windows.Forms.CheckBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.DgvDrivers = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenseNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenseExpiration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LicenseCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblDriver = new System.Windows.Forms.Label();
            this.MktCPF = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DgvDrivers)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nome";
            // 
            // TxtName
            // 
            this.TxtName.Location = new System.Drawing.Point(12, 25);
            this.TxtName.MaxLength = 200;
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(248, 21);
            this.TxtName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Num. CNH";
            // 
            // TxtLicenseNumber
            // 
            this.TxtLicenseNumber.Location = new System.Drawing.Point(12, 65);
            this.TxtLicenseNumber.MaxLength = 11;
            this.TxtLicenseNumber.Name = "TxtCNH";
            this.TxtLicenseNumber.Size = new System.Drawing.Size(142, 21);
            this.TxtLicenseNumber.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Vencimento";
            // 
            // MktLicenseExpiration
            // 
            this.MktLicenseExpiration.Location = new System.Drawing.Point(160, 65);
            this.MktLicenseExpiration.Mask = "00/00/0000";
            this.MktLicenseExpiration.Name = "MktLicenseExpiration";
            this.MktLicenseExpiration.Size = new System.Drawing.Size(100, 21);
            this.MktLicenseExpiration.TabIndex = 2;
            this.MktLicenseExpiration.ValidatingType = typeof(System.DateTime);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Categ.";
            // 
            // TxtLicenseCategory
            // 
            this.TxtLicenseCategory.Location = new System.Drawing.Point(12, 105);
            this.TxtLicenseCategory.MaxLength = 2;
            this.TxtLicenseCategory.Name = "TxtLicenseCategory";
            this.TxtLicenseCategory.Size = new System.Drawing.Size(41, 21);
            this.TxtLicenseCategory.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "CPF";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 129);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "RG";
            // 
            // TxtRG
            // 
            this.TxtRG.Location = new System.Drawing.Point(140, 145);
            this.TxtRG.MaxLength = 20;
            this.TxtRG.Name = "TxtRG";
            this.TxtRG.Size = new System.Drawing.Size(120, 21);
            this.TxtRG.TabIndex = 6;
            // 
            // CbActive
            // 
            this.CbActive.AutoSize = true;
            this.CbActive.Checked = true;
            this.CbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbActive.Location = new System.Drawing.Point(90, 107);
            this.CbActive.Name = "CbActive";
            this.CbActive.Size = new System.Drawing.Size(55, 17);
            this.CbActive.TabIndex = 4;
            this.CbActive.Text = "Ativo";
            this.CbActive.UseVisualStyleBackColor = true;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(294, 23);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 7;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(294, 52);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 23);
            this.BtnEdit.TabIndex = 8;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(294, 81);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 9;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 195);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Pesquisa";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(12, 211);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(357, 21);
            this.TxtSearch.TabIndex = 10;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // DgvDrivers
            // 
            this.DgvDrivers.AllowUserToAddRows = false;
            this.DgvDrivers.AllowUserToDeleteRows = false;
            this.DgvDrivers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvDrivers.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvDrivers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvDrivers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvDrivers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.DriverNameColumn,
            this.LicenseNumber,
            this.LicenseExpiration,
            this.LicenseCategory,
            this.CPF,
            this.RG,
            this.Active});
            this.DgvDrivers.Location = new System.Drawing.Point(12, 257);
            this.DgvDrivers.MultiSelect = false;
            this.DgvDrivers.Name = "DgvDrivers";
            this.DgvDrivers.ReadOnly = true;
            this.DgvDrivers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvDrivers.Size = new System.Drawing.Size(357, 150);
            this.DgvDrivers.TabIndex = 11;
            this.DgvDrivers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDrivers_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // DriverNameColumn
            // 
            this.DriverNameColumn.DataPropertyName = "Name";
            this.DriverNameColumn.HeaderText = "Nome";
            this.DriverNameColumn.Name = "Name";
            this.DriverNameColumn.ReadOnly = true;
            this.DriverNameColumn.Width = 65;
            // 
            // LicenseNumber
            // 
            this.LicenseNumber.DataPropertyName = "LicenseNumber";
            this.LicenseNumber.HeaderText = "Num_CNH";
            this.LicenseNumber.Name = "LicenseNumber";
            this.LicenseNumber.ReadOnly = true;
            this.LicenseNumber.Width = 90;
            // 
            // LicenseExpiration
            // 
            this.LicenseExpiration.DataPropertyName = "LicenseExpiration";
            this.LicenseExpiration.HeaderText = "Vencimento_CNH";
            this.LicenseExpiration.Name = "LicenseExpiration";
            this.LicenseExpiration.ReadOnly = true;
            this.LicenseExpiration.Width = 130;
            // 
            // LicenseCategory
            // 
            this.LicenseCategory.DataPropertyName = "LicenseCategory";
            this.LicenseCategory.HeaderText = "Categoria_CNH";
            this.LicenseCategory.Name = "LicenseCategory";
            this.LicenseCategory.ReadOnly = true;
            this.LicenseCategory.Width = 120;
            // 
            // CPF
            // 
            this.CPF.DataPropertyName = "CPF";
            this.CPF.HeaderText = "CPF";
            this.CPF.Name = "CPF";
            this.CPF.ReadOnly = true;
            this.CPF.Width = 54;
            // 
            // RG
            // 
            this.RG.DataPropertyName = "RG";
            this.RG.HeaderText = "RG";
            this.RG.Name = "RG";
            this.RG.ReadOnly = true;
            this.RG.Width = 49;
            // 
            // Active
            // 
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Ativo";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            this.Active.Width = 61;
            // 
            // LblDriver
            // 
            this.LblDriver.AutoSize = true;
            this.LblDriver.Location = new System.Drawing.Point(12, 241);
            this.LblDriver.Name = "LblDriver";
            this.LblDriver.Size = new System.Drawing.Size(99, 13);
            this.LblDriver.TabIndex = 19;
            this.LblDriver.Text = "Motoristas - 000";
            // 
            // MktCPF
            // 
            this.MktCPF.Location = new System.Drawing.Point(12, 145);
            this.MktCPF.Mask = "000\\.000\\.000-00";
            this.MktCPF.Name = "MktCPF";
            this.MktCPF.Size = new System.Drawing.Size(122, 21);
            this.MktCPF.TabIndex = 5;
            // 
            // DriverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 426);
            this.Controls.Add(this.MktCPF);
            this.Controls.Add(this.LblDriver);
            this.Controls.Add(this.DgvDrivers);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.CbActive);
            this.Controls.Add(this.TxtRG);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtLicenseCategory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MktLicenseExpiration);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtLicenseNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DriverForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Motorista";
            this.Load += new System.EventHandler(this.DriverForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvDrivers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtLicenseNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox MktLicenseExpiration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtLicenseCategory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtRG;
        private System.Windows.Forms.CheckBox CbActive;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.DataGridView DgvDrivers;
        private System.Windows.Forms.Label LblDriver;
        private System.Windows.Forms.MaskedTextBox MktCPF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseExpiration;
        private System.Windows.Forms.DataGridViewTextBoxColumn LicenseCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPF;
        private System.Windows.Forms.DataGridViewTextBoxColumn RG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Active;
    }
}