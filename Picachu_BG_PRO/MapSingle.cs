using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Picachu_BG_PRO {

    public class MapSingle {
        public HeroSingle[,] arr;
        public int available;
        public int x, y;
        public Vector2 SelectedItem;
        Vector2 Position;

        public int[,] Dist;
        public Vector2[,] Trace;
        public bool hint = false;
        public Vector2 hint1, hint2;
        public void Bfs(int x1, int y1) {
            Queue<int> Qx = new Queue<int>(), Qy = new Queue<int>();
            for (int i = 0; i <= x + 1; i++) for (int j = 0; j <= y + 1; j++) {
                    Dist[i, j] = (int)1e9;
                    Trace[i, j] = new Vector2(-1, -1);
                }
            Dist[x1, y1] = 0;
            Qx.Enqueue(x1); Qy.Enqueue(y1);
            while (Qx.Count > 0) {
                int u = Qx.Dequeue();
                int v = Qy.Dequeue();
                if (Dist[u, v] == 3) break;
                for (int i = v - 1; i >= 0; i--) {
                    if (Dist[u, i] == (int)1e9) {
                        Dist[u, i] = Dist[u, v] + 1;
                        Trace[u, i] = new Vector2(u, v);
                        if (arr[u, i].index == -1) {
                            Qx.Enqueue(u);
                            Qy.Enqueue(i);
                        }
                    }
                    if (arr[u, i].index != -1) break;
                }
                for (int i = v + 1; i <= y + 1; i++) {
                    if (Dist[u, i] == (int)1e9) {
                        Dist[u, i] = Dist[u, v] + 1;
                        Trace[u, i] = new Vector2(u, v);
                        if (arr[u, i].index == -1) {
                            Qx.Enqueue(u);
                            Qy.Enqueue(i);

                        }
                    }
                    if (arr[u, i].index != -1) break;
                } for (int i = u - 1; i >= 0; i--) {
                    if (Dist[i, v] == (int)1e9) {
                        Dist[i, v] = Dist[u, v] + 1;
                        Trace[i, v] = new Vector2(u, v);
                        if (arr[i, v].index == -1) {
                            Qx.Enqueue(i);
                            Qy.Enqueue(v);
                        }
                    }
                    if (arr[i, v].index != -1) break;
                }
                for (int i = u + 1; i <= x + 1; i++) {
                    if (Dist[i, v] == (int)1e9) {
                        Dist[i, v] = Dist[u, v] + 1;
                        Trace[i, v] = new Vector2(u, v);
                        if (arr[i, v].index == -1) {
                            Qx.Enqueue(i);
                            Qy.Enqueue(v);
                        }
                    }
                    if (arr[i, v].index != -1) break;
                }
            }
        }
        private bool HasMoreTurns(int x1, int y1) {
            Queue<int> Qx = new Queue<int>(), Qy = new Queue<int>();
            for (int i = 0; i <= x + 1; i++) for (int j = 0; j <= y + 1; j++) Dist[i, j] = (int)1e9;
            Dist[x1, y1] = 0;
            Qx.Enqueue(x1); Qy.Enqueue(y1);
            while (Qx.Count > 0) {
                int u = Qx.Dequeue();
                int v = Qy.Dequeue();
                if (Dist[u, v] == 3) break;
                for (int i = v - 1; i >= 0; i--) {
                    if (Dist[u, i] == (int)1e9) {
                        Dist[u, i] = Dist[u, v] + 1;
                        if (arr[u, i].index == -1) {
                            Qx.Enqueue(u);
                            Qy.Enqueue(i);
                        }
                    }
                    if (arr[u, i].index != -1) {
                        if (arr[u, i].index == arr[x1, y1].index && (u != x1 || i != y1)) {
                            hint1 = new Vector2(x1, y1);
                            hint2 = new Vector2(u, i);
                            return true;
                        }
                        break;
                    }
                }
                for (int i = v + 1; i <= y + 1; i++) {
                    if (Dist[u, i] == (int)1e9) {
                        Dist[u, i] = Dist[u, v] + 1;
                        if (arr[u, i].index == -1) {
                            Qx.Enqueue(u);
                            Qy.Enqueue(i);
                        }
                    }
                    if (arr[u, i].index != -1) {
                        if (arr[u, i].index == arr[x1, y1].index && (u != x1 || i != y1)) {
                            hint1 = new Vector2(x1, y1);
                            hint2 = new Vector2(u, i);
                            return true;
                        }
                        break;
                    }

                }
                for (int i = u - 1; i >= 0; i--) {
                    if (Dist[i, v] == (int)1e9) {
                        Dist[i, v] = Dist[u, v] + 1;
                        if (arr[i, v].index == -1) {
                            Qx.Enqueue(i);
                            Qy.Enqueue(v);
                        }
                    }
                    if (arr[i, v].index != -1) {
                        if (arr[i, v].index == arr[x1, y1].index && (i != x1 || v != y1)) {
                            hint1 = new Vector2(x1, y1);
                            hint2 = new Vector2(i, v);
                            return true;
                        }
                        break;
                    }

                }
                for (int i = u + 1; i <= x + 1; i++) {
                    if (Dist[i, v] == (int)1e9) {
                        Dist[i, v] = Dist[u, v] + 1;
                        if (arr[i, v].index == -1) {
                            Qx.Enqueue(i);
                            Qy.Enqueue(v);
                        }
                    }
                    if (arr[i, v].index != -1) {
                        if (arr[i, v].index == arr[x1, y1].index && (i != x1 || v != y1)) {
                            hint1 = new Vector2(x1, y1);
                            hint2 = new Vector2(i, v);
                            return true;
                        }
                        break;
                    }

                }
            }
            return false;
        }
        public bool HasMoreTurns() {
            for (int i = 1; i <= x; i++)
                for (int j = 1; j <= y; j++) {
                    if (arr[i, j].index != -1)
                        if (HasMoreTurns(i, j)) {
                            return true;
                        }
                }
            return false;
        }
        public void Shuffle() {
            HeroSingle[] lis = new HeroSingle[available];
            HeroSingle[] lisShuffle = new HeroSingle[available];
            int t = 0;
            for (int i = 1; i <= x; i++)
                for (int j = 1; j <= y; j++) {
                    if (arr[i, j].index != -1)
                        lis[t++] = arr[i, j];
                }
            Random random = new Random(System.Environment.TickCount);
            for (int i = 0; i < available; i++) {
                int temp = random.Next(available - i) + 1;
                for (int j = 0; j < available; j++) if (lis[j] != null) {
                        temp--;
                        if (temp == 0) {
                            lisShuffle[i] = lis[j];
                            lis[j] = null;
                            break;
                        }
                    }
            }
            t = 0;
            for (int i = 1; i <= x; i++)
                for (int j = 1; j <= y; j++) {
                    if (arr[i, j].index != -1) {
                        arr[i, j] = lisShuffle[t++];
                        arr[i, j].Position = Position + new Vector2(i * arr[i, j].Texture.Width * arr[i, j].Scale, j * arr[i, j].Texture.Height * arr[i, j].Scale);
                    }
                }
        }


        public bool IsConnected(int x1, int y1, int x2, int y2) {
            Bfs(x1, y1);
            return Dist[x2, y2] <= 3;
        }
        public MapSingle(int _x, int _y) {
            Random random = new Random(System.Environment.TickCount);
            SelectedItem = new Vector2(-1, -1);

            x = _x; y = _y;
            arr = new HeroSingle[x + 2, y + 2];
            Dist = new int[x + 2, y + 2];
            Trace = new Vector2[x + 2, y + 2];

            int num = x * y;
            available = num;
            for (int i = 0; i <= x + 1; i++)
                for (int j = 0; j <= y + 1; j++) {
                    if (i == 0 || j == 0 || i == x + 1 || j == y + 1) {
                        arr[i, j] = new HeroSingle(-1);
                    } else
                        if (arr[i, j] == null) {
                            int t = random.Next(num - 1) + 1;
                            int u = 0, v = 0;
                            for (u = i; u <= x; u++) {
                                bool found = false;
                                for (v = (u == i ? j + 1 : 1); v <= y; v++) {
                                    if (arr[u, v] == null) t--;
                                    if (t == 0) {
                                        found = true;
                                        break;
                                    }
                                }
                                if (found) break;
                            }
                            num -= 2;
                            t = random.Next(36);
                            arr[i, j] = new HeroSingle(t);
                            arr[u, v] = new HeroSingle(t);
                        }
                }
            float Scale = (float)SharedValuesSingle.WindowHeight / SharedValuesSingle.Background.Height;
            SetPosition(new Vector2((Scale * SharedValuesSingle.Background.Width - (x + 2) * (arr[1, 1].Texture.Width * arr[1, 1].Scale)) / 2, (Scale * SharedValuesSingle.Background.Height - (y + 2) * (arr[1, 1].Texture.Height * arr[1, 1].Scale)) / 2));

            while (!HasMoreTurns()) Shuffle();
            SharedValuesSingle.fadeTexture = SharedValuesSingle.theGame.CreateFadeTexture((int)(arr[1, 1].Texture.Width * SharedValuesSingle.HerosScale * 1.5f),
(int)(arr[1, 1].Texture.Height * SharedValuesSingle.HerosScale * 1.5f));
            SharedValuesSingle.fadeTexture2 = SharedValuesSingle.theGame.CreateFadeTexture((int)(arr[1, 1].Texture.Width * SharedValuesSingle.HerosScale * 1.0f),
(int)(arr[1, 1].Texture.Height * SharedValuesSingle.HerosScale * 1.0f));
        }
        public void SetPosition(Vector2 _Position) {
            Position = _Position;
            for (int i = 0; i <= x + 1; i++)
                for (int j = 0; j <= y + 1; j++) {
                    arr[i, j].Position = Position + new Vector2(i * arr[1, 1].Texture.Width * arr[1, 1].Scale, j * arr[1, 1].Texture.Height * arr[1, 1].Scale);
                }
        }
        public void SetSelectedItem(int x, int y) {
            SelectedItem = new Vector2(x, y);
        }
        public Vector2 GetMouseSelection() {
            for (int i = 1; i <= x; i++)
                for (int j = 1; j <= y; j++) {
                    if (arr[i, j].index != -1 && arr[i, j].Position.X <= Mouse.GetState().X && arr[i, j].Position.Y <= Mouse.GetState().Y && Mouse.GetState().X < arr[i, j].Position.X + arr[i, j].Texture.Width * SharedValuesSingle.HerosScale && Mouse.GetState().Y < arr[i, j].Position.Y + arr[i, j].Texture.Height * SharedValuesSingle.HerosScale) {
                        return new Vector2(i, j);
                    }
                }
            return new Vector2(-1, -1);
        }
        public bool IsSelected(int x, int y) {
            return SelectedItem.X == x && SelectedItem.Y == y;
        }

        public void Draw() {
            Vector2 CurrentMouse = new Vector2(-1, -1);
            for (int i = 0; i <= x + 1; i++)
                for (int j = 0; j <= y + 1; j++) {
                    SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.Slot, arr[i, j].Position,
                new Rectangle(0, 0, SharedValuesSingle.Slot.Width, SharedValuesSingle.Slot.Height), new Color(new Vector4(Color.White.ToVector3(), SharedValuesSingle.slotFadeAmount)), 0.0f, Vector2.Zero, arr[1, 1].Scale, SpriteEffects.None, 0);
                    if (arr[i, j].index != -1) {
                        arr[i, j].Draw(false);
                        if (arr[i, j].Position.X <= Mouse.GetState().X && arr[i, j].Position.Y <= Mouse.GetState().Y && Mouse.GetState().X < arr[i, j].Position.X + arr[i, j].Texture.Width * SharedValuesSingle.HerosScale && Mouse.GetState().Y < arr[i, j].Position.Y + arr[i, j].Texture.Height * SharedValuesSingle.HerosScale) {
                            SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[i, j].Position,
             new Color(new Vector4(Color.LightYellow.ToVector3(), SharedValuesSingle.fadeAmount)));
                            CurrentMouse = new Vector2(i, j);

                        }
                    }
                }

            if (CurrentMouse.X != -1 && SharedValuesSingle.PathCover) {
                if (SelectedItem.X != -1 && Trace[(int)CurrentMouse.X, (int)CurrentMouse.Y].X != -1) {
                    int u = (int)CurrentMouse.X, v = (int)CurrentMouse.Y, t, i, j;
                    Color color = (arr[(int)CurrentMouse.X, (int)CurrentMouse.Y].index == arr[(int)SelectedItem.X, (int)SelectedItem.Y].index ? Color.Green : Color.Red);
                    while (u != SelectedItem.X || v != SelectedItem.Y) {
                        t = u;
                        i = (int)Trace[u, v].X;
                        j = (int)Trace[t, v].Y;
                        if (i == u) {
                            if (j < v) {
                                for (int k = j + 1; k <= v; k++) {
                                    SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[u, k].Position,
            new Color(new Vector4(color.ToVector3(), SharedValuesSingle.fadeAmount)));
                                }
                            } else {
                                for (int k = j - 1; k >= v; k--) {
                                    SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[u, k].Position,
            new Color(new Vector4(color.ToVector3(), SharedValuesSingle.fadeAmount)));
                                }
                            }
                        } else {
                            if (i < u) {
                                for (int k = i + 1; k <= u; k++) {
                                    SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[k, v].Position,
            new Color(new Vector4(color.ToVector3(), SharedValuesSingle.fadeAmount)));
                                }
                            } else {
                                for (int k = i - 1; k >= u; k--) {
                                    SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[k, v].Position,
            new Color(new Vector4(color.ToVector3(), SharedValuesSingle.fadeAmount)));
                                }
                            }
                        }
                        u = i; v = j;
                    }
                }
            }

            if (SelectedItem.X != -1 && SharedValuesSingle.PathFinding) {
                bool[,] Checked = new bool[x + 2, y + 2];
                for (int i = 0; i <= x + 1; i++)
                    for (int j = 0; j <= y + 1; j++) if (arr[i, j].index == arr[(int)SelectedItem.X, (int)SelectedItem.Y].index && Trace[i, j].X != -1) {
                            int u = i, v = j, t, p, q;
                            while (u != SelectedItem.X || v != SelectedItem.Y) {

                                t = u;
                                p = (int)Trace[u, v].X;
                                q = (int)Trace[t, v].Y;
                                if (p == u) {
                                    if (q < v) {
                                        for (int k = q + 1; k <= v; k++) {
                                            if (!Checked[u, k]) {
                                                SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[u, k].Position,
                        new Color(new Vector4(Color.LightYellow.ToVector3(), SharedValuesSingle.fadeAmount)));
                                                Checked[u, k] = true;
                                            }
                                        }
                                    } else {
                                        for (int k = q - 1; k >= v; k--) {
                                            if (!Checked[u, k]) {
                                                SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[u, k].Position,
                        new Color(new Vector4(Color.LightYellow.ToVector3(), SharedValuesSingle.fadeAmount)));
                                                Checked[u, k] = true;
                                            }
                                        }
                                    }
                                } else {
                                    if (p < u) {
                                        for (int k = p + 1; k <= u; k++) {
                                            if (!Checked[k, v]) {
                                                SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[k, v].Position,
                        new Color(new Vector4(Color.LightYellow.ToVector3(), SharedValuesSingle.fadeAmount)));
                                                Checked[k, v] = true;
                                            }
                                        }
                                    } else {
                                        for (int k = p - 1; k >= u; k--) {
                                            if (!Checked[k, v]) {
                                                SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[k, v].Position,
                        new Color(new Vector4(Color.LightYellow.ToVector3(), SharedValuesSingle.fadeAmount)));
                                                Checked[k, v] = true;
                                            }
                                        }
                                    }
                                }
                                u = p; v = q;
                            }
                        }

            }
            if (hint) {
                SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[(int)hint1.X, (int)hint1.Y].Position,
             new Color(new Vector4(Color.Yellow.ToVector3(), SharedValuesSingle.fadeAmount)));
                SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture2, arr[(int)hint2.X, (int)hint2.Y].Position,
              new Color(new Vector4(Color.Yellow.ToVector3(), SharedValuesSingle.fadeAmount)));
            }
            if (SelectedItem.X != -1) {
                arr[(int)SelectedItem.X, (int)SelectedItem.Y].Draw(true);
            }
        }
    }
}
