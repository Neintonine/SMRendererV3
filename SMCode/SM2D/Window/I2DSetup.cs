using OpenTK;
using SM.Base.Window;

namespace SM2D
{
    public interface I2DSetup : ISetup
    {
        Vector2? WorldScale { get; set; }
    }
}