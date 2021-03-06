﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle.Components
{
    class Component
    {
        public string Name { get; }
        public string Description { get; } = "A component.";
        public bool IsEnabled { get; set; } = true;
        public Entity _owner;

        public Component(string name, bool isEnabled = true)
        {
            Name = name;
            IsEnabled = isEnabled;
        }
        public Component(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public T GetSiblingComponent<T>() where T : Component
        {
            return _owner.GetComponent<T>();
        }
        
        virtual public bool Load()
        {
            return true;
        }
        virtual public void Update(GameTime time)
        { }

        public override string ToString()
        {
            return Name + " - " + Description;
        }
    }
}
