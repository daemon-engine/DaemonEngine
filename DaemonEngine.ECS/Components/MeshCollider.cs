using DaemonEngine.Mathematics;
using DaemonEngine.Physics;

namespace DaemonEngine.ECS.Components;

public class MeshCollider : ColliderBase
{
    public MeshCollider()
    {
    }

    public override string Name => nameof(MeshCollider);
    public override PhysicsBodyShape Shape => PhysicsBodyShape.Mesh;

    public Vector3 Size { get; set; } = Vector3.One;
}
