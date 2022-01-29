using Serilog;

namespace DaemonEngine;

public abstract class ApplicationBase : IApplication
{
    public ApplicationBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public void Run()
    {
        OnStart();

        //while(true)
        //{
        //    Update(0.0f);
        //}

        OnShutdown();
    }

    public abstract void OnStart();
    public abstract void OnShutdown();
    public abstract void OnUpdate(float deltaTime);
}
