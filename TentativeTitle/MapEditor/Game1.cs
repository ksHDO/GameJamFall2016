using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TentativeTitle;

namespace MapEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public static int Height { get; private set; }
        public static int Width { get; private set; }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D spriteSheet;
        // Map _testMap;
        Editor _editor;
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
            IsMouseVisible = true;

            Height = 800;
            Width = 800;
            graphics.PreferredBackBufferHeight = Height;
            graphics.PreferredBackBufferWidth = Width;
            Window.Position = new Point(Window.Position.X, 100);
            graphics.ApplyChanges();

            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            ShapeGenerator.Initialize(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteSheet = Content.Load<Texture2D>("tileset1");


            _editor = new Editor();
            _editor.Load(Content);
            EditorText.Load(Content);
            KeyboardInput.AddKey(Keys.P);
            KeyboardInput.AddKey(Keys.Q);
            KeyboardInput.AddKey(Keys.E);
            //_testMap = new Map(500, 500, spriteSheet);
            //_testMap.Load(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            _editor.Unload();
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

            // TODO: Add your update logic here
            // _testMap.Update(gameTime);
            MouseInput.Update();
            KeyboardInput.Update();
            _editor.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            // spriteBatch.Draw(spriteSheet, new Vector2 ( 10, 10 ), new Rectangle(0, 0, 32, 32), Color.White);
            // spriteBatch.Draw(spriteSheet, new Vector2(10, 100), new Rectangle(64, 0, 32, 32), Color.White);
            _editor.Draw(spriteBatch);
            EditorText.Draw(spriteBatch);
            // _testMap.Draw(spriteBatch);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
