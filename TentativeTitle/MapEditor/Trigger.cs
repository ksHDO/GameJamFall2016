using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    [Serializable]
    class Trigger
    {
        public int ID { get; set; }
        public Point Location { get; set; }
    }
}
