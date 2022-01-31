namespace DaemonEngine.Windows;

public interface IWindow
{
    int Width { get; }
    int Height { get; }
    float AspectRatio { get; }

    void Initialize();
    void Shutdown();

    bool IsRunning();
    double GetTime();

    void Update();
}
