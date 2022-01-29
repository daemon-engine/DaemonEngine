namespace DaemonEngine.Windows;

public interface IWindow
{
    bool IsRunning();

    void Initialize();
    void Shutdown();

    void Update();
}
