using DaemonEngine.Scripting;

namespace DaemonEngine.ECS.Components;

public class NativeScript : ComponentBase
{
    public NativeScript()
    {
    }

    public INativeScript? Script { get; set; }
}
