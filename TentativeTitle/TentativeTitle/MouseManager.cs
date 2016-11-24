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
    class MouseManager
    {
        public enum Cursor
        {
            DEFAULT, CROSSHAIR
        }

        public static Cursor CurrentCursor { get; set; } = CurrentCursor = Cursor.DEFAULT;

        private static Texture2D _textureDefault;
        private static Texture2D _textureCrosshair;


        public static void Draw(SpriteBatch batch)
        {
            Texture2D currentCursor = GetCurrentCursorTexture(CurrentCursor);
            Vector2 mousePos = MouseInput.LastPos;
            Vector2 cursorOffset = GetCurrentCursorBounds(CurrentCursor, currentCursor);
            batch.Draw(currentCursor, mousePos + cursorOffset, null, Color.White, 0, Vector2.Zero, 2.0f, SpriteEffects.None, 0);
        }

        private static Texture2D GetCurrentCursorTexture(Cursor cursor)
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

        private static Vector2 GetCurrentCursorBounds(Cursor cursor, Texture2D texture)
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

        public static void LoadContent(ContentManager content)
        {
            _textureDefault = content.Load<Texture2D>(@"sprites/cursor/cursorDefault");
            _textureCrosshair = content.Load<Texture2D>(@"sprites/cursor/cursorCrosshair");
        }

        public static void UnloadContent()
        {
            _textureDefault.Dispose();
            _textureCrosshair.Dispose();
        }

        public static void Update(GameTime gameTime)
        {
        }



        
    }
}
