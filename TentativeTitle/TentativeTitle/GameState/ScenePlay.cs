using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Media;

namespace TentativeTitle.GameState
{
    class ScenePlay : Scene
    {
        private Song _song;
        public void Draw(SpriteBatch batch)
        {


        }

        public void LoadContent(ContentManager content)
        {
            MouseManager.CurrentCursor = MouseManager.Cursor.CROSSHAIR;
            _song = content.Load<Song>(@"music/AVoid");
            MediaPlayer.Play(_song);
            MediaPlayer.IsRepeating = true;
        }

        public void UnloadContent()
        {
            MediaPlayer.Stop();
            _song.Dispose();
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
