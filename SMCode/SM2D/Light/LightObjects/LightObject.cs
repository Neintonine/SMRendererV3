using System.Collections.Generic;
using OpenTK.Graphics;
using SM.Base.Types;
using SM.OGL.Shaders;

namespace SM2D.Light
{
    public abstract class LightObject
    {
        internal abstract int Type { get; }

        public CVector2 Position = new CVector2(0);
        public Color4 Color = Color4.White;

        internal virtual void SetUniforms(Dictionary<string, Uniform> uniforms)
        {
            uniforms["Type"].SetUniform1(Type);
            uniforms["Position"].SetUniform2(Position);
            uniforms["Color"].SetUniform4(Color);
        }
    }
}