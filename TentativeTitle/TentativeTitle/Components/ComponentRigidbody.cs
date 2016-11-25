using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle.Components
{

    struct ConstantForce
    {
        public Vector2 Direction { get; set; }
        public float Speed { get; set; }

        public Vector2 Velocity
        {
            get
            {
                return Direction.MultiplyLength(Speed);
            }
        }

        public ConstantForce(Vector2 dir, float speed)
        {
            Direction = dir;
            Speed = speed;
        }

    }

    class ComponentRigidbody : Component
    {

        public Vector2 LastPos { get; set; } = new Vector2();

        private bool[] _wasCollided = new bool[4];
        //private bool _wasCollided_l = false;
        //private bool _wasCollided_r = false;
        //private bool _wasCollided_t = false;
        //private bool _wasCollided_b = false;

        private bool[] _isCollided = new bool[4];
        //private bool _isCollided_l = false;
        //private bool _isCollided_r = false;
        //private bool _isCollided_t = false;
        //private bool _isCollided_b = false;

        //public float Mass { get; private set; }

        //private List<ConstantForce>


        public ComponentRigidbody() : base("rigidbody")
        {
            
        }

        public override void Update(GameTime time)
        {
            
            //ComponentMovement  movement  = GetSiblingComponent<ComponentMovement>();
            ComponentCollision collision = GetSiblingComponent<ComponentCollision>();
            ComponentTransform transform = GetSiblingComponent<ComponentTransform>();
            ComponentPhysics   physics   = GetSiblingComponent<ComponentPhysics>();

            bool inAir = true;

            foreach(ComponentCollision collider in ComponentCollision.ColliderList)
            {
                if(collision != null && collider != null && collider != collision)
                {
                    if (collision.IsCollided(collider))
                    {
                        Transform worldTrans = transform.WorldTransform;
                        //Vector2 vecBack = _lastPos - worldTrans.Translate;

                        RectF_IntersectSide dirCollision = collision.Collider.IntersectSide(collider.Collider);

                        switch (dirCollision)
                        {
                            case RectF_IntersectSide.Left:
                                transform.Pos += new Vector2((collision.GetOverlapAmountRight(collider)), 0.0f);
                                physics.Velocity = new Vector2(0, ((physics.InAir) ? 0.0f : physics.Velocity.Y)); //((physics.WasInAirLastFrame) ? 0.0f : physics.Velocity.Y)
                                break;
                            case RectF_IntersectSide.Right:
                                transform.Pos += new Vector2(-(collision.GetOverlapAmountRight(collider)), 0.0f);
                                physics.Velocity = new Vector2(0, ((physics.InAir) ? 0.0f : physics.Velocity.Y));
                                break;
                            case RectF_IntersectSide.Inside:
                                //transform.Pos = LastPos + (physics.Velocity.MultiplyLength(.5f) * (float)time.ElapsedGameTime.TotalSeconds);
                                //physics.Velocity = physics.Velocity;
                                transform.Pos -= new Vector2(0.0f, (collision.GetOverlapAmountBottom(collider)));
                                //physics.Velocity = new Vector2(0, 0);
                                //physics.AccelerationX = 0.0f;
                                //physics.AccelerationY = 0.0f;

                                //physics.InAir = false;
                                break;
                            case RectF_IntersectSide.Bottom:
                                //float temp = ((physics.InAir) ? 0.0f : physics.Velocity.X);
                                transform.Pos += new Vector2(0.0f, -(collision.GetOverlapAmountBottom(collider)));
                                physics.Velocity = new Vector2(((physics.InAir) ? 0.0f : physics.Velocity.X), 0.0f);
                                break;
                            case RectF_IntersectSide.Top:
                                transform.Pos += new Vector2(0.0f, (collision.GetOverlapAmountBottom(collider)));
                                physics.Velocity = new Vector2(((physics.InAir) ? 0.0f : physics.Velocity.X), 0.0f);
                                break;
                            default:
                                break;
                        }
                        //RectF_IntersectSide checkBelowResult = new RectangleF(collider.Collider, 0.0f, 0.1f).IntersectSide(collision.Collider);
                        inAir = !(/*checkBelowResult == RectF_IntersectSide.Bottom || */dirCollision == RectF_IntersectSide.Inside);
                        break;
                    }
                }
            }

            physics.InAir = inAir;

            LastPos = transform.Pos;
        }


    }

}
