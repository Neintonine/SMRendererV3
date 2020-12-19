using System.Collections.Generic;
using SM.OGL.Shaders;

namespace SM2D.Light
{
    public class PointLight : LightObject
    {
        internal override int Type { get; } = 0;

        public float Power = 5;
        public float InnerCircle = 1;
        public float OuterCircle = 1;

        internal override void SetUniforms(Dictionary<string, Uniform> uniforms)
        {
            base.SetUniforms(uniforms);

            uniforms["Power"].SetUniform1(Power);
            uniforms["Inner"].SetUniform1(1 / InnerCircle);
            uniforms["Outer"].SetUniform1(1 / OuterCircle);
        }
    }
}