using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Graphics.Renderer.Enums;
using DaemonEngine.Graphics.OpenGL.Helpers;
using DaemonEngine.Graphics.Renderer;
using DaemonEngine.OpenGL.DllImport;
using DaemonEngine.OpenGL.DllImport.Enums;
using Serilog;

namespace DaemonEngine.Graphics.OpenGL.Renderer;

internal class OpenGLRenderer : RendererBase
{
    public OpenGLRenderer(ILogger logger)
        : base(logger)
    {
    }

    public override void Initialize()
    {
        GL.Enable(GLCapabilities.DepthTest);
    }

    public override void Shutdown()
    {
    }

    public override void RenderGeometry(IPipeline pipeline, IVertexBuffer vertexBuffer, IIndexBuffer indexBuffer, int indexCount = 0)
    {
        Throw.IfNull(pipeline, nameof(pipeline));
        Throw.IfNull(vertexBuffer, nameof(vertexBuffer));
        Throw.IfNull(indexBuffer, nameof(indexBuffer));

        pipeline.Bind();
        indexBuffer.Bind();

        var count = indexCount == 0 ? indexBuffer.Count : indexCount;
        GL.DrawElements(GLConstants.GL_TRIANGLES, count, GLConstants.GL_UNSIGNED_INT);
    }

    public override void RenderMesh(IMesh mesh)
    {
        Throw.IfNull(mesh, nameof(mesh));

        mesh.Bind();

        var count = mesh.GetIndexBufferCount();
        GL.DrawElements(GLConstants.GL_TRIANGLES, count, GLConstants.GL_UNSIGNED_INT);
    }

    public override void SetViewport(int x, int y, int width, int height)
    {
        GL.Viewport(x, y, width, height);
    }

    public override void Clear(ClearMask clearMask)
    {
        GL.Clear(OpenGLHelper.ClearMaskToOpenGLType(clearMask));
    }

    public override void ClearColor(float red, float green, float blue, float alpha)
    {
        GL.ClearColor(red, green, blue, alpha);
    }
}
