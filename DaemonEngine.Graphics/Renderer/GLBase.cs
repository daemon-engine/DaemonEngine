using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public abstract class GLBase
{
    public GLBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract void Bind();
    public abstract void Unbind();
}
