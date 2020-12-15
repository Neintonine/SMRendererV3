using System.Collections.Generic;
using SM.OGL.Shaders;

namespace SM2D.Light
{
    public class PointLight : LightObject
    {
        internal override int Type { get; } = 0;

        public float Power = 5;

        internal override void SetUniforms(Dictionary<string, Uniform> uniforms)
        {
            base.SetUniforms(uniforms);

            uniforms["Power"].SetUniform1(Power);
        }
    }
}