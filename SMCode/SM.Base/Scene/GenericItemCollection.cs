using System.Collections.Generic;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    /// <summary>
    /// Contains a list of show items.
    /// </summary>
    /// <typeparam name="TItem">The type of show items.</typeparam>
    public abstract class GenericItemCollection<TItem> : List<TItem>, IShowItem, IShowCollection<TItem>
        where TItem : IShowItem
    {
        /// <inheritdoc />
        public List<TItem> Objects => this;

        /// <inheritdoc />
        public void Update(UpdateContext context)
        {
            for(int i = 0; i < Objects.Count; i++)
                this[i].Update(context);
        }

        /// <inheritdoc cref="IShowCollection{TItem}.Draw" />
        public virtual void Draw(DrawContext context)
        {
            for (int i = 0; i < Objects.Count; i++)
                this[i].Draw(context);
        }
    }

    /// <summary>
    /// Contains a list of show items with transformation.
    /// </summary>
    /// <typeparam name="TItem">The type of show items.</typeparam>
    /// <typeparam name="TTransformation">The type of transformation.</typeparam>
    public abstract class GenericItemCollection<TItem, TTransformation> : GenericItemCollection<TItem>
        where TItem : IShowItem
        where TTransformation : GenericTransformation, new()
    {
        /// <summary>
        /// Transformation of the collection
        /// </summary>
        public TTransformation Transform = new TTransformation();

        /// <inheritdoc />
        public override void Draw(DrawContext context)
        {
            context.View = Transform.GetMatrix() * context.View;
            
            base.Draw(context);
        }
    }
}