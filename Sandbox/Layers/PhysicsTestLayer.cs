using DaemonEngine.Core.Layer;
using DaemonEngine.Core.Scene;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.Mathematics;
using DaemonEngine.Physics;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Entities;

namespace Sandbox.Layers;

internal class PhysicsTestLayer : LayerBase
{
    private IShader _shader;

    private Scene _scene;
    private SceneHierarchy _sceneHierarchy;

    private Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);

    private IShader _lineShader;
    private IPipeline _linePipeline;
    private IVertexBuffer _lineVertexBuffer;
    private IIndexBuffer _lineIndexBuffer;

    public PhysicsTestLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        _lineShader = GraphicsFactory.CreateShader("Assets/Shaders/FlatColorLine.shader");

        var bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("COLOR", ShaderDataType.Float3)
        });

        var pipelineOptions = new PipelineOptions
        {
            BufferLayout = bufferLayout,
            Shader = _lineShader,
            PrimitiveTopology = PrimitiveTopology.Lines
        };
        _linePipeline = GraphicsFactory.CreatePipeline(pipelineOptions);

        var lineVertices = new float[8 * (3 + 3)]
        {
            0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, // 0
            0.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, // 1
            1.0f, 1.0f, 0.0f, 0.0f, 1.0f, 0.0f, // 2
            1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, // 3

            0.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f, // 4
            0.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, // 5
            1.0f, 1.0f, 1.0f, 0.0f, 1.0f, 0.0f, // 6
            1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 0.0f  // 7
        };

        _lineVertexBuffer = GraphicsFactory.CreateVertexBuffer(lineVertices.Length * sizeof(float), lineVertices);

        var lineIndices = new uint[24]
        {
            0, 1, // line 1
            1, 2, // line 2
            2, 3, // line 3
            3, 0, // line 4

            4, 5, // line 5
            5, 6, // line 6
            6, 7, // line 7
            7, 4, // line 8

            0, 4, // line 9
            1, 5, // line 10

            2, 6, // line 9
            3, 7, // line 10
        };

        _lineIndexBuffer = GraphicsFactory.CreateIndexBuffer(lineIndices.Length, lineIndices);

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LitBasic.shader");

        var physics = ServiceProvider.GetRequiredService<IPhysics>();

        _scene = new Scene(Logger, Renderer, physics);

        _scene.AddEntity(new FloorEntity(meshFactory, _shader, "Assets/Models/Plane/plane.obj"));
        //_scene.AddEntity(new CubeEntity(meshFactory, _shader, "Assets/Models/cube.obj"));
        _scene.AddEntity(new PhysicsEntity("Cube", meshFactory, _shader, "Assets/Models/cube.obj", 1.0f));
        _scene.AddEntity(new PhysicsEntity("Falling Cube", meshFactory, _shader, "Assets/Models/cube.obj", 15.0f, 100.0f));
        _scene.AddEntity(new FPSCameraEntity(new Vector3(0.0f, 1.0f, 5.0f)));

        _scene.RuntimeStart();

        _sceneHierarchy = new SceneHierarchy(_scene, Logger);
    }

    public override void OnShutdown()
    {
        _scene.RuntimeStop();
    }

    public override void OnUpdate(float deltaTime)
    {
        _shader.Bind();
        _shader.SetFloat3("_DirectionalLight.direction", _lightDirection);
        _shader.SetFloat3("_DirectionalLight.ambient", 0.05f, 0.05f, 0.05f);
        _shader.SetFloat3("_DirectionalLight.diffuse", 0.4f, 0.4f, 0.4f);
        _shader.SetFloat3("_DirectionalLight.specular", 1.0f, 1.0f, 1.0f);

        _scene.RuntimeUpdate(deltaTime);

        _lineShader.Bind();
        _lineShader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(0.0f, 0.5f, 0.0f)));
        Renderer.RenderGeometry(_linePipeline, _lineVertexBuffer, _lineIndexBuffer);
    }

    public override void OnGUI()
    {
        _sceneHierarchy.OnGUI();
    }
}
