using BepuPhysics;
using BepuUtilities;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace DaemonEngine.Physics.Bepuphysics2.Callbacks;

internal struct DefaultPoseIntegratorCallback : IPoseIntegratorCallbacks
{
    public Vector3 Gravity;
    public float LinearDamping;
    public float AngularDamping;

    Vector3 gravityDt;
    float _linearDampingDt;
    float _angularDampingDt;

    /// <summary>
    /// Performs any required initialization logic after the Simulation instance has been constructed.
    /// </summary>
    /// <param name="simulation">Simulation that owns these callbacks.</param>
    public void Initialize(Simulation simulation)
    {
        //In this demo, we don't need to initialize anything.
        //If you had a simulation with per body gravity stored in a CollidableProperty<T> or something similar, having the simulation provided in a callback can be helpful.
    }

    /// <summary>
    /// Gets how the pose integrator should handle angular velocity integration.
    /// </summary>
    public AngularIntegrationMode AngularIntegrationMode => AngularIntegrationMode.Nonconserving; //Don't care about fidelity in this demo!

    public DefaultPoseIntegratorCallback(Vector3 gravity, float linearDamping = 0.03f, float angularDamping = 0.03f)
        : this()
    {
        Gravity = gravity;
        LinearDamping = linearDamping;
        AngularDamping = angularDamping;
    }

    /// <summary>
    /// Called prior to integrating the simulation's active bodies. When used with a substepping timestepper, this could be called multiple times per frame with different time step values.
    /// </summary>
    /// <param name="dt">Current time step duration.</param>
    public void PrepareForIntegration(float dt)
    {
        //No reason to recalculate gravity * dt for every body; just cache it ahead of time.
        gravityDt = Gravity * dt;

        //Since these callbacks don't use per-body damping values, we can precalculate everything.
        _linearDampingDt = MathF.Pow(MathHelper.Clamp(1 - LinearDamping, 0, 1), dt);
        _angularDampingDt = MathF.Pow(MathHelper.Clamp(1 - AngularDamping, 0, 1), dt);
    }

    /// <summary>
    /// Callback called for each active body within the simulation during body integration.
    /// </summary>
    /// <param name="bodyIndex">Index of the body being visited.</param>
    /// <param name="pose">Body's current pose.</param>
    /// <param name="localInertia">Body's current local inertia.</param>
    /// <param name="workerIndex">Index of the worker thread processing this body.</param>
    /// <param name="velocity">Reference to the body's current velocity to integrate.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void IntegrateVelocity(int bodyIndex, in RigidPose pose, in BodyInertia localInertia, int workerIndex, ref BodyVelocity velocity)
    {
        //Note that we avoid accelerating kinematics. Kinematics are any body with an inverse mass of zero (so a mass of ~infinity). No force can move them.
        if (localInertia.InverseMass > 0)
        {
            velocity.Linear = (velocity.Linear + gravityDt) * _linearDampingDt;
            velocity.Angular *= _angularDampingDt;
        }
    }
}