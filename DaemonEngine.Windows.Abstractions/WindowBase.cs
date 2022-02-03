using DaemonEngine.Extensions.Runtime;
using Serilog;

namespace DaemonEngine.Windows;

public abstract class WindowBase : IWindow
{
    public WindowBase(ILogger logger, WindowOptions windowOptions)
    {
        Throw.IfNull(logger, nameof(logger));
        Throw.IfNull(windowOptions, nameof(windowOptions));

        Logger = logger;
        WindowOptions = windowOptions;
    }

    protected ILogger Logger { get; }
    protected WindowOptions WindowOptions { get; }
    protected EventCallbackFn EventCallback { get; private set; }

    public int Width { get { return WindowOptions.Width; } }

    public int Height { get { return WindowOptions.Height; } }

    public float AspectRatio
    {
        get
        {
            return (float)Width / (float)Height;
        }
    }

    public void SetEventCallback(EventCallbackFn eventCallbackFn)
    {
        EventCallback = eventCallbackFn;
    }

    public abstract void Initialize();
    public abstract void Shutdown();

    public abstract void Maximize();
    public abstract void Restore();
    public abstract double GetTime();

    public abstract void Update();

    public abstract object GetNativeWindowHandle();
}
