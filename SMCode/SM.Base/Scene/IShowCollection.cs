using System.Collections.Generic;

namespace SM.Base.Scene
{
    public interface IShowCollection
    {
        List<IShowItem> Objects { get; }
    }
}