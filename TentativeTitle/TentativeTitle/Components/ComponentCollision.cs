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
            _transform = _owner.GetComponent<ComponentTransform>();
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
            _boundingBox.X = _boundingBox.X - (int) (_boundingBox.Width /2.0f);
            _boundingBox.Y = _boundingBox.Y - (int) (_boundingBox.Height/2.0f);
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

    }
}
