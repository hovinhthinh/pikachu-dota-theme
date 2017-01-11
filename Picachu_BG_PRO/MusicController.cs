using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class MusicController {
        public static int n = 6;
        public static Song[] arr;
        static Random rand = new Random(Environment.TickCount);
        public static void play() {
            if (MediaPlayer.State == MediaState.Stopped) MediaPlayer.Play(arr[rand.Next(n)]);
        }
    }
}
