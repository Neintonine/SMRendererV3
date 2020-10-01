using System.Collections.Generic;
using OpenTK;
using SM.Base.Contexts;

namespace SM.Base.Scene
{
    /// <summary>
    /// A generic scene that imports different functions.
    /// </summary>
    /// <typeparam name="TCamera">The type of cameras.</typeparam>
    /// <typeparam name="TItem">The type of show items.</typeparam>
    public abstract class GenericScene<TCamera, TItem> : GenericItemCollection<TItem>
        where TCamera : GenericCamera, new()
        where TItem : IShowItem
    {
        /// <summary>
        /// The active camera, that is used if the context doesn't force the viewport camera.
        /// <para>If none set, it automaticly uses the viewport camera.</para>
        /// </summary>
        public TCamera Camera { get; set; }
        /// <summary>
        /// A camera to control the background.
        /// </summary>
        public TCamera BackgroundCamera { get; set; } = new TCamera();
        /// <summary>
        /// A camera to control the HUD.
        /// </summary>
        public TCamera HUDCamera { get; set; } = new TCamera();

        /// <summary>
        /// This contains the background.
        /// </summary>
        protected IBackgroundItem _background;
        /// <summary>
        /// This defines the HUD objects.
        /// </summary>
        public List<TItem> HUD { get; } = new List<TItem>();
        /// <summary>
        /// A collection for cameras to switch easier to different cameras.
        /// </summary>
        public Dictionary<string, TCamera> Cameras = new Dictionary<string, TCamera>();

        /// <inheritdoc />
        public override void Draw(DrawContext context)
        {
            if (!context.ForceViewport && Camera != null) context.View = Camera.CalculateViewMatrix();

            DrawContext backgroundDrawContext = context;
            backgroundDrawContext.View = BackgroundCamera.CalculateViewMatrix();
            _background?.Draw(backgroundDrawContext);

            base.Draw(context);

            context.View = HUDCamera.CalculateViewMatrix();
            for (int i = 0; i < HUD.Count; i++) 
                HUD[i].Draw(context);
        }

        /// <summary>
        /// Called, when the user activates the scene.
        /// </summary>
        internal void Activate()
        {
            OnActivating();
        }

        /// <summary>
        /// Called, when the user activates the scene.
        /// </summary>
        protected virtual void OnActivating()
        { }
    }
}