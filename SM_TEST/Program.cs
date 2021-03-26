﻿using System;
using System.Diagnostics;
using OpenTK;
using SM.Base.Controls;
using SM.Base.Time;
using SM.Base.Window;
using SM2D;
using SM2D.Drawing;
using SM2D.Object;
using SM2D.Scene;

namespace SM_TEST
{
    class Program
    {
        static Scene scene;
        private static GLWindow window;
        private static PolyLine line;
        private static DrawParticles particles;
        static void Main(string[] args)
        {
            window = new GLWindow(1280, 720, "0ms", WindowFlags.Window, VSyncMode.Off);
            window.ApplySetup(new Window2DSetup());
            window.SetRenderPipeline(new TestRenderPipeline());

            window.SetScene(scene = new Scene());

            particles = new DrawParticles(TimeSpan.FromSeconds(5))
            {
                MaxSpeed = 50,
            };
            particles.Transform.Size.Set(20);
            scene.Objects.Add(particles);

            window.UpdateFrame += WindowOnUpdateFrame;
            window.RenderFrame += Window_RenderFrame;
            window.Run();

            Debug.WriteLine("Window Closed");
        }

        private static void Window_RenderFrame(object sender, FrameEventArgs e)
        {
            window.Title = Math.Floor(e.Time * 1000) + "ms";
        }

        private static void WindowOnUpdateFrame(object sender, FrameEventArgs e)
        {
            if (Mouse.LeftClick)
                particles.Trigger();
        }
    }
}