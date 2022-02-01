using DaemonEngine.Extensions.OpenGL.Enums;

namespace DaemonEngine.Graphics.Renderer;

public interface IRenderer
{
    void Initialize();
    void Shutdown();

    void RenderGeometry(IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, int indexCount = 0);

    void SetViewport(int x, int y, int width, int height);
    void ClearColor(float red, float green, float blue, float alpha);
    void Clear(GLClearMask clearMask);
}
