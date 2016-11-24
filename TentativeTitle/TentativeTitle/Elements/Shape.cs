using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Elements
{

    class Shape : ElementObject
    {
        protected Vector2 _size;
         
        public Shape(Vector2 position, Color color) : base(position, color)
        { }
        public Shape(Vector2 position, Align align, Color color) : base(position, align, color)
        { }

        public virtual Rectangle GetBoundingBox()
        {
            return new Rectangle(_position.ToPoint(), _size.ToPoint());
        }
    }
}
