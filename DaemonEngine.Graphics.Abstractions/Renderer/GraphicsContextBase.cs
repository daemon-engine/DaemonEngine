using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public interface IGraphicsContext
{
    void Initialize();
}

public abstract class GraphicsContextBase : IGraphicsContext
{
    public GraphicsContextBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger  Logger { get; }

    public abstract void Initialize();
}
