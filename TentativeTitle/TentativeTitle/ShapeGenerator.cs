using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle
{
    public class ShapeGenerator
    {
        private static GraphicsDevice _graphics;

        public static bool Initialize(GraphicsDevice graphics)
        {
            _graphics = graphics;
            return true;
        }

        public static Texture2D GenerateRectangle(int width, int height, Color color)
        {
            Texture2D rect = new Texture2D(_graphics, width, height);
            Color[] data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
                data[i] = color;

            rect.SetData(data);
            return rect;
        }

        public static Texture2D GenerateBorder(int width, int height, int borderWidth, Color color)
        {
            Texture2D rect = new Texture2D(_graphics, width, height);
            Color[] data = new Color[width * height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bool colored = false;
                    for (int k = 0; k <= borderWidth; k++)
                    {
                        if (i == k || j == k || i == width - 1 - k || j == height - 1 - k)
                        {
                            data[i + j * width] = color;
                            colored = true;
                            break;
                        }
                    }

                    if (!colored)
                        data[i + j * width] = Color.Transparent;
                }
            }

            rect.SetData(data);
            return rect;
        }
    }
}
