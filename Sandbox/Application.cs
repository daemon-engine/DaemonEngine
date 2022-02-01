using DaemonEngine.Windows;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using Serilog;
using System.Numerics;
using DaemonEngine.EventSystem;
using DaemonEngine.EventSystem.Events.Window;
using DaemonEngine.Windows.Inputs;
using DaemonEngine.Application;

namespace Sandbox;

public class Application : ApplicationBase
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

    public Application(ILogger logger, IWindow window, IInput input, IRenderer renderer, IGraphicsFactory graphicsFactory)
        : base(logger, window, input, renderer, graphicsFactory)
    {
    }

    public override void OnStart()
    {
        Logger.Information("OnStart");

        var cubeVertices = Builder.GenerateCubeVertices();
        var cubeIndices = Builder.GenerateCubeIndices();

        _shader = GraphicsFactory.CreateShader("Assets/Shaders/LearnOpenGL/Chapter1/CoordinateSystem.shader");

        _texture1 = GraphicsFactory.CreateTexture("Assets/Textures/container.jpg");
        _texture2 = GraphicsFactory.CreateTexture("Assets/Textures/awesomeface.png");

        var layout = new BufferLayout(new List<BufferElement>
        {
            new BufferElement("POSITION", ShaderDataType.Float3),
            new BufferElement("TEXCOORD", ShaderDataType.Float2)
        });
        _pipeline = GraphicsFactory.CreatePipeline(_shader, layout);
        _vertexBuffer = GraphicsFactory.CreateVertexBuffer(120 * sizeof(float), cubeVertices);
        _ibo = GraphicsFactory.CreateIndexBuffer(36, cubeIndices);

        _shader.Bind();
        _shader.SetInt("_Texture1", 0);
        _shader.SetInt("_Texture2", 1);

        _camera = new FPSCamera(45.0f, Window.AspectRatio, Input);
    }

    public override void OnShutdown()
    {
        Logger.Information("OnShutdown");
    }

    public override void OnUpdate(float deltaTime)
    {
        _camera.Update(deltaTime);

        Renderer.Clear(DaemonEngine.Extensions.OpenGL.Enums.GLClearMask.ColorBufferBit | DaemonEngine.Extensions.OpenGL.Enums.GLClearMask.DepthBufferBit);
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
        dispatcher.Dispatch<WindowCloseEvent>(OnWindowCloseEvent);

        base.OnEvent(e);
    }

    private bool OnWindowResizeEvent(WindowResizeEvent e)
    {
        _camera.SetViewport(e.Width, e.Height);
        return true;
    }

    private bool OnWindowCloseEvent(WindowCloseEvent e)
    {
        Logger.Information("WindowCloseEvent invoked");
        Close();
        return true;
    }
}
