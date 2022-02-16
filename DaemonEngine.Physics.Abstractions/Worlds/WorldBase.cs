using Serilog;

namespace DaemonEngine.Physics.Worlds;

public abstract class WorldBase : IWorld
{
    public WorldBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract PhysicsBody CreateBody(PhysicsBodyType bodyType);

    public abstract object GetBodyReference(PhysicsBody physicsBody);

    public abstract void Step();
}
