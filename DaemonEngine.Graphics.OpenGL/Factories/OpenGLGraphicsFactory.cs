using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.OpenGL.Renderer;
using DaemonEngine.Graphics.Renderer;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Factories;

internal class OpenGLGraphicsFactory : IGraphicsFactory
{
    public OpenGLGraphicsFactory(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public IShader CreateShader(string filepath)
    {
        return new OpenGLShader(filepath);
    }

    public IPipeline CreatePipeline(IShader shader, IBufferLayout bufferLayout)
    {
        return new OpenGLPipeline(shader, bufferLayout);
    }

    public IVertexBuffer CreateVertexBuffer(int size, float[] vertices)
    {
        return new OpenGLVertexBuffer(size, vertices);
    }

    public IIndexBuffer CreateIndexBuffer(int count, uint[] indices)
    {
        return new OpenGLIndexBuffer(count, indices);
    }
}
