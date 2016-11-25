using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle.Components
{
    class ComponentGravity : Component
    {
        public float Gravity { get; set; } = -800.5f;//-9.8f;
        public ComponentGravity(bool isEnabled = true) : base("gravity", isEnabled)
        {

        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

    }
}
