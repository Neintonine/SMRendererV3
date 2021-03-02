using OpenTK;
using SM.Base.Windows;

namespace SM2D
{
    public interface I2DSetup : ISetup
    {
        Vector2? WorldScale { get; set; }
    }
}