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
    public class SpriteSingle {
        public Texture2D Texture;

        public Vector2 Position = new Vector2(0, 0);
        public float Scale = 1.0f;

        public SpriteSingle(string theAssetName, float _Scale) {
            if (theAssetName == "") return;
            Texture = SharedValuesSingle.contentManager.Load<Texture2D>(theAssetName);
            Scale = _Scale;
        }
        public void Draw(bool IsSelected) {
            if (IsSelected) {
                SharedValuesSingle.spriteBatch.Draw(Texture, Position - new Vector2(12, 12),
                    new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0);
                SharedValuesSingle.spriteBatch.Draw(SharedValuesSingle.fadeTexture, Position - new Vector2(12, 12),
                new Color(new Vector4(Color.LightYellow.ToVector3(), SharedValuesSingle.fadeAmount)));

            } else {
                SharedValuesSingle.spriteBatch.Draw(Texture, Position,
                    new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            }

        }

    }

    public class HeroSingle : SpriteSingle {
        public int index;
        public HeroSingle(int _index)
            : base(_index == -1 ? "" : SharedValuesSingle.Heros[_index], SharedValuesSingle.HerosScale) {
            index = _index;
        }
    }
    public class Hint {
        public static Texture2D[] arr;
        public static Vector2 ordinate = new Vector2(940, 275);
        public static Vector2 origin = new Vector2(1095, 435);
        public static float r = 100;
        public static bool isClicked() {
            double dist = Math.Sqrt(Math.Pow(Mouse.GetState().X - origin.X, 2) + Math.Pow(Mouse.GetState().Y - origin.Y, 2));
            return dist <= r && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
        public static void draw() {
            int i = 0;

            double dist = Math.Sqrt(Math.Pow(Mouse.GetState().X - origin.X, 2) + Math.Pow(Mouse.GetState().Y - origin.Y, 2));
            if (dist <= r) {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    i = 2;
                else i = 1;
            } else {
                i = 0;
            }
            SharedValuesSingle.spriteBatch.Draw(arr[i], ordinate,
new Rectangle(0, 0, arr[i].Width, arr[i].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);

        }
    }
    public class Level {
        public static int level;
        public static Vector2[] size;
        public static double[] time;

        static Level() {
            level = 0;
            size = new Vector2[10];
            time = new double[10];
            size[0] = new Vector2(4, 4); time[0] = 120;
            size[1] = new Vector2(8, 6); time[1] = 360;
            size[2] = new Vector2(10, 8); time[2] = 450;
            size[3] = new Vector2(12, 8); time[3] = 450;
            size[4] = new Vector2(12, 8); time[4] = 360;
            size[5] = new Vector2(14, 10); time[5] = 720;
            size[6] = new Vector2(14, 10); time[6] = 450;
            size[7] = new Vector2(16, 12); time[7] = 720;
            size[8] = new Vector2(16, 12); time[8] = 450;
            size[9] = new Vector2(16, 12); time[9] = 300;
        }
        public static Texture2D[] arr;
        public static Vector2 ordinate = new Vector2(1175, 125);
        public static void draw() {
            SharedValuesSingle.spriteBatch.Draw(arr[level], ordinate,
new Rectangle(0, 0, arr[level].Width, arr[level].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }

    public class Swap {
        public static Texture2D[] arr;
        public static Vector2 ordinate = new Vector2(950, 530);
        public static Vector2 origin = new Vector2(1000, 580);
        public static float r = 45;

        public static int remain;
        public static bool isClicked() {
            double dist = Math.Sqrt(Math.Pow(Mouse.GetState().X - origin.X, 2) + Math.Pow(Mouse.GetState().Y - origin.Y, 2));
            return dist <= r && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
        public static void draw() {
            if (remain == 0) {
                SharedValuesSingle.spriteBatch.Draw(arr[6], ordinate,
new Rectangle(0, 0, arr[6].Width, arr[6].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                return;
            }
            int t = 0;
            if (remain == 3) t = 0;
            else
                if (remain == 2) t = 2;
                else
                    if (remain == 1) t = 4;
            double dist = Math.Sqrt(Math.Pow(Mouse.GetState().X - origin.X, 2) + Math.Pow(Mouse.GetState().Y - origin.Y, 2));
            if (dist <= r) {
                SharedValuesSingle.spriteBatch.Draw(arr[t + 1], ordinate,
new Rectangle(0, 0, arr[t + 1].Width, arr[t + 1].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            } else {
                SharedValuesSingle.spriteBatch.Draw(arr[t], ordinate,
new Rectangle(0, 0, arr[t].Width, arr[t].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
        }
    }
    public class Quit {
        public static Texture2D[] arr;
        public static Vector2 ordinate = new Vector2(1125, 523);
        public static Vector2 origin = new Vector2(1183, 582);
        public static float r = 47;
        public static bool isClicked() {
            double dist = Math.Sqrt(Math.Pow(Mouse.GetState().X - origin.X, 2) + Math.Pow(Mouse.GetState().Y - origin.Y, 2));
            return dist <= r && Mouse.GetState().LeftButton == ButtonState.Pressed;
        }
        public static void draw() {
            double dist = Math.Sqrt(Math.Pow(Mouse.GetState().X - origin.X, 2) + Math.Pow(Mouse.GetState().Y - origin.Y, 2));
            if (dist <= r) {
                SharedValuesSingle.spriteBatch.Draw(arr[1], ordinate,
new Rectangle(0, 0, arr[1].Width, arr[1].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            } else {
                SharedValuesSingle.spriteBatch.Draw(arr[0], ordinate,
new Rectangle(0, 0, arr[0].Width, arr[0].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
