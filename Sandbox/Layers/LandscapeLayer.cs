using DaemonEngine.Core.Layer;
using DaemonEngine.Core.Scene;
using DaemonEngine.ECS.Components;
using DaemonEngine.EventSystem;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;
using Sandbox.Entities;

namespace Sandbox.Layers;

internal class LandscapeLayer : LayerBase
{
    private IShader _shader;

    private readonly Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);

    private Scene _scene;
    private SceneHierarchy _sceneHierarchy;

    public LandscapeLayer(string name, IServiceProvider serviceProvider) 
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LitBasic.shader");

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();

        _scene = new Scene(Logger, Renderer, Physics);
        _sceneHierarchy = new SceneHierarchy(_scene, Logger);

        var landscapeEntity = new MeshEntity("Assets/Models/Landscape/landscape.obj", Vector3.Zero, meshFactory, _shader);
        landscapeEntity.AddComponent<Rigidbody>();
        landscapeEntity.AddComponent<MeshCollider>();
        _scene.AddEntity(landscapeEntity);

        _scene.AddEntity(new SphereEntity(meshFactory, _shader, new Vector3(0.0f, 20.0f, 0.0f)));

        _scene.AddEntity(new FPSCameraEntity(Vector3.YAxis * 10.0f));

        _scene.RuntimeStart();
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

    public override void OnGUI()
    {
        _sceneHierarchy.OnGUI();
    }

    public override void OnEvent(IEvent e)
    {
        _scene.RuntimeEvent(e);
    }
}
