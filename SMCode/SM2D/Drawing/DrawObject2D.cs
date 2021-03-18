using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using SM.Base.Drawing;
using SM.Base.Shaders;
using SM.Base.Textures;
using SM.Base.Window;
using SM2D.Object;
using SM2D.Types;

namespace SM2D.Drawing
{
    public class DrawObject2D : DrawingBasis<Transformation>
    {
        public Texture Texture
        {
            get => (Texture) Material.Texture;
            set => Material.Texture = value;
        }

        public Color4 Color
        {
            get => Material.Tint;
            set => Material.Tint = value;
        }

        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            context.Shader.Draw(context);
        }
        
        public void SetShader(MaterialShader shader) => Material.CustomShader = shader;

        public Polygon ApplyPolygon(ICollection<Vector2> vertices, bool centerUVs = false)
        {
            Polygon polygon = new Polygon(vertices);
            Mesh = polygon;
            return polygon;
        }
        public Polygon ApplyPolygon(ICollection<PolygonVertex> vertices, bool centerUVs = false)
        {
            Polygon polygon = new Polygon(vertices);
            Mesh = polygon;
            return polygon;
        }
        public void ApplyPolygon(Polygon polygon)
        {
            Mesh = polygon;
        }

        public Polygon ApplyCircle(int segments = 32, bool centerUVs = false)
        {
            Polygon pol = Polygon.GenerateCircle(segments);
            Mesh = pol;
            return pol;
        }

    }
}