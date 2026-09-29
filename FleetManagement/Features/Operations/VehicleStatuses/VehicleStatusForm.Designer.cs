namespace FleetManagement
{
    partial class VehicleStatusForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.MktStartAt = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MktEndAt = new System.Windows.Forms.MaskedTextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.DgvVehicle = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblStatus = new System.Windows.Forms.Label();
            this.LnkCurrentStart = new System.Windows.Forms.LinkLabel();
            this.LnkCurrentEnd = new System.Windows.Forms.LinkLabel();
            this.DescriptionControl = new FleetManagement.DescriptionControl();
            this.VehicleControl = new FleetManagement.VehicleControl();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVehicle)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Dt. Inicio";
            // 
            // MktStartAt
            // 
            this.MktStartAt.Location = new System.Drawing.Point(13, 95);
            this.MktStartAt.Mask = "00/00/0000 00:00";
            this.MktStartAt.Name = "MktStartAt";
            this.MktStartAt.Size = new System.Drawing.Size(121, 21);
            this.MktStartAt.TabIndex = 1;
            this.MktStartAt.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(199, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Dt. Final";
            // 
            // MktEndAt
            // 
            this.MktEndAt.Location = new System.Drawing.Point(202, 95);
            this.MktEndAt.Mask = "00/00/0000 00:00";
            this.MktEndAt.Name = "MktEndAt";
            this.MktEndAt.Size = new System.Drawing.Size(121, 21);
            this.MktEndAt.TabIndex = 2;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(352, 12);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 4;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(352, 41);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 23);
            this.BtnEdit.TabIndex = 5;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(352, 70);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 6;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Pesquisa";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(13, 210);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(414, 21);
            this.TxtSearch.TabIndex = 7;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // DgvVehicle
            // 
            this.DgvVehicle.AllowUserToAddRows = false;
            this.DgvVehicle.AllowUserToDeleteRows = false;
            this.DgvVehicle.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DgvVehicle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DgvVehicle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgvVehicle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.VehicleId,
            this.Model,
            this.Description,
            this.StartAt,
            this.EndAt});
            this.DgvVehicle.Location = new System.Drawing.Point(13, 260);
            this.DgvVehicle.MultiSelect = false;
            this.DgvVehicle.Name = "DgvVehicle";
            this.DgvVehicle.ReadOnly = true;
            this.DgvVehicle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVehicle.Size = new System.Drawing.Size(414, 150);
            this.DgvVehicle.TabIndex = 8;
            this.DgvVehicle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVehicle_CellDoubleClick);
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
            // Model
            // 
            this.Model.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Modelo";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            this.Model.Width = 72;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Descrição";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 88;
            // 
            // StartAt
            // 
            this.StartAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.StartAt.DataPropertyName = "StartAt";
            dataGridViewCellStyle1.Format = "g";
            dataGridViewCellStyle1.NullValue = null;
            this.StartAt.DefaultCellStyle = dataGridViewCellStyle1;
            this.StartAt.HeaderText = "Data_Hora_Inicio";
            this.StartAt.Name = "StartAt";
            this.StartAt.ReadOnly = true;
            this.StartAt.Width = 131;
            // 
            // EndAt
            // 
            this.EndAt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EndAt.DataPropertyName = "EndAt";
            dataGridViewCellStyle2.Format = "g";
            dataGridViewCellStyle2.NullValue = null;
            this.EndAt.DefaultCellStyle = dataGridViewCellStyle2;
            this.EndAt.HeaderText = "Data_Hora_Final";
            this.EndAt.Name = "EndAt";
            this.EndAt.ReadOnly = true;
            this.EndAt.Width = 126;
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Location = new System.Drawing.Point(13, 244);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(77, 13);
            this.LblStatus.TabIndex = 14;
            this.LblStatus.Text = "Status - 000";
            // 
            // LnkCurrentStart
            // 
            this.LnkCurrentStart.AutoSize = true;
            this.LnkCurrentStart.Location = new System.Drawing.Point(78, 79);
            this.LnkCurrentStart.Name = "LnkCurrentStart";
            this.LnkCurrentStart.Size = new System.Drawing.Size(36, 13);
            this.LnkCurrentStart.TabIndex = 18;
            this.LnkCurrentStart.TabStop = true;
            this.LnkCurrentStart.Text = "Atual";
            this.LnkCurrentStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkCurrentStart_LinkClicked);
            // 
            // LnkCurrentEnd
            // 
            this.LnkCurrentEnd.AutoSize = true;
            this.LnkCurrentEnd.Location = new System.Drawing.Point(259, 79);
            this.LnkCurrentEnd.Name = "LnkCurrentEnd";
            this.LnkCurrentEnd.Size = new System.Drawing.Size(36, 13);
            this.LnkCurrentEnd.TabIndex = 19;
            this.LnkCurrentEnd.TabStop = true;
            this.LnkCurrentEnd.Text = "Atual";
            this.LnkCurrentEnd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkCurrentEnd_LinkClicked);
            // 
            // DescriptionControl
            // 
            this.DescriptionControl.Location = new System.Drawing.Point(13, 122);
            this.DescriptionControl.Name = "DescriptionControl";
            this.DescriptionControl.Size = new System.Drawing.Size(414, 69);
            this.DescriptionControl.TabIndex = 3;
            // 
            // VehicleControl
            // 
            this.VehicleControl.Location = new System.Drawing.Point(13, 12);
            this.VehicleControl.Name = "VehicleControl";
            this.VehicleControl.Size = new System.Drawing.Size(310, 45);
            this.VehicleControl.TabIndex = 0;
            // 
            // VehicleStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 423);
            this.Controls.Add(this.LnkCurrentEnd);
            this.Controls.Add(this.LnkCurrentStart);
            this.Controls.Add(this.DescriptionControl);
            this.Controls.Add(this.VehicleControl);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.DgvVehicle);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.MktEndAt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.MktStartAt);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleStatusForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro Status Veículo";
            this.Load += new System.EventHandler(this.VehicleStatusForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVehicle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox MktStartAt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox MktEndAt;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.DataGridView DgvVehicle;
        private System.Windows.Forms.Label LblStatus;
        private VehicleControl VehicleControl;
        private DescriptionControl DescriptionControl;
        private System.Windows.Forms.LinkLabel LnkCurrentStart;
        private System.Windows.Forms.LinkLabel LnkCurrentEnd;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndAt;
    }
}