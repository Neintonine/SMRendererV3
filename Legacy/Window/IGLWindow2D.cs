using OpenTK;
using SM.Base;
using SM2D.Controls;
using SM2D.Scene;

namespace SM2D
{
    public interface IGLWindow2D : IGenericWindow<Scene.Scene, Camera>
    {
        Vector2? Scaling { get; set; }

        Mouse2D Mouse { get; }
    }
}