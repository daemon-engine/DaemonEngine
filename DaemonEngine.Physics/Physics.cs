using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Physics.Worlds;

namespace DaemonEngine.Physics;

public interface IPhysics
{
    bool ShowColliders { get; set; }

    void Step();
    void RenderColliders();
    PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions);
    object GetBodyReference(PhysicsBody body);
}

internal sealed class Physics : IPhysics
{
    private List<PhysicsBody> _physicsBodies;

    public Physics(IWorld world, IRenderer renderer)
    {
        _physicsBodies = new List<PhysicsBody>();

        World = world;
        Renderer = renderer;
    }

    public bool ShowColliders { get; set; }

    protected IWorld World { get; }
    private IRenderer Renderer { get; }

    public void Step()
    {
        World.Step();
    }

    public void RenderColliders()
    {
        foreach (var physicsBody in _physicsBodies)
        {
            Renderer.RenderMesh(physicsBody.ColliderShape.Mesh);
        }
    }

    public PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions)
    {
        var physicsBody = World.CreateBody(physicsBodyOptions);

        _physicsBodies.Add(physicsBody);

        return physicsBody;
    }

    public object GetBodyReference(PhysicsBody body)
    {
        return World.GetBodyReference(body);
    }
}
