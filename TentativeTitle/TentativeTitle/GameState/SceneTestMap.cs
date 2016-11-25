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
        private Vector2 _position = new Vector2(0, 600);

        public void Draw(SpriteBatch batch)
        {
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
            _map = new Map(content, @"sprites\tiles", 32, 32, 32);
            _map = FileReadWriter.ReadFromBinary<Map>(MapDirectory);
            _tileset = content.Load<Texture2D>(_map.TextureMap.TileTexture);
        }

        public void UnloadContent()
        {
            _tileset.Dispose();  
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
