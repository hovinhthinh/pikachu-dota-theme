using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace Picachu_BG_PRO {
    public partial class newGameForm : Form {
        public int mapX, mapY;
        public string gameName;
        public string gameSize;

        public newGameForm() {
            InitializeComponent();
        }
        Thread thr;
        Thread thrServer;
        TcpListener server;
        Socket sck = null;
        Stream stream;
        public void broadcast() {
            while (true) {
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                    ProtocolType.Udp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 1008);

                string hostname = "12091008 " + gameName + "_" + gameSize;
                byte[] data = Encoding.ASCII.GetBytes(hostname);
                sock.SetSocketOption(SocketOptionLevel.Socket,
                           SocketOptionName.Broadcast, 1);

                sock.SendTo(data, iep);
                sock.Close();
                Thread.Sleep(1000);
            }
        }
        public void initServer() {
            try {
                server = new TcpListener(1209);
                Console.WriteLine("Listening...");
                server.Start();
                sck = server.AcceptSocket();
                Console.WriteLine("Server connected...");
                thr.Abort();
                stream = new NetworkStream(sck);
                lbStatus.Text = "Connected";
                btStart.Enabled = true;
            } catch {
            }
        }
        private void newGameForm_Load(object sender, EventArgs e) {
            ThreadStart thrStart = new ThreadStart(broadcast);
            thr = new Thread(thrStart);
            btStart.Enabled = false;
            thr.Start();
            thrServer = new Thread(new ThreadStart(initServer));
            thrServer.Start();
        }

        private void newGameForm_FormClosing(object sender, FormClosingEventArgs e) {
            try {
                thr.Abort();
                server.Stop();
            } catch {
            }

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                                ProtocolType.Udp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 1008);

            string hostname = "12091008Remove " + gameName + "_" + gameSize;
            byte[] data = Encoding.ASCII.GetBytes(hostname);
            sock.SetSocketOption(SocketOptionLevel.Socket,
                       SocketOptionName.Broadcast, 1);

            sock.SendTo(data, iep);
            sock.Close();
        }
        Thread thrGame;
        private void btStart_Click(object sender, EventArgs e) {
            if (gameSize == "16_x_12") {
                mapX = 16;
                mapY = 12;
            }
            if (gameSize == "14_x_11") {
                mapX = 14;
                mapY = 11;
            }
            if (gameSize == "11_x_10") {
                mapX = 11;
                mapY = 10;
            }
            if (gameSize == "10_x_7") {
                mapX = 10;
                mapY = 7;
            }
            if (gameSize == "6_x_5") {
                mapX = 6;
                mapY = 5;
            }

            this.Close();
            thrGame = new Thread(new ThreadStart(delegate() {
                using (Multi game = new Multi(stream, true)) {
                    game.mapX = mapX;
                    game.mapY = mapY;
                    game.Run();
                }
            }));
            thrGame.Start();
        }
    }
}
