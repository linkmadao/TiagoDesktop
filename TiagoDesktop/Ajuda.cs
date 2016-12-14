using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace TiagoDesktop
{
    public partial class Ajuda : Form
    {
        public Ajuda()
        {
            InitializeComponent();
        }

        private void Ajuda_Load(object sender, EventArgs e)
        {
            //Verifica se o Teamviewer existe
            if (File.Exists(@"C:\Program Files\TeamViewer\Version7\TeamViewer.exe") || File.Exists(@"C:\Program Files (x86)\TeamViewer\Version7\TeamViewer.exe") ||
                File.Exists(@"C:\Program Files\TeamViewer\TeamViewer.exe") || File.Exists(@"C:\Program Files (x86)\TeamViewer\TeamViewer.exe"))
            {
                if (RetornaIDTeamviewer() == null)
                {
                    lblTeamviewerID.Text = "Erro ao pegar o id!";
                }
                else
                {
                    lblTeamviewerID.Text = RetornaIDTeamviewer();
                }

                Size = new Size(Size.Width, 270);
            }
            else
            {
                lblTeamviewerID.Visible = false;
                lblTeamviewerTitulo.Visible = false;

                Size = new Size(Size.Width, 236);
            }

            //Pega a data da build
            DateTime buildDate =  new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;

            //E mostra nesse campo
            lblDataVersao.Text = buildDate.ToString();
            lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private string RetornaIDTeamviewer()
        {
            string id = null;

            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\TeamViewer"))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue("ClientID");
                        if (o != null)
                        {
                            id = o.ToString();
                        }
                    }
                }
                if (id == null)
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\TeamViewer"))
                    {
                        if (key != null)
                        {
                            Object o = key.GetValue("ClientID");
                            if (o != null)
                            {
                                id = o.ToString();
                            }
                        }
                    }
                }
                else if (id == null)
                {    
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\TeamViewer\\Version"))
                    {
                        if (key != null)
                        {
                            Object o = key.GetValue("ClientID");
                            if (o != null)
                            {
                                id = o.ToString();
                            }
                        }
                    }
                    
                }
                else if (id == null)
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\TeamViewer\\Version"))
                    {
                        if (key != null)
                        {
                            Object o = key.GetValue("ClientID");
                            if (o != null)
                            {
                                id = o.ToString();
                            }
                        }
                    }

                }
                else if (id == null)
                {
                    var versions = new[] { "4", "5", "5.1", "6", "7", "8", "9" }.Reverse().ToList(); //Reverse to get ClientID of newer version if possible

                    foreach (var version in versions)
                    {
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\TeamViewer\\Version{1}"))
                        {
                            if (key != null)
                            {
                                Object o = key.GetValue("ClientID");
                                if (o != null)
                                {
                                    id = o.ToString();
                                }
                            }
                        }
                    }
                }
                else if (id == null)
                {
                    var versions = new[] { "4", "5", "5.1", "6", "7", "8", "9" }.Reverse().ToList(); //Reverse to get ClientID of newer version if possible

                    foreach (var version in versions)
                    {
                        using (RegistryKey key = Registry.LocalMachine.OpenSubKey("Software\\TeamViewer\\Version{1}"))
                        {
                            if (key != null)
                            {
                                Object o = key.GetValue("ClientID");
                                if (o != null)
                                {
                                    id = o.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)  //just for demonstration...it's always best to handle specific exceptions
            {
                //react appropriately
            }

            return id;
        }

        private void Ajuda_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Diz que pode abrir a tela de ajuda novamente
            TiagoDesktop.telaAjudaAberta = false;
        }
    }
}
