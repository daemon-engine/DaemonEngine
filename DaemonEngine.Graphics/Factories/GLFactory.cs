using DaemonEngine.Graphics.Renderer;
using Serilog;

namespace DaemonEngine.Graphics.Factories;

internal class GLFactory : IGLFactory
{
    public GLFactory(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public Shader CreateShader()
    {
        return new Shader(Logger);
    }

    public Pipeline CreatePipeline()
    {
        return new Pipeline(Logger);
    }

    public IndexBuffer CreateIndexBuffer(int count, uint[] indices)
    {
        return new IndexBuffer(Logger, count, indices);
    }

    public VertexBuffer CreateVertexBuffer(int size, float[] vertices)
    {
        return new VertexBuffer(Logger, size, vertices);
    }
}
