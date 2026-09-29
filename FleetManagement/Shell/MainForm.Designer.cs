namespace FleetManagement
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.NavigationMenuStrip = new System.Windows.Forms.MenuStrip();
            this.MenuRegistration = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DriverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LicenseExpirationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleStatusMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OperationsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleMovementMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefuelingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MaintenanceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuArrival = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleArrivalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.OperationsReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleMovementReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RefuelingReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MaintenanceReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FineReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RegistrationsReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DriverReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VehicleStatusReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LicenseExpirationReportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.NavigationMenuStrip.SuspendLayout();
            this.MainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // NavigationMenuStrip
            // 
            this.NavigationMenuStrip.BackColor = System.Drawing.SystemColors.Control;
            this.NavigationMenuStrip.Font = new System.Drawing.Font("Verdana", 9F);
            this.NavigationMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuRegistration,
            this.OperationsMenuItem,
            this.MenuArrival,
            this.MenuReport,
            this.ExitMenuItem});
            this.NavigationMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.NavigationMenuStrip.Name = "NavigationMenuStrip";
            this.NavigationMenuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.NavigationMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.NavigationMenuStrip.Size = new System.Drawing.Size(1008, 24);
            this.NavigationMenuStrip.TabIndex = 0;
            // 
            // MenuRegistration
            // 
            this.MenuRegistration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VehicleMenuItem,
            this.DriverMenuItem,
            this.LicenseExpirationMenuItem,
            this.VehicleStatusMenuItem});
            this.MenuRegistration.Name = "MenuRegistration";
            this.MenuRegistration.Size = new System.Drawing.Size(77, 20);
            this.MenuRegistration.Text = "Cadastro";
            // 
            // VehicleMenuItem
            // 
            this.VehicleMenuItem.Name = "VehicleMenuItem";
            this.VehicleMenuItem.Size = new System.Drawing.Size(177, 22);
            this.VehicleMenuItem.Text = "Veículo";
            this.VehicleMenuItem.Click += new System.EventHandler(this.VehicleMenuItem_Click);
            // 
            // DriverMenuItem
            // 
            this.DriverMenuItem.Name = "DriverMenuItem";
            this.DriverMenuItem.Size = new System.Drawing.Size(177, 22);
            this.DriverMenuItem.Text = "Motorista";
            this.DriverMenuItem.Click += new System.EventHandler(this.DriverMenuItem_Click);
            // 
            // LicenseExpirationMenuItem
            // 
            this.LicenseExpirationMenuItem.Name = "LicenseExpirationMenuItem";
            this.LicenseExpirationMenuItem.Size = new System.Drawing.Size(177, 22);
            this.LicenseExpirationMenuItem.Text = "Vencimento CNH";
            this.LicenseExpirationMenuItem.Click += new System.EventHandler(this.LicenseExpirationMenuItem_Click);
            // 
            // VehicleStatusMenuItem
            // 
            this.VehicleStatusMenuItem.Name = "VehicleStatusMenuItem";
            this.VehicleStatusMenuItem.Size = new System.Drawing.Size(177, 22);
            this.VehicleStatusMenuItem.Text = "Status Veículo";
            this.VehicleStatusMenuItem.Click += new System.EventHandler(this.VehicleStatusMenuItem_Click);
            // 
            // OperationsMenuItem
            // 
            this.OperationsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VehicleMovementMenuItem,
            this.RefuelingMenuItem,
            this.FineMenuItem,
            this.MaintenanceMenuItem});
            this.OperationsMenuItem.Name = "OperationsMenuItem";
            this.OperationsMenuItem.Size = new System.Drawing.Size(73, 20);
            this.OperationsMenuItem.Text = "Controle";
            // 
            // VehicleMovementMenuItem
            // 
            this.VehicleMovementMenuItem.Name = "VehicleMovementMenuItem";
            this.VehicleMovementMenuItem.Size = new System.Drawing.Size(167, 22);
            this.VehicleMovementMenuItem.Text = "Veículo";
            this.VehicleMovementMenuItem.Click += new System.EventHandler(this.VehicleMovementMenuItem_Click);
            // 
            // RefuelingMenuItem
            // 
            this.RefuelingMenuItem.Name = "RefuelingMenuItem";
            this.RefuelingMenuItem.Size = new System.Drawing.Size(167, 22);
            this.RefuelingMenuItem.Text = "Abastecimento";
            this.RefuelingMenuItem.Click += new System.EventHandler(this.RefuelingMenuItem_Click);
            // 
            // FineMenuItem
            // 
            this.FineMenuItem.Name = "FineMenuItem";
            this.FineMenuItem.Size = new System.Drawing.Size(167, 22);
            this.FineMenuItem.Text = "Multa";
            this.FineMenuItem.Click += new System.EventHandler(this.FineMenuItem_Click);
            // 
            // MaintenanceMenuItem
            // 
            this.MaintenanceMenuItem.Name = "MaintenanceMenuItem";
            this.MaintenanceMenuItem.Size = new System.Drawing.Size(167, 22);
            this.MaintenanceMenuItem.Text = "Manutenção";
            this.MaintenanceMenuItem.Click += new System.EventHandler(this.MaintenanceMenuItem_Click);
            // 
            // MenuArrival
            // 
            this.MenuArrival.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VehicleArrivalMenuItem});
            this.MenuArrival.Name = "MenuArrival";
            this.MenuArrival.Size = new System.Drawing.Size(76, 20);
            this.MenuArrival.Text = "Chegada";
            // 
            // VehicleArrivalMenuItem
            // 
            this.VehicleArrivalMenuItem.Name = "VehicleArrivalMenuItem";
            this.VehicleArrivalMenuItem.Size = new System.Drawing.Size(128, 22);
            this.VehicleArrivalMenuItem.Text = "Controle";
            this.VehicleArrivalMenuItem.Click += new System.EventHandler(this.VehicleArrivalMenuItem_Click);
            // 
            // MenuReport
            // 
            this.MenuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OperationsReportMenuItem,
            this.RegistrationsReportMenuItem});
            this.MenuReport.Name = "MenuReport";
            this.MenuReport.Size = new System.Drawing.Size(75, 20);
            this.MenuReport.Text = "Relatório";
            // 
            // OperationsReportMenuItem
            // 
            this.OperationsReportMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VehicleMovementReportMenuItem,
            this.RefuelingReportMenuItem,
            this.MaintenanceReportMenuItem,
            this.FineReportMenuItem});
            this.OperationsReportMenuItem.Name = "OperationsReportMenuItem";
            this.OperationsReportMenuItem.Size = new System.Drawing.Size(132, 22);
            this.OperationsReportMenuItem.Text = "Controle";
            // 
            // VehicleMovementReportMenuItem
            // 
            this.VehicleMovementReportMenuItem.Name = "VehicleMovementReportMenuItem";
            this.VehicleMovementReportMenuItem.Size = new System.Drawing.Size(167, 22);
            this.VehicleMovementReportMenuItem.Text = "Veículo";
            this.VehicleMovementReportMenuItem.Click += new System.EventHandler(this.VehicleMovementReportMenuItem_Click);
            // 
            // RefuelingReportMenuItem
            // 
            this.RefuelingReportMenuItem.Name = "RefuelingReportMenuItem";
            this.RefuelingReportMenuItem.Size = new System.Drawing.Size(167, 22);
            this.RefuelingReportMenuItem.Text = "Abastecimento";
            this.RefuelingReportMenuItem.Click += new System.EventHandler(this.RefuelingReportMenuItem_Click);
            // 
            // MaintenanceReportMenuItem
            // 
            this.MaintenanceReportMenuItem.Name = "MaintenanceReportMenuItem";
            this.MaintenanceReportMenuItem.Size = new System.Drawing.Size(167, 22);
            this.MaintenanceReportMenuItem.Text = "Manutenção";
            this.MaintenanceReportMenuItem.Click += new System.EventHandler(this.MaintenanceReportMenuItem_Click);
            // 
            // FineReportMenuItem
            // 
            this.FineReportMenuItem.Name = "FineReportMenuItem";
            this.FineReportMenuItem.Size = new System.Drawing.Size(167, 22);
            this.FineReportMenuItem.Text = "Multa";
            this.FineReportMenuItem.Click += new System.EventHandler(this.FineReportMenuItem_Click);
            // 
            // RegistrationsReportMenuItem
            // 
            this.RegistrationsReportMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VehicleReportMenuItem,
            this.DriverReportMenuItem,
            this.VehicleStatusReportMenuItem,
            this.LicenseExpirationReportMenuItem});
            this.RegistrationsReportMenuItem.Name = "RegistrationsReportMenuItem";
            this.RegistrationsReportMenuItem.Size = new System.Drawing.Size(132, 22);
            this.RegistrationsReportMenuItem.Text = "Cadastro";
            // 
            // VehicleReportMenuItem
            // 
            this.VehicleReportMenuItem.Name = "VehicleReportMenuItem";
            this.VehicleReportMenuItem.Size = new System.Drawing.Size(177, 22);
            this.VehicleReportMenuItem.Text = "Veículo";
            this.VehicleReportMenuItem.Click += new System.EventHandler(this.VehicleReportMenuItem_Click);
            // 
            // DriverReportMenuItem
            // 
            this.DriverReportMenuItem.Name = "DriverReportMenuItem";
            this.DriverReportMenuItem.Size = new System.Drawing.Size(177, 22);
            this.DriverReportMenuItem.Text = "Motorista";
            this.DriverReportMenuItem.Click += new System.EventHandler(this.DriverReportMenuItem_Click);
            // 
            // VehicleStatusReportMenuItem
            // 
            this.VehicleStatusReportMenuItem.Name = "VehicleStatusReportMenuItem";
            this.VehicleStatusReportMenuItem.Size = new System.Drawing.Size(177, 22);
            this.VehicleStatusReportMenuItem.Text = "Status Veículo";
            this.VehicleStatusReportMenuItem.Click += new System.EventHandler(this.VehicleStatusReportMenuItem_Click);
            // 
            // LicenseExpirationReportMenuItem
            // 
            this.LicenseExpirationReportMenuItem.Name = "LicenseExpirationReportMenuItem";
            this.LicenseExpirationReportMenuItem.Size = new System.Drawing.Size(177, 22);
            this.LicenseExpirationReportMenuItem.Text = "Vencimento CNH";
            this.LicenseExpirationReportMenuItem.Visible = false;
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(43, 20);
            this.ExitMenuItem.Text = "Sair";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // MainStatusStrip
            // 
            this.MainStatusStrip.Font = new System.Drawing.Font("Verdana", 9F);
            this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.MainStatusStrip.Location = new System.Drawing.Point(0, 659);
            this.MainStatusStrip.Name = "MainStatusStrip";
            this.MainStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.MainStatusStrip.Size = new System.Drawing.Size(1008, 22);
            this.MainStatusStrip.TabIndex = 1;
            this.MainStatusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(217, 17);
            this.toolStripStatusLabel1.Text = "Desenvolvido por: Maycon Wisley";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 681);
            this.Controls.Add(this.MainStatusStrip);
            this.Controls.Add(this.NavigationMenuStrip);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.NavigationMenuStrip;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quem pegou o veículo?";
            this.NavigationMenuStrip.ResumeLayout(false);
            this.NavigationMenuStrip.PerformLayout();
            this.MainStatusStrip.ResumeLayout(false);
            this.MainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip NavigationMenuStrip;
        private System.Windows.Forms.StatusStrip MainStatusStrip;
        private System.Windows.Forms.ToolStripMenuItem MenuRegistration;
        private System.Windows.Forms.ToolStripMenuItem VehicleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DriverMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LicenseExpirationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VehicleStatusMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OperationsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VehicleMovementMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefuelingMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MaintenanceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuReport;
        private System.Windows.Forms.ToolStripMenuItem OperationsReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VehicleMovementReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefuelingReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MaintenanceReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegistrationsReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VehicleReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DriverReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VehicleStatusReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LicenseExpirationReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem FineReportMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuArrival;
        private System.Windows.Forms.ToolStripMenuItem VehicleArrivalMenuItem;
    }
}

