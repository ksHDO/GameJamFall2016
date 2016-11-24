using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Components
{
    class EntityManager
    {

        private const int MAX_ENTITIES = 1024;
        private Entity[] _entities;
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




        public void Update(GameTime time)
        {
            for (int i = 0; i < _entityIndex; i++)
            {
                if (_entities[i].IsEnabled)
                {
                    _entities[i].Update(time);
                }
            }
        }


    }
}
