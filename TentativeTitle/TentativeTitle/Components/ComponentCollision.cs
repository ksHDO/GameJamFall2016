using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle.Components
{
    class ComponentCollision : Component
    {
        //ComponentTransform _transform;
        public static List<ComponentCollision> ColliderList { get; set; } = new List<ComponentCollision>();


        RectangleF _boundingBox;
        public RectangleF Collider
        {
            get
            {
                return _boundingBox;
            }
        }

        public bool IsThisCollided { get; private set; } = false;



        public ComponentCollision() : base("collision")
        {
            _boundingBox = new RectangleF(0.0f, 0.0f, 0.0f, 0.0f);
            ColliderList.Add(this);
        }

        public override bool Load()
        {
            ComponentTransform _transform = _owner.GetComponent<ComponentTransform>();
            if (_transform == null)
            {
                Console.WriteLine(Name + ": ComponentTransform does not exist in owner [" + _owner.Name + "]");
                return false;
            }

            return true;
        }


        public void SetSize(float w, float h)
        {
            _boundingBox.Width  = w;
            _boundingBox.Height = h;
        }

        public override void Update(GameTime time)
        {
            IsThisCollided = false;
            Vector2 trans = GetSiblingComponent<ComponentTransform>().WorldTransform.Translate;

            _boundingBox.X = trans.X;
            _boundingBox.Y = trans.Y;

            

            //Center();
            //ComponentTransform trans = GetSiblingComponent<ComponentTransform>();
            //foreach (ComponentCollision collider in ColliderList)
            //{

            //    if (collider != null && collider != this)
            //    {
            //        if (IsCollided(collider))
            //        {
            //            Transform worldTrans =  transform.WorldTransform;
            //            //Vector2 vecBack = _lastPos - worldTrans.Translate;

            //            transform.Pos = LastPos;
            //            Vector2 dirCollision = collision.GetCollisionNormal(collider);
            //            if (Math.Abs(dirCollision.Y) == 1) physics.Velocity = new Vector2(physics.Velocity.X, 0.0f);
            //            else if (Math.Abs(dirCollision.X) == 1) physics.Velocity = new Vector2(0, physics.Velocity.Y);
            //        }
            //    }

            //}
        }

        public RectF_IntersectSide GetCollisionNormal(RectangleF other)
        {
            return Collider.IntersectSide(other);
        }

        public RectF_IntersectSide GetCollisionNormal(ComponentCollision other)
        {
            RectangleF otherCollider = other.Collider;


            return Collider.IntersectSide(otherCollider);


           // bool b_l_Contact = otherCollider.Contains(new Point(Collider.Left, Collider.Bottom));
           // bool b_r_Contact = otherCollider.Contains(new Point(Collider.Right, Collider.Bottom));
           // bool t_l_Contact = otherCollider.Contains(new Point(Collider.Left, Collider.Top));
           // bool t_r_Contact = otherCollider.Contains(new Point(Collider.Right, Collider.Top));

           //if(b_r_Contact)
           //{
           //     if (b_l_Contact) return new Vector2(0, 1.0f);
           //     if (t_r_Contact) return new Vector2(-1.0f, 0);
           //}
           //else if(t_l_Contact)
           //{
           //     if (b_l_Contact) return new Vector2(1.0f, 0);
           //     if (t_r_Contact) return new Vector2(0, -1.0f);
           //}

           //Console.WriteLine("Collider contacts failed!");
           //return new Vector2(0, 1.0f);

        }

        

        private void Center()
        {
            _boundingBox.X = _boundingBox.X - (_boundingBox.Width /2.0f);
            _boundingBox.Y = _boundingBox.Y - (_boundingBox.Height/2.0f);
        }

        public bool IsCollided(ComponentCollision other)
        {
            return Collider.Intersects(other.Collider);
        }

        public float GetOverlapAmountBottom(ComponentCollision other)
        {
            return Collider.Bottom - other.Collider.Top;
        }

        public float GetOverlapAmountTop(ComponentCollision other)
        {
            return other.Collider.Bottom - Collider.Top;
        }

        public float GetOverlapAmountRight(ComponentCollision other)
        {
            return Collider.Right - other.Collider.Left;
        }

        public float GetOverlapAmountLeft(ComponentCollision other)
        {
            return other.Collider.Right - Collider.Left;
        }



        public float GetOverlapAmountBottom(RectangleF other)
        {
            return Collider.Bottom - other.Top;
        }

        public float GetOverlapAmountTop(RectangleF other)
        {
            return other.Bottom - Collider.Top;
        }

        public float GetOverlapAmountRight(RectangleF other)
        {
            return Collider.Right - other.Left;
        }

        public float GetOverlapAmountLeft(RectangleF other)
        {
            return other.Right - Collider.Left;
        }



        private float Fabs(float f)
        {
            return f < 0 ? -f : f;
        }

        public float GetDistanceBottom(ComponentCollision other)
        {
            return Fabs(GetOverlapAmountBottom(other));
        }

        public float GetDistanceTop(ComponentCollision other)
        {
            return Fabs(GetOverlapAmountTop(other));
        }

        public float GetDistanceRight(ComponentCollision other)
        {
            return Fabs(GetOverlapAmountRight(other));
        }

        public float GetDistanceLeft(ComponentCollision other)
        {
            return Fabs(GetOverlapAmountLeft(other));
        }

        //----------------

        public float GetDistanceBottom(RectangleF other)
        {
            return Fabs(GetOverlapAmountBottom(other));
        }

        public float GetDistanceTop(RectangleF other)
        {
            return Fabs(GetOverlapAmountTop(other));
        }

        public float GetDistanceRight(RectangleF other)
        {
            return Fabs(GetOverlapAmountRight(other));
        }

        public float GetDistanceLeft(RectangleF other)
        {
            return Fabs(GetOverlapAmountLeft(other));
        }

    }
}
