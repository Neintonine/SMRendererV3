using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Drawing;
using SM.Base.Scene;
using SM.OGL.Mesh;

namespace SM.Base.Windows
{
    public struct DrawContext
    {
        public IGenericWindow Window { get; internal set; }
        public GenericScene Scene { get; internal set; }
        public RenderPipeline RenderPipeline { get; internal set; }
        public object LastObject { get; internal set; }

        public GenericCamera UseCamera { get; internal set; }
        public Matrix4 World => UseCamera.World;
        public Matrix4 View => UseCamera.View;

        public GenericMesh Mesh { get; set; }
        public PrimitiveType? ForcedType { get; set; }
        public Material Material { get; set; }
        public MaterialShader Shader => Material.CustomShader ?? RenderPipeline.DefaultShader;

        public Matrix4 ModelMatrix;
        public Matrix3 TextureMatrix;
        public IList<Instance> Instances;

        public void SetCamera(GenericCamera camera)
        {
            UseCamera = camera;
            camera.CalculateViewMatrix(Window);
        }
    }
}