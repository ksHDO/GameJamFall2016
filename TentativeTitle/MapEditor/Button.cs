using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    delegate void ButtonClick();
    class Button : IDisposable
    {
        private event ButtonClick ClickEvent;
        public bool Visible { get; set; }
        public Texture2D Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                if (_texture != null)
                {
                    _dimensions = new Vector2(_texture.Width, _texture.Height);
                }
            }
        }
        public Vector2 Position { get; set; } = Vector2.Zero;
        public float Scale { get; set; } = 1.0f;

        private Vector2 _dimensions = Vector2.Zero;
        private Texture2D _texture;

        public Button(Texture2D texture)
        {
            Texture = texture;
        }

        public Button(ContentManager content, string asset)
        {
            Texture = content.Load<Texture2D>(asset);
        }

        public void Dispose()
        {
            Texture.Dispose();
        }

        public void SetEvent(ButtonClick click)
        {
            ClickEvent = click;
        }

        public void Update()
        {
            if (MouseInput.CheckLeftPressed())
            {
                Vector2 pos = MouseInput.LastPos;
                if (pos.X >= Position.X && pos.X < Position.X + (_dimensions.X * Scale) && pos.Y >= Position.Y && pos.Y < Position.Y + (_dimensions.Y * Scale))
                {
                    ClickEvent();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Texture != null)
                spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }
    }
}
