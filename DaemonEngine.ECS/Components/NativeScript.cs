using DaemonEngine.Scripting;

namespace DaemonEngine.ECS.Components;

public class NativeScript : ComponentBase
{
    public NativeScript()
    {
    }

    public override string Name => $"{nameof(NativeScript)} ({Script.GetType().Name})";

    public INativeScript? Script { get; set; }
}
