using DaemonEngine.Physics.Worlds;

namespace DaemonEngine.Physics;

public interface IPhysics
{
    PhysicsBody CreateBody(PhysicsBodyType bodyType);

    object GetBodyReference(PhysicsBody body);

    void Step();
}

internal sealed class Physics : IPhysics
{
    public Physics(IWorld world)
    {
        World = world;
    }

    protected IWorld World { get; }

    public PhysicsBody CreateBody(PhysicsBodyType bodyType)
    {
        return World.CreateBody(bodyType);
    }

    public object GetBodyReference(PhysicsBody body)
    {
        return World.GetBodyReference(body);
    }

    public void Step()
    {
        World.Step();
    }
}
