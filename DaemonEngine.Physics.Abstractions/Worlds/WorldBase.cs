using Serilog;

namespace DaemonEngine.Physics.Worlds;

public abstract class WorldBase : IWorld
{
    public WorldBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract void AddStatic();
    public abstract object AddDynamic();
    public abstract object GetBodyReference(object bodyHandle);

    public abstract void Step();
}
