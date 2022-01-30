using DaemonEngine.Extensions.OpenGL.Enums;
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

    public abstract void Clear(GLClearMask clearMask);
    public abstract void ClearColor(float red, float green, float blue, float alpha);
}
