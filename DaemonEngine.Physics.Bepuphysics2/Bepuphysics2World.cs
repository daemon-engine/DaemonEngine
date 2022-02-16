﻿using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using BepuUtilities.Memory;
using DaemonEngine.Extensions.Runtime;
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
        var poseIntegratorCallback = new DefaultPoseIntegratorCallback(new System.Numerics.Vector3(0.0f, -1.0f, 0.0f));
        //var poseIntegratorCallback = new DefaultPoseIntegratorCallback(new System.Numerics.Vector3(0.0f, -9.81f, 0.0f));

        Simulation = Simulation.Create(BufferPool, narrowPhaseCallback, poseIntegratorCallback, Timestepper);
    }

    protected ITimestepper Timestepper { get; }
    protected IThreadDispatcher ThreadDispatcher { get; }
    protected BufferPool BufferPool { get; }
    protected Simulation Simulation { get; }

    public override PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions)
    {
        var resultBody = new PhysicsBody(physicsBodyOptions);

        switch (resultBody.PhysicsBodyType)
        {
            case PhysicsBodyType.Dynamic: CreateDynamicBody(ref resultBody); break;
            case PhysicsBodyType.Kinematic: CreateKinematicBody(ref resultBody); break;
            case PhysicsBodyType.Static:
            default: CreateStaticBody(ref resultBody); break;
        }

        return resultBody;
    }

    private void CreateDynamicBody(ref PhysicsBody physicsBody, float mass = 1.0f)
    {
        var shape = new Box(physicsBody.ColliderSize.X * 2.0f, physicsBody.ColliderSize.Y * 2.0f, physicsBody.ColliderSize.Z * 2.0f);
        var collidableDescription = new CollidableDescription(Simulation.Shapes.Add(shape), 0.01f);

        var bodyActivityDescription = BodyDescription.GetDefaultActivity<Box>(shape);

        shape.ComputeInertia(mass, out var bodyInertia);
        var bodyDescription = BodyDescription.CreateDynamic(physicsBody.Position, bodyInertia, collidableDescription, bodyActivityDescription);

        var bodyHandle = Simulation.Bodies.Add(bodyDescription);

        physicsBody.BodyHandle = bodyHandle;
    }
    
    private void CreateKinematicBody(ref PhysicsBody physicsBody)
    {
        var shape = new Box(physicsBody.ColliderSize.X * 2.0f, physicsBody.ColliderSize.Y * 2.0f, physicsBody.ColliderSize.Z * 2.0f);
        var collidableDescription = new CollidableDescription(Simulation.Shapes.Add(shape), 0.01f);

        var bodyActivityDescription = BodyDescription.GetDefaultActivity<Box>(shape);

        var bodyDescription = BodyDescription.CreateKinematic(physicsBody.Position, collidableDescription, bodyActivityDescription);

        physicsBody.BodyHandle = bodyDescription;
    }

    private void CreateStaticBody(ref PhysicsBody physicsBody)
    {
        var shape = new Box(physicsBody.ColliderSize.X * 2.0f, physicsBody.ColliderSize.Y * 2.0f, physicsBody.ColliderSize.Z * 2.0f);
        var collidableDescription = new CollidableDescription(Simulation.Shapes.Add(shape), 0.1f);

        var bodyDescription = new StaticDescription(physicsBody.Position, collidableDescription);

        Simulation.Statics.Add(bodyDescription);

        physicsBody.Position = bodyDescription.Pose.Position;
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
