namespace DaemonEngine.Windows;

public interface IWindow
{
    void Initialize();
    void Shutdown();

    bool IsRunning();
    double GetTime();

    void Update();
}
