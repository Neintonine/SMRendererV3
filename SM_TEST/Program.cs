using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
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
        static void Main(string[] args)
        {
            font = new Font(@"C:\Windows\Fonts\Arial.ttf")
            {
                FontSize = 32
            };

            GLWindow2D window = new GLWindow2D {Scaling = new Vector2(0, 500)};
            window.SetScene(scene = new Scene());
            window.Load += WindowOnLoad;
            window.UpdateFrame += WindowOnUpdateFrame;
            window.Run();
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            float speed = 40;

            //col.Transform.Position.Y += (float)e.Time * speed;
        }

        private static void WindowOnLoad(object sender, EventArgs e)
        {
            col = new ItemCollection()
            {
                Transform = { Position = new SM.Base.Types.Vector2(0, -400) },
                ZIndex = 1
            };

            col.Objects.Add(new DrawTexture(new Bitmap("soldier_logo.png"))
            {
                ZIndex = 1
            });
            col.Objects.Add(new DrawColor(Color4.Aqua)
            {
                Transform = { Rotation = 45, Position = new SM.Base.Types.Vector2(0, 25) },
            });

            scene.Objects.Add(col);
            scene.Objects.Add(new DrawText(font, "Testing...")
            {
                Transform = { Position = new SM.Base.Types.Vector2(0, -400)},
                Color = Color4.Black
            });

            scene.Objects.Add(new DrawPolygon(new Polygon(new[]
                {
                    new Vector2(.25f, 0),
                    new Vector2(.75f, 0),
                    new Vector2(1, .25f),
                    new Vector2(1, .75f),
                    new Vector2(.75f, 1),
                    new Vector2(.25f, 1),
                    new Vector2(0, .75f),
                    new Vector2(0, .25f)
                }),Color4.Blue)
            );
            scene.Objects.Add(new DrawPolygon(new Polygon(new[]
            {
                new PolygonVertex(new Vector2(.25f, 0), Color4.White),
                new PolygonVertex(new Vector2(.75f, 0), Color4.White),
                new PolygonVertex(new Vector2(1, .25f), Color4.White),
                new PolygonVertex(new Vector2(1, .75f), Color4.White),
                new PolygonVertex(new Vector2(.75f, 1), Color4.White),
                new PolygonVertex(new Vector2(.25f, 1), Color4.White),
                new PolygonVertex(new Vector2(0, .75f), Color4.Gray),
                new PolygonVertex(new Vector2(0, .25f), Color4.Gray)
            }), Color4.LawnGreen)
            {
                Transform = {Position = new SM.Base.Types.Vector2(50,0)}
            });
            scene.Objects.Add(new DrawPolygon(new Polygon(new[]
            {
                new PolygonVertex(new Vector2(.25f, 0), Color4.White),
                new PolygonVertex(new Vector2(.75f, 0), Color4.White),
                new PolygonVertex(new Vector2(1, .25f), Color4.White),
                new PolygonVertex(new Vector2(1, .75f), Color4.White),
                new PolygonVertex(new Vector2(.75f, 1), Color4.White),
                new PolygonVertex(new Vector2(.25f, 1), Color4.White),
                new PolygonVertex(new Vector2(0, .75f), Color4.Gray),
                new PolygonVertex(new Vector2(0, .25f), Color4.Gray)
            }), new Bitmap("soldier_logo.png"))
            {
                Transform = {Position = new SM.Base.Types.Vector2(-50,0)}
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