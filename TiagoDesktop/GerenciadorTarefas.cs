using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TiagoDesktop
{
    public partial class GerenciadorTarefas : Form
    {
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

        private Process[] listaProcessosAntiga, runningProcesses;
        
        DataGridView dgvListaInicial = new DataGridView();

        public GerenciadorTarefas(Process[] lista)
        {
            try
            {
                InitializeComponent();
                dgvListaInicial = dgvProcessos;
                listaProcessosAntiga = lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dgvProcessos = UpdateProcessList();
            }
        }
               
        #region Método Novo
        private DataGridView UpdateProcessList()
        {
            DataGridView dgvAtualizada = dgvProcessos;
            dgvAtualizada.Rows.Clear();

            ProcessDiff diferenciaProcessos = new ProcessDiff();

            runningProcesses = Process.GetProcesses();
            int currentSessionID = Process.GetCurrentProcess().SessionId;

            Process[] sameAsthisSession = (from c in runningProcesses where c.SessionId == currentSessionID select c).ToArray();

            IEnumerable<Process> novaLista = diferenciaProcessos.GetDiff(listaProcessosAntiga, sameAsthisSession);

            foreach (Process p in novaLista)
            {
                double memsize = 0; // memsize in Megabyte

                PerformanceCounter total_cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                PerformanceCounter myAppCpu = new PerformanceCounter("Process", "% Processor Time", p.ProcessName, true);
                memsize = Math.Round((Convert.ToDouble(p.PrivateMemorySize64) / 1024 / 1024), 1);
                if (memsize > 1000)
                {
                    memsize = Math.Round(memsize / 1024, 1);
                    dgvAtualizada.Rows.Add(p.ProcessName, myAppCpu.NextValue().ToString() + " %", memsize.ToString() + " GB");
                }
                else
                {
                    try
                    {
                        dgvAtualizada.Rows.Add(p.ProcessName, myAppCpu.NextValue().ToString() + " %", memsize.ToString() + " MB");
                    }
                    catch (InvalidOperationException)
                    {
                        throw;
                    }
                }
            }

            return dgvAtualizada;
        }

        private void contador_Tick(object sender, EventArgs e)
        {
            if(dgvProcessos.RowCount != UpdateProcessList().RowCount)
            {
                dgvProcessos.Rows.Clear();
                dgvProcessos = UpdateProcessList();
            }
            else
            {
                DataGridView dgvAtualizada = UpdateProcessList();

                for(int i = 0; i < (dgvProcessos.RowCount - 1); i++)
                {
                    if(dgvProcessos.Rows[i].Cells[1].Value.ToString() != dgvAtualizada.Rows[i].Cells[1].Value.ToString())
                    {
                        dgvProcessos.Rows[i].Cells[1].Value = dgvAtualizada.Rows[i].Cells[1].Value;
                    }
                    if(dgvProcessos.Rows[i].Cells[2].Value.ToString() != dgvAtualizada.Rows[i].Cells[2].Value.ToString())
                    {
                        dgvProcessos.Rows[i].Cells[2].Value = dgvAtualizada.Rows[i].Cells[2].Value;
                    }
                }
            }
        }
        #endregion

        private void dgvProcessos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Linha que o usuário clicou
            int rowindex = dgvProcessos.CurrentCell.RowIndex;
            //Coluna que o usuário clicou
            int columnindex = dgvProcessos.CurrentCell.ColumnIndex;

            string process = dgvProcessos.Rows[rowindex].Cells[columnindex].Value.ToString();

            if (Process.GetProcessesByName(process).Any())
            {
                ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_SHOWNORMAL);
                ShowWindow(Process.GetProcessesByName(process).First().MainWindowHandle, SW_RESTORE);
                SetForegroundWindow(Process.GetProcessesByName(process).First().MainWindowHandle);
            }
        }
    }
}
