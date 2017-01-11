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

using System.Threading;

namespace Picachu_BG_PRO {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Single : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;

        MapSingle map;

        MouseState prevMouseState;
        private float ChangingRate = 0.0005f;
        private double leftTime;
        private double removeTime;

        private int score;
        SpriteFont Font;
        SoundEffect tickEffect, swapEffect, hintEffect, errorEffect, quitEffect, levelUpEffect;
        private bool gameover;
        public int currentTick;


        public Texture2D CreateFadeTexture(int width, int height) {
            Texture2D texture = new Texture2D(
            GraphicsDevice, width, height, 1,
            TextureUsage.None,
            SurfaceFormat.Color);
            int pixelCount = width * height;
            Color[] pixelData = new Color[pixelCount];
            Random rnd = new Random();
            for (int i = 0; i < pixelCount; i++) {
                //could fade to a different color
                pixelData[i] = Color.LightYellow;
            }
            texture.SetData(pixelData);
            return (texture);
        }
        public Single() {
            SharedValuesSingle.theGame = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SharedValuesSingle.WindowWidth;
            graphics.PreferredBackBufferHeight = SharedValuesSingle.WindowHeight;
            //graphics.IsFullScreen = true;
            //gra

            Content.RootDirectory = "Content";
            SharedValuesSingle.contentManager = Content;
            this.IsMouseVisible = true;
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            base.Initialize();
            Window.Title = "Picachu Dota <By BG_PRO>";
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>

        protected void loadGraphic() {
            SharedValuesSingle.Slot = Content.Load<Texture2D>("Slot");
            SharedValuesSingle.Background = Content.Load<Texture2D>("Background");
            SharedValuesSingle.PanelBG = Content.Load<Texture2D>("SingleBackground");
            score = 0;

            Hint.arr = new Texture2D[3];
            for (int i = 0; i < 3; i++) {
                Hint.arr[i] = Content.Load<Texture2D>(@"SingleResources\Hint\" + i.ToString());
            }

            Level.arr = new Texture2D[10];
            Level.level = 0;
            for (int i = 0; i < 10; i++) {
                Level.arr[i] = Content.Load<Texture2D>(@"SingleResources\Level\" + i.ToString());
            }

            Swap.arr = new Texture2D[7];
            for (int i = 0; i < 7; i++) {
                Swap.arr[i] = Content.Load<Texture2D>(@"SingleResources\Swap\" + i.ToString());
            }
            Swap.remain = 3;

            Quit.arr = new Texture2D[2];
            for (int i = 0; i < 2; i++) {
                Quit.arr[i] = Content.Load<Texture2D>(@"SingleResources\Quit\" + i.ToString());
            }

        }
        public void loadSoundEffects() {
            tickEffect = Content.Load<SoundEffect>(@"SingleResources\Sound\Tick");
            swapEffect = Content.Load<SoundEffect>(@"SingleResources\Sound\Swap");
            hintEffect = Content.Load<SoundEffect>(@"SingleResources\Sound\Hint");
            errorEffect = Content.Load<SoundEffect>(@"SingleResources\Sound\Error");
            quitEffect = Content.Load<SoundEffect>(@"SingleResources\Sound\Quit");
            levelUpEffect = Content.Load<SoundEffect>(@"SingleResources\Sound\LevelUp");
        }
        void FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            MediaPlayer.Stop();
        }
        protected override void LoadContent() {
            System.Windows.Forms.Form f = System.Windows.Forms.Form.FromHandle(Window.Handle) as System.Windows.Forms.Form;
            if (f != null) {
                f.FormClosing += FormClosing;
            }
            // Create a new SpriteBatch, which can be used to draw textures.
            SharedValuesSingle.spriteBatch = new SpriteBatch(GraphicsDevice);
            SharedValuesSingle.PathCover = Option.pathCover;
            // TODO: use this.Content to load your game content here
            loadGraphic();
            loadSoundEffects();
            map = new MapSingle((int)Level.size[Level.level].X, (int)Level.size[Level.level].Y);
            leftTime = Level.time[Level.level];
            removeTime = 0;
            gameover = false;
            Font = Content.Load<SpriteFont>("Font");
            MusicController.arr = new Song[MusicController.n];
            for (int i = 0; i < MusicController.n; i++) {
                MusicController.arr[i] = Content.Load<Song>(@"BGMusic\" + i.ToString());
            }
            currentTick = Environment.TickCount;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        private void scoreDraw() {
            int width = (int)(leftTime / Level.time[Level.level] * 272);
            if (width <= 0) width = 1;
            Texture2D rect = new Texture2D(graphics.GraphicsDevice, width, 7);

            Color[] data = new Color[width * 7];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.LightGreen;
            rect.SetData(data);

            SharedValuesSingle.spriteBatch.Draw(rect, new Vector2(964, 270), Color.White);

            string scoreString = score.ToString();
            while (scoreString.Length < 3) scoreString = "0" + scoreString;

            SharedValuesSingle.spriteBatch.DrawString(Font, scoreString, new Vector2(997, 185), Color.White);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            SharedValuesSingle.spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            if (SharedValuesSingle.Background != null) SharedValuesSingle.SetBackground();
            MouseState mouseState = Mouse.GetState();
            if (Option.music) MusicController.play();
            if (Keyboard.GetState().IsKeyDown(Keys.F10)) {
                SharedValuesSingle.PathFinding = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F11)) {
                SharedValuesSingle.PathFinding = false;
            }
            if (map != null) {
                map.Draw();


                if (mouseState.LeftButton == ButtonState.Pressed && (prevMouseState.LeftButton != mouseState.LeftButton)) {
                    if (Hint.isClicked()) {
                        if (Option.soundEffect) hintEffect.Play();
                        map.hint = true;
                        removeTime += (Level.time[Level.level] * 0.1);
                        if (leftTime <= 0 && !gameover) {
                            gameover = true;
                            System.Windows.Forms.MessageBox.Show("Gameover !!!\nYour score: " + score);

                            this.Exit();
                        }
                    }
                    if (Swap.isClicked() && Swap.remain > 0) {
                        do {
                            if (Option.soundEffect) swapEffect.Play();
                            map.Shuffle();
                        } while (!map.HasMoreTurns());
                        Swap.remain--;
                    }
                    if (Quit.isClicked()) {
                        if (Option.soundEffect) quitEffect.Play();
                        Thread.Sleep(400);
                        this.Exit();
                    }

                    Vector2 selectedPosition = map.GetMouseSelection();
                    if (selectedPosition.X != -1) {
                        map.hint = false;
                        if (map.SelectedItem.X == -1) {
                            map.SelectedItem = selectedPosition;
                            if (Option.soundEffect) tickEffect.Play();
                            map.Bfs((int)map.SelectedItem.X, (int)map.SelectedItem.Y);
                        } else {
                            if (!map.IsSelected((int)selectedPosition.X, (int)selectedPosition.Y)) {
                                if (map.arr[(int)selectedPosition.X, (int)selectedPosition.Y].index == map.arr[(int)map.SelectedItem.X, (int)map.SelectedItem.Y].index) {
                                    if (map.IsConnected((int)selectedPosition.X, (int)selectedPosition.Y, (int)map.SelectedItem.X, (int)map.SelectedItem.Y)) {
                                        map.arr[(int)selectedPosition.X, (int)selectedPosition.Y].index = -1;
                                        map.arr[(int)map.SelectedItem.X, (int)map.SelectedItem.Y].index = -1;
                                        map.available -= 2;
                                        if (Option.soundEffect) tickEffect.Play();
                                        score++;
                                        if (map.available == 0) {
                                            Level.level++;
                                            levelUpEffect.Play();
                                            currentTick = Environment.TickCount;
                                            removeTime = 0;
                                            if (Level.level == 10) {
                                                System.Windows.Forms.MessageBox.Show("You won !!!");
                                                this.Exit();
                                            } else {
                                                int x = (int)Level.size[Level.level].X, y = (int)Level.size[Level.level].Y;
                                                map = new MapSingle(x, y);
                                                leftTime = Level.time[Level.level];
                                            }
                                        } else
                                            while (!map.HasMoreTurns()) map.Shuffle();
                                    } else {
                                        if (Option.soundEffect) errorEffect.Play();
                                    }
                                } else {
                                    if (Option.soundEffect) errorEffect.Play();
                                }
                                map.SetSelectedItem(-1, -1);
                            }
                        }
                    }
                } else
                    if (mouseState.RightButton == ButtonState.Pressed) {
                        map.SetSelectedItem(-1, -1);
                    }
                if (Level.level < 10) {
                    scoreDraw();
                    Level.draw();
                }
                Hint.draw();
                Swap.draw();
                Quit.draw();
            }

            SharedValuesSingle.spriteBatch.End();

            SharedValuesSingle.fadeAmount += ChangingRate * gameTime.ElapsedGameTime.Milliseconds;
            leftTime = Level.time[Level.level] - ((double)Environment.TickCount - currentTick) / 1000 - removeTime;
            if (leftTime <= 0 && !gameover) {
                gameover = true;
                System.Windows.Forms.MessageBox.Show("Gameover !!!\nYour score: " + score);
                this.Exit();
            }
            if (SharedValuesSingle.fadeAmount >= 0.6f) {
                ChangingRate = -ChangingRate;
                SharedValuesSingle.fadeAmount = 0.6f;
            } else
                if (SharedValuesSingle.fadeAmount <= 0.1f) {
                    ChangingRate = -ChangingRate;
                    SharedValuesSingle.fadeAmount = 0.1f;
                }

            prevMouseState = mouseState;
            base.Draw(gameTime);
        }
    }
}
