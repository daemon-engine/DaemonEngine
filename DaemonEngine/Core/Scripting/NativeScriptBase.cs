using DaemonEngine.ECS;

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

    public virtual void Stop()
    {
    }
}
