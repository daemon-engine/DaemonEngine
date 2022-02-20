using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.Mathematics;
using DaemonEngine.Physics.Worlds;

namespace DaemonEngine.Physics;

public interface IPhysics
{
    bool ShowColliders { get; set; }

    void Step();
    void RenderColliders(IEnumerable<PhysicsBody> physicsBodies);
    PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions);
    object GetBodyReference(PhysicsBody body);
}

internal sealed class Physics : IPhysics
{
    private IPipeline? _pipeline;
    private IShader? _shader;

    public Physics(IWorld world, IRenderer renderer, IGraphicsFactory graphicsFactory)
    {
        World = world;
        Renderer = renderer;

        SetupDebugPipeline(graphicsFactory);

#if DEBUG
        ShowColliders = true;
#else
        ShowColliders = false;
#endif
    }

    public bool ShowColliders { get; set; }
    private IWorld World { get; }
    private IRenderer Renderer { get; }

    public void Step()
    {
        World.Step();
    }

    public void RenderColliders(IEnumerable<PhysicsBody> physicsBodies)
    {
        foreach (var physicsBody in physicsBodies)
        {
            if(physicsBody.ColliderShape.Mesh == null)
            {
                continue;
            }

            var model = Matrix4.Identity * Matrix4.Rotate(Vector3.ToEulerAngles(physicsBody.Rotation)) * Matrix4.Translate(physicsBody.Position);

            _shader!.Bind();
            _shader!.SetMat4("_Model", model);

            Renderer.RenderMesh(physicsBody.ColliderShape.Mesh);
        }
    }

    public PhysicsBody CreateBody(PhysicsBodyOptions physicsBodyOptions)
    {
        return World.CreateBody(physicsBodyOptions, _pipeline!);
    }

    public object GetBodyReference(PhysicsBody body)
    {
        return World.GetBodyReference(body);
    }

    private void SetupDebugPipeline(IGraphicsFactory graphicsFactory)
    {
        _shader = graphicsFactory.CreateShader("Assets/Shaders/FlatColorLine.shader");
        var bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("COLOR", ShaderDataType.Float3),
        });

        var options = new PipelineOptions
        {
            Shader = _shader,
            BufferLayout = bufferLayout,
            PrimitiveTopology = Graphics.Renderer.Enums.PrimitiveTopology.Lines
        };
        _pipeline = graphicsFactory.CreatePipeline(options);
    }
}
