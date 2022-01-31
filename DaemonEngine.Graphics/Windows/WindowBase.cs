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

    public int Width { get { return WindowOptions.Width; } }

    public int Height { get { return WindowOptions.Height; } }

    public float AspectRatio
    {
        get
        {
            return (float)Width / (float)Height;
        }
    }

    public abstract void Initialize();
    public abstract void Shutdown();

    public abstract void SetEventCallback(EventCallbackFn eventCallbackFn);

    public abstract double GetTime();

    public abstract void Update();

    public abstract object GetNativeWindowHandle();
}
