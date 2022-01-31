using DaemonEngine;
using DaemonEngine.Windows;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using Serilog;
using System.Numerics;

namespace Sandbox;

public class Application : ApplicationBase
{
    IPipeline _pipeline;
    IVertexBuffer _vertexBuffer;
    IIndexBuffer _ibo;
    ITexture _texture1;
    ITexture _texture2;

    IShader _shader;

    float rotationX = 0f;
    float rotationY = 0f;

    public Application(ILogger logger, IWindow window, IRenderer renderer, IGraphicsFactory graphicsFactory)
        : base(logger, window, renderer, graphicsFactory)
    {
    }

    public override void OnStart()
    {
        Logger.Information("OnStart");

        var cubeVertices = Builder.GenerateCubeVertices();
        var cubeIndices = Builder.GenerateCubeIndices();

        //float[] vertices = new float[4 * (3 + 2)] {
        //     0.5f,  0.5f, 0.0f, 1.0f, 1.0f,  // top right
        //     0.5f, -0.5f, 0.0f, 1.0f, 0.0f,  // bottom right
        //    -0.5f, -0.5f, 0.0f, 0.0f, 0.0f,  // bottom left
        //    -0.5f,  0.5f, 0.0f, 0.0f, 1.0f   // top left 
        //};
        //uint[] indices = new uint[2 * 3] {  // note that we start from 0!
        //    0, 1, 3,   // first triangle
        //    1, 2, 3    // second triangle
        //};

        _shader = GraphicsFactory.CreateShader("Assets/Shaders/CoordinateSystem.shader");

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
        _shader.SetInt("Texture1", 0);
        _shader.SetInt("Texture2", 1);
    }

    public override void OnShutdown()
    {
        Logger.Information("OnShutdown");
    }

    public override void OnUpdate(float deltaTime)
    {
        rotationX += deltaTime;
        rotationY += deltaTime;

        Renderer.Clear(DaemonEngine.Extensions.OpenGL.Enums.GLClearMask.ColorBufferBit);
        Renderer.ClearColor(0.3f, 0.5f, 0.8f, 1.0f);

        Matrix4x4 model = Matrix4x4.CreateRotationX(rotationX) * Matrix4x4.CreateRotationY(rotationY);
        Matrix4x4 view = Matrix4x4.CreateTranslation(new Vector3(0.0f, 0.0f, -3.0f));
        Matrix4x4 projection = Matrix4x4.CreatePerspectiveFieldOfView(45.0f * (3.14f / 180.0f), Window.AspectRatio, 0.1f, 100.0f);

        _shader.Bind();
        _shader.SetMat4("_Model", model);
        _shader.SetMat4("_View", view);
        _shader.SetMat4("_Projection", projection);

        _texture1.Bind();
        _texture2.Bind(1);
        Renderer.RenderGeometry(_pipeline, _vertexBuffer, _ibo);
    }
}
