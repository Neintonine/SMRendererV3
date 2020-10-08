using System;
using System.Configuration.Assemblies;
using OpenTK;
using SM.Base.Scene;
using SM.Base.Types;
using SM.Utility;

namespace SM2D.Types
{
    public class Transformation : GenericTransformation
    {
        private float _eulerRotation = 0;
        private CVector2 _position = new CVector2(0);
        private CVector2 _scale = new CVector2(50);

        public CVector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public CVector2 Size
        {
            get => _scale;
            set => _scale = value;
        }
        public float Rotation
        {
            get => _eulerRotation;
            set => _eulerRotation = value;
        }

        protected override Matrix4 RequestMatrix()
        {
            return Matrix4.CreateScale(_scale.X, _scale.Y, 1) *
                   Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(_eulerRotation)) *
                   Matrix4.CreateTranslation(_position.X, _position.Y, 0);
        }

        public void TurnTo(Vector2 v)
        {
            _eulerRotation = RotationUtility.TurnTowards(Position, v);
        }

        public Vector2 LookAtVector()
        {
            if (_modelMatrix.Determinant < 0.0001) return new Vector2(0);

            Vector3 vec = Vector3.TransformNormal(Vector3.UnitX, _modelMatrix);
            vec.Normalize();
            return vec.Xy;
        }
    }
}