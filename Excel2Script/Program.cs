using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace FTBExcel2Script
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] argv)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter("FTBExcel2Script_Run.log", false);
            }
            catch
            {

            }
            if (sw != null)
            {
                Trace.Listeners.Add(new TextWriterTraceListener(sw));
            }
                
            Trace.AutoFlush = true;

            if (argv.Length > 0)
            {

            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
        }
    }
}
