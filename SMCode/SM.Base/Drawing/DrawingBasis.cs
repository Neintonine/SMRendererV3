#region usings

using System.Collections.Generic;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    ///     Contains general basis systems for drawing objects.
    /// </summary>
    public abstract class DrawingBasis : IShowItem
    {
        /// <summary>
        ///     The material it should use.
        /// </summary>
        protected Material _material = new Material();

        /// <summary>
        ///     The mesh it should use.
        /// </summary>
        protected GenericMesh _mesh = SMRenderer.DefaultMesh;

        /// <inheritdoc />
        public object Parent { get; set; }

        /// <inheritdoc />
        public string Name { get; set; } = "Unnamed draw object";

        /// <inheritdoc />
        public ICollection<string> Flags { get; set; }
        
        /// <inheritdoc />
        public void Draw(DrawContext context)
        {
            context.Material = _material;
            context.Mesh = _mesh;

            DrawContext(ref context);
        }

        /// <inheritdoc />
        public virtual void OnAdded(object sender)
        {
        }

        /// <inheritdoc />
        public virtual void OnRemoved(object sender)
        {
        }

        /// <summary>
        ///     Draws the context, that was given to them.
        /// </summary>
        /// <param name="context"></param>
        protected virtual void DrawContext(ref DrawContext context)
        {
        }
    }

    /// <summary>
    ///     Contains general basis systems for drawing objects.
    /// </summary>
    /// <typeparam name="TTransformation">The transformation type</typeparam>
    public abstract class DrawingBasis<TTransformation> : DrawingBasis
        where TTransformation : GenericTransformation, new()
    {
        /// <summary>
        ///     The current transformation.
        /// </summary>
        public TTransformation Transform = new TTransformation();

        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            context.ModelMaster = Transform.GetMatrix();
        }
    }
}