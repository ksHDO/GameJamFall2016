using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TentativeTitle.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TentativeTitle.Entities
{
    class EntityProjectile : EntitySprite
    {
        public EntityProjectile(string name, Texture2D tex, Vector2 pos = new Vector2(), Vector2 boundingBox = new Vector2(), bool enabled = true) : base(name,tex,pos,enabled)
        {
            AddComponent(new ComponentCollision());
            AddComponent(new ComponentPhysics());
            AddComponent(new ComponentGravity());
            AddComponent(new ComponentRigidbody());
            GetComponent<ComponentCollision>().SetSize(boundingBox.X, boundingBox.Y);
            GetComponent<ComponentRigidbody>().LastPos = GetComponent<ComponentTransform>().Pos;

        }


    }

}
