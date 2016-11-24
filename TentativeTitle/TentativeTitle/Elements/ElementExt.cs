using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Elements
{
    static class ElementExt
    {
        /// <summary>
        /// Requires the existance of a "MouseInput" class.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool IsClicked(this Shape element)
        {
            Vector2 mousePos = MouseInput.LastPos;
            Rectangle bounds = element.GetBoundingBox();
            return (mousePos.X >= bounds.Left && mousePos.Y >= bounds.Top) &&
                (mousePos.X < bounds.Right && mousePos.Y < bounds.Bottom);
        }
    }
}
