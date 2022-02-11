using DaemonEngine.Graphics.Renderer.Data;
using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public interface IFramebuffer
{
    uint GetColorAttachment(int index);

    void Resize(int width, int height);
    void Clear();

    void Bind();
    void Unbind();
}


public abstract class FramebufferBase : IFramebuffer
{
    public FramebufferBase(ILogger logger, FramebufferOptions framebufferOptions)
    {
        Logger = logger;
        Options = framebufferOptions;
    }

    protected ILogger Logger { get; }
    protected FramebufferOptions Options { get; }

    public abstract uint GetColorAttachment(int index);

    public abstract void Resize(int width, int height);
    public abstract void Clear();

    public abstract void Bind();
    public abstract void Unbind();
}
