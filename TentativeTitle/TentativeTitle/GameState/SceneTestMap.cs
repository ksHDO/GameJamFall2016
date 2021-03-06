﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TentativeTitle.Maps;
using TentativeTitle.Components;
using TentativeTitle.Entities;
using Microsoft.Xna.Framework.Media;

namespace TentativeTitle.GameState
{
    class SceneTestMap : Scene
    {
        public const string MapDirectory = "Content\\maps\\start.map";
        private Texture2D _tileset;
        private Map _map;
        private Vector2 _position = new Vector2(0, 1200);
        private Song _song;

        private Vector2 _backgroundPosition = new Vector2(0, 0);
        private Texture2D _textureBackground;

        EntityManager _entityManager;
        Vector2 _oldPlayerPos = new Vector2();

        public void Draw(SpriteBatch batch)
        {
            Rectangle backgroundBounds = _textureBackground.Bounds;
            int backgroundWidth = backgroundBounds.Width;
            int backgroundHeight = backgroundBounds.Height;
            Settings currentSettings = Settings.GetSingleton();
            int timesBackgroundWidth = (int) (currentSettings.DisplayWidth / (backgroundWidth * 1.0f)) + 2;
            int timesBackgroundHeight = (int) (currentSettings.DisplayHeight / (backgroundHeight * 1.0f)) + 2;
            for (int x = 0; x < timesBackgroundWidth; x++)
            {
                for (int y = 0; y < timesBackgroundHeight; y++)
                {
                    Vector2 drawPos = new Vector2((float)x * (float)(backgroundBounds.Width) * 1.0f, (float)y * (float)(backgroundBounds.Height * 1.0f));
                    batch.Draw(_textureBackground, _backgroundPosition + drawPos, null, Color.White, 0.0f, Vector2.Zero, 1.03f, SpriteEffects.None, 0);
                }
            }

            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    Vector2 drawPos = new Vector2((float)x * (_map.TextureMap.TileWidth) * 2.0f, y * (_map.TextureMap.TileHeight * 2.0f));
                    batch.Draw(_tileset, drawPos - _position, _map.GetTileOnSheet(_map.Tiles[x, y].ID), Color.White, 0.0f, Vector2.Zero, 2.01f, SpriteEffects.None, 0);
                }
            }

            _entityManager.Draw(batch);


        }

        public void LoadContent(ContentManager content)
        {
            _entityManager = new EntityManager();

            _map = FileReadWriter.ReadFromBinary<Map>(MapDirectory);
            _tileset = content.Load<Texture2D>(_map.TextureMap.TileTexture);
            _textureBackground = content.Load<Texture2D>("sprites/backgrounds/background1");
            _song = content.Load<Song>("music/AVoid");
            MediaPlayer.Play(_song);
            MediaPlayer.IsRepeating = true;

            KeyboardInput.AddKey(Microsoft.Xna.Framework.Input.Keys.Space);


            int curPlat = 0;
            for (int x = 0; x < _map.Width; x++)
            {
                for (int y = 0; y < _map.Height; y++)
                {
                    if( _map.Tiles[x,y].ID != 8
                        && _map.Tiles[x, y].ID != 10
                        && _map.Tiles[x, y].ID != 11
                        && _map.Tiles[x, y].ID != 12
                        && _map.Tiles[x, y].ID != 13
                        && _map.Tiles[x, y].ID != 17
                        && _map.Tiles[x, y].ID != 18
                        && _map.Tiles[x, y].ID != 19)
                    {
                        //content.Load<Texture2D>(@"sprites/objects/warpBall")
                        _entityManager.AddEntity(new EntityPlatform("platform" + curPlat++, null, new Vector2(x * (_map.TextureMap.TileWidth) * 2.0f, y * (_map.TextureMap.TileHeight * 2.0f)) - _position, new Vector2((_map.TextureMap.TileWidth * 2.0f), (_map.TextureMap.TileHeight * 2.0f)), true));
                    }

                    
                }
            }

           
            //_oldPlayerPos = _entityManager.GetEntity("player").GetComponent<ComponentTransform>().WorldTransform.Translate;

            _entityManager.AddEntity(new EntityProjectile("player",
               content.Load<Texture2D>(@"sprites/player/char1"),
               new Vector2(400.0f,240.0f),
               new Vector2(32.0f, 64.0f), true));

            _oldPlayerPos = _entityManager.GetEntity("player").GetComponent<ComponentTransform>().WorldTransform.Translate;


            _entityManager.AddEntity(new EntityProjectile("warpBall",
              content.Load<Texture2D>(@"sprites/objects/warpBall"),
              new Vector2(400.0f, 240.0f),
              new Vector2(32.0f, 32.0f), true));
            //_entityManager.GetEntity("warpBall").GetComponent<ComponentTransform>().SetParent(_entityManager.GetEntity("player").GetComponent<ComponentTransform>());
            _entityManager.GetEntity("warpBall").GetComponent<ComponentTransform>().Scale = 0.6f;
            _entityManager.GetEntity("warpBall").GetComponent<ComponentCollision>().SetSize(32.0f * 0.6f, 32.0f * 0.6f);
            _entityManager.GetEntity("warpBall").GetComponent<ComponentPhysics>().DragX = 0.9999f;
        }

        public void UnloadContent()
        {
            _tileset.Dispose();
            _textureBackground.Dispose();
            MediaPlayer.Stop();
            _song.Dispose();
        }

        public void Update(GameTime gameTime)
        {
            //float speed = 50.0f;
            float parallax = 0.075f;
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            ComponentTransform playerTransform = _entityManager.GetEntity("player").GetComponent<ComponentTransform>();
            ComponentPhysics playerPhysics = _entityManager.GetEntity("player").GetComponent<ComponentPhysics>();


           // _backgroundPosition.X -= dt * speed * parallax;
           // _entityManager.MoveAllBy(new Vector2(-dt * speed, 0.0f));
           // _position.X += dt * speed;


            

            if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
               // _backgroundPosition.X += dt * speed * parallax;
                playerPhysics.Velocity = new Vector2(-300.0f, playerPhysics.Velocity.Y);
              //  _entityManager.MoveAllBy(new Vector2(dt * speed,0.0f));
             //   _position.X -= dt * speed;
                //playerTransform.Pos += new Vector2(dt * speed, 0.0f);
            }
            else if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.D))
            {
             //   _backgroundPosition.X -= dt * speed * parallax;
                //playerPhysics.AddVelocity(new Vector2(speed, 0.0f));
                playerPhysics.Velocity = new Vector2(300.0f, playerPhysics.Velocity.Y);
              //  _entityManager.MoveAllBy(new Vector2(-dt * speed, 0.0f));
                //playerTransform.Pos += new Vector2(dt * speed, 0.0f);
            //    _position.X += dt * speed;
            }

            if (KeyboardInput.CheckIsPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                if(!playerPhysics.InAir) playerPhysics.Velocity = new Vector2(playerPhysics.Velocity.X, -450.0f);
            }

                if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.W))
            {
                //_backgroundPosition.Y += dt * speed * parallax;
               // _entityManager.MoveAllBy(new Vector2(0.0f, dt * speed));
               // playerPhysics.AddVelocity(new Vector2(0.0f, -speed));
               // _position.Y -= dt * speed;
            }
            else if (KeyboardInput.CheckIsKeyDown(Microsoft.Xna.Framework.Input.Keys.S))
            {
                //_backgroundPosition.Y -= dt * speed * parallax;
               // _entityManager.MoveAllBy(new Vector2(0.0f, -dt * speed));
               // playerPhysics.AddVelocity(new Vector2(0.0f, +dt * speed));

               // _position.Y += dt * speed;
            }

            Rectangle backgroundBounds = _textureBackground.Bounds;
            if (_backgroundPosition.X > 0)
                _backgroundPosition.X -= backgroundBounds.Width;
            else if (_backgroundPosition.X < -backgroundBounds.Width)
                _backgroundPosition.X += backgroundBounds.Width;
            if (_backgroundPosition.Y > 0)
                _backgroundPosition.Y -= backgroundBounds.Height;
            else if (_backgroundPosition.Y < -backgroundBounds.Height)
                _backgroundPosition.Y += backgroundBounds.Height;

            if (MouseInput.CheckLeftPressed())
            {
                _entityManager.GetEntity("warpBall").GetComponent<ComponentTransform>().Pos = playerTransform.Pos + new Vector2(0,-36.0f);
                ComponentPhysics ballPhys = _entityManager.GetEntity("warpBall").GetComponent<ComponentPhysics>();
                ballPhys.Velocity =
                    new Vector2(MouseInput.CurrentPos.X - playerTransform.Pos.X, MouseInput.CurrentPos.Y - playerTransform.Pos.Y)
                    .Normalized().MultiplyLength(500.0f);
            }

            if (KeyboardInput.CheckIsPressed(Microsoft.Xna.Framework.Input.Keys.E))
            {

                Entity warpBall = _entityManager.GetEntity("warpBall");
                if (!warpBall.GetComponent<ComponentPhysics>().InAir)
                {
                    Entity player = _entityManager.GetEntity("player");
                    Vector2 playerPos = player.GetComponent<ComponentTransform>().Pos;
                    playerPos = warpBall.GetComponent<ComponentTransform>().Pos;
                    playerPos.Y -= 32;
                    playerPos.X += 16;
                    Vector2 vel = player.GetComponent<ComponentPhysics>().Velocity;
                    vel.Y = 0;
                    player.GetComponent<ComponentPhysics>().Velocity = vel;
                    player.GetComponent<ComponentTransform>().Pos = playerPos;
                }

                
            }


            _entityManager.Update(gameTime);

            Vector2 delta = playerTransform.WorldTransform.Translate - _oldPlayerPos;
            


            _backgroundPosition.X += delta.X * parallax;
            _backgroundPosition.Y += delta.Y * parallax;

            _entityManager.MoveAllBy(new Vector2(-delta.X, -delta.Y));
            //playerTransform.Pos += new Vector2(delta.X, delta.Y);
            _position += new Vector2(delta.X, delta.Y);

            _oldPlayerPos = playerTransform.WorldTransform.Translate;

           
        }
    }
}
