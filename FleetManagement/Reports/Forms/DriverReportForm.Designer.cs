namespace FleetManagement
{
    partial class DriverReportForm
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
            this.BtnList = new System.Windows.Forms.Button();
            this.CbxListDriver = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BtnList
            // 
            this.BtnList.Location = new System.Drawing.Point(181, 36);
            this.BtnList.Name = "BtnList";
            this.BtnList.Size = new System.Drawing.Size(75, 23);
            this.BtnList.TabIndex = 1;
            this.BtnList.Text = "Listar";
            this.BtnList.UseVisualStyleBackColor = true;
            this.BtnList.Click += new System.EventHandler(this.BtnList_Click);
            // 
            // CbxListDriver
            // 
            this.CbxListDriver.FormattingEnabled = true;
            this.CbxListDriver.Items.AddRange(new object[] {
            "Todos",
            "Ativos",
            "Desativados"});
            this.CbxListDriver.Location = new System.Drawing.Point(15, 36);
            this.CbxListDriver.Name = "CbxListDriver";
            this.CbxListDriver.Size = new System.Drawing.Size(137, 21);
            this.CbxListDriver.TabIndex = 0;
            this.CbxListDriver.SelectedIndexChanged += new System.EventHandler(this.CbxListDriver_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Listar motoristas";
            // 
            // DriverReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 78);
            this.Controls.Add(this.BtnList);
            this.Controls.Add(this.CbxListDriver);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DriverReportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório Motorista";
            this.Load += new System.EventHandler(this.DriverReportForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnList;
        private System.Windows.Forms.ComboBox CbxListDriver;
        private System.Windows.Forms.Label label1;
    }
}