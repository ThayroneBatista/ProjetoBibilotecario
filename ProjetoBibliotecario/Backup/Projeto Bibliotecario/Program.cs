using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Projeto_Bibliotecario
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// Created by Thayrone Batista
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new frmLivro());
        }
    }
}