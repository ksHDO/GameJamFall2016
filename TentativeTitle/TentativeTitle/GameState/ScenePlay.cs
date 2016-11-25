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
using TentativeTitle.Entities;
using Microsoft.Xna.Framework.Input;
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

            
            _entityManager.AddEntity(new EntityProjectile("testEntity", content.Load<Texture2D>(@"sprites/player/Character"), new Vector2(100.0f, 100.0f), new Vector2(32.0f,32.0f), true));
            _entityManager.AddEntity(new EntityPlatform("platform", content.Load<Texture2D>(@"sprites/tiles/texture1"), new Vector2(75.0f, 200.0f), new Vector2(225.0f, 96.0f), true));


            _entityManager.LoadContent(content);
        }

        public void UnloadContent()
        {
            MediaPlayer.Stop();
            _song.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            ComponentTransform temp = _entityManager.GetEntity("testEntity").GetComponent<ComponentTransform>();
            ComponentPhysics tempPhys = _entityManager.GetEntity("testEntity").GetComponent<ComponentPhysics>();
            //Vector2 tempPos = ((MouseInput.CurrentPos - temp.GetPos()).Normalized().MultiplyLength(100.0).MultiplyLength(gameTime.ElapsedGameTime.TotalSeconds));


            if (MouseInput.CheckLeftPressed())
            {
                temp.Pos = MouseInput.CurrentPos;
            }

            if (KeyboardInput.CheckIsKeyDown(Keys.A))
            {
                tempPhys.AddVelocity(new Vector2(-20.0f, 0.0f));
            }
            if (KeyboardInput.CheckIsKeyDown(Keys.D))
            {
                tempPhys.AddVelocity(new Vector2(20.0f, 0.0f));
            }
            if (KeyboardInput.CheckIsKeyDown(Keys.Space))
            {
                tempPhys.AddVelocity(new Vector2(0.0f, -30.0f));
            }

            _entityManager.Update(gameTime);
            
        }
    }
}
