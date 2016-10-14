namespace TiagoDesktop
{
    partial class GerenciadorTarefas
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
            this.dgvProcessos = new System.Windows.Forms.DataGridView();
            this.Processo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Memoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contador = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProcessos
            // 
            this.dgvProcessos.AllowUserToAddRows = false;
            this.dgvProcessos.AllowUserToDeleteRows = false;
            this.dgvProcessos.AllowUserToResizeColumns = false;
            this.dgvProcessos.AllowUserToResizeRows = false;
            this.dgvProcessos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvProcessos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvProcessos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Processo,
            this.CPU,
            this.Memoria});
            this.dgvProcessos.Location = new System.Drawing.Point(12, 12);
            this.dgvProcessos.MultiSelect = false;
            this.dgvProcessos.Name = "dgvProcessos";
            this.dgvProcessos.ReadOnly = true;
            this.dgvProcessos.Size = new System.Drawing.Size(307, 417);
            this.dgvProcessos.TabIndex = 3;
            this.dgvProcessos.TabStop = false;
            this.dgvProcessos.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvProcessos_CellMouseClick);
            // 
            // Processo
            // 
            this.Processo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Processo.HeaderText = "Nome";
            this.Processo.Name = "Processo";
            this.Processo.ReadOnly = true;
            // 
            // CPU
            // 
            this.CPU.HeaderText = "CPU";
            this.CPU.Name = "CPU";
            this.CPU.ReadOnly = true;
            this.CPU.Width = 54;
            // 
            // Memoria
            // 
            this.Memoria.HeaderText = "Memória";
            this.Memoria.Name = "Memoria";
            this.Memoria.ReadOnly = true;
            this.Memoria.Width = 72;
            // 
            // contador
            // 
            this.contador.Enabled = true;
            this.contador.Interval = 2000;
            this.contador.Tick += new System.EventHandler(this.contador_Tick);
            // 
            // GerenciadorTarefas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 441);
            this.Controls.Add(this.dgvProcessos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GerenciadorTarefas";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processos em Execução";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvProcessos;
        private System.Windows.Forms.Timer contador;
        private System.Windows.Forms.DataGridViewTextBoxColumn Processo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPU;
        private System.Windows.Forms.DataGridViewTextBoxColumn Memoria;
    }
}