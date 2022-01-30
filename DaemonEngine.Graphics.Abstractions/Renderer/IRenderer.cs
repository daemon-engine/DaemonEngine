using DaemonEngine.Extensions.OpenGL.Enums;

namespace DaemonEngine.Graphics.Renderer;

public interface IRenderer
{
    void RenderGeometry(IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, int indexCount = 0);

    void ClearColor(float red, float green, float blue, float alpha);
    void Clear(GLClearMask clearMask);
}
