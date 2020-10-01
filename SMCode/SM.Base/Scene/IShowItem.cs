using SM.Base.Contexts;

namespace SM.Base.Scene
{
    /// <summary>
    /// Adds requirements to object, to be properly used as a update and/or draw item.
    /// </summary>
    public interface IShowItem
    {
        /// <summary>
        /// Tells the object to update own systems.
        /// </summary>
        /// <param name="context">The update context</param>
        void Update(UpdateContext context);
        /// <summary>
        /// Tells the object to draw its object.
        /// </summary>
        /// <param name="context"></param>
        void Draw(DrawContext context);
    }
}