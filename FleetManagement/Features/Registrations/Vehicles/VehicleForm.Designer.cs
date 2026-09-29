namespace FleetManagement
{
    partial class VehicleForm
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
            this.TxtPlate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtModel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtChassis = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TxtRenavam = new System.Windows.Forms.TextBox();
            this.CbActive = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.DgvVehicle = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Plate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Chassis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Renavam = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblVehicles = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgvVehicle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Placa";
            // 
            // TxtPlate
            // 
            this.TxtPlate.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtPlate.Location = new System.Drawing.Point(14, 26);
            this.TxtPlate.MaxLength = 8;
            this.TxtPlate.Name = "TxtPlate";
            this.TxtPlate.Size = new System.Drawing.Size(111, 21);
            this.TxtPlate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Modelo";
            // 
            // TxtModel
            // 
            this.TxtModel.Location = new System.Drawing.Point(14, 66);
            this.TxtModel.MaxLength = 255;
            this.TxtModel.Name = "TxtModel";
            this.TxtModel.Size = new System.Drawing.Size(186, 21);
            this.TxtModel.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Chassi";
            // 
            // TxtChassis
            // 
            this.TxtChassis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtChassis.Location = new System.Drawing.Point(14, 106);
            this.TxtChassis.MaxLength = 22;
            this.TxtChassis.Name = "TxtChassis";
            this.TxtChassis.Size = new System.Drawing.Size(221, 21);
            this.TxtChassis.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(242, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Renavam";
            // 
            // TxtRenavam
            // 
            this.TxtRenavam.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TxtRenavam.Location = new System.Drawing.Point(241, 106);
            this.TxtRenavam.MaxLength = 12;
            this.TxtRenavam.Name = "TxtRenavam";
            this.TxtRenavam.Size = new System.Drawing.Size(120, 21);
            this.TxtRenavam.TabIndex = 4;
            // 
            // CbActive
            // 
            this.CbActive.AutoSize = true;
            this.CbActive.Checked = true;
            this.CbActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CbActive.Location = new System.Drawing.Point(17, 143);
            this.CbActive.Name = "CbActive";
            this.CbActive.Size = new System.Drawing.Size(55, 17);
            this.CbActive.TabIndex = 5;
            this.CbActive.Text = "Ativo";
            this.CbActive.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Pesquisa";
            // 
            // TxtSearch
            // 
            this.TxtSearch.Location = new System.Drawing.Point(14, 193);
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(453, 21);
            this.TxtSearch.TabIndex = 9;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(392, 13);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(75, 23);
            this.BtnSave.TabIndex = 6;
            this.BtnSave.Text = "&Gravar";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Enabled = false;
            this.BtnEdit.Location = new System.Drawing.Point(392, 42);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(75, 23);
            this.BtnEdit.TabIndex = 7;
            this.BtnEdit.Text = "&Alterar";
            this.BtnEdit.UseVisualStyleBackColor = true;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.Enabled = false;
            this.BtnDelete.Location = new System.Drawing.Point(392, 71);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(75, 23);
            this.BtnDelete.TabIndex = 8;
            this.BtnDelete.Text = "&Excluir";
            this.BtnDelete.UseVisualStyleBackColor = true;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
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
            this.Plate,
            this.Model,
            this.Chassis,
            this.Renavam,
            this.Status});
            this.DgvVehicle.Location = new System.Drawing.Point(14, 243);
            this.DgvVehicle.MultiSelect = false;
            this.DgvVehicle.Name = "DgvVehicle";
            this.DgvVehicle.ReadOnly = true;
            this.DgvVehicle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DgvVehicle.Size = new System.Drawing.Size(453, 165);
            this.DgvVehicle.TabIndex = 10;
            this.DgvVehicle.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvVehicle_CellDoubleClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // Plate
            // 
            this.Plate.DataPropertyName = "Plate";
            this.Plate.HeaderText = "Placa";
            this.Plate.Name = "Plate";
            this.Plate.ReadOnly = true;
            // 
            // Model
            // 
            this.Model.DataPropertyName = "Model";
            this.Model.HeaderText = "Modelo";
            this.Model.Name = "Model";
            this.Model.ReadOnly = true;
            // 
            // Chassis
            // 
            this.Chassis.DataPropertyName = "Chassis";
            this.Chassis.HeaderText = "Chassi";
            this.Chassis.Name = "Chassis";
            this.Chassis.ReadOnly = true;
            // 
            // Renavam
            // 
            this.Renavam.DataPropertyName = "Renavam";
            this.Renavam.HeaderText = "Renavam";
            this.Renavam.Name = "Renavam";
            this.Renavam.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // LblVehicles
            // 
            this.LblVehicles.AutoSize = true;
            this.LblVehicles.Location = new System.Drawing.Point(14, 227);
            this.LblVehicles.Name = "LblVehicles";
            this.LblVehicles.Size = new System.Drawing.Size(87, 13);
            this.LblVehicles.TabIndex = 15;
            this.LblVehicles.Text = "Veículos - 000";
            // 
            // VehicleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 419);
            this.Controls.Add(this.LblVehicles);
            this.Controls.Add(this.DgvVehicle);
            this.Controls.Add(this.BtnDelete);
            this.Controls.Add(this.BtnEdit);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CbActive);
            this.Controls.Add(this.TxtRenavam);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TxtChassis);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TxtModel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtPlate);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VehicleForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Veículo";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.VehicleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgvVehicle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPlate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtModel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtChassis;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtRenavam;
        private System.Windows.Forms.CheckBox CbActive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Button BtnSave;
        private System.Windows.Forms.Button BtnEdit;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.DataGridView DgvVehicle;
        private System.Windows.Forms.Label LblVehicles;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Plate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn Chassis;
        private System.Windows.Forms.DataGridViewTextBoxColumn Renavam;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}