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
        public float Rotation { get; set; }
        public Vector2 Origin { get; set; }
        public float Scale { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public float LayerDepth { get; set; }
         
        public Shape(Vector2 position, Color color, float rotation = 0.0f, Vector2? origin = null, float scale = 1.0f, SpriteEffects spriteEffect = SpriteEffects.None, float layerDepth = 0.0f) : base(position, color)
        {
            SetShapeValues(rotation, origin, scale, spriteEffect, layerDepth);
        }
        public Shape(Vector2 position, Align align, Color color, float rotation = 0.0f, Vector2? origin = null, float scale = 1.0f, SpriteEffects spriteEffect = SpriteEffects.None, float layerDepth = 0.0f) : base(position, align, color)
        {
            SetShapeValues(rotation, origin, scale, spriteEffect, layerDepth);
        }

        protected void SetShapeValues(float rotation, Vector2? origin, float scale, SpriteEffects spriteEffect, float layerDepth)
        {
            Rotation = rotation;
            Scale = scale;
            SpriteEffect = spriteEffect;
            LayerDepth = layerDepth;

            if (origin == null)
                Origin = Vector2.Zero;
            else
                Origin = (Vector2) origin;
        }

        public virtual Rectangle GetBoundingBox()
        {
            return new Rectangle(_position.ToPoint(), _size.ToPoint());
        }
    }
}
