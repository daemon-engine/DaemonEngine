using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using DaemonEngine.Physics.Worlds;

namespace DaemonEngine.Physics;

internal sealed class Physics : IPhysics
{
    private List<PhysicsBody> _physicsBodies;

    public Physics(IWorld world, IRenderer renderer, IGraphicsFactory graphicsFactory)
    {
        _physicsBodies = new List<PhysicsBody>();

        World = world;
        Renderer = renderer;

#if DEBUG
        ShowColliders = true;
#else
        ShowColliders = false;
#endif
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
