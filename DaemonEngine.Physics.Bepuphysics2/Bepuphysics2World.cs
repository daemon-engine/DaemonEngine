using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using BepuUtilities.Memory;
using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Mathematics;
using DaemonEngine.Physics.Bepuphysics2.Callbacks;
using DaemonEngine.Physics.Worlds;
using Serilog;

namespace DaemonEngine.Physics.Bepuphysics2;

internal sealed class Bepuphysics2World : WorldBase
{
    public Bepuphysics2World(ILogger logger, ITimestepper timestepper, IThreadDispatcher threadDispatcher) 
        : base(logger)
    {
        Timestepper = timestepper;
        ThreadDispatcher = threadDispatcher;

        BufferPool = new BufferPool();

        var narrowPhaseCallback = new DefaultNarrowPhaseCallback();
        var poseIntegratorCallback = new DefaultPoseIntegratorCallback(new System.Numerics.Vector3(0.0f, -9.81f, 0.0f));

        Simulation = Simulation.Create(BufferPool, narrowPhaseCallback, poseIntegratorCallback, Timestepper);
    }

    protected ITimestepper Timestepper { get; }
    protected IThreadDispatcher ThreadDispatcher { get; }
    protected BufferPool BufferPool { get; }
    protected Simulation Simulation { get; }

    public override PhysicsBody CreateBody(PhysicsBodyType bodyType)
    {
        var resultBody = new PhysicsBody(bodyType);

        switch (resultBody.PhysicsBodyType)
        {
            case PhysicsBodyType.Dynamic:   CreateDynamicBody(ref resultBody); break;
            case PhysicsBodyType.Kinematic: CreateKinematicBody(ref resultBody); break;
            case PhysicsBodyType.Static:
            default: CreateStaticBody(ref resultBody); break;
        }

        return resultBody;
    }

    private void CreateDynamicBody(ref PhysicsBody physicsBody, float mass = 1.0f)
    {
        var position = new Vector3(0.0f, 0.0f, 0.0f);

        var cubeShape = new Box(1.0f, 1.0f, 1.0f);
        cubeShape.ComputeInertia(mass, out var bodyInertia);
        var collidableDescription = new CollidableDescription(Simulation.Shapes.Add(cubeShape), 0.01f);
        var bodyActivityDescription = BodyDescription.GetDefaultActivity<Box>(cubeShape);
        var _cubeBody = BodyDescription.CreateDynamic(position, bodyInertia, collidableDescription, bodyActivityDescription);

        var bodyHandle = Simulation.Bodies.Add(_cubeBody);

        physicsBody.Position = _cubeBody.Pose.Position;
        physicsBody.BodyHandle = bodyHandle;
    }
    
    private void CreateKinematicBody(ref PhysicsBody physicsBody)
    {
        var position = new Vector3(0.0f, 0.0f, 0.0f);

        var cubeShape = new Box(1.0f, 1.0f, 1.0f);
        var collidableDescription = new CollidableDescription(Simulation.Shapes.Add(cubeShape), 0.01f);
        var bodyActivityDescription = BodyDescription.GetDefaultActivity<Box>(cubeShape);

        var bodyDescription = BodyDescription.CreateKinematic(position, collidableDescription, bodyActivityDescription);

        physicsBody.Position = bodyDescription.Pose.Position;
        physicsBody.BodyHandle = bodyDescription;
    }

    private void CreateStaticBody(ref PhysicsBody physicsBody)
    {
        var planeShape = new Box(10.0f, 1.0f, 10.0f);
        var planeCollidableDescription = new CollidableDescription(Simulation.Shapes.Add(planeShape), 0.1f);
        var _planeBody = new StaticDescription(new Vector3(0.0f, 0.0f, 0.0f), planeCollidableDescription);

        Simulation.Statics.Add(_planeBody);

        physicsBody.Position = _planeBody.Pose.Position;
        physicsBody.BodyHandle = null;
    }

    public override object GetBodyReference(PhysicsBody physicsBody)
    {
        Throw.IfNull(physicsBody, nameof(physicsBody));
        Throw.IfNull(physicsBody.BodyHandle, nameof(physicsBody.BodyHandle));

        var bodyRef = Simulation.Bodies.GetBodyReference((BodyHandle)physicsBody.BodyHandle!);

        return bodyRef;
    }

    public override void Step()
    {
        Simulation.Timestep(1.0f / 60.0f, ThreadDispatcher);
    }
}
