#region usings

using System.Collections.Generic;

#endregion

namespace SM.Base.Drawing
{
    /// <summary>
    /// A custom dictionary, with a few useful methods.
    /// </summary>
    public class ShaderArguments : Dictionary<string, object>
    {
        /// <summary>
        /// Returns the stored value or the default value if the name doesn't exist.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        public TType Get<TType>(string name, TType defaultValue = default)
        {
            return ContainsKey(name) ? (TType) this[name] : defaultValue;
        }
    }
}