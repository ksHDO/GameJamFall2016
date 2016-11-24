using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TentativeTitle
{
    class MouseManager : GameState.Scene
    {
        public enum Cursor
        {
            DEFAULT, CROSSHAIR
        }

        public Cursor CurrentCursor { get; set; }

        private Texture2D _textureDefault;
        private Texture2D _textureCrosshair;

        public MouseManager()
        {
            CurrentCursor = Cursor.DEFAULT;
        }

        public void Draw(SpriteBatch batch)
        {
            Texture2D currentCursor = GetCurrentCursorTexture(CurrentCursor);
            Vector2 mousePos = MouseInput.LastPos;
            Vector2 cursorOffset = GetCurrentCursorBounds(CurrentCursor, currentCursor);
            batch.Draw(currentCursor, mousePos + cursorOffset, Color.White);
        }

        private Texture2D GetCurrentCursorTexture(Cursor cursor)
        {
            Texture2D output;
            switch(cursor)
            {
                case Cursor.CROSSHAIR:
                    output = _textureCrosshair;
                    break;
                default:
                    output = _textureDefault;
                    break;
            }
            return output;
        }

        private Vector2 GetCurrentCursorBounds(Cursor cursor, Texture2D texture)
        {
            Vector2 output;
            Rectangle bounds = texture.Bounds;
            switch(cursor)
            {
                case Cursor.CROSSHAIR:
                    output = new Vector2(-bounds.Width / 2, -bounds.Height / 2);
                    break;
                default:
                    output = Vector2.Zero;
                    break;
            }
            return output;
        }

        public void LoadContent(ContentManager content)
        {
            _textureDefault = content.Load<Texture2D>(@"sprites/cursor/cursorDefault");
            _textureCrosshair = content.Load<Texture2D>(@"sprites/cursor/cursorCrosshair");
        }

        public void UnloadContent()
        {
            _textureDefault.Dispose();
            _textureCrosshair.Dispose();
        }

        public void Update(GameTime gameTime)
        {
        }



        
    }
}
