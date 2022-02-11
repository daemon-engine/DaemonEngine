using DaemonEngine.Graphics.Renderer;
using DaemonEngine.GraphicsLibraryFramework.DllImport;
using DaemonEngine.GraphicsLibraryFramework.DllImport.Structs;
using DaemonEngine.OpenGL.DllImport;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

public class OpenGLContext : GraphicsContextBase
{
    private readonly GlfwNativeWindowHandle _windowHandler;
    public OpenGLContext(ILogger logger, object windowHandler)
        : base(logger)
    {
        _windowHandler = (GlfwNativeWindowHandle)windowHandler;
    }

    public override void Initialize()
    {
        Glfw.MakeContextCurrent(_windowHandler);

        var vendor = GL.GetString(GLConstants.GL_VENDOR);
        var renderer = GL.GetString(GLConstants.GL_RENDERER);
        var version = GL.GetString(GLConstants.GL_VERSION);

        Logger.Information("----------------------------------------------");
        Logger.Information("OpenGL Info:");
        Logger.Information($"| Vendor: {vendor}");
        Logger.Information($"| Renderer: {renderer}");
        Logger.Information($"| Version: {version}");
        Logger.Information("----------------------------------------------");
    }
}
