using SM.Base.Contexts;
using SM.Base.Objects.Static;
using SM.OGL.Mesh;

namespace SM.Base.Scene
{
    /// <summary>
    /// Contains general basis systems for drawing objects.
    /// </summary>
    public abstract class DrawingBasis : IShowItem
    {
        /// <summary>
        /// The material it should use.
        /// </summary>
        protected Material _material = new Material();
        /// <summary>
        /// The mesh it should use.
        /// </summary>
        protected GenericMesh _mesh = Defaults.DefaultMesh;

        /// <inheritdoc />
        public virtual void Update(UpdateContext context)
        {

        }

        /// <inheritdoc />
        public virtual void Draw(DrawContext context)
        {
        }

        /// <summary>
        /// Applies the current settings to the context.
        /// </summary>
        /// <param name="context"></param>
        protected void ApplyContext(ref DrawContext context)
        {
            _material.Shader ??= Defaults.DefaultShader;

            context.Material = _material;
            context.Mesh = _mesh;
        }
    }
    /// <summary>
    /// Contains general basis systems for drawing objects.
    /// </summary>
    /// <typeparam name="TTransformation">The transformation type</typeparam>
    public abstract class DrawingBasis<TTransformation> : DrawingBasis
        where TTransformation : GenericTransformation, new()
    {
        /// <summary>
        /// The current transformation.
        /// </summary>
        public TTransformation Transform = new TTransformation();
    }
}