namespace QuemPegouOVeiculo
{
    partial class FrmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.MstPrincipal = new System.Windows.Forms.MenuStrip();
            this.MenuCadastro = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuCadVeiculo = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuCadMotorista = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuCadVenciCNH = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuCadStatusVeic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuControle = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuConVeiculo = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuConAbastecimento = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuConMulta = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuConManutencao = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuChegada = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuCheControle = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRelatorio = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelControle = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelConVeiculo = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelConAbastecimento = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelConManutencao = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelConMulta = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelCadastro = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelCadVeiculo = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelCadMotorista = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelCadStatusVeic = new System.Windows.Forms.ToolStripMenuItem();
            this.SubMenuRelCadVencCNH = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSair = new System.Windows.Forms.ToolStripMenuItem();
            this.SstPrincipal = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MstPrincipal.SuspendLayout();
            this.SstPrincipal.SuspendLayout();
            this.SuspendLayout();
            // 
            // MstPrincipal
            // 
            this.MstPrincipal.BackColor = System.Drawing.SystemColors.Control;
            this.MstPrincipal.Font = new System.Drawing.Font("Verdana", 9F);
            this.MstPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuCadastro,
            this.MenuControle,
            this.MenuChegada,
            this.MenuRelatorio,
            this.MenuSair});
            this.MstPrincipal.Location = new System.Drawing.Point(0, 0);
            this.MstPrincipal.Name = "MstPrincipal";
            this.MstPrincipal.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MstPrincipal.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.MstPrincipal.Size = new System.Drawing.Size(1173, 24);
            this.MstPrincipal.TabIndex = 0;
            // 
            // MenuCadastro
            // 
            this.MenuCadastro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuCadVeiculo,
            this.SubMenuCadMotorista,
            this.SubMenuCadVenciCNH,
            this.SubMenuCadStatusVeic});
            this.MenuCadastro.Name = "MenuCadastro";
            this.MenuCadastro.Size = new System.Drawing.Size(77, 20);
            this.MenuCadastro.Text = "Cadastro";
            // 
            // SubMenuCadVeiculo
            // 
            this.SubMenuCadVeiculo.Name = "SubMenuCadVeiculo";
            this.SubMenuCadVeiculo.Size = new System.Drawing.Size(177, 22);
            this.SubMenuCadVeiculo.Text = "Veículo";
            this.SubMenuCadVeiculo.Click += new System.EventHandler(this.SubMenuCadVeiculo_Click);
            // 
            // SubMenuCadMotorista
            // 
            this.SubMenuCadMotorista.Name = "SubMenuCadMotorista";
            this.SubMenuCadMotorista.Size = new System.Drawing.Size(177, 22);
            this.SubMenuCadMotorista.Text = "Motorista";
            this.SubMenuCadMotorista.Click += new System.EventHandler(this.SubMenuCadMotorista_Click);
            // 
            // SubMenuCadVenciCNH
            // 
            this.SubMenuCadVenciCNH.Name = "SubMenuCadVenciCNH";
            this.SubMenuCadVenciCNH.Size = new System.Drawing.Size(177, 22);
            this.SubMenuCadVenciCNH.Text = "Vencimento CNH";
            this.SubMenuCadVenciCNH.Click += new System.EventHandler(this.SubMenuCadVenciCNH_Click);
            // 
            // SubMenuCadStatusVeic
            // 
            this.SubMenuCadStatusVeic.Name = "SubMenuCadStatusVeic";
            this.SubMenuCadStatusVeic.Size = new System.Drawing.Size(177, 22);
            this.SubMenuCadStatusVeic.Text = "Status Veículo";
            this.SubMenuCadStatusVeic.Click += new System.EventHandler(this.SubMenuCadStatusVeic_Click);
            // 
            // MenuControle
            // 
            this.MenuControle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuConVeiculo,
            this.SubMenuConAbastecimento,
            this.SubMenuConMulta,
            this.SubMenuConManutencao});
            this.MenuControle.Name = "MenuControle";
            this.MenuControle.Size = new System.Drawing.Size(73, 20);
            this.MenuControle.Text = "Controle";
            // 
            // SubMenuConVeiculo
            // 
            this.SubMenuConVeiculo.Name = "SubMenuConVeiculo";
            this.SubMenuConVeiculo.Size = new System.Drawing.Size(167, 22);
            this.SubMenuConVeiculo.Text = "Veículo";
            this.SubMenuConVeiculo.Click += new System.EventHandler(this.SubMenuConVeiculo_Click);
            // 
            // SubMenuConAbastecimento
            // 
            this.SubMenuConAbastecimento.Name = "SubMenuConAbastecimento";
            this.SubMenuConAbastecimento.Size = new System.Drawing.Size(167, 22);
            this.SubMenuConAbastecimento.Text = "Abastecimento";
            this.SubMenuConAbastecimento.Click += new System.EventHandler(this.SubMenuConAbastecimento_Click);
            // 
            // SubMenuConMulta
            // 
            this.SubMenuConMulta.Name = "SubMenuConMulta";
            this.SubMenuConMulta.Size = new System.Drawing.Size(167, 22);
            this.SubMenuConMulta.Text = "Multa";
            this.SubMenuConMulta.Click += new System.EventHandler(this.SubMenuConMulta_Click);
            // 
            // SubMenuConManutencao
            // 
            this.SubMenuConManutencao.Name = "SubMenuConManutencao";
            this.SubMenuConManutencao.Size = new System.Drawing.Size(167, 22);
            this.SubMenuConManutencao.Text = "Manutenção";
            this.SubMenuConManutencao.Click += new System.EventHandler(this.SubMenuConManutencao_Click);
            // 
            // MenuChegada
            // 
            this.MenuChegada.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuCheControle});
            this.MenuChegada.Name = "MenuChegada";
            this.MenuChegada.Size = new System.Drawing.Size(76, 20);
            this.MenuChegada.Text = "Chegada";
            // 
            // SubMenuCheControle
            // 
            this.SubMenuCheControle.Name = "SubMenuCheControle";
            this.SubMenuCheControle.Size = new System.Drawing.Size(128, 22);
            this.SubMenuCheControle.Text = "Controle";
            this.SubMenuCheControle.Click += new System.EventHandler(this.SubMenuCheControle_Click);
            // 
            // MenuRelatorio
            // 
            this.MenuRelatorio.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRelControle,
            this.SubMenuRelCadastro});
            this.MenuRelatorio.Name = "MenuRelatorio";
            this.MenuRelatorio.Size = new System.Drawing.Size(75, 20);
            this.MenuRelatorio.Text = "Relatório";
            // 
            // SubMenuRelControle
            // 
            this.SubMenuRelControle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRelConVeiculo,
            this.SubMenuRelConAbastecimento,
            this.SubMenuRelConManutencao,
            this.SubMenuRelConMulta});
            this.SubMenuRelControle.Name = "SubMenuRelControle";
            this.SubMenuRelControle.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelControle.Text = "Controle";
            // 
            // SubMenuRelConVeiculo
            // 
            this.SubMenuRelConVeiculo.Name = "SubMenuRelConVeiculo";
            this.SubMenuRelConVeiculo.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelConVeiculo.Text = "Veículo";
            this.SubMenuRelConVeiculo.Click += new System.EventHandler(this.SubMenuRelConVeiculo_Click);
            // 
            // SubMenuRelConAbastecimento
            // 
            this.SubMenuRelConAbastecimento.Name = "SubMenuRelConAbastecimento";
            this.SubMenuRelConAbastecimento.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelConAbastecimento.Text = "Abastecimento";
            this.SubMenuRelConAbastecimento.Click += new System.EventHandler(this.SubMenuRelConAbastecimento_Click);
            // 
            // SubMenuRelConManutencao
            // 
            this.SubMenuRelConManutencao.Name = "SubMenuRelConManutencao";
            this.SubMenuRelConManutencao.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelConManutencao.Text = "Manutenção";
            this.SubMenuRelConManutencao.Click += new System.EventHandler(this.SubMenuRelConManutencao_Click);
            // 
            // SubMenuRelConMulta
            // 
            this.SubMenuRelConMulta.Name = "SubMenuRelConMulta";
            this.SubMenuRelConMulta.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelConMulta.Text = "Multa";
            this.SubMenuRelConMulta.Click += new System.EventHandler(this.SubMenuRelConMulta_Click);
            // 
            // SubMenuRelCadastro
            // 
            this.SubMenuRelCadastro.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SubMenuRelCadVeiculo,
            this.SubMenuRelCadMotorista,
            this.SubMenuRelCadStatusVeic,
            this.SubMenuRelCadVencCNH});
            this.SubMenuRelCadastro.Name = "SubMenuRelCadastro";
            this.SubMenuRelCadastro.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelCadastro.Text = "Cadastro";
            // 
            // SubMenuRelCadVeiculo
            // 
            this.SubMenuRelCadVeiculo.Name = "SubMenuRelCadVeiculo";
            this.SubMenuRelCadVeiculo.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelCadVeiculo.Text = "Veículo";
            this.SubMenuRelCadVeiculo.Click += new System.EventHandler(this.SubMenuRelCadVeiculo_Click);
            // 
            // SubMenuRelCadMotorista
            // 
            this.SubMenuRelCadMotorista.Name = "SubMenuRelCadMotorista";
            this.SubMenuRelCadMotorista.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelCadMotorista.Text = "Motorista";
            this.SubMenuRelCadMotorista.Click += new System.EventHandler(this.SubMenuRelCadMotorista_Click);
            // 
            // SubMenuRelCadStatusVeic
            // 
            this.SubMenuRelCadStatusVeic.Name = "SubMenuRelCadStatusVeic";
            this.SubMenuRelCadStatusVeic.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelCadStatusVeic.Text = "Status Veículo";
            this.SubMenuRelCadStatusVeic.Click += new System.EventHandler(this.SubMenuRelCadStatusVeic_Click);
            // 
            // SubMenuRelCadVencCNH
            // 
            this.SubMenuRelCadVencCNH.Name = "SubMenuRelCadVencCNH";
            this.SubMenuRelCadVencCNH.Size = new System.Drawing.Size(180, 22);
            this.SubMenuRelCadVencCNH.Text = "Vencimento CNH";
            this.SubMenuRelCadVencCNH.Visible = false;
            // 
            // MenuSair
            // 
            this.MenuSair.Name = "MenuSair";
            this.MenuSair.Size = new System.Drawing.Size(43, 20);
            this.MenuSair.Text = "Sair";
            this.MenuSair.Click += new System.EventHandler(this.MenuSair_Click);
            // 
            // SstPrincipal
            // 
            this.SstPrincipal.Font = new System.Drawing.Font("Verdana", 9F);
            this.SstPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.SstPrincipal.Location = new System.Drawing.Point(0, 657);
            this.SstPrincipal.Name = "SstPrincipal";
            this.SstPrincipal.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.SstPrincipal.Size = new System.Drawing.Size(1173, 22);
            this.SstPrincipal.TabIndex = 1;
            this.SstPrincipal.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(217, 17);
            this.toolStripStatusLabel1.Text = "Desenvolvido por: Maycon Wisley";
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1173, 679);
            this.Controls.Add(this.SstPrincipal);
            this.Controls.Add(this.MstPrincipal);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.MstPrincipal;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quem pegou o veículo?";
            this.MstPrincipal.ResumeLayout(false);
            this.MstPrincipal.PerformLayout();
            this.SstPrincipal.ResumeLayout(false);
            this.SstPrincipal.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MstPrincipal;
        private System.Windows.Forms.StatusStrip SstPrincipal;
        private System.Windows.Forms.ToolStripMenuItem MenuCadastro;
        private System.Windows.Forms.ToolStripMenuItem SubMenuCadVeiculo;
        private System.Windows.Forms.ToolStripMenuItem SubMenuCadMotorista;
        private System.Windows.Forms.ToolStripMenuItem SubMenuCadVenciCNH;
        private System.Windows.Forms.ToolStripMenuItem SubMenuCadStatusVeic;
        private System.Windows.Forms.ToolStripMenuItem MenuControle;
        private System.Windows.Forms.ToolStripMenuItem SubMenuConVeiculo;
        private System.Windows.Forms.ToolStripMenuItem SubMenuConAbastecimento;
        private System.Windows.Forms.ToolStripMenuItem SubMenuConMulta;
        private System.Windows.Forms.ToolStripMenuItem SubMenuConManutencao;
        private System.Windows.Forms.ToolStripMenuItem MenuRelatorio;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelControle;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelConVeiculo;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelConAbastecimento;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelConManutencao;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelCadastro;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelCadVeiculo;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelCadMotorista;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelCadStatusVeic;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelCadVencCNH;
        private System.Windows.Forms.ToolStripMenuItem MenuSair;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem SubMenuRelConMulta;
        private System.Windows.Forms.ToolStripMenuItem MenuChegada;
        private System.Windows.Forms.ToolStripMenuItem SubMenuCheControle;
    }
}

