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
            set
            {
                _bounds = value;
            }
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



    /// <summary>
    /// Add last
    /// </summary>
    class ComponentSprite : Component
    {
        Sprite _sprite;


        public Rectangle Bounds
        {
            get
            {
                return _sprite.Bounds;
            }
        }

        public Texture2D Texture
        {
            get
            {
                return _sprite.Texture;
            }
        }

        public ComponentSprite(Texture2D tex = null) : base("sprite")
        {
            _sprite = new Sprite(tex);
        }


        public Texture2D SetTexture(Texture2D tex)
        {
            return _sprite.SetTexture(tex);
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }


        //public Texture2D GetTexture()
        //{
        //    return _sprite.Texture;
        //}

        public override void Update(GameTime time)
        {
            //ComponentTransform transform = (ComponentTransform)GetSiblingComponent<ComponentTransform>();
            //Point trans = transform.WorldTransform.Translate.ToPoint();
            //trans.X -= (_sprite.Bounds.Width / 2);
            //trans.Y -= (_sprite.Bounds.Height / 2);

            //Rectangle rect = new Rectangle(trans.X, trans.Y, Bounds.Width, Bounds.Height);


            //rect.Width  = (int)((float)rect.Width * transform.Scale);
            //rect.Height = (int)((float)trans.Y * transform.Scale);
          

            

           // _sprite.Bounds = 
        }


    }

}