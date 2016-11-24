﻿using System;
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
            get
            {
                return _translate;
            }
        }

        public float Rotate
        {
            get
            {
                return _rotate;
            }
        }

        public float Scale
        {
            get
            {
                return _scale;
            }
        }

        public Transform(Vector2 translate = new Vector2(), float rotate = 0.0f, float scale = 0.0f)
        {
            _translate = translate;
            _rotate = rotate;
            _scale = scale;
        }

        public static Transform operator +(Transform t1, Transform t2)
        {
            return new Transform(t1._translate + t2._translate, t1._rotate + t2._rotate);
        }

    }

    class ComponentTransform : Component
    {

        Transform           _transform;
        ComponentTransform  _parent;

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
                    return _transform + _parent.WorldTransform;
                }
            }
        }

        public ComponentTransform() : base("transform")
        {
            _transform = new Transform();
        }

        void SetParent(ComponentTransform parent = null)
        {
            _parent = parent;
        }



    }
}
