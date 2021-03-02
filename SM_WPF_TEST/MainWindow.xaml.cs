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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenTK.Graphics;
using SM2D;
using SM2D.Drawing;
using SM2D.Pipelines;
using SM2D.Scene;

namespace SM_WPF_TEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /*
            GLWPFWindow2D gl;
            Scene scene;
            gl = new GLWPFWindow2D();
            Grid.SetColumn(gl, 1);
            grid.Children.Add(gl);

            gl.Start();

            gl.SetScene(scene = new Scene());
            gl.SetRenderPipeline(Default2DPipeline.Pipeline);

            DrawObject2D cube = new DrawObject2D();
            cube.Color = Color4.Blue;
            scene.Objects.Add(cube);

            new Window1().Show();*/
        }
    }
}
