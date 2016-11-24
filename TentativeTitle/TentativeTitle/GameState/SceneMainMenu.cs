using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TentativeTitle.Elements;

namespace TentativeTitle.GameState
{
    class SceneMainMenu : Scene
    {
        private SpriteFont _fontVisitor;
        private Text _textTesting;
        private Text _textPlay;
        private Text _textQuit;

        public void Draw(SpriteBatch batch)
        {
            _textTesting.Draw(batch);
            _textPlay.Draw(batch);
            _textQuit.Draw(batch);
        }

        public void LoadContent(ContentManager content)
        {
            _fontVisitor = content.Load<SpriteFont>(@"fonts/visitor");
            _textTesting = new Text(_fontVisitor, "Testing", Vector2.Zero, Align.CenterMid, Color.White, TextAlignment.CenterMid);
            _textPlay = new Text(_fontVisitor, "Play", new Vector2(0, 50), Align.CenterMid, Color.White, TextAlignment.CenterMid);
            _textQuit = new Text(_fontVisitor, "Quit Game", new Vector2(0, 100), Align.CenterMid, Color.White, TextAlignment.CenterMid);
        }

        public void UnloadContent()
        {
               
        }

        public void Update(GameTime gameTime)
        {
            // MouseManager.CurrentCursor = MouseManager.Cursor.CROSSHAIR;
            if (MouseInput.CheckLeftPressed())
            {
                Vector2 mousePos = MouseInput.LastPos;
                Rectangle quitBoundingBox = _textQuit.GetBoundingBox();
                if ((mousePos.X >= quitBoundingBox.X && mousePos.Y >= quitBoundingBox.Y) &&
                    (mousePos.X < quitBoundingBox.Right && mousePos.Y < quitBoundingBox.Bottom))
                {
                    Game1.State = State.QUIT;
                }
            }
        }
    }
}
