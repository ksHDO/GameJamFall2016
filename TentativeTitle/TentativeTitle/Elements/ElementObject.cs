using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Elements
{
    enum Align
    {
        LeftTop = 0, CenterTop = 1, RightTop = 2,
        LeftMid = 10, CenterMid = 11, RightMid = 12,
        LeftBottom = 20, CenterBottom = 21, RightBottom = 22,

        Left = LeftTop,
        Center = CenterTop,
        Right = RightTop,

        Top = LeftTop,
        Mid = LeftMid,
        Bottom = LeftBottom
    }

    class ElementObject
    {
        protected Effect _effect;
        /// <summary>
        /// Position of the object without Alignment,
        /// </summary>
        private Vector2 _rawPosition;
        /// <summary>
        /// Position of the object with Alignment.
        /// </summary>
        protected Vector2 _position;
        protected Color _color;
        protected Align _align;

        public ElementObject(Vector2 position, Color color) : this(position, Align.Left, color)
        { }

        public ElementObject(Vector2 position, Align align, Color color)
        {
            SetAlign(align);
            SetPosition(position);
            SetColor(color);
        }

        public void SetColor(Color color)
        {
            _color = color;
        }
        public void SetPosition(Vector2 position)
        {
            _rawPosition = position;
            _position = _position + GetAlignedPosition();
        }

        public void SetEffect(Effect effect)
        {
            _effect = effect;
        }

        public void SetAlign(Align align)
        {
            _align = align;
        }


        /// <summary>
        /// HIGHLY recommended use for all classes inheriting Shape that override the Draw method.
        /// </summary>
        /// <returns></returns>
        protected Vector2 GetAlignedPosition()
        {
            // Access current window width and height.
            Settings currentSettings = Settings.GetSingleton();

            int width = currentSettings.DisplayWidth;
            int height = currentSettings.DisplayHeight;
            int vAlign = VAlignToInt(_align);
            int hAlign = HAlignToInt(_align);

            Vector2 position = new Vector2(0, 0);

            if (vAlign != 0)
            {
                if (vAlign == 1)
                {
                    position.X = width / 2;
                }
                else if (vAlign == 2)
                {
                    position.X = width;
                }
            }
            if (hAlign != 0)
            {
                if (vAlign == 1)
                {
                    position.Y = height / 2;
                }
                else if (vAlign == 2)
                {
                    position.Y = height;
                }
            }

            return position;
        }

        private static int VAlignToInt(Align alignment = Align.Left)
        {
            int value = (int)alignment;
            while (value >= 10)
                value -= 10;
            if (value < 0)
                value = 0;
            return value % 3;
        }
        private static int HAlignToInt(Align alignment = Align.Left)
        {
            int value = ((int)alignment) / 10;
            return VAlignToInt((Align)value);
        }

        // Don't use if it's a text class.
        virtual public void Draw(SpriteBatch batch)
        {
            
        }
    }
}
