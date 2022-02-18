using DaemonEngine.Mathematics;

namespace DaemonEngine.Physics;

public class PhysicsBodyOptions
{
    public float Mass { get; set; }
    public Vector3 Position { get; set; }
    public Vector3 Rotation { get; set; }

    public Vector3 ColliderSize { get; set; }
    public float Radius { get; set; }

    public PhysicsBodyShape Shape { get; set; }
    public PhysicsBodyType BodyType { get; set; }
}
