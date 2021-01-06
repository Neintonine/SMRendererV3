#region usings

using System.Collections.Generic;
using System.Dynamic;
using OpenTK;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Contexts
{
    /// <summary>
    ///     Contains important information for drawing.
    /// </summary>
    public struct DrawContext
    {
        /// <summary>
        ///     This says if it was forced to use the viewport camera.
        /// </summary>
        public bool ForceViewport;

        /// <summary>
        ///     Contains the currently used render pipeline.
        /// </summary>
        public RenderPipeline ActivePipeline;

        public GenericScene ActiveScene;
        public GenericWindow Window;


        public GenericCamera UsedCamera =>
            ForceViewport || ActiveScene._camera == null ? Window._viewportCamera : ActiveScene._camera;



        /// <summary>
        ///     The mesh.
        /// </summary>
        public GenericMesh Mesh;

        /// <summary>
        ///     The material.
        /// </summary>
        public Material Material;

        /// <summary>
        ///     The drawing instances.
        ///     <para>If there is only one, it's index 0</para>
        /// </summary>
        public IList<Instance> Instances;



        /// <summary>
        ///     The current world scale.
        /// </summary>
        public Vector2 WorldScale;

        /// <summary>
        ///     The last collection the context was passed though.
        /// </summary>
        public object LastPassthough;

        

        /// <summary>
        ///     Returns the appropriate shader.
        ///     <para>
        ///         Returns the material shader, if available, otherwise it will take the default shader from the render
        ///         pipeline.
        ///     </para>
        /// </summary>
        public MaterialShader Shader => Material.CustomShader ?? ActivePipeline._defaultShader;
        /// <summary>
        /// Arguments for shaders
        /// </summary>
        public IDictionary<string, object> ShaderArguments;



        /// <summary>
        ///     The current world matrix.
        /// </summary>
        public Matrix4 World;

        /// <summary>
        ///     The current view matrix.
        /// </summary>
        public Matrix4 View;

        /// <summary>
        ///     The current WorldView matrix.
        /// </summary>
        public Matrix4 WorldView;

        /// <summary>
        ///     The master model matrix.
        /// </summary>
        public Matrix4 ModelMaster;
    }
}