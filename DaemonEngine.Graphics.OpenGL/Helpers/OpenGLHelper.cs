
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.OpenGL.DllImport.Enums;

namespace DaemonEngine.Graphics.OpenGL.Helpers;
internal static class OpenGLHelper
{
    public static GLClearMask ClearMaskToOpenGLType(ClearMask clearMask)
    {
        return (GLClearMask)clearMask;
    }
}
