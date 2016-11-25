using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TentativeTitle.Components;

namespace TentativeTitle.Entities
{
    class EntityPlatform : EntitySprite
    {
        public EntityPlatform(string name, Texture2D tex, Vector2 pos = new Vector2(), Vector2 boundingBox = new Vector2(), bool enabled = true) : base(name,tex,pos,enabled)
        {
            AddComponent(new ComponentCollision());
            GetComponent<ComponentCollision>().SetSize((int)boundingBox.X, (int)boundingBox.Y);
        }

    }
}
