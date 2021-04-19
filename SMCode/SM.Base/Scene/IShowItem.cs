#region usings

using System.Collections.Generic;
using SM.Base.Drawing;
using SM.Base.Window;
using SM.OGL.Mesh;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Adds requirements to object, to be properly used as a update and/or draw item.
    /// </summary>
    public interface IShowItem : ICollectionItem
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
        /// If true it will ignore the object.
        /// </summary>
        bool Active { get; set; }
        /// <summary>
        /// Íf true it will ignore the object when rendering.
        /// </summary>
        bool RenderActive { get; set; }

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

    /// <summary>
    /// Interface to implement transformation.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public interface ITransformItem<TTransform>
        where TTransform : GenericTransformation
    {
        /// <summary>
        /// Controls the transformation of the object.
        /// </summary>
        TTransform Transform { get; set; }
    }

    /// <summary>
    /// Merges <see cref="IShowItem"/> and <see cref="ITransformItem{TTransform}"/>.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
    public interface IShowTransformItem<TTransform> : IShowItem, ITransformItem<TTransform>
        where TTransform : GenericTransformation
    {
    }

    /// <summary>
    /// Interface to implement models in the object.
    /// </summary>
    public interface IModelItem
    {
        /// <summary>
        /// The mesh the rendering should use.
        /// </summary>
        GenericMesh Mesh { get; set; }
    }
}