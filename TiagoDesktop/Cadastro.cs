using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiagoDesktop
{
    public partial class Cadastro : Form
    {
        public Cadastro()
        {
            InitializeComponent();
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            if(txtSenha.Text == "")
            {
                lblSenha.Text = "X";
                lblSenha.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (txtSenha.Text.Length < 4)
            {
                lblSenha.Text = "X";
                lblSenha.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblSenha.Text = "OK";
                lblSenha.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        private void btnCadastro_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == "")
            {
                MessageBox.Show("Digite uma senha!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblSenha.Text = "X";
                lblSenha.ForeColor = System.Drawing.Color.DarkRed;
                txtSenha.Focus();
            }
            else if (txtSenha.Text.Length < 4)
            {
                MessageBox.Show("A senha precisa ter mais de 3 caracteres!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblSenha.Text = "X";
                lblSenha.ForeColor = System.Drawing.Color.DarkRed;
                txtSenha.Clear();
                txtSenha.Focus();
            }
            else
            {
                xml xmlcontroller = new xml();
                xmlcontroller.Cria(txtUser.Text, txtSenha.Text);

                TiagoDesktop.usuarioCadastrado = true;

                Close();
            }
        }

        private void Cadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!TiagoDesktop.usuarioCadastrado)
            {
                Application.Exit();
            }
        }
    }
}
