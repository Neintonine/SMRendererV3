using System.Collections.Generic;
using System.Drawing.Printing;
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
    /// <inheritdoc />
    public class DrawObject2D : DrawingBasis<Transformation>
    {
        /// <summary>
        /// The texture the object should use.
        /// </summary>
        public Texture Texture
        {
            get => (Texture) Material.Texture;
            set => Material.Texture = value;
        }

        /// <summary>
        /// The color or tint the object should use.
        /// </summary>
        public Color4 Color
        {
            get => Material.Tint;
            set => Material.Tint = value;
        }

        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            Material.Draw(context);
        }

        /// <summary>
        /// Applies a polygon to the object.
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public Polygon ApplyPolygon(ICollection<Vector2> vertices)
        {
            Polygon polygon = new Polygon(vertices);
            Mesh = polygon;
            return polygon;
        }
        
        /// <summary>
        /// Applies a polygon to the object.
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        public Polygon ApplyPolygon(ICollection<PolygonVertex> vertices)
        {
            Polygon polygon = new Polygon(vertices);
            Mesh = polygon;
            return polygon;
        }

        /// <summary>
        /// Applies a polygon to the object.
        /// </summary>
        public void ApplyPolygon(Polygon polygon)
        {
            Mesh = polygon;
        }

        /// <summary>
        /// This applies a circle.
        /// </summary>
        /// <param name="segments"></param>
        /// <returns></returns>
        public Polygon ApplyCircle(int segments = 32)
        {
            Polygon pol = Polygon.GenerateCircle(segments);
            Mesh = pol;
            return pol;
        }

    }
}