#region usings

using SM.Base.Scene;

#endregion

namespace SM2D.Scene
{
    public interface I2DShowItem : IShowItem
    {
        int ZIndex { get; set; }
    }
}