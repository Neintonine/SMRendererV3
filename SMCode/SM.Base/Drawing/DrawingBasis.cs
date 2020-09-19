using SM.Base.Contexts;
using SM.Base.StaticObjects;
using SM.OGL.Mesh;

namespace SM.Base.Scene
{
    public class DrawingBasis<TTransformation> : IShowItem
        where TTransformation : GenericTransformation, new()
    {
        protected Material _material = new Material();
        protected Mesh _mesh = Plate.Object;

        public TTransformation Transform = new TTransformation();
        public virtual void Update(UpdateContext context)
        {

        }

        public virtual void Draw(DrawContext context)
        { }

        protected void ApplyContext(ref DrawContext context)
        {
            context.Material = _material;
            context.Mesh = _mesh;
        }
    }
}