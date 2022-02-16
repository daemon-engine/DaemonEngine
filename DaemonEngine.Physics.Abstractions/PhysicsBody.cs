using DaemonEngine.Mathematics;

namespace DaemonEngine.Physics;

public class PhysicsBody
{
    public PhysicsBody(PhysicsBodyType bodyType)
    {
        PhysicsBodyType = bodyType;
        Position = Vector3.Zero;
        EulerAngles = Vector3.Zero;
    }

    public PhysicsBody(PhysicsBodyType bodyType, Vector3 position, Vector3 eulerAngles)
    {
        PhysicsBodyType = bodyType;
        Position = position;
        EulerAngles = eulerAngles;
    }

    public Vector3 Position { get; set; }
    public Vector3 EulerAngles { get; set; }
    public PhysicsBodyType PhysicsBodyType { get; set; }

    public object? BodyHandle { get; set; }
}
