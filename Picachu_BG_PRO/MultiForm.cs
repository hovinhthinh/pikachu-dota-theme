using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Net.Sockets;
namespace Picachu_BG_PRO {
    public partial class MultiForm : Form {
        public MultiForm() {
            InitializeComponent();
        }
        List<string> dataSource;
        Thread thr;
        Socket sock;
        public void receive() {
            while (true) {
                sock = new Socket(AddressFamily.InterNetwork,
                SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 1008);
                sock.Bind(iep);
                EndPoint ep = (EndPoint)iep;
                byte[] data = new byte[1024];
                int recv = sock.ReceiveFrom(data, ref ep);
                string stringData = Encoding.ASCII.GetString(data, 0, recv);
                if (stringData.IndexOf("12091008Remove") == 0) {
                    string hostName = stringData.Split(' ')[1];
                    string ipAddress = ep.ToString().Split(':')[0];
                    dataSource.Remove(hostName + " " + ipAddress);

                } else
                    if (stringData.IndexOf("12091008") == 0) {
                        string hostName = stringData.Split(' ')[1];
                        string ipAddress = ep.ToString().Split(':')[0];
                        bool existed = false;
                        foreach (string s in dataSource) {
                            //Console.WriteLine(s);
                            if (s == hostName + " " + ipAddress) {
                                existed = true;
                                break;
                            }
                        }
                        if (!existed) {
                            dataSource.Add(hostName + " " + ipAddress);
                            //Console.WriteLine(hostName + " " + ipAddress);
                        }
                    }
                sock.Close();
                listGame.DataSource = null;
                listGame.DataSource = dataSource;

            }
        }
        private void MultiForm_Load(object sender, EventArgs e) {
            dataSource = new List<string>();


            tbName.Text = System.Environment.MachineName;
            ThreadStart thrStart = new ThreadStart(receive);
            thr = new Thread(thrStart);
            thr.Start();
        }

        private void btNewGame_Click(object sender, EventArgs e) {
            newGameForm newForm = new newGameForm();
            newForm.gameName = tbName.Text.Replace(" ", "_");
            if (sz0.Checked) newForm.gameSize = "16_x_12";
            if (sz1.Checked) newForm.gameSize = "14_x_11";
            if (sz2.Checked) newForm.gameSize = "11_x_10";
            if (sz3.Checked) newForm.gameSize = "10_x_7";
            if (sz4.Checked) newForm.gameSize = "6_x_5";
            newForm.ShowDialog();
        }

        private void MultiForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                thr.Abort();
                sock.Close();
            } catch {
            }
        }
        Thread thrGame;
        private void btJoinGame_Click(object sender, EventArgs e) {
            if (listGame.SelectedIndex != -1) {
                string host = ((string)listGame.SelectedItem).Split(' ')[1];
                TcpClient client = new TcpClient(host, 1209);
                Console.WriteLine("Client connected");
                Stream stream = client.GetStream();

                readyForm newForm = new readyForm();

                thrGame = new Thread(new ThreadStart(delegate() {
                    using (Multi game = new Multi(stream, false)) {
                        game.Run();
                    }
                    newForm.Close();
                }));
                thrGame.Start();

                newForm.ShowDialog();
            }
        }
    }
}
