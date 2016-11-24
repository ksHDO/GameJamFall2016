using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TentativeTitle.Components
{

    public struct Sprite
    {
        Texture2D _tex;
        Rectangle _bounds;

        public Texture2D Texture
        {
            get
            {
                return _tex;
            }
        }

        public Rectangle Bounds
        {
            get
            {
                return _bounds;
            }
        }

        public Sprite(Texture2D tex = null)
        {
            _tex = tex;
            _bounds = tex.Bounds;
        }

        /// <summary>
        /// Returns old texture for disposal or other operations
        /// </summary>
        /// <returns></returns>
        public Texture2D SetTexture(Texture2D tex)
        {
            Texture2D oldTex = _tex;
            _tex = tex;
            return oldTex;
        }

        public void Dispose()
        {
            _tex.Dispose();
        }

    }

    class ComponentSprite : Component
    {
        Sprite _sprite;
        public ComponentSprite(Texture2D tex = null) : base("sprite")
        {
            _sprite = new Sprite(tex);
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }

        public Rectangle GetBounds()
        {
            return _sprite.Bounds;
        }


        public Texture2D GetTexture()
        {
            return _sprite.Texture;
        }

    }

}