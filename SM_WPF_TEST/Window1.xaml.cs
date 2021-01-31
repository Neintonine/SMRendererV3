using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OpenTK.Graphics;
using SM2D;
using SM2D.Drawing;
using SM2D.Pipelines;
using SM2D.Scene;

namespace SM_WPF_TEST
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            GLWPFWindow2D gl;
            Scene scene;
            Content = gl = new GLWPFWindow2D();
            gl.Start();

            gl.SetScene(scene = new Scene());
            gl.SetRenderPipeline(Basic2DPipeline.Pipeline);

            DrawObject2D obj = new DrawObject2D()
            {
                Color = Color4.Red
            };
            obj.ApplyCircle();
            scene.Objects.Add(obj);
        }
    }
}
