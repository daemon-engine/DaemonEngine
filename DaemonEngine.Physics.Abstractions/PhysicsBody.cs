using DaemonEngine.Mathematics;
using DaemonEngine.Physics.Shapes;

namespace DaemonEngine.Physics;

public class PhysicsBody
{
    public PhysicsBody(PhysicsBodyOptions physicsBodyOptions)
    {
        PhysicsBodyType = physicsBodyOptions.BodyType;
        PhysicsBodyShape = physicsBodyOptions.Shape;
        Position = physicsBodyOptions.Position;
        EulerAngles = Vector3.Zero;
        Mass = physicsBodyOptions.Mass;
        ColliderSize = physicsBodyOptions.ColliderSize;
    }

    public IColliderShape ColliderShape { get; set; }

    public Vector3 Position { get; set; }
    public Vector3 EulerAngles { get; set; }
    public float Mass { get; set; } = 1.0f;

    public Vector3 ColliderSize { get; set; }

    public PhysicsBodyShape PhysicsBodyShape { get; set; }
    public PhysicsBodyType PhysicsBodyType { get; set; }

    public object? BodyHandle { get; set; }
}
