using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Contexts;
using SM.Base.Drawing;
using SM.Base.Objects;
using SM.Base.Textures;
using SM2D.Object;
using SM2D.Scene;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawObject2D : DrawingBasis<Transformation>, I2DShowItem
    {
        public int ZIndex { get; set; }

        public bool ShadowCaster = false;

        public Texture Texture
        {
            get => (Texture) _material.Texture;
            set => _material.Texture = value;
        }

        public Color4 Color
        {
            get => _material.Tint;
            set => _material.Tint = value;
        }

        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.ShaderArguments["occluder"] = ShadowCaster;
            context.Shader.Draw(context);
        }

        public Material GetMaterialReference() => _material;
        public void SetMaterialReference(Material material) => _material = material;

        public void SetShader(MaterialShader shader) => _material.CustomShader = shader;

        public Polygon ApplyPolygon(ICollection<Vector2> vertices)
        {
            Polygon polygon = new Polygon(vertices);
            _mesh = polygon;
            return polygon;
        }
        public Polygon ApplyPolygon(ICollection<PolygonVertex> vertices)
        {
            Polygon polygon = new Polygon(vertices);
            _mesh = polygon;
            return polygon;
        }
        public void ApplyPolygon(Polygon polygon)
        {
            _mesh = polygon;
        }

        public void ApplyMesh(Mesh mesh) => _mesh = mesh;

        public Polygon ApplyCircle(int segments = 32)
        {
            Polygon pol = Polygon.GenerateCircle(segments);
            _mesh = pol;
            return pol;
        }
        
    }
}