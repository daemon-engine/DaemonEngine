using DaemonEngine.Mathematics;

namespace DaemonEngine.Physics;

public class PhysicsBodyOptions
{
    public Vector3 Position { get; set; }
    public Vector3 Scale { get; set; }

    public PhysicsBodyShape Shape { get; set; }
    public PhysicsBodyType BodyType { get; set; }
}
