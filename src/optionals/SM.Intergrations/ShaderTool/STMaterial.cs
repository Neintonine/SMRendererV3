using System;
using System.Collections.Generic;
using System.IO.Pipes;
using OpenTK;
using OpenTK.Graphics;
using ShaderToolParser.Nodes;
using ShaderToolParser.Nodes.Textures;
using ShaderToolParser.Variables;
using SM.Base.Drawing;
using SM.Base.Textures;
using SM.Base.Window;

namespace SM.Intergrations.ShaderTool
{
    public class STMaterial : Material
    {
        private Vector4 _tintVector = Vector4.One;

        public override Color4 Tint
        {
            get => Color4.FromXyz(_tintVector);
            set => _tintVector = Color4.ToXyz(value);
        }

        public STMaterial(STPDrawNode node)
        {
            if (node.OGLEffect == null)
                throw new Exception("[ERROR AT IMPORTING MATERIAL] DrawNode didn't contain a OpenGL-shader.");

            CustomShader = new STMaterialShader(node);

            foreach (KeyValuePair<string, STPVariable> pair in node.Variables)
            {
                if (pair.Value.Type == STPBasisType.Texture)
                    ShaderArguments[pair.Key] = new Texture(((STPTextureNode) pair.Value.Texture).Bitmap);
            }
        }

        public override void Draw(DrawContext context)
        {
            ShaderArguments["MVP"] = context.Instances[0].ModelMatrix * context.ModelMatrix * context.View * context.World;
            ShaderArguments["MasterTextureMatrix"] = context.Instances[0].TextureMatrix * context.TextureMatrix;
            ShaderArguments["HasVColor"] = context.Mesh.Attributes.Has("color");

            ShaderArguments["_MATColor"] = _tintVector;

            base.Draw(context);
        }
    }
}