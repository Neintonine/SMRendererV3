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
        /// <inheritdoc />
        protected override bool AutoCompile { get; set; } = true;

        public static IFramebufferWindow ScreenWindow;

        /// <summary>
        /// Represents the screen buffer.
        /// </summary>
        public static readonly Framebuffer Screen = new Framebuffer
        {
            _id = 0,
            CanCompile = false,
            _window = ScreenWindow,
            _windowScale = 1,
        };
        
        private IFramebufferWindow _window;
        private float _windowScale;

        /// <inheritdoc />
        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Framebuffer;

        /// <summary>
        /// Contains the size of the framebuffer.
        /// </summary>
        public Vector2 Size { get; private set; }

        /// <summary>
        /// Contains all color attachments.
        /// </summary>
        public Dictionary<string, ColorAttachment> ColorAttachments { get; private set; } =
            new Dictionary<string, ColorAttachment>();
        /// <summary>
        /// Contains the current renderbuffer attachments of the framebuffer.
        /// </summary>
        public Dictionary<RenderbufferAttachment, int> RenderbufferAttachments { get; } = new Dictionary<RenderbufferAttachment, int>();

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

            foreach (RenderbufferAttachment attachment in RenderbufferAttachments.Keys.ToArray())
            {
                int att = attachment.Generate(this);
                GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, attachment.FramebufferAttachment, RenderbufferTarget.Renderbuffer, att);
                RenderbufferAttachments[attachment] = att;
            }

            var err = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (err != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Failed loading framebuffer.\nProblem: " + err);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            
            foreach (var attachment in ColorAttachments.Values) attachment.Dispose();
            foreach (KeyValuePair<RenderbufferAttachment, int> pair in RenderbufferAttachments.ToArray())
            {
                GL.DeleteRenderbuffer(pair.Value);
                RenderbufferAttachments[pair.Key] = -1;
            }
            GL.DeleteFramebuffer(this);
            base.Dispose();

        }

        /// <summary>
        /// Appends a color attachment.
        /// </summary>
        public void Append(string key, int pos) => Append(key, new ColorAttachment(pos));
        /// <summary>
        /// Appends a color attachment.
        /// </summary>
        public void Append(string key, ColorAttachment value)
        {
            ColorAttachments.Add(key, value);
        }

        /// <summary>
        /// Appends a renderbuffer attachment to the framebuffer.
        /// </summary>
        /// <param name="attachment"></param>
        public void AppendRenderbuffer(RenderbufferAttachment attachment)
        {
            RenderbufferAttachments.Add(attachment, -1);
        }

        /// <summary>
        /// Activates the framebuffer without clearing the buffer.
        /// </summary>
        public void Activate()
        {
            Activate(FramebufferTarget.Framebuffer, ClearBufferMask.None);
        }

        /// <summary>
        /// Activates the framebuffer for the specific target framebuffer and without clearing.
        /// </summary>
        /// <param name="target"></param>
        public void Activate(FramebufferTarget target)
        {
            Activate(target, ClearBufferMask.None);
        }

        /// <summary>
        /// Activates the framebuffer while clearing the specified buffer.
        /// </summary>
        /// <param name="clearMask"></param>
        public void Activate(ClearBufferMask clearMask)
        {
            Activate(FramebufferTarget.Framebuffer, clearMask);
        }

        /// <summary>
        /// Activates the framebuffer for the specific target and with clearing.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="clear"></param>
        public void Activate(FramebufferTarget target, ClearBufferMask clear)
        {
            GL.BindFramebuffer(target, this);
            GL.Clear(clear);
        }

        /// <summary>
        /// Returns a <see cref="Framebuffer"/> handle of the current framebuffer.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Framebuffer GetCurrentlyActive(FramebufferTarget target = FramebufferTarget.Framebuffer)
        {
            Framebuffer buffer = new Framebuffer()
            {
                CanCompile = false,
                ReportAsNotCompiled = true
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