using DaemonEngine.Mathematics;
using DaemonEngine.Physics;

namespace DaemonEngine.ECS.Components;

public enum RigidbodyType : byte
{
    Static,
    Dynamic,
    Kinematic
}

public class Rigidbody : ComponentBase
{
    public Rigidbody()
    {
    }

    public override string Name => nameof(Rigidbody);

    public RigidbodyType Type { get; set; } = RigidbodyType.Static;
    public float Mass { get; set; } = 1.0f;

    public PhysicsBody PhysicsBody { get; set; }

    // Debug
    public Vector3 LinearVelocity { get; set; }
    public Vector3 AngularVelocity { get; set; }
}
