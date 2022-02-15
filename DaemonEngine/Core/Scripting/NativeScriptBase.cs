namespace DaemonEngine.Scripting;

public abstract class NativeScriptBase : INativeScript
{
    public abstract void Start();
    public abstract void Update(float deltaTime);

    public virtual void Stop()
    {
    }
}
