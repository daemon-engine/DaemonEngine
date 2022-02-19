using BepuPhysics;
using BepuPhysics.Collidables;
using DaemonEngine.Graphics;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Physics.Shapes;
using DaemonEngine.Physics.Shapes.Factories;

namespace DaemonEngine.Physics.Bepuphysics2.Shape.Factories;

public interface IBepuphysics2ColliderShapeFactory : IColliderShapeFactory
{
    IColliderShape CreateColliderShape(PhysicsBody physicsBody, ref Simulation simulation, IPipeline pipeline);
}

internal class Bepuphysics2ColliderShapeFactory : IBepuphysics2ColliderShapeFactory
{
    public Bepuphysics2ColliderShapeFactory(IPrimitiveGeometric primitiveGeometric)
    {
        PrimitiveGeometric = primitiveGeometric;
    }

    protected IPrimitiveGeometric PrimitiveGeometric { get; }

    public IColliderShape CreateColliderShape(PhysicsBody physicsBody, ref Simulation simulation, IPipeline pipeline)
    {
        var size = physicsBody.ColliderSize * 2;
        var radius = physicsBody.SphereRadius * 2;

        var mesh = physicsBody.PhysicsBodyShape switch
        {
            PhysicsBodyShape.Mesh => null,
            PhysicsBodyShape.Sphere => PrimitiveGeometric.CreateSphere(radius, pipeline),
            PhysicsBodyShape.Box => PrimitiveGeometric.CreateCube(size, pipeline),
            _ => PrimitiveGeometric.CreateCube(size, pipeline),
        };

        BodyActivityDescription bodyActivityDescription;
        CollidableDescription collidableDescription;
        BodyInertia bodyInertia;
        switch (physicsBody.PhysicsBodyShape)
        {
            //case PhysicsBodyShape.Mesh: CreateMeshShape(physicsBody, simulation, out collidableDescription); break;
            case PhysicsBodyShape.Sphere: CreateSphereShape(physicsBody, simulation, physicsBody.SphereRadius, out bodyActivityDescription, out collidableDescription, out bodyInertia); break;
            case PhysicsBodyShape.Box:
            default: CreateBoxShape(physicsBody, simulation, size, out bodyActivityDescription, out collidableDescription, out bodyInertia); break;
        }

        return new Bepuphysics2ColliderShapeBase(mesh!, collidableDescription, bodyActivityDescription, bodyInertia);
    }

    private static void CreateSphereShape(PhysicsBody physicsBody, Simulation simulation, float radius, out BodyActivityDescription bodyActivityDescription, out CollidableDescription collidableDescription, out BodyInertia bodyInertia)
    {
        var shape = new Sphere(radius);

        shape.ComputeInertia(physicsBody.Mass, out bodyInertia);

        var shapeId = simulation.Shapes.Add(shape);
        collidableDescription = new CollidableDescription(shapeId, 0.01f);

        bodyActivityDescription = BodyDescription.GetDefaultActivity<Sphere>(shape);
    }

    private static void CreateBoxShape(PhysicsBody physicsBody, Simulation simulation, Mathematics.Vector3 size, out BodyActivityDescription bodyActivityDescription, out CollidableDescription collidableDescription, out BodyInertia bodyInertia)
    {
        var shape = new Box(size.X, size.Y, size.Z);

        shape.ComputeInertia(physicsBody.Mass, out bodyInertia);

        var shapeId = simulation.Shapes.Add(shape);
        collidableDescription = new CollidableDescription(shapeId, 0.01f);

        bodyActivityDescription = BodyDescription.GetDefaultActivity<Box>(shape);
    }

    private static void CreateMeshShape(PhysicsBody physicsBody, Simulation simulation, out CollidableDescription collidableDescription)
    {
        //BepuUtilities.Memory.Buffer<Triangle> buffer = new BepuUtilities.Memory.Buffer<Triangle>();

        var shape = new BepuPhysics.Collidables.Mesh();

        var shapeId = simulation.Shapes.Add(shape);
        collidableDescription = new CollidableDescription(shapeId, 0.01f);
    }
}
