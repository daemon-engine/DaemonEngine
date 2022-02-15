using DaemonEngine.Core.Layer;
using DaemonEngine.Core.Scene;
using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using DaemonEngine.Scripting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Sandbox.Layers;

internal class CubeEntity : EntityBase
{
    public CubeEntity(IMeshFactory meshFactory, IShader shader, string modelFilepath) 
        : base("Cube")
    {
        var transform = AddComponent<Transform>();
        transform.Position = new Vector3(0.0f, 1.0f, 0.0f);

        var meshRenderer = AddComponent<MeshRenderer>();

        meshRenderer.Shader = shader;
        meshRenderer.Ambient = new Vector3(0.1745f, 0.01175f, 0.01175f);
        meshRenderer.Diffuse = new Vector3(0.61424f, 0.04136f, 0.04136f);
        meshRenderer.Specular = new Vector3(0.727811f, 0.626959f, 0.626959f);
        meshRenderer.Shininess = 0.6f;

        meshRenderer.Model = new Model(meshFactory, shader, modelFilepath);
    }
}

internal class FloorEntity : EntityBase
{
    public FloorEntity(IMeshFactory meshFactory, IShader shader, string modelFilepath) 
        : base("Floor")
    {
        var transform = AddComponent<Transform>();
        transform.Position = new Vector3(0.0f, 0.0f, 0.0f);

        var meshRenderer = AddComponent<MeshRenderer>();

        meshRenderer.Shader = shader;
        meshRenderer.Ambient = new Vector3(0.1f, 0.18725f, 0.1745f);
        meshRenderer.Diffuse = new Vector3(0.396f, 0.74151f, 0.69102f);
        meshRenderer.Specular = new Vector3(0.297254f, 0.30829f, 0.306678f);
        meshRenderer.Shininess = 0.1f;

        meshRenderer.Model = new Model(meshFactory, shader, modelFilepath);
    }
}

internal class SceneTestLayer : LayerBase
{
    private Scene _scene;

    private IShader _shader;
    //private FPSCamera _camera;
    private readonly Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);

    public SceneTestLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        //_camera = new FPSCamera(60.0f, Window.AspectRatio);
        //_camera.Position.Y = 1.0f;

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LitBasic.shader");

        _scene = new Scene(Logger, Renderer);

        _scene.AddEntity(new FloorEntity(meshFactory, _shader, "Assets/Models/Plane/plane.obj"));
        _scene.AddEntity(new CubeEntity(meshFactory, _shader, "Assets/Models/cube.obj"));
        _scene.AddEntity(new FPSCameraEntity(new Vector3(0.0f, 1.0f, 5.0f)));

        _scene.RuntimeStart();
    }

    public override void OnShutdown()
    {
        _scene.RuntimeStop();
    }

    public override void OnUpdate(float deltaTime)
    {
        //_camera.Update(deltaTime);

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
        Renderer.ClearColor(0.3f, 0.4f, 0.8f, 1.0f);

        _shader.Bind();
        _shader.SetFloat3("_DirectionalLight.direction", _lightDirection);
        _shader.SetFloat3("_DirectionalLight.ambient", 0.05f, 0.05f, 0.05f);
        _shader.SetFloat3("_DirectionalLight.diffuse", 0.4f, 0.4f, 0.4f);
        _shader.SetFloat3("_DirectionalLight.specular", 1.0f, 1.0f, 1.0f);

        _scene.RuntimeUpdate(deltaTime);
    }

    public override void OnGUI()
    {
    }
}
