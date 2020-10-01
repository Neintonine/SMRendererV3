using System.Collections.Generic;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    /// <summary>
    /// Adds functions, that is required for a collection.
    /// </summary>
    /// <typeparam name="TItem">The type of show item.</typeparam>
    public interface IShowCollection<TItem> where TItem : IShowItem
    {
        /// <summary>
        /// The object collection.
        /// </summary>
        List<TItem> Objects { get; }

        /// <summary>
        /// This draws the objects in the <see cref="Objects"/> list.
        /// </summary>
        /// <param name="context">The context how the objects need to be drawn.</param>
        void Draw(DrawContext context);
    }
}