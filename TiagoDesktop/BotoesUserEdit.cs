using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TiagoDesktop
{
    public partial class BotoesUserEdit : Form
    {
        private bool atualizaInformacoes = false;

        xml xmlcontroller = new xml();

        public BotoesUserEdit()
        {
            InitializeComponent();
        }

        private void BotoesUserEdit_Load(object sender, EventArgs e)
        {
            //Pegando informações
            string[] informacaoUser1 = xmlcontroller.InformacoesProgramasUser("User1");
            string[] informacaoUser2 = xmlcontroller.InformacoesProgramasUser("User2");
            string[] informacaoUser3 = xmlcontroller.InformacoesProgramasUser("User3");
            string[] informacaoUser4 = xmlcontroller.InformacoesProgramasUser("User4");

            //Aplicando informações
            if (informacaoUser1[0] != "False")
            {
                txtNomeUser1.Text = informacaoUser1[0];
                txtComandoUser1.Text = informacaoUser1[1];
                txtParametrosUser1.Text = informacaoUser1[2];
            }
            if (informacaoUser2[0] != "False")
            {
                txtNomeUser2.Text = informacaoUser2[0];
                txtComandoUser2.Text = informacaoUser2[1];
                txtParametrosUser2.Text = informacaoUser2[2];
            }
            if (informacaoUser3[0] != "False")
            {
                txtNomeUser3.Text = informacaoUser3[0];
                txtComandoUser3.Text = informacaoUser3[1];
                txtParametrosUser3.Text = informacaoUser3[2];
            }
            if (informacaoUser4[0] != "False")
            {
                txtNomeUser4.Text = informacaoUser4[0];
                txtComandoUser4.Text = informacaoUser4[1];
                txtParametrosUser4.Text = informacaoUser4[2];
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvarConfUser1_Click(object sender, EventArgs e)
        {
            if (txtNomeUser1.Text != "")
            {
                if (txtComandoUser1.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasUser("User1", txtNomeUser1.Text, txtComandoUser1.Text, txtParametrosUser1.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoUser1.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeUser1.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnSalvarConfUser2_Click(object sender, EventArgs e)
        {
            if (txtNomeUser2.Text != "")
            {
                if (txtComandoUser2.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasUser("User2", txtNomeUser2.Text, txtComandoUser2.Text, txtParametrosUser2.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoUser2.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeUser2.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnSalvarConfUser3_Click(object sender, EventArgs e)
        {
            if (txtNomeUser3.Text != "")
            {
                if (txtComandoUser3.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasUser("User3", txtNomeUser3.Text, txtComandoUser3.Text, txtParametrosUser3.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoUser3.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeUser3.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnSalvarConfUser4_Click(object sender, EventArgs e)
        {
            if (txtNomeUser4.Text != "")
            {
                if (txtComandoUser4.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasUser("User4", txtNomeUser4.Text, txtComandoUser4.Text, txtParametrosUser4.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoUser4.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeUser4.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnProcurarUser1_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogUser = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogUser.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogUser.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogUser.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoUser1.Text = openFileDialogUser.InitialDirectory + openFileDialogUser.FileName;
            }
        }

        private void btnProcurarUser2_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogUser = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogUser.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogUser.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogUser.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoUser2.Text = openFileDialogUser.InitialDirectory + openFileDialogUser.FileName;
            }
        }

        private void btnProcurarUser3_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogUser = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogUser.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogUser.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogUser.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoUser3.Text = openFileDialogUser.InitialDirectory + openFileDialogUser.FileName;
            }
        }

        private void btnProcurarUser4_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogUser = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogUser.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogUser.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogUser.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoUser4.Text = openFileDialogUser.InitialDirectory + openFileDialogUser.FileName;
            }
        }

        private void btnRemConfUser1_Click(object sender, EventArgs e)
        {
            if (txtNomeUser1.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão " + txtNomeUser1.Text + "?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasUser("User1");
                    atualizaInformacoes = true;
                }

                txtNomeUser1.Clear();
                txtComandoUser1.Clear();
                txtParametrosUser1.Clear();
            }
        }

        private void btnRemConfUser2_Click(object sender, EventArgs e)
        {
            if (txtNomeUser2.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão " + txtNomeUser2.Text + "?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasUser("User2");
                    atualizaInformacoes = true;
                }

                txtNomeUser2.Clear();
                txtComandoUser2.Clear();
                txtParametrosUser2.Clear();
            }
        }

        private void btnRemConfUser3_Click(object sender, EventArgs e)
        {
            if (txtNomeUser3.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão " + txtNomeUser3.Text + "?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasUser("User3");
                    atualizaInformacoes = true;
                }

                txtNomeUser3.Clear();
                txtComandoUser3.Clear();
                txtParametrosUser3.Clear();
            }
        }

        private void btnRemConfUser4_Click(object sender, EventArgs e)
        {
            if (txtNomeUser4.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão " + txtNomeUser4.Text + "?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasUser("User4");
                    atualizaInformacoes = true;
                }

                txtNomeUser4.Clear();
                txtComandoUser4.Clear();
                txtParametrosUser4.Clear();
            }
        }

        private void BotoesUserEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (atualizaInformacoes)
            {
                Configuracao.atualizaInformacoes = true;
                atualizaInformacoes = false;
            }
        }
    }
}
