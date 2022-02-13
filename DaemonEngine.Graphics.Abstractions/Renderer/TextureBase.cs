using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public interface ITexture
{
    void SetData(int size, IntPtr data);

    void Bind(uint slot = 0);
    void Unbind();

    /// Temp
    uint GetId();
}

public abstract class TextureBase : ITexture
{
    public TextureBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract uint GetId();
    public abstract void SetData(int size, IntPtr data);

    public abstract void Bind(uint slot = 0);
    public abstract void Unbind();
}
