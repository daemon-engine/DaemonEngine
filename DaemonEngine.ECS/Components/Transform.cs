using DaemonEngine.Mathematics;

namespace DaemonEngine.ECS.Components;

public sealed class Transform : ComponentBase
{
    public Transform()
    {
        Position = Vector3.Zero;
        Rotation = Vector3.Zero;
        Scale = Vector3.One;
    }

    public override string Name => nameof(Transform);

    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }
    public Vector3 Scale { get; set; }
}
