using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Office.Core;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using System.IO;
namespace Picachu_BG_PRO {
    public partial class MainForm : Form {
        PowerPoint.Application oPPT;
        PowerPoint.Presentations objPresSet;
        PowerPoint.Presentation objPres;
        public MainForm() {
            InitializeComponent();
        }
        public void runSingle() {
            using (Single game = new Single()) {
                game.Run();
            }
        }
        private void MainForm_Load(object sender, EventArgs e) {

        }

        private void button6_Click(object sender, EventArgs e) {
            System.Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e) {
            ThreadStart thrStart = new ThreadStart(runSingle);
            Thread thr = new Thread(thrStart);
            thr.Start();
        }

        private void button3_Click(object sender, EventArgs e) {
            new MultiForm().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e) {
            new optionForm().ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e) {
            oPPT = new PowerPoint.ApplicationClass();
            oPPT.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            objPresSet = oPPT.Presentations;
            objPres = objPresSet.Open(Directory.GetCurrentDirectory().ToString() + @"\credit.pps", MsoTriState.msoTrue,
MsoTriState.msoTrue, MsoTriState.msoTrue);
        }

        private void button2_Click(object sender, EventArgs e) {
            oPPT = new PowerPoint.ApplicationClass();
            oPPT.Visible = Microsoft.Office.Core.MsoTriState.msoTrue;
            objPresSet = oPPT.Presentations;
            objPres = objPresSet.Open(Directory.GetCurrentDirectory().ToString() + @"\help.pps", MsoTriState.msoTrue,
MsoTriState.msoTrue, MsoTriState.msoTrue);
        }
    }
}
