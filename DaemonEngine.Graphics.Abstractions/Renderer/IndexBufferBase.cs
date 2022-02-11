using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public interface IIndexBuffer
{
    int Count { get; }

    void Bind();
    void Unbind();
}

public abstract class IndexBufferBase : IIndexBuffer
{
    public IndexBufferBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public int Count { get; protected set; }

    public abstract void Bind();
    public abstract void Unbind();
}
