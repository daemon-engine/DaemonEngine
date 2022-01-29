using DaemonEngine;
using DaemonEngine.Windows;
using Serilog;

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
    }
}
