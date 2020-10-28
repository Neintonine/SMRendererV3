#region usings

using System.Collections.Generic;
using SM.Base.Contexts;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Adds requirements to object, to be properly used as a update and/or draw item.
    /// </summary>
    public interface IShowItem
    {
        /// <summary>
        ///     Parent of the object.
        /// </summary>
        object Parent { get; set; }

        /// <summary>
        ///     Contains the name for the object.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Contains specific flags for the object.
        /// </summary>
        ICollection<string> Flags { get; set; }
        
        /// <summary>
        ///     Tells the object to draw its object.
        /// </summary>
        /// <param name="context"></param>
        void Draw(DrawContext context);

        /// <summary>
        ///     Action, that is called, when the object was added to a GenericItemCollection.
        /// </summary>
        void OnAdded(object sender);

        /// <summary>
        ///     Action, that is called, when the object was removed from a GenericItemCollection.
        /// </summary>
        void OnRemoved(object sender);
    }
}