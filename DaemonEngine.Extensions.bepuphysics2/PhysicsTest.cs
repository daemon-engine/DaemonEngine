using BepuUtilities;
using BepuUtilities.Memory;
using BepuPhysics;
using static DaemonEngine.Extensions.bepuphysics2.Callbacks;
using System.Numerics;

namespace DaemonEngine.Extensions.bepuphysics2;

internal class DefaultTimestepper : ITimestepper
{
    /// <summary>
    /// Fires after the sleeper completes and before bodies are integrated.
    /// </summary>
    public event TimestepperStageHandler Slept;
    /// <summary>
    /// Fires after bodies have had their velocities and bounding boxes updated, but before collision detection begins.
    /// </summary>
    public event TimestepperStageHandler BeforeCollisionDetection;
    /// <summary>
    /// Fires after all collisions have been identified, but before constraints are solved.
    /// </summary>
    public event TimestepperStageHandler CollisionsDetected;
    /// <summary>
    /// Fires after the solver executes and before body poses are integrated.
    /// </summary>
    public event TimestepperStageHandler ConstraintsSolved;
    /// <summary>
    /// Fires after bodies have their poses integrated and before data structures are incrementally optimized.
    /// </summary>
    public event TimestepperStageHandler PosesIntegrated;

    public void Timestep(Simulation simulation, float dt, IThreadDispatcher threadDispatcher = null)
    {
        simulation.Sleep(threadDispatcher);
        Slept?.Invoke(dt, threadDispatcher);

        simulation.IntegrateVelocitiesBoundsAndInertias(dt, threadDispatcher);
        BeforeCollisionDetection?.Invoke(dt, threadDispatcher);

        simulation.CollisionDetection(dt, threadDispatcher);
        CollisionsDetected?.Invoke(dt, threadDispatcher);

        simulation.Solve(dt, threadDispatcher);
        ConstraintsSolved?.Invoke(dt, threadDispatcher);

        simulation.IntegratePoses(dt, threadDispatcher);
        PosesIntegrated?.Invoke(dt, threadDispatcher);

        simulation.IncrementallyOptimizeDataStructures(threadDispatcher);
    }
}

public class PhysicsTest
{
    public PhysicsTest()
    {
        var gravity = new Vector3(0.0f, -10.0f, 0.0f);

        var bufferPool = new BufferPool();
        Simulation = Simulation.Create(bufferPool, new NarrowPhaseCallbacks(), new PoseIntegratorCallbacks(new Vector3(0, -10, 0)), new DefaultTimestepper());

        SimulationDispatcher = new SimpleThreadDispatcher(Environment.ProcessorCount / 4);
    }

    public Simulation Simulation { get; }
    protected IThreadDispatcher SimulationDispatcher { get; }

    public void Update()
    {
        Simulation.Timestep(1.0f / 60.0f, SimulationDispatcher);
    }
}
