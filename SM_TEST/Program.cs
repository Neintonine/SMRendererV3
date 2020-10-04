using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics;
using SM.Base;
using SM.Base.Time;
using SM2D;
using SM2D.Drawing;
using SM2D.Object;
using SM2D.Scene;
using Font = SM.Base.Text.Font;
using Vector2 = OpenTK.Vector2;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        private static Font font;
        private static ItemCollection col;
        private static DrawPolygon polyogn;
        private static GLWindow2D window;
        static void Main(string[] args)
        {
            font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 32
            };

            Log.SetLogFile(compressionFolder:"logs");

            window = new GLWindow2D {Scaling = new Vector2(0, 1000)};
            //window.GrabCursor();
            window.SetScene(scene = new Scene());
            window.Load += WindowOnLoad;
            window.RenderFrame += WindowOnUpdateFrame;
            window.Run();
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            polyogn.Transform.Position.Set(window.Mouse.InWorld(window.ViewportCamera));
        }

        private static void WindowOnLoad(object sender, EventArgs e)
        {
            Interval timer = new Interval(5);
            timer.EndAction += (timer1, context) => Console.WriteLine("Interval...");
            timer.Start();

            col = new ItemCollection()
            {
                Transform = { Position = new SM.Base.Types.CVector2(0, -400) },
                ZIndex = 1
            };

            col.Add(new DrawTexture(new Bitmap("soldier_logo.png"))
            {
                ZIndex = 1
            });
            col.Add(new DrawColor(Color4.Black)
            {
                Transform = { Rotation = 45, Position = new SM.Base.Types.CVector2(0, 25) },
                ZIndex = 2
            });

            scene.Objects.Add(col);
            scene.Objects.Add(new DrawText(font, "Testing...")
            {
                Transform = { Position = new SM.Base.Types.CVector2(0, -400)},
                Color = Color4.Black
            });

            scene.Objects.Add(polyogn = new DrawPolygon(Polygon.GenerateCircle(),Color4.Blue));
            scene.Objects.Add(new DrawPolygon(new Polygon(new[]
            {
                new PolygonVertex(new Vector2(.25f, 0), Color4.White),
                new PolygonVertex(new Vector2(.75f, 0), Color4.White),
                new PolygonVertex(new Vector2(1, .25f), Color4.White),
                new PolygonVertex(new Vector2(1, .75f), Color4.White),
                new PolygonVertex(new Vector2(.75f, 1), Color4.White),
                new PolygonVertex(new Vector2(.25f, 1), Color4.White),
                new PolygonVertex(new Vector2(0, .75f), new Color4(10,10,10,255)),
                new PolygonVertex(new Vector2(0, .25f), new Color4(10,10,10,255))
            }), Color4.LawnGreen)
            {
                Transform = {Position = new SM.Base.Types.CVector2(50,0)}
            });
            scene.Objects.Add(new DrawPolygon(new Polygon(new[]
            {
                new PolygonVertex(new Vector2(.25f, 0), Color4.White),
                new PolygonVertex(new Vector2(.75f, 0), Color4.White),
                new PolygonVertex(new Vector2(1, .25f), Color4.White),
                new PolygonVertex(new Vector2(1, .75f), Color4.White),
                new PolygonVertex(new Vector2(.75f, 1), Color4.White),
                new PolygonVertex(new Vector2(.25f, 1), Color4.White),
                new PolygonVertex(new Vector2(0, .75f), new Color4(10,10,10,255)),
                new PolygonVertex(new Vector2(0, .25f), new Color4(10,10,10,255))
            }), new Bitmap("soldier_logo.png"))
            {
                Transform = {Position = new SM.Base.Types.CVector2(-50,0)}
            });

            scene.Background.Color = Color4.Beige;

            /*scene.HUD.Add(new DrawText(font, "GIVE ME A HUD HUG!")
            {
                Color = Color4.Black,
                Spacing = .75f
            });*/
        }
    }
}