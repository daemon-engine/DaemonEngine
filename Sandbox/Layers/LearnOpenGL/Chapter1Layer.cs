using DaemonEngine.Core;
using DaemonEngine.Core.Layer;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.Windows.Inputs;
using Microsoft.Extensions.DependencyInjection;
using System.Numerics;

namespace Sandbox.Layers.LearnOpenGL;

public class Chapter1Layer : LayerBase
{
    IPipeline _pipeline;
    IVertexBuffer _vertexBuffer;
    IIndexBuffer _ibo;
    ITexture _texture1;
    ITexture _texture2;

    IShader _shader;

    Vector3[] _cubePositions = new Vector3[10]
    {
        new Vector3( 0.0f,  0.0f,  0.0f),
        new Vector3( 2.0f,  5.0f, -15.0f),
        new Vector3(-1.5f, -2.2f, -2.5f),
        new Vector3(-3.8f, -2.0f, -12.3f),
        new Vector3( 2.4f, -0.4f, -3.5f),
        new Vector3(-1.7f,  3.0f, -7.5f),
        new Vector3( 1.3f, -2.0f, -2.5f),
        new Vector3( 1.5f,  2.0f, -2.5f),
        new Vector3( 1.5f,  0.2f, -1.5f),
        new Vector3(-1.3f,  1.0f, -1.5f)
    };

    private FPSCamera _camera;

    public Chapter1Layer(string name, IServiceProvider serviceProvider) 
        : base(name, serviceProvider)
    {
    }

    public override void OnStart()
    {
        var cursor = ServiceProvider.GetService<ICursor>();
        cursor.Disable();

        Logger.Information($"{Name}.OnStart()");

        var cubeVertices = Builder.GenerateCubeVertices();
        var cubeIndices = Builder.GenerateCubeIndices();

        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter1/CoordinateSystem.shader");

        _texture1 = GraphicsFactory.CreateTexture("Assets/Textures/container.jpg");
        _texture2 = GraphicsFactory.CreateTexture("Assets/Textures/awesomeface.png");

        var layout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("NORMALS", ShaderDataType.Float3),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });
        _pipeline = GraphicsFactory.CreatePipeline(_shader, layout);
        _vertexBuffer = GraphicsFactory.CreateVertexBuffer(192 * sizeof(float), cubeVertices);
        _ibo = GraphicsFactory.CreateIndexBuffer(36, cubeIndices);

        _shader.Bind();
        _shader.SetInt("_Texture1", 0);
        _shader.SetInt("_Texture2", 1);

        //_camera = new FPSCamera(45.0f, Window.AspectRatio);
    }

    public override void OnShutdown()
    {
        Logger.Information($"{Name}.OnShutdown()");
    }

    public override void OnUpdate(float deltaTime)
    {
        _camera.Update(deltaTime);

        Renderer.Clear(ClearMask.ColorBufferBit | ClearMask.DepthBufferBit);
        Renderer.ClearColor(0.3f, 0.5f, 0.8f, 1.0f);

        for (int i = 0; i < 10; i++)
        {
            float angle = 20.0f * i;

            Matrix4x4 model = Matrix4x4.Identity
                * Matrix4x4.CreateTranslation(_cubePositions[i])
                * Matrix4x4.CreateRotationX(angle / 0.01745329251f)
                * Matrix4x4.CreateRotationY(angle / 0.01745329251f)
                * Matrix4x4.CreateRotationZ(angle / 0.01745329251f);

            _shader.Bind();
            _shader.SetMat4("_Model", model);
            _shader.SetMat4("_View", _camera.ViewMatrix);
            _shader.SetMat4("_Projection", _camera.ProjectionMatrix);

            _texture1.Bind();
            _texture2.Bind(1);
            Renderer.RenderGeometry(_pipeline, _vertexBuffer, _ibo);
        }
    }

    public override void OnEvent(IEvent e)
    {
        var dispatcher = new EventDispatcher(e);
        dispatcher.Dispatch<WindowResizeEvent>(OnWindowResizeEvent);
    }

    private bool OnWindowResizeEvent(WindowResizeEvent e)
    {
        _camera.SetViewport(e.Width, e.Height);
        return true;
    }
}
