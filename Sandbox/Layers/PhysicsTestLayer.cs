using BepuPhysics;
using BepuPhysics.Collidables;
using DaemonEngine.Core.Layer;
using DaemonEngine.Extensions.bepuphysics2;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers;

internal class PhysicsTestLayer : LayerBase
{
    private IShader _shader;

    private Model _plane;
    private Model _cube;
    private BodyDescription _cubeBody;

    private Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);
    private FPSCamera _camera;

    public PhysicsTestLayer(string name, IServiceProvider serviceProvider) 
        : base(name, serviceProvider)
    {
    }

    protected PhysicsTest Physics { get; set; }

    public override void OnStart()
    {
        _camera = new FPSCamera(60.0f, Window.AspectRatio);

        Physics = new PhysicsTest();

        var planeShape = Physics.Simulation.Shapes.Add(new Box(10.0f, 1.0f, 10.0f));
        Physics.Simulation.Statics.Add(new StaticDescription(new System.Numerics.Vector3(0.0f, 0.0f, 0.0f), planeShape, 0.01f));

        var cubeShape = new Box(1.0f, 1.0f, 1.0f);
        cubeShape.ComputeInertia(1.0f, out BodyInertia bodyInertia);
        var collidableDescription = new CollidableDescription(Physics.Simulation.Shapes.Add(cubeShape), 0.01f);
        var bodyActivityDescription = new BodyActivityDescription(1.0f);
        _cubeBody = BodyDescription.CreateDynamic(new Vector3(0.0f, 5.0f, 0.0f), bodyInertia, collidableDescription, bodyActivityDescription);

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter3/LitBasic.shader");
        _plane = new Model(meshFactory, _shader, "Assets/Models/Plane/plane.obj");
        _cube = new Model(meshFactory, _shader, "Assets/Models/cube.obj");
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _camera.Update(deltaTime);

        Physics.Update();

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
        Renderer.ClearColor(0.3f, 0.5f, 0.8f, 1.0f);

        _shader.Bind();
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        _shader.SetFloat3("_ViewPos", _camera.Position);

        _shader.SetFloat3("_DirectionalLight.direction", _lightDirection);
        _shader.SetFloat3("_DirectionalLight.ambient", 0.05f, 0.05f, 0.05f);
        _shader.SetFloat3("_DirectionalLight.diffuse", 0.4f, 0.4f, 0.4f);
        _shader.SetFloat3("_DirectionalLight.specular", 1.0f, 1.0f, 1.0f);

        _shader.SetFloat3("_Material.ambient", new Vector3(0.24725f, 0.1995f, 0.0745f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.75164f, 0.60648f, 0.22648f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.628281f, 0.555802f, 0.366065f));
        _shader.SetFloat("_Material.shininess", 0.4f * 128.0f);

        _shader.SetMat4("_Model", Matrix4.Identity);
        Renderer.RenderMesh(_plane.Mesh);

        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(_cubeBody.Pose.Position));
        Renderer.RenderMesh(_cube.Mesh);
    }
}
