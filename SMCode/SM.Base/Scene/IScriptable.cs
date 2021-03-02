using SM.Base;
using SM.Base.Windows;

namespace SM.Base.Scene
{
    /// <summary>
    /// Defines a object as script.
    /// </summary>
    public interface IScriptable
    {
        bool Active { get; set; }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        void Update(UpdateContext context);
    }
}