#region usings

using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Scene;
using SM.Base.Window;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    ///     Contains general basis systems for drawing objects.
    /// </summary>
    public abstract class DrawingBasis : IShowItem, IModelItem
    {
        /// <summary>
        ///     The material it should use.
        /// </summary>
        public Material Material = new Material();

        /// <summary>
        /// Transformation for the textures.
        /// </summary>
        public TextureTransformation TextureTransform = new TextureTransformation();

        /// <summary>
        /// This allows custom shaders to add own arguments.
        /// </summary>
        public ShaderArguments ShaderArguments => Material.ShaderArguments;

        /// <summary>
        /// This can force a shader to render the object with the specified mesh type.
        /// </summary>
        public PrimitiveType? ForcedMeshType { get; set; }


        /// <summary>
        ///     The mesh it should use.
        /// </summary>
        public GenericMesh Mesh { get; set; } = SMRenderer.DefaultMesh;

        /// <inheritdoc />
        public object Parent { get; set; }

        /// <inheritdoc />
        public string Name { get; set; } = "Unnamed draw object";

        /// <inheritdoc />
        public ICollection<string> Flags { get; set; }

        /// <summary>
        ///     This value determents if the object should draw something.
        /// </summary>
        public bool Active { get; set; } = true;

        public bool RenderActive { get; set; } = true;

        /// <inheritdoc />
        public void Draw(DrawContext context)
        {
            context.Material = Material;
            context.Mesh = Mesh;

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
            context.ForcedType = ForcedMeshType;
            context.TextureMatrix *= TextureTransform.GetMatrix();
            context.LastObject = this;
        }
    }

    /// <summary>
    ///     Contains general basis systems for drawing objects.
    /// </summary>
    /// <typeparam name="TTransformation">The transformation type</typeparam>
    public abstract class DrawingBasis<TTransformation> : DrawingBasis, IShowTransformItem<TTransformation>
        where TTransformation : GenericTransformation, new()
    {
        /// <summary>
        ///     The current transformation.
        /// </summary>
        public TTransformation Transform { get; set; } = new TTransformation();

        /// <inheritdoc />
        protected override void DrawContext(ref DrawContext context)
        {
            base.DrawContext(ref context);
            Transform.LastMaster = context.ModelMatrix;
            context.ModelMatrix = Transform.InWorldSpace;
        }
    }
}