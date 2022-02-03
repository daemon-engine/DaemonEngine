
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.OpenGL.DllImport.Enums;

namespace DaemonEngine.Graphics.OpenGL.Helpers;
internal static class OpenGLHelper
{
    public static GLClearMask ClearMaskToOpenGLType(ClearMask clearMask)
    {
        switch (clearMask)
        {
            case ClearMask.DepthBufferBit:
                return GLClearMask.DepthBufferBit;
            case ClearMask.StencilBufferBit:
                return GLClearMask.StencilBufferBit;
            case ClearMask.ColorBufferBit:
                return GLClearMask.ColorBufferBit;
            default:
                return GLClearMask.None;
        }
    }
}
