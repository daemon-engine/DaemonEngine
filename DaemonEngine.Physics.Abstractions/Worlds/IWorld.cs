using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Physics.Worlds;

public interface IWorld
{
    PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions, IPipeline pipeline);

    object GetBodyReference(PhysicsBody physicsBody);

    void Step();
}
