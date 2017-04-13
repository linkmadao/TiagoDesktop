using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Security.Cryptography;

namespace TiagoDesktop
{
    public partial class TiagoDesktop : Form
    {
        #region variáveis
        #region Boolean Statico
        public static bool
            loginAtivo = false,
            erroLogin = false,
            usuarioCadastrado = false,
            atualizaInformacoes = false,
            telaAjudaAberta = false;
            
        #endregion
        private bool iniciarAberto = false, menuEscondido = false;
        private int mousePositionY = 0;

        //Chama a classe de controle do XML
        xml xmlController = new xml();
        //Processos
        private Process[] runningProcesses, sameAsthisSession;

        #region DLLS
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
        #endregion
        #endregion

        public TiagoDesktop()
        {
            InitializeComponent();

            //Pega a lista de programas que estão rodando no pc atualmente
            runningProcesses = Process.GetProcesses();
            //Pega o ID da barra TiagoDesktop
            int currentSessionID = Process.GetCurrentProcess().SessionId;
            //Gera uma nova lista baseada nos programas que foram executados após a execução da barra TiagoDesktop
            sameAsthisSession = (from c in runningProcesses where c.SessionId == currentSessionID select c).ToArray();
        }

        private void TiagoDesktop_Load(object sender, EventArgs e)
        {
            #region MDI
            //Cria um MdiClient
            MdiClient ctlMDI;
            foreach (Control ctl in this.Controls)
            {
                try
                {
                    ctlMDI = (MdiClient)ctl;

                    //Modifica o fundo do MDI para branco
                    ctlMDI.BackColor = Color.White;
                    //E aplica a imagem de background
                    ctlMDI.BackgroundImage = this.BackgroundImage;
                }
                catch (InvalidCastException)
                { }
            }
            #endregion

            #region Verifica se o arquivo login.xml existe, caso não exista abre a tela de cadastro
            //Cita o caminho do arquivo de login
            string curFile = @"c:\TiagoSM\login.xml";
            //Caso o arquivo não exista
            if (!File.Exists(curFile))
            {
                //Esconde a barra do menu inicial
                menuInicial.Visible = false;
                //Abre a tela de cadastro de usuário
                Cadastro formCadastro = new Cadastro();
                formCadastro.ShowDialog();

                //Se o usuário não for cadastrado e a tela for fechada
                if (!usuarioCadastrado)
                {
                    //Fecha a aplicação
                    Application.Exit();
                }
                //Caso o contrário
                else
                {
                    //Mostra o menu inicial
                    menuInicial.Visible = true;
                }
            }
            #endregion
            #region Caso exista o arquivo
            else
            {
                try
                {
                    #region Fechar Exlorer
                    //Se a barra iniciar com o windows, ele fecha o Explorer
                    if (xmlController.VerificaInicializacao())
                    {
                        //("Função", "/Forçar /Imagem Processo")
                        Process.Start("taskkill", "/F /IM explorer.exe");
                    }
                    #endregion

                    #region Inicia o Software
                    //Verifica se o arquivo programaAdm1.xml existe
                    if (File.Exists(@"c:\TiagoSM\programaAdm1.xml"))
                    {
                        #region Papel de Parede
                        //Verifica se há algum papel de parede diferente do original
                        if (xmlController.InformacaoPapelParede() != "" && xmlController.InformacaoPapelParede() != null && xmlController.InformacaoPapelParede() != "False")
                        {
                            //Altera a imagem de fundo
                            this.BackgroundImage = Image.FromFile(xmlController.InformacaoPapelParede());
                            //Faz com que ela se ajuste ao tamanho da tela
                            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                        }
                        #endregion

                        #region Esconde Menu Inicial
                        //Se o menu inicial estiver como "escondido"
                        if (xmlController.VerificaStatusMenu())
                        {
                            //Esconde o menu inicial
                            menuInicial.Visible = false;
                            //e ativa esse boolean para checagem
                            menuEscondido = true;
                        }
                        else
                        {
                            //Mostra o menu inicial
                            menuInicial.Visible = true;
                            //desativa esse boolean
                            menuEscondido = false;
                        }
                        #endregion

                        #region Configura textos e imagem dos Botões ADM e User
                        //Se o xmlController.InformacoesProgramasUser("BLOCO")[nome] != "NADA", ele preenche com o texto, caso contrário deixa o nome original.
                        btnUser1.Text = xmlController.InformacoesProgramasUser("User1")[0] != "" ? xmlController.InformacoesProgramasUser("User1")[0] : "Botão User 1";
                        if (xmlController.InformacoesProgramasUser("User1")[1] != null)
                        {
                            btnUser1.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User1")[1]).ToBitmap();
                            btnUser1Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User1")[1]).ToBitmap();
                        }

                        btnUser2.Text = xmlController.InformacoesProgramasUser("User2")[0] != "" ? xmlController.InformacoesProgramasUser("User2")[0] : "Botão User 2";
                        if (xmlController.InformacoesProgramasUser("User2")[1] != null)
                        {
                            btnUser2.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User2")[1]).ToBitmap();
                            btnUser2Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User2")[1]).ToBitmap();
                        }
                        btnUser3.Text = xmlController.InformacoesProgramasUser("User3")[0] != "" ? xmlController.InformacoesProgramasUser("User3")[0] : "Botão User 3";
                        if (xmlController.InformacoesProgramasUser("User3")[1] != null)
                        {
                            btnUser3.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User3")[1]).ToBitmap();
                            btnUser3Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User3")[1]).ToBitmap();
                        }
                        BtnUser4.Text = xmlController.InformacoesProgramasUser("User4")[0] != "" ? xmlController.InformacoesProgramasUser("User4")[0] : "Botão User 4";
                        if (xmlController.InformacoesProgramasUser("User4")[1] != null)
                        {
                            BtnUser4.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User4")[1]).ToBitmap();
                            btnUser4Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User4")[1]).ToBitmap();
                        }

                        //Se o xmlController.InformacoesProgramasADM("BLOCO")[nome] != "NADA", ele preenche com o texto, caso contrário deixa o nome original.
                        btnADM1.Text = xmlController.InformacoesProgramasADM("ADM1")[0] != "" ? xmlController.InformacoesProgramasADM("ADM1")[0] : "Botão ADM 1";
                        if (xmlController.InformacoesProgramasADM("ADM1")[1] != null)
                        {
                            btnADM1.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM1")[1]).ToBitmap();
                            btnADM1Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM1")[1]).ToBitmap();
                        }
                        btnADM2.Text = xmlController.InformacoesProgramasADM("ADM2")[0] != "" ? xmlController.InformacoesProgramasADM("ADM2")[0] : "Botão ADM 2";
                        if (xmlController.InformacoesProgramasADM("ADM2")[1] != null)
                        {
                            btnADM2.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM2")[1]).ToBitmap();
                            btnADM2Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM2")[1]).ToBitmap();
                        }
                        btnADM3.Text = xmlController.InformacoesProgramasADM("ADM3")[0] != "" ? xmlController.InformacoesProgramasADM("ADM3")[0] : "Botão ADM 3";
                        if (xmlController.InformacoesProgramasADM("ADM3")[1] != null)
                        {
                            btnADM3.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM3")[1]).ToBitmap();
                            btnADM3Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM3")[1]).ToBitmap();
                        }
                        btnADM4.Text = xmlController.InformacoesProgramasADM("ADM4")[0] != "" ? xmlController.InformacoesProgramasADM("ADM4")[0] : "Botão ADM 4";
                        if (xmlController.InformacoesProgramasADM("ADM4")[1] != null)
                        {
                            btnADM4.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM4")[1]).ToBitmap();
                            btnADM4Aberto.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM4")[1]).ToBitmap();
                        }
                        #endregion

                        #region Configura Visualização Botões ADM e User
                        //Se o xmlController.InformacoesProgramasUser("BLOCO")[status] == "nao", ele deixa falso, caso contrário deixa true.
                        btnUser1.Visible = xmlController.InformacoesProgramasUser("User1")[3] == "no" ? true : false;
                        btnUser2.Visible = xmlController.InformacoesProgramasUser("User2")[3] == "no" ? true : false;
                        btnUser3.Visible = xmlController.InformacoesProgramasUser("User3")[3] == "no" ? true : false;
                        BtnUser4.Visible = xmlController.InformacoesProgramasUser("User4")[3] == "no" ? true : false;

                        //Se o xmlController.InformacoesProgramasADM("BLOCO")[status] == "nao", ele deixa falso, caso contrário deixa true.
                        btnADM1.Visible = xmlController.InformacoesProgramasADM("ADM1")[3] == "no" ? true : false;
                        btnADM2.Visible = xmlController.InformacoesProgramasADM("ADM2")[3] == "no" ? true : false;
                        btnADM3.Visible = xmlController.InformacoesProgramasADM("ADM3")[3] == "no" ? true : false;
                        btnADM4.Visible = xmlController.InformacoesProgramasADM("ADM4")[3] == "no" ? true : false;
                        #endregion
                    }
                    #endregion

                }
                catch (Exception)
                {
                    MessageBox.Show("Erro ao iniciar o TiagoDesktop. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362 - 6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnWinExplorer.Text + "!");
                }
            }
            #endregion
        }

        private void TiagoDesktop_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Desativado função de fechar pelo gerenciador de tarefas"e.CloseReason == CloseReason.TaskManagerClosing ||" 
            //Se o aplicativo for fechado pelo Shudown ou pela ação Application.Exit();
            if (e.CloseReason == CloseReason.WindowsShutDown || e.CloseReason == CloseReason.ApplicationExitCall)
            {
                Application.Exit();
            }
            else
            {
                //Aparece uma mensagem
                AutoClosingMessageBox.Show("Não é possível fechar o programa!", "Fechamento abortado!", 2000);
                //Cancela a ação
                e.Cancel = true;
                return;
            }
        }

        private void TiagoDesktop_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            //Se a tela de ajuda não estiver aberta
            if (!telaAjudaAberta)
            {
                //Diz que a tela ta aberta e impede outras de serem abertas
                telaAjudaAberta = true;
                
                //Chama a janela
                Ajuda ajuda = new Ajuda();
                ajuda.ShowDialog();
            }

            // DESATIVADO POR HORA
            // EXECUTANDO TESTES
            /*
            //Pega o serial do PenDrive para gerar a chave
            USBSerialNumber usb = new USBSerialNumber();
            string chave = usb.getSerialNumberFromDriveLetter("F");

            MessageBox.Show("Chave: " + chave + "\nChave Criptografada: " + LockKey.Criptografa(chave));
            MessageBox.Show("Chave: " + chave + "\nChave Descriptografada: " + LockKey.Descriptografa(LockKey.Criptografa(chave)));
            */
        }

        #region Checa e Reabre Programa
        //Verifica se o programa está rodando e reabre ele
        private bool ProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();
            bool isRunning = false;

            Process[] pList = Process.GetProcessesByName(FileName);
            foreach (Process p in pList)
            { 
                if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                    isRunning = true;
            }

            if(isRunning)
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
        //Apenas verifica se o programa está rodando
        private bool CheckIfProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();
            bool isRunning = false;

            Process[] pList = Process.GetProcessesByName(FileName);
            foreach (Process p in pList)
            {
                try
                {
                    try
                    {
                        if (p.MainModule.FileName.StartsWith(FilePath, StringComparison.InvariantCultureIgnoreCase))
                            isRunning = true;
                    }
                    catch (NullReferenceException)
                    {
                        throw;
                    }
                }
                catch (Win32Exception)
                {

                }
            }

            return isRunning;
        }
        //Pega o nome do processo que está rodando de acordo com o caminho do arquivo
        private string ProcessProgramIsRunning(string FullPath)
        {
            string FilePath = Path.GetDirectoryName(FullPath);
            string FileName = Path.GetFileNameWithoutExtension(FullPath).ToLower();

            return FileName;
        }
        #endregion

        #region Verifica Alt + F4
        //Evita que o programa se feche usando Alt + f4
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Caso a tecla apertada seja alt ou f4
            if (keyData == (Keys.Alt | Keys.F4))
            {
                return true;
            }
            else
            {
                return base.ProcessDialogKey(keyData);
            }
        }
        #endregion

        #region Botões ADM Barra de Tarefas
        private void Deslogar_Click(object sender, EventArgs e)
        {
            //Cria e abre o form de Login
            Login formLogin = new Login();
            formLogin.ShowDialog();

            #region Verifica login
            if (loginAtivo)
            {
                //Se o comando for Sim
                if (MessageBox.Show("Deseja deslogar do usuário?", "Efetuar Logoff", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Executa o comando Logoff
                    System.Diagnostics.Process.Start("Shutdown", "-l");
                }

                //Reseta a variável
                loginAtivo = false;
            }
            else
            {
                //Se o usuário tiver errado o login
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
            #endregion
        }

        private void Desligar_Click(object sender, EventArgs e)
        {
            //Cria e abre o form de Login
            Login formLogin = new Login();
            formLogin.ShowDialog();

            #region Verifica login
            if (loginAtivo)
            {
                //Se o comando for Sim
                if (MessageBox.Show("Deseja desligar o computador?", "Desligar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //Pega o comando e o parâmetro que está inserido no arquivo MenuUser em XML
                    string[] comandoUser = xmlController.InformacoesProgramasUser("Desligar");

                    //Executa o comando Reinciar
                    Process.Start(comandoUser[1], "-s -t 10");
                }

                //Reseta a variável
                loginAtivo = false;
            }
            else
            {
                //Se o usuário tiver errado o login
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
            #endregion
        }

        private void Configuracao_Click(object sender, EventArgs e)
        {
            //Cria e abre o form de Login
            Login formLogin = new Login();
            formLogin.ShowDialog();

            #region Verifica login
            if (loginAtivo)
            {
                //Reseta a variável
                loginAtivo = false;

                //Cria e abre o form de Configuração
                Configuracao formConfiguracao = new Configuracao();
                formConfiguracao.ShowDialog();
            }
            else
            {
                //Se o usuário tiver errado o login
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
            #endregion
        }
        #endregion

        #region Botão User Barra de Tarefas
        private void gerenciadorTarefas_Click(object sender, EventArgs e)
        {
            GerenciadorTarefas formGerTar = new GerenciadorTarefas(sameAsthisSession);
            formGerTar.ShowDialog();
        }
        #endregion

        #region Botões ADM Menu
        private void btnWinExplorer_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            formLogin.ShowDialog();

            if (loginAtivo)
            {
                //Reseta a variável
                loginAtivo = false;

                string[] comandoADM = xmlController.InformacoesProgramasADM("Explorer");
                if (!ProgramIsRunning(comandoADM[1]))
                {
                    try
                    {
                        //Pega o comando e o parâmetro que está inserido no arquivo MenuADM em XML
                        Process.Start(comandoADM[1], comandoADM[2]);
                        btnADMExplorerAberto.Image = btnWinExplorer.Image;
                        btnADMExplorerAberto.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnWinExplorer.Text + "!");
                        btnADMExplorerAberto.Visible = false;
                    }
                }
            }
            else
            {
                if(erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            formLogin.ShowDialog();

            if (loginAtivo)
            {
                //Reseta a variável
                loginAtivo = false;


                string[] comandoADM = xmlController.InformacoesProgramasADM("CMD");
                if (!ProgramIsRunning(comandoADM[1]))
                {
                    try
                    {
                        //Pega o comando e o parâmetro que está inserido no arquivo MenuADM em XML
                        Process.Start(xmlController.InformacoesProgramasADM("CMD")[1]);
                        btnADMCMDAberto.Image = btnExecutar.Image;
                        btnADMCMDAberto.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnExecutar.Text + "!");
                        btnADMCMDAberto.Visible = false;
                    }
                }
            }
            else
            {
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
        }

        private void btnADM1_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            formLogin.ShowDialog();

            if (loginAtivo)
            {
                //Reseta a variável
                loginAtivo = false;
                string[] comandoADM = xmlController.InformacoesProgramasADM("ADM1");
                if (!ProgramIsRunning(@"" + comandoADM[1]))
                {
                    try
                    {
                        //Pega o comando e o parâmetro que está inserido no arquivo MenuADM em XML
                        Process.Start(@"" + comandoADM[1], comandoADM[2]);
                        btnADM1Aberto.Image = btnADM1.Image;
                        btnADM1Aberto.ToolTipText = btnADM1.Text;
                        btnADM1Aberto.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnADM1.Text + "!");
                        btnADM1Aberto.Visible = false;
                    }
                }
            }
            else
            {
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
        }

        private void btnADM2_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            formLogin.ShowDialog();

            if (loginAtivo)
            {
                //Reseta a variável
                loginAtivo = false;

                string[] comandoADM = xmlController.InformacoesProgramasADM("ADM2");
                if (!ProgramIsRunning(@"" + comandoADM[1]))
                {
                    try
                    {
                        //Pega o comando e o parâmetro que está inserido no arquivo MenuADM em XML
                        Process.Start(@"" + comandoADM[1], comandoADM[2]);
                        btnADM2Aberto.Image = btnADM2.Image;
                        btnADM2Aberto.ToolTipText = btnADM2.Text;
                        btnADM2Aberto.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnADM2.Text + "!");
                        btnADM2Aberto.Visible = false;
                    }
                }
            }
            else
            {
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
        }

        private void btnADM3_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            formLogin.ShowDialog();

            if (loginAtivo)
            {
                //Reseta a variável
                loginAtivo = false;

                string[] comandoADM = xmlController.InformacoesProgramasADM("ADM3");
                if (!ProgramIsRunning(@"" + comandoADM[1]))
                {
                    try
                    {
                        //Pega o comando e o parâmetro que está inserido no arquivo MenuADM em XML
                        Process.Start(@"" + comandoADM[1], comandoADM[2]);
                        btnADM3Aberto.Image = btnADM3.Image;
                        btnADM3Aberto.ToolTipText = btnADM3.Text;
                        btnADM3Aberto.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnADM3.Text + "!");
                        btnADM3Aberto.Visible = false;
                    }
                }
            }
            else
            {
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
        }

        private void btnADM4_Click(object sender, EventArgs e)
        {
            Login formLogin = new Login();
            formLogin.ShowDialog();

            if (loginAtivo)
            {
                //Reseta a variável
                loginAtivo = false;

                string[] comandoADM = xmlController.InformacoesProgramasADM("ADM4");
                if (!ProgramIsRunning(@"" + comandoADM[1]))
                {
                    try
                    {
                        //Pega o comando e o parâmetro que está inserido no arquivo MenuADM em XML
                        Process.Start(@"" + comandoADM[1], comandoADM[2]);
                        btnADM4Aberto.Image = btnADM4.Image;
                        btnADM4Aberto.ToolTipText = btnADM4.Text;
                        btnADM4Aberto.Visible = true;
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnADM4.Text + "!");
                        btnADM4Aberto.Visible = false;
                    }
                }
            }
            else
            {
                if (erroLogin)
                {
                    AutoClosingMessageBox.Show("O usuário ou a senha estão errados!", "Login inválido.", 2000);
                }
            }
        }
        #endregion

        #region Botões User Menu
        private void btnDesligar_Click(object sender, EventArgs e)
        {
            //Pega o comando e o parâmetro que está inserido no arquivo MenuUser em XML
            string[] comandoUser = xmlController.InformacoesProgramasUser("Desligar");

            //Executa o comando Desligar
            Process.Start(comandoUser[1], comandoUser[2]);
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            //Pega o comando e o parâmetro que está inserido no arquivo MenuUser em XML
            string[] comandoUser = xmlController.InformacoesProgramasUser("Reiniciar");

            //Executa o comando Reinciar
            Process.Start(comandoUser[1], comandoUser[2]);
        }

        private void btnUser1_Click(object sender, EventArgs e)
        {
            //Pega o comando e o parâmetro que está inserido no arquivo MenuADM em XML
            string[] comandoUser = xmlController.InformacoesProgramasUser("User1");
            if (!ProgramIsRunning(@"" + comandoUser[1]))
            {
                try
                {
                    Process.Start(@"" + comandoUser[1], comandoUser[2]);
                    btnUser1Aberto.Image = btnUser1.Image;
                    btnUser1Aberto.ToolTipText = btnUser1.Text;
                    btnUser1Aberto.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o "+btnUser1.Text+"!");
                    btnUser1Aberto.Visible = false;
                }
            }
        }

        private void btnUser2_Click(object sender, EventArgs e)
        {
            string[] comandoUser = xmlController.InformacoesProgramasUser("User2");
            if (!ProgramIsRunning(@"" + comandoUser[1]))
            {
                try
                {
                    Process.Start(@"" + comandoUser[1], comandoUser[2]);
                    btnUser2Aberto.Image = btnUser2.Image;
                    btnUser2Aberto.ToolTipText = btnUser2.Text;
                    btnUser2Aberto.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnUser2.Text + "!");
                    btnUser2Aberto.Visible = false;
                }
            }
        }

        private void btnUser3_Click(object sender, EventArgs e)
        {
            string[] comandoUser = xmlController.InformacoesProgramasUser("User3");
            if (!ProgramIsRunning(@"" + comandoUser[1]))
            {
                try
                {
                    Process.Start(@"" + comandoUser[1], comandoUser[2]);
                    btnUser3Aberto.Image = btnUser3.Image;
                    btnUser3Aberto.ToolTipText = btnUser3.Text;
                    btnUser3Aberto.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + btnUser3.Text + "!");
                    btnUser3Aberto.Visible = false;
                }
            }
        }

        private void btnUser4_Click(object sender, EventArgs e)
        {
            string[] comandoUser = xmlController.InformacoesProgramasUser("User4");
            if (!ProgramIsRunning(@"" + comandoUser[1]))
            {
                try
                {
                    Process.Start(@"" + comandoUser[1], comandoUser[2]);
                    btnUser4Aberto.Image = BtnUser4.Image;
                    btnUser4Aberto.ToolTipText = BtnUser4.Text;
                    btnUser4Aberto.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Erro ao abrir o programa. \nFavor ligar para a CFTV & Automação e infomar o erro! Telefone: 31 3362-6134\n A CTVF & Automação agradece seu apoio, caro usuário!", "Erro ao abrir o " + BtnUser4.Text + "!");
                    btnUser4Aberto.Visible = false;
                }
            }
        }
        #endregion

        #region Aplicativos abertos Barra de Tarefas
        private void btnUser1Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoUser = xmlController.InformacoesProgramasUser("User1");
            if (CheckIfProgramIsRunning(@"" + comandoUser[1]))
            {
                string process = ProcessProgramIsRunning(@"" + comandoUser[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
                
        }

        private void btnUser2Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoUser = xmlController.InformacoesProgramasUser("User2");
            if (CheckIfProgramIsRunning(@"" + comandoUser[1]))
            {
                string process = ProcessProgramIsRunning(@"" + comandoUser[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnUser3Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoUser = xmlController.InformacoesProgramasUser("User3");
            if (CheckIfProgramIsRunning(@"" + comandoUser[1]))
            {
                string process = ProcessProgramIsRunning(@"" + comandoUser[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnUser4Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoUser = xmlController.InformacoesProgramasUser("User4");
            if (CheckIfProgramIsRunning(@"" + comandoUser[1]))
            {
                string process = ProcessProgramIsRunning(@"" + comandoUser[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnADMCMDAberto_Click(object sender, EventArgs e)
        {
            string[] comandoADM = xmlController.InformacoesProgramasADM("CMD");
            if (CheckIfProgramIsRunning(comandoADM[1]))
            {
                string process = ProcessProgramIsRunning(comandoADM[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnADMExplorerAberto_Click(object sender, EventArgs e)
        {
            string[] comandoADM = xmlController.InformacoesProgramasADM("Explorer");
            if (CheckIfProgramIsRunning(comandoADM[1]))
            {
                string process = ProcessProgramIsRunning(comandoADM[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnADM1Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoADM = xmlController.InformacoesProgramasADM("ADM1");
            if (CheckIfProgramIsRunning(@""+comandoADM[1]))
            {
                string process = ProcessProgramIsRunning(@""+comandoADM[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnADM2Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoADM = xmlController.InformacoesProgramasADM("ADM2");
            if (CheckIfProgramIsRunning(@"" + comandoADM[1]))
            {
                string process = ProcessProgramIsRunning(@"" + comandoADM[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnADM3Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoADM = xmlController.InformacoesProgramasADM("ADM3");
            if (CheckIfProgramIsRunning(@"" + comandoADM[1]))
            {
                string process = ProcessProgramIsRunning(@"" + comandoADM[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }

        private void btnADM4Aberto_Click(object sender, EventArgs e)
        {
            string[] comandoADM = xmlController.InformacoesProgramasADM("ADM4");
            if (CheckIfProgramIsRunning(@"" + comandoADM[1]))
            {
                string process = ProcessProgramIsRunning(@"" + comandoADM[1]);

                if (Process.GetProcessesByName(process).Any())
                {
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                    ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                    SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
                }
            }
        }
        #endregion

        #region Modifica Status Menu Inicial
        private void iniciar_DropDownOpened(object sender, EventArgs e)
        {
            iniciarAberto = true;
        }

        private void iniciar_DropDownClosed(object sender, EventArgs e)
        {
            iniciarAberto = false;
        }
        #endregion

        private void TClock_Tick(object sender, EventArgs e)
        {
            #region Atualiza Informações
            if(atualizaInformacoes)
            {
                #region Papel de Parede
                //Verifica se há algum papel de parede diferente do original
                if (xmlController.InformacaoPapelParede() != "" && xmlController.InformacaoPapelParede() != null && xmlController.InformacaoPapelParede() != "False")
                {
                    //Altera a imagem de fundo
                    this.BackgroundImage = Image.FromFile(xmlController.InformacaoPapelParede());
                    //Faz com que ela se ajuste ao tamanho da tela
                    this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                }
                #endregion

                #region Esconde Menu Inicial
                //Se o menu inicial estiver como "escondido"
                if (xmlController.VerificaStatusMenu())
                {
                    //Esconde o menu inicial
                    menuInicial.Visible = false;
                    //e ativa esse boolean para checagem
                    menuEscondido = true;
                }
                else
                {
                    //Mostra o menu inicial
                    menuInicial.Visible = true;
                    //desativa esse boolean
                    menuEscondido = false;
                }
                #endregion

                #region Configura textos e imagem dos Botões ADM e User
                //Se o xmlController.InformacoesProgramasUser("BLOCO")[nome] != "NADA", ele preenche com o texto, caso contrário deixa o nome original.
                btnUser1.Text = xmlController.InformacoesProgramasUser("User1")[0] != "" ? xmlController.InformacoesProgramasUser("User1")[0] : "Botão User 1";
                if (xmlController.InformacoesProgramasUser("User1")[1] != null)
                {
                    btnUser1.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User1")[1]).ToBitmap();
                }

                btnUser2.Text = xmlController.InformacoesProgramasUser("User2")[0] != "" ? xmlController.InformacoesProgramasUser("User2")[0] : "Botão User 2";
                if (xmlController.InformacoesProgramasUser("User2")[1] != null)
                {
                    btnUser2.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User2")[1]).ToBitmap();
                }
                btnUser3.Text = xmlController.InformacoesProgramasUser("User3")[0] != "" ? xmlController.InformacoesProgramasUser("User3")[0] : "Botão User 3";
                if (xmlController.InformacoesProgramasUser("User3")[1] != null)
                {
                    btnUser3.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User3")[1]).ToBitmap();
                }
                BtnUser4.Text = xmlController.InformacoesProgramasUser("User4")[0] != "" ? xmlController.InformacoesProgramasUser("User4")[0] : "Botão User 4";
                if (xmlController.InformacoesProgramasUser("User4")[1] != null)
                {
                    BtnUser4.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasUser("User4")[1]).ToBitmap();
                }

                //Se o xmlController.InformacoesProgramasADM("BLOCO")[nome] != "NADA", ele preenche com o texto, caso contrário deixa o nome original.
                btnADM1.Text = xmlController.InformacoesProgramasADM("ADM1")[0] != "" ? xmlController.InformacoesProgramasADM("ADM1")[0] : "Botão ADM 1";
                if (xmlController.InformacoesProgramasADM("ADM1")[1] != null)
                {
                    btnADM1.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM1")[1]).ToBitmap();
                }
                btnADM2.Text = xmlController.InformacoesProgramasADM("ADM2")[0] != "" ? xmlController.InformacoesProgramasADM("ADM2")[0] : "Botão ADM 2";
                if (xmlController.InformacoesProgramasADM("ADM2")[1] != null)
                {
                    btnADM2.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM2")[1]).ToBitmap();
                }
                btnADM3.Text = xmlController.InformacoesProgramasADM("ADM3")[0] != "" ? xmlController.InformacoesProgramasADM("ADM3")[0] : "Botão ADM 3";
                if (xmlController.InformacoesProgramasADM("ADM3")[1] != null)
                {
                    btnADM3.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM3")[1]).ToBitmap();
                }
                btnADM4.Text = xmlController.InformacoesProgramasADM("ADM4")[0] != "" ? xmlController.InformacoesProgramasADM("ADM4")[0] : "Botão ADM 4";
                if (xmlController.InformacoesProgramasADM("ADM4")[1] != null)
                {
                    btnADM4.Image = Icon.ExtractAssociatedIcon(xmlController.InformacoesProgramasADM("ADM4")[1]).ToBitmap();
                }
                #endregion

                #region Configura Visualização Botões ADM e User
                //Se o xmlController.InformacoesProgramasUser("BLOCO")[status] == "nao", ele deixa falso, caso contrário deixa true.
                btnUser1.Visible = xmlController.InformacoesProgramasUser("User1")[3] == "no" ? true : false;
                btnUser2.Visible = xmlController.InformacoesProgramasUser("User2")[3] == "no" ? true : false;
                btnUser3.Visible = xmlController.InformacoesProgramasUser("User3")[3] == "no" ? true : false;
                BtnUser4.Visible = xmlController.InformacoesProgramasUser("User4")[3] == "no" ? true : false;

                //Se o xmlController.InformacoesProgramasADM("BLOCO")[status] == "nao", ele deixa falso, caso contrário deixa true.
                btnADM1.Visible = xmlController.InformacoesProgramasADM("ADM1")[3] == "no" ? true : false;
                btnADM2.Visible = xmlController.InformacoesProgramasADM("ADM2")[3] == "no" ? true : false;
                btnADM3.Visible = xmlController.InformacoesProgramasADM("ADM3")[3] == "no" ? true : false;
                btnADM4.Visible = xmlController.InformacoesProgramasADM("ADM4")[3] == "no" ? true : false;
                #endregion

                atualizaInformacoes = false;
            }
            #endregion

            #region Verifica se o menu está escondido
            if (menuEscondido)
            {
                //Pega a posição atual em Y do mouse
                mousePositionY = Cursor.Position.Y;

                //Se o menu iniciar não estiver aberto
                if (!iniciarAberto)
                {
                    //Verifica se a posição do mouse está dentro da área do menu
                    if (mousePositionY >= (this.Height - 1 - menuInicial.Height))
                    {
                        menuInicial.Visible = true;
                    }
                    else
                    {
                        menuInicial.Visible = false;
                    }
                }
            }
            #endregion

            #region Verifica Status Botões
            if (File.Exists(@"c:\TiagoSM\programaAdmCMD.xml"))
            {
                string[] comandoUser1 = xmlController.InformacoesProgramasUser("User1");
                if (comandoUser1[1] != null)
                {
                    if (CheckIfProgramIsRunning(@"" + comandoUser1[1]))
                    {
                        btnUser1Aberto.Visible = true;
                    }
                    else
                    {
                        btnUser1Aberto.Visible = false;
                    }
                }

                string[] comandoUser2 = xmlController.InformacoesProgramasUser("User2");
                if (comandoUser2[1] != null)
                {
                    if (CheckIfProgramIsRunning(@"" + comandoUser2[1]))
                    {
                        btnUser2Aberto.Visible = true;
                    }
                    else
                    {
                        btnUser2Aberto.Visible = false;
                    }
                }

                string[] comandoUser3 = xmlController.InformacoesProgramasUser("User3");
                if (comandoUser3[1] != null)
                {
                    if (CheckIfProgramIsRunning(@"" + comandoUser3[1]))
                    {
                        btnUser3Aberto.Visible = true;
                    }
                    else
                    {
                        btnUser3Aberto.Visible = false;
                    }
                }

                string[] comandoUser4 = xmlController.InformacoesProgramasUser("User4");
                if (comandoUser4[1] != null)
                {
                    if (CheckIfProgramIsRunning(@"" + comandoUser4[1]))
                    {
                        btnUser4Aberto.Visible = true;
                    }
                    else
                    {
                        btnUser4Aberto.Visible = false;
                    }
                }

                string[] comandocmd = xmlController.InformacoesProgramasADM("CMD");
                if (comandocmd[1] != null)
                {
                    if (CheckIfProgramIsRunning(comandocmd[1]))
                    {
                        btnADMCMDAberto.Visible = true;
                    }
                    else
                    {
                        btnADMCMDAberto.Visible = false;
                    }
                }

                string[] comandoExplorer = xmlController.InformacoesProgramasADM("Explorer");
                if (comandoExplorer[1] != null)
                {
                    if (CheckIfProgramIsRunning(comandoExplorer[1]))
                    {
                        btnADMExplorerAberto.Visible = true;
                    }
                    else
                    {
                        btnADMExplorerAberto.Visible = false;
                    }
                }

                string[] comandoADM1 = xmlController.InformacoesProgramasADM("ADM1");
                if (comandoADM1[1] != null)
                {
                    if (CheckIfProgramIsRunning(comandoADM1[1]))
                    {
                        btnADM1Aberto.Visible = true;
                    }
                    else
                    {
                        btnADM1Aberto.Visible = false;
                    }
                }

                string[] comandoADM2 = xmlController.InformacoesProgramasADM("ADM2");
                if (comandoADM2[1] != null)
                {
                    if (CheckIfProgramIsRunning(comandoADM2[1]))
                    {
                        btnADM2Aberto.Visible = true;
                    }
                    else
                    {
                        btnADM2Aberto.Visible = false;
                    }
                }

                string[] comandoADM3 = xmlController.InformacoesProgramasADM("ADM3");
                if (comandoADM3[1] != null)
                {
                    if (CheckIfProgramIsRunning(comandoADM3[1]))
                    {
                        btnADM3Aberto.Visible = true;
                    }
                    else
                    {
                        btnADM3Aberto.Visible = false;
                    }
                }

                string[] comandoADM4 = xmlController.InformacoesProgramasADM("ADM4");
                if (comandoADM4[1] != null)
                {
                    if (CheckIfProgramIsRunning(comandoADM4[1]))
                    {
                        btnADM4Aberto.Visible = true;
                    }
                    else
                    {
                        btnADM4Aberto.Visible = false;
                    }
                }
            }
            #endregion
        }
    }
}
