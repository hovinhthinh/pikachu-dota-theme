using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Picachu_BG_PRO {
    public partial class optionForm : Form {
        public optionForm() {
            InitializeComponent();
        }
        public void write() {
            StreamWriter writer = new StreamWriter("config.dat");
            writer.WriteLine("[Sound Effects]");
            writer.WriteLine(radioButton1.Checked ? 1 : 0);
            writer.WriteLine("[Music]");
            writer.WriteLine(radioButton3.Checked ? 1 : 0);
            writer.WriteLine("[Path Cover]");
            writer.WriteLine(radioButton5.Checked ? 1 : 0);
            writer.Close();
            reload();
        }
        public void reload() {
            if (radioButton1.Checked) Option.soundEffect = true; else Option.soundEffect = false;
            if (radioButton3.Checked) Option.music = true; else Option.music = false;
            if (radioButton5.Checked) Option.pathCover = true; else Option.pathCover = false;
        }
        private void optionForm_Load(object sender, EventArgs e) {
            StreamReader reader = new StreamReader("config.dat");
            int o1, o2, o3;
            reader.ReadLine();
            string s = reader.ReadLine();
            int.TryParse(s, out o1);
            reader.ReadLine();
            s = reader.ReadLine();
            int.TryParse(s, out o2);
            reader.ReadLine();
            s = reader.ReadLine();
            int.TryParse(s, out o3);
            reader.Close();
            Console.WriteLine(o1 + " " + o2 + " " + o3);
            if (o1 == 1) radioButton1.Checked = true; else radioButton2.Checked = true;
            if (o2 == 1) radioButton3.Checked = true; else radioButton4.Checked = true;
            if (o3 == 1) radioButton5.Checked = true; else radioButton6.Checked = true;

            reload();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) {
            write();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) {
            write();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e) {
            write();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e) {
            write();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e) {
            write();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e) {
            write();
        }
    }
}
