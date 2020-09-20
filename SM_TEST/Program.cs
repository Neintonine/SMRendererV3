using System;
using System.Drawing;
using OpenTK.Graphics;
using SM2D;
using SM2D.Drawing;
using SM2D.Scene;
using Font = SM.Base.Text.Font;
using Vector2 = OpenTK.Vector2;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        private static Font font;
        static void Main(string[] args)
        {
            font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 64
            };

            GLWindow2D window = new GLWindow2D {Scaling = new Vector2(0, 1000)};
            window.SetScene(scene = new Scene());
            window.Load += WindowOnLoad;
            window.Run();
        }

        private static void WindowOnLoad(object sender, EventArgs e)
        {
            scene.Objects.Add(new DrawTexture(new Bitmap("soldier_logo.png")));
            scene.Objects.Add(new DrawTexture(new Bitmap("soldier_logo.png"))
            {
                Transform = { Position = new Vector2(100), Rotation = 45},
            });
            scene.Objects.Add(new DrawText(font, "Testing...")
            {
                Transform = {Size = new Vector2(1), Position = new SM.Base.Types.Vector2(0, -400)},
                Color = Color4.Black
            });
            scene.Background.Color = Color4.Beige;
        }
    }
}