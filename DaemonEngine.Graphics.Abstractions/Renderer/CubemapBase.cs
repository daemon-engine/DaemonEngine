using DaemonEngine.Graphics.Renderer.Data;
using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public interface ICubemap
{
    void Bind();
    void Unbind();
}

public abstract class CubemapBase : ICubemap
{
    public CubemapBase(ILogger logger, CubemapOptions cubemapOptions)
    {
        Logger = logger;
        Options = cubemapOptions;
    }

    protected ILogger Logger { get; }
    protected CubemapOptions Options { get; }

    public abstract void Bind();
    public abstract void Unbind();
}
