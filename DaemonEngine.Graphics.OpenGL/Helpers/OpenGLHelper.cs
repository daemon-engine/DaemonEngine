
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.OpenGL.DllImport.Enums;

namespace DaemonEngine.Graphics.OpenGL.Helpers;
internal static class OpenGLHelper
{
    public static GLClearMask ClearMaskToOpenGLType(ClearMask clearMask)
    {
        var value = GLClearMask.None;

        if ((clearMask & ClearMask.ColorBufferBit) == ClearMask.ColorBufferBit)
        {
            value |= GLClearMask.ColorBufferBit;
        }

        if ((clearMask & ClearMask.DepthBufferBit) == ClearMask.DepthBufferBit)
        {
            value |= GLClearMask.DepthBufferBit;
        }

        if ((clearMask & ClearMask.StencilBufferBit) == ClearMask.StencilBufferBit)
        {
            value |= GLClearMask.StencilBufferBit;
        }

        return value;
    }
}
