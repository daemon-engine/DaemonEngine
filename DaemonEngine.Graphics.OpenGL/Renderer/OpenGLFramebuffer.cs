using DaemonEngine.Graphics.OpenGL.DllImport.Enums;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLFramebuffer : FramebufferBase
{
    private uint _id;

    private readonly List<FramebufferAttachment> _colorAttachments;
    private readonly FramebufferAttachment _depthAttachment;

    private uint[] _colorAttachmentIds;
    private uint _depthAttachmentId;

    public OpenGLFramebuffer(ILogger logger, FramebufferOptions framebufferOptions)
        : base(logger, framebufferOptions)
    {
        var status = GL.CheckFramebufferStatus(GLConstants.GL_FRAMEBUFFER);
        if (status == GLFramebufferStatus.Unsupported)
        {
            Logger.Error("Framebuffers are Unsupported!");
            return;
        }

        _colorAttachments = new List<FramebufferAttachment>();
        foreach (var attachment in Options.Attachments)
        {
            if (attachment == FramebufferAttachment.Depth)
            {
                _depthAttachment = attachment;
            }
            else
            {
                _colorAttachments.Add(attachment);
            }
        }

        Invalidate();
    }

    private void Invalidate()
    {
        uint[] ids = new uint[1];
        GL.GenFramebuffers(1, ref ids);
        _id = ids[0];

        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, _id);

        if (_colorAttachments.Any())
        {
            _colorAttachmentIds = new uint[_colorAttachments.Count];
            GL.GenTextures(_colorAttachments.Count, ref _colorAttachmentIds);
            CreateColorAttachments(_colorAttachments);
        }

        if (_depthAttachment != FramebufferAttachment.None)
        {
            uint[] depthIds = new uint[1];
            GL.GenTextures(1, ref depthIds);
            _depthAttachmentId = depthIds[0];
            GL.BindTexture(GLConstants.GL_TEXTURE_2D, _depthAttachmentId);

            GL.TexStorage2D(GLConstants.GL_TEXTURE_2D, 1, GLConstants.GL_DEPTH24_STENCIL8, Options.Width, Options.Height);

            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MIN_FILTER, GLConstants.GL_LINEAR);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MAG_FILTER, GLConstants.GL_LINEAR);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_R, GLConstants.GL_CLAMP_TO_EDGE);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_S, GLConstants.GL_CLAMP_TO_EDGE);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_T, GLConstants.GL_CLAMP_TO_EDGE);

            GL.FramebufferTexture2D(GLConstants.GL_FRAMEBUFFER, GLConstants.GL_DEPTH_STENCIL_ATTACHMENT, GLConstants.GL_TEXTURE_2D, _depthAttachmentId, 0);
        }

        if (_colorAttachments.Count > 1)
        {
            uint[] buffers = new uint[4]
            { 
                GLConstants.GL_COLOR_ATTACHMENT0, GLConstants.GL_COLOR_ATTACHMENT1, GLConstants.GL_COLOR_ATTACHMENT2, GLConstants.GL_COLOR_ATTACHMENT3
            };
            GL.DrawBuffers(_colorAttachments.Count, buffers);
        }
        
        if(!_colorAttachments.Any())
        {
            GL.DrawBuffer(GLConstants.GL_NONE);
        }

        var status = GL.CheckFramebufferStatus(GLConstants.GL_FRAMEBUFFER);
        if (status != GLFramebufferStatus.Complete)
        {
            Logger.Error("Framebuffer are Incomplete!");
        }

        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, 0);
    }

    private void CreateColorAttachments(List<FramebufferAttachment> colorAttachments)
    {
        for (uint i = 0; i < _colorAttachmentIds.Length; i++)
        {
            GL.BindTexture(GLConstants.GL_TEXTURE_2D, _colorAttachmentIds[i]);

            GL.TexImage2D(GLConstants.GL_TEXTURE_2D, 0, GLConstants.GL_RGB, Options.Width, Options.Height, 0, GLConstants.GL_RGB, GLConstants.GL_UNSIGNED_BYTE, null);

            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MIN_FILTER, GLConstants.GL_LINEAR);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MAG_FILTER, GLConstants.GL_LINEAR);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_R, GLConstants.GL_CLAMP_TO_EDGE);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_S, GLConstants.GL_CLAMP_TO_EDGE);
            GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_T, GLConstants.GL_CLAMP_TO_EDGE);

            GL.FramebufferTexture2D(GLConstants.GL_FRAMEBUFFER, GLConstants.GL_COLOR_ATTACHMENT0 + i, GLConstants.GL_TEXTURE_2D, _colorAttachmentIds[i], 0);
        }
    }

    public override uint GetColorAttachment(int index)
    {
        return _colorAttachmentIds[index];
    }

    public override void Resize(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            return;
        }

        Options.Width = width;
        Options.Height = height;

        Invalidate();
    }

    public override void Clear()
    {
        GL.ClearColor(Options.ClearColor.X, Options.ClearColor.Y, Options.ClearColor.Z, Options.ClearColor.W);
        GL.Clear(GLClearMask.ColorBufferBit | GLClearMask.DepthBufferBit);
    }

    public override void Bind()
    {
        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, _id);
        GL.Viewport(0, 0, Options.Width, Options.Height);
    }

    public override void Unbind()
    {
        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, 0);
    }
}
