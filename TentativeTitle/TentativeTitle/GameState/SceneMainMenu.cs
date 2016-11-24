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

        public void Draw(SpriteBatch batch)
        {
            _textTesting.Draw(batch);
        }

        public void LoadContent(ContentManager content)
        {
            _fontVisitor = content.Load<SpriteFont>(@"fonts/visitor");
            _textTesting = new Text(_fontVisitor, "Testing", Vector2.Zero, Align.CenterMid, Color.White, TextAlignment.CenterMid);
        }

        public void UnloadContent()
        {
               
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
