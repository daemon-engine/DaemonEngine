using DaemonEngine.Application;
using DaemonEngine.Core.Inputs;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Key;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.Mathematics;
using Microsoft.Extensions.DependencyInjection;

namespace Sandbox.Layers.LearnOpenGL;

internal class Chapter3Layer : LayerBase
{
    private Mesh _mesh;
    private Model _model;
    private Model _sphere;
    private FPSCamera _camera;
    private IShader _shader;

    private readonly IMeshFactory _meshFactory;
    private readonly IApplication _application;

    public Chapter3Layer(string name, IServiceProvider serviceProvider) 
        : base(name, serviceProvider)
    {
        _meshFactory = ServiceProvider.GetRequiredService<IMeshFactory>();
        _application = ServiceProvider.GetRequiredService<IApplication>();
    }

    public override void OnStart()
    {
        _camera = new FPSCamera(45.0f, Window.AspectRatio);

        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter3/Basic.shader");

        var bufferLayout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("NORMAL", ShaderDataType.Float3),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });

        var vertices = Builder.GenerateCubeVertices();
        var indices = Builder.GenerateCubeIndices();
        _mesh = _meshFactory.CreateMesh(vertices, indices, _shader, bufferLayout);

        _model = new Model(_meshFactory, _shader, bufferLayout, "Assets/Models/cube.obj");
        _sphere = new Model(_meshFactory, _shader, bufferLayout, "Assets/Models/Sphere/sphere.obj");
    }

    public override void OnShutdown()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
        _camera.Update(deltaTime);

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
        Renderer.ClearColor(0.3f, 0.5f, 0.8f, 1.0f);

        _shader.Bind();
        _shader.SetMat4("_Model", Matrix4.Identity);
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        Renderer.RenderMesh(_mesh);

        //_shader.Bind();
        _shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(2.5f, 0.0f, 2.5f)));
        _shader.SetMat4("_View", _camera.ViewMatrix);
        _shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        Renderer.RenderMesh(_model.Mesh);

        //_shader.Bind();
        //_shader.SetMat4("_Model", Matrix4.Identity * Matrix4.Translate(new Vector3(-2.5f, 0.0f, -2.5f)));
        //_shader.SetMat4("_View", _camera.ViewMatrix);
        //_shader.SetMat4("_Projection", _camera.ProjectionMatrix);
        //Renderer.RenderMesh(_sphere.Mesh);
    }

    public override void OnEvent(IEvent e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<KeyPressedEvent>(OnKeyPressedEvent);
    }

    private bool OnKeyPressedEvent(KeyPressedEvent e)
    {
        if(e.KeyCode == (int)Keycode.ESCAPE)
        {
            _application.Stop();
        }
        return true;
    }
}
