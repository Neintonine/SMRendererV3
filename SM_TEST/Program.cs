using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SM.Base;
using SM.Base.Window;
using SM2D;
using SM2D.Pipelines;
using SM2D.Scene;
using Font = SM.Base.Drawing.Text.Font;
using Vector2 = OpenTK.Vector2;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        private static Font font;
        private static GLWindow window;
        static void Main(string[] args)
        {
            window = new GLWindow();
            window.ApplySetup(new Window2DSetup());
            window.SetRenderPipeline(Basic2DPipeline.Pipeline);
            window.SetScene(scene = new Scene());
            window.Run();
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
        }

        private static void WindowOnLoad(IGenericWindow window)
        { }
    }
}