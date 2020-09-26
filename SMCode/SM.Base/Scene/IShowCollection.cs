using System.Collections.Generic;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    public interface IShowCollection<TItem> where TItem : IShowItem
    {
        List<TItem> Objects { get; }

        void Draw(DrawContext context);
    }
}