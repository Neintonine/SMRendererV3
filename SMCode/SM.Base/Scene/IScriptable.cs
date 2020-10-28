using SM.Base.Contexts;

namespace SM.Base.Scene
{
    /// <summary>
    /// Defines a object as script.
    /// </summary>
    public interface IScriptable
    {
        /// <summary>
        ///     Updates the object.
        /// </summary>
        void Update(UpdateContext context);
    }
}