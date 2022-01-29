namespace DaemonEngine;

public interface IApplication
{
    void Run();

    void OnStart();
    void OnShutdown();
    void OnUpdate(float deltaTime);
}
