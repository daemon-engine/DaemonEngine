using BepuPhysics;
using BepuUtilities;
using BepuUtilities.Memory;
using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using DaemonEngine.Physics.Bepuphysics2.Callbacks;
using DaemonEngine.Physics.Bepuphysics2.Shape;
using DaemonEngine.Physics.Bepuphysics2.Shape.Factories;
using DaemonEngine.Physics.Worlds;
using Serilog;

namespace DaemonEngine.Physics.Bepuphysics2;

internal sealed class Bepuphysics2World : WorldBase
{
    private Simulation _simulation;

    public Bepuphysics2World(ILogger logger, ITimestepper timestepper, IThreadDispatcher threadDispatcher, IBepuphysics2ColliderShapeFactory colliderShapeFactory) 
        : base(logger)
    {
        Timestepper = timestepper;
        ThreadDispatcher = threadDispatcher;
        ColliderShapeFactory = colliderShapeFactory;

        BufferPool = new BufferPool();

        var narrowPhaseCallback = new DefaultNarrowPhaseCallback();
        var poseIntegratorCallback = new DefaultPoseIntegratorCallback(new System.Numerics.Vector3(0.0f, -9.81f, 0.0f));

        _simulation = Simulation.Create(BufferPool, narrowPhaseCallback, poseIntegratorCallback, Timestepper);
    }

    protected ITimestepper Timestepper { get; }
    protected IThreadDispatcher ThreadDispatcher { get; }
    protected IBepuphysics2ColliderShapeFactory ColliderShapeFactory { get; }
    protected BufferPool BufferPool { get; }

    public override PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions, IPipeline pipeline)
    {
        var resultBody = new PhysicsBody(physicsBodyOptions);

        resultBody.ColliderShape = ColliderShapeFactory.CreateColliderShape(resultBody, ref _simulation, pipeline);

        switch (resultBody.PhysicsBodyType)
        {
            case PhysicsBodyType.Dynamic: CreateDynamicBody(ref resultBody); break;
            case PhysicsBodyType.Kinematic: CreateKinematicBody(ref resultBody); break;
            case PhysicsBodyType.Static:
            default: CreateStaticBody(ref resultBody); break;
        }

        return resultBody;
    }

    private void CreateDynamicBody(ref PhysicsBody physicsBody)
    {
        var collider = (Bepuphysics2ColliderShapeBase)physicsBody.ColliderShape;

        var pose = new RigidPose(physicsBody.Position, physicsBody.Rotation);

        var bodyDescription = BodyDescription.CreateDynamic(pose, collider.BodyInertia, collider.CollidableDescription, collider.BodyActivityDescription);

        var bodyHandle = _simulation.Bodies.Add(bodyDescription);

        physicsBody.BodyHandle = bodyHandle;
    }
    
    private void CreateKinematicBody(ref PhysicsBody physicsBody)
    {
        var collider = (Bepuphysics2ColliderShapeBase)physicsBody.ColliderShape;

        var pose = new RigidPose(physicsBody.Position, physicsBody.Rotation);

        var bodyDescription = BodyDescription.CreateKinematic(pose, collider.CollidableDescription, collider.BodyActivityDescription);

        var bodyHandle = _simulation.Bodies.Add(bodyDescription);

        physicsBody.BodyHandle = bodyHandle;
    }

    private void CreateStaticBody(ref PhysicsBody physicsBody)
    {
        var collider = (Bepuphysics2ColliderShapeBase)physicsBody.ColliderShape;

        var rotation = physicsBody.Rotation;

        var bodyDescription = new StaticDescription(physicsBody.Position, rotation, collider.CollidableDescription);

        _simulation.Statics.Add(bodyDescription);

        physicsBody.Position = bodyDescription.Pose.Position;
        physicsBody.BodyHandle = null;
    }

    public override object GetBodyReference(PhysicsBody physicsBody)
    {
        Throw.IfNull(physicsBody, nameof(physicsBody));
        Throw.IfNull(physicsBody.BodyHandle, nameof(physicsBody.BodyHandle));

        var bodyRef = _simulation.Bodies.GetBodyReference((BodyHandle)physicsBody.BodyHandle!);

        return bodyRef;
    }

    public override void Step()
    {
        _simulation.Timestep(1.0f / 60.0f, ThreadDispatcher);
    }
}
