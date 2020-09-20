using System.Collections.Generic;
using OpenTK;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    public abstract class GenericScene<TCamera> : IShowCollection
        where TCamera : GenericCamera, new()
    {

        protected IBackgroundItem _background;
        public List<IShowItem> HUD { get; } = new List<IShowItem>();
        public List<IShowItem> Objects { get; } = new List<IShowItem>();
        public TCamera Camera { get; set; }
        public TCamera BackgroundCamera { get; set; } = new TCamera();
        public Dictionary<string, TCamera> Cameras = new Dictionary<string, TCamera>();

        public void Draw(DrawContext context)
        {
            if (!context.ForceViewport && Camera != null) context.View = Camera.ViewMatrix;

            DrawContext backgroundDrawContext = context;
            backgroundDrawContext.View = BackgroundCamera.CalculateViewMatrix();
            _background?.Draw(backgroundDrawContext);

            for(int i = 0; i < Objects.Count; i++)
                Objects[i].Draw(context);

            context.View = Matrix4.Identity;
            for (int i = 0; i < HUD.Count; i++) 
                HUD[i].Draw(context);
        }

    }
}