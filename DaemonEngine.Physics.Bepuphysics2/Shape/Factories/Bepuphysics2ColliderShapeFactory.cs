using BepuPhysics;
using BepuPhysics.Collidables;
using DaemonEngine.Graphics;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Physics.Shapes;
using DaemonEngine.Physics.Shapes.Factories;
using System.Runtime.InteropServices;

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
            //PhysicsBodyShape.Mesh => PrimitiveGeometric.CreateMesh(physicsBody.MeshData.Vertices, physicsBody.MeshData.Indices, pipeline),
            PhysicsBodyShape.Sphere => PrimitiveGeometric.CreateSphere(radius, pipeline),
            PhysicsBodyShape.Box => PrimitiveGeometric.CreateCube(size, pipeline),
            _ => PrimitiveGeometric.CreateCube(size, pipeline),
        };

        BodyActivityDescription bodyActivityDescription = new(0.01f);
        CollidableDescription collidableDescription;
        BodyInertia bodyInertia = new();
        switch (physicsBody.PhysicsBodyShape)
        {
            case PhysicsBodyShape.Mesh: CreateMeshShape(physicsBody, simulation, out collidableDescription); break;
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
        var triangles = new Triangle[physicsBody.MeshData.Vertices.Length / 8];
        int triIndex = 0;
        for (int i = 0; i < physicsBody.MeshData.Vertices.Length / 8; i += 24)
        {
            var pos0 = physicsBody.MeshData.Vertices[i + 0];
            var pos1 = physicsBody.MeshData.Vertices[i + 1];
            var pos2 = physicsBody.MeshData.Vertices[i + 2];

            var pos3 = physicsBody.MeshData.Vertices[i + 8];
            var pos4 = physicsBody.MeshData.Vertices[i + 9];
            var pos5 = physicsBody.MeshData.Vertices[i + 10];

            var pos6 = physicsBody.MeshData.Vertices[i + 16];
            var pos7 = physicsBody.MeshData.Vertices[i + 17];
            var pos8 = physicsBody.MeshData.Vertices[i + 18];

            var vertex0 = new System.Numerics.Vector3(pos0, pos1, pos2);
            var vertex1 = new System.Numerics.Vector3(pos3, pos4, pos5);
            var vertex2 = new System.Numerics.Vector3(pos6, pos7, pos8);

            triangles[triIndex++] = new Triangle(vertex0, vertex1, vertex2);
        }

        unsafe
        {
            var mem = MemoryMarshal.CreateFromPinnedArray(triangles, 0, triangles.Length);

            BepuUtilities.Memory.Buffer<Triangle> buffer = new(mem.Pin().Pointer, triangles.Length, 1);

            var shape = new BepuPhysics.Collidables.Mesh(buffer, physicsBody.MeshData.Size!, new BepuUtilities.Memory.BufferPool());

            var shapeId = simulation.Shapes.Add(shape);
            collidableDescription = new CollidableDescription(shapeId, 0.01f);
        }
    }
}
