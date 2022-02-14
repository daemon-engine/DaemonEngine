using BepuUtilities;
using BepuUtilities.Memory;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.CollisionDetection;
using BepuPhysics.Constraints;
using System;
using System.Runtime.CompilerServices;
using System.Numerics;
namespace DaemonEngine.Extensions.bepuphysics2;

internal struct TestPoseIntegratorCallbacks : IPoseIntegratorCallbacks
{
    private readonly float _linearDamping;
    private readonly float _angularDamping;

    public TestPoseIntegratorCallbacks(Vector3 gravity, float linearDamping = 0.03f, float angularDamping = 0.03f)
    {
        Gravity = gravity;
        _linearDamping = linearDamping;
        _angularDamping = angularDamping;
    }

    public Vector3 Gravity { get; set; }
    public AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving;

    public void Initialize(Simulation simulation)
    {
    }

    public void PrepareForIntegration(float dt)
    {
    }

    public void IntegrateVelocity(int bodyIndex, in RigidPose pose, in BodyInertia localInertia, int workerIndex, ref BodyVelocity velocity)
    {
        velocity.Linear = (velocity.Linear + new Vector3(0.0f, -9.81f, 0.0f));
    }
}

internal struct TestNarrowPhaseCallbacks : INarrowPhaseCallbacks
{
    public TestNarrowPhaseCallbacks()
    {
        ContactSpringiness = new SpringSettings(30.0f, 1.0f);
        MaximumRecoveryVelocity = 2.0f;
        FrictionCoefficient = 1.0f;
    }

    public SpringSettings ContactSpringiness { get; set; }
    public float MaximumRecoveryVelocity { get; set; }
    public float FrictionCoefficient { get; set; }

    public void Initialize(Simulation simulation)
    {
        //Use a default if the springiness value wasn't initialized... at least until struct field initializers are supported outside of previews.
        if (ContactSpringiness.AngularFrequency == 0 && ContactSpringiness.TwiceDampingRatio == 0)
        {
            ContactSpringiness = new(30, 1);
            MaximumRecoveryVelocity = 2f;
            FrictionCoefficient = 1f;
        }
    }

    public bool AllowContactGeneration(int workerIndex, CollidableReference a, CollidableReference b)
    {
        return a.Mobility == CollidableMobility.Dynamic || b.Mobility == CollidableMobility.Dynamic;
    }

    public bool AllowContactGeneration(int workerIndex, CollidablePair pair, int childIndexA, int childIndexB)
    {
        return true;
    }

    public bool ConfigureContactManifold<TManifold>(int workerIndex, CollidablePair pair, ref TManifold manifold, out PairMaterialProperties pairMaterial) where TManifold : struct, IContactManifold<TManifold>
    {
        pairMaterial.FrictionCoefficient = FrictionCoefficient;
        pairMaterial.MaximumRecoveryVelocity = MaximumRecoveryVelocity;
        pairMaterial.SpringSettings = ContactSpringiness;
        return true;
    }

    public bool ConfigureContactManifold(int workerIndex, CollidablePair pair, int childIndexA, int childIndexB, ref ConvexContactManifold manifold)
    {
        return true;
    }

    public void Dispose()
    {
    }
}

internal class DefaultTimestepper : ITimestepper
{
    /// <summary>
    /// Fires after the sleeper completes and before bodies are integrated.
    /// </summary>
    public event TimestepperStageHandler Slept;
    /// <summary>
    /// Fires after bodies have their bounding boxes updated for the frame's predicted motion and before collision detection.
    /// </summary>
    public event TimestepperStageHandler BeforeCollisionDetection;
    /// <summary>
    /// Fires after all collisions have been identified, but before the substep loop begins.
    /// </summary>
    public event TimestepperStageHandler CollisionsDetected;
    /// <summary>
    /// Fires after the solver executes and before the final integration step.
    /// </summary>
    public event TimestepperStageHandler ConstraintsSolved;

    public void Timestep(Simulation simulation, float dt, IThreadDispatcher threadDispatcher = null)
    {
        simulation.Sleep(threadDispatcher);
        Slept?.Invoke(dt, threadDispatcher);

        simulation.PredictBoundingBoxes(dt, threadDispatcher);
        BeforeCollisionDetection?.Invoke(dt, threadDispatcher);

        simulation.CollisionDetection(dt, threadDispatcher);
        CollisionsDetected?.Invoke(dt, threadDispatcher);

        simulation.Solve(dt, threadDispatcher);
        ConstraintsSolved?.Invoke(dt, threadDispatcher);

        simulation.IncrementallyOptimizeDataStructures(threadDispatcher);
    }
}

public class PhysicsTest
{
    public PhysicsTest()
    {
        var bufferPool = new BufferPool();
        Simulation = Simulation.Create(bufferPool, new TestNarrowPhaseCallbacks(), new TestPoseIntegratorCallbacks(), new DefaultTimestepper());
    }

    public Simulation Simulation { get; }

    public void Update()
    {
        const float timeToSimulate = 1 / 60f;
        Simulation.Timestep(timeToSimulate);
    }
}
