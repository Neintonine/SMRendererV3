﻿using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Mesh;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawComplex: DrawingBasis<Transformation>, I2DShowItem
    {
        public int ZIndex { get; set; }

        public Material Material
        {
            get => _material;
            set => _material = value;
        }

        public GenericMesh Mesh
        {
            get => _mesh;
            set => _mesh = value;
        }

        protected override void DrawContext(ref DrawContext context)
        {
            context.Instances[0].ModelMatrix = Transform.GetMatrix();

            context.Shader.Draw(context);
        }
    }
}