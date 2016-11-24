using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Components
{
    class GameTime { }
    class Entity
    {
        private static int _currentID = 0;

        protected const int MAX_COMPONENTS = 20;
        protected Component[] _components;
        protected int _componentIndex = 0;

        public int ID { get; private set; }
        public string Name { get;}
        public bool IsEnabled { get; set; }

        public Entity(string name, bool enabled)
        {
            ID = _currentID++;
            _components = new Component[MAX_COMPONENTS];
            Name = name;
            IsEnabled = enabled;
        }

        public bool AddComponent(Component component)
        {
            for (int i = 0; i < _componentIndex; i++)
            {
                if (_components[i].GetType() == component.GetType())
                {
                    Console.Write("Entity::AddComponents - Entity already contains a component of that type! Could not add [" + component.Name + "] " + component.GetType().ToString() + "\n");
                    return false;
                }
                else if (_components[i].Name == component.Name)
                {
                    Console.Write("Entity::AddComponents - Entity already contains a component with that name! Could not add [" + component.Name + "] " + component.GetType().ToString() + "\n");
                    return false;
                }
            }

            if (_componentIndex == MAX_COMPONENTS)
            {
                Console.Write("Entity::AddComponents - Entity has reached maximum amount of components! Could not add [" + component.Name + "] " + component.GetType().ToString() + "\n");
                return false;
            }

            component._owner = this;
            _components[_componentIndex] = component;
            _componentIndex++;
            return true;
        }

        public void UpdateComponent(Component component)
        {
            for (int i = 0; i < _componentIndex; i++)
            {
                if (_components[i].GetType() == component.GetType())
                {
                    _components[i] = component;
                }
            }
        }

        public Component GetComponent<T>()
        {
            for (int i = 0; i < _componentIndex; i++)
            {
                if (_components[i] is T)
                {
                    return _components[i];
                }
            }
            Console.Write("Entity::GetComponent - Could not find component (" + typeof(T).ToString() + ")\n");
            return null;
        }

        public Component GetComponent(string name)
        {
            for (int i = 0; i < _componentIndex; i++)
            {
                if (_components[i].Name == name)
                {
                    return _components[i];
                }
            }
            Console.Write("Entity::GetComponent - Could not find component with name [" + name + "]!\n");
            return null;
        }

        public void Update(GameTime time)
        {
            for (int i = 0; i < _componentIndex; i++)
            {
                if (_components[i].IsEnabled)
                {
                    _components[i].Update(time);
                }
            }
        }

        public override string ToString()
        {
            string componentList = "";
            for (int i = 0; i < _componentIndex; i++)
            {
                componentList += _components[i].ToString() + " (" + _components[i].GetType().ToString() + ") \n";
            }

            return "------ " + Name + " -----\n" +
                   "------------------------ \n" +
                   componentList +
                   "------------------------ \n";
        }
    }
}
