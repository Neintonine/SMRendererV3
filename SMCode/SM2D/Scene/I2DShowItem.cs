using SM.Base.Scene;

namespace SM2D.Scene
{
    public interface I2DShowItem : IShowItem
    {
        int ZIndex { get; set; }

    }
}