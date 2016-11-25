using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TentativeTitle.Maps;

namespace TentativeTitle.GameState
{
    class SceneTestMap : Scene
    {
        public const string MapDirectory = "C:\\Users\\wsheng\\Documents\\Personal\\GameJamFall2016\\GameJamFall2016\\TentativeTitle\\TentativeTitle\\bin\\Windows\\x86\\Debug\\Content\\maps\\start.map";
        private Texture2D _tileset;
        private Map _map;
        private Vector2 _position = new Vector2(0, 1200);

        private Vector2 _backgroundPosition = new Vector2(0, 0);
        private Texture2D _textureBackground;

        public void Draw(SpriteBatch batch)
        {
            Rectangle backgroundBounds = _textureBackground.Bounds;
            int backgroundWidth = backgroundBounds.Width;
            int backgroundHeight = backgroundBounds.Height;
            Settings currentSettings = Settings.GetSingleton();
            int timesBackgroundWidth = (int) (currentSettings.DisplayWidth / (backgroundWidth * 1.0f)) + 2;
            int timesBackgroundHeight = (int) (currentSettings.DisplayHeight / (backgroundHeight * 1.0f)) + 2;
            for (int x = 0; x < timesBackgroundWidth; x++)
            {
                for (int y = 0; y < timesBackgroundHeight; y++)
                {
                    Vector2 drawPos = new Vector2(x * (backgroundBounds.Width) * 1.0f, y * (backgroundBounds.Height * 1.0f));
                    batch.Draw(_textureBackground, _backgroundPosition + drawPos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                }
            }

            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    Vector2 drawPos = new Vector2(x * (_map.TextureMap.TileWidth) * 2.0f, y * (_map.TextureMap.TileHeight * 2.0f));
                    batch.Draw(_tileset, drawPos - _position, _map.GetTileOnSheet(_map.Tiles[x, y].ID), Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0);
                }
            }
        }

        public void LoadContent(ContentManager content)
        {
            _map = FileReadWriter.ReadFromBinary<Map>(MapDirectory);
            _tileset = content.Load<Texture2D>(_map.TextureMap.TileTexture);
            _textureBackground = content.Load<Texture2D>("sprites/backgrounds/background1");
        }

        public void UnloadContent()
        {
            _tileset.Dispose();  
        }

        public void Update(GameTime gameTime)
        {
            float speed = 120.0f;
            float parallax = 0.5f;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                _backgroundPosition.X += dt * speed * parallax;
                _position.X -= dt * speed;
            }
            else if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
                _backgroundPosition.X -= dt * speed * parallax;
                _position.X += dt * speed;
            }
            if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                _backgroundPosition.Y += dt * speed * parallax;
                _position.Y -= dt * speed;
            }
            else if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                _backgroundPosition.Y -= dt * speed * parallax;
                _position.Y += dt * speed;
            }

            Rectangle backgroundBounds = _textureBackground.Bounds;
            if (_backgroundPosition.X > 0)
                _backgroundPosition.X -= backgroundBounds.Width;
            else if (_backgroundPosition.X < -backgroundBounds.Width)
                _backgroundPosition.X += backgroundBounds.Width;
            if (_backgroundPosition.Y > 0)
                _backgroundPosition.Y -= backgroundBounds.Height;
            else if (_backgroundPosition.Y < -backgroundBounds.Height)
                _backgroundPosition.Y += backgroundBounds.Height;
        }
    }
}
