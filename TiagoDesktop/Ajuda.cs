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
            //Pega a data da build
            DateTime buildDate =  new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;

            //E mostra nesse campo
            lblDataVersao.Text = buildDate.ToString();
            lblVersao.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            
        }
    }
}
