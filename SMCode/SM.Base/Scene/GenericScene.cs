#region usings

using System.Collections.Generic;
using System.Dynamic;
using SM.Base.Contexts;
using SM.Base.Drawing;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     A generic scene, that imports functions for scene control.
    /// </summary>
    public abstract class GenericScene
    {
        private IBackgroundItem _background;
        
        /// <summary>
        ///     This contains the background.
        /// </summary>
        protected IBackgroundItem _Background
        {
            get => _background;
            set
            {
                value.Parent = this;
                _background = value;
            }
        }
        /// <summary>
        /// If true, the scene was already initialized.
        /// </summary>
        public bool IsInitialized { get; private set; }


        /// <summary>
        ///     Updates this scene.
        /// </summary>
        /// <param name="context"></param>
        public virtual void Update(UpdateContext context)
        {
        }

        /// <summary>
        ///     Draws this scene.
        /// </summary>
        public virtual void Draw(DrawContext context)
        {
        }

        /// <summary>
        ///     Called, when the user activates the scene.
        /// </summary>
        internal void Activate()
        {
            if (!IsInitialized)
            {
                OnInitialization();
                IsInitialized = true;
            }

            OnActivating();
        }

        /// <summary>
        ///     Called, when the user activates the scene for the first time.
        /// </summary>
        protected virtual void OnInitialization()
        { }

        /// <summary>
        ///     Called, when the user activates the scene.
        /// </summary>
        protected virtual void OnActivating()
        {
        }
    }

    /// <summary>
    ///     A generic scene that imports different functions.
    /// </summary>
    /// <typeparam name="TCamera">The type of cameras.</typeparam>
    /// <typeparam name="TItem">The type of show items.</typeparam>
    /// <typeparam name="TCollection">The type for collections</typeparam>
    public abstract class GenericScene<TCamera, TCollection, TItem> : GenericScene
        where TCamera : GenericCamera, new()
        where TCollection : GenericItemCollection<TItem>, new()
        where TItem : IShowItem
    {
        private TCollection _hud = new TCollection();
        private TCollection _objectCollection = new TCollection();

        public bool ShowAxisHelper { get; set; } = false;

        /// <summary>
        ///     A collection for cameras to switch easier to different cameras.
        /// </summary>
        public Dictionary<string, TCamera> Cameras = new Dictionary<string, TCamera>();

        /// <summary>
        ///     The active camera, that is used if the context doesn't force the viewport camera.
        ///     <para>If none set, it automaticly uses the viewport camera.</para>
        /// </summary>
        public TCamera Camera { get; set; }

        /// <summary>
        ///     A camera to control the background.
        /// </summary>
        public TCamera BackgroundCamera { get; set; } = new TCamera();

        /// <summary>
        ///     A camera to control the HUD.
        /// </summary>
        public TCamera HUDCamera { get; set; } = new TCamera();

        /// <summary>
        ///     Objects inside the scene.
        /// </summary>
        public TCollection Objects
        {
            get => _objectCollection;
            set
            {
                value.Parent = this;
                _objectCollection = value;
            }
        }

        /// <summary>
        ///     This defines the HUD objects.
        /// </summary>
        public TCollection HUD
        {
            get => _hud;
            set
            {
                value.Parent = this;
                _hud = value;
            }
        }


        /// <inheritdoc />
        public override void Update(UpdateContext context)
        {
            _objectCollection.Update(context);
            _hud.Update(context);
        }

        /// <inheritdoc />
        public override void Draw(DrawContext context)
        {
            DrawBackground(context);

            DrawMainObjects(context);

            DrawHUD(context);
            DrawDebug(context);
        }

        public void DrawBackground(DrawContext context)
        {
            var backgroundDrawContext = context;
            backgroundDrawContext.View = BackgroundCamera.CalculateViewMatrix();
            _Background?.Draw(backgroundDrawContext);
        }

        public void DrawMainObjects(DrawContext context)
        {
            if (!context.ForceViewport && Camera != null) context.View = Camera.CalculateViewMatrix();
            _objectCollection.Draw(context);
        }

        public void DrawHUD(DrawContext context)
        {
            context.View = HUDCamera.CalculateViewMatrix();
            _hud.Draw(context);
        }

        public virtual void DrawDebug(DrawContext context)
        {

        }
    }
}