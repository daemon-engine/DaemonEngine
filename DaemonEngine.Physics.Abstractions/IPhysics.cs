using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Physics;

public interface IPhysics
{
    bool ShowColliders { get; set; }

    void Step();
    void RenderColliders();
    PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions);
    object GetBodyReference(PhysicsBody body);
}