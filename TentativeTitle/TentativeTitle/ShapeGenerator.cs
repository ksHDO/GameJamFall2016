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

        public static Texture2D GenerateCircle(int radius, Color color)
        {
            int diameter = radius * 2;
            Texture2D circle = new Texture2D(_graphics, diameter, diameter);
            Color[] data = new Color[diameter * diameter];

            for (int i = 0; i < data.Length; i++)
                data[i] = Color.Transparent;

            double step = 1.0f / radius;

            for (double angle = 0; angle < Math.PI * 2; angle += step)
            {
                int x = (int)(radius + radius * Math.Cos(angle));
                int y = (int)(radius + radius * Math.Sin(angle));

                data[y * diameter + x] = color;
            }

            for (int i = 0; i < diameter; i++)
            {
                int yStart = -1;
                int yEnd = -1;

                for (int j = 0; j < diameter; j++)
                {
                    if (yStart == -1)
                    {
                        if (j == diameter - 1)
                        {
                            break;
                        }

                        if (data[i + (j * diameter)] == color &&
                            data[i + ((j + 1) * diameter)] == Color.Transparent)
                        {
                            yStart = j + 1;
                            continue;
                        }
                    }
                    else if (data[i + (j * diameter)] == color)
                    {
                        yEnd = j;
                        break;
                    }
                }
                if (yStart != -1 && yEnd != -1)
                {
                    for (int j = yStart; j < yEnd; j++)
                    {
                        data[i + (j * diameter)] = color;
                    }
                }
            }

            circle.SetData(data);
            return circle;
        }
    }
}
