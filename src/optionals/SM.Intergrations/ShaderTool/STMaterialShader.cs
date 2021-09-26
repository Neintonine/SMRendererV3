
using System;
using System.Collections.Generic;
using OpenTK;
using ShaderToolParser.Nodes;
using ShaderToolParser.Variables;
using SM.Base.Shaders;
using SM.Base.Textures;
using SM.Base.Window;
using SM.OGL.Shaders;
using SM.OGL.Texture;

namespace SM.Intergrations.ShaderTool
{
    public class STMaterialShader : MaterialShader
    {
        private event Action<DrawContext> _uniforms;

        public STMaterialShader(STPDrawNode drawNode) : base(new ShaderFileCollection())
        {
            if (drawNode.OGLEffect == null)
                throw new Exception("[ERROR AT IMPORTING SHADER] DrawNode didn't contain a OpenGL-shader.");

            STPCompositeNode composeNode = drawNode.OGLEffect;

            ShaderFiles.Vertex = new[] { new ShaderFile(composeNode.Vertex.ShaderCode) };
            ShaderFiles.Fragment = new [] {new ShaderFile(composeNode.Fragment.ShaderCode)};
            if (composeNode.Geometry != null)
                ShaderFiles.Geometry = new[] {new ShaderFile(composeNode.Geometry.ShaderCode)};

            foreach (KeyValuePair<string, STPVariable> pair in drawNode.Variables)
            {
                switch (pair.Value.Type)
                {
                    case STPBasisType.Bool:
                        _uniforms += context => Uniforms[pair.Key].SetBool(context.Material.ShaderArguments.Get(pair.Key, false));
                        break;
                    case STPBasisType.Float:
                        _uniforms += context => Uniforms[pair.Key].SetFloat(context.Material.ShaderArguments.Get(pair.Key, 0.0f));
                        break;
                    case STPBasisType.Vector2:
                        _uniforms += context => Uniforms[pair.Key].SetVector2(context.Material.ShaderArguments.Get(pair.Key, Vector2.Zero));
                        break;
                    case STPBasisType.Vector3:
                        _uniforms += context => Uniforms[pair.Key].SetVector3(context.Material.ShaderArguments.Get(pair.Key, Vector3.Zero));
                        break;
                    case STPBasisType.Vector4:
                        _uniforms += context =>
                            Uniforms[pair.Key].SetVector4(context.Material.ShaderArguments.Get(pair.Key, Vector4.Zero));
                        break;
                    case STPBasisType.Matrix:
                        _uniforms += context => Uniforms[pair.Key].SetMatrix4(context.Material.ShaderArguments.Get(pair.Key, Matrix4.Identity));
                        break;
                    case STPBasisType.Texture:
                        _uniforms += context => Uniforms[pair.Key].SetTexture(context.Material.ShaderArguments.Get<TextureBase>(pair.Key, null));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected override void DrawProcess(DrawContext context)
        {
            _uniforms.Invoke(context);

            DrawObject(context.ForcedType.GetValueOrDefault(context.Mesh.PrimitiveType), context.Mesh);
        }
    }
}