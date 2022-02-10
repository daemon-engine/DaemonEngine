using DaemonEngine.Graphics.OpenGL.DllImport.Enums;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLFramebuffer : IFramebuffer
{
    private uint _id;

    private uint _colorAttachment;
    private uint _depthAttachment;

    public OpenGLFramebuffer()
    {
        var status = GL.CheckFramebufferStatus(GLConstants.GL_FRAMEBUFFER);
        if (status == GLFramebufferStatus.Unsupported)
        {
            return;
        }

        Invalidate();
    }

    private void Invalidate()
    {
        uint[] ids = new uint[1];
        GL.GenFramebuffers(1, ref ids);
        _id = ids[0];

        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, _id);

        // Create color attachment
        GL.GenTextures(1, ref ids);
        _colorAttachment = ids[0];
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, _colorAttachment);

        GL.TexImage2D(GLConstants.GL_TEXTURE_2D, 0, GLConstants.GL_RGB, 1366, 768, 0, GLConstants.GL_RGB, GLConstants.GL_UNSIGNED_BYTE, null!);

        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MIN_FILTER, GLConstants.GL_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MAG_FILTER, GLConstants.GL_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_R, GLConstants.GL_CLAMP_TO_EDGE);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_S, GLConstants.GL_CLAMP_TO_EDGE);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_T, GLConstants.GL_CLAMP_TO_EDGE);

        GL.FramebufferTexture2D(GLConstants.GL_FRAMEBUFFER, GLConstants.GL_COLOR_ATTACHMENT0, GLConstants.GL_TEXTURE_2D, _colorAttachment, 0);

        // Create depth attachment
        GL.GenTextures(1, ref ids);
        _depthAttachment = ids[0];
        GL.BindTexture(GLConstants.GL_TEXTURE_2D, _depthAttachment);

        GL.TexStorage2D(GLConstants.GL_TEXTURE_2D, 1, GLConstants.GL_DEPTH24_STENCIL8, 1366, 768);

        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MIN_FILTER, GLConstants.GL_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_MAG_FILTER, GLConstants.GL_LINEAR);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_R, GLConstants.GL_CLAMP_TO_EDGE);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_S, GLConstants.GL_CLAMP_TO_EDGE);
        GL.TexParameteri(GLConstants.GL_TEXTURE_2D, GLConstants.GL_TEXTURE_WRAP_T, GLConstants.GL_CLAMP_TO_EDGE);

        GL.FramebufferTexture2D(GLConstants.GL_FRAMEBUFFER, GLConstants.GL_DEPTH_STENCIL_ATTACHMENT, GLConstants.GL_TEXTURE_2D, _depthAttachment, 0);

        var status = GL.CheckFramebufferStatus(GLConstants.GL_FRAMEBUFFER);
        if (status != GLFramebufferStatus.Complete)
        {
        }

        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, 0);
    }

    public uint GetColorAttachment()
    {
        return _colorAttachment;
    }

    public void Bind()
    {
        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, _id);
    }

    public void Unbind()
    {
        GL.BindFramebuffer(GLConstants.GL_FRAMEBUFFER, 0);
    }
}
