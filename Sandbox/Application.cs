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
    IPipeline _pipeline;
    IVertexBuffer _vertexBuffer;
    IIndexBuffer _ibo;

    public Application(ILogger logger, IWindow window, IRenderer renderer, IGraphicsFactory graphicsFactory)
        : base(logger, window, renderer, graphicsFactory)
    {
    }

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

        var shader = GraphicsFactory.CreateShader();

        _pipeline = GraphicsFactory.CreatePipeline(shader);
        _vertexBuffer = GraphicsFactory.CreateVertexBuffer(12 * sizeof(float), vertices);
        _ibo = GraphicsFactory.CreateIndexBuffer(6, indices);

        // Must happend after VBO is bound
        GL.VertexAttribPointer(0, 3, GLConstants.GL_FLOAT, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);

    }

    public override void OnShutdown()
    {
        Logger.Information("OnShutdown");
    }

    public override void OnUpdate(float deltaTime)
    {
        Renderer.Clear(DaemonEngine.Extensions.OpenGL.Enums.GLClearMask.ColorBufferBit);
        Renderer.ClearColor(0.3f, 0.5f, 0.8f, 1.0f);

        Renderer.RenderGeometry(_pipeline, _vertexBuffer, _ibo);
    }
}
