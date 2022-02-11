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
        return new OpenGLShader(Logger, filepath);
    }
    public IShader CreateShader(string vertexShaderSource, string fragmentShaderSource)
    {
        return new OpenGLShader(Logger, vertexShaderSource, fragmentShaderSource);
    }

    public IPipeline CreatePipeline(PipelineOptions pipelineOptions)
    {
        return new OpenGLPipeline(Logger, pipelineOptions);
    }

    public IVertexBuffer CreateVertexBuffer(int size, float[] vertices)
    {
        return new OpenGLVertexBuffer(size, vertices);
    }

    public IIndexBuffer CreateIndexBuffer(int count, uint[] indices)
    {
        return new OpenGLIndexBuffer(Logger, count, indices);
    }

    public IFramebuffer CreateFramebuffer(FramebufferOptions framebufferOptions)
    {
        return new OpenGLFramebuffer(Logger, framebufferOptions);
    }

    public ICubemap CreateCubemap(CubemapOptions cubemapOptions)
    {
        return new OpenGLCubemap(Logger, cubemapOptions);
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
