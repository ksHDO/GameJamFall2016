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

        Rectangle _boundingBox;
        public Rectangle Collider
        {
            get
            {
                return _boundingBox;
            }
        }
        ComponentTransform _transform;


        public ComponentCollision() : base("collision")
        {
            _boundingBox = new Rectangle(0, 0, 0, 0);
        }

        public override bool Load()
        {
            _transform = (ComponentTransform)_owner.GetComponent<ComponentTransform>();
            if (_transform == null)
            {
                Console.WriteLine(Name + ": ComponentTransform does not exist in owner [" + _owner.Name + "]");
                return false;
            }

            return true;
        }


        public void SetSize(int w, int h)
        {
            _boundingBox.Width  = w;
            _boundingBox.Height = h;
        }

        public override void Update(GameTime time)
        {
            Point trans = _transform.WorldTransform.Translate.ToPoint();

            _boundingBox.X = trans.X;
            _boundingBox.Y = trans.Y;

            Center();
        }

        private void Center()
        {
            _boundingBox.X = _boundingBox.X - _boundingBox.Width/2;
            _boundingBox.Y = _boundingBox.Y - _boundingBox.Height/2;
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


        //----------------- This is a meme: lol

        private float Fabs(float f)
        {
            if (f < 0) return -f;
            else return f;
        }

        public float GetDistanceBottom(ComponentCollision other)
        {
            return Fabs(Collider.Bottom - other.Collider.Top);
        }

        public float GetDistanceTop(ComponentCollision other)
        {
            return Fabs(Collider.Top - other.Collider.Bottom);
        }

        public float GetDistanceRight(ComponentCollision other)
        {
            return Fabs(Collider.Right - other.Collider.Left);
        }

        public float GetDistanceLeft(ComponentCollision other)
        {
            return Fabs(other.Collider.Right - Collider.Left);
        }

    }
}
