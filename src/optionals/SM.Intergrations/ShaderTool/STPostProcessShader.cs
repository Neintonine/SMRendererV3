using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using ShaderToolParser.Nodes;
using ShaderToolParser.Variables;
using SM.Base.Drawing;
using SM.Base.Objects.Static;
using SM.Base.Textures;
using SM.OGL.Shaders;
using SM.OGL.Texture;

namespace SM.Intergrations.ShaderTool
{
    public class STPostProcessShader : GenericShader
    {
        private event Action<ShaderArguments> _uniforms;

        public STPostProcessShader(STPDrawNode postProcessNode) : base(new ShaderFileCollection())
        {
            if (postProcessNode.OGLEffect == null)
                throw new Exception("[ERROR AT IMPORTING SHADER] DrawNode didn't contain a OpenGL-shader.");

            STPCompositeNode composeNode = postProcessNode.OGLEffect;

            ShaderFileFiles.Vertex = new[] { new ShaderFile(composeNode.Vertex.ShaderCode) };
            ShaderFileFiles.Fragment = new[] { new ShaderFile(composeNode.Fragment.ShaderCode) };
            if (composeNode.Geometry != null)
                ShaderFileFiles.Geometry = new[] { new ShaderFile(composeNode.Geometry.ShaderCode) };

            foreach (KeyValuePair<string, STPVariable> pair in postProcessNode.Variables)
            {
                switch (pair.Value.Type)
                {
                    case STPBasisType.Bool:
                        _uniforms += context => Uniforms[pair.Key].SetUniform1(context.Get(pair.Key, false));
                        break;
                    case STPBasisType.Float:
                        _uniforms += context => Uniforms[pair.Key].SetUniform1(context.Get(pair.Key, 0.0f));
                        break;
                    case STPBasisType.Vector2:
                        _uniforms += context => Uniforms[pair.Key].SetUniform2(context.Get(pair.Key, Vector2.Zero));
                        break;
                    case STPBasisType.Vector3:
                        _uniforms += context => Uniforms[pair.Key].SetUniform3(context.Get(pair.Key, Vector3.Zero));
                        break;
                    case STPBasisType.Vector4:
                        _uniforms += context =>
                            Uniforms[pair.Key].SetUniform4(context.Get(pair.Key, Vector4.Zero));
                        break;
                    case STPBasisType.Matrix:
                        _uniforms += context => Uniforms[pair.Key].SetMatrix4(context.Get(pair.Key, Matrix4.Identity));
                        break;
                    case STPBasisType.Texture:
                        _uniforms += context => Uniforms[pair.Key].SetTexture(context.Get<TextureBase>(pair.Key, null));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Draw(ShaderArguments arguments)
        {
            Activate();
            Plate.Object.Activate();

            _uniforms.Invoke(arguments);

            GL.DrawArrays(PrimitiveType.Quads, 0, 4);
            CleanUp();
        }
    }
}