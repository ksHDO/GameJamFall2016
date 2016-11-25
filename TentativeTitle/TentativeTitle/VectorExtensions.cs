using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle
{
    
    public static class VectorExtensions
    {
        public static Vector2 MultiplyLength(this Vector2 v, float d)
        {
            return new Vector2(v.X * d, v.Y * d);
        }

        public static Vector2 Normalized(this Vector2 v)
        {
            Vector2 temp = new Vector2(v.X,v.Y);
            temp.Normalize();
            return temp;
        }

        

    }
}
