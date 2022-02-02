using DaemonEngine.EventSystem;

namespace DaemonEngine.Core.Layer;

public interface ILayer
{
    void OnStart();
    void OnShutdown();
    void OnUpdate(float deltaTime);
    void OnEvent(IEvent e);
}
