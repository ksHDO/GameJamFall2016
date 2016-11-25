using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TentativeTitle.Entities;

namespace TentativeTitle.Components
{
    class EntityManager
    {

        private const int MAX_ENTITIES = 512;
        private Entity[] _entities = new Entity[MAX_ENTITIES];
        private int _entityIndex = 0;


        public bool AddEntity(Entity entity)
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                if (_entities[i].Name == entity.Name)
                {
                    Console.Write("Entity::AddComponents - Entity already contains a component with that name! Could not add [" + entity.Name + "] " + entity.GetType().ToString() + "\n");
                    return false;
                }
            }

            if (_entityIndex == MAX_ENTITIES)
            {
                Console.Write("Entity::AddComponents - Entity has reached maximum amount of components! Could not add [" + entity.Name + "] " + entity.GetType().ToString() + "\n");
                return false;
            }

            _entities[_entityIndex] = entity;
            _entityIndex++;
            return true;
        }


        public void UpdateEntities(Entity entity)
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                if (_entities[i].GetType() == entity.GetType())
                {
                    _entities[i] = entity;
                }
            }
        }

        public void MoveAllBy(Vector2 delta)
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                _entities[i].GetComponent<ComponentTransform>().Pos = _entities[i].GetComponent<ComponentTransform>().Pos + delta;
            }
        }

        public bool LoadContent(ContentManager content)
        {
            //if(!AddEntity(new EntitySprite("testSprite", content.Load<Texture2D>("sprites/player/Character"))))
            //{
            //    Console.WriteLine("EntityManager: loading test failed");
            //    return false;
            //}
            //
            //((ComponentTransform)((EntitySprite)GetEntity("testSprite")).GetComponent<ComponentTransform>()).SetPos(new Vector2(20.0f, 20.0f));

            return true;
        }

        public Entity GetEntity(string name)
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                if (_entities[i].Name == name)
                {
                    return _entities[i];
                }
            }
            Console.Write("Entity::GetComponent - Could not find component with name [" + name + "]!\n");
            return null;
        }

        //public Entity GetComponentAs<T>(string name)
        //{
        //    for (int i = 0; i < _entityIndex; i++)
        //    {
        //        if (_entities[i] is T)
        //        {
        //            return _entities[i];
        //        }
        //    }
        //    Console.Write("Entity::GetComponent - Could not find entity (" + typeof(T).ToString() + ")\n");
        //    return null;
        //}

        public void Update(GameTime time)
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                if (_entities[i] != null)
                {
                    if (_entities[i].IsEnabled)
                    {
                        _entities[i].Update(time);
                    }
                }
            }
        }
       
        public bool Initialize()
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                if (_entities[i] != null)
                {
                    _entities[i].Initialize();
                }
            }

            return true;
        }


        public void Draw(SpriteBatch batch)
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                if (_entities[i].IsEnabled)
                {
                    _entities[i].Draw(batch);
                }
            }
        }



    }
}
