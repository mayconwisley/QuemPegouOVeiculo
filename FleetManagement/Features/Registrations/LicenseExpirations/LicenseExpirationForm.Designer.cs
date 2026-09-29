namespace FleetManagement
{
    partial class LicenseExpirationForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.MktDtExpiration = new System.Windows.Forms.MaskedTextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.CbExpired = new System.Windows.Forms.CheckBox();
            this.DgvLicenseExpirations = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DriverNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblExpiration = new System.Windows.Forms.Label();
            this.DriverControl = new FleetManagement.DriverControl();
            ((System.ComponentModel.ISupportInitialize)(this.DgvLicenseExpirations)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dt. Vencimento";
            // 
            // MktDtExpiration
            // 
            this.MktDtExpiration.Location = new System.Drawing.Point(18, 89);
            this.MktDtExpiration.Mask = "00/00/0000";
            this.MktDtExpiration.Name = "MktDtExpiration";
            this.MktDtExpiration.Size = new System.Drawing.Size(100, 21);
            this.MktDtExpiration.TabIndex = 1;
            this.MktDtExpiration.ValidatingType = typeof(System.DateTime);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(346, 10);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 3;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(346, 39);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 23);
            this.BtnEdit.TabIndex = 4;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(346, 68);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 5;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Pesquisa";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(18, 141);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(403, 21);
            this.TxtSearch.TabIndex = 6;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // CbExpired
            // 
            this.CbExpired.AutoSize = true;
            this.CbExpired.Location = new System.Drawing.Point(135, 91);
            this.CbExpired.Name = "CbExpired";
            this.CbExpired.Size = new System.Drawing.Size(70, 17);
            this.CbExpired.TabIndex = 2;
            this.CbExpired.Text = "Vencida";
            this.CbExpired.UseVisualStyleBackColor = true;
            // 
            // DgvLicenseExpirations
            // 
            this.DgvLicenseExpirations.AllowUserToAddRows = false;
            this.DgvLicenseExpirations.AllowUserToDeleteRows = false;
            this.DgvLicenseExpirations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DgvLicenseExpirations.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvLicenseExpirations.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvLicenseExpirations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvLicenseExpirations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.DriverId,
            this.DriverNameColumn,
            this.DateColumn,
            this.Status});
            this.DgvLicenseExpirations.Location = new System.Drawing.Point(18, 192);
            this.DgvLicenseExpirations.MultiSelect = false;
            this.DgvLicenseExpirations.Name = "DgvLicenseExpirations";
            this.DgvLicenseExpirations.ReadOnly = true;
            this.DgvLicenseExpirations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvLicenseExpirations.Size = new System.Drawing.Size(403, 150);
            this.DgvLicenseExpirations.TabIndex = 7;
            this.DgvLicenseExpirations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvLicenseExpirations_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // DriverId
            // 
            this.DriverId.DataPropertyName = "DriverId";
            this.DriverId.HeaderText = "Id_Motorista";
            this.DriverId.Name = "DriverId";
            this.DriverId.ReadOnly = true;
            this.DriverId.Visible = false;
            // 
            // DriverNameColumn
            // 
            this.DriverNameColumn.DataPropertyName = "Name";
            this.DriverNameColumn.HeaderText = "Nome";
            this.DriverNameColumn.Name = "Name";
            this.DriverNameColumn.ReadOnly = true;
            this.DriverNameColumn.Width = 65;
            // 
            // DateColumn
            // 
            this.DateColumn.DataPropertyName = "Date";
            this.DateColumn.HeaderText = "Data";
            this.DateColumn.Name = "Date";
            this.DateColumn.ReadOnly = true;
            this.DateColumn.Width = 59;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 68;
            // 
            // LblExpiration
            // 
            this.LblExpiration.AutoSize = true;
            this.LblExpiration.Location = new System.Drawing.Point(18, 176);
            this.LblExpiration.Name = "LblExpiration";
            this.LblExpiration.Size = new System.Drawing.Size(107, 13);
            this.LblExpiration.TabIndex = 11;
            this.LblExpiration.Text = "Vencimento - 000";
            // 
            // DriverControl
            // 
            this.DriverControl.Location = new System.Drawing.Point(18, 10);
            this.DriverControl.Name = "DriverControl";
            this.DriverControl.Size = new System.Drawing.Size(322, 44);
            this.DriverControl.TabIndex = 0;
            // 
            // LicenseExpirationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 353);
            this.Controls.Add(this.DriverControl);
            this.Controls.Add(this.LblExpiration);
            this.Controls.Add(this.DgvLicenseExpirations);
            this.Controls.Add(this.CbExpired);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.MktDtExpiration);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseExpirationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Vencimento CNH";
            this.Load += new System.EventHandler(this.LicenseExpirationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvLicenseExpirations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox MktDtExpiration;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.CheckBox CbExpired;
        private System.Windows.Forms.DataGridView DgvLicenseExpirations;
        private System.Windows.Forms.Label LblExpiration;
        private DriverControl DriverControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DriverNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}