using BepuPhysics;
using BepuPhysics.Collidables;
using DaemonEngine.Core.Inputs;
using DaemonEngine.Core.Layer;
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

    private BodyHandle _cubeBodyHandle;

    private Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);
    private FPSCamera _camera;

    public PhysicsTestLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        _camera = new FPSCamera(60.0f, Window.AspectRatio);
        _camera.Position.Y = 5.0f;

        World.AddStatic();
        _cubeBodyHandle = (BodyHandle)World.AddDynamic();

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

        World.Step();

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

        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3()));
        Renderer.RenderMesh(_plane.Mesh);

        _shader.SetFloat3("_Material.ambient", new Vector3(0.0215f, 0.1745f, 0.0215f));
        _shader.SetFloat3("_Material.diffuse", new Vector3(0.07568f, 0.61424f, 0.07568f));
        _shader.SetFloat3("_Material.specular", new Vector3(0.633f, 0.727811f, 0.633f));
        _shader.SetFloat("_Material.shininess", 0.6f * 128.0f);

        BodyReference bodyRef = (BodyReference)World.GetBodyReference(_cubeBodyHandle);
        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(bodyRef.Pose.Position));
        Renderer.RenderMesh(_cube.Mesh);
    }
}
