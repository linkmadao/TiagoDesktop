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
    public partial class AlteraSenha : Form
    {
        public AlteraSenha()
        {
            InitializeComponent();
        }

        private void txtSenhaAtual_TextChanged(object sender, EventArgs e)
        {
            if (txtSenhaAtual.Text == "")
            {
                lblSenhaAtual.Text = "X";
                lblSenhaAtual.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (txtSenhaAtual.Text.Length < 4)
            {
                lblSenhaAtual.Text = "X";
                lblSenhaAtual.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblSenhaAtual.Text = "OK";
                lblSenhaAtual.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        private void txtNovaSenha_TextChanged(object sender, EventArgs e)
        {
            if (txtNovaSenha.Text == "")
            {
                lblNovaSenha.Text = "X";
                lblNovaSenha.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (txtNovaSenha.Text.Length < 4)
            {
                lblNovaSenha.Text = "X";
                lblNovaSenha.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblNovaSenha.Text = "OK";
                lblNovaSenha.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        private void txtNovaSenha2_TextChanged(object sender, EventArgs e)
        {
            if (txtNovaSenha2.Text == "")
            {
                lblNovaSenha2.Text = "X";
                lblNovaSenha2.ForeColor = System.Drawing.Color.DarkRed;
            }
            else if (txtNovaSenha2.Text.Length < 4)
            {
                lblNovaSenha2.Text = "X";
                lblNovaSenha2.ForeColor = System.Drawing.Color.DarkRed;
            }
            else
            {
                lblNovaSenha2.Text = "OK";
                lblNovaSenha2.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }
        

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (txtSenhaAtual.Text == "")
            {
                MessageBox.Show("Digite uma senha!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblSenhaAtual.Text = "X";
                lblSenhaAtual.ForeColor = System.Drawing.Color.DarkRed;
                txtSenhaAtual.Focus();
            }
            else if (txtSenhaAtual.Text.Length < 4)
            {
                MessageBox.Show("A senha precisa ter mais de 3 caracteres!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lblSenhaAtual.Text = "X";
                lblSenhaAtual.ForeColor = System.Drawing.Color.DarkRed;
                txtSenhaAtual.Clear();
                txtSenhaAtual.Focus();
            }
            else
            {
                if (txtNovaSenha.Text == "")
                {
                    MessageBox.Show("Digite uma senha!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblNovaSenha.Text = "X";
                    lblNovaSenha.ForeColor = System.Drawing.Color.DarkRed;
                    txtNovaSenha.Focus();
                }
                else if (txtNovaSenha.Text.Length < 4)
                {
                    MessageBox.Show("A senha precisa ter mais de 3 caracteres!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    lblNovaSenha.Text = "X";
                    lblNovaSenha.ForeColor = System.Drawing.Color.DarkRed;
                    txtNovaSenha.Clear();
                    txtNovaSenha.Focus();
                }
                else
                {
                    if (txtNovaSenha2.Text == "")
                    {
                        MessageBox.Show("Digite uma senha!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lblNovaSenha2.Text = "X";
                        lblNovaSenha2.ForeColor = System.Drawing.Color.DarkRed;
                        txtNovaSenha2.Focus();
                    }
                    else if (txtNovaSenha2.Text.Length < 4)
                    {
                        MessageBox.Show("A senha precisa ter mais de 3 caracteres!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        lblNovaSenha2.Text = "X";
                        lblNovaSenha2.ForeColor = System.Drawing.Color.DarkRed;
                        txtNovaSenha2.Clear();
                        txtNovaSenha2.Focus();
                    }
                    else
                    {
                        if (txtNovaSenha.Text.ToUpper() != txtNovaSenha2.Text.ToUpper())
                        {
                            MessageBox.Show("As senhas não são idênticas. \nDigite novamente a senha!", "As senhas não conferem", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            //Reseta campo 1
                            lblNovaSenha.Text = "X";
                            lblNovaSenha.ForeColor = System.Drawing.Color.DarkRed;
                            txtNovaSenha.Clear();
                            txtNovaSenha.Focus();

                            //Reseta campo 2
                            lblNovaSenha2.Text = "X";
                            lblNovaSenha2.ForeColor = System.Drawing.Color.DarkRed;
                            txtNovaSenha2.Clear();
                        }
                        else
                        {
                            xml xmlcontroller = new xml();

                            if (!xmlcontroller.VerificaUsuario("ADMINISTRADOR", txtSenhaAtual.Text))
                            {
                                MessageBox.Show("A senha do usuário atual está incorreta. \nDigite novamente a senha!", "Senha inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                txtSenhaAtual.Clear();
                                txtSenhaAtual.Focus();
                            }
                            else
                            {
                                xmlcontroller.AlteraSenha(txtNovaSenha.Text);
                                txtSenhaAtual.Clear();
                                txtSenhaAtual.Focus();
                                txtNovaSenha.Clear();
                                txtNovaSenha2.Clear();

                                MessageBox.Show("A senha do usuário atual foi alterada com sucesso.", "Senha alterada com sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
        }
    }
}
