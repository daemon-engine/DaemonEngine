﻿using DaemonEngine.Extensions.Runtime;
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
        Logger.Information("OpenGL Renderer Initializing");

        GL.Enable(GLCapabilities.DepthTest);
    }

    public override void Shutdown()
    {
        Logger.Information("OpenGL Renderer Shutting down");
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

    public override void SetViewport(int x, int y, int width, int height)
    {
        GL.Viewport(x, y, width, height);
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