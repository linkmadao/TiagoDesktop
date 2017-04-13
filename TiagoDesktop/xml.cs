using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiagoDesktop
{
    class xml
    {
        public void Cria(string usuario, string senha)
        {
            //Cria o documento XML como banco de dados
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create("login.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            writer.WriteStartElement("Login");
            writer.WriteAttributeString("Usuario", usuario.ToUpper());
            writer.WriteAttributeString("Senha", senha.ToUpper());
            writer.WriteAttributeString("iniciarComWindows", "false");
            writer.WriteAttributeString("escondeMenu", "false");
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();


            System.IO.Directory.CreateDirectory(@"C:\TiagoSM");

            DirectoryInfo di = new DirectoryInfo(@"C:\TiagoSM");
            di.Attributes = FileAttributes.Hidden;

            FileInfo destino = new FileInfo(@"C:\TiagoSM\login.xml");
            FileInfo atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\login.xml");
            File.Copy(atual.FullName, destino.FullName, true);

            CriaListaProgramasAdm();
            CriaListaProgramasUser();
            CriaXMLPapelParedePrograma();
        }

        public bool VerificaUsuario(string usuario, string senha)
        {
            bool status = false;

            string[] login = new string[2];
            string curFile = @"c:\TiagoSM\login.xml";

            if (File.Exists(curFile))
            {
                XmlReader reader = XmlReader.Create(curFile);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Login")
                    {
                        login[0] = reader.GetAttribute(0);
                        login[1] = reader.GetAttribute(1);
                    } //end if
                } //end while

                reader.Close();

                if(login[0] == usuario.ToUpper() && login[1] == senha.ToUpper())
                {
                    status = true;
                }
            }

            return status;
        }

        public bool VerificaInicializacao()
        {
            bool status = false;
            string curFile = @"c:\TiagoSM\login.xml";
            
            if (File.Exists(curFile))
            {
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                XmlReader reader = XmlReader.Create(curFile);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Login")
                    {
                        if (reader.GetAttribute(2) == "true" && rkApp.GetValue("TiagoDesktop") != null)
                        {
                            status = true;
                        }
                    } //end if
                } //end while

                reader.Close();
            }

            return status;
        }

        public bool VerificaStatusMenu()
        {
            bool status = false;

            string curFile = @"c:\TiagoSM\login.xml";

            if (File.Exists(curFile))
            {
                XmlReader reader = XmlReader.Create(curFile);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Login")
                    {
                        if (reader.GetAttribute(3) == "true")
                        {
                            status = true;
                        }
                    } //end if
                } //end while

                reader.Close();
            }

            return status;
        }

        public void AlteraInicializacao(bool status)
        {
            string curFile = @"c:\TiagoSM\login.xml";

            if (File.Exists(curFile))
            {
                RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements("Login") select c;

                if (status)
                {
                    DesabilitaExplorer();
                    foreach (XElement dados in query)
                    {
                        dados.Attribute("iniciarComWindows").Value = "true";
                    }

                    rkApp.SetValue("TiagoDesktop", Application.ExecutablePath.ToString());
                    rkApp.Close();

                }
                else
                {
                    HabilitaExplorer();
                    foreach (XElement dados in query)
                    {
                        dados.Attribute("iniciarComWindows").Value = "false";
                    }
                    rkApp.DeleteValue("TiagoDesktop", false);
                    rkApp.Close();
                }

                xmlFile.Save(curFile);

                MessageBox.Show("Modo de inicialização modificado com sucesso!", "Inicialização alterada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void AlteraStatusMenu(bool status)
        {
            string curFile = @"c:\TiagoSM\login.xml";

            if (File.Exists(curFile))
            {
                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements("Login") select c;

                if (status)
                {
                    foreach (XElement dados in query)
                    {
                        dados.Attribute("escondeMenu").Value = "true";
                    }
                }
                else
                {
                    foreach (XElement dados in query)
                    {
                        dados.Attribute("escondeMenu").Value = "false";
                    }
                }

                xmlFile.Save(curFile);


                MessageBox.Show("Status do menu modificado com sucesso!", "Status do menu alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void AlteraSenha(string senha)
        {
            string curFile = @"c:\TiagoSM\login.xml";

            if (File.Exists(curFile))
            {
                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements("Login") select c;

                foreach (XElement dados in query)
                {
                    dados.Attribute("Senha").Value = senha.ToUpper();
                }
                xmlFile.Save(curFile);

                MessageBox.Show("Senha modificada com sucesso!", "Alteração de senha", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DesabilitaExplorer()
        {
            RegistryKey regKey;
            // Change the Local Machine shell executable
            regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            regKey.SetValue("Shell", Path.GetDirectoryName(Application.ExecutablePath) + "\\TiagoDesktop.exe", RegistryValueKind.String);
            regKey.Close();
            // Create the Shell executable Registry entry for Current User
            regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            regKey.SetValue("Shell", Path.GetDirectoryName(Application.ExecutablePath) + "\\TiagoDesktop.exe");
            regKey.Close();
        }

        private void HabilitaExplorer()
        {
            RegistryKey regKey;
            // Change the Local Machine shell executable
            regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            regKey.SetValue("Shell", "explorer.exe", RegistryValueKind.String);
            regKey.Close();
            // Create the Shell executable Registry entry for Current User
            regKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            regKey.SetValue("Shell", "explorer.exe");
            regKey.Close();
        }        

        private void CriaListaProgramasAdm()
        {
            FileInfo atual;

            #region CMD
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsCMD = new XmlWriterSettings();
            settingsCMD.Indent = true;
            XmlWriter writerCMD = XmlWriter.Create("programaAdmCMD.xml", settingsCMD);
            writerCMD.WriteStartDocument();
            writerCMD.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa CMD
            writerCMD.WriteStartElement("CMD");
            writerCMD.WriteAttributeString("Nome", "Executar");
            writerCMD.WriteAttributeString("Caminho", "cmd");
            writerCMD.WriteAttributeString("Comando", "");
            writerCMD.WriteAttributeString("Habilitado", "yes");
            writerCMD.WriteEndElement(); //Programa CMD
            writerCMD.WriteEndDocument();
            writerCMD.Flush();
            writerCMD.Close();

            FileInfo destinoCMD = new FileInfo(@"C:\TiagoSM\programaAdmCMD.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaAdmCMD.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoCMD.FullName, true);
            
            #endregion

            #region Explorer
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsExplorer = new XmlWriterSettings();
            settingsExplorer.Indent = true;
            XmlWriter writerExplorer = XmlWriter.Create("programaAdmExplorer.xml", settingsExplorer);
            writerExplorer.WriteStartDocument();
            writerExplorer.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa Explorer
            writerExplorer.WriteStartElement("Explorer");
            writerExplorer.WriteAttributeString("Nome", "Windows Explorer");
            writerExplorer.WriteAttributeString("Caminho", "explorer");
            writerExplorer.WriteAttributeString("Comando", "c:");
            writerExplorer.WriteAttributeString("Habilitado", "yes");
            writerExplorer.WriteEndElement(); //Programa Explorer
            writerExplorer.WriteEndDocument();
            writerExplorer.Flush();
            writerExplorer.Close();

            FileInfo destinoExplorer = new FileInfo(@"C:\TiagoSM\programaAdmExplorer.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaAdmExplorer.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoExplorer.FullName, true);
            #endregion

            #region ADM1
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsADM1 = new XmlWriterSettings();
            settingsADM1.Indent = true;
            XmlWriter writerADM1 = XmlWriter.Create("programaAdm1.xml", settingsADM1);
            writerADM1.WriteStartDocument();
            writerADM1.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa ADM 1
            writerADM1.WriteStartElement("ADM1");
            writerADM1.WriteAttributeString("Nome", "");
            writerADM1.WriteAttributeString("Caminho", "");
            writerADM1.WriteAttributeString("Comando", "");
            writerADM1.WriteAttributeString("Habilitado", "no");
            writerADM1.WriteEndElement(); //Programa ADM 1
            writerADM1.WriteEndDocument();
            writerADM1.Flush();
            writerADM1.Close();
            FileInfo destinoADM1 = new FileInfo(@"C:\TiagoSM\programaAdm1.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaAdm1.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoADM1.FullName, true);
            #endregion

            #region ADM2
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsADM2 = new XmlWriterSettings();
            settingsADM2.Indent = true;
            XmlWriter writerADM2 = XmlWriter.Create("programaAdm2.xml", settingsADM2);
            writerADM2.WriteStartDocument();
            writerADM2.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa ADM 2
            writerADM2.WriteStartElement("ADM2");
            writerADM2.WriteAttributeString("Nome", "");
            writerADM2.WriteAttributeString("Caminho", "");
            writerADM2.WriteAttributeString("Comando", "");
            writerADM2.WriteAttributeString("Habilitado", "no");
            writerADM2.WriteEndElement(); //Programa ADM 2
            writerADM2.WriteEndDocument();
            writerADM2.Flush();
            writerADM2.Close();
            FileInfo destinoADM2 = new FileInfo(@"C:\TiagoSM\programaAdm2.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaAdm2.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoADM2.FullName, true);
            #endregion

            #region ADM3
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsADM3 = new XmlWriterSettings();
            settingsADM3.Indent = true;
            XmlWriter writerADM3 = XmlWriter.Create("programaAdm3.xml", settingsADM3);
            writerADM3.WriteStartDocument();
            writerADM3.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa ADM 3
            writerADM3.WriteStartElement("ADM3");
            writerADM3.WriteAttributeString("Nome", "");
            writerADM3.WriteAttributeString("Caminho", "");
            writerADM3.WriteAttributeString("Comando", "");
            writerADM3.WriteAttributeString("Habilitado", "no");
            writerADM3.WriteEndElement(); //Programa ADM 3
            writerADM3.WriteEndDocument();
            writerADM3.Flush();
            writerADM3.Close();
            FileInfo destinoADM3 = new FileInfo(@"C:\TiagoSM\programaAdm3.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaAdm3.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoADM3.FullName, true);
            #endregion

            #region ADM4
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsADM4 = new XmlWriterSettings();
            settingsADM4.Indent = true;
            XmlWriter writerADM4 = XmlWriter.Create("programaAdm4.xml", settingsADM4);
            writerADM4.WriteStartDocument();
            writerADM4.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa ADM 4
            writerADM4.WriteStartElement("ADM4");
            writerADM4.WriteAttributeString("Nome", "");
            writerADM4.WriteAttributeString("Caminho", "");
            writerADM4.WriteAttributeString("Comando", "");
            writerADM4.WriteAttributeString("Habilitado", "no");
            writerADM4.WriteEndElement(); //Programa ADM 4
            writerADM4.WriteEndDocument();
            writerADM4.Flush();
            writerADM4.Close();
            FileInfo destinoADM4 = new FileInfo(@"C:\TiagoSM\programaAdm4.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaAdm4.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoADM4.FullName, true);
            #endregion
        }

        private void CriaListaProgramasUser()
        {
            FileInfo atual;

            #region Desligar
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsDesligar = new XmlWriterSettings();
            settingsDesligar.Indent = true;
            XmlWriter writerDesligar = XmlWriter.Create("programaUserDesligar.xml", settingsDesligar);
            writerDesligar.WriteStartDocument();
            writerDesligar.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa CMD
            writerDesligar.WriteStartElement("Desligar");
            writerDesligar.WriteAttributeString("Nome", "Desligar");
            writerDesligar.WriteAttributeString("Caminho", "shutdown");
            writerDesligar.WriteAttributeString("Comando", "-s -t 30");
            writerDesligar.WriteAttributeString("Habilitado", "yes");
            writerDesligar.WriteEndElement(); //Programa Desligar
            writerDesligar.WriteEndDocument();
            writerDesligar.Flush();
            writerDesligar.Close();
     
            FileInfo destinoDesligar = new FileInfo(@"C:\TiagoSM\programaUserDesligar.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaUserDesligar.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoDesligar.FullName, true);
            #endregion

            #region Reiniciar
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsReiniciar = new XmlWriterSettings();
            settingsReiniciar.Indent = true;
            XmlWriter writerReiniciar = XmlWriter.Create("programaUserReiniciar.xml", settingsReiniciar);
            writerReiniciar.WriteStartDocument();
            writerReiniciar.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa Reiniciar
            writerReiniciar.WriteStartElement("Reiniciar");
            writerReiniciar.WriteAttributeString("Nome", "Reiniciar");
            writerReiniciar.WriteAttributeString("Caminho", "shutdown");
            writerReiniciar.WriteAttributeString("Comando", "-r -t 5");
            writerReiniciar.WriteAttributeString("Habilitado", "yes");
            writerReiniciar.WriteEndElement(); //Programa Reiniciar
            writerReiniciar.WriteEndDocument();
            writerReiniciar.Flush();
            writerReiniciar.Close();

            FileInfo destinoReiniciar = new FileInfo(@"C:\TiagoSM\programaUserReiniciar.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaUserReiniciar.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoReiniciar.FullName, true);
            #endregion

            #region User1
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsUser1 = new XmlWriterSettings();
            settingsUser1.Indent = true;
            XmlWriter writerUser1 = XmlWriter.Create("programaUser1.xml", settingsUser1);
            writerUser1.WriteStartDocument();
            writerUser1.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa User1
            writerUser1.WriteStartElement("User1");
            writerUser1.WriteAttributeString("Nome", "");
            writerUser1.WriteAttributeString("Caminho", "");
            writerUser1.WriteAttributeString("Comando", "");
            writerUser1.WriteAttributeString("Habilitado", "no");
            writerUser1.WriteEndElement(); //Programa User1
            writerUser1.WriteEndDocument();
            writerUser1.Flush();
            writerUser1.Close();

            FileInfo destinoUser1 = new FileInfo(@"C:\TiagoSM\programaUser1.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaUser1.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoUser1.FullName, true);

            #endregion

            #region User2
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsUser2 = new XmlWriterSettings();
            settingsUser2.Indent = true;
            XmlWriter writerUser2 = XmlWriter.Create("programaUser2.xml", settingsUser2);
            writerUser2.WriteStartDocument();
            writerUser2.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa User2
            writerUser2.WriteStartElement("User2");
            writerUser2.WriteAttributeString("Nome", "");
            writerUser2.WriteAttributeString("Caminho", "");
            writerUser2.WriteAttributeString("Comando", "");
            writerUser2.WriteAttributeString("Habilitado", "no");
            writerUser2.WriteEndElement(); //Programa User2
            writerUser2.WriteEndDocument();
            writerUser2.Flush();
            writerUser2.Close();

            FileInfo destinoUser2 = new FileInfo(@"C:\TiagoSM\programaUser2.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaUser2.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoUser2.FullName, true);
            #endregion

            #region User3
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsUser3 = new XmlWriterSettings();
            settingsUser3.Indent = true;
            XmlWriter writerUser3 = XmlWriter.Create("programaUser3.xml", settingsUser3);
            writerUser3.WriteStartDocument();
            writerUser3.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa User3
            writerUser3.WriteStartElement("User3");
            writerUser3.WriteAttributeString("Nome", "");
            writerUser3.WriteAttributeString("Caminho", "");
            writerUser3.WriteAttributeString("Comando", "");
            writerUser3.WriteAttributeString("Habilitado", "no");
            writerUser3.WriteEndElement(); //Programa User3
            writerUser3.WriteEndDocument();
            writerUser3.Flush();
            writerUser3.Close();

            FileInfo destinoUser3 = new FileInfo(@"C:\TiagoSM\programaUser3.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaUser3.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoUser3.FullName, true);
            #endregion

            #region User4
            //Cria o documento XML que armazena sequência de programas
            XmlWriterSettings settingsUser4 = new XmlWriterSettings();
            settingsUser4.Indent = true;
            XmlWriter writerUser4 = XmlWriter.Create("programaUser4.xml", settingsUser4);
            writerUser4.WriteStartDocument();
            writerUser4.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            //Programa User4
            writerUser4.WriteStartElement("User4");
            writerUser4.WriteAttributeString("Nome", "");
            writerUser4.WriteAttributeString("Caminho", "");
            writerUser4.WriteAttributeString("Comando", "");
            writerUser4.WriteAttributeString("Habilitado", "no");
            writerUser4.WriteEndElement(); //Programa User4
            writerUser4.WriteEndDocument();
            writerUser4.Flush();
            writerUser4.Close();

            FileInfo destinoUser4 = new FileInfo(@"C:\TiagoSM\programaUser4.xml");
            atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\programaUser4.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destinoUser4.FullName, true);
            #endregion
        }

        public string[] InformacoesProgramasADM(string ProgramaRequisitado)
        {
            //Cria um vetor de string com 4 linhas
            string[] programa = new string[4];
            //Caminho ao qual o software irá procurar o XML
            string curFile = "";
            string fileOpen = "";

            bool programaExiste = true;

            switch (ProgramaRequisitado)
            {
                case "CMD":
                    curFile = @"c:\TiagoSM\programaAdmCMD.xml";
                    fileOpen = "programaAdmCMD.xml";
                    break;
                case "Explorer":
                    curFile = @"c:\TiagoSM\programaAdmExplorer.xml";
                    fileOpen = "programaAdmExplorer.xml";
                    break;
                case "ADM1":
                    curFile = @"c:\TiagoSM\programaAdm1.xml";
                    fileOpen = "programaAdm1.xml";
                    break;
                case "ADM2":
                    curFile = @"c:\TiagoSM\programaAdm2.xml";
                    fileOpen = "programaAdm2.xml";
                    break;
                case "ADM3":
                    curFile = @"c:\TiagoSM\programaAdm3.xml";
                    fileOpen = "programaAdm3.xml";
                    break;
                case "ADM4":
                    curFile = @"c:\TiagoSM\programaAdm4.xml";
                    fileOpen = "programaAdm4.xml";
                    break;
                default:
                    programaExiste = false;
                    break;
            }
            //Verifica se o nome digitado na função existe
            if(programaExiste)
            {
                //Verifica se o XML Existe
                if (File.Exists(curFile))
                {
                    //Se sim, ele irá carregar o arquivo
                    XmlReader reader = XmlReader.Create(curFile);

                    //Enquanto estiver executando a leitura
                    while (reader.Read())
                    {
                        //Verifica se o tipo de nodulo é um Element e se o nome dele é comparável ao nome recebido
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == ProgramaRequisitado)
                        {
                            //Verifica se o campo nome está vazio, se não estiver continua executando
                            if (reader.GetAttribute(0) != "")
                            {
                                //Preenche os dados no vetor criado posteriormente e prepara para retornar
                                //Nome
                                programa[0] = reader.GetAttribute(0);
                                //Caminho
                                programa[1] = reader.GetAttribute(1);
                                //Parâmetros
                                programa[2] = reader.GetAttribute(2);
                                //Status
                                programa[3] = reader.GetAttribute(3);
                            }
                            //Caso estiver vazio
                            else
                            {
                                //Manda um texto para verificação
                                programa[0] = "False";
                            }
                        } //end if
                    } //end while
                    //Fecha o arquivo carregado
                    reader.Close();
                }
            }
            else
            {
                //Mostra que o programa não existe
                programa[0] = "Programa Inválido";
            }
            
            //Retorna o velor de string
            return programa;
        }

        public string[] InformacoesProgramasUser(string ProgramaRequisitado)
        {
            //Cria um vetor de string com 4 linhas
            string[] programa = new string[4];
            //Caminho ao qual o software irá procurar o XML
            string curFile = "";
            string fileOpen = "";

            bool programaExiste = true;

            switch (ProgramaRequisitado)
            {
                case "Desligar":
                    curFile = @"c:\TiagoSM\programaUserDesligar.xml";
                    fileOpen = "programaUserDesligar.xml";
                    break;
                case "Reiniciar":
                    curFile = @"c:\TiagoSM\programaUserReiniciar.xml";
                    fileOpen = "programaUserReiniciar.xml";
                    break;
                case "User1":
                    curFile = @"c:\TiagoSM\programaUser1.xml";
                    fileOpen = "programaUser1.xml";
                    break;
                case "User2":
                    curFile = @"c:\TiagoSM\programaUser2.xml";
                    fileOpen = "programaUser2.xml";
                    break;
                case "User3":
                    curFile = @"c:\TiagoSM\programaUser3.xml";
                    fileOpen = "programaUser3.xml";
                    break;
                case "User4":
                    curFile = @"c:\TiagoSM\programaUser4.xml";
                    fileOpen = "programaUser4.xml";
                    break;
                default:
                    programaExiste = false;
                    break;
            }
            //Verifica se o nome digitado na função existe
            if (programaExiste)
            {
                //Verifica se o XML Existe
                if (File.Exists(curFile))
                {
                    //Se sim, ele irá carregar o arquivo
                    XmlReader reader = XmlReader.Create(curFile);

                    //Enquanto estiver executando a leitura
                    while (reader.Read())
                    {
                        //Verifica se o tipo de nodulo é um Element e se o nome dele é comparável ao nome recebido
                        if (reader.NodeType == XmlNodeType.Element && reader.Name == ProgramaRequisitado)
                        {
                            //Verifica se o campo nome está vazio, se não estiver continua executando
                            if (reader.GetAttribute(0) != "")
                            {
                                //Preenche os dados no vetor criado posteriormente e prepara para retornar
                                //Nome
                                programa[0] = reader.GetAttribute(0);
                                //Caminho
                                programa[1] = reader.GetAttribute(1);
                                //Parâmetros
                                programa[2] = reader.GetAttribute(2);
                                //Status
                                programa[3] = reader.GetAttribute(3);
                            }
                            //Caso estiver vazio
                            else
                            {
                                //Manda um texto para verificação
                                programa[0] = "False";
                            }
                        } //end if
                    } //end while
                    //Fecha o arquivo carregado
                    reader.Close();
                }
            }
            else
            {
                //Mostra que o programa não existe
                programa[0] = "Programa Inválido";
            }
            //Retorna o velor de string
            return programa;
        }

        public void AlteraInformacoesProgramasADM(string Programa, string nome, string caminho, string comando)
        {
            //Caminho ao qual o software irá procurar o XML
            string curFile = "";
            string fileOpen = "";

            switch (Programa)
            {
               case "ADM1":
                    curFile = @"c:\TiagoSM\programaAdm1.xml";
                    fileOpen = "programaAdm1.xml";
                    break;
                case "ADM2":
                    curFile = @"c:\TiagoSM\programaAdm2.xml";
                    fileOpen = "programaAdm2.xml";
                    break;
                case "ADM3":
                    curFile = @"c:\TiagoSM\programaAdm3.xml";
                    fileOpen = "programaAdm3.xml";
                    break;
                case "ADM4":
                    curFile = @"c:\TiagoSM\programaAdm4.xml";
                    fileOpen = "programaAdm4.xml";
                    break;
            }

            //Verifica se o XML Existe
            if (File.Exists(curFile))
            {
                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements(Programa) select c;

                foreach (XElement dados in query)
                {
                    dados.Attribute("Nome").Value = nome;
                    dados.Attribute("Caminho").Value = caminho;
                    dados.Attribute("Comando").Value = comando;
                    dados.Attribute("Habilitado").Value = nome == "" ? "yes" : "no";
                }

                xmlFile.Save(curFile);
                MessageBox.Show("Botão " + Programa.Substring(0, 3) + " " + Programa.Substring(3, 1) + " alterado com sucesso!", "Alteração do botão " + Programa.Substring(0, 3) + " " + Programa.Substring(3, 1), MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        public void AlteraInformacoesProgramasUser(string Programa, string nome, string caminho, string comando)
        {
            //Caminho ao qual o software irá procurar o XML
            string curFile = "";
            string fileOpen = "";

            switch (Programa)
            {
                case "User1":
                    curFile = @"c:\TiagoSM\programaUser1.xml";
                    fileOpen = "programaUser1.xml";
                    break;
                case "User2":
                    curFile = @"c:\TiagoSM\programaUser2.xml";
                    fileOpen = "programaUser2.xml";
                    break;
                case "User3":
                    curFile = @"c:\TiagoSM\programaUser3.xml";
                    fileOpen = "programaUser3.xml";
                    break;
                case "User4":
                    curFile = @"c:\TiagoSM\programaUser4.xml";
                    fileOpen = "programaUser4.xml";
                    break;
            }

            //Verifica se o XML Existe
            if (File.Exists(curFile))
            {
                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements(Programa) select c;

                foreach (XElement dados in query)
                {
                    dados.Attribute("Nome").Value = nome;
                    dados.Attribute("Caminho").Value = caminho;
                    dados.Attribute("Comando").Value = comando;
                    dados.Attribute("Habilitado").Value = nome == "" ? "yes" : "no";
                }

                xmlFile.Save(curFile);
                MessageBox.Show("Botão " + Programa.Substring(0, 4) + " " + Programa.Substring(4, 1) + " alterado com sucesso!", "Alteração do botão " + Programa.Substring(0, 4) + " " + Programa.Substring(4, 1), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void RemoveInformacoesProgramasADM(string Programa)
        {
            //Caminho ao qual o software irá procurar o XML
            string curFile = "";
            string fileOpen = "";

            switch (Programa)
            {
                case "ADM1":
                    curFile = @"c:\TiagoSM\programaAdm1.xml";
                    fileOpen = "programaAdm1.xml";
                    break;
                case "ADM2":
                    curFile = @"c:\TiagoSM\programaAdm2.xml";
                    fileOpen = "programaAdm2.xml";
                    break;
                case "ADM3":
                    curFile = @"c:\TiagoSM\programaAdm3.xml";
                    fileOpen = "programaAdm3.xml";
                    break;
                case "ADM4":
                    curFile = @"c:\TiagoSM\programaAdm4.xml";
                    fileOpen = "programaAdm4.xml";
                    break;
            }

            //Verifica se o XML Existe
            if (File.Exists(curFile))
            {
                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements(Programa) select c;

                foreach (XElement dados in query)
                {
                    dados.Attribute("Nome").Value = "";
                    dados.Attribute("Caminho").Value = "";
                    dados.Attribute("Comando").Value = "";
                    dados.Attribute("Habilitado").Value = "no";
                }

                xmlFile.Save(curFile);
                MessageBox.Show("Botão " + Programa.Substring(0, 3) + " " + Programa.Substring(3, 1) + " alterado com sucesso!", "Alteração do botão " + Programa.Substring(0, 3) + " " + Programa.Substring(3, 1), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void RemoveInformacoesProgramasUser(string Programa)
        {
            //Caminho ao qual o software irá procurar o XML
            string curFile = "";
            string fileOpen = "";

            switch (Programa)
            {
                case "User1":
                    curFile = @"c:\TiagoSM\programaUser1.xml";
                    fileOpen = "programaUser1.xml";
                    break;
                case "User2":
                    curFile = @"c:\TiagoSM\programaUser2.xml";
                    fileOpen = "programaUser2.xml";
                    break;
                case "User3":
                    curFile = @"c:\TiagoSM\programaUser3.xml";
                    fileOpen = "programaUser3.xml";
                    break;
                case "User4":
                    curFile = @"c:\TiagoSM\programaUser4.xml";
                    fileOpen = "programaUser4.xml";
                    break;
            }

            //Verifica se o XML Existe
            if (File.Exists(curFile))
            {
                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements(Programa) select c;

                foreach (XElement dados in query)
                {
                    dados.Attribute("Nome").Value = "";
                    dados.Attribute("Caminho").Value = "";
                    dados.Attribute("Comando").Value = "";
                    dados.Attribute("Habilitado").Value = "no";
                }

                xmlFile.Save(curFile);
                MessageBox.Show("Botão " + Programa.Substring(0, 4) + " " + Programa.Substring(4, 1) + " alterado com sucesso!", "Alteração do botão " + Programa.Substring(0, 4) + " " + Programa.Substring(4, 1), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CriaXMLPapelParedePrograma()
        {
            //Cria o documento XML como banco de dados
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create("papelParede.xml", settings);
            writer.WriteStartDocument();
            writer.WriteComment("Arquivo gerado pelo programa TiagoDesktop.");
            writer.WriteStartElement("PapelParede");
            writer.WriteAttributeString("Caminho", "");
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

            FileInfo destino = new FileInfo(@"C:\TiagoSM\papelParede.xml");
            FileInfo atual = new FileInfo(Path.GetDirectoryName(Application.ExecutablePath) + "\\papelParede.xml");
            atual.Attributes = FileAttributes.Normal;
            File.Copy(atual.FullName, destino.FullName, true);
        }

        public string InformacaoPapelParede()
        {
            string curFile = @"c:\TiagoSM\papelParede.xml";
            string diretorio = "";

            //Verifica se o XML Existe
            if (File.Exists(curFile))
            {
                //Se sim, ele irá carregar o arquivo
                XmlReader reader = XmlReader.Create(curFile);

                //Enquanto estiver executando a leitura
                while (reader.Read())
                {
                    //Verifica se o tipo de nodulo é um Element e se o nome dele é comparável ao nome recebido
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "PapelParede")
                    {
                        //Verifica se o campo diretorio está vazio, se não estiver continua executando
                        if (reader.GetAttribute(0) != "")
                        {
                            diretorio = reader.GetAttribute(0);
                        }
                        //Caso estiver vazio
                        else
                        {
                            //Manda um texto para verificação
                            diretorio = "False";
                        }
                    } //end if
                } //end while
                //Fecha o arquivo carregado
                reader.Close();
            }
            else
            {
                CriaXMLPapelParedePrograma();
            }
          

            //Retorna o velor de string
            return diretorio;
        }

        public void AlteraPapelParede(string caminho)
        {
            string curFile = @"c:\TiagoSM\papelParede.xml";

            //Verifica se o XML Existe
            if (File.Exists(curFile))
            {
                XDocument xmlFile = XDocument.Load(curFile);

                var query = from c in xmlFile.Elements("PapelParede") select c;

                foreach (XElement dados in query)
                {
                    dados.Attribute("Caminho").Value = caminho;
                }

                xmlFile.Save(curFile);
                MessageBox.Show("Papel de parede alterado com sucesso!", "Alteração do Papel de Parede", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}
