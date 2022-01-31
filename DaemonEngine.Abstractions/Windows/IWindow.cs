using DaemonEngine.EventSystem;

namespace DaemonEngine.Windows;

public delegate void EventCallbackFn(IEvent e);

public interface IWindow
{
    int Width { get; }
    int Height { get; }
    float AspectRatio { get; }

    void Initialize();
    void Shutdown();

    void SetEventCallback(EventCallbackFn eventCallbackFn);

    double GetTime();

    void Update();

    object GetNativeWindowHandle();
}
