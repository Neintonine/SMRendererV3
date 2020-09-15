using System;
using OpenTK.Graphics.OpenGL4;

namespace SM.OGL.Shaders
{
    public class GenericShader : GLObject
    {
        protected ShaderFileCollection ShaderFileFiles;
        protected UniformCollection Uniforms;

        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Program;

        public GenericShader(ShaderFileCollection shaderFileFiles)
        {
            ShaderFileFiles = shaderFileFiles;
        }

        public void Compile()
        {
            
            _id = GL.CreateProgram();

            ShaderFileFiles.Append(this);
            GL.LinkProgram(_id);
            this.Name(GetType().Name);
            ShaderFileFiles.Detach(this);

            GL.GetProgram(_id, GetProgramParameterName.ActiveUniforms, out int uniformCount);
            if (uniformCount < 1)
                throw new Exception("[Critical] No uniforms has been found.");

            Uniforms = new UniformCollection();
            for (int i = 0; i < uniformCount; i++)
            {
                string key = GL.GetActiveUniform(_id, i, out _, out _);
                int loc = GL.GetUniformLocation(_id, key);

                if (key.StartsWith("[")) key = key.Split('[', ']')[0];
                Uniforms.Add(key, loc);
            }

        }

        public void DrawObject(Mesh.Mesh mesh, bool bindVAO)
        {
            if (bindVAO) GL.BindVertexArray(mesh);
            GL.DrawArrays(mesh.PrimitiveType, 0, mesh.Vertex.Count);
        } 
    }
}