using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Base.Utility
{
    class MathUtils
    {
        public static float Lerp(float start, float end, float t)
        {
            return start + t * (end - start);
        }
    }
}
