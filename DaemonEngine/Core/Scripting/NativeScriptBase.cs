using DaemonEngine.ECS;
using DaemonEngine.EventSystem;

namespace DaemonEngine.Scripting;

public abstract class NativeScriptBase : INativeScript
{
    protected IEntity Entity { get; private set; }

    public void SetEntity(IEntity entity)
    {
        Entity = entity;
    }

    public abstract void Start();
    public abstract void Update(float deltaTime);
    public virtual void OnEvent(IEvent e)
    {
    }

    public virtual void Stop()
    {
    }
}
