using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Picachu_BG_PRO {

    class Move {
        public int x1, y1, x2, y2;
        public Move(int _x1, int _y1, int _x2, int _y2) {
            x1 = _x1;
            y1 = _y1;
            x2 = _x2;
            y2 = _y2;
        }
        public Move(StreamReader reader) {
            int.TryParse(reader.ReadLine(), out x1);
            int.TryParse(reader.ReadLine(), out y1);
            int.TryParse(reader.ReadLine(), out x2);
            int.TryParse(reader.ReadLine(), out y2);
        }
        public void writeMove(StreamWriter writer) {
            writer.WriteLine(x1);
            writer.WriteLine(y1);
            writer.WriteLine(x2);
            writer.WriteLine(y2);
        }
    }

    public class ReadingThread {
        StreamReader reader;
        StreamWriter writer;
        MapMulti map;
        Multi game;
        public bool isActive;
        public Thread thread;
        public ReadingThread(StreamReader _reader, StreamWriter _writer, MapMulti _map, Multi _game) {
            game = _game;
            reader = _reader;
            map = _map;
            writer = _writer;
            isActive = true;
            ThreadStart threadStart = new ThreadStart(run);
            thread = new Thread(threadStart);
            thread.Start();
        }
        public void run() {
            try {
                while (isActive) {
                    string command = reader.ReadLine();
                    if (command == "Move") {
                        Move move = new Move(reader);
                        map.arr[move.x1, move.y1].index = -1;
                        map.arr[move.x2, move.y2].index = -1;

                        if ((int)map.SelectedItem.X == move.x1 && (int)map.SelectedItem.Y == move.y1)
                            map.SetSelectedItem(-1, -1);
                        if ((int)map.SelectedItem.X == move.x2 && (int)map.SelectedItem.Y == move.y2)
                            map.SetSelectedItem(-1, -1);

                        game.opnScore++;
                        map.available -= 2;

                        if (game.isServer && map.available > 0) {
                            if (!map.HasMoreTurns()) {
                                do {
                                    map.Shuffle();
                                } while (!map.HasMoreTurns());
                                writer.WriteLine("Map");
                                map.upMap(writer);
                            }
                        }

                    }
                    if (command == "Map") {
                        map.readMap(reader, writer);
                    }
                    if (command == "Quit") {
                        isActive = false;
                        game.gameEnd = true;
                        System.Windows.Forms.MessageBox.Show("Your opponent resigned !!!");
                        //game.closeConnection();
                        game.Exit();
                    }
                }
            } catch {
            }
        }
    }
}
