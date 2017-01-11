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
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Picachu_BG_PRO {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Multi : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        public int mapX, mapY;
        MapMulti map;
        public Stream stream;
        public bool isServer;
        public StreamReader reader;
        public StreamWriter writer;
        public double youPercent, opnPercent;
        public double winPercent;
        public bool gameEnd;

        SoundEffect tickEffect, errorEffect, quitEffect;

        SpriteFont Font;

        MouseState prevMouseState;
        private float ChangingRate = 0.0005f;
        ReadingThread thr;
        public int youScore, opnScore;
        public void closeConnection() {
            stream.Close();
        }
        public Texture2D CreateFadeTexture(int width, int height) {
            Texture2D texture = new Texture2D(
            GraphicsDevice, width, height, 1,
            TextureUsage.None,
            SurfaceFormat.Color);
            int pixelCount = width * height;
            Color[] pixelData = new Color[pixelCount];
            Random rnd = new Random();
            for (int i = 0; i < pixelCount; i++) {
                pixelData[i] = Color.LightYellow;
            }
            texture.SetData(pixelData);
            return (texture);
        }
        public Multi(Stream _stream, bool _isServer) {
            gameEnd = false;
            stream = _stream;
            isServer = _isServer;

            reader = new StreamReader(stream);
            writer = new StreamWriter(stream);
            writer.AutoFlush = true;

            SharedValuesMulti.theGame = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = SharedValuesMulti.WindowWidth;
            graphics.PreferredBackBufferHeight = SharedValuesMulti.WindowHeight;
            Content.RootDirectory = "Content";
            SharedValuesMulti.contentManager = Content;
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

        void FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e) {
            if (!gameEnd) writer.WriteLine("Quit");
            thr.thread.Abort();
            closeConnection();
            MediaPlayer.Stop();
        }
        public void loadSoundEffects() {
            tickEffect = Content.Load<SoundEffect>(@"MultiResources\Sound\Tick");
            errorEffect = Content.Load<SoundEffect>(@"MultiResources\Sound\Error");
            quitEffect = Content.Load<SoundEffect>(@"MultiResources\Sound\Quit");
        }
        protected override void LoadContent() {
            System.Windows.Forms.Form f = System.Windows.Forms.Form.FromHandle(Window.Handle) as System.Windows.Forms.Form;
            if (f != null) {
                f.FormClosing += FormClosing;
            }

            // Create a new SpriteBatch, which can be used to draw textures.
            SharedValuesMulti.spriteBatch = new SpriteBatch(GraphicsDevice);
            SharedValuesMulti.PathCover = Option.pathCover;
            loadSoundEffects();
            // TODO: use this.Content to load your game content here
            SharedValuesMulti.Slot = Content.Load<Texture2D>("Slot");
            SharedValuesMulti.Background = Content.Load<Texture2D>("Background");
            SharedValuesMulti.PanelBG = Content.Load<Texture2D>("MultiBackground");
            youScore = opnScore = 0;
            youPercent = opnPercent = 0;
            Font = Content.Load<SpriteFont>("Font");
            winPercent = 215;
            Resign.arr = new Texture2D[2];
            for (int i = 0; i < 2; i++) {
                Resign.arr[i] = Content.Load<Texture2D>(@"MultiResources\Quit\" + i.ToString());
            }
            MusicController.arr = new Song[MusicController.n];
            for (int i = 0; i < MusicController.n; i++) {
                MusicController.arr[i] = Content.Load<Song>(@"BGMusic\" + i.ToString());
            }

            if (isServer) {
                map = new MapMulti(mapX, mapY);
                map.writeMap(writer);
            } else {
                map = new MapMulti(reader, writer);
            }
            thr = new ReadingThread(reader, writer, map, this);

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

        private void scoreDraw(GameTime gameTime) {
            string scoreString = youScore.ToString();
            while (scoreString.Length < 3) scoreString = "0" + scoreString;

            SharedValuesMulti.spriteBatch.DrawString(Font, scoreString, new Vector2(965, 341), Color.Black);
            scoreString = opnScore.ToString();
            while (scoreString.Length < 3) scoreString = "0" + scoreString;

            SharedValuesMulti.spriteBatch.DrawString(Font, scoreString, new Vector2(1168, 341), Color.Black);

            double p = (double)youScore / (map.x * map.y / 2 / 2 + 1) * 215;
            if (youPercent < p) youPercent += 0.01 * gameTime.ElapsedGameTime.Milliseconds;
            int h = (int)Math.Floor(youPercent);
            if (h <= 0) h = 1;
            Texture2D rect = new Texture2D(graphics.GraphicsDevice, 27, h);

            Color[] data = new Color[27 * h];

            for (int i = 0; i < data.Length; ++i) data[i] = Color.Blue;
            rect.SetData(data);
            SharedValuesMulti.spriteBatch.Draw(rect, new Vector2(976, 640 - h + 1), Color.White);
            /**/
            p = (double)opnScore / (map.x * map.y / 2 / 2 + 1) * 215;
            if (opnPercent < p) opnPercent += 0.01 * gameTime.ElapsedGameTime.Milliseconds;
            h = (int)Math.Floor(opnPercent);
            if (h <= 0) h = 1;
            rect = new Texture2D(graphics.GraphicsDevice, 26, h);

            data = new Color[26 * h];

            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            rect.SetData(data);
            SharedValuesMulti.spriteBatch.Draw(rect, new Vector2(1175, 640 - h + 1), Color.White);


        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            SharedValuesMulti.spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            if (SharedValuesMulti.Background != null) SharedValuesMulti.SetBackground();
            MouseState mouseState = Mouse.GetState();
            if (Option.music) MusicController.play();
            if (Keyboard.GetState().IsKeyDown(Keys.F10)) {
                SharedValuesMulti.PathFinding = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F11)) {
                SharedValuesMulti.PathFinding = false;
            }
            if (map != null) {
                map.Draw();

                if (youPercent >= winPercent || opnPercent >= winPercent) {
                    gameEnd = true;
                    string s = "Draw !!!";
                    if (youScore > opnScore) s = "You won !!!";
                    if (youScore < opnScore) s = "You lost !!!";

                    System.Windows.Forms.MessageBox.Show(s);
                    //thr.thread.Abort();
                    //closeConnection();
                    this.Exit();
                }
                if (mouseState.LeftButton == ButtonState.Pressed && (prevMouseState.LeftButton != mouseState.LeftButton)) {
                    if (Resign.isClicked()) {
                        if (Option.soundEffect) quitEffect.Play();
                        Thread.Sleep(400);
                        writer.WriteLine("Quit");
                        //thr.thread.Abort();
                        //closeConnection();

                        this.Exit();
                    }
                    Vector2 selectedPosition = map.GetMouseSelection();
                    if (selectedPosition.X != -1) {
                        if (map.SelectedItem.X == -1) {
                            map.SelectedItem = selectedPosition;
                            if (Option.soundEffect) tickEffect.Play();
                            map.Bfs((int)map.SelectedItem.X, (int)map.SelectedItem.Y);
                        } else {
                            if (!map.IsSelected((int)selectedPosition.X, (int)selectedPosition.Y)) {
                                if (map.arr[(int)selectedPosition.X, (int)selectedPosition.Y].index == map.arr[(int)map.SelectedItem.X, (int)map.SelectedItem.Y].index) {
                                    if (map.IsConnected((int)selectedPosition.X, (int)selectedPosition.Y, (int)map.SelectedItem.X, (int)map.SelectedItem.Y)) {
                                        Move move = new Move((int)selectedPosition.X, (int)selectedPosition.Y, (int)map.SelectedItem.X, (int)map.SelectedItem.Y);
                                        writer.WriteLine("Move");
                                        move.writeMove(writer);
                                        map.arr[(int)selectedPosition.X, (int)selectedPosition.Y].index = -1;
                                        map.arr[(int)map.SelectedItem.X, (int)map.SelectedItem.Y].index = -1;
                                        map.available -= 2;
                                        youScore++;
                                        if (Option.soundEffect) tickEffect.Play();

                                        if (map.available > 0 && isServer) {
                                            if (!map.HasMoreTurns()) {
                                                do {
                                                    map.Shuffle();
                                                } while (!map.HasMoreTurns());
                                                writer.WriteLine("Map");
                                                map.upMap(writer);
                                            }
                                        }

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
                scoreDraw(gameTime);

            }
            Resign.draw();

            SharedValuesMulti.spriteBatch.End();

            SharedValuesMulti.fadeAmount += ChangingRate * gameTime.ElapsedGameTime.Milliseconds;
            if (SharedValuesMulti.fadeAmount >= 0.6f) {
                ChangingRate = -ChangingRate;
                SharedValuesMulti.fadeAmount = 0.6f;
            } else
                if (SharedValuesMulti.fadeAmount <= 0.1f) {
                    ChangingRate = -ChangingRate;
                    SharedValuesMulti.fadeAmount = 0.1f;
                }

            prevMouseState = mouseState;
            base.Draw(gameTime);
        }
    }
}
