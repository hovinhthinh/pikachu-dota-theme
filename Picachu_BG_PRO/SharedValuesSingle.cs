using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public class SharedValuesSingle {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        public static int WindowWidth = 920 + 345;
        public static int WindowHeight = 690;
        public static string[] Heros;
        public static float HerosScale = 0.75f;
        public static ContentManager contentManager;
        public static SpriteBatch spriteBatch;
        public static Texture2D Background;
        public static Texture2D PanelBG;
        public static Texture2D Slot;
        
        public static float fadeAmount = 0.0f;
        public static float slotFadeAmount = 0.75f;
        public static Texture2D fadeTexture, fadeTexture2;
        public static Single theGame;
        public static bool PathCover = true;
        public static bool PathFinding = false;
        

        static SharedValuesSingle() {
            Heros = new string[108];
            RandomShuffle();
        }
        static void RandomShuffle() {
            Random random = new Random(System.Environment.TickCount);
            bool[] Checked = new bool[108];
            for (int i = 0; i < 108; i++) {
                int t = random.Next(108 - i) + 1;
                for (int j = 0; i < 108; j++) if (!Checked[j]) {
                        t--;
                        if (t == 0) {
                            Heros[i] = j.ToString();
                            Checked[j] = true;
                            break;
                        }
                    }
            }
        }

        public static void SetBackground() {
            spriteBatch.Draw(Background, new Vector2(0, 0),
                new Rectangle(0, 0, Background.Width, Background.Height), Color.White, 0.0f, Vector2.Zero, (float)WindowHeight / Background.Height, SpriteEffects.None, 0);
            spriteBatch.Draw(PanelBG, new Vector2(920, 0),
                            new Rectangle(0, 0, Background.Width, Background.Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }

    }
}
