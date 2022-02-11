﻿using DaemonEngine.Graphics.Renderer;

namespace DaemonEngine.Graphics.Renderer;

public interface IRenderer
{
    void Initialize();
    void Shutdown();

    void RenderGeometry(IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, int indexCount = 0);
    void RenderMesh(IMesh mesh);
    void SubmitFullscreenQuad(uint colorAttachment, IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer);

    void SetViewport(int x, int y, int width, int height);
    void ClearColor(float red, float green, float blue, float alpha);
    void Clear(ClearMask clearMask);
}
