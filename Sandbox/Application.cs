using DaemonEngine;
using DaemonEngine.Windows;
using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.Renderer;
using Serilog;

// Temp
using DaemonEngine.Extensions.OpenGL;

namespace Sandbox;

public class Application : ApplicationBase
{
    Pipeline? _pipeline;
    IndexBuffer? _ibo;
    Shader? _shader;

    public Application(ILogger logger, IWindow window, IGLFactory factory)
        : base(logger, window)
    {
        GLFactory = factory;
    }

    protected IGLFactory GLFactory { get; }

    public override void OnStart()
    {
        Logger.Information("OnStart");

        float[] vertices = new float[4 * 3] {
             0.5f,  0.5f, 0.0f,  // top right
             0.5f, -0.5f, 0.0f,  // bottom right
            -0.5f, -0.5f, 0.0f,  // bottom left
            -0.5f,  0.5f, 0.0f   // top left 
        };
        uint[] indices = new uint[2 * 3] {  // note that we start from 0!
            0, 1, 3,   // first triangle
            1, 2, 3    // second triangle
        };

        _pipeline = GLFactory.CreatePipeline();
        _ = GLFactory.CreateVertexBuffer(12 * sizeof(float), vertices);
        _ibo = GLFactory.CreateIndexBuffer(6, indices);

        // Must happend after VBO is bound
        GL.VertexAttribPointer(0, 3, GLConstants.GL_FLOAT, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

        _shader = GLFactory.CreateShader();
    }

    public override void OnShutdown()
    {
        Logger.Information("OnShutdown");
    }

    public override void OnUpdate(float deltaTime)
    {
        RenderCommand.Clear();
        RenderCommand.ClearColor(0.3f, 0.5f, 0.8f, 1.0f);

        _shader?.Bind();
        _pipeline?.Bind();
        _ibo?.Bind();
#pragma warning disable CS8604 // Possible null reference argument.
        RenderCommand.DrawIndexed(_pipeline, _ibo);
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
