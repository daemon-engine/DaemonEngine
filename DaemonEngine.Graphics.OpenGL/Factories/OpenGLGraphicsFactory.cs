using DaemonEngine.Graphics.Factories;
using DaemonEngine.Graphics.OpenGL.Renderer;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.Graphics.Renderer.Data;
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
    public IShader CreateShader(string vertexShaderSource, string fragmentShaderSource)
    {
        return new OpenGLShader(vertexShaderSource, fragmentShaderSource);
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

    public IFramebuffer CreateFramebuffer(FramebufferOptions framebufferOptions)
    {
        return new OpenGLFramebuffer(framebufferOptions);
    }

    public ITexture CreateTexture(string filepath)
    {
        return new OpenGLTexture(filepath);
    }
    public ITexture CreateTexture(int width, int height)
    {
        return new OpenGLTexture(width, height);
    }
}
