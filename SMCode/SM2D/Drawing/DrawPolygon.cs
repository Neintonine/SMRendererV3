using System.Drawing;
using OpenTK.Graphics;
using SM.Base.Textures;
using SM.OGL.Texture;
using SM2D.Object;

namespace SM2D.Drawing
{
    public class DrawPolygon : DrawColor
    {
        public Polygon Polygon
        {
            get => (Polygon)_mesh;
            set => _mesh = value;
        }

        public Texture Texture
        {
            get => (Texture)_material.Texture;
            set => _material.Texture = value;
        }

        public DrawPolygon(Polygon polygon) {}

        public DrawPolygon(Polygon polygon, Bitmap map) : this(polygon, map, Color4.White) {}

        public DrawPolygon(Polygon polygon, Color4 color) : base(color)
        {
            _mesh = polygon;
        }
        public DrawPolygon(Polygon polygon, Bitmap map, Color4 tint) : base(tint)
        {
            _mesh = polygon;

            _material.Texture = new Texture(map);
        }
    }
}