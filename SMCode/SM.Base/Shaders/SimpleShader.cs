using System;
using System.Collections.Generic;
using System.Dynamic;
using SM.Base.Windows;
using SM.OGL.Shaders;
using SM.Utility;

namespace SM.Base.Drawing
{
    public class SimpleShader : MaterialShader
    {
        public static Dictionary<string, Tuple<ShaderFile, Action<UniformCollection, DrawContext>>> VertexFiles =
            new Dictionary<string, Tuple<ShaderFile, Action<UniformCollection, DrawContext>>>();

        static ShaderFile _extensionDefineFile = new ShaderFile("#define SM_SIMPLE_EXTENSION");

        static SimpleShader()
        {
            VertexFiles.Add("basic", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.basic_vertex.glsl"))
                {
                    StringOverrides = {["extension"] = "0"}
                },
                BasicSetUniforms
            ));
            VertexFiles.Add("E_basic", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.basic_vertex.glsl"))
                {
                    StringOverrides = {["extension"] = "1"}
                },
                BasicSetUniforms
            ));

            VertexFiles.Add("instanced", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(
                    AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.instanced_vertex.glsl"))
                {
                    StringOverrides = { ["instanceMax"] = SMRenderer.MaxInstances.ToString(), ["extension"] = "0" }
                },
                InstancedSetUniforms
            ));
            VertexFiles.Add("E_instanced", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(
                    AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.instanced_vertex.glsl"))
                {
                    StringOverrides = { ["instanceMax"] = SMRenderer.MaxInstances.ToString(), ["extension"] = "0" }
                },
                InstancedSetUniforms
            ));
        }

        static void BasicSetUniforms(UniformCollection uniforms, DrawContext context)
        {
            // Vertex Uniforms
            uniforms["MVP"].SetMatrix4(context.Instances[0].ModelMatrix * context.ModelMatrix * context.View * context.World);
            uniforms["MasterTextureMatrix"].SetMatrix3(context.Instances[0].TextureMatrix * context.TextureMatrix);
            uniforms["HasVColor"]
                .SetUniform1(context.Mesh.Attributes.Has("color"));

            DrawObject(context.Mesh);
        }

        static void InstancedSetUniforms(UniformCollection uniforms, DrawContext context)
        {
            uniforms["MVP"].SetMatrix4(context.ModelMatrix * context.View * context.World);
            uniforms["MasterTextureMatrix"].SetMatrix3(context.TextureMatrix);
            uniforms["HasVColor"]
                .SetUniform1(context.Mesh.Attributes.Has("color"));

            UniformArray instances = uniforms.GetArray("Instances");

            int shaderInstanceI = 0;
            for (int i = 0; i < context.Instances.Count; i++)
            {
                if (shaderInstanceI > instances.Length - 1)
                {
                    DrawObject(context.Mesh, instances.Length);
                    shaderInstanceI = 0;
                }

                var shaderInstance = instances[shaderInstanceI];
                var instance = context.Instances[i];
                if (instance == null) continue;
                shaderInstance["ModelMatrix"].SetMatrix4(instance.ModelMatrix);
                shaderInstance["TextureMatrix"].SetMatrix3(instance.TextureMatrix);

                shaderInstanceI++;
            }
            DrawObject(context.Mesh, shaderInstanceI);
        }

        private string _vertexPreset;

        public Action<UniformCollection, DrawContext> SetUniform;

        public SimpleShader(string vertexPreset, string fragment, Action<UniformCollection, DrawContext> setUniform) : base(new ShaderFileCollection(VertexFiles[vertexPreset].Item1, new ShaderFile(fragment)))
        {
            _vertexPreset = vertexPreset;
            SetUniform = setUniform;
        }

        public SimpleShader(string vertexPreset, string vertexExtension, string fragment,
            Action<UniformCollection, DrawContext> setUniform) : base(new ShaderFileCollection(
            new[] {VertexFiles["E_"+vertexPreset].Item1, new ShaderFile(vertexExtension)},
            new[] {new ShaderFile(fragment),}))
        {
            _vertexPreset = vertexPreset;
            SetUniform = setUniform;
        }

        protected override void DrawProcess(DrawContext context)
        {
            SetUniform?.Invoke(Uniforms, context);

            VertexFiles[_vertexPreset].Item2(Uniforms, context);
        }
    }
}