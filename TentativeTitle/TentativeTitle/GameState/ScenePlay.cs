using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace TentativeTitle.GameState
{
    class ScenePlay : Scene
    {
        
        public void Draw(SpriteBatch batch)
        {
        }

        public void LoadContent(ContentManager content)
        {
            MouseManager.CurrentCursor = MouseManager.Cursor.CROSSHAIR;
        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
