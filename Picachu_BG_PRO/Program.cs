using System;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
namespace Picachu_BG_PRO {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}

