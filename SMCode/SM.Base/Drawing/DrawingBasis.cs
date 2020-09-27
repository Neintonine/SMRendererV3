using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.OGL.Mesh;

namespace SM.Base.Scene
{
    public abstract class DrawingBasis : IShowItem
    {
        protected Material _material = new Material();
        protected GenericMesh _mesh = Defaults.DefaultMesh;
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
    public abstract class DrawingBasis<TTransformation> : DrawingBasis
        where TTransformation : GenericTransformation, new()
    {
        public TTransformation Transform = new TTransformation();
    }
}