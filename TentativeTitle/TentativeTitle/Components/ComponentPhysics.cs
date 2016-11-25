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
        public float TimeSpeed { get; set; } = 5.0f;
        public Vector2 StartPos { get; set; } = new Vector2(100, 100);
        public float Mass { get; set; }

        public ComponentPhysics(bool isEnabled = true) : base("physics", isEnabled)
        {
            
        }

        public override void Update(GameTime time)
        {
            float dt = (float) time.ElapsedGameTime.TotalSeconds;
            ComponentTransform transform = GetSiblingComponent<ComponentTransform>();
            float x = 0;
            float y = 0;
            Vector2 vel;
            float velY = 0;
            float velX = 0;
            if (transform != null)
            {
                ComponentGravity gravity = GetSiblingComponent<ComponentGravity>();
                if (gravity != null)
                {
                    velY = (float) ((Velocity.Y * Math.Sin(MathHelper.ToRadians(Velocity.X)) * Time - gravity.Gravity * Time * Time / 2.0));
                    velX = (float) ((Velocity.Y * Math.Cos(MathHelper.ToRadians(Velocity.X)) * Time));
                }
                
            }
            x = StartPos.X + velX;
            y = StartPos.Y + velY;

            Time += TimeSpeed * dt;
            transform.Pos = new Vector2(x, y);

            

            base.Update(time);
        }

    }
}
