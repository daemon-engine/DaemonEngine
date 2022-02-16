using DaemonEngine.Scripting;

namespace DaemonEngine.ECS.Components;

public class NativeScript : ComponentBase
{
    public NativeScript()
    {
    }

    public override string Name => $"{nameof(NativeScript)} ({Script!.GetType().Name})";

    public string ScriptName => Script!.GetType().Name;

    public INativeScript? Script { get; private set; }

    public TNativeScript AttachScript<TNativeScript>(IEntity entity)
        where TNativeScript : class, INativeScript
    {
        var nativeScript = Activator.CreateInstance<TNativeScript>();

        Script = nativeScript;
        Script.SetEntity(entity);

        return nativeScript;
    }
}
