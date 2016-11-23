using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Components
{
    class Component
    {
        public string _name { get; }
        public string _description { get; } = "A component.";
        public bool _isEnabled { get; set; } = true;
        public Entity _owner;

        public Component(string name)
        {
            _name = name;
        }
        public Component(string name, string description)
        {
            _name = name;
            _description = description;
        }
        public Component GetSiblingComponent<T>()
        {
            return _owner.GetComponent<T>();
        }

        virtual public void Load()
        { }
        virtual public void Update(GameTime time)
        { }

        public override string ToString()
        {
            return _name + " - " + _description;
        }
    }
}
