#region usings

using SM.Base.Window;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Defines a object as script.
    /// </summary>
    public interface IScriptable
    {
        /// <summary>
        /// If not active, ItemCollections will ignore them.
        /// </summary>
        bool Active { get; set; }

        /// <summary>
        /// If not active, ItemCollections will ignore them.
        /// </summary>
        bool UpdateActive { get; set; }

        /// <summary>
        ///     Updates the object.
        /// </summary>
        void Update(UpdateContext context);
    }
}