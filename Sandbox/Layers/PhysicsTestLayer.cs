using DaemonEngine.Core.Layer;
using DaemonEngine.Core.Scene;
using DaemonEngine.EventSystem;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Entities;

namespace Sandbox.Layers;

internal class PhysicsTestLayer : LayerBase
{
    private IShader _shader;

    private Scene _scene;
    private SceneHierarchy _sceneHierarchy;

    private Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);

    public PhysicsTestLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LitBasic.shader");

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();

        _scene = new Scene(Logger, Renderer, Physics);

        _scene.AddEntity(new FloorEntity(meshFactory, _shader, "Assets/Models/Plane/plane.obj"));
        _scene.AddEntity(new PhysicsEntity("Cube", meshFactory, _shader, "Assets/Models/cube.obj", Vector3.YAxis));
        _scene.AddEntity(new PhysicsEntity("Falling Cube", meshFactory, _shader, "Assets/Models/cube.obj", Vector3.YAxis * 15.0f, 1.0f));
        _scene.AddEntity(new PhysicsEntity("Falling Cube", meshFactory, _shader, "Assets/Models/cube.obj", new Vector3(-5.0f, 5.0f, 5.0f), 1.0f));
        _scene.AddEntity(new PhysicsEntity("Falling Cube", meshFactory, _shader, "Assets/Models/cube.obj", new Vector3(-5.0f, 200.0f, 5.0f), 1.0f));
        _scene.AddEntity(new PhysicsEntity("Falling Cube", meshFactory, _shader, "Assets/Models/cube.obj", new Vector3(5.0f, 200.0f, 5.0f), 10.0f));
        _scene.AddEntity(new SphereEntity(meshFactory, _shader, Vector3.YAxis * 20));
        _scene.AddEntity(new SphereEntity(meshFactory, _shader, Vector3.YAxis * 25));
        _scene.AddEntity(new SphereEntity(meshFactory, _shader, new Vector3(5.0f, 1.0f, 5.0f)));
        _scene.AddEntity(new MeshEntity("Assets/Models/Landscape/landscape.obj", Vector3.XAxis * 30.0f, meshFactory, _shader));
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
    }

    public override void OnEvent(IEvent e)
    {
        _scene.RuntimeEvent(e);
    }

    public override void OnGUI()
    {
        _sceneHierarchy.OnGUI();

        ImGuiNET.ImGui.Begin("Performance");

        var io = ImGuiNET.ImGui.GetIO();
        ImGuiNET.ImGui.Text($"FPS: {1.0f / io.DeltaTime:f1} f/sec");
        ImGuiNET.ImGui.Text($"Delta Time: {io.DeltaTime * 1000.0f:f3} ms/f");

        ImGuiNET.ImGui.Begin("Settings");

        var showColliders = Physics.ShowColliders;
        if (ImGuiNET.ImGui.Checkbox("Show Physics Colliders", ref showColliders))
        {
            Logger.Information($"Showing Physics Colliders: {showColliders}");
            Physics.ShowColliders = showColliders;
        }

        ImGuiNET.ImGui.End();
    }
}
