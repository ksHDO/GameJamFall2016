using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace TentativeTitle.Components
{
    class ComponentPhysics : Component
    {
        public float Acceleration { get; set; } = 1.0f;
        // public float Velocity { get; set; } = 40.0f;
        public Vector2 Velocity { get; set; } = new Vector2(-60.0f, 40.0f);
        public float TerminalVelocity { get; set; } = -50.0f;
        // public float Angle { get; set; } = -30;
        public float Time { get; set; } = 0.0f;
        public Vector2 StartPos { get; set; } = new Vector2(100, 100);
        public float Mass { get; set; }

        public ComponentPhysics(bool isEnabled = true) : base("physics", isEnabled)
        {
            
        }

        public override void Update(GameTime time)
        {
            ComponentTransform transform = GetSiblingComponent<ComponentTransform>();
            float x = 0;
            float y = 0;
            if (transform != null)
            {
                ComponentGravity gravity = GetSiblingComponent<ComponentGravity>();
                if (gravity != null)
                {
                    Vector2 position = transform.Pos;
                    y = (float) (StartPos.Y + Velocity.Y * Math.Sin(MathHelper.ToRadians(Velocity.X)) * Time - gravity.Gravity * Time * Time / 2.0);
                    x = (float)(StartPos.X + Velocity.Y * Math.Cos(MathHelper.ToRadians(Velocity.X)) * Time);
                }
                
            }

            // Velocity += (float) (Acceleration * time.ElapsedGameTime.TotalSeconds);
            // if (Velocity < TerminalVelocity)
            //     Velocity = TerminalVelocity;
            Time += 2 * (float) time.ElapsedGameTime.TotalSeconds;
            transform.Pos = new Vector2(x, y);

            base.Update(time);
        }

    }
}
