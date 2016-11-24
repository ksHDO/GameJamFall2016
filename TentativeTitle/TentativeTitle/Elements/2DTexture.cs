using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Elements
{
    class Texture : Shape
    {
        private Texture2D _texture;

        public Texture(Texture2D texture, Vector2 position, Color color) : base(position, color)
        {
            _texture = texture;
        }

        public Texture(Texture2D texture, Vector2 position, Align align, Color color) : base(position, align, color)
        {
            _texture = texture;
        }

        public Texture(ContentManager content, string assetName, Vector2 position, Color color) : base(position, color)
        {
            _texture = content.Load<Texture2D>(assetName);
        }

        public Texture(ContentManager content, string assetName, Align align, Vector2 position, Color color) : base(position, align, color)
        {
            _texture = content.Load<Texture2D>(assetName);
        }

        ~Texture()
        {
            if (_texture != null)
                _texture.Dispose();
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(_texture, _position, null, Color.White, Rotation, Origin, Scale, SpriteEffect, LayerDepth);
        }
    }
}
