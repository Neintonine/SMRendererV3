using System.Collections.Generic;
using OpenTK;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    public abstract class GenericScene<TCamera, TItem> : IShowCollection<TItem>
        where TCamera : GenericCamera, new()
        where TItem : IShowItem
    {

        protected IBackgroundItem _background;
        public List<TItem> HUD { get; } = new List<TItem>();
        public List<TItem> Objects { get; } = new List<TItem>();
        public TCamera Camera { get; set; }
        public TCamera BackgroundCamera { get; set; } = new TCamera();
        public TCamera HUDCamera { get; set; } = new TCamera();
        public Dictionary<string, TCamera> Cameras = new Dictionary<string, TCamera>();

        public virtual void Draw(DrawContext context)
        {
            if (!context.ForceViewport && Camera != null) context.View = Camera.ViewMatrix;

            DrawContext backgroundDrawContext = context;
            backgroundDrawContext.View = BackgroundCamera.CalculateViewMatrix();
            _background?.Draw(backgroundDrawContext);

            for(int i = 0; i < Objects.Count; i++)
                Objects[i].Draw(context);

            context.View = HUDCamera.CalculateViewMatrix();
            for (int i = 0; i < HUD.Count; i++) 
                HUD[i].Draw(context);
        }

    }
}