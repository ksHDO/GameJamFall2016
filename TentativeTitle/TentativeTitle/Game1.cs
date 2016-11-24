using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TentativeTitle.GameState;

namespace TentativeTitle
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private static Scene _sceneCurrent;
        private static State _state;
        public static Settings _settings;
        private static bool _stateChanged = false;

        public static State State
        {
            get
            {
                return _state; 
            }
            set
            {
                if (_sceneCurrent != null)
                {
                    _sceneCurrent.UnloadContent();
                    _sceneCurrent = null;
                }
                _state = value;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _state = State.MAIN_MENU;
            _settings = new Settings();
            _settings.DefaultSettings();
            Settings.UpdateSingleton(_settings);

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
            
            // TODO: use this.Content to load your game content here
            ShapeGenerator.Initialize(GraphicsDevice);
            MouseManager.LoadContent(Content);
            
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            if (_sceneCurrent != null)
            {
                _sceneCurrent.UnloadContent();
            }

            MouseManager.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MouseInput.Update();
            KeyboardInput.Update();
            // TODO: Add your update logic here
            MouseManager.Update(gameTime);
            if (_sceneCurrent == null)
            {
                switch (_state)
                {
                    case State.QUIT:
                        Exit();
                        break;
                    case State.MAIN_MENU:
                        _sceneCurrent = new SceneMainMenu();
                        _sceneCurrent.LoadContent(Content);
                        break;
                    case State.PLAY:
                        _sceneCurrent = new ScenePlay();
                        _sceneCurrent.LoadContent(Content);
                        break;
                }
            }
            else
            {
                _sceneCurrent.Update(gameTime);
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

            if (_sceneCurrent != null)
            {
                _sceneCurrent.Draw(spriteBatch);
            }
            MouseManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
