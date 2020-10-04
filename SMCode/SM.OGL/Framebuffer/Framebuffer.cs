using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Platform;

namespace SM.OGL.Framebuffer
{
    // TODO: Write summeries for framebuffer-system.
    public class Framebuffer : GLObject
    {
        public static readonly Framebuffer Screen = new Framebuffer()
        {
            _id = 0,
            _canBeCompiled = false
        };

        private bool _canBeCompiled = true;

        public override ObjectLabelIdentifier TypeIdentifier { get; } = ObjectLabelIdentifier.Framebuffer;

        public Vector2 Size { get; private set; }
        
        public Dictionary<string, ColorAttachment> ColorAttachments { get; private set; } = new Dictionary<string, ColorAttachment>();

        public Framebuffer()
        { }

        public Framebuffer(INativeWindow window, float scale = 1) : this(new Vector2(window.Width * scale, window.Height * scale))
        { }

        public Framebuffer(Vector2 size)
        {
            Size = size;
        }

        public override void Compile()
        {
            if (!_canBeCompiled) return;

            base.Compile();
            _id = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, _id);

            DrawBuffersEnum[] enums = new DrawBuffersEnum[ColorAttachments.Count];
            int c = 0;
            foreach (KeyValuePair<string, ColorAttachment> pair in ColorAttachments)
            {
                pair.Value.Generate(this);

                enums[c++] = pair.Value.DrawBuffersEnum;
            }

            GL.DrawBuffers(enums.Length, enums);
            foreach (var pair in ColorAttachments)
                GL.FramebufferTexture(FramebufferTarget.Framebuffer, pair.Value.FramebufferAttachment, pair.Value.ID, 0);

            FramebufferErrorCode err = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            if (err != FramebufferErrorCode.FramebufferComplete)
                throw new Exception("Failed loading framebuffer.\nProblem: "+err);

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        public override void Dispose()
        {
            base.Dispose();
            foreach (ColorAttachment attachment in ColorAttachments.Values) attachment.Dispose();
            GL.DeleteFramebuffer(this);
        }

        public void Append(string key, ColorAttachment value)
        {
            ColorAttachments.Add(key, value);
        }


        public void Activate() => Activate(FramebufferTarget.Framebuffer, ClearBufferMask.None);
        public void Activate(FramebufferTarget target) => Activate(target, ClearBufferMask.None);
        public void Activate(ClearBufferMask clearMask) => Activate(FramebufferTarget.Framebuffer, clearMask);
        public void Activate(FramebufferTarget target, ClearBufferMask clear)
        {
            GL.Clear(clear);
            GL.BindFramebuffer(target, this);
        }
    }
}