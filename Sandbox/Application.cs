using DaemonEngine;
using DaemonEngine.Windows;
using Serilog;

// Temp
using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;

namespace Sandbox;

public class Application : ApplicationBase
{
    public Application(ILogger logger, IWindow window) 
        : base(logger, window)
    {
    }

    public override void OnStart()
    {
        Logger.Information("OnStart");
    }

    public override void OnShutdown()
    {
        Logger.Information("OnShutdown");
    }

    public override void OnUpdate(float deltaTime)
    {
        GL.ClearColor(0.8f, 0.5f, 0.3f, 1.0f);
        GL.Clear(GLClearMask.ColorBufferBit);
    }
}
