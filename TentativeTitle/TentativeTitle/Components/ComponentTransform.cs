using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TentativeTitle.Components
{

    struct Transform
    {
        Vector2 _translate;
        float _rotate;
        float _scale;

       public Vector2 Translate
        {
            set
            {
                _translate = value;
            }
            get
            {
                return _translate;
            }
        }

        public float Rotate
        {
            set
            {
                _rotate = value;
            }
            get
            {
                return _rotate;
            }
        }

        public float Scale
        {
            set
            {
                _scale = value;
            }
            get
            {
                return _scale;
            }
        }

        public Transform(Vector2 translate = new Vector2(), float rotate = 0.0f, float scale = 1.0f)
        {
            _translate = translate;
            _rotate = rotate;
            _scale = scale;
        }

        //public static Transform operator +(Transform t1, Transform t2)
        //{
            
        //    return new Transform(t1._translate + t2._translate , t1._rotate + t2._rotate, t1.Scale);
        //}

    }

    class ComponentTransform : Component
    {

        Transform           _transform;
        ComponentTransform  _parent;


        public float X
        {
            get
            {
                return _transform.Translate.X;
            }
        }

        public float Y
        {
            get
            {
                return _transform.Translate.Y;
            }
        }

        public float Scale
        {
            get
            {
                return _transform.Scale;
            }
            set
            {
                _transform.Scale = value;
            }
        }

        public Transform LocalTransform
        {
            get
            {
                return _transform;
            }
        }

        public Transform WorldTransform
        {
            get
            {
                if(_parent == null)
                {
                    return _transform;
                }
                else
                {
                    // _transform + _parent.WorldTransform;


                    return (this + _parent);
                }
            }
        }

        public Vector2 Pos
        {
            get
            {
                return WorldTransform.Translate;
            }
            set
            {
                _transform.Translate = value;
            }
        }

        public ComponentTransform() : base("transform")
        {
            _transform = new Transform(new Vector2(),0.0f,1.0f);
        }

        void SetParent(ComponentTransform parent = null)
        {
            _parent = parent;
        }
        //public static ComponentTransform operator +(ComponentTransform t1, ComponentTransform t2)
        //{
        //Matrix.CreateRotationZ(MathHelper.ToRadians(t2..Rotate))


        //    return new Transform(t1._translate + t2._translate , t1._rotate + t2._rotate, t1.Scale);
        //}

        public override void Update(GameTime time)
        {
            
        }


        public static Transform operator +(ComponentTransform child, ComponentTransform parent)
        {
            Matrix rotation = Matrix.CreateRotationZ(MathHelper.ToRadians(parent.WorldTransform.Rotate));
            Transform parentTrans = parent.WorldTransform;
            float parScale = parentTrans.Scale;
            Vector2 translate = parentTrans.Translate + (Vector2.Transform(child.LocalTransform.Translate, rotation) * parScale);
            float rotate = parentTrans.Rotate + child.LocalTransform.Rotate;
            return new Transform(translate, rotate, parScale * child.Scale);
        }

    }
}
