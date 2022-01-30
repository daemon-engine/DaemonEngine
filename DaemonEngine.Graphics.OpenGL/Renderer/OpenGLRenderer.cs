using DaemonEngine.Extensions.OpenGL;
using DaemonEngine.Extensions.OpenGL.Enums;
using DaemonEngine.Extensions.Runtime;
using DaemonEngine.Graphics.Renderer;
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
        Logger.Information("OpenGL Renderer Initializing");
    }

    public override void Shutdown()
    {
        Logger.Information("OpenGL Renderer Shutting down");
    }

    public override void RenderGeometry(IPipeline? pipeline, IVertexBuffer? vertexBuffer, IIndexBuffer? indexBuffer, int indexCount = 0)
    {
        Throw.IfNull(pipeline, nameof(pipeline));
        Throw.IfNull(vertexBuffer, nameof(vertexBuffer));
        Throw.IfNull(indexBuffer, nameof(indexBuffer));

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        pipeline.Bind();
        indexBuffer.Bind();

        var count = indexCount == 0 ? indexBuffer.Count : indexCount;
        GL.DrawElements(GLConstants.GL_TRIANGLES, count, GLConstants.GL_UNSIGNED_INT);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    public override void Clear(GLClearMask clearMask)
    {
        GL.Clear(clearMask);
    }

    public override void ClearColor(float red, float green, float blue, float alpha)
    {
        GL.ClearColor(red, green, blue, alpha);
    }
}
