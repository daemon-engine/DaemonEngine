using DaemonEngine.Graphics.Renderer.Enums;
using Serilog;

namespace DaemonEngine.Graphics.Renderer;

public abstract class RendererBase : IRenderer
{
    public RendererBase(ILogger logger)
    {
        Logger = logger;
    }

    protected ILogger Logger { get; }

    public abstract void Initialize();
    public abstract void Shutdown();

    public abstract void RenderGeometry(IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, int indexCount = 0);

    public abstract void SetViewport(int x, int y, int width, int height);
    public abstract void Clear(ClearMask clearMask);
    public abstract void ClearColor(float red, float green, float blue, float alpha);
}
