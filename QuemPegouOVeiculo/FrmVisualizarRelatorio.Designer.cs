namespace QuemPegouOVeiculo
{
    partial class FrmVisualizarRelatorio
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
            this.VeiculoObjBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.RptVisualizadorRelatorio = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.VeiculoObjBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // VeiculoObjBindingSource
            // 
            this.VeiculoObjBindingSource.DataSource = typeof(Objeto.VeiculoObj);
            // 
            // RptVisualizadorRelatorio
            // 
            this.RptVisualizadorRelatorio.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "VeiculoDataSet";
            reportDataSource1.Value = this.VeiculoObjBindingSource;
            this.RptVisualizadorRelatorio.LocalReport.DataSources.Add(reportDataSource1);
            this.RptVisualizadorRelatorio.LocalReport.ReportEmbeddedResource = "QuemPegouOVeiculo.Relatorios.RelVeiculo.rdlc";
            this.RptVisualizadorRelatorio.Location = new System.Drawing.Point(0, 0);
            this.RptVisualizadorRelatorio.Name = "RptVisualizadorRelatorio";
            this.RptVisualizadorRelatorio.ServerReport.BearerToken = null;
            this.RptVisualizadorRelatorio.ShowBackButton = false;
            this.RptVisualizadorRelatorio.ShowFindControls = false;
            this.RptVisualizadorRelatorio.ShowRefreshButton = false;
            this.RptVisualizadorRelatorio.ShowStopButton = false;
            this.RptVisualizadorRelatorio.Size = new System.Drawing.Size(800, 507);
            this.RptVisualizadorRelatorio.TabIndex = 0;
            this.RptVisualizadorRelatorio.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // FrmVisualizarRelatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 507);
            this.Controls.Add(this.RptVisualizadorRelatorio);
            this.MinimizeBox = false;
            this.Name = "FrmVisualizarRelatorio";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizar Relatório";
            this.Load += new System.EventHandler(this.FrmVisualizarRelatorio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.VeiculoObjBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer RptVisualizadorRelatorio;
        private System.Windows.Forms.BindingSource VeiculoObjBindingSource;
    }
}