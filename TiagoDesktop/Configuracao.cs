using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net;

namespace TiagoDesktop
{
    public partial class Configuracao : Form
    {
        xml xmlController = new xml();

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hwndChild, IntPtr hwndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindowA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SetForegroundWindow(IntPtr hWnd);
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);


        public Configuracao()
        {
            InitializeComponent();
        }

        private void Configuracao_Load(object sender, EventArgs e)
        {
            //Se for true
            if (xmlController.VerificaInicializacao())
            {
                cmbInicializacao.SelectedText = "Tiago Desktop";
            }
            else
            {
                cmbInicializacao.SelectedText = "Windows";
            }

            //Se for true
            if (xmlController.VerificaStatusMenu())
            {
                cmbEscondeMenu.SelectedText = "Sim";
            }
            else
            {
                cmbInicializacao.SelectedText = "Não";
            }

            //Verifica se o Teamviewer Existe
            if (File.Exists(@"C:\Program Files\TeamViewer\TeamViewer.exe") || File.Exists(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe"))
            {
                btnTeamviewer.Text = "Abrir TeamViewer";
            }

            //Verifica se o Sharpkeys Existe
            if (File.Exists(@"C:\Program Files\RandyRants.com\SharpKeys\SharpKeys.exe") || File.Exists(@"C:\Program Files (x86)\RandyRants.com\SharpKeys\SharpKeys.exe"))
            {
                btnSharpKeys.Text = "Abrir SharpKeys";
            }

            //Verifica se o Ammyy Admin Existe
            if (File.Exists(@"C:\TiagoSM\AA_v3.exe"))
            {
                btnAmmyy.Text = "Abrir Ammyy Admin";
            }
        }

        private void btnAltIni_Click(object sender, EventArgs e)
        {
            if (cmbInicializacao.Text != "Windows")
            {
                xmlController.AlteraInicializacao(true);
            }
            else if (cmbInicializacao.Text == "Windows")
            {
                xmlController.AlteraInicializacao(false);
            }
            else
            {
                MessageBox.Show("Você não selecionou nenhuma opção para ser efetuado a troca!", "Opção inválida");
            }
        }

        private void btnTrocaSenha_Click(object sender, EventArgs e)
        {
            AlteraSenha alterarSenha = new AlteraSenha();
            alterarSenha.ShowDialog();
        }

        private void btnPainelControle_Click(object sender, EventArgs e)
        {
            if (!ProgramIsRunning("control"))
            {
                Process.Start("control");
            }
        }

        private void btnConfigMenuAdm_Click(object sender, EventArgs e)
        {
            BotoesADMEdit botoesMenuADM = new BotoesADMEdit();
            botoesMenuADM.ShowDialog();
        }

        private void btnConfigMenuUser_Click(object sender, EventArgs e)
        {
            BotoesUserEdit botoesMenuUser = new BotoesUserEdit();
            botoesMenuUser.ShowDialog();
        }

        private bool ProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(@FullPath);
            string FileName = Path.GetFileNameWithoutExtension(@FullPath).ToLower();
            bool isRunning = false;

            Process[] pList = Process.GetProcessesByName(FileName);
            foreach (Process p in pList)
            {
                try
                {
                    if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                        isRunning = true;
                }
                catch (Win32Exception)
                {

                }

            }

            if (isRunning)
            {
                if (Process.GetProcessesByName(FileName).Any())
                {
                    ShowWindow(Process.GetProcessesByName(FileName).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(FileName).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(FileName).First().MainWindowHandle);
                }
            }


            return isRunning;
        }

        private void btnTeamviewer_Click(object sender, EventArgs e)
        {
            //Verifica se o XML Existe
            if (File.Exists(@"C:\Program Files\TeamViewer\TeamViewer.exe"))
            {
                if (!ProgramIsRunning(@"C:\Program Files\TeamViewer\TeamViewer.exe"))
                {
                    try
                    {
                        Process.Start(@"C:\Program Files\TeamViewer\TeamViewer.exe");
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nParece que o programa esta corrompido!", "Erro ao abrir o TeamViewer!");
                    }
                }
            }
            else if (File.Exists(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe"))
            {
                if (!ProgramIsRunning(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe"))
                {
                    try
                    {
                        Process.Start(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe");
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nParece que o programa esta corrompido!", "Erro ao abrir o TeamViewer!");
                    }
                }
            }
            else
            {
                if (File.Exists(@"C:\TiagoSM\TeamViewer_Setup_pt-ckq.exe"))
                {
                    if (!ProgramIsRunning(@"C:\TiagoSM\TeamViewer_Setup_pt-ckq.exe"))
                    {
                        try
                        {
                            Process.Start(@"C:\TiagoSM\TeamViewer_Setup_pt-ckq.exe");
                        }
                        catch
                        {
                            MessageBox.Show("Erro ao abrir o programa. \nParece que o arquivo esta corrompido!", "Erro ao abrir o instalador do TeamViewer 7!");
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Deseja procurar o instalador no computador?", "Procurar instalador do TeamViewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Cria uma instância de Open File Dialog
                        OpenFileDialog openFileDialogUser = new OpenFileDialog();

                        //Seta o filtro de tipos de arquivos
                        openFileDialogUser.Filter = "Aplicativos (.exe)|*.exe";
                        //Diz que o padrão será .exe
                        openFileDialogUser.FilterIndex = 1;
                        //Seta a opção de ter multiplas escolhas
                        openFileDialogUser.Multiselect = true;

                        //Chama o método ShowDialog para mostrar a janela de dialogo
                        if (openFileDialogUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            try
                            {
                                Process.Start(@"" + openFileDialogUser.InitialDirectory + openFileDialogUser.FileName);
                            }
                            catch
                            {
                                MessageBox.Show("Erro ao abrir o programa. \nParece que o arquivo esta corrompido!", "Erro ao abrir o instalador do TeamViewer!");
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Deseja baixar o instalador mais atual?", "Baixar o instalador do TeamViewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            WebClient webClient = new WebClient();
                            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(TeamViewerCompleted);
                            webClient.DownloadFileAsync(new Uri("https://download.teamviewer.com/download/TeamViewer_Setup_pt.exe"), @"c:\TiagoSM\TeamViewer_Setup_pt.exe");
                        }
                        else
                        {
                            MessageBox.Show("Operação cancelada!");
                        }
                    }

                }

            }
        }

        private void TeamViewerCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Download Finalizado!\nInicializando o instalador.", "Download Finalizado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"c:\TiagoSM\TeamViewer_Setup_pt.exe");
            }
            catch
            {
                MessageBox.Show("Erro ao abrir o instalador. \nParece que o arquivo esta corrompido!", "Erro ao abrir o instalador do TeamViewer!");
            }
        }

        private void btnInformacoesSistema_Click(object sender, EventArgs e)
        {
            if (!ProgramIsRunning("msinfo32"))
            {
                Process.Start("msinfo32");
            }
        }

        private void btnGerenciadorDispositivos_Click(object sender, EventArgs e)
        {
            Process.Start("devmgmt.msc");
        }

        private void btnSharpKeys_Click(object sender, EventArgs e)
        {
            //Verifica se o Sharpkeys Existe
            if (File.Exists(@"C:\Program Files\RandyRants.com\SharpKeys\SharpKeys.exe"))
            {
                if (!ProgramIsRunning(@"C:\Program Files\RandyRants.com\SharpKeys\SharpKeys.exe"))
                {
                    try
                    {
                        Process.Start(@"C:\Program Files\RandyRants.com\SharpKeys\SharpKeys.exe");
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nParece que o programa esta corrompido!", "Erro ao abrir o SharpKeys!");
                    }
                }
            }
            else if (File.Exists(@"C:\Program Files (x86)\RandyRants.com\SharpKeys\SharpKeys.exe"))
            {
                if (!ProgramIsRunning(@"C:\Program Files (x86)\RandyRants.com\SharpKeys\SharpKeys.exe"))
                {
                    try
                    {
                        Process.Start(@"C:\Program Files (x86)\RandyRants.com\SharpKeys\SharpKeys.exe");
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nParece que o programa esta corrompido!", "Erro ao abrir o SharpKeys!");
                    }
                }
            }
            else
            {
                if (File.Exists(@"C:\TiagoSM\sharpkeys35.msi"))
                {
                    if (!ProgramIsRunning("msiexec.exe"))
                    {
                        try
                        {
                            Process.Start(@"C:\TiagoSM\sharpkeys35.msi");
                        }
                        catch
                        {
                            MessageBox.Show("Erro ao abrir o programa. \nParece que o arquivo esta corrompido!", "Erro ao abrir o instalador do SharpKeys!");
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("Deseja procurar o instalador no computador?", "Procurar instalador do TeamViewer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //Cria uma instância de Open File Dialog
                        OpenFileDialog openFileDialogUser = new OpenFileDialog();

                        //Seta o filtro de tipos de arquivos
                        openFileDialogUser.Filter = "Pacote do Windows Installer (.msi)|*.msi";
                        //Diz que o padrão será .exe
                        openFileDialogUser.FilterIndex = 1;
                        //Seta a opção de ter multiplas escolhas
                        openFileDialogUser.Multiselect = true;

                        //Chama o método ShowDialog para mostrar a janela de dialogo
                        if (openFileDialogUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            try
                            {
                                Process.Start(@"" + openFileDialogUser.InitialDirectory + openFileDialogUser.FileName);
                            }
                            catch
                            {
                                MessageBox.Show("Erro ao abrir o programa. \nParece que o arquivo esta corrompido!", "Erro ao abrir o instalador do SharpKeys!");
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Deseja baixar o instalador mais atual?", "Baixar o instalador do SharpKeys", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            WebClient webClient = new WebClient();
                            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(SharpKeysComplete);
                            webClient.DownloadFileAsync(new Uri("http://www.randyrants.com/sharpkeys35.msi"), @"c:\TiagoSM\sharpkeys35.msi");
                        }
                        else
                        {
                            MessageBox.Show("Operação cancelada!");
                        }
                    }

                }

            }
        }

        private void SharpKeysComplete(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Download Finalizado!\nInicializando o instalador.", "Download Finalizado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"c:\TiagoSM\sharpkeys35.msi");
            }
            catch
            {
                MessageBox.Show("Erro ao abrir o instalador. \nParece que o arquivo esta corrompido!", "Erro ao abrir o instalador do SharpKeys!");
            }
        }

        private void btnAmmyy_Click(object sender, EventArgs e)
        {
            //Verifica se o Sharpkeys Existe
            if (File.Exists(@"C:\TiagoSM\AA_v3.exe"))
            {
                if (!ProgramIsRunning(@"C:\TiagoSM\AA_v3.exe"))
                {
                    try
                    {
                        Process.Start(@"C:\TiagoSM\AA_v3.exe");
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nParece que o programa esta corrompido!", "Erro ao abrir o Ammyy Admin!");
                    }
                }
            }
            else
            {
                if (MessageBox.Show("Deseja baixar o programa mais atual?", "Baixar o Ammyy Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(AmmyyAdminComplete);
                    webClient.DownloadFileAsync(new Uri("http://www.ammyy.com/AA_v3.exe?em=tiagos.miguel%40outlook.com"), @"c:\TiagoSM\AA_v3.exe");
                }
                else
                {
                    MessageBox.Show("Operação cancelada!");
                }

            }
        }

        private void AmmyyAdminComplete(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                MessageBox.Show("Download Finalizado!\nInicializando o instalador.", "Download Finalizado!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"c:\TiagoSM\AA_v3.exe");
            }
            catch
            {
                MessageBox.Show("Erro ao abrir o instalador. \nParece que o arquivo esta corrompido!", "Erro ao abrir o Ammyy Admin!");
            }
        }

        private void btnGpedit_Click(object sender, EventArgs e)
        {
            if (!ProgramIsRunning("gpedit.msc"))
            {
                Process.Start("gpedit.msc");
            }
        }

        private void btnPlanoDeFundo_Click(object sender, EventArgs e)
        {
            //Cria uma instância de Open File Dialog
            OpenFileDialog openFileDialogUser = new OpenFileDialog();

            //Seta o filtro de tipos de arquivos
            //openFileDialogUser.Filter = "Todos os arquivos |*.*";

            //Diz que o padrão será Arquivos de Imagens
            openFileDialogUser.FilterIndex = 1;
            //Seta a opção de ter multiplas escolhas
            openFileDialogUser.Multiselect = true;

            //Chama o método ShowDialog para mostrar a janela de dialogo
            if (openFileDialogUser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    xmlController.AlteraPapelParede(openFileDialogUser.InitialDirectory + openFileDialogUser.FileName);
                }
                catch
                {
                    MessageBox.Show("Erro ao alterar o papel de parede.");
                }
            }
        }

        private void btnSalvaEscondeMenu_Click(object sender, EventArgs e)
        {
            if (cmbEscondeMenu.Text == "Sim")
            {
                xmlController.AlteraStatusMenu(true);
            }
            else if (cmbInicializacao.Text == "Não")
            {
                xmlController.AlteraStatusMenu(false);
            }
            else
            {
                MessageBox.Show("Você não selecionou nenhuma opção para ser efetuado a troca!", "Opção inválida");
            }
        }
    }
}
