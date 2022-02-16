namespace DaemonEngine.Physics.Worlds;

public interface IWorld
{
    PhysicsBody CreateBody(PhysicsBodyType bodyType);

    object GetBodyReference(PhysicsBody physicsBody);

    void Step();
}
