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
        private Text _textTestMap;
        private Sprite2D _spriteTest;

        public void Draw(SpriteBatch batch)
        {
            _textTesting.Draw(batch);
            _textPlay.Draw(batch);
            _textQuit.Draw(batch);
            _spriteTest.Draw(batch);
            _textTestMap.Draw(batch);
        }

        public void LoadContent(ContentManager content)
        {
            _fontVisitor = content.Load<SpriteFont>(@"fonts/visitor");
            _textTesting = new Text(_fontVisitor, "Testing", Vector2.Zero, Align.CenterMid, Color.White, TextAlignment.CenterMid);
            _textPlay = new Text(_fontVisitor, "Play", new Vector2(0, 50), Align.CenterMid, Color.White, TextAlignment.CenterMid);
            _textQuit = new Text(_fontVisitor, "Quit Game", new Vector2(0, 100), Align.CenterMid, Color.White, TextAlignment.CenterMid);
            _textTestMap = new Text(_fontVisitor, "Test Map", new Vector2(0, 10), Align.Right, Color.Red, TextAlignment.Right);

            _spriteTest = new Sprite2D(content, @"sprites/objects/warpBall", Vector2.Zero, Color.White, 0, null, 0.5f, SpriteEffects.None, 0);
        }

        public void UnloadContent()
        {
               
        }

        public void Update(GameTime gameTime)
        {
            if (MouseInput.CheckLeftPressed())
            {
                if (_textPlay.IsClicked())
                {
                    Game1.State = State.PLAY;
                }
                else if (_textQuit.IsClicked())
                {
                    Game1.State = State.QUIT;
                } else if (_textTestMap.IsClicked())
                {
                    Game1.State = State.TEST_MAP;
                }
            }

            if (KeyboardInput.CheckIsPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
                Game1.State = State.QUIT;
        }
    }
}
