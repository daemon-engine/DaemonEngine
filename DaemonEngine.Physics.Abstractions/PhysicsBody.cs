using DaemonEngine.Mathematics;
using DaemonEngine.Physics.Shapes;

namespace DaemonEngine.Physics;

public class MeshColliderData
{
    public float[] Vertices { get; set; }
    public uint[] Indices { get; set; }
    public Vector3 Size { get; set; } = Vector3.One;
}

public class PhysicsBody
{
    public PhysicsBody(PhysicsBodyOptions physicsBodyOptions)
    {
        PhysicsBodyType = physicsBodyOptions.BodyType;
        PhysicsBodyShape = physicsBodyOptions.Shape;
        Position = physicsBodyOptions.Position;
        Rotation = physicsBodyOptions.Rotation;
        Mass = physicsBodyOptions.Mass;
        ColliderSize = physicsBodyOptions.ColliderSize;
        SphereRadius = physicsBodyOptions.Radius;
        MeshData = physicsBodyOptions.MeshColliderData;
    }

    public IColliderShape ColliderShape { get; set; }

    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public float Mass { get; set; } = 1.0f;

    public Vector3 ColliderSize { get; set; }
    public float SphereRadius { get; set; }
    public MeshColliderData MeshData { get; set; }

    public PhysicsBodyShape PhysicsBodyShape { get; set; }
    public PhysicsBodyType PhysicsBodyType { get; set; }

    public object? BodyHandle { get; set; }
}
