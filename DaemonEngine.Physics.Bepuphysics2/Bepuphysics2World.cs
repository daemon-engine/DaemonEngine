﻿using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using BepuUtilities.Memory;
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

    public override void AddStatic(Vector3 position)
    {
        var planeShape = new Box(10.0f, 1.0f, 10.0f);
        var planeCollidableDescription = new CollidableDescription(Simulation.Shapes.Add(planeShape), 0.1f);
        var _planeBody = new StaticDescription(position, planeCollidableDescription);
        Simulation.Statics.Add(_planeBody);
    }

    public override object AddDynamic(Vector3 position, float mass)
    {
        var cubeShape = new Box(1.0f, 1.0f, 1.0f);
        cubeShape.ComputeInertia(mass, out var bodyInertia);
        var collidableDescription = new CollidableDescription(Simulation.Shapes.Add(cubeShape), 0.01f);
        var bodyActivityDescription = BodyDescription.GetDefaultActivity<Box>(cubeShape);
        var _cubeBody = BodyDescription.CreateDynamic(position, bodyInertia, collidableDescription, bodyActivityDescription);
        var _cubeBodyHandle = Simulation.Bodies.Add(_cubeBody);

        return _cubeBodyHandle;
    }

    public override object GetBodyReference(object bodyHandle)
    {
        var bodyRef = Simulation.Bodies.GetBodyReference((BodyHandle)bodyHandle);
        return bodyRef;
    }

    public override void Step()
    {
        Simulation.Timestep(1.0f / 60.0f, ThreadDispatcher);
    }
}
