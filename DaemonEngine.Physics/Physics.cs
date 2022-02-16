using DaemonEngine.Physics.Worlds;

namespace DaemonEngine.Physics;

public interface IPhysics
{
    PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions);

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

    public PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions)
    {
        return World.CreateBody(physicsBodyOptions);
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
