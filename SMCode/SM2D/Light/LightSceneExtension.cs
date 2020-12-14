using System.Collections.Generic;
using OpenTK.Graphics;

namespace SM2D.Light
{
    public class LightSceneExtension
    {
        public Color4 Ambient = Color4.White;

        public List<LightObject> Lights = new List<LightObject>();
    }
}