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
using TentativeTitle.Components;

namespace TentativeTitle.GameState
{
    class ScenePlay : Scene
    {
        private EntityManager _entityManager = new EntityManager();
        private Song _song;

        public void Draw(SpriteBatch batch)
        {
            _entityManager.Draw(batch);
        }

        public void LoadContent(ContentManager content)
        {
            MouseManager.CurrentCursor = MouseManager.Cursor.CROSSHAIR;
            _song = content.Load<Song>(@"music/AVoid");
            MediaPlayer.Play(_song);
            MediaPlayer.IsRepeating = true;

            _entityManager.LoadContent(content);
        }

        public void UnloadContent()
        {
            MediaPlayer.Stop();
            _song.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            //ComponentTransform temp = ((ComponentTransform)_entityManager.GetEntity("testSprite").GetComponent<ComponentTransform>());
            //Vector2 tempPos = ((MouseInput.CurrentPos - temp.GetPos()).Normalized().MultiplyLength(100.0).MultiplyLength(gameTime.ElapsedGameTime.TotalSeconds));
            //temp.SetPos(temp.GetPos() + tempPos);
            _entityManager.Update(gameTime);
            
        }
    }
}
