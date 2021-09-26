#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

#endregion

namespace SM.OGL.Framebuffer
{
    /// <summary>
    /// Represents a OpenGL Framebuffer.
    /// </summary>
    public class Framebuffer : GLObject
    {
        static Framebuffer CurrentlyActiveFramebuffer;
        static Framebuffer CurrentlyActiveDrawFramebuffer;
        static Framebuffer CurrentlyActiveReadFramebuffer;

        /// <inheritdoc />
        protected override bool AutoCompile { get; set; } = true;

        /// <summary>
        /// The window for the screen
        /// </summary>
        public static IFramebufferWindow ScreenWindow;

        /// <summary>
        /// Represents the screen buffer.
        /// </summary>
        public static readonly Framebuffer Screen = new Framebuffer()
        {
            _id = 0,
            CanCompile = false,
            _window = ScreenWindow,
            _windowScale = 1,
            DefaultApplyViewport = false
        };
        
        private IFramebufferWindow _window;
        private float _windowScale;

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Framebuffer;

        /// <summary>
        /// Contains the size of the framebuffer.
        /// </summary>
        public Vector2 Size { get; private set; }

        public bool DefaultApplyViewport { get; set; } = true;

        public ColorAttachment FirstColorAttachment { get; private set; }

        /// <summary>
        /// Contains all color attachments.
        /// </summary>
        public Dictionary<string, ColorAttachment> ColorAttachments { get; private set; } =
            new Dictionary<string, ColorAttachment>();
        /// <summary>
        /// Contains the current renderbuffer attachments of the framebuffer.
        /// </summary>
        public List<RenderbufferAttachment> RenderbufferAttachments { get; } = new List<RenderbufferAttachment>();

        /// <summary>
        /// Gets the color attachment with the specified color name.
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        public ColorAttachment this[string colorName] => ColorAttachments[colorName];

        /// <summary>
        /// Creates a buffer without any options.
        /// </summary>
        public Framebuffer()
        {
        }

        /// <summary>
        /// Creates a buffer, while always respecting the screen.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="scale"></param>
        public Framebuffer(IFramebufferWindow window, float scale = 1) : this(new Vector2(window.Width * scale,
            window.Height * scale))
        {
            _window = window;
            _windowScale = scale;
        }

        /// <summary>
        /// Creates a buffer, with a specified size.
        /// </summary>
        /// <param name="size"></param>
        public Framebuffer(Vector2 size)
        {
            Size = size;
        }

        /// <inheritdoc />
        public override void Compile()
        {
            if (_id == 0) _window = ScreenWindow;
            if (_window != null) Size = new Vector2(_window.Width * _windowScale, _window.Height * _windowScale);

            base.Compile();
            _id = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _id);

            var enums = new DrawBuffersEnum[ColorAttachments.Count];
            var c = 0;
            foreach (var pair in ColorAttachments)
            {
                pair.Value.Generate(this);

                enums[c++] = pair.Value.DrawBuffersEnum;
            }

            GL.DrawBuffers(enums.Length, enums);
            foreach (var pair in ColorAttachments)
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, pair.Value.FramebufferAttachment, pair.Value.Target, pair.Value.ID,
                    0);
            FramebufferErrorCode err;

            err = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (err != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Failed loading framebuffer.\nProblem: " + err);

            foreach (RenderbufferAttachment attachment in RenderbufferAttachments)
            {
                attachment.Generate(this);
                GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment.FramebufferAttachment, RenderbufferTarget.Renderbuffer, attachment.ID);
            }

            err = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (err != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Failed loading framebuffer.\nProblem: " + err);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <summary>
        /// Disposes and clears the attachment
        /// </summary>
        public void Reset()
        {
            Dispose();
            ColorAttachments.Clear();
            RenderbufferAttachments.Clear();
        }

        /// <inheritdoc />
        public override void Dispose()
        {

            foreach (var attachment in ColorAttachments.Values) attachment.Dispose();
            FirstColorAttachment = null;
            foreach (RenderbufferAttachment pair in RenderbufferAttachments.ToArray())
            {
                GL.DeleteRenderbuffer(pair.ID);
            }
            GL.DeleteFramebuffer(this);
            base.Dispose();

        }

        /// <summary>
        /// Appends a color attachment.
        /// </summary>
        public void Append(string key, int pos) => Append(key, new ColorAttachment(pos, null));
        public void Append(string key, Vector2 size, int pos) => Append(key, new ColorAttachment(pos, size));
        /// <summary>
        /// Appends a color attachment.
        /// </summary>
        public void Append(string key, ColorAttachment value)
        {
            if (ColorAttachments.Count == 0) FirstColorAttachment = value;
            ColorAttachments.Add(key, value);
        }

        /// <summary>
        /// Appends a renderbuffer attachment to the framebuffer.
        /// </summary>
        /// <param name="attachment"></param>
        public void AppendRenderbuffer(RenderbufferAttachment attachment)
        {
            if (RenderbufferAttachments.Contains(attachment)) return;
            RenderbufferAttachments.Add(attachment);
        }

        /// <summary>
        /// Activates the framebuffer without clearing the buffer.
        /// </summary>
        public void Activate(bool? applyViewport = null)
        {
            Activate(FramebufferTarget.Framebuffer, ClearBufferMask.None, applyViewport);
        }

        /// <summary>
        /// Activates the framebuffer for the specific target framebuffer and without clearing.
        /// </summary>
        /// <param name="target"></param>
        public void Activate(FramebufferTarget target, bool? applyViewport = null)
        {
            Activate(target, ClearBufferMask.None, applyViewport);
        }

        /// <summary>
        /// Activates the framebuffer while clearing the specified buffer.
        /// </summary>
        /// <param name="clearMask"></param>
        public void Activate(ClearBufferMask clearMask, bool? applyViewport = null)
        {
            Activate(FramebufferTarget.Framebuffer, clearMask, applyViewport);
        }

        /// <summary>
        /// Activates the framebuffer for the specific target and with clearing.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="clear"></param>
        public void Activate(FramebufferTarget target, ClearBufferMask clear, bool? applyViewport = null)
        {
            switch (target)
            {
                case FramebufferTarget.ReadFramebuffer:
                    CurrentlyActiveReadFramebuffer = this;
                    break;
                case FramebufferTarget.DrawFramebuffer:
                    CurrentlyActiveDrawFramebuffer = this;
                    break;
                case FramebufferTarget.Framebuffer:
                    CurrentlyActiveFramebuffer = this;
                    break;
            }

            GL.BindFramebuffer(target, this);
            if (applyViewport.GetValueOrDefault(DefaultApplyViewport)) 
                GL.Viewport(0, 0, (int)Size.X, (int)Size.Y);
            GL.Clear(clear);
        }

        /// <summary>
        /// Copies content to the specified Framebuffer.
        /// </summary>
        public void CopyTo(Framebuffer target, ClearBufferMask clear = ClearBufferMask.ColorBufferBit, BlitFramebufferFilter filter = BlitFramebufferFilter.Linear)
        {
            Activate(FramebufferTarget.ReadFramebuffer, false);
            target.Activate(FramebufferTarget.DrawFramebuffer, false);

            GL.BlitFramebuffer(0, 0, (int)Size.X, (int)Size.Y, 0, 0, (int)Size.X, (int)Size.Y, clear, filter);
        }

        /// <summary>
        /// Returns a <see cref="Framebuffer"/> handle of the current framebuffer.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Framebuffer GetCurrentlyActive(FramebufferTarget target = FramebufferTarget.Framebuffer)
        {
            switch (target)
            {
                case FramebufferTarget.ReadFramebuffer:
                    return CurrentlyActiveReadFramebuffer ??= getCurrentlyActive(target);
                case FramebufferTarget.DrawFramebuffer:
                    return CurrentlyActiveDrawFramebuffer ??= getCurrentlyActive(target);
                case FramebufferTarget.Framebuffer:
                    return CurrentlyActiveFramebuffer ??= getCurrentlyActive(target);
            }
            return null;
        }
        static Framebuffer getCurrentlyActive(FramebufferTarget target = FramebufferTarget.Framebuffer)
        {
            Framebuffer buffer = new Framebuffer()
            {
                CanCompile = false,
                ReportAsNotCompiled = true,
                DefaultApplyViewport = false
            };
            switch (target)
            {
                case FramebufferTarget.ReadFramebuffer:
                    GL.GetInteger(GetPName.ReadFramebufferBinding, out buffer._id);
                    break;
                case FramebufferTarget.DrawFramebuffer:
                    GL.GetInteger(GetPName.DrawFramebufferBinding, out buffer._id);
                    break;
                case FramebufferTarget.Framebuffer:
                    GL.GetInteger(GetPName.FramebufferBinding, out buffer._id);
                    break;
            }



            return buffer;
        }
    }
}