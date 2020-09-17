using System.Collections.Generic;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    public abstract class GenericScene<TCamera> : IShowCollection
        where TCamera : GenericCamera
    {

        public List<IShowItem> Objects { get; } = new List<IShowItem>();
        public TCamera Camera { get; set; }
        public Dictionary<string, TCamera> Cameras = new Dictionary<string, TCamera>();

        public void Draw(DrawContext context)
        {
            if (!context.ForceViewport && Camera != null) context.View = Camera.ViewMatrix;

            for(int i = 0; i < Objects.Count; i++)
                Objects[i].Draw(context);
        }

    }
}