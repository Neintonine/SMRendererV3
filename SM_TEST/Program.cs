using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            GLWindow2D window = new GLWindow2D();
            window.SetScene(scene = new Scene());
            window.Load += WindowOnLoad;
            window.Run();
        }

        private static void WindowOnLoad(object sender, EventArgs e)
        {
            scene.Objects.Add(new DrawEmpty());
        }
    }
}
