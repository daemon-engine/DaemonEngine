using DaemonEngine.Graphics.Renderer;
using DaemonEngine.GraphicsLibraryFramework.DllImport;
using DaemonEngine.GraphicsLibraryFramework.DllImport.Structs;
using DaemonEngine.OpenGL.DllImport;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

public class OpenGLContext : IGraphicsContext
{
    private readonly GlfwNativeWindowHandle _windowHandler;
    private readonly ILogger _logger;
    public OpenGLContext(ILogger logger, object windowHandler)
    {
        _windowHandler = (GlfwNativeWindowHandle)windowHandler;
        _logger = logger;
    }

    public void Initialize()
    {
        Glfw.MakeContextCurrent(_windowHandler);

        var vendor = GL.GetString(GLConstants.GL_VENDOR);
        var renderer = GL.GetString(GLConstants.GL_RENDERER);
        var version = GL.GetString(GLConstants.GL_VERSION);

        _logger.Information("----------------------------------------------");
        _logger.Information("OpenGL Info:");
        _logger.Information($"| Vendor: {vendor}");
        _logger.Information($"| Renderer: {renderer}");
        _logger.Information($"| Version: {version}");
        _logger.Information("----------------------------------------------");
    }
}
