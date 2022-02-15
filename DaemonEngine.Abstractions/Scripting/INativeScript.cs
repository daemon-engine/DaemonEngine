using DaemonEngine.ECS;

namespace DaemonEngine.Scripting;

public interface INativeScript
{
    void SetEntity(IEntity entity);

    void Start();
    void Stop();
    void Update(float deltaTime);
}
