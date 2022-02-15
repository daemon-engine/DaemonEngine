using DaemonEngine.Mathematics;
using Serilog;

namespace DaemonEngine.Physics.Worlds;

public abstract class WorldBase : IWorld
{
    public WorldBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract void AddStatic(Vector3 position);
    public abstract object AddDynamic(Vector3 position, float mass);
    public abstract object GetBodyReference(object bodyHandle);

    public abstract void Step();
}
