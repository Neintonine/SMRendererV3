using System;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using SM.Base;
using SM.Base.Window;
using SM2D;
using SM2D.Drawing;
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
            window.SetScene(scene = new Scene());
            scene.Objects.Add(new DrawObject2D());
            window.UpdateFrame += WindowOnUpdateFrame;
            window.Run();

            Debug.WriteLine("Window Closed");
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            if (SM.Base.Controls.Keyboard.IsDown(Key.F, true))
            {
                window.WindowFlags = WindowFlags.ExclusiveFullscreen;
                window.ChangeFullscreenResolution(DisplayDevice.Default.SelectResolution(1280,720, DisplayDevice.Default.BitsPerPixel, DisplayDevice.Default.RefreshRate));
            }
            if (SM.Base.Controls.Keyboard.IsDown(Key.W, true)) window.WindowFlags = WindowFlags.Window;
            if (SM.Base.Controls.Keyboard.IsDown(Key.B, true)) window.WindowFlags = WindowFlags.BorderlessWindow;

        }

        private static void WindowOnLoad(IGenericWindow window)
        { }
    }
}