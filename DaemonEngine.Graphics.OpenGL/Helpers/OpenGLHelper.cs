using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;

namespace DaemonEngine.Graphics.OpenGL.Helpers;

internal static class OpenGLHelper
{
    public static uint FramebufferAttachmentToGLType(FramebufferAttachmentType framebufferAttachment)
    {
        return framebufferAttachment switch
        {
            FramebufferAttachmentType.RGB => GLConstants.GL_RGB,
            FramebufferAttachmentType.RGBA8 => GLConstants.GL_RGBA8,
            FramebufferAttachmentType.DEPTH24STENCIL8 => GLConstants.GL_DEPTH24_STENCIL8,
            _ => GLConstants.GL_RGB,
        };
    }

    public static GLClearMask ClearMaskToOpenGLType(ClearMask clearMask)
    {
        return (GLClearMask)clearMask;
    }
}
