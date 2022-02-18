using BepuPhysics;
using BepuPhysics.Collidables;
using DaemonEngine.Graphics;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.Physics.Shapes;
using DaemonEngine.Physics.Shapes.Factories;

namespace DaemonEngine.Physics.Bepuphysics2.Shape.Factories;

public interface IBepuphysics2ColliderShapeFactory : IColliderShapeFactory
{
    IColliderShape CreateColliderShape(PhysicsBody physicsBody, ref Simulation simulation);
}

internal class Bepuphysics2ColliderShapeFactory : IBepuphysics2ColliderShapeFactory
{
    private IPipeline _pipeline;

    public Bepuphysics2ColliderShapeFactory(IPrimitiveGeometric primitiveGeometric, IGraphicsFactory graphicsFactory)
    {
        PrimitiveGeometric = primitiveGeometric;

        var shader = graphicsFactory.CreateShader("Assets/Shaders/FlatColorLine.shader");
        var bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("COLOR", ShaderDataType.Float3),
        });

        var options = new PipelineOptions
        {
            Shader = shader,
            BufferLayout = bufferLayout,
            PrimitiveTopology = Graphics.Renderer.Enums.PrimitiveTopology.Lines
        };
        _pipeline = graphicsFactory.CreatePipeline(options);
    }

    protected IPrimitiveGeometric PrimitiveGeometric { get; }

    public IColliderShape CreateColliderShape(PhysicsBody physicsBody, ref Simulation simulation)
    {
        var size = physicsBody.ColliderSize *= 2;

        var mesh = physicsBody.PhysicsBodyShape switch
        {
            PhysicsBodyShape.Box => PrimitiveGeometric.CreateCube(size, _pipeline),
            _ => PrimitiveGeometric.CreateCube(size, _pipeline),
        };

        BodyActivityDescription bodyActivityDescription;
        CollidableDescription collidableDescription;
        BodyInertia bodyInertia;
        switch (physicsBody.PhysicsBodyShape)
        {
            case PhysicsBodyShape.Box:
            default: CreateBoxShape(physicsBody, simulation, size, out bodyActivityDescription, out collidableDescription, out bodyInertia); break;
        }

        return new Bepuphysics2ColliderShapeBase(mesh, collidableDescription, bodyActivityDescription, bodyInertia);
    }

    private static void CreateBoxShape(PhysicsBody physicsBody, Simulation simulation, Mathematics.Vector3 size, out BodyActivityDescription bodyActivityDescription, out CollidableDescription collidableDescription, out BodyInertia bodyInertia)
    {
        var shape = new Box(size.X, size.Y, size.Z);

        shape.ComputeInertia(physicsBody.Mass, out bodyInertia);

        var shapeId = simulation.Shapes.Add(shape);
        collidableDescription = new CollidableDescription(shapeId, 0.01f);

        bodyActivityDescription = BodyDescription.GetDefaultActivity<Box>(shape);
    }
}
