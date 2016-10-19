namespace TiagoDesktop
{
    partial class TiagoDesktop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TiagoDesktop));
            this.menuInicial = new System.Windows.Forms.ToolStrip();
            this.iniciar = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnUser4 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUser3 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUser2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUser1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnReiniciar = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDesligar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnADM4 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnADM3 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnADM2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnADM1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWinExplorer = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExecutar = new System.Windows.Forms.ToolStripMenuItem();
            this.deslogar = new System.Windows.Forms.ToolStripButton();
            this.configuracao = new System.Windows.Forms.ToolStripButton();
            this.desligar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.gerenciadorTarefas = new System.Windows.Forms.ToolStripButton();
            this.separadorAbertos = new System.Windows.Forms.ToolStripSeparator();
            this.btnUser1Aberto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUser2Aberto = new System.Windows.Forms.ToolStripMenuItem();
            this.btnUser3Aberto = new System.Windows.Forms.ToolStripButton();
            this.btnUser4Aberto = new System.Windows.Forms.ToolStripButton();
            this.btnADMCMDAberto = new System.Windows.Forms.ToolStripButton();
            this.btnADMExplorerAberto = new System.Windows.Forms.ToolStripButton();
            this.btnADM1Aberto = new System.Windows.Forms.ToolStripButton();
            this.btnADM2Aberto = new System.Windows.Forms.ToolStripButton();
            this.btnADM3Aberto = new System.Windows.Forms.ToolStripButton();
            this.btnADM4Aberto = new System.Windows.Forms.ToolStripButton();
            this.tClock = new System.Windows.Forms.Timer(this.components);
            this.menuInicial.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuInicial
            // 
            this.menuInicial.AllowDrop = true;
            this.menuInicial.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuInicial.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuInicial.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciar,
            this.deslogar,
            this.configuracao,
            this.desligar,
            this.toolStripSeparator2,
            this.gerenciadorTarefas,
            this.separadorAbertos,
            this.btnUser1Aberto,
            this.btnUser2Aberto,
            this.btnUser3Aberto,
            this.btnUser4Aberto,
            this.btnADMCMDAberto,
            this.btnADMExplorerAberto,
            this.btnADM1Aberto,
            this.btnADM2Aberto,
            this.btnADM3Aberto,
            this.btnADM4Aberto});
            this.menuInicial.Location = new System.Drawing.Point(0, 530);
            this.menuInicial.Name = "menuInicial";
            this.menuInicial.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuInicial.Size = new System.Drawing.Size(584, 31);
            this.menuInicial.TabIndex = 0;
            this.menuInicial.Text = "Menu";
            // 
            // iniciar
            // 
            this.iniciar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.iniciar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUser4,
            this.btnUser3,
            this.btnUser2,
            this.btnUser1,
            this.btnReiniciar,
            this.btnDesligar,
            this.toolStripSeparator3,
            this.btnADM4,
            this.btnADM3,
            this.btnADM2,
            this.btnADM1,
            this.btnWinExplorer,
            this.btnExecutar});
            this.iniciar.Image = ((System.Drawing.Image)(resources.GetObject("iniciar.Image")));
            this.iniciar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.iniciar.Name = "iniciar";
            this.iniciar.Size = new System.Drawing.Size(37, 28);
            this.iniciar.Text = "Menu Iniciar";
            this.iniciar.DropDownClosed += new System.EventHandler(this.iniciar_DropDownClosed);
            this.iniciar.DropDownOpened += new System.EventHandler(this.iniciar_DropDownOpened);
            // 
            // btnUser4
            // 
            this.btnUser4.Name = "btnUser4";
            this.btnUser4.Size = new System.Drawing.Size(176, 30);
            this.btnUser4.Text = "Botão User 4";
            this.btnUser4.Visible = false;
            this.btnUser4.Click += new System.EventHandler(this.btnUser4_Click);
            // 
            // btnUser3
            // 
            this.btnUser3.Name = "btnUser3";
            this.btnUser3.Size = new System.Drawing.Size(176, 30);
            this.btnUser3.Text = "Botão User 3";
            this.btnUser3.Visible = false;
            this.btnUser3.Click += new System.EventHandler(this.btnUser3_Click);
            // 
            // btnUser2
            // 
            this.btnUser2.Name = "btnUser2";
            this.btnUser2.Size = new System.Drawing.Size(176, 30);
            this.btnUser2.Text = "Botão User 2";
            this.btnUser2.Visible = false;
            this.btnUser2.Click += new System.EventHandler(this.btnUser2_Click);
            // 
            // btnUser1
            // 
            this.btnUser1.Name = "btnUser1";
            this.btnUser1.Size = new System.Drawing.Size(176, 30);
            this.btnUser1.Text = "Botão User 1";
            this.btnUser1.Visible = false;
            this.btnUser1.Click += new System.EventHandler(this.btnUser1_Click);
            // 
            // btnReiniciar
            // 
            this.btnReiniciar.Image = global::TiagoDesktop.Properties.Resources.reboot;
            this.btnReiniciar.Name = "btnReiniciar";
            this.btnReiniciar.Size = new System.Drawing.Size(176, 30);
            this.btnReiniciar.Text = "Reiniciar";
            this.btnReiniciar.Click += new System.EventHandler(this.btnReiniciar_Click);
            // 
            // btnDesligar
            // 
            this.btnDesligar.Image = global::TiagoDesktop.Properties.Resources.shutdown;
            this.btnDesligar.Name = "btnDesligar";
            this.btnDesligar.Size = new System.Drawing.Size(176, 30);
            this.btnDesligar.Text = "Desligar";
            this.btnDesligar.Click += new System.EventHandler(this.btnDesligar_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(173, 6);
            // 
            // btnADM4
            // 
            this.btnADM4.Name = "btnADM4";
            this.btnADM4.Size = new System.Drawing.Size(176, 30);
            this.btnADM4.Text = "Botão ADM 4";
            this.btnADM4.Visible = false;
            this.btnADM4.Click += new System.EventHandler(this.btnADM4_Click);
            // 
            // btnADM3
            // 
            this.btnADM3.Name = "btnADM3";
            this.btnADM3.Size = new System.Drawing.Size(176, 30);
            this.btnADM3.Text = "Botão ADM 3";
            this.btnADM3.Visible = false;
            this.btnADM3.Click += new System.EventHandler(this.btnADM3_Click);
            // 
            // btnADM2
            // 
            this.btnADM2.Name = "btnADM2";
            this.btnADM2.Size = new System.Drawing.Size(176, 30);
            this.btnADM2.Text = "Botão ADM 2";
            this.btnADM2.Visible = false;
            this.btnADM2.Click += new System.EventHandler(this.btnADM2_Click);
            // 
            // btnADM1
            // 
            this.btnADM1.Name = "btnADM1";
            this.btnADM1.Size = new System.Drawing.Size(176, 30);
            this.btnADM1.Text = "Botão ADM 1";
            this.btnADM1.Visible = false;
            this.btnADM1.Click += new System.EventHandler(this.btnADM1_Click);
            // 
            // btnWinExplorer
            // 
            this.btnWinExplorer.Image = global::TiagoDesktop.Properties.Resources.explorer;
            this.btnWinExplorer.Name = "btnWinExplorer";
            this.btnWinExplorer.Size = new System.Drawing.Size(176, 30);
            this.btnWinExplorer.Text = "Windows Explorer";
            this.btnWinExplorer.Click += new System.EventHandler(this.btnWinExplorer_Click);
            // 
            // btnExecutar
            // 
            this.btnExecutar.Image = global::TiagoDesktop.Properties.Resources.execute;
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(176, 30);
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // deslogar
            // 
            this.deslogar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deslogar.Image = ((System.Drawing.Image)(resources.GetObject("deslogar.Image")));
            this.deslogar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deslogar.Name = "deslogar";
            this.deslogar.Size = new System.Drawing.Size(28, 28);
            this.deslogar.Text = "Deslogar Usuário";
            this.deslogar.Click += new System.EventHandler(this.Deslogar_Click);
            // 
            // configuracao
            // 
            this.configuracao.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.configuracao.Image = ((System.Drawing.Image)(resources.GetObject("configuracao.Image")));
            this.configuracao.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.configuracao.Name = "configuracao";
            this.configuracao.Size = new System.Drawing.Size(28, 28);
            this.configuracao.Text = "Configurações";
            this.configuracao.Click += new System.EventHandler(this.Configuracao_Click);
            // 
            // desligar
            // 
            this.desligar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.desligar.Image = ((System.Drawing.Image)(resources.GetObject("desligar.Image")));
            this.desligar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.desligar.Name = "desligar";
            this.desligar.Size = new System.Drawing.Size(28, 28);
            this.desligar.Text = "Desligar o Computador";
            this.desligar.Click += new System.EventHandler(this.Desligar_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // gerenciadorTarefas
            // 
            this.gerenciadorTarefas.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.gerenciadorTarefas.Image = global::TiagoDesktop.Properties.Resources.taskmanager;
            this.gerenciadorTarefas.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.gerenciadorTarefas.Name = "gerenciadorTarefas";
            this.gerenciadorTarefas.Size = new System.Drawing.Size(28, 28);
            this.gerenciadorTarefas.Text = "Gerenciador de Tarefas";
            this.gerenciadorTarefas.Click += new System.EventHandler(this.gerenciadorTarefas_Click);
            // 
            // separadorAbertos
            // 
            this.separadorAbertos.Name = "separadorAbertos";
            this.separadorAbertos.Size = new System.Drawing.Size(6, 31);
            // 
            // btnUser1Aberto
            // 
            this.btnUser1Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnUser1Aberto.Image")));
            this.btnUser1Aberto.Name = "btnUser1Aberto";
            this.btnUser1Aberto.Size = new System.Drawing.Size(36, 31);
            this.btnUser1Aberto.Visible = false;
            this.btnUser1Aberto.Click += new System.EventHandler(this.btnUser1Aberto_Click);
            // 
            // btnUser2Aberto
            // 
            this.btnUser2Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnUser2Aberto.Image")));
            this.btnUser2Aberto.Name = "btnUser2Aberto";
            this.btnUser2Aberto.Size = new System.Drawing.Size(36, 31);
            this.btnUser2Aberto.Visible = false;
            this.btnUser2Aberto.Click += new System.EventHandler(this.btnUser2Aberto_Click);
            // 
            // btnUser3Aberto
            // 
            this.btnUser3Aberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUser3Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnUser3Aberto.Image")));
            this.btnUser3Aberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUser3Aberto.Name = "btnUser3Aberto";
            this.btnUser3Aberto.Size = new System.Drawing.Size(28, 28);
            this.btnUser3Aberto.Visible = false;
            this.btnUser3Aberto.Click += new System.EventHandler(this.btnUser3Aberto_Click);
            // 
            // btnUser4Aberto
            // 
            this.btnUser4Aberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUser4Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnUser4Aberto.Image")));
            this.btnUser4Aberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUser4Aberto.Name = "btnUser4Aberto";
            this.btnUser4Aberto.Size = new System.Drawing.Size(28, 28);
            this.btnUser4Aberto.Visible = false;
            this.btnUser4Aberto.Click += new System.EventHandler(this.btnUser4Aberto_Click);
            // 
            // btnADMCMDAberto
            // 
            this.btnADMCMDAberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnADMCMDAberto.Image = ((System.Drawing.Image)(resources.GetObject("btnADMCMDAberto.Image")));
            this.btnADMCMDAberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnADMCMDAberto.Name = "btnADMCMDAberto";
            this.btnADMCMDAberto.Size = new System.Drawing.Size(28, 28);
            this.btnADMCMDAberto.Text = "t";
            this.btnADMCMDAberto.Visible = false;
            this.btnADMCMDAberto.Click += new System.EventHandler(this.btnADMCMDAberto_Click);
            // 
            // btnADMExplorerAberto
            // 
            this.btnADMExplorerAberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnADMExplorerAberto.Image = ((System.Drawing.Image)(resources.GetObject("btnADMExplorerAberto.Image")));
            this.btnADMExplorerAberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnADMExplorerAberto.Name = "btnADMExplorerAberto";
            this.btnADMExplorerAberto.Size = new System.Drawing.Size(28, 28);
            this.btnADMExplorerAberto.Visible = false;
            this.btnADMExplorerAberto.Click += new System.EventHandler(this.btnADMExplorerAberto_Click);
            // 
            // btnADM1Aberto
            // 
            this.btnADM1Aberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnADM1Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnADM1Aberto.Image")));
            this.btnADM1Aberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnADM1Aberto.Name = "btnADM1Aberto";
            this.btnADM1Aberto.Size = new System.Drawing.Size(28, 28);
            this.btnADM1Aberto.Visible = false;
            this.btnADM1Aberto.Click += new System.EventHandler(this.btnADM1Aberto_Click);
            // 
            // btnADM2Aberto
            // 
            this.btnADM2Aberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnADM2Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnADM2Aberto.Image")));
            this.btnADM2Aberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnADM2Aberto.Name = "btnADM2Aberto";
            this.btnADM2Aberto.Size = new System.Drawing.Size(28, 28);
            this.btnADM2Aberto.Visible = false;
            this.btnADM2Aberto.Click += new System.EventHandler(this.btnADM2Aberto_Click);
            // 
            // btnADM3Aberto
            // 
            this.btnADM3Aberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnADM3Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnADM3Aberto.Image")));
            this.btnADM3Aberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnADM3Aberto.Name = "btnADM3Aberto";
            this.btnADM3Aberto.Size = new System.Drawing.Size(28, 28);
            this.btnADM3Aberto.Visible = false;
            this.btnADM3Aberto.Click += new System.EventHandler(this.btnADM3Aberto_Click);
            // 
            // btnADM4Aberto
            // 
            this.btnADM4Aberto.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnADM4Aberto.Image = ((System.Drawing.Image)(resources.GetObject("btnADM4Aberto.Image")));
            this.btnADM4Aberto.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnADM4Aberto.Name = "btnADM4Aberto";
            this.btnADM4Aberto.Size = new System.Drawing.Size(28, 28);
            this.btnADM4Aberto.Visible = false;
            this.btnADM4Aberto.Click += new System.EventHandler(this.btnADM4Aberto_Click);
            // 
            // tClock
            // 
            this.tClock.Enabled = true;
            this.tClock.Tick += new System.EventHandler(this.tClock_Tick);
            // 
            // TiagoDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.menuInicial);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TiagoDesktop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TiagoDesktop";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TiagoDesktop_FormClosing);
            this.Load += new System.EventHandler(this.TiagoDesktop_Load);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.TiagoDesktop_HelpRequested);
            this.menuInicial.ResumeLayout(false);
            this.menuInicial.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripDropDownButton iniciar;
        private System.Windows.Forms.ToolStripButton configuracao;
        private System.Windows.Forms.ToolStripButton deslogar;
        private System.Windows.Forms.ToolStripButton desligar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton gerenciadorTarefas;
        private System.Windows.Forms.ToolStripMenuItem btnReiniciar;
        private System.Windows.Forms.ToolStrip menuInicial;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem btnWinExplorer;
        private System.Windows.Forms.ToolStripMenuItem btnExecutar;
        private System.Windows.Forms.ToolStripSeparator separadorAbertos;
        private System.Windows.Forms.ToolStripMenuItem btnUser1Aberto;
        private System.Windows.Forms.ToolStripMenuItem btnDesligar;
        private System.Windows.Forms.ToolStripMenuItem btnUser2Aberto;
        private System.Windows.Forms.Timer tClock;
        private System.Windows.Forms.ToolStripMenuItem btnADM4;
        private System.Windows.Forms.ToolStripMenuItem btnADM3;
        private System.Windows.Forms.ToolStripMenuItem btnADM2;
        private System.Windows.Forms.ToolStripMenuItem btnADM1;
        private System.Windows.Forms.ToolStripMenuItem btnUser4;
        private System.Windows.Forms.ToolStripMenuItem btnUser3;
        private System.Windows.Forms.ToolStripMenuItem btnUser2;
        private System.Windows.Forms.ToolStripMenuItem btnUser1;
        private System.Windows.Forms.ToolStripButton btnUser3Aberto;
        private System.Windows.Forms.ToolStripButton btnUser4Aberto;
        private System.Windows.Forms.ToolStripButton btnADMCMDAberto;
        private System.Windows.Forms.ToolStripButton btnADMExplorerAberto;
        private System.Windows.Forms.ToolStripButton btnADM1Aberto;
        private System.Windows.Forms.ToolStripButton btnADM2Aberto;
        private System.Windows.Forms.ToolStripButton btnADM3Aberto;
        private System.Windows.Forms.ToolStripButton btnADM4Aberto;
    }
}

