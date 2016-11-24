using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TentativeTitle.GameState
{
    class SceneMainMenu : Scene
    {
        private SpriteFont _fontVisitor;

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(_fontVisitor, "Testing", Vector2.Zero, Color.White);
        }

        public void LoadContent(ContentManager content)
        {
            _fontVisitor = content.Load<SpriteFont>(@"fonts/visitor");
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
