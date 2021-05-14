#region usings

using System;
using System.Collections.Generic;
using SM.Base.Utility;
using SM.Base.Window;
using SM.OGL.Shaders;

#endregion

namespace SM.Base.Shaders
{
    /// <summary>
    /// Allows for simple creation of shaders.
    /// </summary>
    public class SimpleShader : MaterialShader
    {
        /// <summary>
        /// Vertex files that are stored in this dictionary can be used as vertex presets.
        /// </summary>
        public static Dictionary<string, Tuple<ShaderFile, Action<UniformCollection, DrawContext>>> VertexFiles =
            new Dictionary<string, Tuple<ShaderFile, Action<UniformCollection, DrawContext>>>();
        
        private readonly string _vertexPreset;

        /// <summary>
        /// Stores the function that sets the uniforms.
        /// </summary>
        public Action<UniformCollection, DrawContext> SetUniform;

        static SimpleShader()
        {
            VertexFiles.Add("basic", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(
                    AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.basic_vertex.glsl"))
                {
                    StringOverrides = {["extension"] = "0"}
                },
                BasicSetUniforms
            ));
            VertexFiles.Add("E_basic", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(
                    AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.basic_vertex.glsl"))
                {
                    StringOverrides = {["extension"] = "1"}
                },
                BasicSetUniforms
            ));

            VertexFiles.Add("instanced", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(
                    AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.instanced_vertex.glsl"))
                {
                    StringOverrides = {["instanceMax"] = SMRenderer.MaxInstances.ToString(), ["extension"] = "0"}
                },
                InstancedSetUniforms
            ));
            VertexFiles.Add("E_instanced", new Tuple<ShaderFile, Action<UniformCollection, DrawContext>>(
                new ShaderFile(
                    AssemblyUtility.ReadAssemblyFile("SM.Base.Shaders.SimpleShaderPresets.instanced_vertex.glsl"))
                {
                    StringOverrides = {["instanceMax"] = SMRenderer.MaxInstances.ToString(), ["extension"] = "0"}
                },
                InstancedSetUniforms
            ));
        }

        /// <summary>
        /// Creates a simple shader.
        /// </summary>
        /// <param name="vertexPreset">The vertex preset.</param>
        /// <param name="fragment">The fragment shader</param>
        /// <param name="setUniform">The uniform function.</param>
        public SimpleShader(string vertexPreset, string fragment, Action<UniformCollection, DrawContext> setUniform) :
            base(new ShaderFileCollection(VertexFiles[vertexPreset].Item1, new ShaderFile(fragment)))
        {
            _vertexPreset = vertexPreset;
            SetUniform = setUniform;
        }

        /// <summary>
        /// Creates a simple shader with a extension.
        /// </summary>
        /// <param name="vertexPreset">The vertex preset.</param>
        /// <param name="vertexExtension">The vertex extension shader</param>
        /// <param name="fragment">The fragment shader</param>
        /// <param name="setUniform">The uniform function.</param>
        public SimpleShader(string vertexPreset, string vertexExtension, string fragment,
            Action<UniformCollection, DrawContext> setUniform) : base(new ShaderFileCollection(
            new[] {VertexFiles["E_" + vertexPreset].Item1, new ShaderFile(vertexExtension)},
            new[] {new ShaderFile(fragment)}))
        {
            _vertexPreset = vertexPreset;
            SetUniform = setUniform;
        }

        private static void BasicSetUniforms(UniformCollection uniforms, DrawContext context)
        {
            // Vertex Uniforms
            uniforms["MVP"]
                .SetMatrix4(context.Instances[0].ModelMatrix * context.ModelMatrix * context.View * context.World);
            uniforms["MasterTextureMatrix"].SetMatrix3(context.Instances[0].TextureMatrix * context.TextureMatrix);
            uniforms["HasVColor"]
                .SetUniform1(context.Mesh.Attributes.Has("color"));

            DrawObject(context.ForcedType.GetValueOrDefault(context.Mesh.PrimitiveType), context.Mesh);
        }

        private static void InstancedSetUniforms(UniformCollection uniforms, DrawContext context)
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

            DrawObject(context.ForcedType.GetValueOrDefault(context.Mesh.PrimitiveType), context.Mesh, shaderInstanceI);
        }

        /// <inheritdoc />
        protected override void DrawProcess(DrawContext context)
        {
            SetUniform?.Invoke(Uniforms, context);

            VertexFiles[_vertexPreset].Item2(Uniforms, context);
        }
    }
}