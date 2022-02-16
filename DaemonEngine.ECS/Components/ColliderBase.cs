using DaemonEngine.Physics;

namespace DaemonEngine.ECS.Components;

public abstract class ColliderBase : ComponentBase
{
    public abstract override string Name { get; }
    public abstract PhysicsBodyShape Shape { get; }
}
