using DaemonEngine.Mathematics;
using DaemonEngine.Physics;

namespace DaemonEngine.ECS.Components;

public class SphereCollider : ColliderBase
{
    public SphereCollider()
    {
    }

    public override string Name => nameof(SphereCollider);

    public override PhysicsBodyShape Shape => PhysicsBodyShape.Sphere;

    public Vector3 Offset { get; set; } = Vector3.Zero;
    public float Radius { get; set; } = 1.0f;
}
