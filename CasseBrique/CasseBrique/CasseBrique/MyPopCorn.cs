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

namespace CasseBrique
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MyPopCorn : Game
    {
        public static Random GlobalRnd = new Random(); //Pour faire du random dans le jeux

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KeyboardState _Clavier;

        private Infos _Infos;
        private Raquette _Raquette;
        private Balle _Balle;
        private Monde _Monde;

        private bool IsPaused = false;
        private bool IsGameOver = false;
        private bool IsPauseKeyDownBefore = false;

        private Viewport GameViewport;
        private Viewport InfosViewport;
        private Texture2D Background;

        public MyPopCorn()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 700;

            //IsFixedTimeStep = false;
            graphics.SynchronizeWithVerticalRetrace = false; // draw a apr�s chaque update ne bride pas a 60 donc n'assure pas la synchro verticale
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameViewport = new Viewport(8, 8, 484, 592);
            InfosViewport = new Viewport(510, 30, 170, 592);

            _Infos = new Infos(InfosViewport);

            _Raquette = new Raquette(GameViewport);
            _Raquette.Initialize();

            _Balle = new Balle(GameViewport);
            _Balle.Initialize();

            _Monde = new Monde(GameViewport);
            _Monde.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Background = Content.Load<Texture2D>("Background");

            _Monde.LoadContent(Content);

            _Infos.LoadContent(Content);

            _Raquette.LoadContent(Content, "Raquette");

            _Balle.LoadContent(Content, "Balle");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 125.0f);   //125fps

            _Clavier = Keyboard.GetState();

            GererPause();

            GererMort();

            if (!IsPaused && !IsGameOver && !_Balle.Out)
            {
                _Raquette.HandleInput(_Clavier, Content);

                _Raquette.Update(gameTime);

                _Balle.HandleInput(_Clavier, _Raquette);

                _Balle.Update(gameTime, _Raquette);
            }

            if (!IsPaused && !IsGameOver)
            {
                _Monde.Update(gameTime, _Balle, _Raquette);
                _Monde.ReloadSomeContent(Content);
            }

            _Infos.Update(gameTime, _Raquette);

            bool IsLevelAlive = (from b in _Monde.Briques
                                    where b.Vie != BriqueLevel.Incassable && b.Vie != BriqueLevel.Morte
                                    select b).Any();


            if (!IsLevelAlive)
            {
                _Monde.Niveau++;
                _Monde.Initialize();
                _Raquette.Initialize();
                _Balle.Initialize();
                _Monde.LoadContent(Content);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Background, Vector2.Zero, Color.White);

            _Monde.Draw(spriteBatch, gameTime);

            _Infos.Draw(spriteBatch, gameTime);

            _Raquette.Draw(spriteBatch, gameTime);

            _Balle.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        private void GererPause()
        {
            bool PauseKeyDownNow = _Clavier.IsKeyDown(Keys.P);

            if (PauseKeyDownNow && !IsPauseKeyDownBefore)
            {
                IsPaused = !IsPaused;
            }

            IsPauseKeyDownBefore = PauseKeyDownNow;
        }

        private void GererMort()
        {
            if (_Clavier.IsKeyDown(Keys.Space) && _Balle.Out)
            {
                _Raquette.Vie--;

                if (_Raquette.Vie < 1)
                {
                    IsGameOver = true;
                }
                else
                {
                    _Raquette.Initialize();
                    _Balle.Initialize();
                }
            }
        }
    }
}
