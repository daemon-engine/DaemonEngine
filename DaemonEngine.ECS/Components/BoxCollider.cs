using DaemonEngine.Mathematics;
using DaemonEngine.Physics;

namespace DaemonEngine.ECS.Components;
public class BoxCollider : ColliderBase
{
    public BoxCollider()
    {
    }

    public override string Name => nameof(BoxCollider);
    public override PhysicsBodyShape Shape => PhysicsBodyShape.Box;

    public Vector3 Offset { get; set; } = Vector3.Zero;
    public Vector3 Size { get; set; } = Vector3.One;

}
