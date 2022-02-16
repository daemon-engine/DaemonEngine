using DaemonEngine.Mathematics;

namespace DaemonEngine.Physics;

public class PhysicsBody
{
    public PhysicsBody(PhysicsBodyOptions physicsBodyOptions)
    {
        PhysicsBodyType = physicsBodyOptions.BodyType;
        Position = physicsBodyOptions.Position;
        EulerAngles = Vector3.Zero;
        ColliderSize = physicsBodyOptions.ColliderSize;
    }

    public Vector3 Position { get; set; }
    public Vector3 EulerAngles { get; set; }

    public Vector3 ColliderSize { get; set; }

    public PhysicsBodyType PhysicsBodyType { get; set; }

    public object? BodyHandle { get; set; }
}
