using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle
{

    enum RectF_IntersectSide
    {
        Left,
        Right,
        Bottom,
        Top,
        Inside,
        NoIntersection
    }

    class RectangleF
    {
        public float Width;
        public float Height;

        public float X;
        public float Y;

        public float Bottom
        {
            get
            {
                return Y + Height;
            }
        }

        public float Top
        {
            get
            {
                return Y;
            }
        }

        public float Right
        {
            get
            {
                return X + Width;
            }
        }

        public float Left
        {
            get
            {
                return X;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(X + (Width / 2.0f), Y + (Height / 2.0f));
            }
        }

        public Vector2 TopLeft
        {
            get
            {
                return new Vector2(Left, Top);
            }
        }

        public Vector2 TopRight
        {
            get
            {
                return new Vector2(Right, Top);
            }
        }

        public Vector2 BottomLeft
        {
            get
            {
                return new Vector2(Left, Bottom);
            }
        }

        public Vector2 BottomRight
        {
            get
            {
                return new Vector2(Right, Bottom);
            }
        }

        public Vector2 BottomMid
        {
            get
            {
                return new Vector2(X + Width/2.0f, Bottom);
            }
        }

        public Vector2 TopMid
        {
            get
            {
                return new Vector2(X + Width / 2.0f, Top);
            }
        }


        public Vector2 RightMid
        {
            get
            {
                return new Vector2(Right, Top + Height/2.0f);
            }
        }


        public Vector2 LeftMid
        {
            get
            {
                return new Vector2(Left, Top + Height / 2.0f);
            }
        }


        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;

            Width  = width;
            Height = height;
        }

        public RectangleF(RectangleF original, float offsetX, float offsetY)
        {
            X = original.X + offsetX;
            Y = original.Y + offsetY;

            Width = original.Width;
            Height = original.Height;
        }


        public bool Contains(float x, float y)
        {
            return (x < Right && x > Left && y < Bottom && y > Top);//Yay one line
        }
        public bool Contains(Vector2 pos)
        {
            return Contains(pos.X, pos.Y);
        }

        public bool Intersects(RectangleF other)
        {
            return (IntersectSide(other) != RectF_IntersectSide.NoIntersection);
        }

        public RectF_IntersectSide IntersectSide(RectangleF other)
        {

            //bool l = other.Contains(Left, Bottom);
            //bool r = other.Contains(Right, Bottom);
            //bool t = other.Contains(Left, Top);
            //bool b = other.Contains(Right, Top);



            bool b_l_Contact = other.Contains(Left, Bottom);
            bool b_r_Contact = other.Contains(Right, Bottom);
            bool t_l_Contact = other.Contains(Left,  Top);
            bool t_r_Contact = other.Contains(Right, Top);


            if (b_l_Contact && b_r_Contact && t_l_Contact && t_r_Contact) return RectF_IntersectSide.Inside;


            //if (b_r_Contact && b_l_Contact) return Bottom;


            if (b_r_Contact)
            {
                if (t_r_Contact || other.Contains(new Vector2(Right + .01f, Bottom - (Height / 4.0f)))) return RectF_IntersectSide.Right;
                if (b_l_Contact) return RectF_IntersectSide.Bottom;
                return RectF_IntersectSide.Bottom;
            }
            else if (b_l_Contact)
            {
                if (t_l_Contact || other.Contains(new Vector2(Left, Bottom - (Height / 2.0f)))) return RectF_IntersectSide.Left;
            }
            else if (t_l_Contact)
            {
                if (t_r_Contact) return RectF_IntersectSide.Top;
                //if (b_l_Contact) return RectF_IntersectSide.Left;
                else return RectF_IntersectSide.Top;
            }
            else if (t_r_Contact)
            {
                return RectF_IntersectSide.Top;
            }
            

            return RectF_IntersectSide.NoIntersection;
        }


    }
}
