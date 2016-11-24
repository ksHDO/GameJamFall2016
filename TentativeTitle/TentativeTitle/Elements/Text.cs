using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Elements
{
    enum TextAlignment
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

    class Text : Shape
    {
        private SpriteFont _font;
        private string _text;
        private TextAlignment _textAlignment = TextAlignment.Left;

        public SpriteFont Font
        {
            set { _font = value; }
        }
        public string String
        {
            set
            {
                _text = value;
                _size = _font.MeasureString(_text);
            }
        }

        public void SetTextAlignment(TextAlignment textAlign)
        { _textAlignment = textAlign; }

        public Text(SpriteFont font, string text, Vector2 position, Color color) : this(font, text, position, color, TextAlignment.Left)
        {
        }

        public Text(SpriteFont font, string text, Vector2 position, Align align, Color color) : this(font, text, position, align, color, TextAlignment.Left)
        {
        }

        public Text(SpriteFont font, string text, Vector2 position, Color color, TextAlignment textAlign) : base(position, color)
        {
            Font = font;
            String = text;
            SetTextAlignment(textAlign);
        }

        public Text(SpriteFont font, string text, Vector2 position, Align align, Color color, TextAlignment textAlign) : base(position, align, color)
        {
            Font = font;
            String = text;
            SetTextAlignment(textAlign);
        }

        private Vector2 AlignText()
        {
            Vector2 output = Vector2.Zero;
            Vector2 measurement = _size;

            int vertAlign = VAlignToInt(_textAlignment);
            int horzAlign = HAlignToInt(_textAlignment);

            if (vertAlign != 0)
            {
                // x = width, y = height i think
                if (vertAlign == 1)
                {
                    output.X = (-(measurement.X / 2));
                }
                else if (vertAlign == 2)
                {
                    output.X = -measurement.X;
                }
            }
            if (horzAlign != 0)
            {
                if (horzAlign == 1)
                {
                    output.Y = (-(measurement.Y / 2));
                }
                else if (horzAlign == 2)
                {
                    output.Y = -measurement.Y;
                }
            }

            return output;
        }


        private static int VAlignToInt(TextAlignment alignment = TextAlignment.Left)
        {
            int value = (int)alignment;
            while (value >= 10)
                value -= 10;
            if (value < 0)
                value = 0;
            return value % 3;
        }
        private static int HAlignToInt(TextAlignment alignment = TextAlignment.Left)
        {
            int value = ((int)alignment) / 10;
            return VAlignToInt((TextAlignment)value);
        }

        override public void Draw(SpriteBatch batch)
        {
            batch.DrawString(_font, _text, _position + AlignText(), _color);
        }

    }
}
