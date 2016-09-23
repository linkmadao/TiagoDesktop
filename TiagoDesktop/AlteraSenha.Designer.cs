namespace TiagoDesktop
{
    partial class AlteraSenha
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
            this.btnAlterar = new System.Windows.Forms.Button();
            this.txtNovaSenha2 = new System.Windows.Forms.TextBox();
            this.txtNovaSenha = new System.Windows.Forms.TextBox();
            this.txtSenhaAtual = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNovaSenha2 = new System.Windows.Forms.Label();
            this.lblNovaSenha = new System.Windows.Forms.Label();
            this.lblSenhaAtual = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAlterar
            // 
            this.btnAlterar.Location = new System.Drawing.Point(21, 86);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(234, 23);
            this.btnAlterar.TabIndex = 4;
            this.btnAlterar.Text = "Alterar Senha";
            this.btnAlterar.UseVisualStyleBackColor = true;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // txtNovaSenha2
            // 
            this.txtNovaSenha2.Location = new System.Drawing.Point(111, 59);
            this.txtNovaSenha2.Name = "txtNovaSenha2";
            this.txtNovaSenha2.PasswordChar = '*';
            this.txtNovaSenha2.Size = new System.Drawing.Size(129, 20);
            this.txtNovaSenha2.TabIndex = 3;
            this.txtNovaSenha2.TextChanged += new System.EventHandler(this.txtNovaSenha2_TextChanged);
            // 
            // txtNovaSenha
            // 
            this.txtNovaSenha.Location = new System.Drawing.Point(111, 37);
            this.txtNovaSenha.Name = "txtNovaSenha";
            this.txtNovaSenha.PasswordChar = '*';
            this.txtNovaSenha.Size = new System.Drawing.Size(130, 20);
            this.txtNovaSenha.TabIndex = 2;
            this.txtNovaSenha.TextChanged += new System.EventHandler(this.txtNovaSenha_TextChanged);
            // 
            // txtSenhaAtual
            // 
            this.txtSenhaAtual.Location = new System.Drawing.Point(111, 6);
            this.txtSenhaAtual.Name = "txtSenhaAtual";
            this.txtSenhaAtual.PasswordChar = '*';
            this.txtSenhaAtual.Size = new System.Drawing.Size(130, 20);
            this.txtSenhaAtual.TabIndex = 1;
            this.txtSenhaAtual.TextChanged += new System.EventHandler(this.txtSenhaAtual_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Digite novamente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nova Senha:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Senha Atual:";
            // 
            // lblNovaSenha2
            // 
            this.lblNovaSenha2.AutoSize = true;
            this.lblNovaSenha2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNovaSenha2.Location = new System.Drawing.Point(247, 62);
            this.lblNovaSenha2.Name = "lblNovaSenha2";
            this.lblNovaSenha2.Size = new System.Drawing.Size(0, 13);
            this.lblNovaSenha2.TabIndex = 15;
            // 
            // lblNovaSenha
            // 
            this.lblNovaSenha.AutoSize = true;
            this.lblNovaSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNovaSenha.Location = new System.Drawing.Point(247, 40);
            this.lblNovaSenha.Name = "lblNovaSenha";
            this.lblNovaSenha.Size = new System.Drawing.Size(0, 13);
            this.lblNovaSenha.TabIndex = 14;
            // 
            // lblSenhaAtual
            // 
            this.lblSenhaAtual.AutoSize = true;
            this.lblSenhaAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenhaAtual.Location = new System.Drawing.Point(247, 9);
            this.lblSenhaAtual.Name = "lblSenhaAtual";
            this.lblSenhaAtual.Size = new System.Drawing.Size(0, 13);
            this.lblSenhaAtual.TabIndex = 13;
            // 
            // AlteraSenha
            // 
            this.AcceptButton = this.btnAlterar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 121);
            this.Controls.Add(this.lblNovaSenha2);
            this.Controls.Add(this.lblNovaSenha);
            this.Controls.Add(this.lblSenhaAtual);
            this.Controls.Add(this.btnAlterar);
            this.Controls.Add(this.txtNovaSenha2);
            this.Controls.Add(this.txtNovaSenha);
            this.Controls.Add(this.txtSenhaAtual);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AlteraSenha";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alterar Senha";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAlterar;
        private System.Windows.Forms.TextBox txtNovaSenha2;
        private System.Windows.Forms.TextBox txtNovaSenha;
        private System.Windows.Forms.TextBox txtSenhaAtual;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNovaSenha2;
        private System.Windows.Forms.Label lblNovaSenha;
        private System.Windows.Forms.Label lblSenhaAtual;
    }
}