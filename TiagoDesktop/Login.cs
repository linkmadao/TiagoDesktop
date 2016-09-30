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
    public partial class Login : Form
    {
        private xml xmlcontroller;

        public Login()
        {
            InitializeComponent();
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                lblUser.Text = "X";
                lblUser.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblUser.Text = "OK";
                lblUser.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            if (txtSenha.Text == "")
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text == "")
            {
                MessageBox.Show("Digite uma usuário!", "Usuário inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblUser.Text = "X";
                lblUser.ForeColor = System.Drawing.Color.DarkRed;
                txtUser.Focus();
            }
            else if (txtSenha.Text == "")
            {
                MessageBox.Show("Digite uma senha!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblSenha.Text = "X";
                lblSenha.ForeColor = System.Drawing.Color.DarkRed;
                txtSenha.Focus();
            }
            else
            {
                if(xmlcontroller.VerificaUsuario(txtUser.Text, txtSenha.Text))
                {
                    TiagoDesktop.loginAtivo = true;
                    TiagoDesktop.erroLogin = false;
                }
                else
                {
                    TiagoDesktop.loginAtivo = false;
                    TiagoDesktop.erroLogin = true;
                }

                //Fecha janela
                Close();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
             xmlcontroller = new xml();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            TiagoDesktop.erroLogin = false;

            Close();
        }
    }
}
