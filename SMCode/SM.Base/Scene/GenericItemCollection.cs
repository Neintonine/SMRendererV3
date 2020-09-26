using System.Collections.Generic;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    public abstract class GenericItemCollection<TItem, TTransformation> : IShowItem, IShowCollection<TItem>
        where TItem : IShowItem
        where TTransformation : GenericTransformation, new()
    {
        public List<TItem> Objects { get; } = new List<TItem>();
        public TTransformation Transform = new TTransformation();
        public void Update(UpdateContext context)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Draw(DrawContext context)
        {
            context.View = Transform.GetMatrix() * context.View;

            for (int i = 0; i < Objects.Count; i++)
                Objects[i].Draw(context);
        }
    }
}