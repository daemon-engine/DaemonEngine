using DaemonEngine.Mathematics;

namespace DaemonEngine.ECS.Components;

public sealed class Transform : ComponentBase
{
    public Transform()
    {
    }

    public override string Name => nameof(Transform);

    public Vector3 Position { get; set; } = Vector3.Zero;
    public Vector3 Rotation { get; set; } = Vector3.Zero;
    public Vector3 Scale { get; set; } = Vector3.One;
}
