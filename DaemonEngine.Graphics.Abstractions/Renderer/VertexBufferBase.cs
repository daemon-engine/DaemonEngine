using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public interface IVertexBuffer
{
    void Bind();
    void Unbind();
}

public abstract class VertexBufferBase : IVertexBuffer
{
    public VertexBufferBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract void Bind();
    public abstract void Unbind();
}
