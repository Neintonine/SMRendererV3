using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using SM.Base;
using SM2D;
using SM2D.Drawing;
using SM2D.Scene;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        static void Main(string[] args)
        {
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
            scene.Background = new DrawBackground(Color4.Beige);
        }
    }
}