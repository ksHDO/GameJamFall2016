using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TentativeTitle.Maps
{
    [Serializable]
    public struct Tile
    {
        /// <summary>
        /// ID of tires.
        /// </summary>
        public int ID { get; set; }
        // public int Y { get; set; }
        public bool Collidable { get; set; }
    }
}
