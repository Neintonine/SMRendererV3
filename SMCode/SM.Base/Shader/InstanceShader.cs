using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SM.Base.Contexts;
using SM.Base.Scene;
using SM.Base.StaticObjects;
using SM.OGL.Shaders;

namespace SM.Base.Shader
{
    public class InstanceShader : GenericShader, IShader
    {
        protected override bool AutoCompile { get; } = true;

        public Action<UniformCollection, DrawContext, int> SetUniformVertex;
        public Action<UniformCollection, DrawContext> SetUniformFragment;

        public InstanceShader(string vertex, string fragment) : base(new ShaderFileCollection(vertex, fragment))
        {
        }
        public void Draw(DrawContext context)
        {
            GL.UseProgram(this);

            Uniforms["MVP"].SetMatrix4(context.View * context.World);

            for (int i = 0; i < context.Instances.Length; i++) SetUniformVertex?.Invoke(Uniforms, context, i);
            
            SetUniformFragment?.Invoke(Uniforms, context);

            DrawObject(context.Mesh, context.Instances.Length, true);

            GL.UseProgram(0);
        }
    }
}