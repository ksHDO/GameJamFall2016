using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle.Components
{
    class ComponentMovement : Component
    {
        public Vector2 Velocity { get; set; } = new Vector2();

        public ComponentMovement(bool isEnabled = true) : base("movement", isEnabled)
        {

        }

        //public override void Update(GameTime time)
        //{
        //    //GetSiblingComponent<ComponentTransform>().Pos += Velocity;
        //}

    }
}
