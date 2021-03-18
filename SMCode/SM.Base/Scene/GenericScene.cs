#region usings

using System;
using System.Collections.Generic;
using SM.Base.Utility;
using SM.Base.Window;
using SM.Base.Window.Contexts;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     A generic scene, that imports functions for scene control.
    /// </summary>
    public abstract class GenericScene : IInitializable
    {
        private IBackgroundItem _background;
        private readonly Dictionary<Type, object> _extensions = new Dictionary<Type, object>();

        private GenericItemCollection _hud;
        private GenericItemCollection _objectCollection;

        /// <summary>
        ///     A collection for cameras to switch easier to different cameras.
        /// </summary>
        public Dictionary<string, GenericCamera> Cameras = new Dictionary<string, GenericCamera>();

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
        ///     Objects inside the scene.
        /// </summary>
        public GenericItemCollection Objects
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
        public GenericItemCollection HUD
        {
            get => _hud;
            set
            {
                value.Parent = this;
                _hud = value;
            }
        }


        /// <summary>
        ///     If true, shows a axis helper at (0,0,0)
        /// </summary>
        public bool ShowAxisHelper { get; set; } = false;

        /// <summary>
        ///     The active camera, that is used if the context doesn't force the viewport camera.
        ///     <para>If none set, it automaticly uses the viewport camera.</para>
        /// </summary>
        public GenericCamera Camera { get; set; }

        /// <summary>
        ///     A camera to control the background.
        /// </summary>
        public GenericCamera BackgroundCamera { get; set; }

        /// <summary>
        ///     A camera to control the HUD.
        /// </summary>
        public GenericCamera HUDCamera { get; set; }

        /// <summary>
        ///     If true, the scene was already initialized.
        /// </summary>
        public bool IsInitialized { get; set; }

        /// <inheritdoc/>
        public virtual void Activate()
        {
        }

        /// <inheritdoc/>
        public virtual void Initialization()
        {
        }

        /// <summary>
        ///     Updates this scene.
        /// </summary>
        /// <param name="context"></param>
        public virtual void Update(UpdateContext context)
        {
            _objectCollection?.Update(context);
            _hud?.Update(context);
        }

        /// <summary>
        /// Executes a fixed update for this scene.
        /// </summary>
        public virtual void FixedUpdate(FixedUpdateContext context)
        {
            _objectCollection?.FixedUpdate(context);
            _hud?.FixedUpdate(context);
        }

        /// <summary>
        ///     Draws this scene.
        /// </summary>
        public virtual void Draw(DrawContext context)
        {
            DrawBackground(context);

            DrawMainObjects(context);

            DrawHUD(context);
            DrawDebug(context);
        }

        /// <summary>
        ///     Draws only the background.
        /// </summary>
        /// <param name="context"></param>
        public virtual void DrawBackground(DrawContext context)
        {
            var backgroundDrawContext = context;
            backgroundDrawContext.SetCamera(BackgroundCamera);
            _Background?.Draw(backgroundDrawContext);
        }

        /// <summary>
        ///     Draws only the main objects
        /// </summary>
        /// <param name="context"></param>
        public virtual void DrawMainObjects(DrawContext context)
        {
            if (!context.Window.ForceViewportCamera && Camera != null) context.SetCamera(Camera);
            _objectCollection.Draw(context);
        }

        /// <summary>
        ///     Draws only the HUD
        /// </summary>
        /// <param name="context"></param>
        public virtual void DrawHUD(DrawContext context)
        {
            context.SetCamera(HUDCamera);
            _hud?.Draw(context);
        }

        /// <summary>
        ///     Draw the debug informations.
        /// </summary>
        /// <param name="context"></param>
        public virtual void DrawDebug(DrawContext context)
        {
        }

        /// <summary>
        ///     Adds a extension to the scene.
        /// </summary>
        /// <param name="extension"></param>
        public virtual void SetExtension(object extension)
        {
            _extensions[extension.GetType()] = extension;
        }

        /// <summary>
        ///     Gets a extension with the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual T GetExtension<T>() where T : class
        {
            object ext = _extensions[typeof(T)];
            if (ext == null)
            {
                Log.Write(LogType.Warning,
                    $"Tried to get the extension '{typeof(T).Name}', that doesn't exist in the scene.");
                return null;
            }

            return (T) ext;
        }

        /// <summary>
        /// This is triggered when the scene gets deactivated.
        /// </summary>
        public virtual void Deactivate()
        {
        }
    }

    /// <summary>
    ///     A generic scene that imports different functions.
    /// </summary>
    /// <typeparam name="TCamera">The type of cameras.</typeparam>
    /// <typeparam name="TItem">The type of show items.</typeparam>
    /// <typeparam name="TCollection">The type for collections</typeparam>
    public abstract class GenericScene<TCamera, TCollection> : GenericScene
        where TCamera : GenericCamera, new()
        where TCollection : GenericItemCollection, new()
    {
        public new TCollection Objects
        {
            get => (TCollection) base.Objects;
            set => base.Objects = value;
        }

        public new TCollection HUD
        {
            get
            {
                base.HUD ??= new TCollection();
                return (TCollection) base.HUD;
            }
            set => base.HUD = value;
        }

        public new TCamera Camera
        {
            get => (TCamera) base.Camera;
            set => base.Camera = value;
        }

        public new TCamera HUDCamera
        {
            get => (TCamera) base.HUDCamera;
            set => base.HUDCamera = value;
        }

        public new TCamera BackgroundCamera
        {
            get => (TCamera) base.BackgroundCamera;
            set => base.BackgroundCamera = value;
        }
    }
}