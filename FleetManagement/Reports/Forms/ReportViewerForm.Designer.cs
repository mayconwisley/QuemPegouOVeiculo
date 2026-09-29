namespace FleetManagement
{
    partial class ReportViewerForm
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.VehicleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ReportViewerControl = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.VehicleBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // VehicleBindingSource
            // 
            this.VehicleBindingSource.DataSource = typeof(FleetManagement.Desktop.Models.VehicleModel);
            // 
            // ReportViewerControl
            // 
            this.ReportViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "VehicleDataSet";
            reportDataSource1.Value = this.VehicleBindingSource;
            this.ReportViewerControl.LocalReport.DataSources.Add(reportDataSource1);
            this.ReportViewerControl.LocalReport.ReportEmbeddedResource = "FleetManagement.Reports.Templates.VehicleReport.rdlc";
            this.ReportViewerControl.Location = new System.Drawing.Point(0, 0);
            this.ReportViewerControl.Name = "ReportViewerControl";
            this.ReportViewerControl.ServerReport.BearerToken = null;
            this.ReportViewerControl.ShowBackButton = false;
            this.ReportViewerControl.ShowFindControls = false;
            this.ReportViewerControl.ShowRefreshButton = false;
            this.ReportViewerControl.ShowStopButton = false;
            this.ReportViewerControl.Size = new System.Drawing.Size(800, 507);
            this.ReportViewerControl.TabIndex = 0;
            this.ReportViewerControl.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // ReportViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 507);
            this.Controls.Add(this.ReportViewerControl);
            this.MinimizeBox = false;
            this.Name = "ReportViewerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizar Relatório";
            this.Load += new System.EventHandler(this.ReportViewerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VehicleBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer ReportViewerControl;
        private System.Windows.Forms.BindingSource VehicleBindingSource;
    }
}
