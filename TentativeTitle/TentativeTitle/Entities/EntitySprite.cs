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
    class EntitySprite : Entity
    {
        ComponentTransform _transform;
        public EntitySprite(string name, Texture2D tex, Vector2 pos = new Vector2(), bool enabled = true) : base(name,enabled)
        {
            //AddComponent(new ComponentPhysics());
            //AddComponent(new ComponentGravity());
            _transform = new ComponentTransform();
            AddComponent(_transform, false);
            AddComponent(new ComponentSprite(tex));
            GetComponent<ComponentTransform>().Pos = pos;
        }

       // public override 

        /// <summary>
        /// Returns Texture2D for disposal
        /// </summary>
        /// <param name="tex"></param>
        /// <returns></returns>
        public Texture2D SetTexture(Texture2D tex)
        {
            return GetComponent<ComponentSprite>().SetTexture(tex);
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            _transform.Update(time);

        }

        //public override bool Initialize()
        //{

        //}

        public override void Draw(SpriteBatch batch)
        {
            Texture2D tex = GetComponent<ComponentSprite>().Texture;
            ComponentTransform trans = GetComponent<ComponentTransform>();
            if (trans != null)
            {
                Transform worldTrans = trans.WorldTransform;
                batch.Draw(tex, (worldTrans.Translate), null, Color.White, worldTrans.Rotate, new Vector2(), worldTrans.Scale, SpriteEffects.None, 0.0f);
            }
        }

    }
}
