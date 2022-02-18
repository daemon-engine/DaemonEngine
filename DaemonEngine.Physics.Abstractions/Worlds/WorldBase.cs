using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Physics.Shapes;
using Serilog;

namespace DaemonEngine.Physics.Worlds;

public abstract class WorldBase : IWorld
{
    public WorldBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions, IPipeline pipeline);

    public abstract object GetBodyReference(PhysicsBody physicsBody);

    public abstract void Step();
}
