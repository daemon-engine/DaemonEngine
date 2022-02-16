namespace DaemonEngine.Physics.Worlds;

public interface IWorld
{
    PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions);

    object GetBodyReference(PhysicsBody physicsBody);

    void Step();
}
