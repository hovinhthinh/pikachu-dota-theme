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
    public class SpriteMulti {
        public Texture2D Texture;

        public Vector2 Position = new Vector2(0, 0);
        public float Scale = 1.0f;


        public SpriteMulti(string theAssetName, float _Scale) {
            if (theAssetName == "") return;
            Texture = SharedValuesMulti.contentManager.Load<Texture2D>(theAssetName);
            Scale = _Scale;
        }
        public void Draw(bool IsSelected) {
            if (IsSelected) {
                SharedValuesMulti.spriteBatch.Draw(Texture, Position - new Vector2(12, 12),
                    new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0);
                SharedValuesMulti.spriteBatch.Draw(SharedValuesMulti.fadeTexture, Position - new Vector2(12, 12),
                new Color(new Vector4(Color.LightYellow.ToVector3(), SharedValuesMulti.fadeAmount)));

            } else {
                SharedValuesMulti.spriteBatch.Draw(Texture, Position,
                    new Rectangle(0, 0, Texture.Width, Texture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
            }

        }

    }

    public class HeroMulti : SpriteMulti {
        public int index;
        public HeroMulti(int _index)
            : base(_index == -1 ? "" : SharedValuesMulti.Heros[_index], SharedValuesMulti.HerosScale) {
            index = _index;
        }
    }
    public class Resign {
        public static Texture2D[] arr;
        public static bool isClicked() {
            return (Mouse.GetState().X >= 1055 && Mouse.GetState().X <= 1120 && Mouse.GetState().Y >= 575 && Mouse.GetState().Y <= 640 && Mouse.GetState().LeftButton == ButtonState.Pressed);
        }
        public static void draw() {
            int i = 0;
            if (Mouse.GetState().X >= 1055 && Mouse.GetState().X <= 1120 && Mouse.GetState().Y >= 575 && Mouse.GetState().Y <= 640) i = 1;
            SharedValuesMulti.spriteBatch.Draw(arr[i], new Vector2(1055, 570),
new Rectangle(0, 0, arr[i].Width, arr[i].Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }
}
