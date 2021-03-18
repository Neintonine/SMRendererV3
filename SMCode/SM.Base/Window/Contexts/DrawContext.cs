#region usings

using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.Base.Shaders;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Window
{
    /// <summary>
    /// The draw context contains (more or less) important information for drawing a object.
    /// </summary>
    public struct DrawContext
    {
        /// <summary>
        /// The window, the context came origionally.
        /// </summary>
        public IGenericWindow Window { get; internal set; }
        /// <summary>
        /// The scene, the context was send to.
        /// </summary>
        public GenericScene Scene { get; internal set; }
        /// <summary>
        /// The render pipeline, the context is using.
        /// </summary>
        public RenderPipeline RenderPipeline { get; internal set; }
        /// <summary>
        /// The last object it came though.
        /// <para>Debugging pourpose.</para>
        /// </summary>
        public object LastObject { get; internal set; }

        /// <summary>
        /// The camera the context is using.
        /// </summary>
        public GenericCamera UseCamera { get; internal set; }
        /// <summary>
        /// The world matrix.
        /// </summary>
        public Matrix4 World => UseCamera.World;
        /// <summary>
        /// The view matrix.
        /// </summary>
        public Matrix4 View => UseCamera.View;

        /// <summary>
        /// The mesh, that is rendered.
        /// </summary>
        public GenericMesh Mesh { get; set; }
        /// <summary>
        /// If set, it will force the mesh to render with that primitive type.
        /// </summary>
        public PrimitiveType? ForcedType { get; set; }
        /// <summary>
        /// The material the mesh is going to use.
        /// </summary>
        public Material Material { get; set; }
        /// <summary>
        /// The shader the mesh is going to use.
        /// </summary>
        public MaterialShader Shader => Material.CustomShader ?? RenderPipeline.DefaultShader;

        /// <summary>
        /// The master model matrix.
        /// </summary>
        public Matrix4 ModelMatrix;
        /// <summary>
        /// The master texture matrix.
        /// </summary>
        public Matrix3 TextureMatrix;
        /// <summary>
        /// Instances
        /// </summary>
        public IList<Instance> Instances;

        /// <summary>
        /// This sets the camera of the context.
        /// </summary>
        /// <param name="camera"></param>
        public void SetCamera(GenericCamera camera)
        {
            UseCamera = camera;
            camera.CalculateViewMatrix(Window);
        }
    }
}