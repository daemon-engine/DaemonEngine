using DaemonEngine.Windows;
using Serilog;

namespace DaemonEngine.Graphics.Windows;

public abstract class WindowBase : IWindow
{
    public WindowBase(ILogger logger, WindowOptions windowOptions)
    {
        Logger = logger;
        WindowOptions = windowOptions;
    }

    protected ILogger Logger { get; }
    protected WindowOptions WindowOptions { get; }

    public abstract void Initialize();
    public abstract void Shutdown();

    public abstract bool IsRunning();
    public abstract double GetTime();

    public abstract void Update();
}
