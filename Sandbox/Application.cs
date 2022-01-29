using DaemonEngine;
using Serilog;

namespace Sandbox;

public class Application : ApplicationBase
{
    public Application(ILogger logger) 
        : base(logger)
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
