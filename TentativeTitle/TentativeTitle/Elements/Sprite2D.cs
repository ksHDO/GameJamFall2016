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
    class Sprite2D : Shape
    {
        private Texture2D _texture;

        public Sprite2D(Texture2D texture, Vector2 position, Color color) : base(position, color)
        {
            _texture = texture;
        }

        public Sprite2D(Texture2D texture, Vector2 position, Align align, Color color) : base(position, align, color)
        {
            _texture = texture;
        }

        public Sprite2D(ContentManager content, string assetName, Vector2 position, Color color, float rotation = 0.0f, Vector2? origin = null, float scale = 1.0f, SpriteEffects spriteEffect = SpriteEffects.None, float layerDepth = 0.0f)
            : base(position, color, rotation, origin, scale, spriteEffect, layerDepth)
        {
            _texture = content.Load<Texture2D>(assetName);
        }

        public Sprite2D(ContentManager content, string assetName, Align align, Vector2 position, Color color, float rotation = 0.0f, Vector2? origin = null, float scale = 1.0f, SpriteEffects spriteEffect = SpriteEffects.None, float layerDepth = 0.0f)
            : base(position, align, color, rotation, origin, scale, spriteEffect, layerDepth)
        {
            _texture = content.Load<Texture2D>(assetName);
        }

        ~Sprite2D()
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
