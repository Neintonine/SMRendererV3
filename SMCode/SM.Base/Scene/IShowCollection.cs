#region usings

using System.Collections.Generic;
using SM.Base;
using SM.Base.Windows;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Adds functions, that is required for a collection.
    /// </summary>
    /// <typeparam name="TItem">The type of show item.</typeparam>
    public interface IShowCollection
    {
        /// <summary>
        ///     The object collection.
        /// </summary>
        List<IShowItem> Objects { get; }

        /// <summary>
        ///     This draws the objects in the <see cref="Objects" /> list.
        /// </summary>
        /// <param name="context">The context how the objects need to be drawn.</param>
        void Draw(DrawContext context);
    }
}