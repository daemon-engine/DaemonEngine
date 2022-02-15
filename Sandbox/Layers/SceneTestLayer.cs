using DaemonEngine.Core.Layer;
using DaemonEngine.Core.Scene;
using DaemonEngine.ECS;
using DaemonEngine.ECS.Components;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers;

internal class FloorEntity : EntityBase
{
    public FloorEntity(string name, IMeshFactory meshFactory, IShader shader, string modelFilepath) 
        : base(name)
    {
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
    private FPSCamera _camera;
    private readonly Vector3 _lightDirection = new(-0.2f, -1.0f, -0.3f);

    public SceneTestLayer(string name, IServiceProvider serviceProvider)
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        _camera = new FPSCamera(60.0f, Window.AspectRatio);

        var meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter3/LitBasic.shader");

        _scene = new Scene(Logger, Renderer);

        _scene.AddEntity(new FloorEntity("Floor", meshFactory, _shader, "Assets/Models/Plane/plane.obj"));

        var cubeEntity = _scene.CreateEntity("Cube");
        var cubeMeshRenderer = cubeEntity.AddComponent<MeshRenderer>();
        cubeMeshRenderer.Shader = _shader;
        cubeMeshRenderer.Ambient = new Vector3(0.1745f, 0.01175f, 0.01175f);
        cubeMeshRenderer.Diffuse = new Vector3(0.61424f, 0.04136f, 0.04136f);
        cubeMeshRenderer.Specular = new Vector3(0.727811f, 0.626959f, 0.626959f);
        cubeMeshRenderer.Shininess = 0.6f;
        cubeMeshRenderer.Model = new Model(meshFactory, _shader, "Assets/Models/cube.obj");
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _camera.Update(deltaTime);

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
        Renderer.ClearColor(0.3f, 0.4f, 0.8f, 1.0f);

        _shader.Bind();
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        _shader.SetFloat3("_ViewPos", _camera.Position);

        _shader.SetFloat3("_DirectionalLight.direction", _lightDirection);
        _shader.SetFloat3("_DirectionalLight.ambient", 0.05f, 0.05f, 0.05f);
        _shader.SetFloat3("_DirectionalLight.diffuse", 0.4f, 0.4f, 0.4f);
        _shader.SetFloat3("_DirectionalLight.specular", 1.0f, 1.0f, 1.0f);

        _scene.Update(deltaTime);
    }

    public override void OnGUI()
    {
    }
}
