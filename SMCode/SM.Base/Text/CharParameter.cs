using System;
using System.Configuration.Assemblies;
using OpenTK;

namespace SM.Data.Fonts
{
    [Serializable]
    public class CharParameter
    {
        public int X;
        public float Width;

        public float RelativeX;
        public float RelativeWidth;
    }
}