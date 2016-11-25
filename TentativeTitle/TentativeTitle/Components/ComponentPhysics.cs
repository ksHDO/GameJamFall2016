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
        

        public float AccelerationX { get; set; } = 0.0f;
        public float AccelerationY { get; set; } = 0.0f;

        public float DragX { get; set; } = 0.99999f;
        public float DragY { get; set; } = 0.999999f;


        // public float Velocity { get; set; } = 40.0f;
        
        public float TerminalVelocity { get; set; } = 40.0f;

        public Vector2 Velocity { get; set; } = new Vector2(0.0f, 0.0f);


        // public float Angle { get; set; } = -30;
        public float Time { get; set; } = 0.0f;
        public float TimeSpeed { get; set; } = 5.0f;
        public Vector2 StartPos { get; set; } = new Vector2(100, 100);
        public float Mass { get; set; }

        public float Airtime { get; set; } = 0.0f;
        private bool _inAir = true;

        private bool _oldInAir = false;
        public bool InAir { get { return _inAir; } set { _oldInAir = _inAir; _inAir = value; } } 

        public bool WasInAirLastFrame { get; set; } = false;
        
        

        public ComponentPhysics(bool isEnabled = true) : base("physics", isEnabled)
        {
            
        }

        

        public override void Update(GameTime time)
        {
            float dt = (float) time.ElapsedGameTime.TotalSeconds;
            ComponentTransform transform = GetSiblingComponent<ComponentTransform>();

            //float x = 0;
            //float y = 0;
            //Vector2 vel;
            //float velY = 0;
            //float velX = 0;
            if (transform != null)
            {
                ComponentGravity gravity = GetSiblingComponent<ComponentGravity>();
                if (gravity != null)
                {
                    if (InAir)
                    {
                        AccelerationY = gravity.Gravity;
                    }
                    else if(_oldInAir)
                    {
                        _oldInAir = false;
                        WasInAirLastFrame = true;
                    }
                    else
                    {
                        WasInAirLastFrame = false;
                    }
                    

                    Velocity = new Vector2((Velocity.X + (AccelerationX * dt)) * DragX, (Velocity.Y - (AccelerationY * dt))*DragY);

                    if (Velocity.Length() > TerminalVelocity) Velocity = Velocity.Normalized().MultiplyLength(TerminalVelocity);

                    //velY = (float) ((Velocity.Y * Math.Sin(MathHelper.ToRadians(Velocity.X)) * Time - gravity.Gravity * Airtime * Airtime / 2.0));
                    //velX = (float) ((Velocity.Y * Math.Cos(MathHelper.ToRadians(Velocity.X)) * Time));
                    //velX = 
                }
                
            }
            //x = StartPos.X + Velocity.X;
            //y = StartPos.Y + Velocity.Y;

            //if(InAir)
            //{
            //    Airtime += TimeSpeed;
            //}

            
            
            //Time += TimeSpeed * dt;
            Vector2 oldPos = transform.Pos;
            transform.Pos = new Vector2(oldPos.X + (Velocity.X * dt), oldPos.Y + (Velocity.Y * dt));


            //base.Update(time);
        }


        public void AddVelocity(Vector2 vel)
        {
            Velocity += vel;
        }

    }
}
