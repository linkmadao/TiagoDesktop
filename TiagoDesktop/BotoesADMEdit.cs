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
    public partial class BotoesADMEdit : Form
    {
        private bool atualizaInformacoes = false;

        xml xmlcontroller = new xml();

        public BotoesADMEdit()
        {
            InitializeComponent();           
        }

        private void BotoesADMEdit_Load(object sender, EventArgs e)
        {
            //Pegando informações
            string[] informacaoADM1 = xmlcontroller.InformacoesProgramasADM("ADM1");
            string[] informacaoADM2 = xmlcontroller.InformacoesProgramasADM("ADM2");
            string[] informacaoADM3 = xmlcontroller.InformacoesProgramasADM("ADM3");
            string[] informacaoADM4 = xmlcontroller.InformacoesProgramasADM("ADM4");

            //Aplicando informações
            if (informacaoADM1[0] != "False")
            {
                txtNomeADM1.Text = informacaoADM1[0];
                txtComandoADM1.Text = informacaoADM1[1];
                txtParametrosADM1.Text = informacaoADM1[2];
            }
            if (informacaoADM2[0] != "False")
            {
                txtNomeADM2.Text = informacaoADM2[0];
                txtComandoADM2.Text = informacaoADM2[1];
                txtParametrosADM2.Text = informacaoADM2[2];
            }
            if (informacaoADM3[0] != "False")
            {
                txtNomeADM3.Text = informacaoADM3[0];
                txtComandoADM3.Text = informacaoADM3[1];
                txtParametrosADM3.Text = informacaoADM3[2];
            }
            if (informacaoADM4[0] != "False")
            {
                txtNomeADM4.Text = informacaoADM4[0];
                txtComandoADM4.Text = informacaoADM4[1];
                txtParametrosADM4.Text = informacaoADM4[2];
            }
        }

        private void btnProcurarADM1_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogADM = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogADM.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogADM.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogADM.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if(openFileDialogADM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoADM1.Text = openFileDialogADM.InitialDirectory + openFileDialogADM.FileName;
            }
        }

        private void btnProcurarADM2_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogADM = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogADM.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogADM.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogADM.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogADM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoADM2.Text = openFileDialogADM.InitialDirectory + openFileDialogADM.FileName;
            }
        }

        private void btnProcurarADM3_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogADM = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogADM.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogADM.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogADM.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogADM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoADM3.Text = openFileDialogADM.InitialDirectory + openFileDialogADM.FileName;
            }
        }

        private void btnProcurarADM4_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogADM = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            openFileDialogADM.Filter = "Aplicativos (.exe)|*.exe|Batch (.bat)|*.bat|Pacote do Windows (.msi)|*.msi";
            //Diz que o padrão será .exe
            openFileDialogADM.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogADM.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogADM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComandoADM4.Text = openFileDialogADM.InitialDirectory + openFileDialogADM.FileName;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvarConfADM1_Click(object sender, EventArgs e)
        {
            if(txtNomeADM1.Text != "")
            {
                if(txtComandoADM1.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasADM("ADM1", txtNomeADM1.Text, txtComandoADM1.Text, txtParametrosADM1.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoADM1.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeADM1.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnSalvarConfADM2_Click(object sender, EventArgs e)
        {
            if (txtNomeADM2.Text != "")
            {
                if (txtComandoADM2.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasADM("ADM2", txtNomeADM2.Text, txtComandoADM2.Text, txtParametrosADM2.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoADM2.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeADM2.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnSalvaConfADM3_Click(object sender, EventArgs e)
        {
            if (txtNomeADM3.Text != "")
            {
                if (txtComandoADM3.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasADM("ADM3", txtNomeADM3.Text, txtComandoADM3.Text, txtParametrosADM3.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoADM3.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeADM3.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnSalvaConfADM4_Click(object sender, EventArgs e)
        {
            if (txtNomeADM4.Text != "")
            {
                if (txtComandoADM4.Text != "")
                {
                    xmlcontroller.AlteraInformacoesProgramasADM("ADM4", txtNomeADM4.Text, txtComandoADM4.Text, txtParametrosADM4.Text);
                    atualizaInformacoes = true;
                }
                else
                {
                    txtComandoADM4.Focus();
                    MessageBox.Show("O campo 'Comando' não foi preenchido. Favor preencher antes de continuar.");
                }
            }
            else
            {
                txtNomeADM4.Focus();
                MessageBox.Show("O campo 'Nome' não foi preenchido. Favor preencher antes de continuar.");
            }
        }

        private void btnRemConfADM1_Click(object sender, EventArgs e)
        {
            if(txtNomeADM1.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão "+txtNomeADM1.Text +"?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasADM("ADM1");
                    atualizaInformacoes = true;
                }

                txtNomeADM1.Clear();
                txtComandoADM1.Clear();
                txtParametrosADM1.Clear();
            }
        }

        private void btnRemConfADM2_Click(object sender, EventArgs e)
        {
            if (txtNomeADM2.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão " + txtNomeADM2.Text + "?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasADM("ADM2");
                    atualizaInformacoes = true;
                }

                txtNomeADM2.Clear();
                txtComandoADM2.Clear();
                txtParametrosADM2.Clear();
            }
        }

        private void btnRemConfADM3_Click(object sender, EventArgs e)
        {
            if(txtNomeADM3.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão " + txtNomeADM3.Text + "?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasADM("ADM3");
                    atualizaInformacoes = true;
                }

                txtNomeADM3.Clear();
                txtComandoADM3.Clear();
                txtParametrosADM3.Clear();
            }
        }

        private void btnRemConfADM4_Click(object sender, EventArgs e)
        {
            if (txtNomeADM4.Text != "")
            {
                if (MessageBox.Show("Deseja apagar as configurações do botão " + txtNomeADM4.Text + "?", "Apagar Configuração", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    xmlcontroller.RemoveInformacoesProgramasADM("ADM4");
                    atualizaInformacoes = true;
                }

                txtNomeADM4.Clear();
                txtComandoADM4.Clear();
                txtParametrosADM4.Clear();
            }
        }

        private void BotoesADMEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (atualizaInformacoes)
            {
                Configuracao.atualizaInformacoes = true;
                atualizaInformacoes = false;
            }
        }
    }
}
