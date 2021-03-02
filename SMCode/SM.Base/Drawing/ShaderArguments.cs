using System;
using System.Collections.Generic;

namespace SM.Base.Drawing
{
    public class ShaderArguments : Dictionary<string, object>
    {
        public TType Get<TType>(string name, TType defaultValue = default)
        {
            return ContainsKey(name) ? (TType)this[name] : defaultValue;
        }
    }
}