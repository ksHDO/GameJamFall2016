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
        bool[] _sidesCollided = new bool[4];
        RectangleF[] _collidedRects = new RectangleF[4];
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
            float dt = (float)time.ElapsedGameTime.TotalSeconds;
            //ComponentMovement  movement  = GetSiblingComponent<ComponentMovement>();
            ComponentCollision collision = GetSiblingComponent<ComponentCollision>();
            ComponentTransform transform = GetSiblingComponent<ComponentTransform>();
            ComponentPhysics   physics   = GetSiblingComponent<ComponentPhysics>();

            bool inAir = true;

            //Vector2 oldPos = transform.Pos;
            //transform.Pos = new Vector2(oldPos.X + (Velocity.X * dt), oldPos.Y + (Velocity.Y * dt));


            //Only deal with one axis at a time
            #region newcollision

            bool right = (physics.Velocity.X >= 0);
            transform.Pos += new Vector2(physics.Velocity.X * dt, 0.0f);
            foreach (ComponentCollision collider in ComponentCollision.ColliderList)
            {
                if (collision != null && collider != null && collider != collision)
                {
                    //RectF_IntersectSide side = collision.GetCollisionNormal(collider);

                    bool collided = collider.Collider.Contains(((right) ? collision.Collider.RightMid : collision.Collider.LeftMid));
                        //(
                        //    (
                        //    collider.Collider.Contains(
                        //    ((right)? collision.Collider.TopRight : collision.Collider.TopLeft))
                        //    &&
                        //    ! collider.Collider.Contains(
                        //    ((!right) ? collision.Collider.TopRight : collision.Collider.TopLeft))
                        //    )
                        //|| 
                        //    (
                        //    collider.Collider.Contains(
                        //    ((right) ? collision.Collider.BottomRight : collision.Collider.BottomLeft))
                        //    &&
                        //    !collider.Collider.Contains(
                        //    ((!right) ? collision.Collider.BottomRight : collision.Collider.BottomLeft))
                        //    )
                        //);

                    if (collided)//(side == ((right)? RectF_IntersectSide.Right : RectF_IntersectSide.Left))
                    {
                        //Transform worldTrans = transform.WorldTransform;
                        float xDiff = ((right) ? collision.GetOverlapAmountRight(collider) : collision.GetOverlapAmountLeft(collider));
                       

                        transform.Pos += new Vector2(((right) ? -xDiff : xDiff), 0.0f);
                        physics.Velocity = new Vector2(0.0f, ((physics.InAir) ? physics.Velocity.Y : 0.0f ));
                        break;
                    }
                }
            }


            bool down = (physics.Velocity.Y >= 0);
            transform.Pos += new Vector2(0.0f, physics.Velocity.Y * dt);
            foreach (ComponentCollision collider in ComponentCollision.ColliderList)
            {
                if (collision != null && collider != null && collider != collision)
                {
                    //RectF_IntersectSide side = collision.GetCollisionNormal(collider);

                    //bool collided = collider.Collider.Contains(
                    //                       ((down) ? collision.Collider.BottomRight : collision.Collider.TopRight))
                    //                       &&
                    //                       collider.Collider.Contains(
                    //                       ((down) ? collision.Collider.BottomLeft : collision.Collider.TopLeft));



                    bool collided = collider.Collider.Contains(((down) ? collision.Collider.BottomMid : collision.Collider.TopMid));

                    //   (
                    //    (
                    //    collider.Collider.Contains(
                    //    ((down) ? collision.Collider.BottomRight : collision.Collider.TopRight))
                    //    &&
                    //    !collider.Collider.Contains(
                    //    ((!down) ? collision.Collider.BottomRight : collision.Collider.TopRight))
                    //    )
                    //||
                    //    (
                    //    collider.Collider.Contains(
                    //    ((down) ? collision.Collider.BottomLeft : collision.Collider.TopLeft))
                    //    &&
                    //    !collider.Collider.Contains(
                    //    ((!down) ? collision.Collider.BottomLeft : collision.Collider.TopLeft))
                    //    )
                    // );

                    if (collided)//(side == ((down) ? RectF_IntersectSide.Bottom : RectF_IntersectSide.Top))
                    {
                        if(down) inAir = false;
                        //Transform worldTrans = transform.WorldTransform;
                        float yDiff = ((down) ? collision.GetOverlapAmountBottom(collider) : collision.GetOverlapAmountTop(collider));
                        transform.Pos += new Vector2(0.0f, ((down) ? -yDiff : yDiff));
                        physics.Velocity = new Vector2(((physics.InAir) ? 0.0f : physics.Velocity.X), 0.0f);
                        break;
                    }
                }
            }

            


            #endregion



            //_sidesCollided[0] = false;
            //_sidesCollided[1] = false;
            //_sidesCollided[2] = false;
            //_sidesCollided[3] = false;

            //_collidedRects[0] = null;
            //_collidedRects[1] = null;
            //_collidedRects[2] = null;
            //_collidedRects[3] = null;


            //foreach (ComponentCollision collider in ComponentCollision.ColliderList)
            //{
            //    if(collision != null && collider != null && collider != collision)
            //    {
            //        if (collision.IsCollided(collider))
            //        {
            //            Transform worldTrans = transform.WorldTransform;
            //            //Vector2 vecBack = _lastPos - worldTrans.Translate;

            //            RectF_IntersectSide dirCollision = collision.Collider.IntersectSide(collider.Collider);

            //            switch (dirCollision)
            //            {
            //                case RectF_IntersectSide.Left:
            //                    //transform.Pos += new Vector2((collision.GetOverlapAmountRight(collider)), 0.0f);
            //                    //physics.Velocity = new Vector2(0, ((physics.InAir) ? 0.0f : physics.Velocity.Y)); //((physics.WasInAirLastFrame) ? 0.0f : physics.Velocity.Y)
            //                    _sidesCollided[0] = true;
            //                    _collidedRects[0] = collider.Collider;
            //                    break;
            //                case RectF_IntersectSide.Right:
            //                    //transform.Pos += new Vector2(-(collision.GetOverlapAmountRight(collider)), 0.0f);
            //                    //physics.Velocity = new Vector2(0, ((physics.InAir) ? 0.0f : physics.Velocity.Y));
            //                    _sidesCollided[2] = true;
            //                    _collidedRects[2] = collider.Collider;
            //                    break;
            //                case RectF_IntersectSide.Inside:
            //                    //transform.Pos = LastPos + (physics.Velocity.MultiplyLength(.5f) * (float)time.ElapsedGameTime.TotalSeconds);
            //                    //physics.Velocity = physics.Velocity;
            //                    transform.Pos -= new Vector2(0.0f, (collision.GetOverlapAmountBottom(collider)));
            //                    //physics.Velocity = new Vector2(0, 0);
            //                    //physics.AccelerationX = 0.0f;
            //                    //physics.AccelerationY = 0.0f;

            //                    //physics.InAir = false;
            //                    break;
            //                case RectF_IntersectSide.Bottom:
            //                    //float temp = ((physics.InAir) ? 0.0f : physics.Velocity.X);
            //                    _sidesCollided[3] = true;
            //                    _collidedRects[3] = collider.Collider;

            //                    //transform.Pos += new Vector2(0.0f, -(collision.GetOverlapAmountBottom(collider)));
            //                    //physics.Velocity = new Vector2(((physics.InAir) ? 0.0f : physics.Velocity.X), 0.0f);
            //                    break;
            //                case RectF_IntersectSide.Top:
            //                    _sidesCollided[1] = true;
            //                    _collidedRects[1] = collider.Collider;

            //                    //transform.Pos += new Vector2(0.0f, (collision.GetOverlapAmountBottom(collider)));
            //                    //physics.Velocity = new Vector2(((physics.InAir) ? 0.0f : physics.Velocity.X), 0.0f);
            //                    break;
            //                default:
            //                    break;
            //            }
            //            //RectF_IntersectSide checkBelowResult = new RectangleF(collider.Collider, 0.0f, 0.1f).IntersectSide(collision.Collider);
            //            inAir = !(/*checkBelowResult == RectF_IntersectSide.Bottom || */dirCollision == RectF_IntersectSide.Inside);
                        
            //        }
            //    }
            //}

            ////Left
            //if (_sidesCollided[0])
            //{
            //    transform.Pos += new Vector2((collision.GetDistanceLeft(_collidedRects[0])),0.0f);
            //    physics.Velocity = new Vector2(0.0f, ((physics.InAir) ? 0.0f : physics.Velocity.Y));
            //}
            ////Top
            //if (_sidesCollided[1])
            //{
            //    //float temp = ((physics.InAir) ? 0.0f : physics.Velocity.X);
            //    transform.Pos += new Vector2(0.0f, (collision.GetDistanceTop(_collidedRects[1])));
            //    physics.Velocity = new Vector2(((physics.InAir) ? 0.0f : physics.Velocity.X), 0.0f);
            //}
            ////Right
            //if (_sidesCollided[2])
            //{
            //    transform.Pos += new Vector2(-(collision.GetDistanceRight(_collidedRects[2])), 0.0f);
            //    physics.Velocity = new Vector2(0, ((physics.InAir) ? 0.0f : physics.Velocity.Y));
            //}
            ////Bottom
            //if (_sidesCollided[3])
            //{
            //    //float temp = ((physics.InAir) ? 0.0f : physics.Velocity.X);
            //    transform.Pos += new Vector2(0.0f, -(collision.GetDistanceBottom(_collidedRects[3])));
            //    physics.Velocity = new Vector2(((physics.InAir) ? 0.0f : physics.Velocity.X), 0.0f);
            //}

            physics.InAir = inAir;

            LastPos = transform.Pos;
        }


    }

}
